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
    partial class AboutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelDevelopers = new System.Windows.Forms.LinkLabel();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelHomepageLink = new System.Windows.Forms.LinkLabel();
            this.panelDescription = new System.Windows.Forms.Panel();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelCredits = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel.SuspendLayout();
            this.panelDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.labelDevelopers, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelHomepageLink, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.panelDescription, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.okButton, 0, 5);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // labelDevelopers
            // 
            this.tableLayoutPanel.SetColumnSpan(this.labelDevelopers, 2);
            resources.ApplyResources(this.labelDevelopers, "labelDevelopers");
            this.labelDevelopers.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelDevelopers.Name = "labelDevelopers";
            this.labelDevelopers.UseCompatibleTextRendering = true;
            // 
            // labelProductName
            // 
            resources.ApplyResources(this.labelProductName, "labelProductName");
            this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelProductName.Name = "labelProductName";
            // 
            // labelVersion
            // 
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelVersion.Name = "labelVersion";
            // 
            // labelCopyright
            // 
            this.tableLayoutPanel.SetColumnSpan(this.labelCopyright, 2);
            resources.ApplyResources(this.labelCopyright, "labelCopyright");
            this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelCopyright.Name = "labelCopyright";
            // 
            // labelHomepageLink
            // 
            this.tableLayoutPanel.SetColumnSpan(this.labelHomepageLink, 2);
            resources.ApplyResources(this.labelHomepageLink, "labelHomepageLink");
            this.labelHomepageLink.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelHomepageLink.Name = "labelHomepageLink";
            this.labelHomepageLink.TabStop = true;
            this.labelHomepageLink.UseCompatibleTextRendering = true;
            this.labelHomepageLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelHomepageLink_LinkClicked);
            // 
            // panelDescription
            // 
            this.panelDescription.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel.SetColumnSpan(this.panelDescription, 2);
            this.panelDescription.Controls.Add(this.textBoxDescription);
            this.panelDescription.Controls.Add(this.labelCredits);
            resources.ApplyResources(this.panelDescription, "panelDescription");
            this.panelDescription.Name = "panelDescription";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.BackColor = System.Drawing.Color.White;
            this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.TabStop = false;
            // 
            // labelCredits
            // 
            resources.ApplyResources(this.labelCredits, "labelCredits");
            this.labelCredits.Name = "labelCredits";
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.tableLayoutPanel.SetColumnSpan(this.okButton, 2);
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // logoPictureBox
            // 
            resources.ApplyResources(this.logoPictureBox, "logoPictureBox");
            this.logoPictureBox.Image = global::TransmissionRemoteDotnet.Properties.Resources.welcomefinish;
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.TabStop = false;
            // 
            // AboutDialog
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.logoPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.panelDescription.ResumeLayout(false);
            this.panelDescription.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.LinkLabel labelHomepageLink;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Panel panelDescription;
        private System.Windows.Forms.Label labelCredits;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.LinkLabel labelDevelopers;
    }
}
