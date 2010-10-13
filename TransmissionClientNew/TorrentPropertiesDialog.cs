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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jayrock.Json;

namespace TransmissionRemoteDotnet
{
    public partial class TorrentPropertiesDialog : CultureForm
    {
        private ListView.SelectedListViewItemCollection selections;

        public TorrentPropertiesDialog(ListView.SelectedListViewItemCollection selections)
        {
            this.selections = selections;
            InitializeComponent();
        }

        private string BuildTrackerList(JsonArray Trackers)
        {
            int oldtier = -1;
            string result = string.Empty;
            foreach (JsonObject tracker in Trackers)
            {
                int tier = Toolbox.ToInt(tracker[ProtocolConstants.TIER]);
                string announceUrl = (string)tracker[ProtocolConstants.ANNOUNCE];
                if (oldtier == -1)
                    oldtier = tier;
                if (oldtier != tier)
                    result += Environment.NewLine;
                result += announceUrl + Environment.NewLine;
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JsonObject request = Requests.CreateBasicObject(ProtocolConstants.METHOD_TORRENTSET);
            JsonObject arguments = Requests.GetArgObject(request);
            arguments.Put(ProtocolConstants.KEY_IDS, Toolbox.ListViewSelectionToIdArray(selections));
            arguments.Put(ProtocolConstants.FIELD_SPEEDLIMITUPENABLED, uploadLimitEnableField.Checked ? 1 : 0);
            arguments.Put(ProtocolConstants.FIELD_UPLOADLIMITED, uploadLimitEnableField.Checked ? 1 : 0);
            arguments.Put(ProtocolConstants.FIELD_UPLOADLIMIT, uploadLimitField.Value);
            arguments.Put(ProtocolConstants.FIELD_SPEEDLIMITUP, uploadLimitField.Value);
            arguments.Put(ProtocolConstants.FIELD_SPEEDLIMITDOWNENABLED, downloadLimitEnableField.Checked ? 1 : 0);
            arguments.Put(ProtocolConstants.FIELD_DOWNLOADLIMITED, downloadLimitEnableField.Checked ? 1 : 0);
            arguments.Put(ProtocolConstants.FIELD_DOWNLOADLIMIT, downloadLimitField.Value);
            arguments.Put(ProtocolConstants.FIELD_SPEEDLIMITDOWN, downloadLimitField.Value);
            arguments.Put(ProtocolConstants.FIELD_PEERLIMIT, peerLimitValue.Value);
            if (seedRatioLimitValue.Enabled)
                arguments.Put(ProtocolConstants.FIELD_SEEDRATIOLIMIT, seedRatioLimitValue.Value);
            if (honorsSessionLimits.Enabled)
                arguments.Put(ProtocolConstants.FIELD_HONORSSESSIONLIMITS, honorsSessionLimits.Checked);
            if (seedRatioLimitedCheckBox.Enabled)
                arguments.Put(ProtocolConstants.FIELD_SEEDRATIOMODE, (int)(2 - seedRatioLimitedCheckBox.CheckState));
            if (bandwidthComboBox.Enabled)
            {
                int bandwidthPriority = 0;
                if (bandwidthComboBox.SelectedIndex == 0)
                    bandwidthPriority = -1;
                else if (bandwidthComboBox.SelectedIndex == 2)
                    bandwidthPriority = 1;
                arguments.Put(ProtocolConstants.FIELD_BANDWIDTHPRIORITY, bandwidthPriority);
            }
            Program.Form.SetupAction(CommandFactory.RequestAsync(request));
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TorrentPropertiesDialog_Load(object sender, EventArgs e)
        {
            Torrent firstTorrent = (Torrent)selections[0];
            this.Text = selections.Count == 1 ? firstTorrent.TorrentName : OtherStrings.MultipleTorrentProperties;
            uploadLimitField.Value = firstTorrent.SpeedLimitUp >= 0 && firstTorrent.SpeedLimitUp <= uploadLimitField.Maximum ? firstTorrent.SpeedLimitUp : 0;
            downloadLimitField.Value = firstTorrent.SpeedLimitDown >= 0 && firstTorrent.SpeedLimitDown <= downloadLimitField.Maximum ? firstTorrent.SpeedLimitDown : 0;
            uploadLimitField.Enabled = uploadLimitEnableField.Checked = firstTorrent.SpeedLimitUpEnabled;
            downloadLimitField.Enabled = downloadLimitEnableField.Checked = firstTorrent.SpeedLimitDownEnabled;
            bandwidthComboBox.Items.Add(OtherStrings.Low);
            bandwidthComboBox.Items.Add(OtherStrings.Normal);
            bandwidthComboBox.Items.Add(OtherStrings.High);
            bandwidthComboBox.SelectedIndex = 1;
            try
            {
                honorsSessionLimits.Checked = firstTorrent.HonorsSessionLimits;
                honorsSessionLimits.Enabled = true;
            }
            catch
            {
                honorsSessionLimits.Enabled = false;
            }
            try
            {
                seedRatioLimitValue.Value = firstTorrent.SeedRatioLimit >= 0 && (decimal)firstTorrent.SeedRatioLimit <= seedRatioLimitValue.Maximum ? (decimal)firstTorrent.SeedRatioLimit : 0;
                seedRatioLimitedCheckBox.CheckState = (CheckState)(2 - firstTorrent.SeedRatioMode);
                seedRatioLimitValue.Enabled = seedRatioLimitedCheckBox.CheckState == CheckState.Checked;
                seedRatioLimitedCheckBox.Enabled = true;
            }
            catch
            {
                seedRatioLimitValue.Enabled = seedRatioLimitedCheckBox.Enabled = false;
            }
            try
            {
                if (firstTorrent.BandwidthPriority < 0)
                    bandwidthComboBox.SelectedIndex = 0;
                else if (firstTorrent.BandwidthPriority > 0)
                    bandwidthComboBox.SelectedIndex = 2;
                else
                    bandwidthComboBox.SelectedIndex = 1;
                label4.Enabled = bandwidthComboBox.Enabled = true;
            }
            catch
            {
                label4.Enabled = bandwidthComboBox.Enabled = false;
            }
            peerLimitValue.Value = firstTorrent.MaxConnectedPeers >= 0 && (decimal)firstTorrent.MaxConnectedPeers <= peerLimitValue.Maximum ? (decimal)firstTorrent.MaxConnectedPeers : 0;
            trackersList.Text = BuildTrackerList(firstTorrent.Trackers);
        }

        private void downloadLimitEnableField_CheckedChanged(object sender, EventArgs e)
        {
            downloadLimitField.Enabled = downloadLimitEnableField.Checked;
        }

        private void uploadLimitEnableField_CheckedChanged(object sender, EventArgs e)
        {
            uploadLimitField.Enabled = uploadLimitEnableField.Checked;
        }

        private void seedRatioLimitedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            seedRatioLimitValue.Enabled = seedRatioLimitedCheckBox.CheckState == CheckState.Checked;
        }
    }
}
