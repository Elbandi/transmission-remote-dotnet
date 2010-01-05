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

#if !FILECONF
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using Jayrock.Json;
using Jayrock.Json.Conversion;

namespace TransmissionRemoteDotnet
{
    public enum ProxyMode
    {
        Auto = 0,
        Enabled = 1,
        Disabled = 2
    }

    class LocalSettingsSingleton
    {
        /* Some unconfigurable variables. */
        private const string REGISTRY_KEY_ROOT = @"Software\TransmissionRemote";
        public const int BALLOON_TIMEOUT = 4;
        public const int FILES_REFRESH_MULTIPLICANT = 3;

        private Dictionary<string, object> profileConfMap = new Dictionary<string, object>();
        private Dictionary<string, object> rootConfMap = new Dictionary<string, object>();

        /* Registry keys */
        private const string REGKEY_HOST = "host",
            REGKEY_PORT = "port",
            REGKEY_USESSL = "usessl",
            REGKEY_AUTOCONNECT = "autoConnect",
            REGKEY_AUTOCHECKUPDATE = "autoCheckupdate",
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
            /*REGKEY_SAMBASHARE = "sambaShare",
            REGKEY_SAMBASHAREENABLED = "sambaShareEnabled",*/
            REGKEY_SAMBASHAREMAPPINGS = "sambaShareMappings",
            REGKEY_UPLOADPROMPT = "uploadPrompt",
            REGKEY_DESTINATION_PATH_HISTORY = "destPathHistory";

        public static LocalSettingsSingleton OneInstance()
        {
            return new LocalSettingsSingleton();
        }
        public void BackupSettings()
        {
            int last = REGISTRY_KEY_ROOT.LastIndexOf(@"\");
            string trname = REGISTRY_KEY_ROOT.Substring(last + 1);
            string parentname = REGISTRY_KEY_ROOT.Substring(0, last);
            RegistryKey root = GetRootKey(parentname, true);
            if (Array.Exists<string>(root.GetSubKeyNames(), delegate(string s) { return trname.Equals(s); }))
            {
                Toolbox.RenameSubKey(root, trname, trname + ".backup");
                MessageBox.Show("Atraktuk");
            }
        }
        private LocalSettingsSingleton()
        {
            RegistryKey key = GetRootKey(false);
            if (key == null)
                throw new Exception();
            foreach (string subKey in key.GetValueNames())
            {
                if (subKey.StartsWith("_"))
                    rootConfMap[subKey.Remove(0, 1)] = key.GetValue(subKey);
            }
            if (rootConfMap.ContainsKey(REGKEY_CURRENTPROFILE))
            {
                this.CurrentProfile = (string)rootConfMap[REGKEY_CURRENTPROFILE];
            }
            else
            {
                this.CurrentProfile = "Default";
            }
            key.Close();
        }

        public object GetObject(string key)
        {
            return this.GetObject(key, true);
        }

        public object GetObject(string key, bool root)
        {
            if (root)
            {
                return rootConfMap.ContainsKey(key) ? rootConfMap[key] : null;
            }
            else
            {
                return profileConfMap.ContainsKey(key) ? profileConfMap[key] : null;
            }
        }


        public string[] ListObject(bool root)
        {
            string[] keys;
            if (root)
            {
                keys = new string[rootConfMap.Keys.Count];
                rootConfMap.Keys.CopyTo(keys, 0);

            }
            else
            {
                keys = new string[profileConfMap.Keys.Count];
                profileConfMap.Keys.CopyTo(keys, 0);
            }
            return keys;
        }

        public bool ContainsKey(string key, bool root)
        {
            return (root ? rootConfMap : profileConfMap).ContainsKey(key);
        }

        public bool ContainsKey(string key)
        {
            return this.ContainsKey(key, true);
        }

        public void SetObject(string key, object value)
        {
            this.SetObject(key, value, true);
        }

        public void SetObject(string key, object value, bool root)
        {
            if (root)
            {
                rootConfMap[key] = value;
            }
            else
            {
                profileConfMap[key] = value;
            }
        }

        private RegistryKey GetProfileKey(string name, bool writeable)
        {
            if (name.Equals("Default"))
            {
                return GetRootKey(writeable);
            }
            else
            {
                return GetRootKey(false).OpenSubKey(name, writeable);
            }
        }

        public List<string> Profiles
        {
            get
            {
                List<string> profiles = new List<string>();
                profiles.AddRange(GetRootKey(false).GetSubKeyNames());
                profiles.Sort();
                profiles.Insert(0, "Default");
                return profiles;
            }
        }

        public void Commit()
        {
            RegistryKey profileKey = null;
            RegistryKey rootKey = null;
            try
            {
                rootKey = GetRootKey(true);
                foreach (KeyValuePair<string, object> pair in rootConfMap)
                {
                    if (pair.Key != null && pair.Value != null)
                        rootKey.SetValue("_" + pair.Key, pair.Value);
                }
                profileKey = GetProfileKey(this.CurrentProfile, true);
                if (profileKey != null)
                {
                    foreach (KeyValuePair<string, object> pair in profileConfMap)
                    {
                        if (pair.Key != null && pair.Value != null)
                            profileKey.SetValue(pair.Key, pair.Value);
                    }
                }
                Program.Form.refreshTimer.Interval = RefreshRate * 1000;
                Program.Form.filesTimer.Interval = RefreshRate * 1000 * FILES_REFRESH_MULTIPLICANT;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error writing settings to registry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (rootKey != null)
                    rootKey.Close();
                if (profileKey != null)
                    profileKey.Close();
            }
        }

        public void CreateProfile(string name)
        {
            RegistryKey root = GetRootKey(true);
            root.CreateSubKey(name);
            root.Close();
            this.CurrentProfile = name;
        }

        public string[] DestPathHistory
        {
            get
            {
                if (profileConfMap.ContainsKey(REGKEY_DESTINATION_PATH_HISTORY))
                {
                    try
                    {
                        JsonArray array = (JsonArray)JsonConvert.Import((string)profileConfMap[REGKEY_DESTINATION_PATH_HISTORY]);
                        return (string[])array.ToArray(typeof(string));
                    }
                    catch { }
                }
                return new string[] { };
            }
        }

        public void AddDestinationPath(string path)
        {
            if (profileConfMap.ContainsKey(REGKEY_DESTINATION_PATH_HISTORY))
            {
                try
                {
                    JsonArray oldArray = (JsonArray)JsonConvert.Import((string)profileConfMap[REGKEY_DESTINATION_PATH_HISTORY]);
                    JsonArray newArray = new JsonArray();
                    newArray.Add(path);
                    for (int i = 0; i < oldArray.Length && newArray.Length < 5; i++)
                    {
                        string oldString = (string)oldArray[i];
                        if (oldString != path)
                            newArray.Add(oldString);
                    }
                    profileConfMap[REGKEY_DESTINATION_PATH_HISTORY] = newArray.ToString();
                }
                catch
                {
                    profileConfMap[REGKEY_DESTINATION_PATH_HISTORY] = (new JsonArray(new string[] { path })).ToString();
                }
            }
            else
            {
                profileConfMap.Add(REGKEY_DESTINATION_PATH_HISTORY, (new JsonArray(new string[] { path })).ToString());
            }
        }

        private RegistryKey GetRootKey(bool writeable)
        {
            return GetRootKey(REGISTRY_KEY_ROOT, writeable);
        }

        private RegistryKey GetRootKey(string keyroot, bool writeable)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(keyroot, writeable);
            if (key == null && writeable)
                key = Registry.CurrentUser.CreateSubKey(keyroot);
            return key;
        }

        public string CurrentProfile
        {
            get
            {
                if (rootConfMap.ContainsKey(REGKEY_CURRENTPROFILE))
                {
                    return (string)rootConfMap[REGKEY_CURRENTPROFILE];
                }
                else
                {
                    return "Default";
                }
            }
            set
            {
                RegistryKey profileKey = null;
                RegistryKey rootKey = null;
                if (Program.Connected)
                {
                    Program.Connected = false;
                }
                try
                {
                    rootConfMap[REGKEY_CURRENTPROFILE] = value;
                    profileKey = GetProfileKey(value, false);
                    profileConfMap.Clear();
                    foreach (string subKey in profileKey.GetValueNames())
                    {
                        if (!subKey.StartsWith("_"))
                            profileConfMap[subKey] = profileKey.GetValue(subKey);
                    }
                    profileKey.Close();
                    if (Program.Form != null && AutoConnect)
                        Program.Form.Connect();
                }
                catch
                {
                    if (!value.Equals("Default"))
                        this.CurrentProfile = "Default";
                }
                finally
                {
                    if (profileKey != null)
                        profileKey.Close();
                    if (rootKey != null)
                        rootKey.Close();
                }
            }
        }

        public void RemoveProfile(string name)
        {
            RegistryKey key = GetRootKey(true);
            key.DeleteSubKeyTree(name);
            key.Close();
        }

        public string Host
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_HOST) ? (string)profileConfMap[REGKEY_HOST] : "";
            }
            set
            {
                profileConfMap[REGKEY_HOST] = value;
            }
        }

        public int Port
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_PORT) ? (int)profileConfMap[REGKEY_PORT] : 9091;
            }
            set
            {
                profileConfMap[REGKEY_PORT] = value;
            }
        }

        public bool UseSSL
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_USESSL) ? ToBool(profileConfMap[REGKEY_USESSL]) : false;
            }
            set
            {
                profileConfMap[REGKEY_USESSL] = ToInt(value);
            }
        }

        public int RefreshRate
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_REFRESHRATE) ? (int)profileConfMap[REGKEY_REFRESHRATE] : 3;
            }
            set
            {
                profileConfMap[REGKEY_REFRESHRATE] = value;
            }
        }

        public bool UploadPrompt
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_UPLOADPROMPT) ? ToBool(profileConfMap[REGKEY_UPLOADPROMPT]) : false;
            }
            set
            {
                this.profileConfMap[REGKEY_UPLOADPROMPT] = ToInt(value);
            }
        }

        public bool AutoConnect
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_AUTOCONNECT) ? ToBool(profileConfMap[REGKEY_AUTOCONNECT]) : false;
            }
            set
            {
                profileConfMap[REGKEY_AUTOCONNECT] = ToInt(value);
            }
        }

        public bool AutoCheckupdate
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_AUTOCHECKUPDATE) ? ToBool(profileConfMap[REGKEY_AUTOCHECKUPDATE]) : false;
            }
            set
            {
                profileConfMap[REGKEY_AUTOCHECKUPDATE] = ToInt(value);
            }
        }

        public string User
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_USER) ? (string)profileConfMap[REGKEY_USER] : "";
            }
            set
            {
                profileConfMap[REGKEY_USER] = value;
            }
        }

        public string Pass
        {
            get
            {
                if (!profileConfMap.ContainsKey(REGKEY_PASS))
                    return "";
                string pass = (string)profileConfMap[REGKEY_PASS];
                try
                {
                    if (pass.StartsWith(Convert.ToChar(1).ToString()))
                        pass = Toolbox.Decrypt(pass.Substring(1));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, e.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pass = "";
                }
                return pass;
            }
            set
            {
                try
                {
                    profileConfMap[REGKEY_PASS] = value.Length > 0 ? Convert.ToChar(1) + Toolbox.Encrypt(value) : "";
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, e.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool AuthEnabled
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_AUTHENABLED) ? ToBool(profileConfMap[REGKEY_AUTHENABLED]) : false;
            }
            set
            {
                profileConfMap[REGKEY_AUTHENABLED] = ToInt(value);
            }
        }

        private static bool ToBool(object o)
        {
            return (int)o == 1;
        }

        private static int ToInt(bool b)
        {
            return b ? 1 : 0;
        }

        public bool MinToTray
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_MINTOTRAY) ? ToBool(profileConfMap[REGKEY_MINTOTRAY]) : false;
            }
            set
            {
                profileConfMap[REGKEY_MINTOTRAY] = ToInt(value);
            }
        }

        public ProxyMode ProxyMode
        {
            get
            {
                if (profileConfMap.ContainsKey(REGKEY_PROXYENABLED))
                {
                    return (ProxyMode)((int)profileConfMap[REGKEY_PROXYENABLED]);
                }
                else
                {
                    return ProxyMode.Auto;
                }
            }
            set
            {
                profileConfMap[REGKEY_PROXYENABLED] = (int)value;
            }
        }

        public string ProxyHost
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_PROXYHOST) ? (string)profileConfMap[REGKEY_PROXYHOST] : "";
            }
            set
            {
                profileConfMap[REGKEY_PROXYHOST] = value;
            }
        }

        public string PlinkPath
        {
            get
            {
                return rootConfMap.ContainsKey(REGKEY_PLINKPATH) ? (string)rootConfMap[REGKEY_PLINKPATH] : null;
            }
            set
            {
                rootConfMap[REGKEY_PLINKPATH] = value;
            }
        }

        public string Locale
        {
            get
            {
                return rootConfMap.ContainsKey(REGKEY_LOCALE) ? (string)rootConfMap[REGKEY_LOCALE] : "en-GB";
            }
            set
            {
                rootConfMap[REGKEY_LOCALE] = value;
            }
        }

        public string PlinkCmd
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_PLINKCMD) ? (string)profileConfMap[REGKEY_PLINKCMD] : "ls -lh \"$DATA\"; read";
            }
            set
            {
                profileConfMap[REGKEY_PLINKCMD] = value;
            }
        }

        public bool PlinkEnable
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_PLINKENABLE) ? ToBool(profileConfMap[REGKEY_PLINKENABLE]) : false;
            }
            set
            {
                profileConfMap[REGKEY_PLINKENABLE] = ToInt(value);
            }
        }

        public int ProxyPort
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_PROXYPORT) ? (int)profileConfMap[REGKEY_PROXYPORT] : 8080;
            }
            set
            {
                profileConfMap[REGKEY_PROXYPORT] = value;
            }
        }

        public string ProxyUser
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_PROXYUSER) ? (string)profileConfMap[REGKEY_PROXYUSER] : "";
            }
            set
            {
                profileConfMap[REGKEY_PROXYUSER] = value;
            }
        }

        public string ProxyPass
        {
            get
            {
                if (!profileConfMap.ContainsKey(REGKEY_PROXYPASS))
                    return "";
                string pass = (string)profileConfMap[REGKEY_PROXYPASS];
                try
                {
                    if (pass.StartsWith(Convert.ToChar(1).ToString()))
                        pass = Toolbox.Decrypt(pass.Substring(1));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, e.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pass = "";
                }
                return pass;
            }
            set
            {
                try
                {
                    profileConfMap[REGKEY_PROXYPASS] = value.Length > 0 ? Convert.ToChar(1) + Toolbox.Encrypt(value) : "";
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, e.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool ProxyAuth
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_PROXYAUTH) ? ToBool(profileConfMap[REGKEY_PROXYAUTH]) : false;
            }
            set
            {
                profileConfMap[REGKEY_PROXYAUTH] = ToInt(value);
            }
        }

        public bool StartPaused
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_STARTPAUSED) ? ToBool(profileConfMap[REGKEY_STARTPAUSED]) : false;
            }
            set
            {
                profileConfMap[REGKEY_STARTPAUSED] = ToInt(value);
            }
        }

        public int RetryLimit
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_RETRYLIMIT) ? (int)profileConfMap[REGKEY_RETRYLIMIT] : 3;
            }
            set
            {
                profileConfMap[REGKEY_RETRYLIMIT] = value;
            }
        }

        public string CustomPath
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_CUSTOMPATH) ? (string)profileConfMap[REGKEY_CUSTOMPATH] : null;
            }
        }

        public string RpcUrl
        {
            get
            {
                return String.Format("{0}://{1}:{2}{3}rpc", new object[] { UseSSL ? "https" : "http", Host, Port, CustomPath == null ? "/transmission/" : CustomPath });
            }
        }

        public bool StartedBalloon
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_STARTEDBALLOON) ? ToBool(profileConfMap[REGKEY_STARTEDBALLOON]) : true;
            }
            set
            {
                profileConfMap[REGKEY_STARTEDBALLOON] = ToInt(value);
            }
        }

        public bool CompletedBaloon
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_COMPLETEDBALLOON) ? ToBool(profileConfMap[REGKEY_COMPLETEDBALLOON]) : true;
            }
            set
            {
                profileConfMap[REGKEY_COMPLETEDBALLOON] = ToInt(value);
            }
        }

        public bool MinOnClose
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_MINONCLOSE) ? ToBool(profileConfMap[REGKEY_MINONCLOSE]) : false;
            }
            set
            {
                profileConfMap[REGKEY_MINONCLOSE] = ToInt(value);
            }
        }

        public string DownLimit
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_DOWNLIMIT) ? (string)profileConfMap[REGKEY_DOWNLIMIT] : "10,50,100,200,300,400,500,700,1000,1500,2000,3000,5000";
            }
            set
            {
                profileConfMap[REGKEY_DOWNLIMIT] = value;
            }
        }

        public string UpLimit
        {
            get
            {
                return profileConfMap.ContainsKey(REGKEY_UPLIMIT) ? (string)profileConfMap[REGKEY_UPLIMIT] : "10,50,100,200,300,400,500,700,1000,1500,2000,3000,5000";
            }
            set
            {
                profileConfMap[REGKEY_UPLIMIT] = value;
            }
        }

        public JsonObject SambaShareMappings
        {
            get
            {
                if (profileConfMap.ContainsKey(REGKEY_SAMBASHAREMAPPINGS))
                {
                    try
                    {
                        return (JsonObject)JsonConvert.Import((string)profileConfMap[REGKEY_SAMBASHAREMAPPINGS]);
                    }
                    catch { }
                }
                return new JsonObject();
            }
            set
            {
                this.profileConfMap[REGKEY_SAMBASHAREMAPPINGS] = value.ToString();
            }
        }

        public void RemoveSambaMapping(string unixPrefix)
        {
            JsonObject mappings = this.SambaShareMappings;
            if (mappings.Contains(unixPrefix))
                mappings.Remove(unixPrefix);
            this.SambaShareMappings = mappings;
        }

        public void AddSambaMapping(string unixPrefix, string sambaPrefix)
        {
            JsonObject mappings = this.SambaShareMappings;
            mappings[unixPrefix] = sambaPrefix.EndsWith(@"\") ? sambaPrefix.Substring(0, sambaPrefix.Length - 1) : sambaPrefix;
            this.SambaShareMappings = mappings;
        }
    }
}
#endif