namespace TransmissionRemoteDotnet
{
    partial class MoveDataPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveDataPrompt));
            this.destinationLabel = new System.Windows.Forms.Label();
            this.moveButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.destinationComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // destinationLabel
            // 
            resources.ApplyResources(this.destinationLabel, "destinationLabel");
            this.destinationLabel.Name = "destinationLabel";
            // 
            // moveButton
            // 
            resources.ApplyResources(this.moveButton, "moveButton");
            this.moveButton.Name = "moveButton";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.Name = "closeButton";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // destinationComboBox
            // 
            this.destinationComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.destinationComboBox, "destinationComboBox");
            this.destinationComboBox.Name = "destinationComboBox";
            this.destinationComboBox.SelectedIndexChanged += new System.EventHandler(this.destinationComboBox_SelectedIndexChanged);
            this.destinationComboBox.TextChanged += new System.EventHandler(this.destinationComboBox_SelectedIndexChanged);
            // 
            // MoveDataPrompt
            // 
            this.AcceptButton = this.moveButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.Controls.Add(this.destinationComboBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.destinationLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveDataPrompt";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label destinationLabel;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ComboBox destinationComboBox;
    }
}