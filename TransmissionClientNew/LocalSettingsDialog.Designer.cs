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

namespace TransmissionRemoteDotnet
{
    partial class LocalSettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocalSettingsDialog));
            this.SaveButton = new System.Windows.Forms.Button();
            this.CloseDialogButton = new System.Windows.Forms.Button();
            this.SaveAndConnectButton = new System.Windows.Forms.Button();
            this.PlinkPathOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabGlobalSettings = new System.Windows.Forms.TabPage();
            this.groupAutoConnect = new System.Windows.Forms.GroupBox();
            this.AutoConnectComboBox = new System.Windows.Forms.ComboBox();
            this.groupCurrentProfile = new System.Windows.Forms.GroupBox();
            this.CurrentProfileComboBox = new System.Windows.Forms.ComboBox();
            this.groupPlinkPath = new System.Windows.Forms.GroupBox();
            this.PlinkPathButton = new System.Windows.Forms.Button();
            this.PlinkPathTextBox = new System.Windows.Forms.TextBox();
            this.groupBehavior = new System.Windows.Forms.GroupBox();
            this.AutoUpdateGeoipCheckBox = new System.Windows.Forms.CheckBox();
            this.TrayGroupBox = new System.Windows.Forms.GroupBox();
            this.ColorTrayIconCheckBox = new System.Windows.Forms.CheckBox();
            this.notificationOnCompletionCheckBox = new System.Windows.Forms.CheckBox();
            this.notificationOnAdditionCheckBox = new System.Windows.Forms.CheckBox();
            this.minimizeOnCloseCheckBox = new System.Windows.Forms.CheckBox();
            this.MinToTrayCheckBox = new System.Windows.Forms.CheckBox();
            this.StartOnSystemCheckBox = new System.Windows.Forms.CheckBox();
            this.UpdateToBetaCheckBox = new System.Windows.Forms.CheckBox();
            this.DontSavePasswordsCheckBox = new System.Windows.Forms.CheckBox();
            this.defaultActionComboBox = new System.Windows.Forms.ComboBox();
            this.defaultActionLabel = new System.Windows.Forms.Label();
            this.DeleteTorrentCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoCheckUpdateCheckBox = new System.Windows.Forms.CheckBox();
            this.UploadPromptCheckBox = new System.Windows.Forms.CheckBox();
            this.tabServersSettings = new System.Windows.Forms.TabPage();
            this.removeServerButton = new System.Windows.Forms.Button();
            this.addServerButton = new System.Windows.Forms.Button();
            this.tabServerSettings = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.RefreshRateTrayValue = new System.Windows.Forms.NumericUpDown();
            this.SaveServerButton = new System.Windows.Forms.Button();
            this.UseSSLCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.HostField = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RetryLimitValue = new System.Windows.Forms.NumericUpDown();
            this.PortField = new System.Windows.Forms.NumericUpDown();
            this.RefreshRateValue = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ClearPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.EnableAuthCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PassField = new System.Windows.Forms.TextBox();
            this.UserField = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ClearProxyPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.ProxyAuthEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.EnableProxyCombo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ProxyPortField = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.ProxyHostField = new System.Windows.Forms.TextBox();
            this.ProxyUserField = new System.Windows.Forms.TextBox();
            this.ProxyPassField = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label13 = new System.Windows.Forms.Label();
            this.PlinkCmdTextBox = new System.Windows.Forms.TextBox();
            this.PlinkEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.ClearDestPathHistoryButton = new System.Windows.Forms.Button();
            this.StartPausedCheckBox = new System.Windows.Forms.CheckBox();
            this.customPathTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.downloadLimitItems = new System.Windows.Forms.TextBox();
            this.uploadLimitItems = new System.Windows.Forms.TextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MappingHelpButton = new TransmissionRemoteDotnet.CustomControls.USButton();
            this.label11 = new System.Windows.Forms.Label();
            this.AddShareButton = new System.Windows.Forms.Button();
            this.UnixPathPrefixTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.SambaShareTextBox = new System.Windows.Forms.TextBox();
            this.RemoveShareButton = new System.Windows.Forms.Button();
            this.listSambaShareMappings = new System.Windows.Forms.ListBox();
            this.listServers = new System.Windows.Forms.ListView();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.columnHost = new System.Windows.Forms.ColumnHeader();
            this.columnPort = new System.Windows.Forms.ColumnHeader();
            this.ServersMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabRssSettings = new System.Windows.Forms.TabPage();
            this.listRssFeeds = new System.Windows.Forms.ListView();
            this.columnFeedName = new System.Windows.Forms.ColumnHeader();
            this.columnFeedUrl = new System.Windows.Forms.ColumnHeader();
            this.groupFeed = new System.Windows.Forms.GroupBox();
            this.RemoveFeedButton = new System.Windows.Forms.Button();
            this.AddFeedButton = new System.Windows.Forms.Button();
            this.FeedNameTextBox = new System.Windows.Forms.TextBox();
            this.FeedUrlLabel = new System.Windows.Forms.Label();
            this.FeedUrlTextBox = new System.Windows.Forms.TextBox();
            this.FeedNameLabel = new System.Windows.Forms.Label();
            this.tabSkinSettings = new System.Windows.Forms.TabPage();
            this.trayImageBrowse = new TransmissionRemoteDotnet.CustomControls.SkinImageBrowseTextBox();
            this.toolbarImageBrowse = new TransmissionRemoteDotnet.CustomControls.SkinImageBrowseTextBox();
            this.stateImageBrowse = new TransmissionRemoteDotnet.CustomControls.SkinImageBrowseTextBox();
            this.infopanelImageBrowse = new TransmissionRemoteDotnet.CustomControls.SkinImageBrowseTextBox();
            this.openImageFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabSettings.SuspendLayout();
            this.tabGlobalSettings.SuspendLayout();
            this.groupAutoConnect.SuspendLayout();
            this.groupCurrentProfile.SuspendLayout();
            this.groupPlinkPath.SuspendLayout();
            this.groupBehavior.SuspendLayout();
            this.TrayGroupBox.SuspendLayout();
            this.tabServersSettings.SuspendLayout();
            this.tabServerSettings.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshRateTrayValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RetryLimitValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PortField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshRateValue)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyPortField)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.ServersMenuStrip.SuspendLayout();
            this.tabRssSettings.SuspendLayout();
            this.groupFeed.SuspendLayout();
            this.tabSkinSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.SaveButton, "SaveButton");
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseDialogButton
            // 
            this.CloseDialogButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.CloseDialogButton, "CloseDialogButton");
            this.CloseDialogButton.Name = "CloseDialogButton";
            this.CloseDialogButton.UseVisualStyleBackColor = true;
            this.CloseDialogButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SaveAndConnectButton
            // 
            this.SaveAndConnectButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.SaveAndConnectButton, "SaveAndConnectButton");
            this.SaveAndConnectButton.Name = "SaveAndConnectButton";
            this.SaveAndConnectButton.UseVisualStyleBackColor = true;
            this.SaveAndConnectButton.Click += new System.EventHandler(this.SaveAndConnectButton_Click);
            // 
            // PlinkPathOpenFileDialog
            // 
            resources.ApplyResources(this.PlinkPathOpenFileDialog, "PlinkPathOpenFileDialog");
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabGlobalSettings);
            this.tabSettings.Controls.Add(this.tabServersSettings);
            this.tabSettings.Controls.Add(this.tabRssSettings);
            this.tabSettings.Controls.Add(this.tabSkinSettings);
            resources.ApplyResources(this.tabSettings, "tabSettings");
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            // 
            // tabGlobalSettings
            // 
            this.tabGlobalSettings.Controls.Add(this.groupAutoConnect);
            this.tabGlobalSettings.Controls.Add(this.groupCurrentProfile);
            this.tabGlobalSettings.Controls.Add(this.groupPlinkPath);
            this.tabGlobalSettings.Controls.Add(this.groupBehavior);
            resources.ApplyResources(this.tabGlobalSettings, "tabGlobalSettings");
            this.tabGlobalSettings.Name = "tabGlobalSettings";
            this.tabGlobalSettings.UseVisualStyleBackColor = true;
            // 
            // groupAutoConnect
            // 
            this.groupAutoConnect.Controls.Add(this.AutoConnectComboBox);
            resources.ApplyResources(this.groupAutoConnect, "groupAutoConnect");
            this.groupAutoConnect.Name = "groupAutoConnect";
            this.groupAutoConnect.TabStop = false;
            // 
            // AutoConnectComboBox
            // 
            this.AutoConnectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AutoConnectComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.AutoConnectComboBox, "AutoConnectComboBox");
            this.AutoConnectComboBox.Name = "AutoConnectComboBox";
            // 
            // groupCurrentProfile
            // 
            this.groupCurrentProfile.Controls.Add(this.CurrentProfileComboBox);
            resources.ApplyResources(this.groupCurrentProfile, "groupCurrentProfile");
            this.groupCurrentProfile.Name = "groupCurrentProfile";
            this.groupCurrentProfile.TabStop = false;
            // 
            // CurrentProfileComboBox
            // 
            this.CurrentProfileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CurrentProfileComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.CurrentProfileComboBox, "CurrentProfileComboBox");
            this.CurrentProfileComboBox.Name = "CurrentProfileComboBox";
            this.CurrentProfileComboBox.SelectedIndexChanged += new System.EventHandler(this.CurrentProfileComboBox_SelectedIndexChanged);
            // 
            // groupPlinkPath
            // 
            this.groupPlinkPath.Controls.Add(this.PlinkPathButton);
            this.groupPlinkPath.Controls.Add(this.PlinkPathTextBox);
            resources.ApplyResources(this.groupPlinkPath, "groupPlinkPath");
            this.groupPlinkPath.Name = "groupPlinkPath";
            this.groupPlinkPath.TabStop = false;
            // 
            // PlinkPathButton
            // 
            resources.ApplyResources(this.PlinkPathButton, "PlinkPathButton");
            this.PlinkPathButton.Name = "PlinkPathButton";
            this.PlinkPathButton.UseVisualStyleBackColor = true;
            this.PlinkPathButton.Click += new System.EventHandler(this.PlinkPathButton_Click);
            // 
            // PlinkPathTextBox
            // 
            resources.ApplyResources(this.PlinkPathTextBox, "PlinkPathTextBox");
            this.PlinkPathTextBox.Name = "PlinkPathTextBox";
            this.PlinkPathTextBox.ReadOnly = true;
            this.PlinkPathTextBox.TextChanged += new System.EventHandler(this.PlinkPathTextBox_TextChanged);
            // 
            // groupBehavior
            // 
            this.groupBehavior.Controls.Add(this.AutoUpdateGeoipCheckBox);
            this.groupBehavior.Controls.Add(this.TrayGroupBox);
            this.groupBehavior.Controls.Add(this.StartOnSystemCheckBox);
            this.groupBehavior.Controls.Add(this.UpdateToBetaCheckBox);
            this.groupBehavior.Controls.Add(this.DontSavePasswordsCheckBox);
            this.groupBehavior.Controls.Add(this.defaultActionComboBox);
            this.groupBehavior.Controls.Add(this.defaultActionLabel);
            this.groupBehavior.Controls.Add(this.DeleteTorrentCheckBox);
            this.groupBehavior.Controls.Add(this.AutoCheckUpdateCheckBox);
            this.groupBehavior.Controls.Add(this.UploadPromptCheckBox);
            resources.ApplyResources(this.groupBehavior, "groupBehavior");
            this.groupBehavior.Name = "groupBehavior";
            this.groupBehavior.TabStop = false;
            // 
            // AutoUpdateGeoipCheckBox
            // 
            resources.ApplyResources(this.AutoUpdateGeoipCheckBox, "AutoUpdateGeoipCheckBox");
            this.AutoUpdateGeoipCheckBox.Name = "AutoUpdateGeoipCheckBox";
            this.AutoUpdateGeoipCheckBox.UseVisualStyleBackColor = true;
            // 
            // TrayGroupBox
            // 
            this.TrayGroupBox.Controls.Add(this.ColorTrayIconCheckBox);
            this.TrayGroupBox.Controls.Add(this.notificationOnCompletionCheckBox);
            this.TrayGroupBox.Controls.Add(this.notificationOnAdditionCheckBox);
            this.TrayGroupBox.Controls.Add(this.minimizeOnCloseCheckBox);
            this.TrayGroupBox.Controls.Add(this.MinToTrayCheckBox);
            resources.ApplyResources(this.TrayGroupBox, "TrayGroupBox");
            this.TrayGroupBox.Name = "TrayGroupBox";
            this.TrayGroupBox.TabStop = false;
            // 
            // ColorTrayIconCheckBox
            // 
            resources.ApplyResources(this.ColorTrayIconCheckBox, "ColorTrayIconCheckBox");
            this.ColorTrayIconCheckBox.Name = "ColorTrayIconCheckBox";
            this.ColorTrayIconCheckBox.UseVisualStyleBackColor = true;
            // 
            // notificationOnCompletionCheckBox
            // 
            resources.ApplyResources(this.notificationOnCompletionCheckBox, "notificationOnCompletionCheckBox");
            this.notificationOnCompletionCheckBox.Name = "notificationOnCompletionCheckBox";
            this.notificationOnCompletionCheckBox.UseVisualStyleBackColor = true;
            // 
            // notificationOnAdditionCheckBox
            // 
            resources.ApplyResources(this.notificationOnAdditionCheckBox, "notificationOnAdditionCheckBox");
            this.notificationOnAdditionCheckBox.Name = "notificationOnAdditionCheckBox";
            this.notificationOnAdditionCheckBox.UseVisualStyleBackColor = true;
            // 
            // minimizeOnCloseCheckBox
            // 
            resources.ApplyResources(this.minimizeOnCloseCheckBox, "minimizeOnCloseCheckBox");
            this.minimizeOnCloseCheckBox.Name = "minimizeOnCloseCheckBox";
            this.minimizeOnCloseCheckBox.UseVisualStyleBackColor = true;
            // 
            // MinToTrayCheckBox
            // 
            resources.ApplyResources(this.MinToTrayCheckBox, "MinToTrayCheckBox");
            this.MinToTrayCheckBox.Name = "MinToTrayCheckBox";
            this.MinToTrayCheckBox.UseVisualStyleBackColor = true;
            this.MinToTrayCheckBox.CheckedChanged += new System.EventHandler(this.MinToTrayCheckBox_CheckedChanged);
            // 
            // StartOnSystemCheckBox
            // 
            resources.ApplyResources(this.StartOnSystemCheckBox, "StartOnSystemCheckBox");
            this.StartOnSystemCheckBox.Name = "StartOnSystemCheckBox";
            this.StartOnSystemCheckBox.UseVisualStyleBackColor = true;
            // 
            // UpdateToBetaCheckBox
            // 
            resources.ApplyResources(this.UpdateToBetaCheckBox, "UpdateToBetaCheckBox");
            this.UpdateToBetaCheckBox.Name = "UpdateToBetaCheckBox";
            this.UpdateToBetaCheckBox.UseVisualStyleBackColor = true;
            // 
            // DontSavePasswordsCheckBox
            // 
            resources.ApplyResources(this.DontSavePasswordsCheckBox, "DontSavePasswordsCheckBox");
            this.DontSavePasswordsCheckBox.Name = "DontSavePasswordsCheckBox";
            this.DontSavePasswordsCheckBox.UseVisualStyleBackColor = true;
            // 
            // defaultActionComboBox
            // 
            this.defaultActionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defaultActionComboBox.DropDownWidth = 160;
            this.defaultActionComboBox.FormattingEnabled = true;
            this.defaultActionComboBox.Items.AddRange(new object[] {
            resources.GetString("defaultActionComboBox.Items"),
            resources.GetString("defaultActionComboBox.Items1"),
            resources.GetString("defaultActionComboBox.Items2")});
            resources.ApplyResources(this.defaultActionComboBox, "defaultActionComboBox");
            this.defaultActionComboBox.Name = "defaultActionComboBox";
            // 
            // defaultActionLabel
            // 
            resources.ApplyResources(this.defaultActionLabel, "defaultActionLabel");
            this.defaultActionLabel.Name = "defaultActionLabel";
            // 
            // DeleteTorrentCheckBox
            // 
            resources.ApplyResources(this.DeleteTorrentCheckBox, "DeleteTorrentCheckBox");
            this.DeleteTorrentCheckBox.Name = "DeleteTorrentCheckBox";
            this.DeleteTorrentCheckBox.UseVisualStyleBackColor = true;
            // 
            // AutoCheckUpdateCheckBox
            // 
            resources.ApplyResources(this.AutoCheckUpdateCheckBox, "AutoCheckUpdateCheckBox");
            this.AutoCheckUpdateCheckBox.Name = "AutoCheckUpdateCheckBox";
            this.AutoCheckUpdateCheckBox.UseVisualStyleBackColor = true;
            // 
            // UploadPromptCheckBox
            // 
            resources.ApplyResources(this.UploadPromptCheckBox, "UploadPromptCheckBox");
            this.UploadPromptCheckBox.Name = "UploadPromptCheckBox";
            this.UploadPromptCheckBox.UseVisualStyleBackColor = true;
            // 
            // tabServersSettings
            // 
            this.tabServersSettings.Controls.Add(this.removeServerButton);
            this.tabServersSettings.Controls.Add(this.addServerButton);
            this.tabServersSettings.Controls.Add(this.tabServerSettings);
            this.tabServersSettings.Controls.Add(this.listServers);
            resources.ApplyResources(this.tabServersSettings, "tabServersSettings");
            this.tabServersSettings.Name = "tabServersSettings";
            this.tabServersSettings.UseVisualStyleBackColor = true;
            // 
            // removeServerButton
            // 
            resources.ApplyResources(this.removeServerButton, "removeServerButton");
            this.removeServerButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.remove16;
            this.removeServerButton.Name = "removeServerButton";
            this.removeServerButton.UseVisualStyleBackColor = true;
            this.removeServerButton.Click += new System.EventHandler(this.removeServerToolStripMenuItem_Click);
            // 
            // addServerButton
            // 
            resources.ApplyResources(this.addServerButton, "addServerButton");
            this.addServerButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.add16;
            this.addServerButton.Name = "addServerButton";
            this.addServerButton.UseVisualStyleBackColor = true;
            this.addServerButton.Click += new System.EventHandler(this.addServerToolStripMenuItem_Click);
            // 
            // tabServerSettings
            // 
            this.tabServerSettings.Controls.Add(this.tabPage1);
            this.tabServerSettings.Controls.Add(this.tabPage2);
            this.tabServerSettings.Controls.Add(this.tabPage3);
            this.tabServerSettings.Controls.Add(this.tabPage5);
            this.tabServerSettings.Controls.Add(this.tabPage6);
            this.tabServerSettings.Controls.Add(this.tabPage7);
            resources.ApplyResources(this.tabServerSettings, "tabServerSettings");
            this.tabServerSettings.Name = "tabServerSettings";
            this.tabServerSettings.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.RefreshRateTrayValue);
            this.tabPage1.Controls.Add(this.SaveServerButton);
            this.tabPage1.Controls.Add(this.UseSSLCheckBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.HostField);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.RetryLimitValue);
            this.tabPage1.Controls.Add(this.PortField);
            this.tabPage1.Controls.Add(this.RefreshRateValue);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // RefreshRateTrayValue
            // 
            resources.ApplyResources(this.RefreshRateTrayValue, "RefreshRateTrayValue");
            this.RefreshRateTrayValue.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.RefreshRateTrayValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RefreshRateTrayValue.Name = "RefreshRateTrayValue";
            this.RefreshRateTrayValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SaveServerButton
            // 
            resources.ApplyResources(this.SaveServerButton, "SaveServerButton");
            this.SaveServerButton.Name = "SaveServerButton";
            this.SaveServerButton.UseVisualStyleBackColor = true;
            this.SaveServerButton.Click += new System.EventHandler(this.SaveServerButton_Click);
            // 
            // UseSSLCheckBox
            // 
            resources.ApplyResources(this.UseSSLCheckBox, "UseSSLCheckBox");
            this.UseSSLCheckBox.Name = "UseSSLCheckBox";
            this.UseSSLCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // HostField
            // 
            resources.ApplyResources(this.HostField, "HostField");
            this.HostField.Name = "HostField";
            this.HostField.TextChanged += new System.EventHandler(this.HostField_TextChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // RetryLimitValue
            // 
            resources.ApplyResources(this.RetryLimitValue, "RetryLimitValue");
            this.RetryLimitValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.RetryLimitValue.Name = "RetryLimitValue";
            // 
            // PortField
            // 
            resources.ApplyResources(this.PortField, "PortField");
            this.PortField.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.PortField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PortField.Name = "PortField";
            this.PortField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // RefreshRateValue
            // 
            resources.ApplyResources(this.RefreshRateValue, "RefreshRateValue");
            this.RefreshRateValue.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.RefreshRateValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RefreshRateValue.Name = "RefreshRateValue";
            this.RefreshRateValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ClearPasswordCheckBox);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.EnableAuthCheckBox);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.PassField);
            this.tabPage2.Controls.Add(this.UserField);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ClearPasswordCheckBox
            // 
            resources.ApplyResources(this.ClearPasswordCheckBox, "ClearPasswordCheckBox");
            this.ClearPasswordCheckBox.Name = "ClearPasswordCheckBox";
            this.ClearPasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // EnableAuthCheckBox
            // 
            resources.ApplyResources(this.EnableAuthCheckBox, "EnableAuthCheckBox");
            this.EnableAuthCheckBox.Name = "EnableAuthCheckBox";
            this.EnableAuthCheckBox.UseVisualStyleBackColor = true;
            this.EnableAuthCheckBox.CheckedChanged += new System.EventHandler(this.EnableAuthCheckBox_CheckedChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // PassField
            // 
            resources.ApplyResources(this.PassField, "PassField");
            this.PassField.Name = "PassField";
            // 
            // UserField
            // 
            resources.ApplyResources(this.UserField, "UserField");
            this.UserField.Name = "UserField";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ClearProxyPasswordCheckBox);
            this.tabPage3.Controls.Add(this.ProxyAuthEnableCheckBox);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.EnableProxyCombo);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.ProxyPortField);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.ProxyHostField);
            this.tabPage3.Controls.Add(this.ProxyUserField);
            this.tabPage3.Controls.Add(this.ProxyPassField);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ClearProxyPasswordCheckBox
            // 
            resources.ApplyResources(this.ClearProxyPasswordCheckBox, "ClearProxyPasswordCheckBox");
            this.ClearProxyPasswordCheckBox.Name = "ClearProxyPasswordCheckBox";
            this.ClearProxyPasswordCheckBox.UseVisualStyleBackColor = true;
            // 
            // ProxyAuthEnableCheckBox
            // 
            resources.ApplyResources(this.ProxyAuthEnableCheckBox, "ProxyAuthEnableCheckBox");
            this.ProxyAuthEnableCheckBox.Name = "ProxyAuthEnableCheckBox";
            this.ProxyAuthEnableCheckBox.UseVisualStyleBackColor = true;
            this.ProxyAuthEnableCheckBox.CheckedChanged += new System.EventHandler(this.ProxyAuthEnableCheckBox_CheckedChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // EnableProxyCombo
            // 
            this.EnableProxyCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EnableProxyCombo.FormattingEnabled = true;
            this.EnableProxyCombo.Items.AddRange(new object[] {
            resources.GetString("EnableProxyCombo.Items"),
            resources.GetString("EnableProxyCombo.Items1"),
            resources.GetString("EnableProxyCombo.Items2")});
            resources.ApplyResources(this.EnableProxyCombo, "EnableProxyCombo");
            this.EnableProxyCombo.Name = "EnableProxyCombo";
            this.EnableProxyCombo.SelectedIndexChanged += new System.EventHandler(this.EnableProxyCombo_SelectedIndexChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // ProxyPortField
            // 
            resources.ApplyResources(this.ProxyPortField, "ProxyPortField");
            this.ProxyPortField.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ProxyPortField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ProxyPortField.Name = "ProxyPortField";
            this.ProxyPortField.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // ProxyHostField
            // 
            resources.ApplyResources(this.ProxyHostField, "ProxyHostField");
            this.ProxyHostField.Name = "ProxyHostField";
            // 
            // ProxyUserField
            // 
            resources.ApplyResources(this.ProxyUserField, "ProxyUserField");
            this.ProxyUserField.Name = "ProxyUserField";
            // 
            // ProxyPassField
            // 
            resources.ApplyResources(this.ProxyPassField, "ProxyPassField");
            this.ProxyPassField.Name = "ProxyPassField";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label15);
            this.tabPage5.Controls.Add(this.linkLabel1);
            this.tabPage5.Controls.Add(this.label13);
            this.tabPage5.Controls.Add(this.PlinkCmdTextBox);
            this.tabPage5.Controls.Add(this.PlinkEnableCheckBox);
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // PlinkCmdTextBox
            // 
            resources.ApplyResources(this.PlinkCmdTextBox, "PlinkCmdTextBox");
            this.PlinkCmdTextBox.Name = "PlinkCmdTextBox";
            // 
            // PlinkEnableCheckBox
            // 
            resources.ApplyResources(this.PlinkEnableCheckBox, "PlinkEnableCheckBox");
            this.PlinkEnableCheckBox.Name = "PlinkEnableCheckBox";
            this.PlinkEnableCheckBox.UseVisualStyleBackColor = true;
            this.PlinkEnableCheckBox.CheckedChanged += new System.EventHandler(this.PlinkEnableCheckBox_CheckedChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.ClearDestPathHistoryButton);
            this.tabPage6.Controls.Add(this.StartPausedCheckBox);
            this.tabPage6.Controls.Add(this.customPathTextBox);
            this.tabPage6.Controls.Add(this.label12);
            this.tabPage6.Controls.Add(this.label17);
            this.tabPage6.Controls.Add(this.label16);
            this.tabPage6.Controls.Add(this.downloadLimitItems);
            this.tabPage6.Controls.Add(this.uploadLimitItems);
            resources.ApplyResources(this.tabPage6, "tabPage6");
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // ClearDestPathHistoryButton
            // 
            resources.ApplyResources(this.ClearDestPathHistoryButton, "ClearDestPathHistoryButton");
            this.ClearDestPathHistoryButton.Name = "ClearDestPathHistoryButton";
            this.ClearDestPathHistoryButton.UseVisualStyleBackColor = true;
            this.ClearDestPathHistoryButton.Click += new System.EventHandler(this.ClearDestPathHistoryButton_Click);
            // 
            // StartPausedCheckBox
            // 
            resources.ApplyResources(this.StartPausedCheckBox, "StartPausedCheckBox");
            this.StartPausedCheckBox.Name = "StartPausedCheckBox";
            this.StartPausedCheckBox.UseVisualStyleBackColor = true;
            // 
            // customPathTextBox
            // 
            resources.ApplyResources(this.customPathTextBox, "customPathTextBox");
            this.customPathTextBox.Name = "customPathTextBox";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // downloadLimitItems
            // 
            resources.ApplyResources(this.downloadLimitItems, "downloadLimitItems");
            this.downloadLimitItems.Name = "downloadLimitItems";
            // 
            // uploadLimitItems
            // 
            resources.ApplyResources(this.uploadLimitItems, "uploadLimitItems");
            this.uploadLimitItems.Name = "uploadLimitItems";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.groupBox1);
            this.tabPage7.Controls.Add(this.RemoveShareButton);
            this.tabPage7.Controls.Add(this.listSambaShareMappings);
            resources.ApplyResources(this.tabPage7, "tabPage7");
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MappingHelpButton);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.AddShareButton);
            this.groupBox1.Controls.Add(this.UnixPathPrefixTextBox);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.SambaShareTextBox);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // MappingHelpButton
            // 
            this.MappingHelpButton.BackgroundImage = global::TransmissionRemoteDotnet.Properties.Resources.help14;
            resources.ApplyResources(this.MappingHelpButton, "MappingHelpButton");
            this.MappingHelpButton.Name = "MappingHelpButton";
            this.MappingHelpButton.TabStop = false;
            this.MappingHelpButton.UseMnemonic = false;
            this.MappingHelpButton.UseVisualStyleBackColor = false;
            this.MappingHelpButton.Click += new System.EventHandler(this.MappingHelpButton_Click);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // AddShareButton
            // 
            resources.ApplyResources(this.AddShareButton, "AddShareButton");
            this.AddShareButton.Name = "AddShareButton";
            this.AddShareButton.UseVisualStyleBackColor = true;
            this.AddShareButton.Click += new System.EventHandler(this.AddShareButton_Click);
            // 
            // UnixPathPrefixTextBox
            // 
            resources.ApplyResources(this.UnixPathPrefixTextBox, "UnixPathPrefixTextBox");
            this.UnixPathPrefixTextBox.Name = "UnixPathPrefixTextBox";
            this.UnixPathPrefixTextBox.TextChanged += new System.EventHandler(this.UnixPathPrefixTextBox_TextChanged);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // SambaShareTextBox
            // 
            resources.ApplyResources(this.SambaShareTextBox, "SambaShareTextBox");
            this.SambaShareTextBox.Name = "SambaShareTextBox";
            this.SambaShareTextBox.TextChanged += new System.EventHandler(this.UnixPathPrefixTextBox_TextChanged);
            // 
            // RemoveShareButton
            // 
            resources.ApplyResources(this.RemoveShareButton, "RemoveShareButton");
            this.RemoveShareButton.Name = "RemoveShareButton";
            this.RemoveShareButton.UseVisualStyleBackColor = true;
            this.RemoveShareButton.Click += new System.EventHandler(this.RemoveShareButton_Click);
            // 
            // listSambaShareMappings
            // 
            this.listSambaShareMappings.FormattingEnabled = true;
            resources.ApplyResources(this.listSambaShareMappings, "listSambaShareMappings");
            this.listSambaShareMappings.Name = "listSambaShareMappings";
            this.listSambaShareMappings.SelectedIndexChanged += new System.EventHandler(this.listSambaShareMappings_SelectedIndexChanged);
            this.listSambaShareMappings.DoubleClick += new System.EventHandler(this.listSambaShareMappings_DoubleClick);
            // 
            // listServers
            // 
            this.listServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnHost,
            this.columnPort});
            this.listServers.ContextMenuStrip = this.ServersMenuStrip;
            this.listServers.FullRowSelect = true;
            this.listServers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listServers.HideSelection = false;
            this.listServers.LabelEdit = true;
            resources.ApplyResources(this.listServers, "listServers");
            this.listServers.MultiSelect = false;
            this.listServers.Name = "listServers";
            this.listServers.ShowGroups = false;
            this.listServers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listServers.UseCompatibleStateImageBehavior = false;
            this.listServers.View = System.Windows.Forms.View.Details;
            this.listServers.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listServers_AfterLabelEdit);
            this.listServers.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listServers_ItemSelectionChanged);
            // 
            // columnName
            // 
            resources.ApplyResources(this.columnName, "columnName");
            // 
            // columnHost
            // 
            resources.ApplyResources(this.columnHost, "columnHost");
            // 
            // columnPort
            // 
            resources.ApplyResources(this.columnPort, "columnPort");
            // 
            // ServersMenuStrip
            // 
            this.ServersMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addServerToolStripMenuItem,
            this.removeServerToolStripMenuItem});
            this.ServersMenuStrip.Name = "ServersMenuStrip";
            resources.ApplyResources(this.ServersMenuStrip, "ServersMenuStrip");
            // 
            // addServerToolStripMenuItem
            // 
            this.addServerToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.add16;
            this.addServerToolStripMenuItem.Name = "addServerToolStripMenuItem";
            resources.ApplyResources(this.addServerToolStripMenuItem, "addServerToolStripMenuItem");
            this.addServerToolStripMenuItem.Click += new System.EventHandler(this.addServerToolStripMenuItem_Click);
            // 
            // removeServerToolStripMenuItem
            // 
            this.removeServerToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.remove16;
            this.removeServerToolStripMenuItem.Name = "removeServerToolStripMenuItem";
            resources.ApplyResources(this.removeServerToolStripMenuItem, "removeServerToolStripMenuItem");
            this.removeServerToolStripMenuItem.Click += new System.EventHandler(this.removeServerToolStripMenuItem_Click);
            // 
            // tabRssSettings
            // 
            this.tabRssSettings.Controls.Add(this.listRssFeeds);
            this.tabRssSettings.Controls.Add(this.groupFeed);
            resources.ApplyResources(this.tabRssSettings, "tabRssSettings");
            this.tabRssSettings.Name = "tabRssSettings";
            this.tabRssSettings.UseVisualStyleBackColor = true;
            // 
            // listRssFeeds
            // 
            this.listRssFeeds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFeedName,
            this.columnFeedUrl});
            resources.ApplyResources(this.listRssFeeds, "listRssFeeds");
            this.listRssFeeds.FullRowSelect = true;
            this.listRssFeeds.HideSelection = false;
            this.listRssFeeds.MultiSelect = false;
            this.listRssFeeds.Name = "listRssFeeds";
            this.listRssFeeds.ShowGroups = false;
            this.listRssFeeds.UseCompatibleStateImageBehavior = false;
            this.listRssFeeds.View = System.Windows.Forms.View.Details;
            this.listRssFeeds.SelectedIndexChanged += new System.EventHandler(this.listRssFeeds_SelectedIndexChanged);
            this.listRssFeeds.DoubleClick += new System.EventHandler(this.listRssFeeds_DoubleClick);
            // 
            // columnFeedName
            // 
            resources.ApplyResources(this.columnFeedName, "columnFeedName");
            // 
            // columnFeedUrl
            // 
            resources.ApplyResources(this.columnFeedUrl, "columnFeedUrl");
            // 
            // groupFeed
            // 
            this.groupFeed.Controls.Add(this.RemoveFeedButton);
            this.groupFeed.Controls.Add(this.AddFeedButton);
            this.groupFeed.Controls.Add(this.FeedNameTextBox);
            this.groupFeed.Controls.Add(this.FeedUrlLabel);
            this.groupFeed.Controls.Add(this.FeedUrlTextBox);
            this.groupFeed.Controls.Add(this.FeedNameLabel);
            resources.ApplyResources(this.groupFeed, "groupFeed");
            this.groupFeed.Name = "groupFeed";
            this.groupFeed.TabStop = false;
            // 
            // RemoveFeedButton
            // 
            resources.ApplyResources(this.RemoveFeedButton, "RemoveFeedButton");
            this.RemoveFeedButton.Name = "RemoveFeedButton";
            this.RemoveFeedButton.UseVisualStyleBackColor = true;
            this.RemoveFeedButton.Click += new System.EventHandler(this.RemoveFeedButton_Click);
            // 
            // AddFeedButton
            // 
            resources.ApplyResources(this.AddFeedButton, "AddFeedButton");
            this.AddFeedButton.Name = "AddFeedButton";
            this.AddFeedButton.UseVisualStyleBackColor = true;
            this.AddFeedButton.Click += new System.EventHandler(this.AddFeedButton_Click);
            // 
            // FeedNameTextBox
            // 
            resources.ApplyResources(this.FeedNameTextBox, "FeedNameTextBox");
            this.FeedNameTextBox.Name = "FeedNameTextBox";
            this.FeedNameTextBox.TextChanged += new System.EventHandler(this.FeedNameTextBox_TextChanged);
            // 
            // FeedUrlLabel
            // 
            resources.ApplyResources(this.FeedUrlLabel, "FeedUrlLabel");
            this.FeedUrlLabel.Name = "FeedUrlLabel";
            // 
            // FeedUrlTextBox
            // 
            resources.ApplyResources(this.FeedUrlTextBox, "FeedUrlTextBox");
            this.FeedUrlTextBox.Name = "FeedUrlTextBox";
            this.FeedUrlTextBox.TextChanged += new System.EventHandler(this.FeedNameTextBox_TextChanged);
            // 
            // FeedNameLabel
            // 
            resources.ApplyResources(this.FeedNameLabel, "FeedNameLabel");
            this.FeedNameLabel.Name = "FeedNameLabel";
            // 
            // tabSkinSettings
            // 
            this.tabSkinSettings.Controls.Add(this.trayImageBrowse);
            this.tabSkinSettings.Controls.Add(this.toolbarImageBrowse);
            this.tabSkinSettings.Controls.Add(this.stateImageBrowse);
            this.tabSkinSettings.Controls.Add(this.infopanelImageBrowse);
            resources.ApplyResources(this.tabSkinSettings, "tabSkinSettings");
            this.tabSkinSettings.Name = "tabSkinSettings";
            this.tabSkinSettings.UseVisualStyleBackColor = true;
            // 
            // trayImageBrowse
            // 
            this.trayImageBrowse.FileName = "";
            this.trayImageBrowse.ImageNumber = 5;
            resources.ApplyResources(this.trayImageBrowse, "trayImageBrowse");
            this.trayImageBrowse.MaxHeight = 48;
            this.trayImageBrowse.MinHeight = 48;
            this.trayImageBrowse.MinimumSize = new System.Drawing.Size(0, 68);
            this.trayImageBrowse.Name = "trayImageBrowse";
            this.trayImageBrowse.Title = "Tray icon Images";
            // 
            // toolbarImageBrowse
            // 
            this.toolbarImageBrowse.FileName = "";
            this.toolbarImageBrowse.ImageNumber = 0;
            resources.ApplyResources(this.toolbarImageBrowse, "toolbarImageBrowse");
            this.toolbarImageBrowse.MaxHeight = 32;
            this.toolbarImageBrowse.MinHeight = 16;
            this.toolbarImageBrowse.MinimumSize = new System.Drawing.Size(0, 76);
            this.toolbarImageBrowse.Name = "toolbarImageBrowse";
            this.toolbarImageBrowse.Title = "Toolbar Images";
            // 
            // stateImageBrowse
            // 
            this.stateImageBrowse.FileName = "";
            this.stateImageBrowse.ImageNumber = 0;
            resources.ApplyResources(this.stateImageBrowse, "stateImageBrowse");
            this.stateImageBrowse.MaxHeight = 16;
            this.stateImageBrowse.MinHeight = 16;
            this.stateImageBrowse.MinimumSize = new System.Drawing.Size(0, 68);
            this.stateImageBrowse.Name = "stateImageBrowse";
            this.stateImageBrowse.Title = "State Images";
            // 
            // infopanelImageBrowse
            // 
            this.infopanelImageBrowse.FileName = "";
            this.infopanelImageBrowse.ImageNumber = 0;
            resources.ApplyResources(this.infopanelImageBrowse, "infopanelImageBrowse");
            this.infopanelImageBrowse.MaxHeight = 16;
            this.infopanelImageBrowse.MinHeight = 16;
            this.infopanelImageBrowse.MinimumSize = new System.Drawing.Size(0, 68);
            this.infopanelImageBrowse.Name = "infopanelImageBrowse";
            this.infopanelImageBrowse.Title = "Torrent Infopanel Images";
            // 
            // openImageFileDialog
            // 
            this.openImageFileDialog.DefaultExt = "*.png";
            this.openImageFileDialog.FileName = "openFileDialog1";
            resources.ApplyResources(this.openImageFileDialog, "openImageFileDialog");
            // 
            // LocalSettingsDialog
            // 
            this.AcceptButton = this.SaveButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseDialogButton;
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.SaveAndConnectButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CloseDialogButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LocalSettingsDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.LocalSettingsDialog_Load);
            this.tabSettings.ResumeLayout(false);
            this.tabGlobalSettings.ResumeLayout(false);
            this.groupAutoConnect.ResumeLayout(false);
            this.groupCurrentProfile.ResumeLayout(false);
            this.groupPlinkPath.ResumeLayout(false);
            this.groupPlinkPath.PerformLayout();
            this.groupBehavior.ResumeLayout(false);
            this.groupBehavior.PerformLayout();
            this.TrayGroupBox.ResumeLayout(false);
            this.TrayGroupBox.PerformLayout();
            this.tabServersSettings.ResumeLayout(false);
            this.tabServerSettings.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshRateTrayValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RetryLimitValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PortField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshRateValue)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyPortField)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ServersMenuStrip.ResumeLayout(false);
            this.tabRssSettings.ResumeLayout(false);
            this.groupFeed.ResumeLayout(false);
            this.groupFeed.PerformLayout();
            this.tabSkinSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CloseDialogButton;
        private System.Windows.Forms.Button SaveAndConnectButton;
        private System.Windows.Forms.OpenFileDialog PlinkPathOpenFileDialog;
        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabGlobalSettings;
        private System.Windows.Forms.TabPage tabServersSettings;
        private System.Windows.Forms.GroupBox groupBehavior;
        private System.Windows.Forms.GroupBox TrayGroupBox;
        private System.Windows.Forms.CheckBox UploadPromptCheckBox;
        private System.Windows.Forms.CheckBox minimizeOnCloseCheckBox;
        private System.Windows.Forms.CheckBox notificationOnAdditionCheckBox;
        private System.Windows.Forms.CheckBox notificationOnCompletionCheckBox;
        private System.Windows.Forms.CheckBox MinToTrayCheckBox;
        private System.Windows.Forms.CheckBox ColorTrayIconCheckBox;
        private System.Windows.Forms.Button PlinkPathButton;
        private System.Windows.Forms.TextBox PlinkPathTextBox;
        private System.Windows.Forms.TabControl tabServerSettings;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox UseSSLCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox HostField;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown RetryLimitValue;
        private System.Windows.Forms.NumericUpDown PortField;
        private System.Windows.Forms.NumericUpDown RefreshRateValue;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown RefreshRateTrayValue;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox EnableAuthCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PassField;
        private System.Windows.Forms.TextBox UserField;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox ProxyAuthEnableCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox EnableProxyCombo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown ProxyPortField;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ProxyHostField;
        private System.Windows.Forms.TextBox ProxyUserField;
        private System.Windows.Forms.TextBox ProxyPassField;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox PlinkCmdTextBox;
        private System.Windows.Forms.CheckBox PlinkEnableCheckBox;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox downloadLimitItems;
        private System.Windows.Forms.TextBox uploadLimitItems;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button AddShareButton;
        private System.Windows.Forms.TextBox UnixPathPrefixTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox SambaShareTextBox;
        private System.Windows.Forms.Button RemoveShareButton;
        private System.Windows.Forms.ListBox listSambaShareMappings;
        private System.Windows.Forms.ListView listServers;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnHost;
        private System.Windows.Forms.ColumnHeader columnPort;
        private System.Windows.Forms.GroupBox groupPlinkPath;
        private System.Windows.Forms.GroupBox groupCurrentProfile;
        private System.Windows.Forms.GroupBox groupAutoConnect;
        private System.Windows.Forms.ComboBox AutoConnectComboBox;
        private System.Windows.Forms.ComboBox CurrentProfileComboBox;
        private System.Windows.Forms.Button SaveServerButton;
        private System.Windows.Forms.ContextMenuStrip ServersMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeServerToolStripMenuItem;
        private System.Windows.Forms.Button addServerButton;
        private System.Windows.Forms.Button removeServerButton;
        private System.Windows.Forms.CheckBox StartPausedCheckBox;
        private System.Windows.Forms.CheckBox AutoCheckUpdateCheckBox;
        private System.Windows.Forms.CheckBox UpdateToBetaCheckBox;
        private System.Windows.Forms.CheckBox AutoUpdateGeoipCheckBox;
        private System.Windows.Forms.CheckBox DeleteTorrentCheckBox;
        private System.Windows.Forms.ComboBox defaultActionComboBox;
        private System.Windows.Forms.Label defaultActionLabel;
        private System.Windows.Forms.CheckBox DontSavePasswordsCheckBox;
        private System.Windows.Forms.CheckBox ClearPasswordCheckBox;
        private System.Windows.Forms.CheckBox ClearProxyPasswordCheckBox;
        private System.Windows.Forms.TextBox customPathTextBox;
        private System.Windows.Forms.Label label12;
        private TransmissionRemoteDotnet.CustomControls.USButton MappingHelpButton;
        private System.Windows.Forms.TabPage tabSkinSettings;
        private System.Windows.Forms.OpenFileDialog openImageFileDialog;
        private TransmissionRemoteDotnet.CustomControls.SkinImageBrowseTextBox stateImageBrowse;
        private TransmissionRemoteDotnet.CustomControls.SkinImageBrowseTextBox infopanelImageBrowse;
        private TransmissionRemoteDotnet.CustomControls.SkinImageBrowseTextBox toolbarImageBrowse;
        private TransmissionRemoteDotnet.CustomControls.SkinImageBrowseTextBox trayImageBrowse;
        private System.Windows.Forms.ListView listRssFeeds;
        private System.Windows.Forms.ColumnHeader columnFeedName;
        private System.Windows.Forms.ColumnHeader columnFeedUrl;
        private System.Windows.Forms.TabPage tabRssSettings;
        private System.Windows.Forms.GroupBox groupFeed;
        private System.Windows.Forms.Label FeedNameLabel;
        private System.Windows.Forms.TextBox FeedNameTextBox;
        private System.Windows.Forms.Label FeedUrlLabel;
        private System.Windows.Forms.TextBox FeedUrlTextBox;
        private System.Windows.Forms.Button AddFeedButton;
        private System.Windows.Forms.Button RemoveFeedButton;
        private System.Windows.Forms.CheckBox StartOnSystemCheckBox;
        private System.Windows.Forms.Button ClearDestPathHistoryButton;
    }
}