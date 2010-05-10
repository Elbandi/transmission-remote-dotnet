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
    partial class StatsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatsDialog));
            this.downloadedBytesLabel2 = new System.Windows.Forms.Label();
            this.uploadedBytesLabel2 = new System.Windows.Forms.Label();
            this.CloseFormButton = new System.Windows.Forms.Button();
            this.CumulativeGroupBox = new System.Windows.Forms.GroupBox();
            this.secondsActiveValue2 = new System.Windows.Forms.Label();
            this.sessionCountValue2 = new System.Windows.Forms.Label();
            this.filesAddedValue2 = new System.Windows.Forms.Label();
            this.uploadedBytesValue2 = new System.Windows.Forms.Label();
            this.downloadedBytesValue2 = new System.Windows.Forms.Label();
            this.secondsActiveLabel2 = new System.Windows.Forms.Label();
            this.sessionCountLabel2 = new System.Windows.Forms.Label();
            this.filesAddedLabel2 = new System.Windows.Forms.Label();
            this.SessionStatsTimer = new System.Windows.Forms.Timer(this.components);
            this.CurrentSessionGroupBox = new System.Windows.Forms.GroupBox();
            this.secondsActiveValue1 = new System.Windows.Forms.Label();
            this.sessionCountValue1 = new System.Windows.Forms.Label();
            this.filesAddedValue1 = new System.Windows.Forms.Label();
            this.uploadedBytesValue1 = new System.Windows.Forms.Label();
            this.downloadedBytesValue1 = new System.Windows.Forms.Label();
            this.secondsActiveLabel1 = new System.Windows.Forms.Label();
            this.sessionCountLabel1 = new System.Windows.Forms.Label();
            this.filesAddedLabel1 = new System.Windows.Forms.Label();
            this.downloadedBytesLabel1 = new System.Windows.Forms.Label();
            this.uploadedBytesLabel1 = new System.Windows.Forms.Label();
            this.unitFactorLabel = new System.Windows.Forms.Label();
            this.unitFactorComboBox = new System.Windows.Forms.ComboBox();
            this.CumulativeGroupBox.SuspendLayout();
            this.CurrentSessionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // downloadedBytesLabel2
            // 
            resources.ApplyResources(this.downloadedBytesLabel2, "downloadedBytesLabel2");
            this.downloadedBytesLabel2.Name = "downloadedBytesLabel2";
            // 
            // uploadedBytesLabel2
            // 
            resources.ApplyResources(this.uploadedBytesLabel2, "uploadedBytesLabel2");
            this.uploadedBytesLabel2.Name = "uploadedBytesLabel2";
            // 
            // CloseFormButton
            // 
            this.CloseFormButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.CloseFormButton, "CloseFormButton");
            this.CloseFormButton.Name = "CloseFormButton";
            this.CloseFormButton.UseVisualStyleBackColor = true;
            this.CloseFormButton.Click += new System.EventHandler(this.CloseFormButton_Click);
            // 
            // CumulativeGroupBox
            // 
            this.CumulativeGroupBox.Controls.Add(this.secondsActiveValue2);
            this.CumulativeGroupBox.Controls.Add(this.sessionCountValue2);
            this.CumulativeGroupBox.Controls.Add(this.filesAddedValue2);
            this.CumulativeGroupBox.Controls.Add(this.uploadedBytesValue2);
            this.CumulativeGroupBox.Controls.Add(this.downloadedBytesValue2);
            this.CumulativeGroupBox.Controls.Add(this.secondsActiveLabel2);
            this.CumulativeGroupBox.Controls.Add(this.sessionCountLabel2);
            this.CumulativeGroupBox.Controls.Add(this.filesAddedLabel2);
            this.CumulativeGroupBox.Controls.Add(this.downloadedBytesLabel2);
            this.CumulativeGroupBox.Controls.Add(this.uploadedBytesLabel2);
            resources.ApplyResources(this.CumulativeGroupBox, "CumulativeGroupBox");
            this.CumulativeGroupBox.Name = "CumulativeGroupBox";
            this.CumulativeGroupBox.TabStop = false;
            // 
            // secondsActiveValue2
            // 
            resources.ApplyResources(this.secondsActiveValue2, "secondsActiveValue2");
            this.secondsActiveValue2.Name = "secondsActiveValue2";
            // 
            // sessionCountValue2
            // 
            resources.ApplyResources(this.sessionCountValue2, "sessionCountValue2");
            this.sessionCountValue2.Name = "sessionCountValue2";
            // 
            // filesAddedValue2
            // 
            resources.ApplyResources(this.filesAddedValue2, "filesAddedValue2");
            this.filesAddedValue2.Name = "filesAddedValue2";
            // 
            // uploadedBytesValue2
            // 
            resources.ApplyResources(this.uploadedBytesValue2, "uploadedBytesValue2");
            this.uploadedBytesValue2.Name = "uploadedBytesValue2";
            // 
            // downloadedBytesValue2
            // 
            resources.ApplyResources(this.downloadedBytesValue2, "downloadedBytesValue2");
            this.downloadedBytesValue2.Name = "downloadedBytesValue2";
            // 
            // secondsActiveLabel2
            // 
            resources.ApplyResources(this.secondsActiveLabel2, "secondsActiveLabel2");
            this.secondsActiveLabel2.Name = "secondsActiveLabel2";
            // 
            // sessionCountLabel2
            // 
            resources.ApplyResources(this.sessionCountLabel2, "sessionCountLabel2");
            this.sessionCountLabel2.Name = "sessionCountLabel2";
            // 
            // filesAddedLabel2
            // 
            resources.ApplyResources(this.filesAddedLabel2, "filesAddedLabel2");
            this.filesAddedLabel2.Name = "filesAddedLabel2";
            // 
            // SessionStatsTimer
            // 
            this.SessionStatsTimer.Interval = 3000;
            this.SessionStatsTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CurrentSessionGroupBox
            // 
            this.CurrentSessionGroupBox.Controls.Add(this.secondsActiveValue1);
            this.CurrentSessionGroupBox.Controls.Add(this.sessionCountValue1);
            this.CurrentSessionGroupBox.Controls.Add(this.filesAddedValue1);
            this.CurrentSessionGroupBox.Controls.Add(this.uploadedBytesValue1);
            this.CurrentSessionGroupBox.Controls.Add(this.downloadedBytesValue1);
            this.CurrentSessionGroupBox.Controls.Add(this.secondsActiveLabel1);
            this.CurrentSessionGroupBox.Controls.Add(this.sessionCountLabel1);
            this.CurrentSessionGroupBox.Controls.Add(this.filesAddedLabel1);
            this.CurrentSessionGroupBox.Controls.Add(this.downloadedBytesLabel1);
            this.CurrentSessionGroupBox.Controls.Add(this.uploadedBytesLabel1);
            resources.ApplyResources(this.CurrentSessionGroupBox, "CurrentSessionGroupBox");
            this.CurrentSessionGroupBox.Name = "CurrentSessionGroupBox";
            this.CurrentSessionGroupBox.TabStop = false;
            // 
            // secondsActiveValue1
            // 
            resources.ApplyResources(this.secondsActiveValue1, "secondsActiveValue1");
            this.secondsActiveValue1.Name = "secondsActiveValue1";
            // 
            // sessionCountValue1
            // 
            resources.ApplyResources(this.sessionCountValue1, "sessionCountValue1");
            this.sessionCountValue1.Name = "sessionCountValue1";
            // 
            // filesAddedValue1
            // 
            resources.ApplyResources(this.filesAddedValue1, "filesAddedValue1");
            this.filesAddedValue1.Name = "filesAddedValue1";
            // 
            // uploadedBytesValue1
            // 
            resources.ApplyResources(this.uploadedBytesValue1, "uploadedBytesValue1");
            this.uploadedBytesValue1.Name = "uploadedBytesValue1";
            // 
            // downloadedBytesValue1
            // 
            resources.ApplyResources(this.downloadedBytesValue1, "downloadedBytesValue1");
            this.downloadedBytesValue1.Name = "downloadedBytesValue1";
            // 
            // secondsActiveLabel1
            // 
            resources.ApplyResources(this.secondsActiveLabel1, "secondsActiveLabel1");
            this.secondsActiveLabel1.Name = "secondsActiveLabel1";
            // 
            // sessionCountLabel1
            // 
            resources.ApplyResources(this.sessionCountLabel1, "sessionCountLabel1");
            this.sessionCountLabel1.Name = "sessionCountLabel1";
            // 
            // filesAddedLabel1
            // 
            resources.ApplyResources(this.filesAddedLabel1, "filesAddedLabel1");
            this.filesAddedLabel1.Name = "filesAddedLabel1";
            // 
            // downloadedBytesLabel1
            // 
            resources.ApplyResources(this.downloadedBytesLabel1, "downloadedBytesLabel1");
            this.downloadedBytesLabel1.Name = "downloadedBytesLabel1";
            // 
            // uploadedBytesLabel1
            // 
            resources.ApplyResources(this.uploadedBytesLabel1, "uploadedBytesLabel1");
            this.uploadedBytesLabel1.Name = "uploadedBytesLabel1";
            // 
            // unitFactorLabel
            // 
            resources.ApplyResources(this.unitFactorLabel, "unitFactorLabel");
            this.unitFactorLabel.Name = "unitFactorLabel";
            // 
            // unitFactorComboBox
            // 
            this.unitFactorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitFactorComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.unitFactorComboBox, "unitFactorComboBox");
            this.unitFactorComboBox.Name = "unitFactorComboBox";
            // 
            // StatsDialog
            // 
            this.AcceptButton = this.CloseFormButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseFormButton;
            this.Controls.Add(this.unitFactorComboBox);
            this.Controls.Add(this.unitFactorLabel);
            this.Controls.Add(this.CurrentSessionGroupBox);
            this.Controls.Add(this.CumulativeGroupBox);
            this.Controls.Add(this.CloseFormButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "StatsDialog";
            this.Load += new System.EventHandler(this.StatsDialog_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StatsDialog_FormClosing);
            this.CumulativeGroupBox.ResumeLayout(false);
            this.CumulativeGroupBox.PerformLayout();
            this.CurrentSessionGroupBox.ResumeLayout(false);
            this.CurrentSessionGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CloseFormButton;
        private System.Windows.Forms.Timer SessionStatsTimer;
        private System.Windows.Forms.GroupBox CumulativeGroupBox;
        private System.Windows.Forms.Label secondsActiveValue1;
        private System.Windows.Forms.Label sessionCountValue1;
        private System.Windows.Forms.Label filesAddedValue1;
        private System.Windows.Forms.Label uploadedBytesValue1;
        private System.Windows.Forms.Label downloadedBytesValue1;
        private System.Windows.Forms.Label secondsActiveLabel1;
        private System.Windows.Forms.Label sessionCountLabel1;
        private System.Windows.Forms.Label filesAddedLabel1;
        private System.Windows.Forms.Label downloadedBytesLabel1;
        private System.Windows.Forms.Label uploadedBytesLabel1;
        private System.Windows.Forms.GroupBox CurrentSessionGroupBox;
        private System.Windows.Forms.Label filesAddedLabel2;
        private System.Windows.Forms.Label sessionCountLabel2;
        private System.Windows.Forms.Label secondsActiveLabel2;
        private System.Windows.Forms.Label secondsActiveValue2;
        private System.Windows.Forms.Label sessionCountValue2;
        private System.Windows.Forms.Label filesAddedValue2;
        private System.Windows.Forms.Label uploadedBytesValue2;
        private System.Windows.Forms.Label downloadedBytesValue2;
        private System.Windows.Forms.Label downloadedBytesLabel2;
        private System.Windows.Forms.Label uploadedBytesLabel2;
        private System.Windows.Forms.Label unitFactorLabel;
        private System.Windows.Forms.ComboBox unitFactorComboBox;
    }
}