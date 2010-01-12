namespace TransmissionRemoteDotnet
{
    partial class TorrentLoadDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TorrentLoadDialog));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.PropertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.TorrentContentsGroupBox = new System.Windows.Forms.GroupBox();
            this.TorrentContentsPanel = new System.Windows.Forms.Panel();
            this.DateLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.SizeLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.CommentLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.NameLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.DateLabelLabel = new System.Windows.Forms.Label();
            this.SizelabelLabel = new System.Windows.Forms.Label();
            this.CommentLabelLabel = new System.Windows.Forms.Label();
            this.NameLabelLabel = new System.Windows.Forms.Label();
            this.SelectNoneButton = new System.Windows.Forms.Button();
            this.SelectInvertButton = new System.Windows.Forms.Button();
            this.SelectAllButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.PropertiesGroupBox.SuspendLayout();
            this.TorrentContentsGroupBox.SuspendLayout();
            this.TorrentContentsPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader3});
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.FullRowSelect = true;
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // CancelBtn
            // 
            resources.ApplyResources(this.CancelBtn, "CancelBtn");
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // OkButton
            // 
            resources.ApplyResources(this.OkButton, "OkButton");
            this.OkButton.Name = "OkButton";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox3
            // 
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Name = "comboBox1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // checkBox2
            // 
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // PropertiesGroupBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.PropertiesGroupBox, 3);
            this.PropertiesGroupBox.Controls.Add(this.checkBox3);
            this.PropertiesGroupBox.Controls.Add(this.comboBox1);
            this.PropertiesGroupBox.Controls.Add(this.numericUpDown1);
            this.PropertiesGroupBox.Controls.Add(this.checkBox2);
            this.PropertiesGroupBox.Controls.Add(this.checkBox1);
            resources.ApplyResources(this.PropertiesGroupBox, "PropertiesGroupBox");
            this.PropertiesGroupBox.Name = "PropertiesGroupBox";
            this.PropertiesGroupBox.TabStop = false;
            // 
            // TorrentContentsGroupBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.TorrentContentsGroupBox, 3);
            this.TorrentContentsGroupBox.Controls.Add(this.listView1);
            this.TorrentContentsGroupBox.Controls.Add(this.TorrentContentsPanel);
            resources.ApplyResources(this.TorrentContentsGroupBox, "TorrentContentsGroupBox");
            this.TorrentContentsGroupBox.Name = "TorrentContentsGroupBox";
            this.TorrentContentsGroupBox.TabStop = false;
            // 
            // TorrentContentsPanel
            // 
            this.TorrentContentsPanel.Controls.Add(this.DateLabel);
            this.TorrentContentsPanel.Controls.Add(this.SizeLabel);
            this.TorrentContentsPanel.Controls.Add(this.CommentLabel);
            this.TorrentContentsPanel.Controls.Add(this.NameLabel);
            this.TorrentContentsPanel.Controls.Add(this.DateLabelLabel);
            this.TorrentContentsPanel.Controls.Add(this.SizelabelLabel);
            this.TorrentContentsPanel.Controls.Add(this.CommentLabelLabel);
            this.TorrentContentsPanel.Controls.Add(this.NameLabelLabel);
            this.TorrentContentsPanel.Controls.Add(this.SelectNoneButton);
            this.TorrentContentsPanel.Controls.Add(this.SelectInvertButton);
            this.TorrentContentsPanel.Controls.Add(this.SelectAllButton);
            resources.ApplyResources(this.TorrentContentsPanel, "TorrentContentsPanel");
            this.TorrentContentsPanel.Name = "TorrentContentsPanel";
            // 
            // DateLabel
            // 
            this.DateLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.DateLabel, "DateLabel");
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.ReadOnly = true;
            // 
            // SizeLabel
            // 
            this.SizeLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.SizeLabel, "SizeLabel");
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.ReadOnly = true;
            // 
            // CommentLabel
            // 
            this.CommentLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.CommentLabel, "CommentLabel");
            this.CommentLabel.Name = "CommentLabel";
            this.CommentLabel.ReadOnly = true;
            // 
            // NameLabel
            // 
            this.NameLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.NameLabel, "NameLabel");
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.ReadOnly = true;
            // 
            // DateLabelLabel
            // 
            resources.ApplyResources(this.DateLabelLabel, "DateLabelLabel");
            this.DateLabelLabel.Name = "DateLabelLabel";
            // 
            // SizelabelLabel
            // 
            resources.ApplyResources(this.SizelabelLabel, "SizelabelLabel");
            this.SizelabelLabel.Name = "SizelabelLabel";
            // 
            // CommentLabelLabel
            // 
            resources.ApplyResources(this.CommentLabelLabel, "CommentLabelLabel");
            this.CommentLabelLabel.Name = "CommentLabelLabel";
            // 
            // NameLabelLabel
            // 
            resources.ApplyResources(this.NameLabelLabel, "NameLabelLabel");
            this.NameLabelLabel.Name = "NameLabelLabel";
            // 
            // SelectNoneButton
            // 
            resources.ApplyResources(this.SelectNoneButton, "SelectNoneButton");
            this.SelectNoneButton.Name = "SelectNoneButton";
            this.SelectNoneButton.UseVisualStyleBackColor = true;
            this.SelectNoneButton.Click += new System.EventHandler(this.SelectNoneButton_Click);
            // 
            // SelectInvertButton
            // 
            resources.ApplyResources(this.SelectInvertButton, "SelectInvertButton");
            this.SelectInvertButton.Name = "SelectInvertButton";
            this.SelectInvertButton.UseVisualStyleBackColor = true;
            this.SelectInvertButton.Click += new System.EventHandler(this.SelectInvertButton_Click);
            // 
            // SelectAllButton
            // 
            resources.ApplyResources(this.SelectAllButton, "SelectAllButton");
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.UseVisualStyleBackColor = true;
            this.SelectAllButton.Click += new System.EventHandler(this.SelectAllButton_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.PropertiesGroupBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TorrentContentsGroupBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.CancelBtn, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.OkButton, 1, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // TorrentLoadDialog
            // 
            this.AcceptButton = this.OkButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.KeyPreview = true;
            this.Name = "TorrentLoadDialog";
            this.Load += new System.EventHandler(this.TorrentLoadDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TorrentLoadDialog_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.PropertiesGroupBox.ResumeLayout(false);
            this.PropertiesGroupBox.PerformLayout();
            this.TorrentContentsGroupBox.ResumeLayout(false);
            this.TorrentContentsPanel.ResumeLayout(false);
            this.TorrentContentsPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox PropertiesGroupBox;
        private System.Windows.Forms.GroupBox TorrentContentsGroupBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Panel TorrentContentsPanel;
        private System.Windows.Forms.Label NameLabelLabel;
        private System.Windows.Forms.Label CommentLabelLabel;
        private System.Windows.Forms.Label SizelabelLabel;
        private System.Windows.Forms.Label DateLabelLabel;
        private SelectableLabel DateLabel;
        private SelectableLabel SizeLabel;
        private SelectableLabel CommentLabel;
        private SelectableLabel NameLabel;
        private System.Windows.Forms.Button SelectInvertButton;
        private System.Windows.Forms.Button SelectNoneButton;
        private System.Windows.Forms.Button SelectAllButton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}