using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Reflection;
using System.IO;
using System.Resources;
using Instedd.GeoChat.Gettext;

using StringsList = System.Collections.Generic.Dictionary<string, string>;
using System.Xml;

namespace TranslationConverter
{
    class Program
    {
        static Dictionary<string, StringsList> trd_language = new Dictionary<string, StringsList>();

        static void GetStrings(string path, StringsList trd_template)
        {
            string file = Path.GetFileName(path).Split(".".ToCharArray(), 2, StringSplitOptions.None)[0];
            using (ResXResourceReader resx = new ResXResourceReader(path))
            {
                foreach (DictionaryEntry de in resx)
                {
                    trd_template[file + "~" + de.Key] = (string)de.Value;
                }
            }
        }

        static string RemoveLastDir(string path)
        {
            int h = path.LastIndexOfAny(new char[] { '\\' });
            return h != -1 ? path.Remove(h) : path;
        }

        static string printlines(string data)
        {
            string[] lines = data.Split("\n".ToCharArray());
            if (lines.Length <= 2)
                return data;
            else
            {
                string s = "";
                foreach (string l in lines)
                {
                    if (s.Length > 0)
                        s += "\\r\\n\"\r\n\"";
                    else
                        s += "\"\r\n\"";
                    s += l.TrimEnd("\n\r".ToCharArray());
                }
                return s;
            }
        }

        static void Main(string[] args)
        {
            try
            {
                //RemoveLastDir(RemoveLastDir(RemoveLastDir(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))));
                if (args.Length != 1 || !Directory.Exists(args[0]))
                {
                    throw new Exception("Need a valid project directory as a parameter");
                }
                string path = args[0] + "\\TransmissionClientNew";
                Directory.CreateDirectory(path + "\\Languages");
                DirectoryInfo di = new DirectoryInfo(path);
                #region resx files process
                foreach (FileInfo subDir in di.GetFiles("*.*-*.resx"))
                    try
                    {

                        string lang = subDir.FullName.Split(".".ToCharArray())[1];
                        Console.Write("Found " + subDir.ToString() + ", processing...");
                        if (!trd_language.ContainsKey(lang))
                        {
                            trd_language.Add(lang, new Dictionary<string, string>());
                        }
                        GetStrings(subDir.FullName, trd_language[lang]);
                        Console.WriteLine("done");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.ToString());
                    }
                #endregion

                Dictionary<string, string> template = trd_language["en-US"];
#if !ONLY_TRANSLATE
                foreach (KeyValuePair<string, StringsList> l in trd_language)
                {
                    StringsList sl = trd_language[l.Key];
                    #region generate template and pofiles
                    Dictionary<string, string> nyelv = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, string> item in template)
                    {
                        string k = item.Key;
                        if (nyelv.ContainsKey(k) && !nyelv[k].Equals(""))
                            continue;
                        if (!l.Key.Equals("en-US"))
                        {
                            string v = sl.ContainsKey(k) ? sl[k] : item.Value;
                            if (!nyelv.ContainsKey(item.Value))
                                nyelv[item.Value] = "";
                            if (!nyelv[item.Value].Equals(v) && !item.Value.Equals(v))
                                nyelv[item.Value] = v;
                        }
                        else
                            nyelv[item.Value] = "";
                    }

                    string pofile = path + "\\Languages\\" + (l.Key.Equals("en-US") ? "template.pot" : l.Key + ".po");
                    if (l.Key.Equals("en-US") || !File.Exists(pofile))
                    {
                        using (TextWriter tw = new StreamWriter(pofile))
                        {
                            CultureInfo ci = new CultureInfo(l.Key);
                            if (l.Key.Equals("en-US"))
                            {
                                tw.WriteLine("# SOME DESCRIPTIVE TITLE.");
                            }
                            else
                            {
                                string tlang = !ci.Parent.Name.Equals("") ? ci.Parent.EnglishName : ci.EnglishName;
                                tw.WriteLine("# {0} translation of transmission-remote-dotnet", tlang);
                            }
                            tw.WriteLine("# Copyright (C) 2009 Alan F");
                            tw.WriteLine("# This file is distributed under the same license as the transmission-remote-dotnet package.");
                            tw.WriteLine("#");
                            tw.WriteLine("# FIRST AUTHOR <EMAIL@ADDRESS>, 2009.");
                            tw.WriteLine("msgid \"\"");
                            tw.WriteLine("msgstr \"\"");
                            tw.WriteLine("\"Project-Id-Version: transmission-remote-dotnet\\n\"");
                            tw.WriteLine("\"Report-Msgid-Bugs-To: \\n\"");
                            tw.WriteLine("\"POT-Creation-Date: 2009-12-13 22:55+0100\\n\"");
                            tw.WriteLine("\"PO-Revision-Date: YEAR-MO-DA HO:MI+ZONE\\n\"");
                            tw.WriteLine("\"Last-Translator: FULL NAME <EMAIL@ADDRESS>\\n\"");
                            tw.WriteLine("\"Language-Team: transmission-remote-dotnet <transmission-remote-dotnet@googlegroups.com>\\n\"");
                            tw.WriteLine("\"MIME-Version: 1.0\\n\"");
                            tw.WriteLine("\"Content-Type: text/plain; charset=UTF-8\\n\"");
                            tw.WriteLine("\"Content-Transfer-Encoding: 8bit\\n\"");
                            tw.WriteLine();

                            foreach (KeyValuePair<string, string> item in nyelv)
                            {
                                foreach (KeyValuePair<string, string> t in template)
                                {
                                    if (t.Value.Equals(item.Key))
                                        tw.WriteLine("#: {0}", t.Key);
                                }
                                tw.WriteLine("#, csharp-format");
                                tw.WriteLine("msgid \"{0}\"", printlines(item.Key.Replace("\"", "\\\"")));
                                tw.WriteLine("msgstr \"{0}\"", printlines(item.Value.Replace("\"", "\\\"")));
                                tw.WriteLine();
                            }
                        }
                    }
                    #endregion
                }
#endif
                PoParser poparser = new PoParser();
                XmlDocument resx = new XmlDocument();
                Dictionary<string, StringsList> podatas = new Dictionary<string, Dictionary<string, string>>();
                foreach (FileInfo subDir in di.GetFiles("*.*-*.resx"))
                    try
                    {
                        string lang = subDir.FullName.Split(".".ToCharArray())[1];
                        string cat = Path.GetFileName(subDir.FullName).Split(".".ToCharArray())[0];
                        if (lang.Equals("en-US") || !trd_language.ContainsKey(lang))
                            continue;
                        Console.Write("Found " + subDir.ToString() + ", translating...");

                        StringsList sl = trd_language[lang];
                        string pofile = path + "\\Languages\\" + lang + ".po";
                        string resxfile = path + "\\Languages\\" + subDir.Name;
                        Dictionary<string, string> podata;
                        if (!podatas.ContainsKey(lang))
                        {
                            using (var reader = new StreamReader(pofile))
                            {
                                podata = poparser.Parse(reader);
                            }
                            podatas[lang] = podata;
                        }
                        else
                            podata = podatas[lang];
                        resx.Load(subDir.FullName);
                        bool changed = false;
                        XmlElement root = resx.DocumentElement;
                        XmlNodeList datas = root.GetElementsByTagName("data");
                        foreach (XmlNode data in datas)
                        {
                            string name = data.Attributes["name"].Value;
                            string value, value_eng, value_new;
                            value = value_eng = value_new = data["value"].InnerText;
                            if (template.ContainsKey(cat + "~" + name))
                            {
                                value_eng = template[cat + "~" + name];
                            }
                            if (podata.ContainsKey(value_eng))
                            {
                                string s = podata[value_eng];
                                if (s.Length > 0)
                                    value_new = s;
                            }
                            if (!value_new.Equals(value))
                            {
                                data["value"].InnerText = value_new;
                                changed = true;
                            }
                        }
                        if (changed)
                            resx.Save(resxfile);
                        Console.WriteLine("done");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.ToString());
                    }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }

    }
}
