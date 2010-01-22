using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace TransmissionRemoteDotnet.CustomControls
{
    /// <summary>
    /// Summary description for SkinImageBrowseTextBox.
    /// </summary>
    public class SkinImageBrowseTextBox : UserControl
    {
        private TextBox fileTextBox;
        private Button browseButton;
        private GroupBox groupBox;
        public PictureBox pictureBox;
        private Panel PicturePanel;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public SkinImageBrowseTextBox()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.PicturePanel = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox.SuspendLayout();
            this.PicturePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // fileTextBox
            // 
            this.fileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fileTextBox.Location = new System.Drawing.Point(6, 19);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.Size = new System.Drawing.Size(258, 20);
            this.fileTextBox.TabIndex = 0;
            this.fileTextBox.Text = "fileTextBox";
            this.fileTextBox.TextChanged += new System.EventHandler(this.fileTextBox_TextChanged);
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Location = new System.Drawing.Point(270, 18);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(24, 21);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "...";
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.PicturePanel);
            this.groupBox.Controls.Add(this.browseButton);
            this.groupBox.Controls.Add(this.fileTextBox);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(300, 100);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "groupBox1";
            // 
            // PicturePanel
            // 
            this.PicturePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PicturePanel.AutoScroll = true;
            this.PicturePanel.Controls.Add(this.pictureBox);
            this.PicturePanel.Location = new System.Drawing.Point(6, 45);
            this.PicturePanel.Name = "PicturePanel";
            this.PicturePanel.Size = new System.Drawing.Size(288, 49);
            this.PicturePanel.TabIndex = 2;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(32, 32);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // SkinImageBrowseTextBox
            // 
            this.Controls.Add(this.groupBox);
            this.MinimumSize = new System.Drawing.Size(0, 76);
            this.Name = "SkinImageBrowseTextBox";
            this.Size = new System.Drawing.Size(300, 100);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.PicturePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = OtherStrings.OpenImageFilter;
            openFile.RestoreDirectory = true;
            openFile.Multiselect = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                fileTextBox.Text = openFile.FileName;
            }
        }

        private void fileTextBox_TextChanged(object sender, EventArgs e)
        {
            pictureBox.Image = Toolbox.LoadSkinImage(fileTextBox.Text, MinHeight, MaxHeight, ImageNumber);
            if (pictureBox.Image != null)
            {
                pictureBox.Width = pictureBox.Image.Width;
                pictureBox.Height = pictureBox.Image.Height;
            }
            else
            {
                pictureBox.Width = 1;
                pictureBox.Height = 1;
            }
        }

        public string Title
        {
            get { return groupBox.Text; }
            set { groupBox.Text = value; }
        }

        public string FileName
        {
            get { return fileTextBox.Text; }
            set { fileTextBox.Text = value; }
        }

        public int MaxHeight { get; set; }
        public int MinHeight { get; set; }
        public int ImageNumber { get; set; }
    }
}
