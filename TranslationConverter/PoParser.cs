using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Instedd.GeoChat.Gettext
{
    public class PoParser
    {
        public Dictionary<string, string> Parse(TextReader reader)
        {
            var result = new Dictionary<string, string>();

            const int StateWaitingKey = 1;
            const int StateConsumingKey = 2;
            const int StateConsumingValue = 3;

            int state = StateWaitingKey;

            StringBuilder currentKey = null;
            StringBuilder currentValue = null;

            string line;
            while(true) {
                line = reader.ReadLine();
                line = line == null ? null : line.Trim();
                if (line == null || line.Length == 0)
                {
                    if (state == StateConsumingValue &&
                        currentKey != null &&
                        currentValue != null)
                    {
                        result[currentKey.ToString().Replace("\\n", "\n")] =
                            currentValue.ToString().Replace("\\n", "\n");
                        currentKey = null;
                        currentValue = null;
                    }

                    if (line == null)
                        break;

                    state = StateWaitingKey;
                    continue;
                }
                else if (line[0] == '#')
                {
                    continue;
                }

                bool isMsgId = line.StartsWith("msgid ");
                bool isMsgStr = !isMsgId && line.StartsWith("msgstr ");

                if (isMsgId || isMsgStr)
                {
                    state = isMsgId ? StateConsumingKey : StateConsumingValue;

                    int firstQuote = line.IndexOf('"');
                    if (firstQuote == -1)
                        continue;

                    int secondQuote = line.LastIndexOf('"');
                    if (secondQuote == -1)
                        continue;

                    var piece = line.Substring(firstQuote + 1, secondQuote - firstQuote - 1).Replace("\\\"", "\"");
                    if (isMsgId)
                    {
                        currentKey = new StringBuilder();
                        currentKey.Append(piece);
                    }
                    else
                    {
                        currentValue = new StringBuilder();
                        currentValue.Append(piece);
                    }
                }
                else if (line[0] == '"')
                {
                    if (line[line.Length - 1] == '"')
                    {
                        line = line.Substring(1, line.Length - 2);
                    }
                    else
                    {
                        line = line.Substring(1, line.Length - 1);
                    }

                    switch (state)
                    {
                        case StateConsumingKey:
                            currentKey.Append(line);
                            break;
                        case StateConsumingValue:
                            currentValue.Append(line);
                            break;
                    }
                }
            }

            return result;
        }
      
        public Dictionary<string, string> Parse(string text)
        {
            return Parse(new StringReader(text));
        }
    }
}
