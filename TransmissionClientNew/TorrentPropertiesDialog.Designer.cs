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
    partial class TorrentPropertiesDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TorrentPropertiesDialog));
            this.tabProperties = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.seedIdleLimitValue = new System.Windows.Forms.NumericUpDown();
            this.seedIdleLimitedCheckBox = new System.Windows.Forms.CheckBox();
            this.bandwidthComboBox = new System.Windows.Forms.ComboBox();
            this.bandwidthLabel = new System.Windows.Forms.Label();
            this.honorsSessionLimits = new System.Windows.Forms.CheckBox();
            this.seedRatioLimitedCheckBox = new System.Windows.Forms.CheckBox();
            this.seedRatioLimitValue = new System.Windows.Forms.NumericUpDown();
            this.peerLimitValue = new System.Windows.Forms.NumericUpDown();
            this.peerLimitLabel = new System.Windows.Forms.Label();
            this.uploadLimitEnableField = new System.Windows.Forms.CheckBox();
            this.downloadLimitField = new System.Windows.Forms.NumericUpDown();
            this.uploadLimitUnitLabel = new System.Windows.Forms.Label();
            this.uploadLimitField = new System.Windows.Forms.NumericUpDown();
            this.downloadLimitUnitLabel = new System.Windows.Forms.Label();
            this.downloadLimitEnableField = new System.Windows.Forms.CheckBox();
            this.tabTrackers = new System.Windows.Forms.TabPage();
            this.trackersList = new System.Windows.Forms.RefreshingListBox();
            this.trackersButtonPanel = new System.Windows.Forms.Panel();
            this.removeTrackerButton = new System.Windows.Forms.Button();
            this.editTrackerButton = new System.Windows.Forms.Button();
            this.addTrackerButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.noteLabel = new System.Windows.Forms.Label();
            this.tabProperties.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seedIdleLimitValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seedRatioLimitValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peerLimitValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.downloadLimitField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uploadLimitField)).BeginInit();
            this.tabTrackers.SuspendLayout();
            this.trackersButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabProperties
            // 
            this.tabProperties.Controls.Add(this.tabGeneral);
            this.tabProperties.Controls.Add(this.tabTrackers);
            resources.ApplyResources(this.tabProperties, "tabProperties");
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.SelectedIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.seedIdleLimitValue);
            this.tabGeneral.Controls.Add(this.seedIdleLimitedCheckBox);
            this.tabGeneral.Controls.Add(this.bandwidthComboBox);
            this.tabGeneral.Controls.Add(this.bandwidthLabel);
            this.tabGeneral.Controls.Add(this.honorsSessionLimits);
            this.tabGeneral.Controls.Add(this.seedRatioLimitedCheckBox);
            this.tabGeneral.Controls.Add(this.seedRatioLimitValue);
            this.tabGeneral.Controls.Add(this.peerLimitValue);
            this.tabGeneral.Controls.Add(this.peerLimitLabel);
            this.tabGeneral.Controls.Add(this.uploadLimitEnableField);
            this.tabGeneral.Controls.Add(this.downloadLimitField);
            this.tabGeneral.Controls.Add(this.uploadLimitUnitLabel);
            this.tabGeneral.Controls.Add(this.uploadLimitField);
            this.tabGeneral.Controls.Add(this.downloadLimitUnitLabel);
            this.tabGeneral.Controls.Add(this.downloadLimitEnableField);
            resources.ApplyResources(this.tabGeneral, "tabGeneral");
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // seedIdleLimitValue
            // 
            resources.ApplyResources(this.seedIdleLimitValue, "seedIdleLimitValue");
            this.seedIdleLimitValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.seedIdleLimitValue.Name = "seedIdleLimitValue";
            // 
            // seedIdleLimitedCheckBox
            // 
            resources.ApplyResources(this.seedIdleLimitedCheckBox, "seedIdleLimitedCheckBox");
            this.seedIdleLimitedCheckBox.Name = "seedIdleLimitedCheckBox";
            this.seedIdleLimitedCheckBox.ThreeState = true;
            this.seedIdleLimitedCheckBox.UseVisualStyleBackColor = true;
            this.seedIdleLimitedCheckBox.CheckStateChanged += new System.EventHandler(this.seedIdleLimitedCheckBox_CheckStateChanged);
            // 
            // bandwidthComboBox
            // 
            this.bandwidthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bandwidthComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.bandwidthComboBox, "bandwidthComboBox");
            this.bandwidthComboBox.Name = "bandwidthComboBox";
            // 
            // bandwidthLabel
            // 
            resources.ApplyResources(this.bandwidthLabel, "bandwidthLabel");
            this.bandwidthLabel.Name = "bandwidthLabel";
            // 
            // honorsSessionLimits
            // 
            resources.ApplyResources(this.honorsSessionLimits, "honorsSessionLimits");
            this.honorsSessionLimits.Name = "honorsSessionLimits";
            this.honorsSessionLimits.UseVisualStyleBackColor = true;
            // 
            // seedRatioLimitedCheckBox
            // 
            resources.ApplyResources(this.seedRatioLimitedCheckBox, "seedRatioLimitedCheckBox");
            this.seedRatioLimitedCheckBox.Name = "seedRatioLimitedCheckBox";
            this.seedRatioLimitedCheckBox.ThreeState = true;
            this.seedRatioLimitedCheckBox.UseVisualStyleBackColor = true;
            this.seedRatioLimitedCheckBox.CheckStateChanged += new System.EventHandler(this.seedRatioLimitedCheckBox_CheckStateChanged);
            // 
            // seedRatioLimitValue
            // 
            this.seedRatioLimitValue.DecimalPlaces = 2;
            resources.ApplyResources(this.seedRatioLimitValue, "seedRatioLimitValue");
            this.seedRatioLimitValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.seedRatioLimitValue.Name = "seedRatioLimitValue";
            // 
            // peerLimitValue
            // 
            resources.ApplyResources(this.peerLimitValue, "peerLimitValue");
            this.peerLimitValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.peerLimitValue.Name = "peerLimitValue";
            // 
            // peerLimitLabel
            // 
            resources.ApplyResources(this.peerLimitLabel, "peerLimitLabel");
            this.peerLimitLabel.Name = "peerLimitLabel";
            // 
            // uploadLimitEnableField
            // 
            resources.ApplyResources(this.uploadLimitEnableField, "uploadLimitEnableField");
            this.uploadLimitEnableField.Name = "uploadLimitEnableField";
            this.uploadLimitEnableField.UseVisualStyleBackColor = true;
            this.uploadLimitEnableField.CheckedChanged += new System.EventHandler(this.uploadLimitEnableField_CheckedChanged);
            // 
            // downloadLimitField
            // 
            resources.ApplyResources(this.downloadLimitField, "downloadLimitField");
            this.downloadLimitField.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.downloadLimitField.Name = "downloadLimitField";
            // 
            // uploadLimitUnitLabel
            // 
            resources.ApplyResources(this.uploadLimitUnitLabel, "uploadLimitUnitLabel");
            this.uploadLimitUnitLabel.Name = "uploadLimitUnitLabel";
            // 
            // uploadLimitField
            // 
            resources.ApplyResources(this.uploadLimitField, "uploadLimitField");
            this.uploadLimitField.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.uploadLimitField.Name = "uploadLimitField";
            // 
            // downloadLimitUnitLabel
            // 
            resources.ApplyResources(this.downloadLimitUnitLabel, "downloadLimitUnitLabel");
            this.downloadLimitUnitLabel.Name = "downloadLimitUnitLabel";
            // 
            // downloadLimitEnableField
            // 
            resources.ApplyResources(this.downloadLimitEnableField, "downloadLimitEnableField");
            this.downloadLimitEnableField.Name = "downloadLimitEnableField";
            this.downloadLimitEnableField.UseVisualStyleBackColor = true;
            this.downloadLimitEnableField.CheckedChanged += new System.EventHandler(this.downloadLimitEnableField_CheckedChanged);
            // 
            // tabTrackers
            // 
            this.tabTrackers.Controls.Add(this.trackersList);
            this.tabTrackers.Controls.Add(this.trackersButtonPanel);
            resources.ApplyResources(this.tabTrackers, "tabTrackers");
            this.tabTrackers.Name = "tabTrackers";
            this.tabTrackers.UseVisualStyleBackColor = true;
            // 
            // trackersList
            // 
            resources.ApplyResources(this.trackersList, "trackersList");
            this.trackersList.Name = "trackersList";
            this.trackersList.SelectedIndexChanged += new System.EventHandler(this.trackersList_SelectedIndexChanged);
            this.trackersList.DoubleClick += new System.EventHandler(this.trackersList_DoubleClick);
            // 
            // trackersButtonPanel
            // 
            this.trackersButtonPanel.Controls.Add(this.removeTrackerButton);
            this.trackersButtonPanel.Controls.Add(this.editTrackerButton);
            this.trackersButtonPanel.Controls.Add(this.addTrackerButton);
            resources.ApplyResources(this.trackersButtonPanel, "trackersButtonPanel");
            this.trackersButtonPanel.Name = "trackersButtonPanel";
            // 
            // removeTrackerButton
            // 
            resources.ApplyResources(this.removeTrackerButton, "removeTrackerButton");
            this.removeTrackerButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.remove16;
            this.removeTrackerButton.Name = "removeTrackerButton";
            this.removeTrackerButton.UseVisualStyleBackColor = true;
            this.removeTrackerButton.Click += new System.EventHandler(this.removeTrackerButton_Click);
            // 
            // editTrackerButton
            // 
            resources.ApplyResources(this.editTrackerButton, "editTrackerButton");
            this.editTrackerButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.edit16;
            this.editTrackerButton.Name = "editTrackerButton";
            this.editTrackerButton.UseVisualStyleBackColor = true;
            this.editTrackerButton.Click += new System.EventHandler(this.editTrackerButton_Click);
            // 
            // addTrackerButton
            // 
            resources.ApplyResources(this.addTrackerButton, "addTrackerButton");
            this.addTrackerButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.add16;
            this.addTrackerButton.Name = "addTrackerButton";
            this.addTrackerButton.UseVisualStyleBackColor = true;
            this.addTrackerButton.Click += new System.EventHandler(this.addTrackerButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.CancelButton, "CancelButton");
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OkButton
            // 
            resources.ApplyResources(this.OkButton, "OkButton");
            this.OkButton.Name = "OkButton";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // noteLabel
            // 
            resources.ApplyResources(this.noteLabel, "noteLabel");
            this.noteLabel.Name = "noteLabel";
            // 
            // TorrentPropertiesDialog
            // 
            this.AcceptButton = this.OkButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelButton;
            this.Controls.Add(this.noteLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.tabProperties);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TorrentPropertiesDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.TorrentPropertiesDialog_Load);
            this.tabProperties.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seedIdleLimitValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seedRatioLimitValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peerLimitValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.downloadLimitField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uploadLimitField)).EndInit();
            this.tabTrackers.ResumeLayout(false);
            this.trackersButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabProperties;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.NumericUpDown downloadLimitField;
        private System.Windows.Forms.NumericUpDown uploadLimitField;
        private System.Windows.Forms.CheckBox downloadLimitEnableField;
        private System.Windows.Forms.CheckBox uploadLimitEnableField;
        private System.Windows.Forms.Label downloadLimitUnitLabel;
        private System.Windows.Forms.Label uploadLimitUnitLabel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.NumericUpDown peerLimitValue;
        private System.Windows.Forms.Label peerLimitLabel;
        private System.Windows.Forms.Label noteLabel;
        private System.Windows.Forms.NumericUpDown seedRatioLimitValue;
        private System.Windows.Forms.CheckBox seedRatioLimitedCheckBox;
        private System.Windows.Forms.CheckBox honorsSessionLimits;
        private System.Windows.Forms.Label bandwidthLabel;
        private System.Windows.Forms.ComboBox bandwidthComboBox;
        private System.Windows.Forms.CheckBox seedIdleLimitedCheckBox;
        private System.Windows.Forms.NumericUpDown seedIdleLimitValue;
        private System.Windows.Forms.TabPage tabTrackers;
        private System.Windows.Forms.RefreshingListBox trackersList;
        private System.Windows.Forms.Panel trackersButtonPanel;
        private System.Windows.Forms.Button addTrackerButton;
        private System.Windows.Forms.Button editTrackerButton;
        private System.Windows.Forms.Button removeTrackerButton;
    }
}