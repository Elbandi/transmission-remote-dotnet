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

#if FILECONF
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Jayrock.Json.Conversion;
using Jayrock.Json;

namespace TransmissionRemoteDotnet
{
    public enum ProxyMode
    {
        Auto = 0,
        Enabled = 1,
        Disabled = 2
    }

    public sealed class LocalSettingsSingleton
    {
        /* Some unconfigurable variables. */
        private const string CONF_FILE = @"settings.json";
        public const int COMPLETED_BALOON_TIMEOUT = 4;
        public const int FILES_REFRESH_MULTIPLICANT = 3;

        /* Registry keys */
        private const string REGKEY_HOST = "host",
            REGKEY_PORT = "port",
            REGKEY_USESSL = "usessl",
            REGKEY_AUTOCONNECT = "autoConnect",
            REGKEY_USER = "user",
            REGKEY_PASS = "pass",
            REGKEY_AUTHENABLED = "authEnabled",
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
            REGKEY_CUSTOMPATH = "customPath";

        private JsonObject jsonRoot;
        private JsonObject profileKey;

        private static LocalSettingsSingleton instance = null;
        private static readonly object padlock = new object();

        public static LocalSettingsSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LocalSettingsSingleton();
                    }
                    return instance;
                }
            }
        }

        private LocalSettingsSingleton()
        {
            FileStream inFile = null;
            try
            {
                inFile = new FileStream(Toolbox.SupportFilePath(CONF_FILE), FileMode.Open, FileAccess.Read);
                byte[] binaryData = new Byte[inFile.Length];
                if (inFile.Read(binaryData, 0, (int)inFile.Length) < 1)
                {
                    throw new Exception("Empty file");
                }
                this.jsonRoot = (JsonObject)JsonConvert.Import(UTF8Encoding.UTF8.GetString(binaryData));
            }
            catch
            {
                this.jsonRoot = new JsonObject();
            }
            finally
            {
                if (inFile != null)
                    inFile.Close();
            }
            if (jsonRoot.Contains(REGKEY_CURRENTPROFILE))
            {
                this.CurrentProfile = (string)jsonRoot[REGKEY_CURRENTPROFILE];
            }
            else
            {
                this.CurrentProfile = "Default";
            }
        }

        public List<string> Profiles
        {
            get
            {
                List<string> profiles = new List<string>();
                foreach (string s in this.profileKey.Names)
                {
                    if (this.profileKey[s].GetType() == typeof(JsonObject))
                    {
                        profiles.Add(s);
                    }
                }
                profiles.Sort();
                profiles.Insert(0, "Default");
                return profiles;
            }
        }

        public void SaveProfileSelection()
        {
            this.jsonRoot[REGKEY_CURRENTPROFILE] = this.currentProfile;
        }

        public void Commit()
        {
            try
            {
                SaveProfileSelection();
                FileStream outFile = new FileStream(Toolbox.SupportFilePath(CONF_FILE), FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(outFile);
                writer.Write(this.profileKey.ToString());
                writer.Close();
                Program.Form.refreshTimer.Interval = RefreshRate * 1000;
                Program.Form.filesTimer.Interval = RefreshRate * 1000 * FILES_REFRESH_MULTIPLICANT;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error writing settings to registry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshUrlCache()
        {
            this.urlCache = String.Format("{0}://{1}:{2}{3}rpc", new object[] { UseSSL ? "https" : "http", Host, Port, CustomPath == null ? "/transmission/" : CustomPath });
        }

        public void CreateProfile(string name)
        {
            this.profileKey.Put(name, new JsonObject());
            this.CurrentProfile = name;
        }

        private string currentProfile;

        public string CurrentProfile
        {
            get
            {
                return this.currentProfile;
            }
            set
            {
                if (Program.Connected)
                {
                    Program.Connected = false;
                }
                this.currentProfile = value;
                SaveProfileSelection();
                if (Program.Form != null && AutoConnect)
                {
                    Program.Form.Connect();
                }
                if (value.Equals("Default"))
                {
                    this.profileKey = jsonRoot;
                }
                else
                {
                    this.profileKey = (JsonObject)this.jsonRoot[currentProfile];
                }
                RefreshUrlCache();
            }
        }

        public void RemoveProfile(string name)
        {
            if (this.profileKey.Contains(name))
            {
                this.profileKey.Remove(name);
            }
        }

        public string Host
        {
            get
            {
                return this.profileKey.Contains(REGKEY_HOST) ? (string)this.profileKey[REGKEY_HOST] : "";
            }
            set
            {
                this.profileKey[REGKEY_HOST] = value;
            }
        }

        public int Port
        {
            get
            {
                return this.profileKey.Contains(REGKEY_PORT) ? ObjToolbox.ToInt(this.profileKey[REGKEY_PORT]) : 9091;
            }
            set
            {
                this.profileKey[REGKEY_PORT] = value;
            }
        }

        public bool UseSSL
        {
            get
            {
                return this.profileKey.Contains(REGKEY_USESSL) ? ObjToolbox.ToBool(this.profileKey[REGKEY_USESSL]) : false;
            }
            set
            {
                this.profileKey[REGKEY_USESSL] = BoolToolbox.ToInt(value);
            }
        }

        public int RefreshRate
        {
            get
            {
                return this.profileKey.Contains(REGKEY_REFRESHRATE) ? ObjToolbox.ToInt(this.profileKey[REGKEY_REFRESHRATE]) : 3;
            }
            set
            {
                this.profileKey[REGKEY_REFRESHRATE] = value;
            }
        }

        private int ObjToolbox.ToInt(object o)
        {
            if (o.GetType() == typeof(JsonNumber))
            {
                return ((JsonNumber)o).Toolbox.ToInt32();
            }
            else
            {
                return (int)o;
            }
        }

        private bool ObjToolbox.ToBool(object o)
        {
            if (o.GetType() == typeof(JsonNumber))
            {
                return ((JsonNumber)o).Toolbox.ToInt32() == 1;
            }
            else
            {
                return (int)o == 1;
            }
        }

        private int BoolToolbox.ToInt(bool b)
        {
            return b ? 1 : 0;
        }

        public bool AutoConnect
        {
            get
            {
                return this.profileKey.Contains(REGKEY_AUTOCONNECT) ? ObjToolbox.ToBool(this.profileKey[REGKEY_AUTOCONNECT]) : false;
            }
            set
            {
                this.profileKey[REGKEY_AUTOCONNECT] = BoolToolbox.ToInt(value);
            }
        }

        public string User
        {
            get
            {
                return this.profileKey.Contains(REGKEY_USER) ? (string)this.profileKey[REGKEY_USER] : "";
            }
            set
            {
                this.profileKey[REGKEY_USER] = value;
            }
        }

        public string Pass
        {
            get
            {
                return this.profileKey.Contains(REGKEY_PASS) ? (string)this.profileKey[REGKEY_PASS] : "";
            }
            set
            {
                this.profileKey[REGKEY_PASS] = value;
            }
        }

        public bool AuthEnabled
        {
            get
            {
                return this.profileKey.Contains(REGKEY_AUTHENABLED) ? ObjToolbox.ToBool(this.profileKey[REGKEY_AUTHENABLED]) : false;
            }
            set
            {
                this.profileKey[REGKEY_AUTHENABLED] = BoolToolbox.ToInt(value);
            }
        }

        public bool MinToTray
        {
            get
            {
                return this.profileKey.Contains(REGKEY_MINTOTRAY) ? ObjToolbox.ToBool(this.profileKey[REGKEY_MINTOTRAY]) : false;
            }
            set
            {
                this.profileKey[REGKEY_MINTOTRAY] = BoolToolbox.ToInt(value);
            }
        }

        public ProxyMode ProxyMode
        {
            get
            {
                if (this.profileKey.Contains(REGKEY_PROXYENABLED))
                {
                    return (ProxyMode)(ObjToolbox.ToInt(this.profileKey[REGKEY_PROXYENABLED]));
                }
                else
                {
                    return ProxyMode.Auto;
                }
            }
            set
            {
                this.profileKey[REGKEY_PROXYENABLED] = (int)value;
            }
        }

        public string ProxyHost
        {
            get
            {
                return this.profileKey.Contains(REGKEY_PROXYHOST) ? (string)this.profileKey[REGKEY_PROXYHOST] : "";
            }
            set
            {
                this.profileKey[REGKEY_PROXYHOST] = value;
            }
        }

        public int ProxyPort
        {
            get
            {
                return this.profileKey.Contains(REGKEY_PROXYPORT) ? ObjToolbox.ToInt(this.profileKey[REGKEY_PROXYPORT]) : 8080;
            }
            set
            {
                this.profileKey[REGKEY_PROXYPORT] = value;
            }
        }

        public string ProxyUser
        {
            get
            {
                return this.profileKey.Contains(REGKEY_PROXYUSER) ? (string)this.profileKey[REGKEY_PROXYUSER] : "";
            }
            set
            {
                this.profileKey[REGKEY_PROXYUSER] = value;
            }
        }

        public string ProxyPass
        {
            get
            {
                return this.profileKey.Contains(REGKEY_PROXYPASS) ? (string)this.profileKey[REGKEY_PROXYPASS] : "";
            }
            set
            {
                this.profileKey[REGKEY_PROXYPASS] = value;
            }
        }

        public bool ProxyAuth
        {
            get
            {
                return this.profileKey.Contains(REGKEY_PROXYAUTH) ? ObjToolbox.ToBool(this.profileKey[REGKEY_PROXYAUTH]) : false;
            }
            set
            {
                this.profileKey[REGKEY_PROXYAUTH] = BoolToolbox.ToInt(value);
            }
        }

        public bool StartPaused
        {
            get
            {
                return this.profileKey.Contains(REGKEY_STARTPAUSED) ? ObjToolbox.ToBool(this.profileKey[REGKEY_STARTPAUSED]) : false;
            }
            set
            {
                this.profileKey[REGKEY_STARTPAUSED] = BoolToolbox.ToInt(value);
            }
        }

        public int RetryLimit
        {
            get
            {
                return this.profileKey.Contains(REGKEY_RETRYLIMIT) ? ObjToolbox.ToInt(this.profileKey[REGKEY_RETRYLIMIT]) : 3;
            }
            set
            {
                this.profileKey[REGKEY_RETRYLIMIT] = value;
            }
        }

        public string CustomPath
        {
            get
            {
                return this.profileKey.Contains(REGKEY_CUSTOMPATH) ? (string)this.profileKey[REGKEY_CUSTOMPATH] : null;
            }
        }

        private string urlCache;

        public string RpcUrl
        {
            get
            {
                return this.urlCache;
            }
        }
    }
}
#endif