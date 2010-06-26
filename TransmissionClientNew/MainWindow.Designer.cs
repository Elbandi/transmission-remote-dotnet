namespace TransmissionRemoteDotnet
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.torrentAndTabsSplitContainer = new TransmissionRemoteDotnet.SplitContainerFix();
            this.mainVerticalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.stateListBox = new TransmissionRemoteDotnet.GListBox();
            this.stateListBoxImageList = new System.Windows.Forms.ImageList(this.components);
            this.torrentListView = new TransmissionRemoteDotnet.ListViewNF();
            this.torrentNameCol = new System.Windows.Forms.ColumnHeader();
            this.torrentNoCol = new System.Windows.Forms.ColumnHeader();
            this.torrentSizeCol = new System.Windows.Forms.ColumnHeader();
            this.torrentDoneCol = new System.Windows.Forms.ColumnHeader();
            this.torrentStatusCol = new System.Windows.Forms.ColumnHeader();
            this.torrentSeedsCol = new System.Windows.Forms.ColumnHeader();
            this.torrentLeechersCol = new System.Windows.Forms.ColumnHeader();
            this.torrentDownSpeedCol = new System.Windows.Forms.ColumnHeader();
            this.torrentUpSpeedCol = new System.Windows.Forms.ColumnHeader();
            this.torrentEtaCol = new System.Windows.Forms.ColumnHeader();
            this.torrentUploadedCol = new System.Windows.Forms.ColumnHeader();
            this.torrentRatioCol = new System.Windows.Forms.ColumnHeader();
            this.torrentAddedAt = new System.Windows.Forms.ColumnHeader();
            this.torrentCompletedAtCol = new System.Windows.Forms.ColumnHeader();
            this.torrentTrackerCol = new System.Windows.Forms.ColumnHeader();
            this.torrentTabControl = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.generalTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.downloadProgressLabel = new System.Windows.Forms.Label();
            this.progressOrPiecesPanel = new System.Windows.Forms.Panel();
            this.piecesGraph = new TransmissionRemoteDotnet.PiecesGraph();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.percentageLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.generalTorrentNameGroupBox = new System.Windows.Forms.GroupBox();
            this.totalSizeLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.locationLabelLabel = new System.Windows.Forms.Label();
            this.locationLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.piecesInfoLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.piecesInfoLabelLabel = new System.Windows.Forms.Label();
            this.hashLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.hashLabelLabel = new System.Windows.Forms.Label();
            this.timeElapsedLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.startedAtLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.uploadLimitLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.uploadRateLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.uploadedLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.remainingLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.downloadedLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.downloadSpeedLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.downloadLimitLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.statusLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.commentLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.errorLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.createdByLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.createdAtLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.leechersLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.ratioLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.seedersLabel = new TransmissionRemoteDotnet.SelectableLabel();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.labelForErrorLabel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timeElapsedLabelLabel = new System.Windows.Forms.Label();
            this.trackersTabPage = new System.Windows.Forms.TabPage();
            this.trackersTorrentNameGroupBox = new System.Windows.Forms.GroupBox();
            this.trackersListView = new TransmissionRemoteDotnet.ListViewNF();
            this.trackersTierCol = new System.Windows.Forms.ColumnHeader();
            this.trackersAnnounceUrlCol = new System.Windows.Forms.ColumnHeader();
            this.trackersStatusCol = new System.Windows.Forms.ColumnHeader();
            this.trackersUpdateInCol = new System.Windows.Forms.ColumnHeader();
            this.trackersSeedsCol = new System.Windows.Forms.ColumnHeader();
            this.trackersLeechersCol = new System.Windows.Forms.ColumnHeader();
            this.trackersDownloadedCol = new System.Windows.Forms.ColumnHeader();
            this.filesTabPage = new System.Windows.Forms.TabPage();
            this.filesTorrentNameGroupBox = new System.Windows.Forms.GroupBox();
            this.filesListView = new TransmissionRemoteDotnet.ListViewNF();
            this.filesPathCol = new System.Windows.Forms.ColumnHeader();
            this.filesTypeCol = new System.Windows.Forms.ColumnHeader();
            this.filesSizeCol = new System.Windows.Forms.ColumnHeader();
            this.filesDoneCol = new System.Windows.Forms.ColumnHeader();
            this.filesPercentCol = new System.Windows.Forms.ColumnHeader();
            this.filesSkipCol = new System.Windows.Forms.ColumnHeader();
            this.filesPriorityCol = new System.Windows.Forms.ColumnHeader();
            this.fileIconImageList = new System.Windows.Forms.ImageList(this.components);
            this.peersTabPage = new System.Windows.Forms.TabPage();
            this.peersTorrentNameGroupBox = new System.Windows.Forms.GroupBox();
            this.peersListView = new TransmissionRemoteDotnet.ListViewNF();
            this.peersIpAddressCol = new System.Windows.Forms.ColumnHeader();
            this.peersHostnameCol = new System.Windows.Forms.ColumnHeader();
            this.peersCountryCol = new System.Windows.Forms.ColumnHeader();
            this.peersFlagsCol = new System.Windows.Forms.ColumnHeader();
            this.peersClientCol = new System.Windows.Forms.ColumnHeader();
            this.peersProgressCol = new System.Windows.Forms.ColumnHeader();
            this.peersDownSpeedCol = new System.Windows.Forms.ColumnHeader();
            this.peersUpSpeedCol = new System.Windows.Forms.ColumnHeader();
            this.speedTabPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label18 = new System.Windows.Forms.Label();
            this.speedResComboBox = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.speedGraph = new CustomUIControls.Graphing.C2DPushGraph();
            this.tabControlImageList = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripVersionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.connectButton = new System.Windows.Forms.ToolStripSplitButton();
            this.disconnectButton = new System.Windows.Forms.ToolStripButton();
            this.toolbarToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addTorrentButton = new System.Windows.Forms.ToolStripButton();
            this.addWebTorrentButton = new System.Windows.Forms.ToolStripButton();
            this.toolbarToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.startTorrentButton = new System.Windows.Forms.ToolStripButton();
            this.pauseTorrentButton = new System.Windows.Forms.ToolStripButton();
            this.recheckTorrentButton = new System.Windows.Forms.ToolStripButton();
            this.configureTorrentButton = new System.Windows.Forms.ToolStripButton();
            this.removeTorrentButton = new System.Windows.Forms.ToolStripButton();
            this.removeAndDeleteButton = new System.Windows.Forms.ToolStripButton();
            this.reannounceButton = new System.Windows.Forms.ToolStripSplitButton();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentlyActiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openNetworkShareButton = new System.Windows.Forms.ToolStripButton();
            this.remoteCmdButton = new System.Windows.Forms.ToolStripButton();
            this.toolbarToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.AltSpeedButton = new System.Windows.Forms.ToolStripButton();
            this.toolbarToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.localConfigureButton = new System.Windows.Forms.ToolStripButton();
            this.remoteConfigureButton = new System.Windows.Forms.ToolStripButton();
            this.sessionStatsButton = new System.Windows.Forms.ToolStripButton();
            this.toolbarToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.RssButton = new System.Windows.Forms.ToolStripButton();
            this.FilterTorrentTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.FilterTorrentLabel = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenuToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addTorrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTorrentWithOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTorrentFromUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenuToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsMenuToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsMenuToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.importLocalSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLocalSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.torrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.torrentMenuToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reannounceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveTorrentDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openNetworkShareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVInfoToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.torrentMenuToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.startAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDetailsPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCategoriesPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.statsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showErrorLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.projectSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForNewVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.filesTimer = new System.Windows.Forms.Timer(this.components);
            this.refreshElapsedTimer = new System.Windows.Forms.Timer(this.components);
            this.openTorrentFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStripImageList = new System.Windows.Forms.ImageList(this.components);
            this.trayIconImageList = new System.Windows.Forms.ImageList(this.components);
            this.saveSettingsFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openSettingsFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.torrentAndTabsSplitContainer.Panel1.SuspendLayout();
            this.torrentAndTabsSplitContainer.Panel2.SuspendLayout();
            this.torrentAndTabsSplitContainer.SuspendLayout();
            this.mainVerticalSplitContainer.Panel1.SuspendLayout();
            this.mainVerticalSplitContainer.Panel2.SuspendLayout();
            this.mainVerticalSplitContainer.SuspendLayout();
            this.torrentTabControl.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.generalTableLayoutPanel.SuspendLayout();
            this.progressOrPiecesPanel.SuspendLayout();
            this.generalTorrentNameGroupBox.SuspendLayout();
            this.trackersTabPage.SuspendLayout();
            this.trackersTorrentNameGroupBox.SuspendLayout();
            this.filesTabPage.SuspendLayout();
            this.filesTorrentNameGroupBox.SuspendLayout();
            this.peersTabPage.SuspendLayout();
            this.peersTorrentNameGroupBox.SuspendLayout();
            this.speedTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.torrentAndTabsSplitContainer);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.statusStrip);
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.TopToolStripPanel, "toolStripContainer1.TopToolStripPanel");
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip);
            // 
            // torrentAndTabsSplitContainer
            // 
            resources.ApplyResources(this.torrentAndTabsSplitContainer, "torrentAndTabsSplitContainer");
            this.torrentAndTabsSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.torrentAndTabsSplitContainer.Name = "torrentAndTabsSplitContainer";
            // 
            // torrentAndTabsSplitContainer.Panel1
            // 
            this.torrentAndTabsSplitContainer.Panel1.Controls.Add(this.mainVerticalSplitContainer);
            // 
            // torrentAndTabsSplitContainer.Panel2
            // 
            this.torrentAndTabsSplitContainer.Panel2.Controls.Add(this.torrentTabControl);
            // 
            // mainVerticalSplitContainer
            // 
            resources.ApplyResources(this.mainVerticalSplitContainer, "mainVerticalSplitContainer");
            this.mainVerticalSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.mainVerticalSplitContainer.Name = "mainVerticalSplitContainer";
            // 
            // mainVerticalSplitContainer.Panel1
            // 
            this.mainVerticalSplitContainer.Panel1.Controls.Add(this.stateListBox);
            // 
            // mainVerticalSplitContainer.Panel2
            // 
            this.mainVerticalSplitContainer.Panel2.Controls.Add(this.torrentListView);
            // 
            // stateListBox
            // 
            resources.ApplyResources(this.stateListBox, "stateListBox");
            this.stateListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.stateListBox.FormattingEnabled = true;
            this.stateListBox.ImageList = this.stateListBoxImageList;
            this.stateListBox.Name = "stateListBox";
            this.stateListBox.SelectedIndexChanged += new System.EventHandler(this.stateListBox_SelectedIndexChanged);
            // 
            // stateListBoxImageList
            // 
            this.stateListBoxImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.stateListBoxImageList, "stateListBoxImageList");
            this.stateListBoxImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // torrentListView
            // 
            this.torrentListView.AllowColumnReorder = true;
            this.torrentListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.torrentNameCol,
            this.torrentNoCol,
            this.torrentSizeCol,
            this.torrentDoneCol,
            this.torrentStatusCol,
            this.torrentSeedsCol,
            this.torrentLeechersCol,
            this.torrentDownSpeedCol,
            this.torrentUpSpeedCol,
            this.torrentEtaCol,
            this.torrentUploadedCol,
            this.torrentRatioCol,
            this.torrentAddedAt,
            this.torrentCompletedAtCol,
            this.torrentTrackerCol});
            resources.ApplyResources(this.torrentListView, "torrentListView");
            this.torrentListView.FullRowSelect = true;
            this.torrentListView.HideSelection = false;
            this.torrentListView.Name = "torrentListView";
            this.torrentListView.OwnerDraw = true;
            this.torrentListView.ShowItemToolTips = true;
            this.torrentListView.SmallImageList = this.stateListBoxImageList;
            this.torrentListView.UseCompatibleStateImageBehavior = false;
            this.torrentListView.View = System.Windows.Forms.View.Details;
            this.torrentListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.torrentListView_DrawColumnHeader);
            this.torrentListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.torrentListView_DrawItem);
            this.torrentListView.SelectedIndexChanged += new System.EventHandler(this.torrentListView_SelectedIndexChanged);
            this.torrentListView.DoubleClick += new System.EventHandler(this.torrentListView_DoubleClick);
            this.torrentListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.torrentListView_ColumnClick);
            this.torrentListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.torrentListView_KeyDown);
            this.torrentListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.torrentListView_DrawSubItem);
            // 
            // torrentNameCol
            // 
            resources.ApplyResources(this.torrentNameCol, "torrentNameCol");
            // 
            // torrentNoCol
            // 
            resources.ApplyResources(this.torrentNoCol, "torrentNoCol");
            // 
            // torrentSizeCol
            // 
            resources.ApplyResources(this.torrentSizeCol, "torrentSizeCol");
            // 
            // torrentDoneCol
            // 
            resources.ApplyResources(this.torrentDoneCol, "torrentDoneCol");
            // 
            // torrentStatusCol
            // 
            resources.ApplyResources(this.torrentStatusCol, "torrentStatusCol");
            // 
            // torrentSeedsCol
            // 
            resources.ApplyResources(this.torrentSeedsCol, "torrentSeedsCol");
            // 
            // torrentLeechersCol
            // 
            resources.ApplyResources(this.torrentLeechersCol, "torrentLeechersCol");
            // 
            // torrentDownSpeedCol
            // 
            resources.ApplyResources(this.torrentDownSpeedCol, "torrentDownSpeedCol");
            // 
            // torrentUpSpeedCol
            // 
            resources.ApplyResources(this.torrentUpSpeedCol, "torrentUpSpeedCol");
            // 
            // torrentEtaCol
            // 
            resources.ApplyResources(this.torrentEtaCol, "torrentEtaCol");
            // 
            // torrentUploadedCol
            // 
            resources.ApplyResources(this.torrentUploadedCol, "torrentUploadedCol");
            // 
            // torrentRatioCol
            // 
            resources.ApplyResources(this.torrentRatioCol, "torrentRatioCol");
            // 
            // torrentAddedAt
            // 
            resources.ApplyResources(this.torrentAddedAt, "torrentAddedAt");
            // 
            // torrentCompletedAtCol
            // 
            resources.ApplyResources(this.torrentCompletedAtCol, "torrentCompletedAtCol");
            // 
            // torrentTrackerCol
            // 
            resources.ApplyResources(this.torrentTrackerCol, "torrentTrackerCol");
            // 
            // torrentTabControl
            // 
            this.torrentTabControl.Controls.Add(this.generalTabPage);
            this.torrentTabControl.Controls.Add(this.trackersTabPage);
            this.torrentTabControl.Controls.Add(this.filesTabPage);
            this.torrentTabControl.Controls.Add(this.peersTabPage);
            this.torrentTabControl.Controls.Add(this.speedTabPage);
            resources.ApplyResources(this.torrentTabControl, "torrentTabControl");
            this.torrentTabControl.ImageList = this.tabControlImageList;
            this.torrentTabControl.Name = "torrentTabControl";
            this.torrentTabControl.SelectedIndex = 0;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.generalTableLayoutPanel);
            resources.ApplyResources(this.generalTabPage, "generalTabPage");
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.UseVisualStyleBackColor = true;
            // 
            // generalTableLayoutPanel
            // 
            resources.ApplyResources(this.generalTableLayoutPanel, "generalTableLayoutPanel");
            this.generalTableLayoutPanel.Controls.Add(this.downloadProgressLabel, 0, 0);
            this.generalTableLayoutPanel.Controls.Add(this.progressOrPiecesPanel, 1, 0);
            this.generalTableLayoutPanel.Controls.Add(this.percentageLabel, 2, 0);
            this.generalTableLayoutPanel.Controls.Add(this.generalTorrentNameGroupBox, 0, 1);
            this.generalTableLayoutPanel.Name = "generalTableLayoutPanel";
            // 
            // downloadProgressLabel
            // 
            resources.ApplyResources(this.downloadProgressLabel, "downloadProgressLabel");
            this.downloadProgressLabel.Name = "downloadProgressLabel";
            // 
            // progressOrPiecesPanel
            // 
            this.progressOrPiecesPanel.Controls.Add(this.piecesGraph);
            this.progressOrPiecesPanel.Controls.Add(this.progressBar);
            resources.ApplyResources(this.progressOrPiecesPanel, "progressOrPiecesPanel");
            this.progressOrPiecesPanel.Name = "progressOrPiecesPanel";
            // 
            // piecesGraph
            // 
            this.piecesGraph.BackColor = System.Drawing.Color.LightSteelBlue;
            resources.ApplyResources(this.piecesGraph, "piecesGraph");
            this.piecesGraph.ForeColor = System.Drawing.Color.LimeGreen;
            this.piecesGraph.Name = "piecesGraph";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // percentageLabel
            // 
            resources.ApplyResources(this.percentageLabel, "percentageLabel");
            this.percentageLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.percentageLabel.Name = "percentageLabel";
            this.percentageLabel.ReadOnly = true;
            // 
            // generalTorrentNameGroupBox
            // 
            resources.ApplyResources(this.generalTorrentNameGroupBox, "generalTorrentNameGroupBox");
            this.generalTableLayoutPanel.SetColumnSpan(this.generalTorrentNameGroupBox, 3);
            this.generalTorrentNameGroupBox.Controls.Add(this.totalSizeLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.label1);
            this.generalTorrentNameGroupBox.Controls.Add(this.locationLabelLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.locationLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.piecesInfoLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.piecesInfoLabelLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.hashLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.hashLabelLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.timeElapsedLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.startedAtLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.uploadLimitLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.uploadRateLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.uploadedLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.remainingLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.downloadedLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.downloadSpeedLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.downloadLimitLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.statusLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.commentLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.errorLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.createdByLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.createdAtLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.leechersLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.ratioLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.seedersLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.label17);
            this.generalTorrentNameGroupBox.Controls.Add(this.label16);
            this.generalTorrentNameGroupBox.Controls.Add(this.labelForErrorLabel);
            this.generalTorrentNameGroupBox.Controls.Add(this.label15);
            this.generalTorrentNameGroupBox.Controls.Add(this.label14);
            this.generalTorrentNameGroupBox.Controls.Add(this.label13);
            this.generalTorrentNameGroupBox.Controls.Add(this.label10);
            this.generalTorrentNameGroupBox.Controls.Add(this.label12);
            this.generalTorrentNameGroupBox.Controls.Add(this.label8);
            this.generalTorrentNameGroupBox.Controls.Add(this.label5);
            this.generalTorrentNameGroupBox.Controls.Add(this.label4);
            this.generalTorrentNameGroupBox.Controls.Add(this.label11);
            this.generalTorrentNameGroupBox.Controls.Add(this.label9);
            this.generalTorrentNameGroupBox.Controls.Add(this.label7);
            this.generalTorrentNameGroupBox.Controls.Add(this.label6);
            this.generalTorrentNameGroupBox.Controls.Add(this.label3);
            this.generalTorrentNameGroupBox.Controls.Add(this.timeElapsedLabelLabel);
            this.generalTorrentNameGroupBox.Name = "generalTorrentNameGroupBox";
            this.generalTorrentNameGroupBox.TabStop = false;
            // 
            // totalSizeLabel
            // 
            this.totalSizeLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.totalSizeLabel, "totalSizeLabel");
            this.totalSizeLabel.Name = "totalSizeLabel";
            this.totalSizeLabel.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // locationLabelLabel
            // 
            resources.ApplyResources(this.locationLabelLabel, "locationLabelLabel");
            this.locationLabelLabel.Name = "locationLabelLabel";
            // 
            // locationLabel
            // 
            this.locationLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.locationLabel, "locationLabel");
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.ReadOnly = true;
            // 
            // piecesInfoLabel
            // 
            this.piecesInfoLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.piecesInfoLabel, "piecesInfoLabel");
            this.piecesInfoLabel.Name = "piecesInfoLabel";
            this.piecesInfoLabel.ReadOnly = true;
            // 
            // piecesInfoLabelLabel
            // 
            resources.ApplyResources(this.piecesInfoLabelLabel, "piecesInfoLabelLabel");
            this.piecesInfoLabelLabel.Name = "piecesInfoLabelLabel";
            // 
            // hashLabel
            // 
            this.hashLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.hashLabel, "hashLabel");
            this.hashLabel.Name = "hashLabel";
            this.hashLabel.ReadOnly = true;
            // 
            // hashLabelLabel
            // 
            resources.ApplyResources(this.hashLabelLabel, "hashLabelLabel");
            this.hashLabelLabel.Name = "hashLabelLabel";
            // 
            // timeElapsedLabel
            // 
            this.timeElapsedLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.timeElapsedLabel, "timeElapsedLabel");
            this.timeElapsedLabel.Name = "timeElapsedLabel";
            this.timeElapsedLabel.ReadOnly = true;
            // 
            // startedAtLabel
            // 
            this.startedAtLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.startedAtLabel, "startedAtLabel");
            this.startedAtLabel.Name = "startedAtLabel";
            this.startedAtLabel.ReadOnly = true;
            // 
            // uploadLimitLabel
            // 
            this.uploadLimitLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.uploadLimitLabel, "uploadLimitLabel");
            this.uploadLimitLabel.Name = "uploadLimitLabel";
            this.uploadLimitLabel.ReadOnly = true;
            // 
            // uploadRateLabel
            // 
            this.uploadRateLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.uploadRateLabel, "uploadRateLabel");
            this.uploadRateLabel.Name = "uploadRateLabel";
            this.uploadRateLabel.ReadOnly = true;
            // 
            // uploadedLabel
            // 
            this.uploadedLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.uploadedLabel, "uploadedLabel");
            this.uploadedLabel.Name = "uploadedLabel";
            this.uploadedLabel.ReadOnly = true;
            // 
            // remainingLabel
            // 
            this.remainingLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.remainingLabel, "remainingLabel");
            this.remainingLabel.Name = "remainingLabel";
            this.remainingLabel.ReadOnly = true;
            // 
            // downloadedLabel
            // 
            this.downloadedLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.downloadedLabel, "downloadedLabel");
            this.downloadedLabel.Name = "downloadedLabel";
            this.downloadedLabel.ReadOnly = true;
            // 
            // downloadSpeedLabel
            // 
            this.downloadSpeedLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.downloadSpeedLabel, "downloadSpeedLabel");
            this.downloadSpeedLabel.Name = "downloadSpeedLabel";
            this.downloadSpeedLabel.ReadOnly = true;
            // 
            // downloadLimitLabel
            // 
            this.downloadLimitLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.downloadLimitLabel, "downloadLimitLabel");
            this.downloadLimitLabel.Name = "downloadLimitLabel";
            this.downloadLimitLabel.ReadOnly = true;
            this.downloadLimitLabel.Tag = "";
            // 
            // statusLabel
            // 
            this.statusLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.statusLabel, "statusLabel");
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.ReadOnly = true;
            // 
            // commentLabel
            // 
            this.commentLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.commentLabel, "commentLabel");
            this.commentLabel.Name = "commentLabel";
            this.commentLabel.ReadOnly = true;
            // 
            // errorLabel
            // 
            this.errorLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.errorLabel, "errorLabel");
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.ReadOnly = true;
            // 
            // createdByLabel
            // 
            this.createdByLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.createdByLabel, "createdByLabel");
            this.createdByLabel.Name = "createdByLabel";
            this.createdByLabel.ReadOnly = true;
            // 
            // createdAtLabel
            // 
            this.createdAtLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.createdAtLabel, "createdAtLabel");
            this.createdAtLabel.Name = "createdAtLabel";
            this.createdAtLabel.ReadOnly = true;
            // 
            // leechersLabel
            // 
            this.leechersLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.leechersLabel, "leechersLabel");
            this.leechersLabel.Name = "leechersLabel";
            this.leechersLabel.ReadOnly = true;
            // 
            // ratioLabel
            // 
            this.ratioLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.ratioLabel, "ratioLabel");
            this.ratioLabel.Name = "ratioLabel";
            this.ratioLabel.ReadOnly = true;
            // 
            // seedersLabel
            // 
            this.seedersLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.seedersLabel, "seedersLabel");
            this.seedersLabel.Name = "seedersLabel";
            this.seedersLabel.ReadOnly = true;
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // labelForErrorLabel
            // 
            resources.ApplyResources(this.labelForErrorLabel, "labelForErrorLabel");
            this.labelForErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.labelForErrorLabel.Name = "labelForErrorLabel";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
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
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
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
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // timeElapsedLabelLabel
            // 
            resources.ApplyResources(this.timeElapsedLabelLabel, "timeElapsedLabelLabel");
            this.timeElapsedLabelLabel.Name = "timeElapsedLabelLabel";
            // 
            // trackersTabPage
            // 
            this.trackersTabPage.Controls.Add(this.trackersTorrentNameGroupBox);
            resources.ApplyResources(this.trackersTabPage, "trackersTabPage");
            this.trackersTabPage.Name = "trackersTabPage";
            this.trackersTabPage.UseVisualStyleBackColor = true;
            // 
            // trackersTorrentNameGroupBox
            // 
            this.trackersTorrentNameGroupBox.Controls.Add(this.trackersListView);
            resources.ApplyResources(this.trackersTorrentNameGroupBox, "trackersTorrentNameGroupBox");
            this.trackersTorrentNameGroupBox.Name = "trackersTorrentNameGroupBox";
            this.trackersTorrentNameGroupBox.TabStop = false;
            // 
            // trackersListView
            // 
            this.trackersListView.AllowColumnReorder = true;
            this.trackersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.trackersTierCol,
            this.trackersAnnounceUrlCol,
            this.trackersStatusCol,
            this.trackersUpdateInCol,
            this.trackersSeedsCol,
            this.trackersLeechersCol,
            this.trackersDownloadedCol});
            resources.ApplyResources(this.trackersListView, "trackersListView");
            this.trackersListView.FullRowSelect = true;
            this.trackersListView.HideSelection = false;
            this.trackersListView.Name = "trackersListView";
            this.trackersListView.UseCompatibleStateImageBehavior = false;
            this.trackersListView.View = System.Windows.Forms.View.Details;
            this.trackersListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.torrentDetailsTabListView_KeyDown);
            // 
            // trackersTierCol
            // 
            resources.ApplyResources(this.trackersTierCol, "trackersTierCol");
            // 
            // trackersAnnounceUrlCol
            // 
            resources.ApplyResources(this.trackersAnnounceUrlCol, "trackersAnnounceUrlCol");
            // 
            // trackersStatusCol
            // 
            resources.ApplyResources(this.trackersStatusCol, "trackersStatusCol");
            // 
            // trackersUpdateInCol
            // 
            resources.ApplyResources(this.trackersUpdateInCol, "trackersUpdateInCol");
            // 
            // trackersSeedsCol
            // 
            resources.ApplyResources(this.trackersSeedsCol, "trackersSeedsCol");
            // 
            // trackersLeechersCol
            // 
            resources.ApplyResources(this.trackersLeechersCol, "trackersLeechersCol");
            // 
            // trackersDownloadedCol
            // 
            resources.ApplyResources(this.trackersDownloadedCol, "trackersDownloadedCol");
            // 
            // filesTabPage
            // 
            this.filesTabPage.Controls.Add(this.filesTorrentNameGroupBox);
            resources.ApplyResources(this.filesTabPage, "filesTabPage");
            this.filesTabPage.Name = "filesTabPage";
            this.filesTabPage.UseVisualStyleBackColor = true;
            // 
            // filesTorrentNameGroupBox
            // 
            this.filesTorrentNameGroupBox.Controls.Add(this.filesListView);
            resources.ApplyResources(this.filesTorrentNameGroupBox, "filesTorrentNameGroupBox");
            this.filesTorrentNameGroupBox.Name = "filesTorrentNameGroupBox";
            this.filesTorrentNameGroupBox.TabStop = false;
            // 
            // filesListView
            // 
            this.filesListView.AllowColumnReorder = true;
            this.filesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.filesPathCol,
            this.filesTypeCol,
            this.filesSizeCol,
            this.filesDoneCol,
            this.filesPercentCol,
            this.filesSkipCol,
            this.filesPriorityCol});
            resources.ApplyResources(this.filesListView, "filesListView");
            this.filesListView.FullRowSelect = true;
            this.filesListView.HideSelection = false;
            this.filesListView.Name = "filesListView";
            this.filesListView.OwnerDraw = true;
            this.filesListView.ShowItemToolTips = true;
            this.filesListView.SmallImageList = this.fileIconImageList;
            this.filesListView.UseCompatibleStateImageBehavior = false;
            this.filesListView.View = System.Windows.Forms.View.Details;
            this.filesListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.torrentListView_DrawColumnHeader);
            this.filesListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.torrentListView_DrawItem);
            this.filesListView.SelectedIndexChanged += new System.EventHandler(this.filesListView_SelectedIndexChanged);
            this.filesListView.DoubleClick += new System.EventHandler(this.filesListView_DoubleClick);
            this.filesListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.filesListView_ColumnClick);
            this.filesListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.torrentDetailsTabListView_KeyDown);
            this.filesListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.filesListView_DrawSubItem);
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
            // filesDoneCol
            // 
            resources.ApplyResources(this.filesDoneCol, "filesDoneCol");
            // 
            // filesPercentCol
            // 
            resources.ApplyResources(this.filesPercentCol, "filesPercentCol");
            // 
            // filesSkipCol
            // 
            resources.ApplyResources(this.filesSkipCol, "filesSkipCol");
            // 
            // filesPriorityCol
            // 
            resources.ApplyResources(this.filesPriorityCol, "filesPriorityCol");
            // 
            // fileIconImageList
            // 
            this.fileIconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.fileIconImageList, "fileIconImageList");
            this.fileIconImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // peersTabPage
            // 
            this.peersTabPage.Controls.Add(this.peersTorrentNameGroupBox);
            resources.ApplyResources(this.peersTabPage, "peersTabPage");
            this.peersTabPage.Name = "peersTabPage";
            this.peersTabPage.UseVisualStyleBackColor = true;
            // 
            // peersTorrentNameGroupBox
            // 
            this.peersTorrentNameGroupBox.Controls.Add(this.peersListView);
            resources.ApplyResources(this.peersTorrentNameGroupBox, "peersTorrentNameGroupBox");
            this.peersTorrentNameGroupBox.Name = "peersTorrentNameGroupBox";
            this.peersTorrentNameGroupBox.TabStop = false;
            // 
            // peersListView
            // 
            this.peersListView.AllowColumnReorder = true;
            this.peersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.peersIpAddressCol,
            this.peersHostnameCol,
            this.peersCountryCol,
            this.peersFlagsCol,
            this.peersClientCol,
            this.peersProgressCol,
            this.peersDownSpeedCol,
            this.peersUpSpeedCol});
            resources.ApplyResources(this.peersListView, "peersListView");
            this.peersListView.FullRowSelect = true;
            this.peersListView.HideSelection = false;
            this.peersListView.Name = "peersListView";
            this.peersListView.OwnerDraw = true;
            this.peersListView.ShowItemToolTips = true;
            this.peersListView.UseCompatibleStateImageBehavior = false;
            this.peersListView.View = System.Windows.Forms.View.Details;
            this.peersListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.torrentListView_DrawColumnHeader);
            this.peersListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.torrentListView_DrawItem);
            this.peersListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.peersListView_ColumnClick);
            this.peersListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.torrentDetailsTabListView_KeyDown);
            this.peersListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.peersListView_DrawSubItem);
            // 
            // peersIpAddressCol
            // 
            resources.ApplyResources(this.peersIpAddressCol, "peersIpAddressCol");
            // 
            // peersHostnameCol
            // 
            resources.ApplyResources(this.peersHostnameCol, "peersHostnameCol");
            // 
            // peersCountryCol
            // 
            resources.ApplyResources(this.peersCountryCol, "peersCountryCol");
            // 
            // peersFlagsCol
            // 
            resources.ApplyResources(this.peersFlagsCol, "peersFlagsCol");
            // 
            // peersClientCol
            // 
            resources.ApplyResources(this.peersClientCol, "peersClientCol");
            // 
            // peersProgressCol
            // 
            resources.ApplyResources(this.peersProgressCol, "peersProgressCol");
            // 
            // peersDownSpeedCol
            // 
            resources.ApplyResources(this.peersDownSpeedCol, "peersDownSpeedCol");
            // 
            // peersUpSpeedCol
            // 
            resources.ApplyResources(this.peersUpSpeedCol, "peersUpSpeedCol");
            // 
            // speedTabPage
            // 
            this.speedTabPage.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.speedTabPage, "speedTabPage");
            this.speedTabPage.Name = "speedTabPage";
            this.speedTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label20);
            this.splitContainer1.Panel2.Controls.Add(this.label19);
            this.splitContainer1.Panel2.Controls.Add(this.speedGraph);
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label18);
            this.splitContainer2.Panel2.Controls.Add(this.speedResComboBox);
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // speedResComboBox
            // 
            this.speedResComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.speedResComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.speedResComboBox, "speedResComboBox");
            this.speedResComboBox.Name = "speedResComboBox";
            this.speedResComboBox.SelectedIndexChanged += new System.EventHandler(this.SpeedResComboBox_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.MidnightBlue;
            resources.ApplyResources(this.label20, "label20");
            this.label20.ForeColor = System.Drawing.Color.Green;
            this.label20.Name = "label20";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.MidnightBlue;
            resources.ApplyResources(this.label19, "label19");
            this.label19.ForeColor = System.Drawing.Color.Yellow;
            this.label19.Name = "label19";
            // 
            // speedGraph
            // 
            this.speedGraph.AutoAdjustPeek = true;
            this.speedGraph.BackColor = System.Drawing.Color.MidnightBlue;
            resources.ApplyResources(this.speedGraph, "speedGraph");
            this.speedGraph.GridColor = System.Drawing.Color.LightBlue;
            this.speedGraph.GridDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.speedGraph.GridSize = ((ushort)(50));
            this.speedGraph.HighQuality = true;
            this.speedGraph.LineInterval = 5F;
            this.speedGraph.MaxLabel = "Max";
            this.speedGraph.MaxPeekMagnitude = 100;
            this.speedGraph.MinLabel = "";
            this.speedGraph.MinPeekMagnitude = 0;
            this.speedGraph.Name = "speedGraph";
            this.speedGraph.ShowGrid = true;
            this.speedGraph.ShowLabels = true;
            this.speedGraph.TextColor = System.Drawing.Color.White;
            // 
            // tabControlImageList
            // 
            this.tabControlImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.tabControlImageList, "tabControlImageList");
            this.tabControlImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripVersionLabel});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            resources.ApplyResources(this.toolStripStatusLabel, "toolStripStatusLabel");
            this.toolStripStatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Spring = true;
            // 
            // toolStripVersionLabel
            // 
            resources.ApplyResources(this.toolStripVersionLabel, "toolStripVersionLabel");
            this.toolStripVersionLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripVersionLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripVersionLabel.Name = "toolStripVersionLabel";
            // 
            // toolStrip
            // 
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectButton,
            this.disconnectButton,
            this.toolbarToolStripSeparator1,
            this.addTorrentButton,
            this.addWebTorrentButton,
            this.toolbarToolStripSeparator2,
            this.startTorrentButton,
            this.pauseTorrentButton,
            this.recheckTorrentButton,
            this.configureTorrentButton,
            this.removeTorrentButton,
            this.removeAndDeleteButton,
            this.reannounceButton,
            this.openNetworkShareButton,
            this.remoteCmdButton,
            this.toolbarToolStripSeparator3,
            this.AltSpeedButton,
            this.toolbarToolStripSeparator4,
            this.localConfigureButton,
            this.remoteConfigureButton,
            this.sessionStatsButton,
            this.toolbarToolStripSeparator5,
            this.RssButton,
            this.FilterTorrentTextBox,
            this.FilterTorrentLabel});
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Stretch = true;
            // 
            // connectButton
            // 
            this.connectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.connectButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.connect;
            resources.ApplyResources(this.connectButton, "connectButton");
            this.connectButton.Name = "connectButton";
            this.connectButton.ButtonClick += new System.EventHandler(this.connectButton_Click);
            this.connectButton.DropDownOpening += new System.EventHandler(this.connectButton_DropDownOpening);
            // 
            // disconnectButton
            // 
            this.disconnectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.disconnectButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.disconnect;
            resources.ApplyResources(this.disconnectButton, "disconnectButton");
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // toolbarToolStripSeparator1
            // 
            this.toolbarToolStripSeparator1.Name = "toolbarToolStripSeparator1";
            resources.ApplyResources(this.toolbarToolStripSeparator1, "toolbarToolStripSeparator1");
            // 
            // addTorrentButton
            // 
            this.addTorrentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addTorrentButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.edit_add;
            resources.ApplyResources(this.addTorrentButton, "addTorrentButton");
            this.addTorrentButton.Name = "addTorrentButton";
            this.addTorrentButton.Click += new System.EventHandler(this.addTorrentButton_Click);
            // 
            // addWebTorrentButton
            // 
            this.addWebTorrentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addWebTorrentButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.net_add;
            resources.ApplyResources(this.addWebTorrentButton, "addWebTorrentButton");
            this.addWebTorrentButton.Name = "addWebTorrentButton";
            this.addWebTorrentButton.Click += new System.EventHandler(this.addWebTorrentButton_Click);
            // 
            // toolbarToolStripSeparator2
            // 
            this.toolbarToolStripSeparator2.Name = "toolbarToolStripSeparator2";
            resources.ApplyResources(this.toolbarToolStripSeparator2, "toolbarToolStripSeparator2");
            // 
            // startTorrentButton
            // 
            this.startTorrentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startTorrentButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.player_play_all;
            resources.ApplyResources(this.startTorrentButton, "startTorrentButton");
            this.startTorrentButton.Name = "startTorrentButton";
            this.startTorrentButton.Click += new System.EventHandler(this.startTorrentButton_Click);
            // 
            // pauseTorrentButton
            // 
            this.pauseTorrentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pauseTorrentButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.player_pause_all;
            resources.ApplyResources(this.pauseTorrentButton, "pauseTorrentButton");
            this.pauseTorrentButton.Name = "pauseTorrentButton";
            this.pauseTorrentButton.Click += new System.EventHandler(this.pauseTorrentButton_Click);
            // 
            // recheckTorrentButton
            // 
            this.recheckTorrentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.recheckTorrentButton, "recheckTorrentButton");
            this.recheckTorrentButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.player_reload;
            this.recheckTorrentButton.Name = "recheckTorrentButton";
            this.recheckTorrentButton.Click += new System.EventHandler(this.recheckTorrentButton_Click);
            // 
            // configureTorrentButton
            // 
            this.configureTorrentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.configureTorrentButton, "configureTorrentButton");
            this.configureTorrentButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.properties;
            this.configureTorrentButton.Name = "configureTorrentButton";
            this.configureTorrentButton.Click += new System.EventHandler(this.ShowTorrentPropsHandler);
            // 
            // removeTorrentButton
            // 
            this.removeTorrentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.removeTorrentButton, "removeTorrentButton");
            this.removeTorrentButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.remove;
            this.removeTorrentButton.Name = "removeTorrentButton";
            this.removeTorrentButton.Click += new System.EventHandler(this.removeTorrentButton_Click);
            // 
            // removeAndDeleteButton
            // 
            this.removeAndDeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.removeAndDeleteButton, "removeAndDeleteButton");
            this.removeAndDeleteButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.remove_and_delete;
            this.removeAndDeleteButton.Name = "removeAndDeleteButton";
            this.removeAndDeleteButton.Click += new System.EventHandler(this.removeAndDeleteButton_Click);
            // 
            // reannounceButton
            // 
            this.reannounceButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reannounceButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.recentlyActiveToolStripMenuItem});
            resources.ApplyResources(this.reannounceButton, "reannounceButton");
            this.reannounceButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.reannounce;
            this.reannounceButton.Name = "reannounceButton";
            this.reannounceButton.ButtonClick += new System.EventHandler(this.reannounceButton_ButtonClick);
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            resources.ApplyResources(this.allToolStripMenuItem, "allToolStripMenuItem");
            this.allToolStripMenuItem.Click += new System.EventHandler(this.reannounceAllToolStripMenuItem_Click);
            // 
            // recentlyActiveToolStripMenuItem
            // 
            this.recentlyActiveToolStripMenuItem.Name = "recentlyActiveToolStripMenuItem";
            resources.ApplyResources(this.recentlyActiveToolStripMenuItem, "recentlyActiveToolStripMenuItem");
            this.recentlyActiveToolStripMenuItem.Click += new System.EventHandler(this.recentlyActiveToolStripMenuItem_Click);
            // 
            // openNetworkShareButton
            // 
            this.openNetworkShareButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.openNetworkShareButton, "openNetworkShareButton");
            this.openNetworkShareButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.samba;
            this.openNetworkShareButton.Name = "openNetworkShareButton";
            this.openNetworkShareButton.Click += new System.EventHandler(this.openNetworkShareButton_Click);
            // 
            // remoteCmdButton
            // 
            this.remoteCmdButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.remoteCmdButton, "remoteCmdButton");
            this.remoteCmdButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.openterm;
            this.remoteCmdButton.Name = "remoteCmdButton";
            this.remoteCmdButton.Click += new System.EventHandler(this.runCmdButton_Click);
            // 
            // toolbarToolStripSeparator3
            // 
            this.toolbarToolStripSeparator3.Name = "toolbarToolStripSeparator3";
            resources.ApplyResources(this.toolbarToolStripSeparator3, "toolbarToolStripSeparator3");
            // 
            // AltSpeedButton
            // 
            this.AltSpeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AltSpeedButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.altspeed_off;
            resources.ApplyResources(this.AltSpeedButton, "AltSpeedButton");
            this.AltSpeedButton.Name = "AltSpeedButton";
            this.AltSpeedButton.Click += new System.EventHandler(this.AltSpeedButton_Click);
            // 
            // toolbarToolStripSeparator4
            // 
            this.toolbarToolStripSeparator4.Name = "toolbarToolStripSeparator4";
            resources.ApplyResources(this.toolbarToolStripSeparator4, "toolbarToolStripSeparator4");
            // 
            // localConfigureButton
            // 
            this.localConfigureButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.localConfigureButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.configure;
            resources.ApplyResources(this.localConfigureButton, "localConfigureButton");
            this.localConfigureButton.Name = "localConfigureButton";
            this.localConfigureButton.Click += new System.EventHandler(this.localConfigureButton_Click);
            // 
            // remoteConfigureButton
            // 
            this.remoteConfigureButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.remoteConfigureButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.netconfigure;
            resources.ApplyResources(this.remoteConfigureButton, "remoteConfigureButton");
            this.remoteConfigureButton.Name = "remoteConfigureButton";
            this.remoteConfigureButton.Click += new System.EventHandler(this.remoteConfigureButton_Click);
            // 
            // sessionStatsButton
            // 
            this.sessionStatsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sessionStatsButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.hwinfo;
            resources.ApplyResources(this.sessionStatsButton, "sessionStatsButton");
            this.sessionStatsButton.Name = "sessionStatsButton";
            this.sessionStatsButton.Click += new System.EventHandler(this.sessionStatsButton_Click);
            // 
            // toolbarToolStripSeparator5
            // 
            this.toolbarToolStripSeparator5.Name = "toolbarToolStripSeparator5";
            resources.ApplyResources(this.toolbarToolStripSeparator5, "toolbarToolStripSeparator5");
            // 
            // RssButton
            // 
            this.RssButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RssButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.feed_icon;
            resources.ApplyResources(this.RssButton, "RssButton");
            this.RssButton.Name = "RssButton";
            this.RssButton.Click += new System.EventHandler(this.RssButton_Click);
            // 
            // FilterTorrentTextBox
            // 
            this.FilterTorrentTextBox.AcceptsTab = true;
            this.FilterTorrentTextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.FilterTorrentTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FilterTorrentTextBox.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            resources.ApplyResources(this.FilterTorrentTextBox, "FilterTorrentTextBox");
            this.FilterTorrentTextBox.Name = "FilterTorrentTextBox";
            this.FilterTorrentTextBox.TextChanged += new System.EventHandler(this.FilterTorrentTextBox_TextChanged);
            // 
            // FilterTorrentLabel
            // 
            this.FilterTorrentLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.FilterTorrentLabel.Name = "FilterTorrentLabel";
            resources.ApplyResources(this.FilterTorrentLabel, "FilterTorrentLabel");
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.torrentToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.fileMenuToolStripSeparator1,
            this.addTorrentToolStripMenuItem,
            this.addTorrentWithOptionsToolStripMenuItem,
            this.addTorrentFromUrlToolStripMenuItem,
            this.fileMenuToolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.connect;
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            resources.ApplyResources(this.connectToolStripMenuItem, "connectToolStripMenuItem");
            this.connectToolStripMenuItem.DropDownOpening += new System.EventHandler(this.connectButton_DropDownOpening);
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            resources.ApplyResources(this.disconnectToolStripMenuItem, "disconnectToolStripMenuItem");
            this.disconnectToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.disconnect;
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // fileMenuToolStripSeparator1
            // 
            this.fileMenuToolStripSeparator1.Name = "fileMenuToolStripSeparator1";
            resources.ApplyResources(this.fileMenuToolStripSeparator1, "fileMenuToolStripSeparator1");
            // 
            // addTorrentToolStripMenuItem
            // 
            resources.ApplyResources(this.addTorrentToolStripMenuItem, "addTorrentToolStripMenuItem");
            this.addTorrentToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.edit_add;
            this.addTorrentToolStripMenuItem.Name = "addTorrentToolStripMenuItem";
            this.addTorrentToolStripMenuItem.Click += new System.EventHandler(this.addTorrentButton_Click);
            // 
            // addTorrentWithOptionsToolStripMenuItem
            // 
            resources.ApplyResources(this.addTorrentWithOptionsToolStripMenuItem, "addTorrentWithOptionsToolStripMenuItem");
            this.addTorrentWithOptionsToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.edit_add;
            this.addTorrentWithOptionsToolStripMenuItem.Name = "addTorrentWithOptionsToolStripMenuItem";
            this.addTorrentWithOptionsToolStripMenuItem.Click += new System.EventHandler(this.addTorrentWithOptionsToolStripMenuItem_Click);
            // 
            // addTorrentFromUrlToolStripMenuItem
            // 
            resources.ApplyResources(this.addTorrentFromUrlToolStripMenuItem, "addTorrentFromUrlToolStripMenuItem");
            this.addTorrentFromUrlToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.net_add;
            this.addTorrentFromUrlToolStripMenuItem.Name = "addTorrentFromUrlToolStripMenuItem";
            this.addTorrentFromUrlToolStripMenuItem.Click += new System.EventHandler(this.addWebTorrentButton_Click);
            // 
            // fileMenuToolStripSeparator2
            // 
            this.fileMenuToolStripSeparator2.Name = "fileMenuToolStripSeparator2";
            resources.ApplyResources(this.fileMenuToolStripSeparator2, "fileMenuToolStripSeparator2");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localSettingsToolStripMenuItem,
            this.remoteSettingsToolStripMenuItem,
            this.optionsMenuToolStripSeparator1,
            this.languageToolStripMenuItem,
            this.optionsMenuToolStripSeparator2,
            this.importLocalSettingsToolStripMenuItem,
            this.exportLocalSettingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            // 
            // localSettingsToolStripMenuItem
            // 
            this.localSettingsToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.configure;
            this.localSettingsToolStripMenuItem.Name = "localSettingsToolStripMenuItem";
            resources.ApplyResources(this.localSettingsToolStripMenuItem, "localSettingsToolStripMenuItem");
            this.localSettingsToolStripMenuItem.Click += new System.EventHandler(this.localConfigureButton_Click);
            // 
            // remoteSettingsToolStripMenuItem
            // 
            resources.ApplyResources(this.remoteSettingsToolStripMenuItem, "remoteSettingsToolStripMenuItem");
            this.remoteSettingsToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.netconfigure;
            this.remoteSettingsToolStripMenuItem.Name = "remoteSettingsToolStripMenuItem";
            this.remoteSettingsToolStripMenuItem.Click += new System.EventHandler(this.remoteConfigureButton_Click);
            // 
            // optionsMenuToolStripSeparator1
            // 
            this.optionsMenuToolStripSeparator1.Name = "optionsMenuToolStripSeparator1";
            resources.ApplyResources(this.optionsMenuToolStripSeparator1, "optionsMenuToolStripSeparator1");
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            // 
            // optionsMenuToolStripSeparator2
            // 
            this.optionsMenuToolStripSeparator2.Name = "optionsMenuToolStripSeparator2";
            resources.ApplyResources(this.optionsMenuToolStripSeparator2, "optionsMenuToolStripSeparator2");
            // 
            // importLocalSettingsToolStripMenuItem
            // 
            this.importLocalSettingsToolStripMenuItem.Name = "importLocalSettingsToolStripMenuItem";
            resources.ApplyResources(this.importLocalSettingsToolStripMenuItem, "importLocalSettingsToolStripMenuItem");
            this.importLocalSettingsToolStripMenuItem.Click += new System.EventHandler(this.importLocalSettingsToolStripMenuItem_Click);
            // 
            // exportLocalSettingsToolStripMenuItem
            // 
            this.exportLocalSettingsToolStripMenuItem.Name = "exportLocalSettingsToolStripMenuItem";
            resources.ApplyResources(this.exportLocalSettingsToolStripMenuItem, "exportLocalSettingsToolStripMenuItem");
            this.exportLocalSettingsToolStripMenuItem.Click += new System.EventHandler(this.exportLocalSettingsToolStripMenuItem_Click);
            // 
            // torrentToolStripMenuItem
            // 
            this.torrentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.torrentMenuToolStripSeparator1,
            this.startToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.recheckToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.removeDeleteToolStripMenuItem,
            this.reannounceToolStripMenuItem,
            this.moveTorrentDataToolStripMenuItem,
            this.openNetworkShareToolStripMenuItem,
            this.cSVInfoToClipboardToolStripMenuItem,
            this.torrentMenuToolStripSeparator2,
            this.startAllToolStripMenuItem,
            this.stopAllToolStripMenuItem});
            resources.ApplyResources(this.torrentToolStripMenuItem, "torrentToolStripMenuItem");
            this.torrentToolStripMenuItem.Name = "torrentToolStripMenuItem";
            this.torrentToolStripMenuItem.Click += new System.EventHandler(this.reannounceButton_ButtonClick);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.find;
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            resources.ApplyResources(this.findToolStripMenuItem, "findToolStripMenuItem");
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // torrentMenuToolStripSeparator1
            // 
            this.torrentMenuToolStripSeparator1.Name = "torrentMenuToolStripSeparator1";
            resources.ApplyResources(this.torrentMenuToolStripSeparator1, "torrentMenuToolStripSeparator1");
            // 
            // startToolStripMenuItem
            // 
            resources.ApplyResources(this.startToolStripMenuItem, "startToolStripMenuItem");
            this.startToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.player_play;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startTorrentButton_Click);
            // 
            // pauseToolStripMenuItem
            // 
            resources.ApplyResources(this.pauseToolStripMenuItem, "pauseToolStripMenuItem");
            this.pauseToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.player_pause;
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseTorrentButton_Click);
            // 
            // recheckToolStripMenuItem
            // 
            resources.ApplyResources(this.recheckToolStripMenuItem, "recheckToolStripMenuItem");
            this.recheckToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.player_reload;
            this.recheckToolStripMenuItem.Name = "recheckToolStripMenuItem";
            this.recheckToolStripMenuItem.Click += new System.EventHandler(this.recheckTorrentButton_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            resources.ApplyResources(this.propertiesToolStripMenuItem, "propertiesToolStripMenuItem");
            this.propertiesToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.properties;
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.ShowTorrentPropsHandler);
            // 
            // removeToolStripMenuItem
            // 
            resources.ApplyResources(this.removeToolStripMenuItem, "removeToolStripMenuItem");
            this.removeToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.remove;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeTorrentButton_Click);
            // 
            // removeDeleteToolStripMenuItem
            // 
            resources.ApplyResources(this.removeDeleteToolStripMenuItem, "removeDeleteToolStripMenuItem");
            this.removeDeleteToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.remove_and_delete;
            this.removeDeleteToolStripMenuItem.Name = "removeDeleteToolStripMenuItem";
            this.removeDeleteToolStripMenuItem.Click += new System.EventHandler(this.removeAndDeleteButton_Click);
            // 
            // reannounceToolStripMenuItem
            // 
            resources.ApplyResources(this.reannounceToolStripMenuItem, "reannounceToolStripMenuItem");
            this.reannounceToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.reannounce;
            this.reannounceToolStripMenuItem.Name = "reannounceToolStripMenuItem";
            this.reannounceToolStripMenuItem.Click += new System.EventHandler(this.reannounceButton_ButtonClick);
            // 
            // moveTorrentDataToolStripMenuItem
            // 
            resources.ApplyResources(this.moveTorrentDataToolStripMenuItem, "moveTorrentDataToolStripMenuItem");
            this.moveTorrentDataToolStripMenuItem.Name = "moveTorrentDataToolStripMenuItem";
            this.moveTorrentDataToolStripMenuItem.Click += new System.EventHandler(this.moveTorrentDataToolStripMenuItem_Click);
            // 
            // openNetworkShareToolStripMenuItem
            // 
            resources.ApplyResources(this.openNetworkShareToolStripMenuItem, "openNetworkShareToolStripMenuItem");
            this.openNetworkShareToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.samba;
            this.openNetworkShareToolStripMenuItem.Name = "openNetworkShareToolStripMenuItem";
            this.openNetworkShareToolStripMenuItem.Click += new System.EventHandler(this.openNetworkShareButton_Click);
            // 
            // cSVInfoToClipboardToolStripMenuItem
            // 
            resources.ApplyResources(this.cSVInfoToClipboardToolStripMenuItem, "cSVInfoToClipboardToolStripMenuItem");
            this.cSVInfoToClipboardToolStripMenuItem.Name = "cSVInfoToClipboardToolStripMenuItem";
            this.cSVInfoToClipboardToolStripMenuItem.Click += new System.EventHandler(this.TorrentsToClipboardHandler);
            // 
            // torrentMenuToolStripSeparator2
            // 
            this.torrentMenuToolStripSeparator2.Name = "torrentMenuToolStripSeparator2";
            resources.ApplyResources(this.torrentMenuToolStripSeparator2, "torrentMenuToolStripSeparator2");
            // 
            // startAllToolStripMenuItem
            // 
            this.startAllToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.player_play_all;
            this.startAllToolStripMenuItem.Name = "startAllToolStripMenuItem";
            resources.ApplyResources(this.startAllToolStripMenuItem, "startAllToolStripMenuItem");
            this.startAllToolStripMenuItem.Click += new System.EventHandler(this.startAllMenuItem_Click);
            // 
            // stopAllToolStripMenuItem
            // 
            this.stopAllToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.player_pause_all;
            this.stopAllToolStripMenuItem.Name = "stopAllToolStripMenuItem";
            resources.ApplyResources(this.stopAllToolStripMenuItem, "stopAllToolStripMenuItem");
            this.stopAllToolStripMenuItem.Click += new System.EventHandler(this.stopAllMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsPanelToolStripMenuItem,
            this.showCategoriesPanelToolStripMenuItem,
            this.viewMenuToolStripSeparator,
            this.statsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // showDetailsPanelToolStripMenuItem
            // 
            this.showDetailsPanelToolStripMenuItem.Name = "showDetailsPanelToolStripMenuItem";
            resources.ApplyResources(this.showDetailsPanelToolStripMenuItem, "showDetailsPanelToolStripMenuItem");
            this.showDetailsPanelToolStripMenuItem.Click += new System.EventHandler(this.showDetailsPanelToolStripMenuItem_Click);
            // 
            // showCategoriesPanelToolStripMenuItem
            // 
            this.showCategoriesPanelToolStripMenuItem.Name = "showCategoriesPanelToolStripMenuItem";
            resources.ApplyResources(this.showCategoriesPanelToolStripMenuItem, "showCategoriesPanelToolStripMenuItem");
            this.showCategoriesPanelToolStripMenuItem.Click += new System.EventHandler(this.categoriesPanelToolStripMenuItem_Click);
            // 
            // viewMenuToolStripSeparator
            // 
            this.viewMenuToolStripSeparator.Name = "viewMenuToolStripSeparator";
            resources.ApplyResources(this.viewMenuToolStripSeparator, "viewMenuToolStripSeparator");
            // 
            // statsToolStripMenuItem
            // 
            resources.ApplyResources(this.statsToolStripMenuItem, "statsToolStripMenuItem");
            this.statsToolStripMenuItem.Image = global::TransmissionRemoteDotnet.Properties.Resources.hwinfo;
            this.statsToolStripMenuItem.Name = "statsToolStripMenuItem";
            this.statsToolStripMenuItem.Click += new System.EventHandler(this.sessionStatsButton_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showErrorLogToolStripMenuItem,
            this.helpMenuToolStripSeparator,
            this.projectSiteToolStripMenuItem,
            this.checkForNewVersionToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // showErrorLogToolStripMenuItem
            // 
            this.showErrorLogToolStripMenuItem.Name = "showErrorLogToolStripMenuItem";
            resources.ApplyResources(this.showErrorLogToolStripMenuItem, "showErrorLogToolStripMenuItem");
            this.showErrorLogToolStripMenuItem.Click += new System.EventHandler(this.showErrorLogToolStripMenuItem_Click);
            // 
            // helpMenuToolStripSeparator
            // 
            this.helpMenuToolStripSeparator.Name = "helpMenuToolStripSeparator";
            resources.ApplyResources(this.helpMenuToolStripSeparator, "helpMenuToolStripSeparator");
            // 
            // projectSiteToolStripMenuItem
            // 
            this.projectSiteToolStripMenuItem.Name = "projectSiteToolStripMenuItem";
            resources.ApplyResources(this.projectSiteToolStripMenuItem, "projectSiteToolStripMenuItem");
            this.projectSiteToolStripMenuItem.Click += new System.EventHandler(this.projectSiteToolStripMenuItem_Click);
            // 
            // checkForNewVersionToolStripMenuItem
            // 
            this.checkForNewVersionToolStripMenuItem.Name = "checkForNewVersionToolStripMenuItem";
            resources.ApplyResources(this.checkForNewVersionToolStripMenuItem, "checkForNewVersionToolStripMenuItem");
            this.checkForNewVersionToolStripMenuItem.Click += new System.EventHandler(this.checkForNewVersionToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // notifyIcon
            // 
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // filesTimer
            // 
            this.filesTimer.Tick += new System.EventHandler(this.filesTimer_Tick);
            // 
            // refreshElapsedTimer
            // 
            this.refreshElapsedTimer.Interval = 1000;
            this.refreshElapsedTimer.Tick += new System.EventHandler(this.refreshElapsedTimer_Tick);
            // 
            // openTorrentFileDialog
            // 
            resources.ApplyResources(this.openTorrentFileDialog, "openTorrentFileDialog");
            this.openTorrentFileDialog.Multiselect = true;
            // 
            // toolStripImageList
            // 
            this.toolStripImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.toolStripImageList, "toolStripImageList");
            this.toolStripImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // trayIconImageList
            // 
            this.trayIconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.trayIconImageList, "trayIconImageList");
            this.trayIconImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // saveSettingsFileDialog
            // 
            this.saveSettingsFileDialog.DefaultExt = "json";
            this.saveSettingsFileDialog.FileName = "settings.json";
            resources.ApplyResources(this.saveSettingsFileDialog, "saveSettingsFileDialog");
            // 
            // openSettingsFileDialog
            // 
            this.openSettingsFileDialog.DefaultExt = "json";
            this.openSettingsFileDialog.FileName = "settings.json";
            resources.ApplyResources(this.openSettingsFileDialog, "openSettingsFileDialog");
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.VisibleChanged += new System.EventHandler(this.MainWindow_VisibleChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragEnter);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.torrentAndTabsSplitContainer.Panel1.ResumeLayout(false);
            this.torrentAndTabsSplitContainer.Panel2.ResumeLayout(false);
            this.torrentAndTabsSplitContainer.ResumeLayout(false);
            this.mainVerticalSplitContainer.Panel1.ResumeLayout(false);
            this.mainVerticalSplitContainer.Panel2.ResumeLayout(false);
            this.mainVerticalSplitContainer.ResumeLayout(false);
            this.torrentTabControl.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.generalTableLayoutPanel.ResumeLayout(false);
            this.generalTableLayoutPanel.PerformLayout();
            this.progressOrPiecesPanel.ResumeLayout(false);
            this.generalTorrentNameGroupBox.ResumeLayout(false);
            this.generalTorrentNameGroupBox.PerformLayout();
            this.trackersTabPage.ResumeLayout(false);
            this.trackersTorrentNameGroupBox.ResumeLayout(false);
            this.filesTabPage.ResumeLayout(false);
            this.filesTorrentNameGroupBox.ResumeLayout(false);
            this.peersTabPage.ResumeLayout(false);
            this.peersTorrentNameGroupBox.ResumeLayout(false);
            this.speedTabPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton remoteCmdButton;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.SplitContainer mainVerticalSplitContainer;
        private TransmissionRemoteDotnet.SplitContainerFix torrentAndTabsSplitContainer;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripButton addTorrentButton;
        private System.Windows.Forms.TabPage trackersTabPage;
        private System.Windows.Forms.TabPage filesTabPage;
        private System.Windows.Forms.ColumnHeader filesPathCol;
        private System.Windows.Forms.ColumnHeader filesTypeCol;
        private System.Windows.Forms.ColumnHeader filesSizeCol;
        private System.Windows.Forms.ColumnHeader filesDoneCol;
        private System.Windows.Forms.ColumnHeader filesPercentCol;
        private System.Windows.Forms.ColumnHeader filesSkipCol;
        private System.Windows.Forms.ColumnHeader filesPriorityCol;
        private System.Windows.Forms.ColumnHeader torrentNameCol;
        private System.Windows.Forms.ColumnHeader torrentNoCol;
        private System.Windows.Forms.ColumnHeader torrentSizeCol;
        private System.Windows.Forms.ColumnHeader torrentDoneCol;
        private System.Windows.Forms.ColumnHeader torrentStatusCol;
        private System.Windows.Forms.ColumnHeader torrentSeedsCol;
        private System.Windows.Forms.ColumnHeader torrentLeechersCol;
        private System.Windows.Forms.ColumnHeader torrentDownSpeedCol;
        private System.Windows.Forms.ColumnHeader torrentUpSpeedCol;
        private System.Windows.Forms.ColumnHeader torrentEtaCol;
        private System.Windows.Forms.ColumnHeader torrentUploadedCol;
        private System.Windows.Forms.ColumnHeader torrentRatioCol;
        private System.Windows.Forms.ColumnHeader torrentAddedAt;
        private System.Windows.Forms.ColumnHeader torrentCompletedAtCol;
        private System.Windows.Forms.ColumnHeader torrentTrackerCol;
        private System.Windows.Forms.ToolStripButton disconnectButton;
        private System.Windows.Forms.ToolStripButton addWebTorrentButton;
        private System.Windows.Forms.ToolStripSeparator toolbarToolStripSeparator2;
        private System.Windows.Forms.ToolStripButton removeTorrentButton;
        private System.Windows.Forms.ToolStripSeparator toolbarToolStripSeparator3;
        private System.Windows.Forms.ToolStripButton localConfigureButton;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton remoteConfigureButton;
        private System.Windows.Forms.ToolStripMenuItem addTorrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTorrentFromUrlToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileMenuToolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        public System.Windows.Forms.ToolStripSplitButton connectButton;
        public System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripVersionLabel;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripSeparator toolbarToolStripSeparator1;
        public ListViewNF torrentListView;
        private System.Windows.Forms.ToolStripMenuItem localSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remoteSettingsToolStripMenuItem;
        public TransmissionRemoteDotnet.GListBox stateListBox;
        public TransmissionRemoteDotnet.ListViewNF filesListView;
        public System.Windows.Forms.Timer filesTimer;
        private System.Windows.Forms.TabPage generalTabPage;
        private System.Windows.Forms.TableLayoutPanel generalTableLayoutPanel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel progressOrPiecesPanel;
        private TransmissionRemoteDotnet.SelectableLabel percentageLabel;
        private TransmissionRemoteDotnet.PiecesGraph piecesGraph;
        private System.Windows.Forms.Label downloadProgressLabel;
        public System.Windows.Forms.TabControl torrentTabControl;
        private TransmissionRemoteDotnet.ListViewNF trackersListView;
        private System.Windows.Forms.ColumnHeader trackersTierCol;
        private System.Windows.Forms.ColumnHeader trackersAnnounceUrlCol;
        private System.Windows.Forms.ColumnHeader trackersStatusCol;
        private System.Windows.Forms.ColumnHeader trackersUpdateInCol;
        private System.Windows.Forms.ColumnHeader trackersSeedsCol;
        private System.Windows.Forms.ColumnHeader trackersLeechersCol;
        private System.Windows.Forms.ColumnHeader trackersDownloadedCol;
        private System.Windows.Forms.Label timeElapsedLabelLabel;
        private System.Windows.Forms.Timer refreshElapsedTimer;
        private System.Windows.Forms.GroupBox generalTorrentNameGroupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labelForErrorLabel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TabPage peersTabPage;
        private TransmissionRemoteDotnet.ListViewNF peersListView;
        private System.Windows.Forms.GroupBox peersTorrentNameGroupBox;
        private System.Windows.Forms.ColumnHeader peersIpAddressCol;
        private System.Windows.Forms.ColumnHeader peersHostnameCol;
        private System.Windows.Forms.ColumnHeader peersCountryCol;
        private System.Windows.Forms.ColumnHeader peersFlagsCol;
        private System.Windows.Forms.ColumnHeader peersClientCol;
        private System.Windows.Forms.ColumnHeader peersProgressCol;
        private System.Windows.Forms.ColumnHeader peersDownSpeedCol;
        private System.Windows.Forms.ColumnHeader peersUpSpeedCol;
        private System.Windows.Forms.GroupBox trackersTorrentNameGroupBox;
        private System.Windows.Forms.GroupBox filesTorrentNameGroupBox;
        internal System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileMenuToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem projectSiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showErrorLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator helpMenuToolStripSeparator;
        private System.Windows.Forms.TabPage speedTabPage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox speedResComboBox;
        private CustomUIControls.Graphing.C2DPushGraph speedGraph;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ImageList tabControlImageList;
        private System.Windows.Forms.ToolStripButton recheckTorrentButton;
        private System.Windows.Forms.ToolStripButton removeAndDeleteButton;
        private System.Windows.Forms.ToolStripSeparator viewMenuToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem statsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton sessionStatsButton;
        private System.Windows.Forms.ToolStripButton configureTorrentButton;
        private System.Windows.Forms.ToolStripMenuItem torrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recheckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeDeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator torrentMenuToolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem startAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton startTorrentButton;
        private System.Windows.Forms.ToolStripButton pauseTorrentButton;
        private System.Windows.Forms.ToolStripMenuItem checkForNewVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDetailsPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator optionsMenuToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator optionsMenuToolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem importLocalSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportLocalSettingsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveSettingsFileDialog;
        private System.Windows.Forms.OpenFileDialog openSettingsFileDialog;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton reannounceButton;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentlyActiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton openNetworkShareButton;
        private System.Windows.Forms.ToolStripMenuItem showCategoriesPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reannounceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openNetworkShareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveTorrentDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cSVInfoToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTorrentWithOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolbarToolStripSeparator5;
        private System.Windows.Forms.ToolStripButton RssButton;
        private System.Windows.Forms.ToolStripTextBox FilterTorrentTextBox;
        private System.Windows.Forms.ToolStripLabel FilterTorrentLabel;
        private System.Windows.Forms.OpenFileDialog openTorrentFileDialog;
        private SelectableLabel seedersLabel;
        private SelectableLabel createdByLabel;
        private SelectableLabel createdAtLabel;
        private SelectableLabel leechersLabel;
        private SelectableLabel ratioLabel;
        private SelectableLabel downloadedLabel;
        private SelectableLabel downloadSpeedLabel;
        private SelectableLabel downloadLimitLabel;
        private SelectableLabel statusLabel;
        private SelectableLabel commentLabel;
        private SelectableLabel errorLabel;
        private SelectableLabel uploadedLabel;
        private SelectableLabel remainingLabel;
        private SelectableLabel startedAtLabel;
        private SelectableLabel uploadLimitLabel;
        private SelectableLabel uploadRateLabel;
        private SelectableLabel timeElapsedLabel;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator torrentMenuToolStripSeparator1;
        private SelectableLabel hashLabel;
        private System.Windows.Forms.Label hashLabelLabel;
        private SelectableLabel piecesInfoLabel;
        private System.Windows.Forms.Label piecesInfoLabelLabel;
        private SelectableLabel locationLabel;
        private System.Windows.Forms.Label locationLabelLabel;
        private System.Windows.Forms.ToolStripButton AltSpeedButton;
        private System.Windows.Forms.ToolStripSeparator toolbarToolStripSeparator4;
        public System.Windows.Forms.ImageList fileIconImageList;
        private System.Windows.Forms.ImageList stateListBoxImageList;
        private System.Windows.Forms.ImageList toolStripImageList;
        private System.Windows.Forms.ImageList trayIconImageList;
        private SelectableLabel totalSizeLabel;
        private System.Windows.Forms.Label label1;
    }
}