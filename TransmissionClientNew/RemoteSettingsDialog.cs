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
    public partial class RemoteSettingsDialog : CultureForm
    {
        public static void CloseIfOpen()
        {
            if (ClassSingleton<RemoteSettingsDialog>.IsActive())
            {
                ClassSingleton<RemoteSettingsDialog>.Instance.CloseAndDispose();
            }
        }

        public static void PortTestReplyArrived()
        {
            if (ClassSingleton<RemoteSettingsDialog>.IsActive())
            {
                ClassSingleton<RemoteSettingsDialog>.Instance.SetPortTestReplyArrived();
            }
        }

        public void SetPortTestReplyArrived()
        {
            testPortButton.Text = (string)testPortButton.Tag;
            testPortButton.Enabled = true;
        }

        private RemoteSettingsDialog()
        {
            InitializeComponent();
        }

        private void CloseFormButton_Click(object sender, EventArgs e)
        {
            CloseAndDispose();
        }

        private void SetNumeric(NumericUpDown numeric, int value, int def)
        {
            SetNumeric(numeric, (decimal)value, def);
        }

        private void SetNumeric(NumericUpDown numeric, decimal value, int def)
        {
            numeric.Value = value <= numeric.Maximum && value >= numeric.Minimum ? value : def;
        }

        private void RemoteSettingsDialog_Load(object sender, EventArgs e)
        {
            try
            {
                JsonObject session = (JsonObject)Program.DaemonDescriptor.SessionData;
                downloadToField.Text = (string)session[ProtocolConstants.DOWNLOAD_DIR];
                limitDownloadValue.Enabled = limitDownloadCheckBox.Checked = Toolbox.ToBool(session[ProtocolConstants.FIELD_SPEEDLIMITDOWNENABLED]);
                SetLimitField(limitDownloadValue, Toolbox.ToInt(session[ProtocolConstants.FIELD_SPEEDLIMITDOWN]));
                limitUploadValue.Enabled = limitUploadCheckBox.Checked = Toolbox.ToBool(session[ProtocolConstants.FIELD_SPEEDLIMITUPENABLED]);
                SetLimitField(limitUploadValue, Toolbox.ToInt(session[ProtocolConstants.FIELD_SPEEDLIMITUP]));
                if (session.Contains("port"))
                {
                    incomingPortValue.Tag = "port";
                    SetNumeric(incomingPortValue, Toolbox.ToInt(session["port"]), 5555);
                }
                else if (session.Contains("peer-port"))
                {
                    incomingPortValue.Tag = "peer-port";
                    SetNumeric(incomingPortValue, Toolbox.ToInt(session["peer-port"]), 5555);
                }
                portForwardCheckBox.Checked = Toolbox.ToBool(session["port-forwarding-enabled"]);
                string enc = session["encryption"] as string;
                if (enc.Equals("preferred"))
                {
                    encryptionCombobox.SelectedIndex = 1;
                }
                else if (enc.Equals("required"))
                {
                    encryptionCombobox.SelectedIndex = 2;
                }
                else
                {
                    encryptionCombobox.SelectedIndex = 0;
                }
                // peer limit
                if (session.Contains(ProtocolConstants.FIELD_PEERLIMIT))
                {
                    SetNumeric(peerLimitValue, Toolbox.ToInt(session[ProtocolConstants.FIELD_PEERLIMIT]), 100);
                    peerLimitValue.Tag = ProtocolConstants.FIELD_PEERLIMIT;
                }
                else if (session.Contains(ProtocolConstants.FIELD_PEERLIMITGLOBAL))
                {
                    SetNumeric(peerLimitValue, Toolbox.ToInt(session[ProtocolConstants.FIELD_PEERLIMITGLOBAL]), 100);
                    peerLimitValue.Tag = ProtocolConstants.FIELD_PEERLIMITGLOBAL;
                }
                if (session.Contains(ProtocolConstants.FIELD_PEERLIMITPERTORRENT))
                {
                    SetNumeric(peerLimitTorrentValue, Toolbox.ToInt(session[ProtocolConstants.FIELD_PEERLIMITPERTORRENT]), 100);
                }
                // pex
                if (session.Contains(ProtocolConstants.FIELD_PEXALLOWED))
                {
                    PEXcheckBox.Checked = Toolbox.ToBool(session[ProtocolConstants.FIELD_PEXALLOWED]);
                    PEXcheckBox.Tag = ProtocolConstants.FIELD_PEXALLOWED;
                }
                else if (session.Contains(ProtocolConstants.FIELD_PEXENABLED))
                {
                    PEXcheckBox.Checked = Toolbox.ToBool(session[ProtocolConstants.FIELD_PEXENABLED]);
                    PEXcheckBox.Tag = ProtocolConstants.FIELD_PEXENABLED;
                }
                // blocklist
                if (blocklistEnabledCheckBox.Enabled = updateBlocklistButton.Enabled = blocklistEnabledCheckBox.Enabled = session.Contains(ProtocolConstants.FIELD_BLOCKLISTENABLED))
                {
                    blocklistEnabledCheckBox.Checked = Toolbox.ToBool(session[ProtocolConstants.FIELD_BLOCKLISTENABLED]);
                }
                if (altSpeedLimitEnable.Enabled =
                    altUploadLimitField.Enabled =
                    altDownloadLimitField.Enabled =
                    altTimeConstraintEnabled.Enabled =
                    timeConstraintEndHours.Enabled =
                    timeConstraintBeginHours.Enabled =
                    timeConstaintEndMinutes.Enabled =
                    timeConstaintBeginMinutes.Enabled =
                    session.Contains(ProtocolConstants.FIELD_ALTSPEEDENABLED))
                {
                    SetNumeric(altDownloadLimitField, Toolbox.ToInt(session[ProtocolConstants.FIELD_ALTSPEEDDOWN]), 0);
                    SetNumeric(altUploadLimitField, Toolbox.ToInt(session[ProtocolConstants.FIELD_ALTSPEEDUP]), 0);
                    altDownloadLimitField.Enabled = altUploadLimitField.Enabled = altSpeedLimitEnable.Checked = Toolbox.ToBool(session[ProtocolConstants.FIELD_ALTSPEEDENABLED]);
                    timeConstaintBeginMinutes.Enabled = timeConstaintEndMinutes.Enabled = timeConstraintBeginHours.Enabled = timeConstraintEndHours.Enabled = altTimeConstraintEnabled.Checked = Toolbox.ToBool(session["alt-speed-time-enabled"]);
                    int altSpeedTimeBegin = Toolbox.ToInt(session[ProtocolConstants.FIELD_ALTSPEEDTIMEBEGIN]);
                    int altSpeedTimeEnd = Toolbox.ToInt(session[ProtocolConstants.FIELD_ALTSPEEDTIMEEND]);
                    SetNumeric(timeConstraintBeginHours, Math.Floor((decimal)altSpeedTimeBegin / 60), 0);
                    SetNumeric(timeConstraintEndHours, Math.Floor((decimal)altSpeedTimeEnd / 60), 0);
                    timeConstaintBeginMinutes.Value = altSpeedTimeBegin % 60;
                    timeConstaintEndMinutes.Value = altSpeedTimeEnd % 60;
                }
                if (seedRatioEnabledCheckBox.Enabled = seedLimitUpDown.Enabled = session.Contains(ProtocolConstants.FIELD_SEEDRATIOLIMITED))
                {
                    SetNumeric(seedLimitUpDown, Toolbox.ToDecimal(session[ProtocolConstants.FIELD_SEEDRATIOLIMIT]), 1);
                    seedLimitUpDown.Enabled = seedRatioEnabledCheckBox.Checked = Toolbox.ToBool(session[ProtocolConstants.FIELD_SEEDRATIOLIMITED]);
                }
                if (seedIdleEnabledCheckBox.Enabled = seedIdleLimitUpDown.Enabled = session.Contains(ProtocolConstants.FIELD_IDLESEEDLIMITENABLED))
                {
                    SetNumeric(seedIdleLimitUpDown, Toolbox.ToDecimal(session[ProtocolConstants.FIELD_IDLESEEDLIMIT]), 1);
                    seedIdleLimitUpDown.Enabled = seedIdleEnabledCheckBox.Checked = Toolbox.ToBool(session[ProtocolConstants.FIELD_IDLESEEDLIMITENABLED]);
                }
                if (incompleteToCheckBox.Enabled = incompleteToField.Enabled = session.Contains(ProtocolConstants.FIELD_INCOMPLETE_DIR))
                {
                    incompleteToField.Text = (string)session[ProtocolConstants.FIELD_INCOMPLETE_DIR];
                    incompleteToField.Enabled = incompleteToCheckBox.Checked = Toolbox.ToBool(session[ProtocolConstants.FIELD_INCOMPLETE_DIR_ENABLED]);
                }
                if (watchdirCheckBox.Enabled = watchdirField.Enabled = session.Contains(ProtocolConstants.FIELD_WATCH_DIR))
                {
                    watchdirField.Text = (string)session[ProtocolConstants.FIELD_WATCH_DIR];
                    watchdirField.Enabled = watchdirCheckBox.Checked = Toolbox.ToBool(session[ProtocolConstants.FIELD_WATCH_DIR_ENABLED]);
                }
                if (dhtEnabled.Enabled = session.Contains(ProtocolConstants.FIELD_DHTENABLED))
                {
                    dhtEnabled.Checked = Toolbox.ToBool(session[ProtocolConstants.FIELD_DHTENABLED]);
                }
                if (LpdEnabledCheckBox.Enabled = session.Contains(ProtocolConstants.FIELD_LPDENABLED))
                {
                    LpdEnabledCheckBox.Checked = Toolbox.ToBool(session[ProtocolConstants.FIELD_LPDENABLED]);
                }
                if (renamePartialFilesCheckBox.Enabled = session.Contains(ProtocolConstants.FIELD_RENAME_PARTIAL_FILES))
                {
                    renamePartialFilesCheckBox.Checked = Toolbox.ToBool(session[ProtocolConstants.FIELD_RENAME_PARTIAL_FILES]);
                }
                testPortButton.Enabled = Program.DaemonDescriptor.RpcVersion >= 5;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to load settings data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CloseAndDispose();
            }
        }

        private void CloseAndDispose()
        {
            this.Close();
            this.Dispose();
        }

        private void SetLimitField(NumericUpDown field, int limit)
        {
            if (Program.DaemonDescriptor.Version < 1.40)
            {
                field.Value = limit >= field.Minimum && limit >= 1024 && limit <= field.Maximum ? limit / 1024 : 0;
            }
            else
            {
                field.Value = limit >= field.Minimum && limit <= field.Maximum ? limit : 0;
            }
        }

        private void LimitUploadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            limitUploadValue.Enabled = limitUploadCheckBox.Checked;
        }

        private void LimitDownloadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            limitDownloadValue.Enabled = limitDownloadCheckBox.Checked;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            JsonObject request = Requests.CreateBasicObject(ProtocolConstants.METHOD_SESSIONSET);
            JsonObject arguments = Requests.GetArgObject(request);
            arguments.Put((string)incomingPortValue.Tag, incomingPortValue.Value);
            arguments.Put(ProtocolConstants.FIELD_PORTFORWARDINGENABLED, portForwardCheckBox.Checked);
            arguments.Put((string)PEXcheckBox.Tag, PEXcheckBox.Checked);
            arguments.Put((string)peerLimitValue.Tag, peerLimitValue.Value);
            arguments.Put(ProtocolConstants.FIELD_PEERLIMITPERTORRENT, peerLimitTorrentValue.Value);
            switch (encryptionCombobox.SelectedIndex)
            {
                case 1:
                    arguments.Put(ProtocolConstants.FIELD_ENCRYPTION, ProtocolConstants.VALUE_PREFERRED);
                    break;
                case 2:
                    arguments.Put(ProtocolConstants.FIELD_ENCRYPTION, ProtocolConstants.VALUE_REQUIRED);
                    break;
                default:
                    arguments.Put(ProtocolConstants.FIELD_ENCRYPTION, ProtocolConstants.VALUE_TOLERATED);
                    break;
            }
            arguments.Put(ProtocolConstants.FIELD_SPEEDLIMITUPENABLED, limitUploadCheckBox.Checked);
            arguments.Put(ProtocolConstants.FIELD_SPEEDLIMITUP, limitUploadValue.Value);
            arguments.Put(ProtocolConstants.FIELD_SPEEDLIMITDOWNENABLED, limitDownloadCheckBox.Checked);
            arguments.Put(ProtocolConstants.FIELD_SPEEDLIMITDOWN, limitDownloadValue.Value);
            if (altSpeedLimitEnable.Enabled)
            {
                arguments.Put(ProtocolConstants.FIELD_ALTSPEEDENABLED, altSpeedLimitEnable.Checked);
                arguments.Put(ProtocolConstants.FIELD_ALTSPEEDDOWN, altDownloadLimitField.Value);
                arguments.Put(ProtocolConstants.FIELD_ALTSPEEDUP, altUploadLimitField.Value);
            }
            if (altTimeConstraintEnabled.Enabled)
            {
                arguments.Put(ProtocolConstants.FIELD_ALTSPEEDTIMEENABLED, altTimeConstraintEnabled.Checked);
                arguments.Put(ProtocolConstants.FIELD_ALTSPEEDTIMEBEGIN, timeConstraintBeginHours.Value*60+timeConstaintBeginMinutes.Value);
                arguments.Put(ProtocolConstants.FIELD_ALTSPEEDTIMEEND, timeConstraintEndHours.Value*60+timeConstaintEndMinutes.Value);
            }
            if (blocklistEnabledCheckBox.Enabled)
            {
                arguments.Put(ProtocolConstants.FIELD_BLOCKLISTENABLED, blocklistEnabledCheckBox.Checked);
            }
            if (seedRatioEnabledCheckBox.Enabled)
            {
                arguments.Put(ProtocolConstants.FIELD_SEEDRATIOLIMITED, seedRatioEnabledCheckBox.Checked);
                arguments.Put(ProtocolConstants.FIELD_SEEDRATIOLIMIT, seedLimitUpDown.Value);
            }
            if (seedIdleEnabledCheckBox.Enabled)
            {
                arguments.Put(ProtocolConstants.FIELD_IDLESEEDLIMITENABLED, seedIdleEnabledCheckBox.Checked);
                arguments.Put(ProtocolConstants.FIELD_IDLESEEDLIMIT, seedIdleLimitUpDown.Value);
            }
            if (incompleteToCheckBox.Enabled)
            {
                arguments.Put(ProtocolConstants.FIELD_INCOMPLETE_DIR_ENABLED, incompleteToCheckBox.Checked);
                arguments.Put(ProtocolConstants.FIELD_INCOMPLETE_DIR, incompleteToField.Text);
            }
            if (watchdirCheckBox.Enabled)
            {
                arguments.Put(ProtocolConstants.FIELD_WATCH_DIR_ENABLED, watchdirCheckBox.Checked);
                arguments.Put(ProtocolConstants.FIELD_WATCH_DIR, watchdirField.Text);
            }
            if (dhtEnabled.Enabled)
            {
                arguments.Put(ProtocolConstants.FIELD_DHTENABLED, dhtEnabled.Checked);
            }
            if (LpdEnabledCheckBox.Enabled)
            {
                arguments.Put(ProtocolConstants.FIELD_LPDENABLED, LpdEnabledCheckBox.Checked);
            }
            if (renamePartialFilesCheckBox.Enabled)
            {
                arguments.Put(ProtocolConstants.FIELD_RENAME_PARTIAL_FILES, renamePartialFilesCheckBox.Checked);
            }
            arguments.Put(ProtocolConstants.DOWNLOAD_DIR, downloadToField.Text);
            CommandFactory.RequestAsync(request).Completed += new EventHandler<ResultEventArgs>(RemoteSettingsDialog_Completed);
            CloseAndDispose();
        }

        void RemoteSettingsDialog_Completed(object sender, ResultEventArgs e)
        {
            Timer t = new Timer();
            t.Interval = 250;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }

        void t_Tick(object sender, EventArgs e)
        {
            Timer t = (Timer)sender;
            t.Stop();
            Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.SessionGet()));
        }

        private void altSpeedLimitEnable_CheckedChanged(object sender, EventArgs e)
        {
            altUploadLimitField.Enabled = altDownloadLimitField.Enabled = altSpeedLimitEnable.Checked;
        }

        private void altTimeConstraintEnabled_CheckedChanged(object sender, EventArgs e)
        { 
            timeConstaintBeginMinutes.Enabled = timeConstaintEndMinutes.Enabled = timeConstraintBeginHours.Enabled = timeConstraintEndHours.Enabled = altTimeConstraintEnabled.Checked;
        }

        private void blocklistEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            updateBlocklistButton.Enabled = blocklistEnabledCheckBox.Checked;
        }

        private void updateBlocklistButton_Click(object sender, EventArgs e)
        {
            updateBlocklistButton.Enabled = false;
            updateBlocklistButton.Tag = updateBlocklistButton.Text;
            updateBlocklistButton.Text = OtherStrings.Updating;
            Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.BlocklistUpdate()));
        }

        public static void BlocklistUpdateDone(int size)
        {
            if (ClassSingleton<RemoteSettingsDialog>.IsActive())
            {
                ClassSingleton<RemoteSettingsDialog>.Instance.SetBlocklistUpdateDone(size);
            }
        }

        public void SetBlocklistUpdateDone(int size)
        {
            updateBlocklistButton.Enabled = true;
            updateBlocklistButton.Text = (string)updateBlocklistButton.Tag;
            label15.Text = String.Format(OtherStrings.XInBlocklist, size);
        }

        private void seedRatioEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            seedLimitUpDown.Enabled = seedRatioEnabledCheckBox.Checked;
        }

        private void seedIdleEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            seedIdleLimitUpDown.Enabled = seedIdleEnabledCheckBox.Checked;
        }

        private void testPortButton_Click(object sender, EventArgs e)
        {
            testPortButton.Enabled = false;
            testPortButton.Tag = testPortButton.Text;
            testPortButton.Text = OtherStrings.Querying;
            Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.PortTest()));
        }

        private void incompleteToCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            incompleteToField.Enabled = incompleteToCheckBox.Checked;
        }

        private void watchdirCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            watchdirField.Enabled = watchdirCheckBox.Checked;
        }
    }
}
