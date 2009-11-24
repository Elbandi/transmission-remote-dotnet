﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Jayrock.Json;
using Jayrock.Json.Conversion;
using Microsoft.Win32;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet.Settings
{
    public class LocalSettings
    {
        /*
         * Modify this in MONO to right settings storage
         */
        public ILocalSettingsStore DefaultLocalStore = new RegistryLocalSettingsStore();
        public bool CompletedBaloon = true;
        public bool MinOnClose = false;
        public bool MinToTray = false;
        public bool AutoCheckupdate = false;
        public bool StartedBalloon = true;
        public string Locale = "en-GB";
        public string PlinkPath = null;
        public bool UploadPrompt = false;
        public string AutoConnect = "";
        private string currentprofile = "";

        public string CurrentProfile
        {
            get { return currentprofile; }
            set
            {
                if (Servers.ContainsKey(value))
                    currentprofile = value;
            }
        }
        public TransmissionServer Current
        {
            get
            {
                TransmissionServer ts = Servers[CurrentProfile];
                if (ts == null)
                    ts = new TransmissionServer();
                return ts;
            }
        }

        public Dictionary<string, TransmissionServer> Servers = new Dictionary<string, TransmissionServer>();
        public Dictionary<string, object> Misc = new Dictionary<string, object>();

        public JsonObject SaveToJson()
        {
            JsonObject jo = new JsonObject();
            jo.Put(SettingsKey.REGKEY_COMPLETEDBALLOON, Toolbox.ToInt(CompletedBaloon));
            jo.Put(SettingsKey.REGKEY_MINONCLOSE, Toolbox.ToInt(MinOnClose));
            jo.Put(SettingsKey.REGKEY_MINTOTRAY, Toolbox.ToInt(MinToTray));
            jo.Put(SettingsKey.REGKEY_STARTEDBALLOON, Toolbox.ToInt(StartedBalloon));
            jo.Put(SettingsKey.REGKEY_AUTOCHECKUPDATE, Toolbox.ToInt(AutoCheckupdate));
            jo.Put(SettingsKey.REGKEY_LOCALE, Locale);
            jo.Put(SettingsKey.REGKEY_PLINKPATH, PlinkPath);
            jo.Put(SettingsKey.REGKEY_UPLOADPROMPT, Toolbox.ToInt(UploadPrompt));
            jo.Put(SettingsKey.REGKEY_AUTOCONNECT, AutoConnect);
            jo.Put(SettingsKey.REGKEY_CURRENTPROFILE, CurrentProfile);
            JsonObject ja = new JsonObject();
            foreach (KeyValuePair<string, TransmissionServer> s in Servers)
            {
                ja.Put(s.Key, s.Value.SaveToJson());
            }
            jo.Put(SettingsKey.REGKEY_PROFILES, ja);
            ja = new JsonObject();
            foreach (KeyValuePair<string, object> s in Misc)
            {
                ja.Put(s.Key, s.Value);
            }
            jo.Put(SettingsKey.REGKEY_MISC, ja);
            return jo;
        }
        public void LoadFromJson(JsonObject o)
        {
            Toolbox.SetData(ref CompletedBaloon, o[SettingsKey.REGKEY_COMPLETEDBALLOON]);
            Toolbox.SetData(ref MinOnClose, o[SettingsKey.REGKEY_MINONCLOSE]);
            Toolbox.SetData(ref MinToTray, o[SettingsKey.REGKEY_MINTOTRAY]);
            Toolbox.SetData(ref StartedBalloon, o[SettingsKey.REGKEY_STARTEDBALLOON]);
            Toolbox.SetData(ref AutoCheckupdate, o[SettingsKey.REGKEY_AUTOCHECKUPDATE]);
            Toolbox.SetData(ref Locale, o[SettingsKey.REGKEY_LOCALE]);
            Toolbox.SetData(ref PlinkPath, o[SettingsKey.REGKEY_PLINKPATH]);
            Toolbox.SetData(ref UploadPrompt, o[SettingsKey.REGKEY_UPLOADPROMPT]);
            Toolbox.SetData(ref AutoConnect, o[SettingsKey.REGKEY_AUTOCONNECT]);
            JsonObject ja = (JsonObject)o[SettingsKey.REGKEY_PROFILES];
            if (ja != null)
            {
                foreach (string n in ja.Names)
                {
                    Servers.Add(n, new TransmissionServer(ja[n] as JsonObject));
                }
            }
            Toolbox.SetData(ref currentprofile, o[SettingsKey.REGKEY_CURRENTPROFILE]);
            if (!Servers.ContainsKey(currentprofile))
                currentprofile = "";
            ja = (JsonObject)o[SettingsKey.REGKEY_MISC];
            if (ja != null)
            {
                foreach (string n in ja.Names)
                {
                    Misc.Add(n, ja[n]);
                }
            }
        }
        public LocalSettings()
        {
        }
        public LocalSettings(JsonObject o)
        {
            LoadFromJson(o);
        }
        /*
         * TODO: this sould remove!!!
         */
        public object GetObject(string key)
        {
            return Misc[key];
        }
        public bool ContainsKey(string key)
        {
            return Misc.ContainsKey(key);
        }
        public void SetObject(string key, object value)
        {
            Misc[key] = value;
        }

        public void Commit()
        {
            //            Program.Form.refreshTimer.Interval = RefreshRate * 1000;
            //            Program.Form.filesTimer.Interval = RefreshRate * 1000 * FILES_REFRESH_MULTIPLICANT;
            if (!DefaultLocalStore.Save(this.SaveToJson()))
                MessageBox.Show("Error writing settings to registry", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static LocalSettings TryLoad()
        {
            LocalSettings newsettings = null;
            ILocalSettingsStore[] SettingsSource = new ILocalSettingsStore[] { 
                new RegistryLocalSettingsStore(), 
                new RegistryJsonLocalSettingsStore(), 
                new FileLocalSettings() 
            };
            foreach (ILocalSettingsStore ls in SettingsSource)
            {
                try
                {
                    JsonObject jo = ls.Load();
                    newsettings = new LocalSettings(jo);
                    break;
                }
                catch { };
            }
            if (newsettings == null)
            { // not load from any source :(, try old mode
                LocalSettingsSingleton oldsettings = LocalSettingsSingleton.OneInstance();
                newsettings = new LocalSettings();
                newsettings.Locale = oldsettings.Locale;
                newsettings.CompletedBaloon = oldsettings.CompletedBaloon;
                newsettings.MinOnClose = oldsettings.MinOnClose;
                newsettings.MinToTray = oldsettings.MinToTray;
                newsettings.PlinkPath = oldsettings.PlinkPath;
                newsettings.StartedBalloon = oldsettings.StartedBalloon;
                newsettings.UploadPrompt = oldsettings.UploadPrompt;
                newsettings.AutoCheckupdate = oldsettings.AutoCheckupdate;
                string origcurrentprofile = oldsettings.CurrentProfile;
                foreach (string p in oldsettings.Profiles)
                {
                    oldsettings.CurrentProfile = p;
                    TransmissionServer ts = new TransmissionServer();
                    ts.CustomPath = oldsettings.CustomPath;
                    ts.DownLimit = oldsettings.DownLimit;
                    ts.UpLimit = oldsettings.UpLimit;
                    ts.Host = oldsettings.Host;
                    ts.Password = oldsettings.Pass;
                    ts.PlinkCmd = oldsettings.PlinkCmd;
                    ts.PlinkEnable = oldsettings.PlinkEnable;
                    ts.Port = oldsettings.Port;
                    ts.RefreshRate = oldsettings.RefreshRate;
                    ts.StartPaused = oldsettings.StartPaused;
                    ts.Username = oldsettings.User;
                    ts.UseSSL = oldsettings.UseSSL;
                    JsonObject mappings = oldsettings.SambaShareMappings;
                    foreach (string key in mappings.Names)
                    {
                        ts.SambaShareMappings.Add(key, (string)mappings[key]);
                    }
                    ts.destpathhistory.AddRange(oldsettings.DestPathHistory);
                    ProxyServer ps = new ProxyServer();
                    ps.Host = oldsettings.ProxyHost;
                    ps.Password = oldsettings.ProxyPass;
                    ps.Port = oldsettings.ProxyPort;
                    ps.Username = oldsettings.ProxyUser;
                    ps.ProxyMode = (ProxyMode)oldsettings.ProxyMode;
                    ts.Proxy = ps;
                    newsettings.Servers.Add(p, ts);
                    if (origcurrentprofile.Equals(p))
                        newsettings.CurrentProfile = p;
                }
                if (newsettings.CurrentProfile.Equals("") && newsettings.Servers.Count > 0)
                    newsettings.CurrentProfile = newsettings.Servers.First().Key;
                foreach (string s in oldsettings.ListObject(true))
                {
                    if (s.StartsWith("mainwindow-") || s.StartsWith("listview-"))
                        newsettings.Misc[s] = oldsettings.GetObject(s, true);
                }
                // move old stuff to backup!
                //oldsettings.BackupSettings();
                if (!newsettings.DefaultLocalStore.Save(newsettings.SaveToJson()))
                    MessageBox.Show("Failed to save settings");
            }
            return newsettings;
        }
    }
    public class Server
    {
        public string Host = "";
        public int Port = 0;
        public string Username = "";
        public string Password = "";

        public virtual void LoadFromJson(JsonObject o)
        {
            Toolbox.SetData(ref Host, o[SettingsKey.REGKEY_HOST]);
            Toolbox.SetData(ref Port, o[SettingsKey.REGKEY_PORT]);
            Toolbox.SetData(ref Username, o[SettingsKey.REGKEY_USER]);
            Toolbox.SetData(ref Password, o[SettingsKey.REGKEY_PASS]);
        }
        public virtual JsonObject SaveToJson()
        {
            JsonObject jo = new JsonObject();
            jo.Put(SettingsKey.REGKEY_HOST, Host);
            jo.Put(SettingsKey.REGKEY_PORT, Port);
            jo.Put(SettingsKey.REGKEY_USER, Username);
            jo.Put(SettingsKey.REGKEY_PASS, Password);
            return jo;
        }
        public Server()
        {
        }
        public Server(JsonObject o)
        {
            LoadFromJson(o);
        }
    }
    public class TransmissionServer : Server
    {
        public bool UseSSL = false;
        public string CustomPath = null;
        public bool StartPaused = false;
        public int RefreshRate = 3;
        public int RetryLimit = 3;
        public Dictionary<string, string> SambaShareMappings = new Dictionary<string, string>();
        public string DownLimit = "10,50,100,200,300,400,500,700,1000,1500,2000,3000,5000";
        public string UpLimit = "10,50,100,200,300,400,500,700,1000,1500,2000,3000,5000";
        public bool PlinkEnable = false;
        public string PlinkCmd = "ls -lh \"$DATA\"; read";
        public List<string> destpathhistory = new List<string>();
        public ProxyServer Proxy = new ProxyServer();

        public override JsonObject SaveToJson()
        {
            JsonObject jo = base.SaveToJson();
            jo.Put(SettingsKey.REGKEY_USESSL, Toolbox.ToInt(UseSSL));
            jo.Put(SettingsKey.REGKEY_REFRESHRATE, RefreshRate);
            jo.Put(SettingsKey.REGKEY_CUSTOMPATH, CustomPath);
            jo.Put(SettingsKey.REGKEY_RETRYLIMIT, RetryLimit);
            jo.Put(SettingsKey.REGKEY_DOWNLIMIT, DownLimit);
            jo.Put(SettingsKey.REGKEY_UPLIMIT, UpLimit);
            jo.Put(SettingsKey.REGKEY_STARTPAUSED, Toolbox.ToInt(StartPaused));
            jo.Put(SettingsKey.REGKEY_PLINKENABLE, Toolbox.ToInt(PlinkEnable));
            jo.Put(SettingsKey.REGKEY_PLINKCMD, PlinkCmd);
            jo.Put(SettingsKey.REGKEY_PROXY, Proxy.SaveToJson());
            jo.Put(SettingsKey.REGKEY_DESTINATION_PATH_HISTORY, new JsonArray(destpathhistory));
            JsonObject ja = new JsonObject();
            foreach (KeyValuePair<string, string> s in SambaShareMappings)
            {
                ja.Put(s.Key, s.Value);
            }
            jo.Put(SettingsKey.REGKEY_SAMBASHAREMAPPINGS, ja);
            return jo;
        }

        public override void LoadFromJson(JsonObject o)
        {
            base.LoadFromJson(o);
            Toolbox.SetData(ref UseSSL, o[SettingsKey.REGKEY_USESSL]);
            Toolbox.SetData(ref RefreshRate, o[SettingsKey.REGKEY_REFRESHRATE]);
            Toolbox.SetData(ref CustomPath, o[SettingsKey.REGKEY_CUSTOMPATH]);
            Toolbox.SetData(ref RetryLimit, o[SettingsKey.REGKEY_RETRYLIMIT]);
            Toolbox.SetData(ref DownLimit, o[SettingsKey.REGKEY_DOWNLIMIT]);
            Toolbox.SetData(ref UpLimit, o[SettingsKey.REGKEY_UPLIMIT]);
            Toolbox.SetData(ref StartPaused, o[SettingsKey.REGKEY_STARTPAUSED]);
            Toolbox.SetData(ref PlinkEnable, o[SettingsKey.REGKEY_PLINKENABLE]);
            Toolbox.SetData(ref PlinkCmd, o[SettingsKey.REGKEY_PLINKCMD]);

            JsonArray ja = (JsonArray)JsonConvert.Import((string)o[SettingsKey.REGKEY_DESTINATION_PATH_HISTORY]);
            destpathhistory.AddRange((string[])ja.ToArray(typeof(string)));
            JsonObject jo = (JsonObject)o[SettingsKey.REGKEY_SAMBASHAREMAPPINGS];
            if (jo != null)
            {
                foreach (string n in jo.Names)
                {
                    SambaShareMappings.Add(n, jo[n] as string);
                }
            }
            jo = (JsonObject)o[SettingsKey.REGKEY_PROXY];
            if (jo != null)
            {
                Proxy = new ProxyServer(jo);
            }
        }
        public TransmissionServer()
        {
            Port = 9091;
        }
        public TransmissionServer(JsonObject o):this()
        {
            LoadFromJson(o);
        }

        public void RemoveSambaMapping(string unixPrefix)
        {
            if (SambaShareMappings.ContainsKey(unixPrefix))
                SambaShareMappings.Remove(unixPrefix);
        }

        public bool AddSambaMapping(string unixPrefix, string sambaPrefix)
        {
            if (SambaShareMappings.ContainsKey(unixPrefix)) return false;
            SambaShareMappings[unixPrefix] = sambaPrefix.EndsWith(@"\") ? sambaPrefix.Substring(0, sambaPrefix.Length - 1) : sambaPrefix;
            return true;
        }

        public void AddDestinationPath(string path)
        {
            if (!destpathhistory.Contains(path))
            {
                while (destpathhistory.Count > 5) destpathhistory.RemoveAt(0);
                destpathhistory.Add(path);
            }
        }
        public string[] DestPathHistory
        {
            get
            {
                return destpathhistory.ToArray();
            }
        }

        public bool AuthEnabled
        {
            get
            {
                return !Username.Equals("");
            }
        }

        public string RpcUrl
        {
            get
            {
                return String.Format("{0}://{1}:{2}{3}rpc", new object[] { UseSSL ? "https" : "http", Host, Port, CustomPath == null ? "/transmission/" : CustomPath });
            }
        }


    }
    public class ProxyServer : Server
    {
        public ProxyMode ProxyMode = ProxyMode.Auto;
        public override JsonObject SaveToJson()
        {
            JsonObject jo = base.SaveToJson();
            jo.Put(SettingsKey.REGKEY_PROXYENABLED, (int)ProxyMode);
            return jo;
        }

        public override void LoadFromJson(JsonObject o)
        {
            base.LoadFromJson(o);
            if (o[SettingsKey.REGKEY_PROXYENABLED] != null)
            {
                ProxyMode = (ProxyMode)Toolbox.ToInt(o[SettingsKey.REGKEY_PROXYENABLED]);
            }
        }
        public ProxyServer()
        {
            Port = 8080;
        }
        public ProxyServer(JsonObject o):this()
        {
            LoadFromJson(o);
        }

        public bool AuthEnabled
        {
            get
            {
                return !Username.Equals("");
            }
        }
    }
    internal class SettingsKey
    {
        public const string
            /* Registry keys */
            REGKEY_PROFILES = "profiles",
            REGKEY_MISC = "misc",
            REGKEY_HOST = "host",
            REGKEY_PORT = "port",
            REGKEY_USESSL = "usessl",
            REGKEY_AUTOCONNECT = "autoConnect",
            REGKEY_AUTOCHECKUPDATE = "autoCheckupdate",
            REGKEY_USER = "user",
            REGKEY_PASS = "pass",
            REGKEY_AUTHENABLED = "authEnabled",
            REGKEY_PROXY = "proxy",
            REGKEY_PROXYENABLED = "proxyEnabled",
            REGKEY_PROXYHOST = "proxyHost",
            REGKEY_PROXYPORT = "proxyPort",
            REGKEY_PROXYUSER = "proxyUser",
            REGKEY_PROXYPASS = "proxyPass",
            REGKEY_PROXYAUTH = "proxyAuth",
            REGKEY_STARTPAUSED = "startPaused",
            REGKEY_RETRYLIMIT = "retryLimit",
            REGKEY_MINTOTRAY = "minToTray",
            REGKEY_REFRESHRATE = "refreshRate",
            REGKEY_CURRENTPROFILE = "currentProfile",
            REGKEY_STARTEDBALLOON = "startedBalloon",
            REGKEY_COMPLETEDBALLOON = "completedBalloon",
            REGKEY_MINONCLOSE = "minOnClose",
            REGKEY_PLINKPATH = "plinkPath",
            REGKEY_PLINKCMD = "plinkCmd",
            REGKEY_PLINKENABLE = "plinkEnable",
            REGKEY_LOCALE = "locale",
            REGKEY_CUSTOMPATH = "customPath",
            REGKEY_DOWNLIMIT = "downlimit",
            REGKEY_UPLIMIT = "uplimit",
            REGKEY_SAMBASHAREMAPPINGS = "sambaShareMappings",
            REGKEY_UPLOADPROMPT = "uploadPrompt",
            REGKEY_DESTINATION_PATH_HISTORY = "destPathHistory";
    }
    /*
    public enum ProxyMode
    {
        Auto = 0,
        Enabled = 1,
        Disabled = 2
    }
     */
}
