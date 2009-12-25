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
    public class Torrent : ListViewItem
    {
        private string SeedersColumnFormat = "{1}";
        /*private JsonObject info;

        public JsonObject Info
        {
            get { return info; }
        }*/

        private long updateSerial;

        public long UpdateSerial
        {
            get { return updateSerial; }
        }

        private void UpdateIcon()
        {
            if (this.HasError)
            {
                base.ImageIndex = 6;
            }
            else if (this.StatusCode == ProtocolConstants.STATUS_CHECKING || this.StatusCode == ProtocolConstants.STATUS_WAITING_TO_CHECK)
            {
                base.ImageIndex = 5;
            }
            else if (this.StatusCode == ProtocolConstants.STATUS_SEEDING)
            {
                base.ImageIndex = 4;
            }
            else if (this.StatusCode == ProtocolConstants.STATUS_DOWNLOADING)
            {
                base.ImageIndex = 1;
            }
            else if (this.StatusCode == ProtocolConstants.STATUS_PAUSED)
            {
                base.ImageIndex = 2;
            }
            else
            {
                base.ImageIndex = -1;
            }
        }

        public bool Update(JsonObject info)
        {
            this.HaveValid = Toolbox.ToLong(info[ProtocolConstants.FIELD_HAVEVALID]);
            this.HaveTotal = this.HaveValid + Toolbox.ToLong(info[ProtocolConstants.FIELD_HAVEUNCHECKED]);
            this.StatusCode = Toolbox.ToShort(info[ProtocolConstants.FIELD_STATUS]);

            if (this.StatusCode == ProtocolConstants.STATUS_CHECKING)
                this.Percentage = Toolbox.ToProgress(info[ProtocolConstants.FIELD_RECHECKPROGRESS]);
            else
                this.Percentage = Toolbox.ToProgress(info[ProtocolConstants.FIELD_PROGRESS]);

            if (info.Contains(ProtocolConstants.FIELD_TRACKERSTATS))
                this.TrackerStats = (JsonArray)info[ProtocolConstants.FIELD_TRACKERSTATS];

            this.Seeders = GetSeeders(info);
            this.Leechers = GetLeechers(info);
            this.PeersSendingToUs = Toolbox.ToInt(info[ProtocolConstants.FIELD_PEERSSENDINGTOUS]);
            this.PeersGettingFromUs = Toolbox.ToInt(info[ProtocolConstants.FIELD_PEERSGETTINGFROMUS]);
            SetSeedersAndLeechersColumns();

            this.DownloadRate = Toolbox.ToLong(info[ProtocolConstants.FIELD_RATEDOWNLOAD]);
            this.UploadRate = Toolbox.ToLong(info[ProtocolConstants.FIELD_RATEUPLOAD]);

            this.Uploaded = Toolbox.ToLong(info[ProtocolConstants.FIELD_UPLOADEDEVER]);

            CalcETA(info);

            this.LocalRatio = Toolbox.CalcRatio(this.Uploaded, this.HaveTotal);

            this.LeftUntilDone = Toolbox.ToLong(info[ProtocolConstants.FIELD_LEFTUNTILDONE]);

            if (info.Contains(ProtocolConstants.FIELD_ADDEDDATE))
            {
                DateTime dateTime = Toolbox.DateFromEpoch(Toolbox.ToDouble(info[ProtocolConstants.FIELD_DONEDATE]));
                if (!dateTime.Year.Equals(1970))
                    this.DoneDate = dateTime;
            }

            this.PieceCount = Toolbox.ToInt(info[ProtocolConstants.FIELD_PIECECOUNT]);
            this.ErrorString = (string)info[ProtocolConstants.FIELD_ERRORSTRING];

            MainWindow form = Program.Form;
            if (Program.Settings.CompletedBaloon
                && form.notifyIcon.Visible == true
                && this.StatusCode == ProtocolConstants.STATUS_DOWNLOADING
                && this.LeftUntilDone > 0
                && (Toolbox.ToLong(info[ProtocolConstants.FIELD_LEFTUNTILDONE]) == 0))
            {
                form.notifyIcon.ShowBalloonTip(LocalSettingsSingleton.BALLOON_TIMEOUT, this.Name, "This torrent has finished downloading.", ToolTipIcon.Info);
                this.DoneDate = DateTime.Now;
            }
            UpdateIcon();
            this.updateSerial = Program.DaemonDescriptor.UpdateSerial;
            this.Peers = (JsonArray)info[ProtocolConstants.FIELD_PEERS];
            this.PieceSize = Toolbox.ToInt(info[ProtocolConstants.FIELD_PIECESIZE]);

            if (info.Contains(ProtocolConstants.FIELD_PIECES))
            {
                string pieces = (string)info[ProtocolConstants.FIELD_PIECES];
                this.Pieces = pieces.Length > 0 ? Convert.FromBase64CharArray(pieces.ToCharArray(), 0, pieces.Length) : new byte[0];
            }

            this.SeedRatioLimit = Toolbox.ToDouble(info[ProtocolConstants.FIELD_SEEDRATIOLIMIT]);
            this.SeedRatioMode = Toolbox.ToInt(info[ProtocolConstants.FIELD_SEEDRATIOMODE]) > 0;

            this.HonorsSessionLimits = Toolbox.ToBool(info[ProtocolConstants.FIELD_HONORSSESSIONLIMITS]);
            this.MaxConnectedPeers = Toolbox.ToInt(info[ProtocolConstants.FIELD_MAXCONNECTEDPEERS]);
            this.SwarmSpeed = Toolbox.GetSpeed(info[ProtocolConstants.FIELD_SWARMSPEED]);
            return (this.StatusCode != Toolbox.ToShort(info[ProtocolConstants.FIELD_STATUS])) || (this.HasError != IsErrorString((string)info[ProtocolConstants.FIELD_ERRORSTRING]));
        }

        private void SetSeedersAndLeechersColumns()
        {
            this.SubItems[4].Text = string.Format(SeedersColumnFormat, (this.Seeders < 0 ? "?" : this.Seeders.ToString()), this.PeersSendingToUs);
            this.SubItems[4].Tag = this.Seeders;
            this.SubItems[5].Text = string.Format(SeedersColumnFormat, (this.Leechers < 0 ? "?" : this.Leechers.ToString()), this.PeersGettingFromUs);
            this.SubItems[5].Tag = this.Leechers;
        }

        public Torrent(JsonObject info)
            : base((string)info[ProtocolConstants.FIELD_NAME])
        {
            this.Id = Toolbox.ToInt(info[ProtocolConstants.FIELD_ID]);
            for (int i = 0; i < 13; i++)
                base.SubItems.Add("");
            if (this.HasError)
            {
                base.ForeColor = Color.Red;
            }
            SeedersColumnFormat = "{0} ({1})";
            base.ToolTipText = base.Text;
            this.SizeWhenDone = Toolbox.ToLong(info[ProtocolConstants.FIELD_SIZEWHENDONE]);
            this.Created = Toolbox.DateFromEpoch(Toolbox.ToDouble(info[ProtocolConstants.FIELD_DATECREATED])).ToString();
            this.Creator = (string)info[ProtocolConstants.FIELD_CREATOR];
            this.Added = Toolbox.DateFromEpoch(Toolbox.ToDouble(info[ProtocolConstants.FIELD_ADDEDDATE]));
            this.Trackers = (JsonArray)info[ProtocolConstants.FIELD_TRACKERS];
            base.Name = this.Hash = (string)info[ProtocolConstants.FIELD_HASHSTRING];
            this.DownloadDir = (string)info[ProtocolConstants.FIELD_DOWNLOADDIR];
            this.TotalSize = Toolbox.ToLong(info[ProtocolConstants.FIELD_TOTALSIZE]);
            this.BandwidthPriority = Toolbox.ToInt(info[ProtocolConstants.FIELD_BANDWIDTHPRIORITY]);
            this.Comment = (string)info[ProtocolConstants.FIELD_COMMENT];
            this.Update(info);
            //Add();
        }

        /*private delegate void AddDelegate();
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
                    if (item.SubItems[13].Text.Length > 0 && form.stateListBox.FindItem(item.SubItems[13].Text) == null)
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
        }*/

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
            if (!itemCollection.Contains(this))
            {
                lock (Program.Form.torrentListView)
                {
                    if (!itemCollection.Contains(this))
                    {
                        itemCollection.Add(this);
                    }
                }
            }
        }

        public void RemoveItem()
        {
            MainWindow form = Program.Form;
            int matchingTrackers = 0;
            ListView.ListViewItemCollection itemCollection = form.torrentListView.Items;
            if (itemCollection.Contains(this))
            {
                lock (form.torrentListView)
                {
                    if (itemCollection.Contains(this))
                    {
                        itemCollection.Remove(this);
                    }
                }
            }
            else
            {
                return;
            }
            if (base.SubItems[13].Text.Length == 0)
                return;

            foreach (Torrent torrent in Program.Form.torrentListView.Items)
            {
                if (torrent.FirstTracker.Equals(this.FirstTracker))
                    matchingTrackers++;
            }

            if (matchingTrackers <= 0)
            {
                lock (form.stateListBox)
                {
                    form.stateListBox.RemoveItem(this.FirstTracker);
                }
            }

            Program.Form.torrentListView.Items.Remove(this);
        }

        public string FirstTracker
        {
            get;
            set;
        }

        public string FirstTrackerTrimmed
        {
            get
            {
                return base.SubItems[13].Text;
            }
            set
            {
                base.SubItems[13].Text = value;
            }
        }

        /*public delegate bool UpdateDelegate(JsonObject info);
        public bool UpdateItem(JsonObject info)
        {
            MainWindow form = Program.Form;
            if (form.InvokeRequired)
            {
                return (bool)form.Invoke(new UpdateDelegate(this.UpdateItem), info);
            }
            else
            {
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
                return stateChange;
            }
        }*/

        public JsonArray Peers
        {
            get;
            set;
        }

        public int PieceCount
        {
            get;
            set;
        }

        public int PieceSize
        {
            get;
            set;
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
            get;
            set;
        }

        public bool SeedRatioMode
        {
            get;
            set;
        }

        public bool HonorsSessionLimits
        {
            get;
            set;
        }

        public byte[] Pieces
        {
            get;
            set;
        }

        public int MaxConnectedPeers
        {
            get;
            set;
        }

        private JsonArray _trackers;
        public JsonArray Trackers
        {
            get
            {
                return this._trackers;
            }
            set
            {
                this._trackers = value;
                try
                {
                    if (value.Length == 0)
                        this.FirstTracker = this.FirstTrackerTrimmed = "";
                    JsonObject tracker = (JsonObject)value[0];
                    Uri announceUrl = new Uri((string)tracker[ProtocolConstants.ANNOUNCE]);
                    this.FirstTracker = announceUrl.Host;
                    this.FirstTrackerTrimmed = Regex.Replace(Regex.Replace(Regex.Replace(announceUrl.Host, @"^tracker\.", "", RegexOptions.IgnoreCase), @"^www\.", "", RegexOptions.IgnoreCase), @"^torrent\.", "", RegexOptions.IgnoreCase);
                }
                catch
                {
                    this.FirstTracker = this.FirstTrackerTrimmed = "";
                }
            }
        }

        public JsonArray TrackerStats
        {
            get;
            set;
        }

        public string Hash
        {
            get;
            set;
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

        private short _statusCode;
        public short StatusCode
        {
            set
            {
                this._statusCode = value;
                this.SubItems[3].Text = this.Status;
            }
            get
            {
                return this._statusCode;
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
            get;
            set;
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
            get;
            set;
        }

        public string Creator
        {
            get;
            set;
        }

        public string DownloadDir
        {
            get;
            set;
        }

        private string _longEta;

        private void CalcETA(JsonObject info)
        {
            double eta = Toolbox.ToDouble(info[ProtocolConstants.FIELD_ETA]);
            TimeSpan ts = TimeSpan.FromSeconds(eta);
            if (eta > 0)
                base.SubItems[8].Text = ts.ToString();
            this._longEta = Toolbox.FormatTimespanLong(ts);
        }

        public string LongEta
        {
            get
            {
                return this._longEta;
            }
        }

        /*public decimal RecheckPercentage
        {
            get
            {
                return (decimal)base.SubItems[2].Tag;
            }
            set
            {
                base.SubItems[2].Tag = value;
                base.SubItems[2].Text = value + "%";
            }
        }*/

        public decimal Percentage
        {
            get
            {
                return (decimal)base.SubItems[2].Tag;
            }
            set
            {
                base.SubItems[2].Tag = value;
                base.SubItems[2].Text = value + "%";
            }
        }

        private int GetSeeders(JsonObject info)
        {
            if (this.TrackerStats != null)
            {
                int seedersMax = 0;
                foreach (JsonObject tracker in this.TrackerStats)
                {
                    int trackerSeeders = Toolbox.ToInt(tracker[ProtocolConstants.TRACKERSTAT_SEEDERCOUNT]);
                    if (seedersMax < trackerSeeders)
                        seedersMax = trackerSeeders;
                }
                return seedersMax;
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

        public int Seeders
        {
            get;
            set;
        }

        public long SizeWhenDone
        {
            get
            {
                return (long)base.SubItems[1].Tag;
            }
            set
            {
                base.SubItems[1].Tag = value;
                base.SubItems[1].Text = Toolbox.GetFileSize(value);
            }
        }

        private int GetLeechers(JsonObject info)
        {
            if (this.TrackerStats != null)
            {
                int leechersMax = 0;
                foreach (JsonObject tracker in this.TrackerStats)
                {
                    int trackerLeechers = Toolbox.ToInt(tracker[ProtocolConstants.TRACKERSTAT_LEECHERCOUNT]);
                    if (leechersMax < trackerLeechers)
                        leechersMax = trackerLeechers;
                }
                return leechersMax;
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

        public int Leechers
        {
            get;
            set;
        }

        public string SwarmSpeed
        {
            get;
            set;
        }

        public long TotalSize
        {
            get;
            set;
        }

        public DateTime Added
        {
            get
            {
                return (DateTime)base.SubItems[11].Tag;
            }
            set
            {
                base.SubItems[11].Tag = value;
                base.SubItems[11].Text = value.ToString();
            }
        }

        public int BandwidthPriority
        {
            get;
            set;
        }

        public int PeersSendingToUs
        {
            get;
            set;
        }

        public int PeersGettingFromUs
        {
            get;
            set;
        }

        public string Created
        {
            get;
            set;
        }

        public string UploadedString
        {
            get
            {
                return base.SubItems[9].Text;
            }
        }

        public long Uploaded
        {
            get
            {
                return (long)base.SubItems[9].Tag;
            }
            set
            {
                base.SubItems[9].Tag = value;
                base.SubItems[9].Text = Toolbox.GetFileSize(value);
            }
        }

        private long _haveTotal;
        public long HaveTotal
        {
            get
            {
                return this._haveTotal;
            }
            set
            {
                this._haveTotal = value;
                this.Percentage = Toolbox.CalcPercentage(value, this.SizeWhenDone);
            }
        }

        public long HaveValid
        {
            get;
            set;
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
            get;
            set;
        }

        public string HaveTotalString
        {
            get
            {
                return Toolbox.GetFileSize(this.HaveTotal);
            }
        }

        public long DownloadRate
        {
            get
            {
                return (long)base.SubItems[6].Tag;
            }
            set
            {
                base.SubItems[6].Tag = value;
                base.SubItems[6].Text = this.StatusCode == ProtocolConstants.STATUS_DOWNLOADING ? Toolbox.GetSpeed(value) : "";
            }
        }

        public string DownloadRateString
        {
            get
            {
                return base.SubItems[6].Text;
            }
        }

        public long UploadRate
        {
            get
            {
                return (long)base.SubItems[7].Tag;
            }
            set
            {
                base.SubItems[7].Tag = value;
                base.SubItems[7].Text = this.StatusCode == ProtocolConstants.STATUS_SEEDING ? Toolbox.GetSpeed(value) : "";
            }
        }

        public string UploadRateString
        {
            get
            {
                return base.SubItems[7].Text;
            }
        }

        public decimal LocalRatio
        {
            get;
            set;
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
            get;
            set;
        }

        /* BEGIN CONFUSION */

        private void SetSpeedLimits(JsonObject info)
        {
            if (info.Contains(ProtocolConstants.FIELD_DOWNLOADLIMIT))
                this.SpeedLimitDown = Toolbox.ToInt(info[ProtocolConstants.FIELD_DOWNLOADLIMIT]);
            else
                this.SpeedLimitDown = Toolbox.ToInt(info[ProtocolConstants.FIELD_SPEEDLIMITDOWN]);

            if (info.Contains(ProtocolConstants.FIELD_SPEEDLIMITDOWNENABLED))
                this.SpeedLimitDownEnabled = Toolbox.ToBool(info[ProtocolConstants.FIELD_SPEEDLIMITDOWNENABLED]);
            else if (info.Contains(ProtocolConstants.FIELD_DOWNLOADLIMITED))
                this.SpeedLimitDownEnabled = Toolbox.ToBool(info[ProtocolConstants.FIELD_DOWNLOADLIMITED]);
            else
                this.SpeedLimitDownEnabled = Toolbox.ToBool(info[ProtocolConstants.FIELD_DOWNLOADLIMITMODE]);

            if (info.Contains(ProtocolConstants.FIELD_UPLOADLIMIT))
                this.SpeedLimitUp = Toolbox.ToInt(info[ProtocolConstants.FIELD_UPLOADLIMIT]);
            else
                this.SpeedLimitUp = Toolbox.ToInt(info[ProtocolConstants.FIELD_SPEEDLIMITUP]);

            if (info.Contains(ProtocolConstants.FIELD_SPEEDLIMITUPENABLED))
                this.SpeedLimitUpEnabled = Toolbox.ToBool(info[ProtocolConstants.FIELD_SPEEDLIMITUPENABLED]);
            else if (info.Contains(ProtocolConstants.FIELD_UPLOADLIMITED))
                this.SpeedLimitUpEnabled = Toolbox.ToBool(info[ProtocolConstants.FIELD_UPLOADLIMITED]);
            else
                this.SpeedLimitUpEnabled = Toolbox.ToBool(info[ProtocolConstants.FIELD_UPLOADLIMITMODE]);
        }

        public int SpeedLimitDown
        {
            get;
            set;
        }

        public bool SpeedLimitDownEnabled
        {
            get;
            set;
        }

        public int SpeedLimitUp
        {
            get;
            set;
        }

        public bool SpeedLimitUpEnabled
        {
            get;
            set;
        }

        /* END CONFUSION */

        // DateTime isn't nullable
        public object DoneDate
        {
            get
            {
                return base.SubItems[12].Tag;
            }
            set
            {
                base.SubItems[12].Tag = value;
                base.SubItems[12].Text = value.ToString();
            }
        }
    }
}
