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
            this.statusStrip1.Location = new System.Drawing.Point(0, 388);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(944, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(827, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(944, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // RefreshButton
            // 
            this.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RefreshButton.Enabled = false;
            this.RefreshButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.player_reload;
            this.RefreshButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(36, 36);
            this.RefreshButton.Text = "toolStripButton1";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // addTorrentButton
            // 
            this.addTorrentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addTorrentButton.Enabled = false;
            this.addTorrentButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.edit_add;
            this.addTorrentButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addTorrentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTorrentButton.Name = "addTorrentButton";
            this.addTorrentButton.Size = new System.Drawing.Size(36, 36);
            this.addTorrentButton.Text = "toolStripButton2";
            this.addTorrentButton.Click += new System.EventHandler(this.addTorrentButton_Click);
            // 
            // addTorrentWithOptionsButton
            // 
            this.addTorrentWithOptionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addTorrentWithOptionsButton.Enabled = false;
            this.addTorrentWithOptionsButton.Image = global::TransmissionRemoteDotnet.Properties.Resources.net_add;
            this.addTorrentWithOptionsButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addTorrentWithOptionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTorrentWithOptionsButton.Name = "addTorrentWithOptionsButton";
            this.addTorrentWithOptionsButton.Size = new System.Drawing.Size(36, 36);
            this.addTorrentWithOptionsButton.Text = "toolStripButton3";
            this.addTorrentWithOptionsButton.Click += new System.EventHandler(this.addTorrentWithOptionsButton_Click);
            // 
            // FeedImageList
            // 
            this.FeedImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.FeedImageList.ImageSize = new System.Drawing.Size(24, 24);
            this.FeedImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rssFeedsListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rssItemsListView);
            this.splitContainer1.Size = new System.Drawing.Size(944, 349);
            this.splitContainer1.SplitterDistance = 175;
            this.splitContainer1.TabIndex = 1;
            // 
            // rssFeedsListView
            // 
            this.rssFeedsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rssFeedsListView.FullRowSelect = true;
            this.rssFeedsListView.HideSelection = false;
            this.rssFeedsListView.LargeImageList = this.FeedImageList;
            this.rssFeedsListView.Location = new System.Drawing.Point(0, 0);
            this.rssFeedsListView.MultiSelect = false;
            this.rssFeedsListView.Name = "rssFeedsListView";
            this.rssFeedsListView.Scrollable = false;
            this.rssFeedsListView.ShowGroups = false;
            this.rssFeedsListView.Size = new System.Drawing.Size(175, 349);
            this.rssFeedsListView.SmallImageList = this.FeedImageList;
            this.rssFeedsListView.TabIndex = 0;
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
            this.rssItemsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rssItemsListView.FullRowSelect = true;
            this.rssItemsListView.GridLines = true;
            this.rssItemsListView.HideSelection = false;
            this.rssItemsListView.Location = new System.Drawing.Point(0, 0);
            this.rssItemsListView.MultiSelect = false;
            this.rssItemsListView.Name = "rssItemsListView";
            this.rssItemsListView.Size = new System.Drawing.Size(765, 349);
            this.rssItemsListView.TabIndex = 0;
            this.rssItemsListView.UseCompatibleStateImageBehavior = false;
            this.rssItemsListView.View = System.Windows.Forms.View.Details;
            this.rssItemsListView.SelectedIndexChanged += new System.EventHandler(this.rssItemsListView_SelectedIndexChanged);
            this.rssItemsListView.DoubleClick += new System.EventHandler(this.rssItemsListView_DoubleClick);
            this.rssItemsListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.rssItemsListView_ColumnClick);
            // 
            // titleColumnHeader
            // 
            this.titleColumnHeader.Text = "Title";
            this.titleColumnHeader.Width = 350;
            // 
            // categoryColumnHeader
            // 
            this.categoryColumnHeader.Text = "Category";
            this.categoryColumnHeader.Width = 150;
            // 
            // descriptionColumnHeader
            // 
            this.descriptionColumnHeader.Text = "Description";
            this.descriptionColumnHeader.Width = 250;
            // 
            // pubdateColumnHeader
            // 
            this.pubdateColumnHeader.Text = "Pub Date";
            this.pubdateColumnHeader.Width = 150;
            // 
            // RssForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 410);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RssForm";
            this.Text = "Rss Downloader";
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