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
using System.Collections;
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
            if (seedIdleLimitValue.Enabled)
                arguments.Put(ProtocolConstants.FIELD_SEEDIDLELIMIT, seedIdleLimitValue.Value);
            if (honorsSessionLimits.Enabled)
                arguments.Put(ProtocolConstants.FIELD_HONORSSESSIONLIMITS, honorsSessionLimits.Checked);
            if (seedRatioLimitedCheckBox.Enabled)
                arguments.Put(ProtocolConstants.FIELD_SEEDRATIOMODE, (int)(2 - seedRatioLimitedCheckBox.CheckState));
            if (seedIdleLimitedCheckBox.Enabled)
                arguments.Put(ProtocolConstants.FIELD_SEEDIDLEMODE, (int)(2 - seedIdleLimitedCheckBox.CheckState));
            if (bandwidthComboBox.Enabled)
            {
                int bandwidthPriority = 0;
                if (bandwidthComboBox.SelectedIndex == 0)
                    bandwidthPriority = -1;
                else if (bandwidthComboBox.SelectedIndex == 2)
                    bandwidthPriority = 1;
                arguments.Put(ProtocolConstants.FIELD_BANDWIDTHPRIORITY, bandwidthPriority);
            }

            Torrent firstTorrent = (Torrent)selections[0];
            JsonArray trackerRemove = new JsonArray();
            foreach (JsonObject tracker in firstTorrent.Trackers)
            {
                int id = Toolbox.ToInt(tracker[ProtocolConstants.FIELD_ID]);
                if (!trackersList.Items.Contains(id))
                    trackerRemove.Add(id);
            }
            JsonArray trackerReplace = new JsonArray();
            foreach (TrackerListItem t in trackersList.Items)
            {
                if (!t.Changed) continue;
                trackerReplace.Add(t.Id);
                trackerReplace.Add(t.Announce);
            }
            JsonArray trackerAdd = new JsonArray();
            foreach (TrackerListItem t in trackersList.Items)
            {
                if (t.Id == -1)
                    trackerAdd.Add(t.Announce);
            }
            if (trackerRemove.Count > 0)
                arguments.Put(ProtocolConstants.FIELD_TRACKER_REMOVE, trackerRemove);
            if (trackerReplace.Count > 0)
                arguments.Put(ProtocolConstants.FIELD_TRACKER_REPLACE, trackerReplace);
            if (trackerAdd.Count > 0)
                arguments.Put(ProtocolConstants.FIELD_TRACKER_ADD, trackerAdd);
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
                seedIdleLimitValue.Value = firstTorrent.SeedIdleLimit >= 0 && (decimal)firstTorrent.SeedIdleLimit <= seedIdleLimitValue.Maximum ? (decimal)firstTorrent.SeedIdleLimit : 0;
                seedIdleLimitedCheckBox.CheckState = (CheckState)(2 - firstTorrent.SeedIdleMode);
                seedIdleLimitValue.Enabled = seedIdleLimitedCheckBox.CheckState == CheckState.Checked;
                seedIdleLimitedCheckBox.Enabled = true;
            }
            catch
            {
                seedIdleLimitValue.Enabled = seedIdleLimitedCheckBox.Enabled = false;
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
            // TODO: http://www.codeguru.com/cpp/controls/controls/lists,treesandcombos/article.php/c2291
            removeTrackerButton.Enabled = false;
            trackersList.Items.AddRange(
                Array.ConvertAll<JsonObject, TrackerListItem>((JsonObject[])firstTorrent.Trackers.ToArray(typeof(JsonObject)), delegate(JsonObject jo)
                    {
                        return new TrackerListItem((string)jo[ProtocolConstants.ANNOUNCE], Toolbox.ToInt(jo[ProtocolConstants.FIELD_ID]));
                    })
                );
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

        private void seedIdleLimitedCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            seedIdleLimitValue.Enabled = seedIdleLimitedCheckBox.CheckState == CheckState.Checked;
        }

        private void trackersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeTrackerButton.Enabled = trackersList.SelectedIndex != -1;
        }

        private void trackersList_DoubleClick(object sender, EventArgs e)
        {
            TrackerListItem current = (TrackerListItem)trackersList.SelectedItem;
            string newannounce = InputBox.Show(OtherStrings.EditTrackerUrl, OtherStrings.EditUrl, current.ToString(), false);
            if (Uri.IsWellFormedUriString(newannounce, UriKind.Absolute))
            {
                if (newannounce != null)
                {
                    int idx = trackersList.Items.IndexOf(newannounce);
                    if (idx == -1 || idx == trackersList.SelectedIndex)
                    {
                        current.Announce = newannounce;
                        trackersList.RefreshItems();
                    }
                    else
                        MessageBox.Show(OtherStrings.TrackerExists, OtherStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show(OtherStrings.InvalidUrl, OtherStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void addTrackerButton_Click(object sender, EventArgs e)
        {
            string newannounce = InputBox.Show(OtherStrings.AddTrackerUrl, OtherStrings.AddUrl, false);
            if (Uri.IsWellFormedUriString(newannounce, UriKind.Absolute))
            {
                if (newannounce != null)
                {
                    if (!trackersList.Items.Contains(newannounce))
                    {
                        trackersList.Items.Add(new TrackerListItem(newannounce, -1));
                    }
                    else
                        MessageBox.Show(OtherStrings.TrackerExists, OtherStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show(OtherStrings.InvalidUrl, OtherStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void removeTrackerButton_Click(object sender, EventArgs e)
        {
            trackersList.Items.RemoveAt(trackersList.SelectedIndex);
        }
    }
}
