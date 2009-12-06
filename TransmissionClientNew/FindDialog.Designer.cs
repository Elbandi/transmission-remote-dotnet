namespace TransmissionRemoteDotnet
{
    partial class FindDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindDialog));
            this.findButton = new System.Windows.Forms.Button();
            this.findTextbox = new System.Windows.Forms.TextBox();
            this.findLabel = new System.Windows.Forms.Label();
            this.CaseSensitiveCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // findButton
            // 
            this.findButton.AccessibleDescription = null;
            this.findButton.AccessibleName = null;
            resources.ApplyResources(this.findButton, "findButton");
            this.findButton.BackgroundImage = null;
            this.findButton.Font = null;
            this.findButton.Name = "findButton";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // findTextbox
            // 
            this.findTextbox.AccessibleDescription = null;
            this.findTextbox.AccessibleName = null;
            resources.ApplyResources(this.findTextbox, "findTextbox");
            this.findTextbox.BackgroundImage = null;
            this.findTextbox.Font = null;
            this.findTextbox.Name = "findTextbox";
            // 
            // findLabel
            // 
            this.findLabel.AccessibleDescription = null;
            this.findLabel.AccessibleName = null;
            resources.ApplyResources(this.findLabel, "findLabel");
            this.findLabel.Font = null;
            this.findLabel.Name = "findLabel";
            // 
            // CaseSensitiveCheckBox
            // 
            this.CaseSensitiveCheckBox.AccessibleDescription = null;
            this.CaseSensitiveCheckBox.AccessibleName = null;
            resources.ApplyResources(this.CaseSensitiveCheckBox, "CaseSensitiveCheckBox");
            this.CaseSensitiveCheckBox.BackgroundImage = null;
            this.CaseSensitiveCheckBox.Font = null;
            this.CaseSensitiveCheckBox.Name = "CaseSensitiveCheckBox";
            this.CaseSensitiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // FindDialog
            // 
            this.AcceptButton = this.findButton;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.CaseSensitiveCheckBox);
            this.Controls.Add(this.findLabel);
            this.Controls.Add(this.findTextbox);
            this.Controls.Add(this.findButton);
            this.DoubleBuffered = true;
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.TextBox findTextbox;
        private System.Windows.Forms.Label findLabel;
        private System.Windows.Forms.CheckBox CaseSensitiveCheckBox;
    }
}