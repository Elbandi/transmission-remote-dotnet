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
            this.filesListView = new TransmissionRemoteDotnet.ListViewNF();
            this.filesPathCol = new System.Windows.Forms.ColumnHeader();
            this.filesTypeCol = new System.Windows.Forms.ColumnHeader();
            this.filesSizeCol = new System.Windows.Forms.ColumnHeader();
            this.filesPriorityCol = new System.Windows.Forms.ColumnHeader();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.startTorrentCheckBox = new System.Windows.Forms.CheckBox();
            this.destinationComboBox = new System.Windows.Forms.ComboBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.peerLimitValue = new System.Windows.Forms.NumericUpDown();
            this.altPeerLimitCheckBox = new System.Windows.Forms.CheckBox();
            this.altDestDirCheckBox = new System.Windows.Forms.CheckBox();
            this.TorrentLoadBackgroundWorker = new System.ComponentModel.BackgroundWorker();
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
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peerLimitValue)).BeginInit();
            this.PropertiesGroupBox.SuspendLayout();
            this.TorrentContentsGroupBox.SuspendLayout();
            this.TorrentContentsPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // filesListView
            // 
            this.filesListView.CheckBoxes = true;
            this.filesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.filesPathCol,
            this.filesTypeCol,
            this.filesSizeCol,
            this.filesPriorityCol});
            resources.ApplyResources(this.filesListView, "filesListView");
            this.filesListView.FullRowSelect = true;
            this.filesListView.HideSelection = false;
            this.filesListView.Name = "filesListView";
            this.filesListView.UseCompatibleStateImageBehavior = false;
            this.filesListView.View = System.Windows.Forms.View.Details;
            this.filesListView.SelectedIndexChanged += new System.EventHandler(this.filesListView_SelectedIndexChanged);
            this.filesListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.filesListView_ColumnClick);
            // 
            // filesPathCol
            // 
            resources.ApplyResources(this.filesPathCol, "filesPathCol");
            // 
            // filesTypeCol
            // 
            resources.ApplyResources(this.filesTypeCol, "filesTypeCol");
            // 
            // filesSizeCol
            // 
            resources.ApplyResources(this.filesSizeCol, "filesSizeCol");
            // 
            // filesPriorityCol
            // 
            resources.ApplyResources(this.filesPriorityCol, "filesPriorityCol");
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
            // startTorrentCheckBox
            // 
            resources.ApplyResources(this.startTorrentCheckBox, "startTorrentCheckBox");
            this.startTorrentCheckBox.Name = "startTorrentCheckBox";
            this.startTorrentCheckBox.UseVisualStyleBackColor = true;
            // 
            // destinationComboBox
            // 
            resources.ApplyResources(this.destinationComboBox, "destinationComboBox");
            this.destinationComboBox.FormattingEnabled = true;
            this.destinationComboBox.Name = "destinationComboBox";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            resources.ApplyResources(this.toolStripStatusLabel, "toolStripStatusLabel");
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
            this.peerLimitValue.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.peerLimitValue.ValueChanged += new System.EventHandler(this.peerLimitValue_ValueChanged);
            // 
            // altPeerLimitCheckBox
            // 
            resources.ApplyResources(this.altPeerLimitCheckBox, "altPeerLimitCheckBox");
            this.altPeerLimitCheckBox.Name = "altPeerLimitCheckBox";
            this.altPeerLimitCheckBox.UseVisualStyleBackColor = true;
            this.altPeerLimitCheckBox.CheckedChanged += new System.EventHandler(this.altPeerLimitCheckBox_CheckedChanged);
            // 
            // altDestDirCheckBox
            // 
            resources.ApplyResources(this.altDestDirCheckBox, "altDestDirCheckBox");
            this.altDestDirCheckBox.Name = "altDestDirCheckBox";
            this.altDestDirCheckBox.UseVisualStyleBackColor = true;
            this.altDestDirCheckBox.CheckedChanged += new System.EventHandler(this.altDestDirCheckBox_CheckedChanged);
            // 
            // TorrentLoadBackgroundWorker
            // 
            this.TorrentLoadBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TorrentLoadBackgroundWorker_DoWork);
            this.TorrentLoadBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.TorrentLoadBackgroundWorker_RunWorkerCompleted);
            // 
            // PropertiesGroupBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.PropertiesGroupBox, 3);
            this.PropertiesGroupBox.Controls.Add(this.startTorrentCheckBox);
            this.PropertiesGroupBox.Controls.Add(this.destinationComboBox);
            this.PropertiesGroupBox.Controls.Add(this.peerLimitValue);
            this.PropertiesGroupBox.Controls.Add(this.altPeerLimitCheckBox);
            this.PropertiesGroupBox.Controls.Add(this.altDestDirCheckBox);
            resources.ApplyResources(this.PropertiesGroupBox, "PropertiesGroupBox");
            this.PropertiesGroupBox.Name = "PropertiesGroupBox";
            this.PropertiesGroupBox.TabStop = false;
            // 
            // TorrentContentsGroupBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.TorrentContentsGroupBox, 3);
            this.TorrentContentsGroupBox.Controls.Add(this.filesListView);
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
            this.SelectNoneButton.Click += new System.EventHandler(this.SelectNoneHandler);
            // 
            // SelectInvertButton
            // 
            resources.ApplyResources(this.SelectInvertButton, "SelectInvertButton");
            this.SelectInvertButton.Name = "SelectInvertButton";
            this.SelectInvertButton.UseVisualStyleBackColor = true;
            this.SelectInvertButton.Click += new System.EventHandler(this.SelectInvertHandler);
            // 
            // SelectAllButton
            // 
            resources.ApplyResources(this.SelectAllButton, "SelectAllButton");
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.UseVisualStyleBackColor = true;
            this.SelectAllButton.Click += new System.EventHandler(this.SelectAllHandler);
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
            this.Controls.Add(this.statusStrip);
            this.KeyPreview = true;
            this.Name = "TorrentLoadDialog";
            this.Load += new System.EventHandler(this.TorrentLoadDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TorrentLoadDialog_KeyDown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peerLimitValue)).EndInit();
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

        private System.ComponentModel.BackgroundWorker TorrentLoadBackgroundWorker;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox PropertiesGroupBox;
        private System.Windows.Forms.GroupBox TorrentContentsGroupBox;
        private System.Windows.Forms.CheckBox altDestDirCheckBox;
        private System.Windows.Forms.ComboBox destinationComboBox;
        private System.Windows.Forms.CheckBox startTorrentCheckBox;
        private System.Windows.Forms.CheckBox altPeerLimitCheckBox;
        private System.Windows.Forms.NumericUpDown peerLimitValue;
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
        private TransmissionRemoteDotnet.ListViewNF filesListView;
        private System.Windows.Forms.ColumnHeader filesPathCol;
        private System.Windows.Forms.ColumnHeader filesPriorityCol;
        private System.Windows.Forms.ColumnHeader filesTypeCol;
        private System.Windows.Forms.ColumnHeader filesSizeCol;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}