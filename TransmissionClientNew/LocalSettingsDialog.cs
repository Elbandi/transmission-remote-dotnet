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
    public partial class LocalSettingsDialog : CultureForm
    {
        private string originalHost;
        private int originalPort;

        public LocalSettingsDialog()
        {
            InitializeComponent();
            LocalSettingsSingleton settings = LocalSettingsSingleton.Instance;
            List<string> profiles = settings.Profiles;
            for (int i = 0; i < profiles.Count; i++)
            {
                profileComboBox.Items.Add(profiles[i]);
                if (profiles[i].Equals(settings.CurrentProfile))
                {
                    profileComboBox.SelectedIndex = i;
                }
            }
            if (profileComboBox.SelectedIndex < 0)
            {
                profileComboBox.SelectedIndex = 0;
            }
            LoadCurrentProfile();
        }

        private void LoadCurrentProfile()
        {
            LocalSettingsSingleton settings = LocalSettingsSingleton.Instance;
            HostField.Text = originalHost = settings.Host;
            PortField.Value = originalPort = settings.Port;
            RefreshRateValue.Value = settings.RefreshRate;
            UseSSLCheckBox.Checked = settings.UseSSL;
            AutoConnectCheckBox.Checked = settings.AutoConnect;
            AutoCheckUpdateCheckBox.Checked = settings.AutoCheckupdate;
            PassField.Enabled = UserField.Enabled = EnableAuthCheckBox.Checked = settings.AuthEnabled;
            UserField.Text = settings.User;
            PassField.Text = settings.Pass;
            notificationOnCompletionCheckBox.Enabled = notificationOnAdditionCheckBox.Enabled
                = minimizeOnCloseCheckBox.Enabled = MinToTrayCheckBox.Checked
                = settings.MinToTray;
            EnableProxyCombo.SelectedIndex = (int)settings.ProxyMode;
            ProxyPortField.Enabled = ProxyHostField.Enabled = settings.ProxyMode == ProxyMode.Enabled;
            ProxyHostField.Text = settings.ProxyHost;
            ProxyPortField.Value = settings.ProxyPort;
            ProxyAuthEnableCheckBox.Checked = settings.ProxyAuth;
            ProxyUserField.Enabled = ProxyPassField.Enabled = (settings.ProxyAuth && settings.ProxyMode == ProxyMode.Enabled);
            ProxyUserField.Text = settings.ProxyUser;
            ProxyPassField.Text = settings.ProxyPass;
            StartPausedCheckBox.Checked = settings.StartPaused;
            RetryLimitValue.Value = settings.RetryLimit;
            notificationOnAdditionCheckBox.Checked = settings.StartedBalloon;
            notificationOnCompletionCheckBox.Checked = settings.CompletedBaloon;
            minimizeOnCloseCheckBox.Checked = settings.MinOnClose;
            textBox2.Text = settings.PlinkPath;
            checkBox1.Checked = settings.PlinkEnable;
            textBox3.Text = settings.PlinkCmd;
            downloadLimitItems.Text = settings.DownLimit;
            uploadLimitItems.Text = settings.UpLimit;
            checkBox2.Checked = settings.UploadPrompt;
            listBox1.Items.Clear();
            JsonObject mappings = settings.SambaShareMappings;
            foreach (string key in mappings.Names)
            {
                listBox1.Items.Add(String.Format("{0} => {1}", key, (string)mappings[key]));
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveSettings()
        {
            LocalSettingsSingleton settings = LocalSettingsSingleton.Instance;
            settings.Host = HostField.Text;
            settings.Port = (int)PortField.Value;
            settings.UseSSL = UseSSLCheckBox.Checked;
            settings.AutoConnect = AutoConnectCheckBox.Checked;
            settings.AutoCheckupdate = AutoCheckUpdateCheckBox.Checked;
            settings.RefreshRate = (int)RefreshRateValue.Value;
            Program.Form.refreshTimer.Interval = (int)RefreshRateValue.Value * 1000;
            settings.AuthEnabled = EnableAuthCheckBox.Checked;
            settings.User = UserField.Text;
            settings.Pass = PassField.Text;
            Program.Form.notifyIcon.Visible = settings.MinToTray = MinToTrayCheckBox.Checked;
            settings.ProxyMode = (ProxyMode)EnableProxyCombo.SelectedIndex;
            settings.ProxyHost = ProxyHostField.Text;
            settings.ProxyPort = (int)ProxyPortField.Value;
            settings.ProxyAuth = ProxyAuthEnableCheckBox.Checked;
            settings.ProxyUser = ProxyUserField.Text;
            settings.ProxyPass = ProxyPassField.Text;
            settings.StartPaused = StartPausedCheckBox.Checked;
            settings.RetryLimit = (int)RetryLimitValue.Value;
            settings.StartedBalloon = notificationOnAdditionCheckBox.Checked;
            settings.CompletedBaloon = notificationOnCompletionCheckBox.Checked;
            settings.MinOnClose = minimizeOnCloseCheckBox.Checked;
            settings.PlinkCmd = textBox3.Text;
            settings.PlinkEnable = checkBox1.Checked;
            settings.PlinkPath = textBox2.Text;
            settings.UpLimit = uploadLimitItems.Text;
            settings.DownLimit = downloadLimitItems.Text;
            settings.UploadPrompt = checkBox2.Checked;
            Program.Form.SetRemoteCmdButtonVisible(Program.Connected);
            settings.Commit();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            LocalSettingsSingleton settings = LocalSettingsSingleton.Instance;
            if (Program.Connected && (settings.Host != originalHost || settings.Port != originalPort))
            {
                Program.Connected = false;
                Program.Form.Connect();
            }
            this.Close();
        }

        private void EnableAuthCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PassField.Enabled = UserField.Enabled = EnableAuthCheckBox.Checked;
        }

        private void EnableProxyCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProxyAuthEnableCheckBox.Enabled = ProxyHostField.Enabled = ProxyPortField.Enabled = (EnableProxyCombo.SelectedIndex == 1);
            ProxyUserField.Enabled = ProxyPassField.Enabled = (ProxyAuthEnableCheckBox.Checked && EnableProxyCombo.SelectedIndex == 1);
        }

        private void ProxyAuthEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ProxyUserField.Enabled = ProxyPassField.Enabled = ProxyAuthEnableCheckBox.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
            if (Program.Connected)
            {
                Program.Connected = false;
            }
            Program.Form.Connect();
            this.Close();
        }

        private void profileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeProfileButton.Enabled = !profileComboBox.SelectedItem.ToString().Equals("Default");
            LocalSettingsSingleton settings = LocalSettingsSingleton.Instance;
            string selectedProfile = profileComboBox.SelectedItem.ToString();
            foreach (ToolStripMenuItem item in Program.Form.connectButton.DropDownItems)
            {
                item.Checked = selectedProfile.Equals(item.ToString());
            }
            if (!selectedProfile.Equals(settings.CurrentProfile))
            {
                SaveSettings();
                settings.CurrentProfile = selectedProfile;
                LoadCurrentProfile();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            addProfileButton.Enabled = textBox1.Text.Length > 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LocalSettingsSingleton settings = LocalSettingsSingleton.Instance;
            ToolStripMenuItem profile = Program.Form.CreateProfileMenuItem(textBox1.Text);
            foreach (ToolStripMenuItem item in Program.Form.connectButton.DropDownItems)
            {
                item.Checked = false;
            }
            profile.Checked = true;
            settings.CreateProfile(textBox1.Text);
            profileComboBox.SelectedIndex = profileComboBox.Items.Add(textBox1.Text);
            textBox1.Text = "";
            LoadCurrentProfile();
        }

        private void removeProfileButton_Click(object sender, EventArgs e)
        {
            LocalSettingsSingleton settings = LocalSettingsSingleton.Instance;
            try
            {
                object selectedItem = profileComboBox.SelectedItem;
                settings.RemoveProfile(selectedItem.ToString());
                profileComboBox.SelectedIndex = 0;
                settings.CurrentProfile = "Default";
                profileComboBox.Items.Remove(selectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MinToTrayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            notificationOnAdditionCheckBox.Enabled = notificationOnCompletionCheckBox.Enabled
                = minimizeOnCloseCheckBox.Enabled = MinToTrayCheckBox.Checked;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button2.Enabled = textBox3.Enabled = ((CheckBox)sender).Checked;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void HostField_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Uri uri = new Uri(HostField.Text);
                HostField.Text = uri.Host;
                PortField.Value = uri.Port;
                if (uri.UserInfo != null)
                {
                    string[] authComponents = uri.UserInfo.Split(':');
                    UserField.Text = authComponents[0];
                    if (authComponents.Length > 1)
                        PassField.Text = authComponents[1];
                }
            }
            catch { }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = listBox1.SelectedIndex >= 0;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            button4.Enabled = textBox4.Text.Length > 0 && textBox5.Text.Length >= 3;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string itemText = listBox1.SelectedItem.ToString();
            LocalSettingsSingleton.Instance.RemoveSambaMapping(itemText.Substring(0, itemText.IndexOf("=>")-1));
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LocalSettingsSingleton.Instance.AddSambaMapping(textBox4.Text, textBox5.Text);
            listBox1.Items.Add(String.Format("{0} => {1}", textBox4.Text, textBox5.Text));
        }
    }
}
