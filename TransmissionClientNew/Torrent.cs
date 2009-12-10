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
using Jayrock.Json;
using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;

namespace TransmissionRemoteDotnet
{
    public class Torrent
    {
        private string SeedersColumnFormat = "{1}";

        private ListViewItem item;

        public ListViewItem Item
        {
            get { return item; }
        }
        private JsonObject info;

        public JsonObject Info
        {
            get { return info; }
        }
        private long updateSerial;

        public long UpdateSerial
        {
            get { return updateSerial; }
        }

        private void UpdateIcon()
        {
            if (this.HasError)
            {
                this.item.ImageIndex = 6;
            }
            else if (this.StatusCode == ProtocolConstants.STATUS_CHECKING || this.StatusCode == ProtocolConstants.STATUS_WAITING_TO_CHECK)
            {
                this.item.ImageIndex = 5;
            }
            else if (this.StatusCode == ProtocolConstants.STATUS_SEEDING)
            {
                this.item.ImageIndex = 4;
            }
            else if (this.StatusCode == ProtocolConstants.STATUS_DOWNLOADING)
            {
                this.item.ImageIndex = 1;
            }
            else if (this.StatusCode == ProtocolConstants.STATUS_PAUSED)
            {
                this.item.ImageIndex = 2;
            }
            else
            {
                this.item.ImageIndex = -1;
            }
        }

        public Torrent(JsonObject info)
        {
            this.updateSerial = Program.DaemonDescriptor.UpdateSerial;
            this.info = info;
            Console.WriteLine(this.Trackers);
            item = new ListViewItem(this.Name);
            UpdateIcon();
            if (this.HasError)
            {
                item.ForeColor = Color.Red;
            }
            SeedersColumnFormat = "{0} ({1})";
            item.ToolTipText = item.Name;
            item.Tag = this;
            item.SubItems.Add(Toolbox.GetFileSize(this.SizeWhenDone));
            decimal percentage = this.StatusCode == ProtocolConstants.STATUS_CHECKING ? this.RecheckPercentage : this.Percentage;
            item.SubItems.Add(percentage.ToString() + "%");
            item.SubItems[2].Tag = percentage;
            item.SubItems.Add(this.Status);
            item.SubItems.Add(string.Format(SeedersColumnFormat, (this.Seeders < 0 ? "?" : this.Seeders.ToString()), this.PeersSendingToUs));
            item.SubItems.Add(string.Format(SeedersColumnFormat, (this.Leechers < 0 ? "?" : this.Leechers.ToString()), this.PeersGettingFromUs));
            item.SubItems.Add(this.StatusCode == ProtocolConstants.STATUS_DOWNLOADING && this.Percentage <= 100 ? this.DownloadRate : "");
            item.SubItems.Add(this.StatusCode == ProtocolConstants.STATUS_SEEDING || this.StatusCode == ProtocolConstants.STATUS_DOWNLOADING ? this.UploadRate : "");
            item.SubItems.Add(this.GetShortETA());
            item.SubItems.Add(this.UploadedString);
            item.SubItems.Add(this.LocalRatioString);
            item.SubItems.Add(this.Added.ToString());
            if (this.IsFinished)
            {
                object doneDate = this.DoneDate;
                if (doneDate != null)
                {
                    item.SubItems.Add(doneDate.ToString());
                }
                else
                {
                    item.SubItems.Add("?");
                }
            }
            else
            {
                item.SubItems.Add("");
            }
            item.SubItems.Add(GetFirstTracker(true));
            lock (Program.TorrentIndex)
            {
                Program.TorrentIndex[this.Hash] = this;
            }
            Add();
        }

        private delegate void AddDelegate();
        private void Add()
        {
            MainWindow form = Program.Form;
            if (form.InvokeRequired)
            {
                form.Invoke(new AddDelegate(this.Add));
            }
            else
            {
                lock (form.torrentListView)
                {
                    form.torrentListView.Items.Add(item);
                }
                lock (form.stateListBox)
                {
                    if (form.stateListBox.FindItem(item.SubItems[13].Text) == null)
                    {
                        form.stateListBox.Items.Add(new GListBoxItem(item.SubItems[13].Text, 8));
                    }
                }
                if (Program.Settings.StartedBalloon && this.updateSerial > 2)
                {
                    Program.Form.notifyIcon.ShowBalloonTip(LocalSettingsSingleton.BALLOON_TIMEOUT, this.Name, String.Format(OtherStrings.NewTorrentIs, this.Status.ToLower()), ToolTipIcon.Info);
                }
                LogError();
            }
        }

        private void LogError()
        {
            if (this.HasError)
            {
                List<ListViewItem> logItems = Program.LogItems;
                lock (logItems)
                {
                    if (logItems.Count > 0)
                    {
                        foreach (ListViewItem item in logItems)
                        {
                            if (item.Tag != null && this.updateSerial - (long)item.Tag < 2 && item.SubItems[1].Text.Equals(this.Name) && item.SubItems[2].Text.Equals(this.ErrorString))
                            {
                                item.Tag = this.updateSerial;
                                return;
                            }
                        }
                    }
                }
                Program.Log(this.Name, this.ErrorString, this.updateSerial);
            }
        }

        public void Show()
        {
            ListView.ListViewItemCollection itemCollection = Program.Form.torrentListView.Items;
            if (!itemCollection.Contains(item))
            {
                lock (Program.Form.torrentListView)
                {
                    if (!itemCollection.Contains(item))
                    {
                        itemCollection.Add(item);
                    }
                }
            }
        }

        public void Remove()
        {
            MainWindow form = Program.Form;
            int matchingTrackers = 0;
            ListView.ListViewItemCollection itemCollection = form.torrentListView.Items;
            if (itemCollection.Contains(item))
            {
                lock (form.torrentListView)
                {
                    if (itemCollection.Contains(item))
                    {
                        itemCollection.Remove(item);
                    }
                }
            }
            else
            {
                return;
            }
            lock (Program.TorrentIndex)
            {
                foreach (KeyValuePair<string, Torrent> pair in Program.TorrentIndex)
                {
                    if (this.item.SubItems[13].Text.Equals(pair.Value.item.SubItems[13].Text))
                    {
                        matchingTrackers++;
                    }
                }
            }
            if (matchingTrackers <= 0)
            {
                lock (form.stateListBox)
                {
                    form.stateListBox.RemoveItem(item.SubItems[13].Text);
                }
            }
        }

        public delegate bool UpdateDelegate(JsonObject info);
        public bool Update(JsonObject info)
        {
            MainWindow form = Program.Form;
            if (form.InvokeRequired)
            {
                return (bool)form.Invoke(new UpdateDelegate(this.Update), info);
            }
            else
            {
                if (Program.Settings.CompletedBaloon
                    && form.notifyIcon.Visible == true
                    && this.StatusCode == ProtocolConstants.STATUS_DOWNLOADING
                    && this.LeftUntilDone > 0
                    && (Toolbox.ToLong(info[ProtocolConstants.FIELD_LEFTUNTILDONE]) == 0))
                {
                    form.notifyIcon.ShowBalloonTip(LocalSettingsSingleton.BALLOON_TIMEOUT, this.Name, "This torrent has finished downloading.", ToolTipIcon.Info);
                    item.SubItems[12].Text = DateTime.Now.ToString();
                    item.SubItems[12].Tag = DateTime.Now;
                }
                bool stateChange = (this.StatusCode != Toolbox.ToShort(info[ProtocolConstants.FIELD_STATUS])) || (this.HasError != IsErrorString((string)info[ProtocolConstants.FIELD_ERRORSTRING]));
                this.info = info;
                UpdateIcon();
                item.SubItems[0].Text = this.Name;
                item.ForeColor = this.HasError ? Color.Red : SystemColors.WindowText;
                item.SubItems[1].Text = Toolbox.GetFileSize(this.SizeWhenDone);
                decimal percentage = this.StatusCode == ProtocolConstants.STATUS_CHECKING ? this.RecheckPercentage : this.Percentage;
                item.SubItems[2].Tag = percentage;
                item.SubItems[2].Text = percentage.ToString() + "%";
                item.SubItems[3].Text = this.Status;
                item.SubItems[4].Text = string.Format(SeedersColumnFormat, (this.Seeders < 0 ? "?" : this.Seeders.ToString()), this.PeersSendingToUs);
                item.SubItems[5].Text = string.Format(SeedersColumnFormat, (this.Leechers < 0 ? "?" : this.Leechers.ToString()), this.PeersGettingFromUs);
                item.SubItems[6].Text = this.StatusCode == ProtocolConstants.STATUS_DOWNLOADING && this.Percentage <= 100 ? this.DownloadRate : "";
                item.SubItems[7].Text = this.StatusCode == ProtocolConstants.STATUS_SEEDING || this.StatusCode == ProtocolConstants.STATUS_DOWNLOADING ? this.UploadRate : "";
                item.SubItems[8].Text = this.GetShortETA();
                item.SubItems[9].Text = this.UploadedString;
                item.SubItems[10].Text = this.LocalRatioString;
                item.SubItems[11].Text = this.Added.ToString();
                this.updateSerial = Program.DaemonDescriptor.UpdateSerial;
                LogError();
                return stateChange;
            }
        }

        public JsonArray Peers
        {
            get
            {
                return (JsonArray)info[ProtocolConstants.FIELD_PEERS];
            }
        }

        public int PieceCount
        {
            get
            {
                return Toolbox.ToInt(info[ProtocolConstants.FIELD_PIECECOUNT]);
            }
        }

        public int PieceSize
        {
            get
            {
                return Toolbox.ToInt(info[ProtocolConstants.FIELD_PIECESIZE]);
            }
        }

        public int HavePieces
        {
            get
            {
                return Toolbox.BitCount(this.Pieces);
                //return Toolbox.ToInt(info[ProtocolConstants.FIELD_PIECECOMPLETE]);
            }
        }
        public double SeedRatioLimit
        {
            get
            {
                return Toolbox.ToDouble(info[ProtocolConstants.FIELD_SEEDRATIOLIMIT]);
            }
        }

        public bool SeedRatioMode
        {
            get
            {
                return Toolbox.ToInt(info[ProtocolConstants.FIELD_SEEDRATIOMODE]) > 0;
            }
        }

        public bool HonorsSessionLimits
        {
            get
            {
                return Toolbox.ToBool(info[ProtocolConstants.FIELD_HONORSSESSIONLIMITS]);
            }
        }

        public byte[] Pieces
        {
            get
            {
                if (info.Contains(ProtocolConstants.FIELD_PIECES))
                {
                    string pieces = (string)info[ProtocolConstants.FIELD_PIECES];
                    return Convert.FromBase64CharArray(pieces.ToCharArray(), 0, pieces.Length); ;
                }
                else
                {
                    return null;
                }
            }
        }

        public string GetFirstTracker(bool trim)
        {
            try
            {
                JsonObject tracker = (JsonObject)this.Trackers[0];
                Uri announceUrl = new Uri((string)tracker[ProtocolConstants.ANNOUNCE]);
                if (!trim)
                {
                    return announceUrl.Host;
                }
                else
                {
                    return Regex.Replace(Regex.Replace(Regex.Replace(announceUrl.Host, @"^tracker\.", "", RegexOptions.IgnoreCase), @"^www\.", "", RegexOptions.IgnoreCase), @"^torrent\.", "", RegexOptions.IgnoreCase);
                }
            }
            catch
            {
                return "";
            }
        }

        public int MaxConnectedPeers
        {
            get
            {
                return Toolbox.ToInt(info[ProtocolConstants.FIELD_MAXCONNECTEDPEERS]);
            }
        }

        public JsonArray Trackers
        {
            get
            {
                return (JsonArray)info[ProtocolConstants.FIELD_TRACKERS];
            }
        }

        public JsonArray TrackerStats
        {
            get
            {
                return (JsonArray)info[ProtocolConstants.FIELD_TRACKERSTATS];
            }
        }

        public string Name
        {
            get
            {
                return (string)info[ProtocolConstants.FIELD_NAME];
            }
        }

        public string Hash
        {
            get
            {
                return (string)info[ProtocolConstants.FIELD_HASHSTRING];
            }
        }

        public string Status
        {
            get
            {
                switch (this.StatusCode)
                {
                    case ProtocolConstants.STATUS_WAITING_TO_CHECK:
                        return OtherStrings.WaitingToCheck;
                    case ProtocolConstants.STATUS_CHECKING:
                        return OtherStrings.Checking;
                    case ProtocolConstants.STATUS_DOWNLOADING:
                        return OtherStrings.Downloading;
                    case ProtocolConstants.STATUS_SEEDING:
                        return OtherStrings.Seeding;
                    case ProtocolConstants.STATUS_PAUSED:
                        return OtherStrings.Paused;
                    default:
                        return OtherStrings.Unknown;
                }
            }
        }

        public short StatusCode
        {
            get
            {
                return Toolbox.ToShort(info[ProtocolConstants.FIELD_STATUS]);
            }
        }

        public string SambaLocation
        {
            get
            {
                string downloadDir = this.DownloadDir;
                string name = this.Name;
                Dictionary<string, string> mappings = Program.Settings.Current.SambaShareMappings;
                foreach (string key in mappings.Keys)
                {
                    if (downloadDir.StartsWith(key))
                        return String.Format(@"{0}\{1}{2}", (string)mappings[key], downloadDir.Length > key.Length ? downloadDir.Substring(key.Length + 1).Replace(@"/", @"\") + @"\" : null, this.Name);
                }
                return null;
            }
        }

        public int Id
        {
            get
            {
                return Toolbox.ToInt(info[ProtocolConstants.FIELD_ID]);
            }
        }

        public bool HasError
        {
            get
            {
                return IsErrorString(this.ErrorString);
            }
        }

        private static bool IsErrorString(string s)
        {
            return s != null && !s.Equals("");
        }

        public string ErrorString
        {
            get
            {
                return (string)info[ProtocolConstants.FIELD_ERRORSTRING];
            }
        }

        public string Creator
        {
            get
            {
                return (string)info[ProtocolConstants.FIELD_CREATOR];
            }
        }

        public string DownloadDir
        {
            get
            {
                return (string)info[ProtocolConstants.FIELD_DOWNLOADDIR];
            }
        }

        public string GetShortETA()
        {
            return GetETA(true);
        }

        public string GetLongETA()
        {
            return GetETA(false);
        }

        private string GetETA(bool small)
        {
            if (this.IsFinished)
            {
                return "";
            }
            else
            {
                double eta = Toolbox.ToDouble(info[ProtocolConstants.FIELD_ETA]);
                if (eta > 0)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(eta);
                    if (small)
                    {
                        return ts.ToString();
                    }
                    else
                    {
                        return Toolbox.FormatTimespanLong(ts);
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        public decimal RecheckPercentage
        {
            get
            {
                return Toolbox.ToProgress(info[ProtocolConstants.FIELD_RECHECKPROGRESS]);
            }
        }

        public decimal Percentage
        {
            get
            {
                return Toolbox.CalcPercentage(this.HaveTotal, this.SizeWhenDone);
            }
        }

        public int Seeders
        {
            get
            {
                if (info.Contains(ProtocolConstants.FIELD_TRACKERSTATS))
                {
                    int seedersTotal = 0;
                    foreach (JsonObject tracker in this.TrackerStats)
                        seedersTotal += Toolbox.ToInt(tracker[ProtocolConstants.TRACKERSTAT_SEEDERCOUNT]);
                    return seedersTotal;
                }
                else if (info.Contains(ProtocolConstants.FIELD_SEEDERS))
                {
                    return Toolbox.ToInt(info[ProtocolConstants.FIELD_SEEDERS]);
                }
                else
                {
                    return -1;
                }
            }
        }

        public long SizeWhenDone
        {
            get
            {
                return Toolbox.ToLong(info[ProtocolConstants.FIELD_SIZEWHENDONE]);
            }
        }

        public int Leechers
        {
            get
            {
                if (info.Contains(ProtocolConstants.FIELD_TRACKERSTATS))
                {
                    int leechersTotal = 0;
                    foreach (JsonObject tracker in this.TrackerStats)
                        leechersTotal += Toolbox.ToInt(tracker[ProtocolConstants.TRACKERSTAT_LEECHERCOUNT]);
                    return leechersTotal;
                }
                else if (info.Contains(ProtocolConstants.FIELD_LEECHERS))
                {
                    return Toolbox.ToInt(info[ProtocolConstants.FIELD_LEECHERS]);
                }
                else
                {
                    return -1;
                }
            }
        }

        public string SwarmSpeed
        {
            get
            {
                return Toolbox.GetSpeed(info[ProtocolConstants.FIELD_SWARMSPEED]);
            }
        }

        public long TotalSize
        {
            get
            {
                return Toolbox.ToLong(info[ProtocolConstants.FIELD_TOTALSIZE]);
            }
        }

        public DateTime Added
        {
            get
            {
                return Toolbox.DateFromEpoch(Toolbox.ToDouble(info[ProtocolConstants.FIELD_ADDEDDATE]));
            }
        }

        public int BandwidthPriority
        {
            get
            {
                return Toolbox.ToInt(info[ProtocolConstants.FIELD_BANDWIDTHPRIORITY]);
            }
        }

        public int PeersSendingToUs
        {
            get
            {
                return Toolbox.ToInt(info[ProtocolConstants.FIELD_PEERSSENDINGTOUS]);
            }
        }

        public int PeersGettingFromUs
        {
            get
            {
                return Toolbox.ToInt(info[ProtocolConstants.FIELD_PEERSGETTINGFROMUS]);
            }
        }

        public string Created
        {
            get
            {
                return Toolbox.DateFromEpoch(Toolbox.ToDouble(info[ProtocolConstants.FIELD_DATECREATED])).ToString();
            }
        }

        public long Uploaded
        {
            get
            {
                return Toolbox.ToLong(info[ProtocolConstants.FIELD_UPLOADEDEVER]);
            }
        }

        public string UploadedString
        {
            get
            {
                return Toolbox.GetFileSize(this.Uploaded);
            }
        }

        public long HaveTotal
        {
            get
            {
                return Toolbox.ToLong(info[ProtocolConstants.FIELD_HAVEVALID]) + Toolbox.ToLong(info[ProtocolConstants.FIELD_HAVEUNCHECKED]);
            }
        }

        public long HaveValid
        {
            get
            {
                return Toolbox.ToLong(info[ProtocolConstants.FIELD_HAVEVALID]);
            }
        }

        public bool IsFinished
        {
            get
            {
                return this.LeftUntilDone <= 0;
            }
        }

        public long LeftUntilDone
        {
            get
            {
                return Toolbox.ToLong(info[ProtocolConstants.FIELD_LEFTUNTILDONE]);
            }
        }

        public string HaveTotalString
        {
            get
            {
                return Toolbox.GetFileSize(this.HaveTotal);
            }
        }

        public string DownloadRate
        {
            get
            {
                return Toolbox.GetSpeed(info[ProtocolConstants.FIELD_RATEDOWNLOAD]);
            }
        }

        public string UploadRate
        {
            get
            {
                return Toolbox.GetSpeed(info[ProtocolConstants.FIELD_RATEUPLOAD]);
            }
        }

        public decimal LocalRatio
        {
            get
            {
                return Toolbox.CalcRatio(this.Uploaded, this.HaveTotal);
            }
        }

        public string LocalRatioString
        {
            get
            {
                decimal ratio = this.LocalRatio;
                return ratio < 0 ? "∞" : ratio.ToString();
            }
        }

        public string Comment
        {
            get
            {
                return (string)info[ProtocolConstants.FIELD_COMMENT];
            }
        }

        /* BEGIN CONFUSION */

        public int SpeedLimitDown
        {
            get
            {
                if (info.Contains(ProtocolConstants.FIELD_DOWNLOADLIMIT))
                {
                    return Toolbox.ToInt(info[ProtocolConstants.FIELD_DOWNLOADLIMIT]);
                }
                else
                {
                    return Toolbox.ToInt(info[ProtocolConstants.FIELD_SPEEDLIMITDOWN]);
                }
            }
        }

        public bool SpeedLimitDownEnabled
        {
            get
            {
                if (info.Contains(ProtocolConstants.FIELD_SPEEDLIMITDOWNENABLED))
                {
                    return Toolbox.ToBool(info[ProtocolConstants.FIELD_SPEEDLIMITDOWNENABLED]);
                }
                else if (info.Contains(ProtocolConstants.FIELD_DOWNLOADLIMITED))
                {
                    return Toolbox.ToBool(info[ProtocolConstants.FIELD_DOWNLOADLIMITED]);
                }
                else
                {
                    return Toolbox.ToBool(info[ProtocolConstants.FIELD_DOWNLOADLIMITMODE]);
                }
            }
        }

        public int SpeedLimitUp
        {
            get
            {
                if (info.Contains(ProtocolConstants.FIELD_UPLOADLIMIT))
                {
                    return Toolbox.ToInt(info[ProtocolConstants.FIELD_UPLOADLIMIT]);
                }
                else
                {
                    return Toolbox.ToInt(info[ProtocolConstants.FIELD_SPEEDLIMITUP]);
                }
            }
        }

        public bool SpeedLimitUpEnabled
        {
            get
            {
                if (info.Contains(ProtocolConstants.FIELD_SPEEDLIMITUPENABLED))
                {
                    return Toolbox.ToBool(info[ProtocolConstants.FIELD_SPEEDLIMITUPENABLED]);
                }
                else if (info.Contains(ProtocolConstants.FIELD_UPLOADLIMITED))
                {
                    return Toolbox.ToBool(info[ProtocolConstants.FIELD_UPLOADLIMITED]);
                }
                else
                {
                    return Toolbox.ToBool(info[ProtocolConstants.FIELD_UPLOADLIMITMODE]);
                }
            }
        }
        /* END CONFUSION */

        // DateTime isn't nullable
        public object DoneDate
        {
            get
            {
                if (info.Contains(ProtocolConstants.FIELD_ADDEDDATE))
                {
                    DateTime dateTime = Toolbox.DateFromEpoch(Toolbox.ToDouble(info[ProtocolConstants.FIELD_DONEDATE]));
                    if (!dateTime.Year.Equals(1970))
                        return dateTime;
                }
                return null;
            }
        }
    }
}
