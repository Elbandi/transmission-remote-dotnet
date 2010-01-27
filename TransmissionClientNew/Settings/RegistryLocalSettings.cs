using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using Jayrock.Json;

namespace TransmissionRemoteDotnet.Settings
{
    public class RegistryLocalSettingsStore : RegistryJsonLocalSettingsStore
    {
        public override JsonObject Load()
        {
            return Load(null);
        }
        private JsonObject Load(RegistryKey root)
        {
            JsonObject jo = new JsonObject();
            using (RegistryKey key = root != null ? root : GetRootKey(false))
            {
                if (!this.GetType().ToString().Equals(key.GetValue("")))
                    throw new Exception("Not for this class"); // TODO: find a better Exception
                foreach (string valueName in key.GetValueNames())
                {
                    if (!valueName.Equals(""))
                        jo.Put(valueName, key.GetValue(valueName));
                }
                foreach (string subKeyName in key.GetSubKeyNames())
                {
                    jo.Put(subKeyName, Load(key.OpenSubKey(subKeyName)));
                }
            }
            return jo;
        }

        public override bool Save(JsonObject s)
        {
            return Save(s, null);
        }
        private bool Save(JsonObject s, RegistryKey root)
        {
            bool noerror = true;
            try
            {
                using (RegistryKey key = root != null ? root : GetRootKey(true))
                {
                    string[] names = new string[s.Names.Count];
                    s.Names.CopyTo(names, 0);
                    foreach (string subkey in key.GetSubKeyNames())
                    {
                        if (!Array.Exists<string>(names, delegate(string ss) { return subkey.Equals(ss); }))
                        {
                            key.DeleteSubKeyTree(subkey);
                        }
                    }
                    foreach (string subkey in key.GetValueNames())
                    {
                        if (!Array.Exists<string>(names, delegate(string ss) { return subkey.Equals(ss); }))
                        {
                            key.DeleteValue(subkey, false);
                        }
                    }
                    foreach (string n in s.Names)
                    {
                        try
                        {
                            if (s[n] != null)
                            {
                                if (s[n] is JsonObject)
                                {
                                    noerror &= Save(s[n] as JsonObject, GetKey(key, n, true));
                                }
                                else
                                {
                                    key.SetValue(n, s[n]);
                                }
                            }
                            else
                            {
                                key.DeleteValue(n, false);
                            }
                        }
                        catch { noerror = false; };
                    }
                    key.SetValue("", this.GetType().ToString());
                }
            }
            catch (Exception)
            {
                return false;
            }
            return noerror;
        }
    }
}
