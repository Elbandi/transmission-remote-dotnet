using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;

namespace GetVersion
{
    class GetVersion
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 1)
                    throw new Exception("One exe need");
                Assembly assembly = Assembly.LoadFile(Path.GetFullPath(args[0]));
                Version ver = assembly.GetName().Version;
                Console.WriteLine(string.Format("{0}.{1}", ver.Major, ver.Minor));
                Environment.ExitCode = 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Source + ": " + e.Message);
                Environment.ExitCode = 1;
            }
        }
    }
}
