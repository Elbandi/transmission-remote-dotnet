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
            resources.ApplyResources(this.findButton, "findButton");
            this.findButton.Name = "findButton";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // findTextbox
            // 
            resources.ApplyResources(this.findTextbox, "findTextbox");
            this.findTextbox.Name = "findTextbox";
            // 
            // findLabel
            // 
            resources.ApplyResources(this.findLabel, "findLabel");
            this.findLabel.Name = "findLabel";
            // 
            // CaseSensitiveCheckBox
            // 
            resources.ApplyResources(this.CaseSensitiveCheckBox, "CaseSensitiveCheckBox");
            this.CaseSensitiveCheckBox.Name = "CaseSensitiveCheckBox";
            this.CaseSensitiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // FindDialog
            // 
            this.AcceptButton = this.findButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CaseSensitiveCheckBox);
            this.Controls.Add(this.findLabel);
            this.Controls.Add(this.findTextbox);
            this.Controls.Add(this.findButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
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