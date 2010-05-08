using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Jayrock.Json;
using Jayrock.Json.Conversion;

namespace TransmissionRemoteDotnet.Settings
{
    public class FileLocalSettingsStore : ILocalSettingsStore
    {
        private const string CONF_FILE = @"settings.json";
        public override JsonObject Load()
        {
            return Load(Toolbox.SupportFilePath(CONF_FILE));
        }

        public JsonObject Load(string Filename)
        {
            JsonObject jo;
            using (FileStream inFile = new FileStream(Filename, FileMode.Open, FileAccess.Read))
            {
                byte[] binaryData = new Byte[inFile.Length];
                if (inFile.Read(binaryData, 0, (int)inFile.Length) < 1)
                {
                    throw new Exception(OtherStrings.EmptyFile);
                }
                jo = (JsonObject)JsonConvert.Import(UTF8Encoding.UTF8.GetString(binaryData));
            }
            return jo;
        }

        public override bool Save(JsonObject s)
        {
            return Save(Toolbox.SupportFilePath(CONF_FILE), s);
        }

        public bool Save(string Filename, JsonObject s)
        {
            try
            {
                using (FileStream outFile = new FileStream(Filename, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(outFile))
                    {
                        writer.Write(s.ToString());
                    }
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
