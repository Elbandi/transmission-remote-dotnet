using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Reflection;
using PaintDotNet.SystemLayer;

namespace TransmissionRemoteDotnet
{
    class UnhandledException
    {
        Exception ex;
        DateTime startupTime;
        string fileName;

        public string FileName
        {
            get { return fileName; }
        }

        public UnhandledException(Exception ex, DateTime startupTime)
        {
            this.ex = ex;
            this.startupTime = startupTime;
            this.fileName = string.Format("trdcrash_{0}.log", DateTime.Now.ToUniversalTime().ToString("yyyyMMdd_HHmmss"));
        }

        public void DoLog()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string fullName = Path.Combine(dir, fileName);

            using (StreamWriter stream = new System.IO.StreamWriter(fullName, true))
            {
                stream.AutoFlush = true;
                WriteCrashLog(ex, stream);
            }
        }

        public static string GetApplicationDir()
        {
            return Application.StartupPath;
        }

        /// <summary>
        /// Returns a full version string of the form: ApplicationConfiguration + BuildType + BuildVersion
        /// i.e.: "Beta 2 Debug build 1.0.*.*"
        /// </summary>
        /// <returns></returns>
        public static string GetVersionString()
        {
            string buildType =
#if DEBUG
 "Debug";
#else
            "Release";
#endif

            string versionFormat = "{0} build {1}"; // {0} is Debug or Release. {1} is the product version.
            string versionText = string.Format(
                versionFormat,
                buildType,
                GetVersionNumberString(new Version(Application.ProductVersion), 4));

            return versionText;
        }


        /// <summary>
        /// Returns a string for just the version number, i.e. "3.01"
        /// </summary>
        /// <returns></returns>
        public static string GetVersionNumberString(Version version, int fieldCount)
        {
            if (fieldCount < 1 || fieldCount > 4)
            {
                throw new ArgumentOutOfRangeException("fieldCount", "must be in the range [1, 4]");
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(version.Major.ToString());

            if (fieldCount >= 2)
            {
                sb.AppendFormat(".{0}", version.Minor.ToString("D2"));
            }

            if (fieldCount >= 3)
            {
                sb.AppendFormat(".{0}", version.Build.ToString());
            }

            if (fieldCount == 4)
            {
                sb.AppendFormat(".{0}", version.Revision.ToString());
            }

            return sb.ToString();
        }

        private void WriteCrashLog(Exception ex, TextWriter stream)
        {
            const string noInfoString = "err";

            string fullAppName = noInfoString;
            string timeOfCrash = noInfoString;
            string appUptime = noInfoString;
            string osVersion = noInfoString;
            string osRevision = noInfoString;
            string osType = noInfoString;
            string processorNativeArchitecture = noInfoString;
            string clrVersion = noInfoString;
            string fxInventory = noInfoString;
            string processorArchitecture = noInfoString;
            string cpuName = noInfoString;
            string cpuCount = noInfoString;
            string cpuSpeed = noInfoString;
            string cpuFeatures = noInfoString;
            string totalPhysicalBytes = noInfoString;
            string dpiInfo = noInfoString;
            string localeName = noInfoString;
            string inkInfo = noInfoString;
            string updaterInfo = noInfoString;
            string featuresInfo = noInfoString;
            string assembliesInfo = noInfoString;
            string transmissionVersion = noInfoString;

            stream.WriteLine("This text file was created because transmission-remote-dotnet crashed. Please search similar issue in http://code.google.com/p/transmission-remote-dotnet/issues/list or open a new one. Attach this file to your comment so we can diagnose and fix the problem.");

            try
            {
                try
                {
                    string fullAppNameFormat = "{0} ({1})";
                    //    <comment>{0} is the product name ("transmission-remote-dotnet"), {1} is the version string ("2.2.2000.3000")</comment>
                    fullAppName = string.Format(fullAppNameFormat, "TRD", GetVersionString());
                }
                catch (Exception ex1)
                {
                    fullAppName = Application.ProductVersion + ", --- Exception while calling PdnInfo.GetFullAppName(): " + ex1.ToString() + Environment.NewLine;
                }

                try
                {
                    string fullAppNameFormat = "{0} ({1})";
                    transmissionVersion = string.Format(fullAppNameFormat, Program.DaemonDescriptor.Version, Program.DaemonDescriptor.Revision);
                }
                catch (Exception ex1)
                {
                    transmissionVersion = Program.DaemonDescriptor.Version.ToString() + " " + Program.DaemonDescriptor.Revision.ToString() +
                        ", --- Exception while calling PdnInfo.GetFullAppName(): " + ex1.ToString() + Environment.NewLine;
                }

                try
                {
                    timeOfCrash = DateTime.Now.ToString();
                }
                catch (Exception ex2)
                {
                    timeOfCrash = "--- Exception while populating timeOfCrash: " + ex2.ToString() + Environment.NewLine;
                }

                try
                {
                    appUptime = (DateTime.Now - startupTime).ToString();
                }
                catch (Exception ex13)
                {
                    appUptime = "--- Exception while populating appUptime: " + ex13.ToString() + Environment.NewLine;
                }

                try
                {
                    osVersion = System.Environment.OSVersion.Version.ToString();
                }
                catch (Exception ex3)
                {
                    osVersion = "--- Exception while populating osVersion: " + ex3.ToString() + Environment.NewLine;
                }

                try
                {
                    osRevision = OS.Revision;
                }
                catch (Exception ex4)
                {
                    osRevision = "--- Exception while populating osRevision: " + ex4.ToString() + Environment.NewLine;
                }

                try
                {
                    osType = OS.Type.ToString();
                }
                catch (Exception ex5)
                {
                    osType = "--- Exception while populating osType: " + ex5.ToString() + Environment.NewLine;
                }

                try
                {
                    processorNativeArchitecture = Processor.NativeArchitecture.ToString().ToLower();
                }
                catch (Exception ex6)
                {
                    processorNativeArchitecture = "--- Exception while populating processorNativeArchitecture: " + ex6.ToString() + Environment.NewLine;
                }

                try
                {
                    clrVersion = System.Environment.Version.ToString();
                }
                catch (Exception ex7)
                {
                    clrVersion = "--- Exception while populating clrVersion: " + ex7.ToString() + Environment.NewLine;
                }

                try
                {
                    fxInventory =
                        (OS.IsDotNetVersionInstalled(2, 0, 0, false) ? "2.0 " : "") +
                        (OS.IsDotNetVersionInstalled(2, 0, 1, false) ? "2.0SP1 " : "") +
                        (OS.IsDotNetVersionInstalled(2, 0, 2, false) ? "2.0SP2 " : "") +
                        (OS.IsDotNetVersionInstalled(3, 0, 0, false) ? "3.0 " : "") +
                        (OS.IsDotNetVersionInstalled(3, 0, 1, false) ? "3.0SP1 " : "") +
                        (OS.IsDotNetVersionInstalled(3, 0, 2, false) ? "3.0SP2 " : "") +
                        (OS.IsDotNetVersionInstalled(3, 5, 0, false) ? "3.5 " : "") +
                        (OS.IsDotNetVersionInstalled(3, 5, 1, false) ? "3.5SP1 " : "") +
                        (OS.IsDotNetVersionInstalled(3, 5, 1, true) ? "3.5SP1_Client " : "") +
                        (OS.IsDotNetVersionInstalled(3, 5, 2, false) ? "3.5SP2 " : "") +
                        (OS.IsDotNetVersionInstalled(4, 0, 0, false) ? "4.0 " : "") +
                        (OS.IsDotNetVersionInstalled(4, 0, 1, false) ? "4.0SP1 " : "") +
                        (OS.IsDotNetVersionInstalled(4, 0, 2, false) ? "4.0SP2 " : "")
                        .Trim();
                }
                catch (Exception ex30)
                {
                    fxInventory = "--- Exception while populating fxInventory: " + ex30.ToString() + Environment.NewLine;
                }

                try
                {
                    processorArchitecture = Processor.Architecture.ToString().ToLower();
                }
                catch (Exception ex8)
                {
                    processorArchitecture = "--- Exception while populating processorArchitecture: " + ex8.ToString() + Environment.NewLine;
                }

                try
                {
                    cpuName = Processor.CpuName;
                }
                catch (Exception ex9)
                {
                    cpuName = "--- Exception while populating cpuName: " + ex9.ToString() + Environment.NewLine;
                }

                try
                {
                    cpuCount = Processor.LogicalCpuCount.ToString() + "x";
                }
                catch (Exception ex10)
                {
                    cpuCount = "--- Exception while populating cpuCount: " + ex10.ToString() + Environment.NewLine;
                }

                try
                {
                    cpuSpeed = "@ ~" + Processor.ApproximateSpeedMhz.ToString() + "MHz";
                }
                catch (Exception ex16)
                {
                    cpuSpeed = "--- Exception while populating cpuSpeed: " + ex16.ToString() + Environment.NewLine;
                }

                try
                {
                    cpuFeatures = string.Empty;
                    string[] featureNames = Enum.GetNames(typeof(ProcessorFeature));
                    bool firstFeature = true;

                    for (int i = 0; i < featureNames.Length; ++i)
                    {
                        string featureName = featureNames[i];
                        ProcessorFeature feature = (ProcessorFeature)Enum.Parse(typeof(ProcessorFeature), featureName);
                        if (Processor.IsFeaturePresent(feature))
                        {
                            if (firstFeature)
                            {
                                cpuFeatures = "(";
                                firstFeature = false;
                            }
                            else
                            {
                                cpuFeatures += ", ";
                            }
                            cpuFeatures += featureName;
                        }
                    }
                    if (cpuFeatures.Length > 0)
                    {
                        cpuFeatures += ")";
                    }
                }
                catch (Exception ex17)
                {
                    cpuFeatures = "--- Exception while populating cpuFeatures: " + ex17.ToString() + Environment.NewLine;
                }

                try
                {
                    totalPhysicalBytes = ((Memory.TotalPhysicalBytes / 1024) / 1024) + " MB";
                }
                catch (Exception ex11)
                {
                    totalPhysicalBytes = "--- Exception while populating totalPhysicalBytes: " + ex11.ToString() + Environment.NewLine;
                }

                try
                {
                    float xScale;

                    try
                    {
                        xScale = UI.GetXScaleFactor();
                    }
                    catch (Exception)
                    {
                        using (Control c = new Control())
                        {
                            UI.InitScaling(c);
                            xScale = UI.GetXScaleFactor();
                        }
                    }
                    dpiInfo = string.Format("{0} dpi ({1}x scale)", (96.0f * xScale).ToString("F2"), xScale.ToString("F2"));
                }
                catch (Exception ex19)
                {
                    dpiInfo = "--- Exception while populating dpiInfo: " + ex19.ToString() + Environment.NewLine;
                }

                try
                {
                    localeName =
                        //                        "pdnr.c: " + CultureInfo.CurrentUICulture.Name +
                        //                        ", hklm: " + Settings.SystemWide.GetString(LanguageName, "n/a") +
                        //                        ", hkcu: " + Settings.CurrentUser.GetString(SettingNames.LanguageName, "n/a") +
                        "cc: " + CultureInfo.CurrentCulture.Name +
                        ", cuic: " + CultureInfo.CurrentUICulture.Name;
                }
                catch (Exception ex14)
                {
                    localeName = "--- Exception while populating localeName: " + ex14.ToString() + Environment.NewLine;
                }
#if DUMMY
                try
                {
                    string autoCheckForUpdates = Settings.SystemWide.GetString(SettingNames.AutoCheckForUpdates, noInfoString);

                    string lastUpdateCheckTimeInfo;

                    try
                    {
                        string lastUpdateCheckTimeString = Settings.CurrentUser.Get(SettingNames.LastUpdateCheckTimeTicks);
                        long lastUpdateCheckTimeTicks = long.Parse(lastUpdateCheckTimeString);
                        DateTime lastUpdateCheckTime = new DateTime(lastUpdateCheckTimeTicks);
                        lastUpdateCheckTimeInfo = lastUpdateCheckTime.ToShortDateString();
                    }
                    catch (Exception)
                    {
                        lastUpdateCheckTimeInfo = noInfoString;
                    }

                    updaterInfo = string.Format(
                        "{0}, {1}",
                        (autoCheckForUpdates == "1") ? "true" : (autoCheckForUpdates == "0" ? "false" : (autoCheckForUpdates ?? "null")),
                        lastUpdateCheckTimeInfo);
                }
                catch (Exception ex17)
                {
                    updaterInfo = "--- Exception while populating updaterInfo: " + ex17.ToString() + Environment.NewLine;
                }
#endif
                try
                {
                    StringBuilder featureSB = new StringBuilder();
                    IEnumerable<string> featureList = Tracing.GetLoggedFeatures();
                    bool first = true;
                    foreach (string feature in featureList)
                    {
                        if (!first)
                        {
                            featureSB.Append(", ");
                        }
                        featureSB.Append(feature);
                        first = false;
                    }
                    featuresInfo = featureSB.ToString();
                }
                catch (Exception ex18)
                {
                    featuresInfo = "--- Exception while populating featuresInfo: " + ex18.ToString() + Environment.NewLine;
                }

                try
                {
                    StringBuilder assembliesInfoSB = new StringBuilder();
                    Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (Assembly assembly in loadedAssemblies)
                    {
                        assembliesInfoSB.AppendFormat("{0}    {1} @ {2}", Environment.NewLine, assembly.FullName, assembly.Location);
                    }
                    assembliesInfo = assembliesInfoSB.ToString();
                }
                catch (Exception ex16)
                {
                    assembliesInfo = "--- Exception while populating assembliesInfo: " + ex16.ToString() + Environment.NewLine;
                }
            }
            catch (Exception ex12)
            {
                stream.WriteLine("Exception while gathering app and system info: " + ex12.ToString());
            }

            stream.WriteLine();
            stream.WriteLine("Exception details:");

            if (ex == null)
            {
                stream.WriteLine("(null)");
            }
            else
            {
                stream.WriteLine(ex.ToString());

                if (ex.InnerException != null)
                {
                    stream.WriteLine("InnerException details:");
                    stream.WriteLine(ex.InnerException.ToString());
                }

                // Determine if there is any 'secondary' exception to report
                Exception[] otherEx = null;

                if (ex is System.Reflection.ReflectionTypeLoadException)
                {
                    otherEx = ((System.Reflection.ReflectionTypeLoadException)ex).LoaderExceptions;
                }

                if (otherEx != null)
                {
                    for (int i = 0; i < otherEx.Length; ++i)
                    {
                        stream.WriteLine();
                        stream.WriteLine("Secondary exception details:");

                        if (otherEx[i] == null)
                        {
                            stream.WriteLine("(null)");
                        }
                        else
                        {
                            stream.WriteLine(otherEx[i].ToString());
                        }
                    }
                }
            }

            stream.WriteLine("------------------------------------------------------------------------------");
            stream.WriteLine();

            stream.WriteLine("Application version: " + fullAppName);
            stream.WriteLine("Transmission version: " + transmissionVersion);
            stream.WriteLine("Time of crash: " + timeOfCrash);
            stream.WriteLine("Application uptime: " + appUptime);

            stream.WriteLine("OS Version: " + osVersion + (string.IsNullOrEmpty(osRevision) ? "" : (" " + osRevision)) + " " + osType + " " + processorNativeArchitecture);
            stream.WriteLine(".NET version: CLR " + clrVersion + " " + processorArchitecture + ", FX " + fxInventory);
            stream.WriteLine("Processor: " + cpuCount + " \"" + cpuName + "\" " + cpuSpeed + " " + cpuFeatures);
            stream.WriteLine("Physical memory: " + totalPhysicalBytes);
            stream.WriteLine("UI DPI: " + dpiInfo);
            stream.WriteLine("Tablet PC: " + inkInfo);
            stream.WriteLine("Updates: " + updaterInfo);
            stream.WriteLine("Locale: " + localeName);
            stream.WriteLine("Features log: " + featuresInfo);
            stream.WriteLine("Loaded assemblies: " + assembliesInfo);

            stream.Flush();
        }
    }
}
