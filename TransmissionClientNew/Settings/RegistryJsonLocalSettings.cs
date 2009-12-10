using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using Jayrock.Json;
using Jayrock.Json.Conversion;

namespace TransmissionRemoteDotnet.Settings
{
    public class RegistryJsonLocalSettingsStore : ILocalSettingsStore
    {
        public string REGISTRY_KEY_ROOT = @"Software\TransmissionRemote\Settings";

        protected RegistryKey GetRootKey(bool writeable)
        {
            return GetKey(Registry.CurrentUser, REGISTRY_KEY_ROOT, writeable);
        }

        protected RegistryKey GetKey(RegistryKey root, string subkey, bool writeable)
        {
            RegistryKey key = root.OpenSubKey(subkey, writeable);
            if (key == null)
                key = root.CreateSubKey(subkey);
            return key;
        }


        public override JsonObject Load()
        {
            JsonObject jo;
            using (RegistryKey key = GetRootKey(false))
            {
                if (!this.GetType().ToString().Equals(key.GetValue("")))
                    throw new Exception("Not for this class"); // TODO: find a better Exception
                jo = (JsonObject)JsonConvert.Import(key.GetValue(this.GetType().ToString()).ToString());
            }
            return jo;
        }

        public override bool Save(JsonObject s)
        {

            try
            {
                using (RegistryKey key = GetRootKey(true))
                {
                    key.SetValue(this.GetType().ToString(), s.ToString());
                    key.SetValue("", this.GetType().ToString());
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
