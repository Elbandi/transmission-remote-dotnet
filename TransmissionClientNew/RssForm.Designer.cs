namespace TransmissionRemoteDotnet
{
    partial class RssForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RssForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.RefreshButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addTorrentButton = new System.Windows.Forms.ToolStripButton();
            this.addTorrentWithOptionsButton = new System.Windows.Forms.ToolStripButton();
            this.FeedImageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rssFeedsListView = new System.Windows.Forms.ListView();
            this.rssItemsListView = new System.Windows.Forms.ListView();
            this.titleColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.categoryColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.descriptionColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.pubdateColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Spring = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshButton,
            this.toolStripSeparator1,
            this.addTorrentButton,
            this.addTorrentWithOptionsButton});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // RefreshButton
            // 
            this.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.RefreshButton, "RefreshButton");
            this.RefreshButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.player_reload;
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // addTorrentButton
            // 
            this.addTorrentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.addTorrentButton, "addTorrentButton");
            this.addTorrentButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.edit_add;
            this.addTorrentButton.Name = "addTorrentButton";
            this.addTorrentButton.Click += new System.EventHandler(this.addTorrentButton_Click);
            // 
            // addTorrentWithOptionsButton
            // 
            this.addTorrentWithOptionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.addTorrentWithOptionsButton, "addTorrentWithOptionsButton");
            this.addTorrentWithOptionsButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.net_add;
            this.addTorrentWithOptionsButton.Name = "addTorrentWithOptionsButton";
            this.addTorrentWithOptionsButton.Click += new System.EventHandler(this.addTorrentWithOptionsButton_Click);
            // 
            // FeedImageList
            // 
            this.FeedImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.FeedImageList, "FeedImageList");
            this.FeedImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rssFeedsListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rssItemsListView);
            // 
            // rssFeedsListView
            // 
            resources.ApplyResources(this.rssFeedsListView, "rssFeedsListView");
            this.rssFeedsListView.FullRowSelect = true;
            this.rssFeedsListView.HideSelection = false;
            this.rssFeedsListView.LargeImageList = this.FeedImageList;
            this.rssFeedsListView.MultiSelect = false;
            this.rssFeedsListView.Name = "rssFeedsListView";
            this.rssFeedsListView.Scrollable = false;
            this.rssFeedsListView.ShowGroups = false;
            this.rssFeedsListView.SmallImageList = this.FeedImageList;
            this.rssFeedsListView.TileSize = new System.Drawing.Size(168, 24);
            this.rssFeedsListView.UseCompatibleStateImageBehavior = false;
            this.rssFeedsListView.View = System.Windows.Forms.View.Tile;
            this.rssFeedsListView.Resize += new System.EventHandler(this.rssFeedsListView_Resize);
            this.rssFeedsListView.SelectedIndexChanged += new System.EventHandler(this.rssFeedsListView_SelectedIndexChanged);
            this.rssFeedsListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.rssFeedsListView_ItemSelectionChanged);
            // 
            // rssItemsListView
            // 
            this.rssItemsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titleColumnHeader,
            this.categoryColumnHeader,
            this.descriptionColumnHeader,
            this.pubdateColumnHeader});
            resources.ApplyResources(this.rssItemsListView, "rssItemsListView");
            this.rssItemsListView.FullRowSelect = true;
            this.rssItemsListView.GridLines = true;
            this.rssItemsListView.HideSelection = false;
            this.rssItemsListView.MultiSelect = false;
            this.rssItemsListView.Name = "rssItemsListView";
            this.rssItemsListView.UseCompatibleStateImageBehavior = false;
            this.rssItemsListView.View = System.Windows.Forms.View.Details;
            this.rssItemsListView.SelectedIndexChanged += new System.EventHandler(this.rssItemsListView_SelectedIndexChanged);
            this.rssItemsListView.DoubleClick += new System.EventHandler(this.rssItemsListView_DoubleClick);
            this.rssItemsListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.rssItemsListView_ColumnClick);
            // 
            // titleColumnHeader
            // 
            resources.ApplyResources(this.titleColumnHeader, "titleColumnHeader");
            // 
            // categoryColumnHeader
            // 
            resources.ApplyResources(this.categoryColumnHeader, "categoryColumnHeader");
            // 
            // descriptionColumnHeader
            // 
            resources.ApplyResources(this.descriptionColumnHeader, "descriptionColumnHeader");
            // 
            // pubdateColumnHeader
            // 
            resources.ApplyResources(this.pubdateColumnHeader, "pubdateColumnHeader");
            // 
            // RssForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "RssForm";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton RefreshButton;
        private System.Windows.Forms.ImageList FeedImageList;
        private System.Windows.Forms.ToolStripButton addTorrentButton;
        private System.Windows.Forms.ToolStripButton addTorrentWithOptionsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView rssFeedsListView;
        private System.Windows.Forms.ListView rssItemsListView;
        private System.Windows.Forms.ColumnHeader titleColumnHeader;
        private System.Windows.Forms.ColumnHeader categoryColumnHeader;
        private System.Windows.Forms.ColumnHeader descriptionColumnHeader;
        private System.Windows.Forms.ColumnHeader pubdateColumnHeader;
    }
}