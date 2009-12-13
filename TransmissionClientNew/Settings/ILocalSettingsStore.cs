using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jayrock.Json;

namespace TransmissionRemoteDotnet.Settings
{
    public abstract class ILocalSettingsStore
    {
        public const int BALLOON_TIMEOUT = 4;
        public const int FILES_REFRESH_MULTIPLICANT = 3;

        public abstract JsonObject Load();
        public abstract bool Save(JsonObject s);
    }
}
