// transmission-remote-dotnet
// http://code.google.com/p/transmission-remote-dotnet/
// Copyright (C) 2009 Alan F
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using Jayrock.Json;
using Troschuetz;
using TransmissionRemoteDotnet.Settings;

namespace TransmissionRemoteDotnet
{
    static class Program
    {
        private const int TCP_SINGLE_INSTANCE_PORT = 24452;
        private const string APPLICATION_GUID = "{1a4ec788-d8f8-46b4-bb6b-598bc39f6307}";

        public static event EventHandler OnConnStatusChanged;
        public static event EventHandler OnTorrentsUpdated;
        public static event EventHandler OnError;

        private static Boolean connected = false;
        private static DateTime startupTime;

        private static UICultureChanger culturechanger = new UICultureChanger();
        public static UICultureChanger CultureChanger
        {
            get { return Program.culturechanger; }
        }

        private static MainWindow form;
        public static MainWindow Form
        {
            get { return Program.form; }
        }

        private static LocalSettings settings = new LocalSettings();
        public static LocalSettings Settings
        {
            get { return Program.settings; }
            set { Program.settings = value; }
        }

        private static Dictionary<string, Torrent> torrentIndex = new Dictionary<string, Torrent>();
        public static Dictionary<string, Torrent> TorrentIndex
        {
            get { return Program.torrentIndex; }
        }

        private static TransmissionDaemonDescriptor daemonDescriptor = new TransmissionDaemonDescriptor();
        public static TransmissionDaemonDescriptor DaemonDescriptor
        {
            get { return Program.daemonDescriptor; }
            set { Program.daemonDescriptor = value; }
        }

        private static List<LogListViewItem> logItems = new List<LogListViewItem>();
        public static List<LogListViewItem> LogItems
        {
            get { return Program.logItems; }
        }

        private static string[] uploadArgs;
        public static string[] UploadArgs
        {
            get { return Program.uploadArgs; }
            set { Program.uploadArgs = value; }
        }

        private static bool uploadPrompt;
        public static bool UploadPrompt
        {
            get { return Program.uploadPrompt; }
            set { Program.uploadPrompt = value; }
        }


        [STAThread]
        static void Main(string[] args)
        {
            startupTime = DateTime.Now;
#if DEBUG
            // In debug builds we'd prefer to have it dump us into the debugger
#else
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.Automatic);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
#endif

            culturechanger.ApplyHelp = culturechanger.ApplyText = culturechanger.ApplyToolTip = true;
            culturechanger.ApplyLocation = culturechanger.ApplySize = false;
            settings = LocalSettings.TryLoad();
            uploadPrompt = settings.UploadPrompt;
            args = Array.FindAll<string>(args, delegate(string str) { return !str.Equals("/m"); });
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(settings.Locale, true);
#if DOTNET35
            using (NamedPipeSingleInstance singleInstance = new TCPSingleInstance(TCP_SINGLE_INSTANCE_PORT))
#else
            using (TCPSingleInstance singleInstance = new TCPSingleInstance(TCP_SINGLE_INSTANCE_PORT))
#endif
            {
                if (singleInstance.IsFirstInstance)
                {
                    try
                    {
                        ServicePointManager.ServerCertificateValidationCallback = TransmissionWebClient.ValidateServerCertificate;
                    }
                    catch
                    {
#if MONO
#pragma warning disable 618
                        ServicePointManager.CertificatePolicy = new PromiscuousCertificatePolicy();
#pragma warning restore 618
#endif
                    }
                    ServicePointManager.Expect100Continue = false;

                    /* Store a list of torrents to upload after connect? */
                    if (args.Length > 0)
                    {
                        Program.uploadArgs = args;
                    }
                    singleInstance.ArgumentsReceived += singleInstance_ArgumentsReceived;
                    singleInstance.ListenForArgumentsFromSuccessiveInstances();
                    SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(form = new MainWindow());
                }
                else
                {
                    try
                    {
                        singleInstance.PassArgumentsToFirstInstance(args);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Unable to communicate with first instance", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        static bool resumeconnect = false;
        static void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Suspend:
                    resumeconnect = Connected;
                    Connected = false;
                    Thread.Sleep(300);
                    break;
                case PowerModes.Resume:
                    Ping resumepinger = new Ping();
                    int counter = 50;
                    resumepinger.PingCompleted += delegate(object pingsender, PingCompletedEventArgs pinge)
                    {
                        if (!pinge.Cancelled && pinge.Error != null)
                        {
                            if (pinge.Reply.Status == IPStatus.Success)
                                Connected = resumeconnect;
                            else if (--counter > 0)
                                resumepinger.SendAsync("127.0.0.1", 100);
                        }
                    };
                    resumepinger.SendAsync("127.0.0.1", 100);
                    Thread.Sleep(5);
                    break;
            }
        }

        public static void Log(string title, string body)
        {
            Log(title, body, -1);
        }

        public static void LogDebug(string title, string body)
        {
            Log(title, body, -1, true);
        }

        public static void Log(string title, string body, long UpdateSerial)
        {
            Log(title, body, UpdateSerial, false);
        }

        public static void Log(string title, string body, long UpdateSerial, bool debug)
        {
            DateTime dt = DateTime.Now;
            LogListViewItem logItem = new LogListViewItem(dt.ToString() + "." + dt.Millisecond);
            logItem.UpdateSerial = UpdateSerial;
            logItem.Debug = debug;
            logItem.SubItems.Add(title);
            logItem.SubItems.Add(body);
            lock (logItems)
            {
                logItems.Add(logItem);
            }
            if (OnError != null)
            {
                OnError(null, null);
            }
        }

        static void singleInstance_ArgumentsReceived(object sender, ArgumentsReceivedEventArgs e)
        {
            if (form != null)
            {
                if (e.Args.Length > 0)
                {
                    if (connected)
                    {
                        form.Upload(e.Args, UploadPrompt);
                    }
                    else
                    {
                        form.ShowMustBeConnectedDialog(uploadArgs = e.Args, UploadPrompt);
                    }
                }
                else
                {
                    form.InvokeShow();
                }
            }
        }

        public static void RaisePostUpdateEvent()
        {
            if (OnTorrentsUpdated != null)
            {
                OnTorrentsUpdated(null, null);
            }
        }

        public static bool Connected
        {
            set
            {
                if (value.Equals(connected))
                    return;
                connected = value;
                if (connected)
                {
                    ProtocolConstants.SetupStatusValues(Program.DaemonDescriptor.RpcVersion >= 14);
                }
                else
                {
                    if (form.InvokeRequired)
                        form.Invoke((MethodInvoker)delegate { form.torrentListView.Items.Clear(); });
                    else
                        form.torrentListView.Items.Clear(); //cant access ui thread as we are on a worker/events thread if power events sets connected state
                    torrentIndex.Clear();
                    Program.DaemonDescriptor.UpdateSerial = 0;
                }
                if (OnConnStatusChanged != null)
                {
                    OnConnStatusChanged(null, null);
                }
            }
            get
            {
                return connected;
            }
        }

        private static void UnhandledException(Exception ex)
        {
            UnhandledException el = new UnhandledException(ex, startupTime);
            System.Threading.Thread t = new System.Threading.Thread(el.DoLog);
            t.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            t.Start();
            string errorFormat;
            string errorText;

            try
            {
                errorFormat = "There was an unhandled error, and transmission-remote-dotnet must be closed. Refer to the file '{0}', which has been placed on your desktop, for more information.";
                // <comment>{0} is a filename</comment>
            }

            catch (Exception)
            {
                errorFormat = "There was an unhandled error, and transmission-remote-dotnet must be closed. Refer to the file '{0}', which has been placed on your desktop, for more information.";
            }

            errorText = string.Format(errorFormat, el.FileName);
            MessageBox.Show(errorText, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            UnhandledException((Exception)e.ExceptionObject);
            Process.GetCurrentProcess().Kill();
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            UnhandledException(e.Exception);
            Process.GetCurrentProcess().Kill();
        }
    }
}
