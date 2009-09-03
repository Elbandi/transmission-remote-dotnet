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
using System.Windows.Forms;
using Jayrock.Json;
using System.Net;
using TransmissionRemoteDotnet.Commands;
using System.Threading;

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

        private static MainWindow form;
        public static MainWindow Form
        {
            get { return Program.form; }
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

        private static List<ListViewItem> logItems = new List<ListViewItem>();
        public static List<ListViewItem> LogItems
        {
            get { return Program.logItems; }
        
        }

        private static string[] uploadArgs; 
        public static string[] UploadArgs
        {
            get { return Program.uploadArgs; }
            set { Program.uploadArgs = value; }
        }

        [STAThread]
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(LocalSettingsSingleton.Instance.Locale);
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
                    if (LocalSettingsSingleton.Instance.AutoConnect && args.Length > 0)
                    {
                        Program.uploadArgs = args;
                    }
                    singleInstance.ArgumentsReceived += singleInstance_ArgumentsReceived;
                    singleInstance.ListenForArgumentsFromSuccessiveInstances();
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

        public static void Log(string title, string body)
        {
            Log(title, body, null);
        }

        public static void Log(string title, string body, object tag)
        {
            ListViewItem logItem = new ListViewItem(DateTime.Now.ToString());
            logItem.Tag = tag;
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
                        form.Upload(e.Args);
                    }
                    else
                    {
                        form.ShowMustBeConnectedDialog(uploadArgs = e.Args);
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
                if (!connected)
                {
                    lock (torrentIndex)
                    {
                        torrentIndex.Clear();
                    }
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
    }
}
