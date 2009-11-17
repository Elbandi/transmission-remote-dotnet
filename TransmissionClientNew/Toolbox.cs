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

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Jayrock.Json;
using System.Net;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace TransmissionRemoteDotnet
{
    public class Toolbox
    {
        private const int STRIPE_OFFSET = 15;
        public static readonly IFormatProvider NUMBER_FORMAT = (new CultureInfo("en-GB")).NumberFormat;

        public static decimal ToProgress(object o)
        {
            decimal result = Math.Round(ToDecimal(o), 2);
            decimal resultMultiplied = result * 100;
            return resultMultiplied <= 100 ? resultMultiplied : result;
        }

        public static string TrimPath(string s)
        {
            int fwdSlashPos = s.IndexOf('/');
            if (fwdSlashPos > 0)
            {
                return s.Remove(0, fwdSlashPos + 1);
            }
            else
            {
                int bckSlashPos = s.IndexOf('\\');
                if (bckSlashPos > 0)
                {
                    return s.Remove(0, bckSlashPos + 1);
                }
            }
            return s;
        }

        public static double ToDouble(object o)
        {
            if (o.GetType().Equals(typeof(string)))
            {
                return double.Parse((string)o, NUMBER_FORMAT);
            }
            else
            {
                return ((JsonNumber)o).ToDouble();
            }
        }

        public static long ToLong(object o)
        {
            return ((JsonNumber)o).ToInt64();
        }

        public static int ToInt(object o)
        {
            if (o != null) {
                return ((JsonNumber)o).ToInt32();
            } else {
                return 0;
            }
        }

        public static decimal ToDecimal(object o)
        {
            if (o.GetType().Equals(typeof(string)))
            {
                return decimal.Parse((string)o, NUMBER_FORMAT);
            }
            else
            {
                return ((JsonNumber)o).ToDecimal();
            }
        }

        public static JsonArray ListViewSelectionToIdArray(ListView.SelectedListViewItemCollection selections)
        {
            JsonArray ids = new JsonArray();
            foreach (ListViewItem item in selections)
            {
                Torrent t = (Torrent)item.Tag;
                ids.Put(t.Id);
            }
            return ids;
        }

        public static void CopyListViewToClipboard(ListView listView)
        {
            StringBuilder sb = new StringBuilder();
            if (listView.SelectedItems.Count > 1)
            {
                for (int i = 0; i < listView.Columns.Count; i++)
                {
                    sb.Append(listView.Columns[i].Text);
                    if (i != listView.Columns.Count - 1)
                    {
                        sb.Append(',');
                    }
                    else
                    {
                        sb.Append(Environment.NewLine);
                    }
                }
            }
            lock (listView)
            {
                foreach (ListViewItem item in listView.SelectedItems)
                {
                    for (int i = 0; i < item.SubItems.Count; i++)
                    {
                        ListViewItem.ListViewSubItem si = item.SubItems[i];
                        sb.Append(si.Text.Contains(",") ? "\""+si.Text+"\"" : si.Text);
                        if (i != item.SubItems.Count - 1)
                        {
                            sb.Append(',');
                        }
                        else
                        {
                            sb.Append(Environment.NewLine);
                        }
                    }
                }
            }
            Clipboard.SetText(sb.ToString());
        }

        public static void StripeListView(ListView list)
        {
            Color window = SystemColors.Window;
            lock (list)
            {
                list.SuspendLayout();
                foreach (ListViewItem item in list.Items)
                {
                    item.BackColor = item.Index % 2 == 1 ?
                        Color.FromArgb(window.R - STRIPE_OFFSET,
                            window.G - STRIPE_OFFSET,
                            window.B - STRIPE_OFFSET)
                        : window;
                }
                list.ResumeLayout();
            }
        }

        public static short ToShort(object o)
        {
            return ((JsonNumber)o).ToInt16();
        }

        public static Boolean ToBool(object o)
        {
            if (o.GetType().Equals(typeof(Boolean)))
            {
                return (Boolean)o;
            }
            else
            {
                return ((JsonNumber)o).ToBoolean();
            }
        }

        public static DateTime DateFromEpoch(double e)
        {
            DateTime epoch = new DateTime(1970, 1, 1);
            return epoch.Add(TimeSpan.FromSeconds(e));
        }

        public static decimal CalcPercentage(long x, long total)
        {
            if (total > 0)
            {
                return Math.Round((x / (decimal)total) * 100, 2);
            }
            else
            {
                return 100;
            }
        }

        public static decimal CalcRatio(long upload_total, long download_total)
        {
            if (download_total <= 0 || upload_total <= 0)
            {
                return -1;
            }
            else
            {
                return Math.Round((decimal)upload_total / download_total, 3);
            }
        }

        public static string KbpsString(int rate)
        {
            return String.Format("{0} {1}/{2}", rate, OtherStrings.KilobyteShort, OtherStrings.Second.ToLower()[0]);
        }
        
        public static string FormatTimespanLong(TimeSpan span)
        {
            return String.Format("{0}{1} {2}{3} {4}{5} {6}{7}", new object[] { span.Days, OtherStrings.Day.ToLower()[0], span.Hours, OtherStrings.Hour.ToLower()[0], span.Minutes, OtherStrings.Minute.ToLower()[0], span.Seconds, OtherStrings.Second.ToLower()[0] });
        }

        public static string GetSpeed(long bytes)
        {
            return String.Format("{0}/{1}", GetFileSize(bytes), OtherStrings.Second.ToLower()[0]);
        }

        public static string GetSpeed(object o)
        {
            return GetSpeed(ToLong(o));
        }

        public static string GetFileSize(long bytes)
        {
            if (bytes >= 1073741824)
            {
                Decimal size = Decimal.Divide(bytes, 1073741824);
                return String.Format("{0:##.##} {1}", size, OtherStrings.GigabyteShort);
            }
            else if (bytes >= 1048576)
            {
                Decimal size = Decimal.Divide(bytes, 1048576);
                return String.Format("{0:##.##} {1}", size, OtherStrings.MegabyteShort);
            }
            else if (bytes >= 1024)
            {
                Decimal size = Decimal.Divide(bytes, 1024);
                return String.Format("{0:##.##} {1}", size, OtherStrings.KilobyteShort);
            }
            else if (bytes > 0 & bytes < 1024)
            {
                Decimal size = bytes;
                return String.Format("{0:##.##} {1}", size, OtherStrings.Byte[0]);
            }
            else
            {
                return "0 " + OtherStrings.Byte[0];
            }
        }

        public static string SupportFilePath(string file)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), file);
        }

        public static void SelectAll(ListView lv)
        {
            lock (lv)
            {
                lv.SuspendLayout();
                foreach (ListViewItem item in lv.Items)
                {
                    item.Selected = true;
                }
                lv.ResumeLayout();
            }
        }
    }
}