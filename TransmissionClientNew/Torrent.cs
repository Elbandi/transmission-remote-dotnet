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
            else if (this.StatusCode == ProtocolConstants.STATUS_CHECK || this.StatusCode == ProtocolConstants.STATUS_CHECK_WAIT)
            {
                base.ImageIndex = 5;
            }
            else if (this.StatusCode == ProtocolConstants.STATUS_SEED)
            {
                base.ImageIndex = 4;
            }
            else if (this.StatusCode == ProtocolConstants.STATUS_DOWNLOAD)
            {
                base.ImageIndex = 1;
            }
            else if (this.StatusCode == ProtocolConstants.STATUS_STOPPED)
            {
                base.ImageIndex = 2;
            }
            else
            {
                base.ImageIndex = -1;
            }
        }

        private void SetText(int idx, string str)
        {
            if (!str.Equals(base.SubItems[idx].Text))
                base.SubItems[idx].Text = str;
        }

        public Color GetRatioColor()
        {
            double seedratio;
            if (this.SeedRatioMode == ProtocolConstants.TR_RATIOLIMIT_UNLIMITED)
                seedratio = -1;
            else
            {
                if (this.SeedRatioMode == ProtocolConstants.TR_RATIOLIMIT_SINGLE)
                {
                    seedratio = this.SeedRatioLimit;
                }
                else
                {
                    JsonObject session = (JsonObject)Program.DaemonDescriptor.SessionData;
                    seedratio = Toolbox.ToBool(session[ProtocolConstants.FIELD_SEEDRATIOLIMITED]) ? Toolbox.ToDouble(session[ProtocolConstants.FIELD_SEEDRATIOLIMIT]) : -1;
                }
            }
            if (this.LocalRatio < 0 || seedratio < 0)
                return SystemColors.WindowText;
            if (this.LocalRatio > seedratio)
                return Color.Green;
            if (this.LocalRatio > seedratio * 0.9)
                return Color.Gold;
            else
                return Color.Red;
        }

        private delegate void UpdateUIDelegate(bool first);
        public void UpdateUi(bool first)
        {
            MainWindow form = Program.Form;
            if (form.InvokeRequired)
            {
                form.Invoke(new UpdateUIDelegate(UpdateUi), new object[] { first });
                return;
            }
            SetText(1, this.Id.ToString());
            base.SubItems[1].Tag = this.Id;
            SetText(2, Toolbox.GetFileSize(this.SizeWhenDone));
            base.SubItems[2].Tag = this.SizeWhenDone;
            SetText(3, this.Percentage + "%");
            base.SubItems[3].Tag = this.Percentage;
            SetText(4, this.Status);
            SetText(5, string.Format(SeedersColumnFormat, (this.Seeders < 0 ? "?" : this.Seeders.ToString()), this.PeersSendingToUs));
            this.SubItems[5].Tag = this.Seeders;
            SetText(6, string.Format(SeedersColumnFormat, (this.Leechers < 0 ? "?" : this.Leechers.ToString()), this.PeersGettingFromUs));
            this.SubItems[6].Tag = this.Leechers;
            SetText(7, this.DownloadRate > 0 ? Toolbox.GetSpeed(this.DownloadRate) : "");
            base.SubItems[7].Tag = this.DownloadRate;
            SetText(8, this.UploadRate > 0 ? Toolbox.GetSpeed(this.UploadRate) : "");
            base.SubItems[8].Tag = this.UploadRate;
            SetText(9, this.Eta > 0 ? TimeSpan.FromSeconds(this.Eta).ToString() : "");
            base.SubItems[9].Tag = this.Eta;
            SetText(10, Toolbox.GetFileSize(this.Uploaded));
            base.SubItems[10].Tag = this.Uploaded;
            SetText(11, this.LocalRatio < 0 ? "∞" : this.LocalRatio.ToString());
            base.SubItems[11].Tag = this.LocalRatio;
            this.SubItems[11].ForeColor = GetRatioColor();
            SetText(12, this.Added.ToString());
            base.SubItems[12].Tag = this.Added;
            if (this.DoneDate != null)
            {
                base.SubItems[13].Tag = this.DoneDate;
                SetText(13, this.DoneDate.ToString());
            }
            SetText(14, this.FirstTrackerTrimmed);

            if (first)
            {
                lock (form.stateListBox)
                {
                    if (this.FirstTrackerTrimmed.Length > 0 && form.stateListBox.FindItem(this.FirstTrackerTrimmed) == null)
                    {
                        form.stateListBox.Items.Add(new GListBoxItem(this.FirstTrackerTrimmed, 8));
                    }
                }
                if (Program.Settings.MinToTray && Program.Settings.StartedBalloon && this.updateSerial > 2)
                {
                    form.ShowTrayTip(LocalSettingsSingleton.BALLOON_TIMEOUT, this.TorrentName, String.Format(OtherStrings.NewTorrentIs, this.Status.ToLower()), ToolTipIcon.Info);
                }
                LogError();
            }
            else if (Program.Settings.MinToTray && this.CompletionPopupPending)
            {
                this.CompletionPopupPending = false;
                form.ShowTrayTip(LocalSettingsSingleton.BALLOON_TIMEOUT, this.TorrentName, OtherStrings.TorrentFinished, ToolTipIcon.Info);
            }
            base.ForeColor = this.HasError ? Color.Red : SystemColors.WindowText;
            UpdateIcon();
        }

        public bool Update(JsonObject info, bool first)
        {
            this.HaveValid = Toolbox.ToLong(info[ProtocolConstants.FIELD_HAVEVALID]);
            this.HaveTotal = this.HaveValid + Toolbox.ToLong(info[ProtocolConstants.FIELD_HAVEUNCHECKED]);
            this.SizeWhenDone = Toolbox.ToLong(info[ProtocolConstants.FIELD_SIZEWHENDONE]);

            if (info.Contains(ProtocolConstants.FIELD_TRACKERSTATS))
                this.TrackerStats = (JsonArray)info[ProtocolConstants.FIELD_TRACKERSTATS];

            this.Eta = Toolbox.ToDouble(info[ProtocolConstants.FIELD_ETA]);

            this.DownloadDir = (string)info[ProtocolConstants.FIELD_DOWNLOADDIR];
            this.Trackers = (JsonArray)info[ProtocolConstants.FIELD_TRACKERS];
            this.Seeders = GetSeeders(info);
            this.Leechers = GetLeechers(info);
            this.PeersSendingToUs = Toolbox.ToInt(info[ProtocolConstants.FIELD_PEERSSENDINGTOUS]);
            this.PeersGettingFromUs = Toolbox.ToInt(info[ProtocolConstants.FIELD_PEERSGETTINGFROMUS]);

            if (Program.DaemonDescriptor.Trunk && Program.DaemonDescriptor.Revision >= 10937 && Program.DaemonDescriptor.Revision < 11194)
            {
                this.DownloadRate = (long)(Toolbox.ToDouble(info[ProtocolConstants.FIELD_RATEDOWNLOAD]) * 1024);
                this.UploadRate = (long)(Toolbox.ToDouble(info[ProtocolConstants.FIELD_RATEUPLOAD]) * 1024);
            }
            else
            {
                this.DownloadRate = Toolbox.ToLong(info[ProtocolConstants.FIELD_RATEDOWNLOAD]);
                this.UploadRate = Toolbox.ToLong(info[ProtocolConstants.FIELD_RATEUPLOAD]);
            }
            this.BandwidthPriority = Toolbox.ToInt(info[ProtocolConstants.FIELD_BANDWIDTHPRIORITY]);
            this.Downloaded = Toolbox.ToLong(info[ProtocolConstants.FIELD_DOWNLOADEDEVER]);
            this.Uploaded = Toolbox.ToLong(info[ProtocolConstants.FIELD_UPLOADEDEVER]);
            long downloadedForRatio = this.Downloaded > 0 ? this.Downloaded : this.HaveValid;
            this.LocalRatio = Toolbox.CalcRatio(this.Uploaded, downloadedForRatio);

            if (info.Contains(ProtocolConstants.FIELD_SECONDSDOWNLOADING))
                this.SecondsDownloading = Toolbox.ToInt(info[ProtocolConstants.FIELD_SECONDSDOWNLOADING]);
            else
                this.SecondsDownloading = -1;
            if (info.Contains(ProtocolConstants.FIELD_SECONDSSEEDING))
                this.SecondsSeeding = Toolbox.ToInt(info[ProtocolConstants.FIELD_SECONDSSEEDING]);
            else
                this.SecondsSeeding = -1;

            if (info.Contains(ProtocolConstants.FIELD_DONEDATE))
            {
                DateTime dateTime = Toolbox.DateFromEpoch(Toolbox.ToDouble(info[ProtocolConstants.FIELD_DONEDATE]));
                if (!dateTime.Year.Equals(1970))
                    this.DoneDate = dateTime.ToLocalTime();
            }

            this.PieceCount = Toolbox.ToInt(info[ProtocolConstants.FIELD_PIECECOUNT]);

            long leftUntilDone = Toolbox.ToLong(info[ProtocolConstants.FIELD_LEFTUNTILDONE]);
            short statusCode = Toolbox.ToShort(info[ProtocolConstants.FIELD_STATUS]);
            string errorString = (string)info[ProtocolConstants.FIELD_ERRORSTRING];

            bool statusChange = (this.StatusCode != statusCode) || (this.HasError != IsErrorString(errorString));

            if (this.StatusCode == ProtocolConstants.STATUS_DOWNLOAD
                && this.LeftUntilDone > 0 && (leftUntilDone == 0))
            {
                this.CompletionPopupPending = !first && Program.Settings.CompletedBaloon;
            }

            this.LeftUntilDone = leftUntilDone;
            this.StatusCode = statusCode;
            this.ErrorString = errorString;

            if (this.StatusCode == ProtocolConstants.STATUS_CHECK)
                this.Percentage = Toolbox.ToProgress(info[ProtocolConstants.FIELD_RECHECKPROGRESS]);
            else
                this.Percentage = Toolbox.CalcPercentage(this.HaveTotal, this.SizeWhenDone);

            this.updateSerial = Program.DaemonDescriptor.UpdateSerial;
            this.Peers = (JsonArray)info[ProtocolConstants.FIELD_PEERS];
            this.PieceSize = Toolbox.ToInt(info[ProtocolConstants.FIELD_PIECESIZE]);

            if (info.Contains(ProtocolConstants.FIELD_PIECES))
            {
                string pieces = (string)info[ProtocolConstants.FIELD_PIECES];
                this.Pieces = pieces.Length > 0 ? Convert.FromBase64CharArray(pieces.ToCharArray(), 0, pieces.Length) : new byte[0];
            }

            this.SeedRatioLimit = Toolbox.ToDouble(info[ProtocolConstants.FIELD_SEEDRATIOLIMIT]);
            this.SeedRatioMode = Toolbox.ToInt(info[ProtocolConstants.FIELD_SEEDRATIOMODE]);
            this.SeedIdleLimit = Toolbox.ToDouble(info[ProtocolConstants.FIELD_SEEDIDLELIMIT]);
            this.SeedIdleMode = Toolbox.ToInt(info[ProtocolConstants.FIELD_SEEDIDLEMODE]);

            this.HonorsSessionLimits = Toolbox.ToBool(info[ProtocolConstants.FIELD_HONORSSESSIONLIMITS]);
            this.MaxConnectedPeers = Toolbox.ToInt(info[ProtocolConstants.FIELD_MAXCONNECTEDPEERS]);
            this.SwarmSpeed = Toolbox.GetSpeed(info[ProtocolConstants.FIELD_SWARMSPEED]);
            SetSpeedLimits(info);

            return statusChange;
        }

        public bool CompletionPopupPending
        {
            get;
            set;
        }

        public string TorrentName
        {
            get
            {
                return base.Text;
            }
        }

        public Torrent(JsonObject info)
            : base((string)info[ProtocolConstants.FIELD_NAME])
        {
            this.Id = Toolbox.ToInt(info[ProtocolConstants.FIELD_ID]);
            for (int i = 0; i < 14; i++)
                base.SubItems.Add("");
            this.UseItemStyleForSubItems = false;
            SeedersColumnFormat = "{0} ({1})";
            base.ToolTipText = base.Text;
            this.Created = Toolbox.DateFromEpoch(Toolbox.ToDouble(info[ProtocolConstants.FIELD_DATECREATED])).ToLocalTime().ToString();
            this.Creator = (string)info[ProtocolConstants.FIELD_CREATOR];
            this.Added = Toolbox.DateFromEpoch(Toolbox.ToDouble(info[ProtocolConstants.FIELD_ADDEDDATE])).ToLocalTime();
            base.Name = this.Hash = (string)info[ProtocolConstants.FIELD_HASHSTRING];
            this.TotalSize = Toolbox.ToLong(info[ProtocolConstants.FIELD_TOTALSIZE]);
            this.Comment = (string)info[ProtocolConstants.FIELD_COMMENT];
            this.Update(info, true);
            this.UpdateUi(true);
            MainWindow form = Program.Form;
            lock (Program.TorrentIndex)
                Program.TorrentIndex.Add(this.Hash, this);
        }

        private void LogError()
        {
            if (this.HasError)
            {
                List<LogListViewItem> logItems = Program.LogItems;
                lock (logItems)
                {
                    if (logItems.Count > 0)
                    {
                        foreach (LogListViewItem item in logItems)
                        {
                            if (item.UpdateSerial >= 0 && this.updateSerial - item.UpdateSerial < 2 && item.SubItems[1].Text.Equals(this.TorrentName) && item.SubItems[2].Text.Equals(this.ErrorString))
                            {
                                item.UpdateSerial = this.updateSerial;
                                return;
                            }
                        }
                    }
                }
                Program.Log(this.TorrentName, this.ErrorString, this.updateSerial);
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
                        this.UpdateUi(false);
                        itemCollection.Add(this);
                    }
                }
            }
        }

        public void RemoveItem()
        {
            MainWindow form = Program.Form;
            int matchingTrackers = 0;
            ListView.ListViewItemCollection itemCollection = Program.Form.torrentListView.Items;
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

            if (this.FirstTrackerTrimmed == null)
                return;

            lock (Program.TorrentIndex)
            {
                foreach (KeyValuePair<string, Torrent> torrent in Program.TorrentIndex)
                {
                    if (torrent.Value.FirstTrackerTrimmed.Equals(this.FirstTrackerTrimmed))
                        matchingTrackers++;
                }
            }

            if (matchingTrackers <= 0)
            {
                lock (form.stateListBox)
                {
                    form.stateListBox.RemoveItem(this.FirstTrackerTrimmed);
                }
            }
        }

        public string FirstTracker
        {
            get;
            set;
        }

        public string FirstTrackerTrimmed
        {
            get;
            set;
        }

        FileItemCollection files = new FileItemCollection();
        public FileItemCollection Files
        {
            get { return files; }
        }

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
                return this.Pieces != null ? Toolbox.BitCount(this.Pieces) : -1;
            }
        }

        public double SeedRatioLimit
        {
            get;
            set;
        }

        public int SeedRatioMode
        {
            get;
            set;
        }

        public double SeedIdleLimit
        {
            get;
            set;
        }

        public int SeedIdleMode
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
                    {
                        this.FirstTracker = this.FirstTrackerTrimmed = "";
                    }
                    else
                    {
                        JsonObject tracker = (JsonObject)value[0];
                        Uri announceUrl = new Uri((string)tracker[ProtocolConstants.ANNOUNCE]);
                        this.FirstTracker = announceUrl.Host;
                        this.FirstTrackerTrimmed = Toolbox.GetDomainName(announceUrl.Host);
                    }
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
                    case ProtocolConstants.STATUS_CHECK_WAIT:
                        return OtherStrings.WaitingToCheck;
                    case ProtocolConstants.STATUS_CHECK:
                        return OtherStrings.Checking;
                    case ProtocolConstants.STATUS_DOWNLOAD:
                        return OtherStrings.Downloading;
                    case ProtocolConstants.STATUS_SEED:
                        return OtherStrings.Seeding;
                    case ProtocolConstants.STATUS_STOPPED:
                        return OtherStrings.Paused;
                    default:
                        return OtherStrings.Unknown;
                }
            }
        }

        public short StatusCode
        {
            get;
            set;
        }

        public string SambaLocation
        {
            get
            {
                string downloadDir = this.DownloadDir;
                string name = this.Files.Count > 1 ? this.TorrentName : "";
                Dictionary<string, string> mappings = Program.Settings.Current.SambaShareMappings;
                foreach (string key in mappings.Keys)
                {
                    if (downloadDir.StartsWith(key))
                        return String.Format(@"{0}\{1}{2}", (string)mappings[key], downloadDir.Length > key.Length ? downloadDir.Substring(key.Length + 1).Replace(@"/", @"\") + @"\" : null, name);
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

        public double Eta
        {
            get;
            set;
        }

        public string LongEta
        {
            get
            {
                return Toolbox.FormatTimespanLong(TimeSpan.FromSeconds(this.Eta));
            }
        }

        public long SecondsDownloading
        {
            get;
            set;
        }

        public long SecondsSeeding
        {
            get;
            set;
        }

        public decimal Percentage
        {
            get;
            set;
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
            get;
            set;
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
            get;
            set;
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
                return base.SubItems[10].Text;
            }
        }

        public long Uploaded
        {
            get;
            set;
        }

        public long Downloaded
        {
            get;
            set;
        }

        public long HaveTotal
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

        public long DownloadRate
        {
            get;
            set;
        }

        public string DownloadRateString
        {
            get
            {
                return this.DownloadRate > 0 ? base.SubItems[7].Text : Toolbox.GetSpeed(0);
            }
        }

        public string DownloadAvgRateString
        {
            get
            {
                if (this.SecondsDownloading >= 0)
                {
                    long speed = (long)Math.Round((double)this.Downloaded / this.SecondsDownloading, 0);
                    return Toolbox.GetSpeed(speed);
                }
                else return "";
            }
        }

        public long UploadRate
        {
            get;
            set;
        }

        public string UploadRateString
        {
            get
            {
                return this.UploadRate > 0 ? base.SubItems[8].Text : Toolbox.GetSpeed(0);
            }
        }

        public string UploadAvgRateString
        {
            get
            {
                if (this.SecondsDownloading >= 0 && this.SecondsSeeding >= 0)
                {
                    long speed = (long)Math.Round((double)this.Downloaded / (this.SecondsDownloading + this.SecondsSeeding), 0);
                    return Toolbox.GetSpeed(speed);
                }
                else return "";
            }
        }

        public double LocalRatio
        {
            get;
            set;
        }

        public string LocalRatioString
        {
            get
            {
                return base.SubItems[11].Text;
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
            {
                if (Program.DaemonDescriptor.RpcVersion > 9 && Program.DaemonDescriptor.Revision >= 10937)
                    this.SpeedLimitDown = (int)Toolbox.ToDouble(info[ProtocolConstants.FIELD_DOWNLOADLIMIT]);
                else
                    this.SpeedLimitDown = Toolbox.ToInt(info[ProtocolConstants.FIELD_DOWNLOADLIMIT]);
            }
            else
                this.SpeedLimitDown = Toolbox.ToInt(info[ProtocolConstants.FIELD_SPEEDLIMITDOWN]);

            if (info.Contains(ProtocolConstants.FIELD_SPEEDLIMITDOWNENABLED))
                this.SpeedLimitDownEnabled = Toolbox.ToBool(info[ProtocolConstants.FIELD_SPEEDLIMITDOWNENABLED]);
            else if (info.Contains(ProtocolConstants.FIELD_DOWNLOADLIMITED))
                this.SpeedLimitDownEnabled = Toolbox.ToBool(info[ProtocolConstants.FIELD_DOWNLOADLIMITED]);
            else
                this.SpeedLimitDownEnabled = Toolbox.ToBool(info[ProtocolConstants.FIELD_DOWNLOADLIMITMODE]);

            if (info.Contains(ProtocolConstants.FIELD_UPLOADLIMIT))
            {
                if (Program.DaemonDescriptor.RpcVersion > 9 && Program.DaemonDescriptor.Revision >= 10937)
                    this.SpeedLimitUp = (int)Toolbox.ToDouble(info[ProtocolConstants.FIELD_UPLOADLIMIT]);
                else
                    this.SpeedLimitUp = Toolbox.ToInt(info[ProtocolConstants.FIELD_UPLOADLIMIT]);
            }
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
            get;
            set;
        }
    }
    public class FileItemCollection : List<FileListViewItem>
    {
        public FileListViewItem Find(string Key)
        {
            return Find(delegate(FileListViewItem fi) { return fi.FileName.Equals(Key); });
        }
        public List<FileListViewItem> FindAll(string filter)
        {
            return FindAll(delegate(FileListViewItem fi) { return fi.FileName.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0; });
        }
    }
}
