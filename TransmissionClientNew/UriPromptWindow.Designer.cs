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
    partial class UriPromptWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UriPromptWindow));
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.okDialogButton = new System.Windows.Forms.Button();
            this.cancelDialogButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.downloadProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.useTorrentLoadDialogCheckBox = new System.Windows.Forms.CheckBox();
            this.addOurCookiesCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // urlTextBox
            // 
            resources.ApplyResources(this.urlTextBox, "urlTextBox");
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // okDialogButton
            // 
            resources.ApplyResources(this.okDialogButton, "okDialogButton");
            this.okDialogButton.Name = "okDialogButton";
            this.okDialogButton.UseVisualStyleBackColor = true;
            this.okDialogButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // cancelDialogButton
            // 
            this.cancelDialogButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancelDialogButton, "cancelDialogButton");
            this.cancelDialogButton.Name = "cancelDialogButton";
            this.cancelDialogButton.UseVisualStyleBackColor = true;
            this.cancelDialogButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.downloadProgressBar});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            resources.ApplyResources(this.toolStripStatusLabel, "toolStripStatusLabel");
            // 
            // downloadProgressBar
            // 
            this.downloadProgressBar.Name = "downloadProgressBar";
            resources.ApplyResources(this.downloadProgressBar, "downloadProgressBar");
            // 
            // useTorrentLoadDialogCheckBox
            // 
            resources.ApplyResources(this.useTorrentLoadDialogCheckBox, "useTorrentLoadDialogCheckBox");
            this.useTorrentLoadDialogCheckBox.Name = "useTorrentLoadDialogCheckBox";
            this.useTorrentLoadDialogCheckBox.UseVisualStyleBackColor = true;
            this.useTorrentLoadDialogCheckBox.CheckedChanged += new System.EventHandler(this.UseTorrentLoadDialogCheckBox_CheckedChanged);
            // 
            // addOurCookiesCheckBox
            // 
            resources.ApplyResources(this.addOurCookiesCheckBox, "addOurCookiesCheckBox");
            this.addOurCookiesCheckBox.Name = "addOurCookiesCheckBox";
            this.addOurCookiesCheckBox.UseVisualStyleBackColor = true;
            // 
            // UriPromptWindow
            // 
            this.AcceptButton = this.okDialogButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelDialogButton;
            this.Controls.Add(this.addOurCookiesCheckBox);
            this.Controls.Add(this.useTorrentLoadDialogCheckBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.cancelDialogButton);
            this.Controls.Add(this.okDialogButton);
            this.Controls.Add(this.urlTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UriPromptWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.UriPromptWindow_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Button okDialogButton;
        private System.Windows.Forms.Button cancelDialogButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar downloadProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.CheckBox useTorrentLoadDialogCheckBox;
        private System.Windows.Forms.CheckBox addOurCookiesCheckBox;
    }
}