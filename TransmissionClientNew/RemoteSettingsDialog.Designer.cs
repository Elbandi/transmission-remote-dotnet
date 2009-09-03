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
            this.PEXcheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.portForwardCheckBox = new System.Windows.Forms.CheckBox();
            this.encryptionCombobox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dhtEnabled = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.testPortButton = new System.Windows.Forms.Button();
            this.updateBlocklistButton = new System.Windows.Forms.Button();
            this.blocklistEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.seedLimitUpDown = new System.Windows.Forms.NumericUpDown();
            this.seedRatioEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
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
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seedLimitUpDown)).BeginInit();
            this.tabPage3.SuspendLayout();
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
            // PEXcheckBox
            // 
            resources.ApplyResources(this.PEXcheckBox, "PEXcheckBox");
            this.PEXcheckBox.Name = "PEXcheckBox";
            this.PEXcheckBox.UseVisualStyleBackColor = true;
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dhtEnabled);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.testPortButton);
            this.tabPage1.Controls.Add(this.updateBlocklistButton);
            this.tabPage1.Controls.Add(this.blocklistEnabledCheckBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.PEXcheckBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.incomingPortValue);
            this.tabPage1.Controls.Add(this.portForwardCheckBox);
            this.tabPage1.Controls.Add(this.downloadToField);
            this.tabPage1.Controls.Add(this.encryptionCombobox);
            this.tabPage1.Controls.Add(this.label3);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dhtEnabled
            // 
            resources.ApplyResources(this.dhtEnabled, "dhtEnabled");
            this.dhtEnabled.Name = "dhtEnabled";
            this.dhtEnabled.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // testPortButton
            // 
            resources.ApplyResources(this.testPortButton, "testPortButton");
            this.testPortButton.Name = "testPortButton";
            this.testPortButton.UseVisualStyleBackColor = true;
            this.testPortButton.Click += new System.EventHandler(this.testPortButton_Click);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.seedLimitUpDown);
            this.tabPage2.Controls.Add(this.seedRatioEnabledCheckBox);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.limitUploadCheckBox);
            this.tabPage2.Controls.Add(this.peerLimitValue);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.limitUploadValue);
            this.tabPage2.Controls.Add(this.limitDownloadValue);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.limitDownloadCheckBox);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.timeConstaintEndMinutes);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.timeConstaintBeginMinutes);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.timeConstraintEndHours);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.timeConstraintBeginHours);
            this.tabPage3.Controls.Add(this.altUploadLimitField);
            this.tabPage3.Controls.Add(this.altDownloadLimitField);
            this.tabPage3.Controls.Add(this.altTimeConstraintEnabled);
            this.tabPage3.Controls.Add(this.altSpeedLimitEnable);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
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
            this.Controls.Add(this.tabControl1);
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seedLimitUpDown)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox PEXcheckBox;
        private System.Windows.Forms.NumericUpDown peerLimitValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage3;
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
        private System.Windows.Forms.Button updateBlocklistButton;
        private System.Windows.Forms.NumericUpDown seedLimitUpDown;
        private System.Windows.Forms.CheckBox seedRatioEnabledCheckBox;
        private System.Windows.Forms.Button testPortButton;
        private System.Windows.Forms.NumericUpDown timeConstaintEndMinutes;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown timeConstaintBeginMinutes;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox dhtEnabled;
    }
}