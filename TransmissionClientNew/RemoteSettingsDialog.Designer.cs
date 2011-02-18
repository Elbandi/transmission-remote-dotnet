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
    partial class RemoteSettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteSettingsDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.downloadToField = new System.Windows.Forms.TextBox();
            this.limitUploadCheckBox = new System.Windows.Forms.CheckBox();
            this.limitUploadValue = new System.Windows.Forms.NumericUpDown();
            this.limitDownloadCheckBox = new System.Windows.Forms.CheckBox();
            this.limitDownloadValue = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.incomingPortValue = new System.Windows.Forms.NumericUpDown();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CloseFormButton = new System.Windows.Forms.Button();
            this.peerLimitValue = new System.Windows.Forms.NumericUpDown();
            this.pexEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.portForwardCheckBox = new System.Windows.Forms.CheckBox();
            this.encryptionCombobox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.cacheSizeUnitLabel = new System.Windows.Forms.Label();
            this.blocklistUrlField = new System.Windows.Forms.TextBox();
            this.blocklistUrlLabel = new System.Windows.Forms.Label();
            this.cacheSizeLabel = new System.Windows.Forms.Label();
            this.cacheSizeValue = new System.Windows.Forms.NumericUpDown();
            this.watchdirCheckBox = new System.Windows.Forms.CheckBox();
            this.watchdirField = new System.Windows.Forms.TextBox();
            this.renamePartialFilesCheckBox = new System.Windows.Forms.CheckBox();
            this.incompleteToCheckBox = new System.Windows.Forms.CheckBox();
            this.incompleteToField = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.updateBlocklistButton = new System.Windows.Forms.Button();
            this.blocklistEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.tabNetwork = new System.Windows.Forms.TabPage();
            this.utpEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.lpdEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.dhtEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.testPortButton = new System.Windows.Forms.Button();
            this.tabLimits = new System.Windows.Forms.TabPage();
            this.seedIdleLimitUpDown = new System.Windows.Forms.NumericUpDown();
            this.seedIdleEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.peerLimitTorrentValue = new System.Windows.Forms.NumericUpDown();
            this.seedLimitUpDown = new System.Windows.Forms.NumericUpDown();
            this.seedRatioEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.tabAltLimits = new System.Windows.Forms.TabPage();
            this.timeConstaintEndMinutes = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.timeConstaintBeginMinutes = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.timeConstraintEndHours = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.timeConstraintBeginHours = new System.Windows.Forms.NumericUpDown();
            this.altUploadLimitField = new System.Windows.Forms.NumericUpDown();
            this.altDownloadLimitField = new System.Windows.Forms.NumericUpDown();
            this.altTimeConstraintEnabled = new System.Windows.Forms.CheckBox();
            this.altSpeedLimitEnable = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.limitUploadValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.limitDownloadValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.incomingPortValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peerLimitValue)).BeginInit();
            this.tabSettings.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cacheSizeValue)).BeginInit();
            this.tabNetwork.SuspendLayout();
            this.tabLimits.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seedIdleLimitUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peerLimitTorrentValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seedLimitUpDown)).BeginInit();
            this.tabAltLimits.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeConstaintEndMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeConstaintBeginMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeConstraintEndHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeConstraintBeginHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.altUploadLimitField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.altDownloadLimitField)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // downloadToField
            // 
            resources.ApplyResources(this.downloadToField, "downloadToField");
            this.downloadToField.Name = "downloadToField";
            // 
            // limitUploadCheckBox
            // 
            resources.ApplyResources(this.limitUploadCheckBox, "limitUploadCheckBox");
            this.limitUploadCheckBox.Name = "limitUploadCheckBox";
            this.limitUploadCheckBox.UseVisualStyleBackColor = true;
            this.limitUploadCheckBox.CheckedChanged += new System.EventHandler(this.LimitUploadCheckBox_CheckedChanged);
            // 
            // limitUploadValue
            // 
            resources.ApplyResources(this.limitUploadValue, "limitUploadValue");
            this.limitUploadValue.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.limitUploadValue.Name = "limitUploadValue";
            // 
            // limitDownloadCheckBox
            // 
            resources.ApplyResources(this.limitDownloadCheckBox, "limitDownloadCheckBox");
            this.limitDownloadCheckBox.Name = "limitDownloadCheckBox";
            this.limitDownloadCheckBox.UseVisualStyleBackColor = true;
            this.limitDownloadCheckBox.CheckedChanged += new System.EventHandler(this.LimitDownloadCheckBox_CheckedChanged);
            // 
            // limitDownloadValue
            // 
            resources.ApplyResources(this.limitDownloadValue, "limitDownloadValue");
            this.limitDownloadValue.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.limitDownloadValue.Name = "limitDownloadValue";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // incomingPortValue
            // 
            resources.ApplyResources(this.incomingPortValue, "incomingPortValue");
            this.incomingPortValue.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.incomingPortValue.Name = "incomingPortValue";
            // 
            // SaveButton
            // 
            resources.ApplyResources(this.SaveButton, "SaveButton");
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseFormButton
            // 
            this.CloseFormButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.CloseFormButton, "CloseFormButton");
            this.CloseFormButton.Name = "CloseFormButton";
            this.CloseFormButton.UseVisualStyleBackColor = true;
            this.CloseFormButton.Click += new System.EventHandler(this.CloseFormButton_Click);
            // 
            // peerLimitValue
            // 
            resources.ApplyResources(this.peerLimitValue, "peerLimitValue");
            this.peerLimitValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.peerLimitValue.Name = "peerLimitValue";
            // 
            // pexEnabledCheckBox
            // 
            resources.ApplyResources(this.pexEnabledCheckBox, "pexEnabledCheckBox");
            this.pexEnabledCheckBox.Name = "pexEnabledCheckBox";
            this.pexEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // portForwardCheckBox
            // 
            resources.ApplyResources(this.portForwardCheckBox, "portForwardCheckBox");
            this.portForwardCheckBox.Name = "portForwardCheckBox";
            this.portForwardCheckBox.UseVisualStyleBackColor = true;
            // 
            // encryptionCombobox
            // 
            this.encryptionCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encryptionCombobox.FormattingEnabled = true;
            this.encryptionCombobox.Items.AddRange(new object[] {
            resources.GetString("encryptionCombobox.Items"),
            resources.GetString("encryptionCombobox.Items1"),
            resources.GetString("encryptionCombobox.Items2")});
            resources.ApplyResources(this.encryptionCombobox, "encryptionCombobox");
            this.encryptionCombobox.Name = "encryptionCombobox";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabGeneral);
            this.tabSettings.Controls.Add(this.tabNetwork);
            this.tabSettings.Controls.Add(this.tabLimits);
            this.tabSettings.Controls.Add(this.tabAltLimits);
            resources.ApplyResources(this.tabSettings, "tabSettings");
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.cacheSizeUnitLabel);
            this.tabGeneral.Controls.Add(this.blocklistUrlField);
            this.tabGeneral.Controls.Add(this.blocklistUrlLabel);
            this.tabGeneral.Controls.Add(this.cacheSizeLabel);
            this.tabGeneral.Controls.Add(this.cacheSizeValue);
            this.tabGeneral.Controls.Add(this.watchdirCheckBox);
            this.tabGeneral.Controls.Add(this.watchdirField);
            this.tabGeneral.Controls.Add(this.renamePartialFilesCheckBox);
            this.tabGeneral.Controls.Add(this.incompleteToCheckBox);
            this.tabGeneral.Controls.Add(this.incompleteToField);
            this.tabGeneral.Controls.Add(this.label15);
            this.tabGeneral.Controls.Add(this.updateBlocklistButton);
            this.tabGeneral.Controls.Add(this.blocklistEnabledCheckBox);
            this.tabGeneral.Controls.Add(this.label1);
            this.tabGeneral.Controls.Add(this.downloadToField);
            resources.ApplyResources(this.tabGeneral, "tabGeneral");
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // cacheSizeUnitLabel
            // 
            resources.ApplyResources(this.cacheSizeUnitLabel, "cacheSizeUnitLabel");
            this.cacheSizeUnitLabel.Name = "cacheSizeUnitLabel";
            // 
            // blocklistUrlField
            // 
            resources.ApplyResources(this.blocklistUrlField, "blocklistUrlField");
            this.blocklistUrlField.Name = "blocklistUrlField";
            // 
            // blocklistUrlLabel
            // 
            resources.ApplyResources(this.blocklistUrlLabel, "blocklistUrlLabel");
            this.blocklistUrlLabel.Name = "blocklistUrlLabel";
            // 
            // cacheSizeLabel
            // 
            resources.ApplyResources(this.cacheSizeLabel, "cacheSizeLabel");
            this.cacheSizeLabel.Name = "cacheSizeLabel";
            // 
            // cacheSizeValue
            // 
            resources.ApplyResources(this.cacheSizeValue, "cacheSizeValue");
            this.cacheSizeValue.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.cacheSizeValue.Name = "cacheSizeValue";
            // 
            // watchdirCheckBox
            // 
            resources.ApplyResources(this.watchdirCheckBox, "watchdirCheckBox");
            this.watchdirCheckBox.Name = "watchdirCheckBox";
            this.watchdirCheckBox.UseVisualStyleBackColor = true;
            this.watchdirCheckBox.CheckedChanged += new System.EventHandler(this.watchdirCheckBox_CheckedChanged);
            // 
            // watchdirField
            // 
            resources.ApplyResources(this.watchdirField, "watchdirField");
            this.watchdirField.Name = "watchdirField";
            // 
            // renamePartialFilesCheckBox
            // 
            resources.ApplyResources(this.renamePartialFilesCheckBox, "renamePartialFilesCheckBox");
            this.renamePartialFilesCheckBox.Name = "renamePartialFilesCheckBox";
            this.renamePartialFilesCheckBox.UseVisualStyleBackColor = true;
            // 
            // incompleteToCheckBox
            // 
            resources.ApplyResources(this.incompleteToCheckBox, "incompleteToCheckBox");
            this.incompleteToCheckBox.Name = "incompleteToCheckBox";
            this.incompleteToCheckBox.UseVisualStyleBackColor = true;
            this.incompleteToCheckBox.CheckedChanged += new System.EventHandler(this.incompleteToCheckBox_CheckedChanged);
            // 
            // incompleteToField
            // 
            resources.ApplyResources(this.incompleteToField, "incompleteToField");
            this.incompleteToField.Name = "incompleteToField";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // updateBlocklistButton
            // 
            resources.ApplyResources(this.updateBlocklistButton, "updateBlocklistButton");
            this.updateBlocklistButton.Name = "updateBlocklistButton";
            this.updateBlocklistButton.UseVisualStyleBackColor = true;
            this.updateBlocklistButton.Click += new System.EventHandler(this.updateBlocklistButton_Click);
            // 
            // blocklistEnabledCheckBox
            // 
            resources.ApplyResources(this.blocklistEnabledCheckBox, "blocklistEnabledCheckBox");
            this.blocklistEnabledCheckBox.Name = "blocklistEnabledCheckBox";
            this.blocklistEnabledCheckBox.UseVisualStyleBackColor = true;
            this.blocklistEnabledCheckBox.CheckedChanged += new System.EventHandler(this.blocklistEnabledCheckBox_CheckedChanged);
            // 
            // tabNetwork
            // 
            this.tabNetwork.Controls.Add(this.utpEnabledCheckBox);
            this.tabNetwork.Controls.Add(this.lpdEnabledCheckBox);
            this.tabNetwork.Controls.Add(this.dhtEnabledCheckBox);
            this.tabNetwork.Controls.Add(this.testPortButton);
            this.tabNetwork.Controls.Add(this.pexEnabledCheckBox);
            this.tabNetwork.Controls.Add(this.label2);
            this.tabNetwork.Controls.Add(this.incomingPortValue);
            this.tabNetwork.Controls.Add(this.portForwardCheckBox);
            this.tabNetwork.Controls.Add(this.encryptionCombobox);
            this.tabNetwork.Controls.Add(this.label3);
            resources.ApplyResources(this.tabNetwork, "tabNetwork");
            this.tabNetwork.Name = "tabNetwork";
            this.tabNetwork.UseVisualStyleBackColor = true;
            // 
            // utpEnabledCheckBox
            // 
            resources.ApplyResources(this.utpEnabledCheckBox, "utpEnabledCheckBox");
            this.utpEnabledCheckBox.Name = "utpEnabledCheckBox";
            this.utpEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // lpdEnabledCheckBox
            // 
            resources.ApplyResources(this.lpdEnabledCheckBox, "lpdEnabledCheckBox");
            this.lpdEnabledCheckBox.Name = "lpdEnabledCheckBox";
            this.lpdEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // dhtEnabledCheckBox
            // 
            resources.ApplyResources(this.dhtEnabledCheckBox, "dhtEnabledCheckBox");
            this.dhtEnabledCheckBox.Name = "dhtEnabledCheckBox";
            this.dhtEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // testPortButton
            // 
            resources.ApplyResources(this.testPortButton, "testPortButton");
            this.testPortButton.Name = "testPortButton";
            this.testPortButton.UseVisualStyleBackColor = true;
            this.testPortButton.Click += new System.EventHandler(this.testPortButton_Click);
            // 
            // tabLimits
            // 
            this.tabLimits.Controls.Add(this.seedIdleLimitUpDown);
            this.tabLimits.Controls.Add(this.seedIdleEnabledCheckBox);
            this.tabLimits.Controls.Add(this.label16);
            this.tabLimits.Controls.Add(this.peerLimitTorrentValue);
            this.tabLimits.Controls.Add(this.seedLimitUpDown);
            this.tabLimits.Controls.Add(this.seedRatioEnabledCheckBox);
            this.tabLimits.Controls.Add(this.label6);
            this.tabLimits.Controls.Add(this.limitUploadCheckBox);
            this.tabLimits.Controls.Add(this.peerLimitValue);
            this.tabLimits.Controls.Add(this.label5);
            this.tabLimits.Controls.Add(this.limitUploadValue);
            this.tabLimits.Controls.Add(this.limitDownloadValue);
            this.tabLimits.Controls.Add(this.label4);
            this.tabLimits.Controls.Add(this.limitDownloadCheckBox);
            resources.ApplyResources(this.tabLimits, "tabLimits");
            this.tabLimits.Name = "tabLimits";
            this.tabLimits.UseVisualStyleBackColor = true;
            // 
            // seedIdleLimitUpDown
            // 
            this.seedIdleLimitUpDown.DecimalPlaces = 2;
            resources.ApplyResources(this.seedIdleLimitUpDown, "seedIdleLimitUpDown");
            this.seedIdleLimitUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.seedIdleLimitUpDown.Name = "seedIdleLimitUpDown";
            // 
            // seedIdleEnabledCheckBox
            // 
            resources.ApplyResources(this.seedIdleEnabledCheckBox, "seedIdleEnabledCheckBox");
            this.seedIdleEnabledCheckBox.Name = "seedIdleEnabledCheckBox";
            this.seedIdleEnabledCheckBox.UseVisualStyleBackColor = true;
            this.seedIdleEnabledCheckBox.CheckedChanged += new System.EventHandler(this.seedIdleEnabledCheckBox_CheckedChanged);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // peerLimitTorrentValue
            // 
            resources.ApplyResources(this.peerLimitTorrentValue, "peerLimitTorrentValue");
            this.peerLimitTorrentValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.peerLimitTorrentValue.Name = "peerLimitTorrentValue";
            // 
            // seedLimitUpDown
            // 
            this.seedLimitUpDown.DecimalPlaces = 2;
            resources.ApplyResources(this.seedLimitUpDown, "seedLimitUpDown");
            this.seedLimitUpDown.Name = "seedLimitUpDown";
            // 
            // seedRatioEnabledCheckBox
            // 
            resources.ApplyResources(this.seedRatioEnabledCheckBox, "seedRatioEnabledCheckBox");
            this.seedRatioEnabledCheckBox.Name = "seedRatioEnabledCheckBox";
            this.seedRatioEnabledCheckBox.UseVisualStyleBackColor = true;
            this.seedRatioEnabledCheckBox.CheckedChanged += new System.EventHandler(this.seedRatioEnabledCheckBox_CheckedChanged);
            // 
            // tabAltLimits
            // 
            this.tabAltLimits.Controls.Add(this.timeConstaintEndMinutes);
            this.tabAltLimits.Controls.Add(this.label14);
            this.tabAltLimits.Controls.Add(this.label13);
            this.tabAltLimits.Controls.Add(this.timeConstaintBeginMinutes);
            this.tabAltLimits.Controls.Add(this.label12);
            this.tabAltLimits.Controls.Add(this.label11);
            this.tabAltLimits.Controls.Add(this.label9);
            this.tabAltLimits.Controls.Add(this.label10);
            this.tabAltLimits.Controls.Add(this.timeConstraintEndHours);
            this.tabAltLimits.Controls.Add(this.label8);
            this.tabAltLimits.Controls.Add(this.timeConstraintBeginHours);
            this.tabAltLimits.Controls.Add(this.altUploadLimitField);
            this.tabAltLimits.Controls.Add(this.altDownloadLimitField);
            this.tabAltLimits.Controls.Add(this.altTimeConstraintEnabled);
            this.tabAltLimits.Controls.Add(this.altSpeedLimitEnable);
            resources.ApplyResources(this.tabAltLimits, "tabAltLimits");
            this.tabAltLimits.Name = "tabAltLimits";
            this.tabAltLimits.UseVisualStyleBackColor = true;
            // 
            // timeConstaintEndMinutes
            // 
            resources.ApplyResources(this.timeConstaintEndMinutes, "timeConstaintEndMinutes");
            this.timeConstaintEndMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.timeConstaintEndMinutes.Name = "timeConstaintEndMinutes";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // timeConstaintBeginMinutes
            // 
            resources.ApplyResources(this.timeConstaintBeginMinutes, "timeConstaintBeginMinutes");
            this.timeConstaintBeginMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.timeConstaintBeginMinutes.Name = "timeConstaintBeginMinutes";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // timeConstraintEndHours
            // 
            resources.ApplyResources(this.timeConstraintEndHours, "timeConstraintEndHours");
            this.timeConstraintEndHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.timeConstraintEndHours.Name = "timeConstraintEndHours";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // timeConstraintBeginHours
            // 
            resources.ApplyResources(this.timeConstraintBeginHours, "timeConstraintBeginHours");
            this.timeConstraintBeginHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.timeConstraintBeginHours.Name = "timeConstraintBeginHours";
            // 
            // altUploadLimitField
            // 
            resources.ApplyResources(this.altUploadLimitField, "altUploadLimitField");
            this.altUploadLimitField.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.altUploadLimitField.Name = "altUploadLimitField";
            // 
            // altDownloadLimitField
            // 
            resources.ApplyResources(this.altDownloadLimitField, "altDownloadLimitField");
            this.altDownloadLimitField.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.altDownloadLimitField.Name = "altDownloadLimitField";
            // 
            // altTimeConstraintEnabled
            // 
            resources.ApplyResources(this.altTimeConstraintEnabled, "altTimeConstraintEnabled");
            this.altTimeConstraintEnabled.Name = "altTimeConstraintEnabled";
            this.altTimeConstraintEnabled.UseVisualStyleBackColor = true;
            this.altTimeConstraintEnabled.CheckedChanged += new System.EventHandler(this.altTimeConstraintEnabled_CheckedChanged);
            // 
            // altSpeedLimitEnable
            // 
            resources.ApplyResources(this.altSpeedLimitEnable, "altSpeedLimitEnable");
            this.altSpeedLimitEnable.Name = "altSpeedLimitEnable";
            this.altSpeedLimitEnable.UseVisualStyleBackColor = true;
            this.altSpeedLimitEnable.CheckedChanged += new System.EventHandler(this.altSpeedLimitEnable_CheckedChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // RemoteSettingsDialog
            // 
            this.AcceptButton = this.SaveButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseFormButton;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.CloseFormButton);
            this.Controls.Add(this.SaveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "RemoteSettingsDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.RemoteSettingsDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.limitUploadValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.limitDownloadValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.incomingPortValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peerLimitValue)).EndInit();
            this.tabSettings.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cacheSizeValue)).EndInit();
            this.tabNetwork.ResumeLayout(false);
            this.tabNetwork.PerformLayout();
            this.tabLimits.ResumeLayout(false);
            this.tabLimits.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seedIdleLimitUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peerLimitTorrentValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seedLimitUpDown)).EndInit();
            this.tabAltLimits.ResumeLayout(false);
            this.tabAltLimits.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeConstaintEndMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeConstaintBeginMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeConstraintEndHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeConstraintBeginHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.altUploadLimitField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.altDownloadLimitField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox downloadToField;
        private System.Windows.Forms.CheckBox incompleteToCheckBox;
        private System.Windows.Forms.TextBox incompleteToField;
        private System.Windows.Forms.TextBox watchdirField;
        private System.Windows.Forms.CheckBox watchdirCheckBox;
        private System.Windows.Forms.Label cacheSizeLabel;
        private System.Windows.Forms.NumericUpDown cacheSizeValue;
        private System.Windows.Forms.Label cacheSizeUnitLabel;
        private System.Windows.Forms.CheckBox limitUploadCheckBox;
        private System.Windows.Forms.NumericUpDown limitUploadValue;
        private System.Windows.Forms.CheckBox limitDownloadCheckBox;
        private System.Windows.Forms.NumericUpDown limitDownloadValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown incomingPortValue;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CloseFormButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox encryptionCombobox;
        private System.Windows.Forms.CheckBox portForwardCheckBox;
        private System.Windows.Forms.CheckBox renamePartialFilesCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox pexEnabledCheckBox;
        private System.Windows.Forms.NumericUpDown peerLimitValue;
        private System.Windows.Forms.NumericUpDown peerLimitTorrentValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabLimits;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabAltLimits;
        private System.Windows.Forms.CheckBox altTimeConstraintEnabled;
        private System.Windows.Forms.CheckBox altSpeedLimitEnable;
        private System.Windows.Forms.NumericUpDown timeConstraintEndHours;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown timeConstraintBeginHours;
        private System.Windows.Forms.NumericUpDown altUploadLimitField;
        private System.Windows.Forms.NumericUpDown altDownloadLimitField;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox blocklistEnabledCheckBox;
        private System.Windows.Forms.TextBox blocklistUrlField;
        private System.Windows.Forms.Label blocklistUrlLabel;
        private System.Windows.Forms.Button updateBlocklistButton;
        private System.Windows.Forms.NumericUpDown seedLimitUpDown;
        private System.Windows.Forms.CheckBox seedRatioEnabledCheckBox;
        private System.Windows.Forms.CheckBox seedIdleEnabledCheckBox;
        private System.Windows.Forms.NumericUpDown seedIdleLimitUpDown;
        private System.Windows.Forms.Button testPortButton;
        private System.Windows.Forms.NumericUpDown timeConstaintEndMinutes;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown timeConstaintBeginMinutes;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox dhtEnabledCheckBox;
        private System.Windows.Forms.CheckBox lpdEnabledCheckBox;
        private System.Windows.Forms.CheckBox utpEnabledCheckBox;
        private System.Windows.Forms.TabPage tabNetwork;
    }
}