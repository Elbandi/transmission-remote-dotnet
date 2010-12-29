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

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Etier.IconHelper;
using Jayrock.Json;
using Jayrock.Json.Conversion;
using MaxMind;
using TransmissionRemoteDotnet.Commmands;
using TransmissionRemoteDotnet.Comparers;
using TransmissionRemoteDotnet.Settings;

namespace TransmissionRemoteDotnet
{
    public partial class MainWindow : CultureForm
    {
        private const string
            DEFAULT_WINDOW_TITLE = "Transmission Remote",
            CONFKEY_MAINWINDOW_HEIGHT = "mainwindow-height",
            CONFKEY_MAINWINDOW_WIDTH = "mainwindow-width",
            CONFKEY_MAINWINDOW_LOCATION_X = "mainwindow-loc-x",
            CONFKEY_MAINWINDOW_LOCATION_Y = "mainwindow-loc-y",
            CONFKEY_SPLITTERDISTANCE = "mainwindow-splitterdistance",
            CONFKEY_MAINWINDOW_STATE = "mainwindow-state",
            CONFKEYPREFIX_LISTVIEW_WIDTHS = "listview-width-",
            CONFKEYPREFIX_LISTVIEW_INDEXES = "listview-indexes-",
            CONFKEYPREFIX_LISTVIEW_SORTINDEX = "listview-sortindex-",
            CONFKEY_FILTER_SPLITTERDISTANCE = "mainwindow-filter-splitterdistance",
            CONFKEY_MAINWINDOW_FILTERSPANEL_COLLAPSED = "mainwindow-filterspanel-collapsed",
            CONFKEY_MAINWINDOW_DETAILSPANEL_COLLAPSED = "mainwindow-detailspanel-collapsed",
            PROJECT_SITE = "http://code.google.com/p/transmission-remote-dotnet/",
            LATEST_VERSION = "http://transmission-remote-dotnet.googlecode.com/svn/wiki/latest_version.txt",
            LATEST_VERSION_BETA = "http://transmission-remote-dotnet.googlecode.com/svn/wiki/latest_version_beta.txt",
            DOWNLOADS_PAGE = "http://code.google.com/p/transmission-remote-dotnet/downloads/list";

        private Boolean minimise = false;
        private ListViewItemSorter lvwColumnSorter;
        private FilesListViewItemSorter filesLvwColumnSorter;
        private PeersListViewItemSorter peersLvwColumnSorter;
        private ContextMenu torrentSelectionMenu;
        private ContextMenu noTorrentSelectionMenu;
        private ContextMenu fileSelectionMenu;
        private ContextMenu noFileSelectionMenu;
        private MenuItem openNetworkShareMenuItemSep;
        private MenuItem openNetworkShareMenuItem;
        private MenuItem openNetworkShareDirMenuItem;
        private WebClient sessionWebClient;
        private WebClient refreshWebClient = new WebClient();
        private WebClient filesWebClient = new WebClient();
        private static FindDialog FindDialog;
        private List<Bitmap> defaulttoolbarimages, defaultstateimages, defaultinfopanelimages, defaulttrayimages;
        private TaskbarHelper taskbar;

        public MainWindow()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(Program.Settings.Locale, true);
            }
            catch { }
            Program.OnConnStatusChanged += new EventHandler(Program_connStatusChanged);
            Program.OnTorrentsUpdated += new EventHandler(Program_onTorrentsUpdated);
            InitializeComponent();
            CreateTrayContextMenu();
            defaultinfopanelimages = new List<Bitmap>();
            defaultinfopanelimages.Add(global::TransmissionRemoteDotnet.Properties.Resources.info16);
            generalTabPage.ImageIndex = 0;
            defaultinfopanelimages.Add(global::TransmissionRemoteDotnet.Properties.Resources.server16);
            trackersTabPage.ImageIndex = 1;
            defaultinfopanelimages.Add(global::TransmissionRemoteDotnet.Properties.Resources.peer16);
            peersTabPage.ImageIndex = 2;
            defaultinfopanelimages.Add(global::TransmissionRemoteDotnet.Properties.Resources.folder16);
            filesTabPage.ImageIndex = 3;
            defaultinfopanelimages.Add(global::TransmissionRemoteDotnet.Properties.Resources.pipe16);
            speedTabPage.ImageIndex = 4;
            tabControlImageList.Images.AddRange(defaultinfopanelimages.ToArray());
            defaultstateimages = new List<Bitmap>();
            defaultstateimages.Add(global::TransmissionRemoteDotnet.Properties.Resources.all16);
            defaultstateimages.Add(global::TransmissionRemoteDotnet.Properties.Resources.down16);
            defaultstateimages.Add(global::TransmissionRemoteDotnet.Properties.Resources.pause16);
            defaultstateimages.Add(global::TransmissionRemoteDotnet.Properties.Resources.apply16);
            defaultstateimages.Add(global::TransmissionRemoteDotnet.Properties.Resources.up16);
            defaultstateimages.Add(global::TransmissionRemoteDotnet.Properties.Resources.player_reload16);
            defaultstateimages.Add(global::TransmissionRemoteDotnet.Properties.Resources.warning16);
            defaultstateimages.Add(global::TransmissionRemoteDotnet.Properties.Resources.incomplete16);
            stateListBoxImageList.Images.AddRange(defaultstateimages.ToArray());
            stateListBoxImageList.Images.Add(tabControlImageList.Images[1]);
            List<ToolStripBitmap> initialtrayicons = new List<ToolStripBitmap>()
            {
                new ToolStripBitmap() { Name = "transmission", Image = global::TransmissionRemoteDotnet.Properties.Resources.icon_transmission.ToBitmap()},
                new ToolStripBitmap() { Name = "notransfer", Image = global::TransmissionRemoteDotnet.Properties.Resources.icon_blue.ToBitmap()},
                new ToolStripBitmap() { Name = "seed", Image = global::TransmissionRemoteDotnet.Properties.Resources.icon_red.ToBitmap()},
                new ToolStripBitmap() { Name = "downloadseed", Image = global::TransmissionRemoteDotnet.Properties.Resources.icon_yellow.ToBitmap()},
                new ToolStripBitmap() { Name = "download", Image = global::TransmissionRemoteDotnet.Properties.Resources.icon_green.ToBitmap()},
            };
            defaulttrayimages = new List<Bitmap>();
            foreach (ToolStripBitmap tsb in initialtrayicons)
            {
                defaulttrayimages.Add(tsb.Image);
                trayIconImageList.Images.Add(tsb.Image);
                int idx = defaulttrayimages.IndexOf(tsb.Image);
                trayIconImageList.Images.SetKeyName(idx, tsb.Name);
            }
            LocalSettings settings = Program.Settings;
            /* 
             * ToolStrips havent got ImageList field in design time.
             * We set the Image field, we can see the toolstripbuttons.
             * The ImageList is set in designtime, so ToolStrips will use ImageIndex.
             */
            List<ToolStripBitmap> initialimages = new List<ToolStripBitmap>()
            {
                new ToolStripBitmap() { Name = "connect", Image = global::TransmissionRemoteDotnet.Properties.Resources.connect, Controls = new ToolStripItem[]{connectButton, connectToolStripMenuItem} },
                new ToolStripBitmap() { Name = "disconnect", Image = global::TransmissionRemoteDotnet.Properties.Resources.disconnect, Controls = new ToolStripItem[]{disconnectButton, disconnectToolStripMenuItem} },
                new ToolStripBitmap() { Name = "addtorrent", Image = global::TransmissionRemoteDotnet.Properties.Resources.edit_add, Controls = new ToolStripItem[]{addTorrentButton, addTorrentToolStripMenuItem} },
                new ToolStripBitmap() { Name = "addtorrentoptions", Image = global::TransmissionRemoteDotnet.Properties.Resources.edit_add, Controls = new ToolStripItem[]{addTorrentWithOptionsToolStripMenuItem} },
                new ToolStripBitmap() { Name = "addurl", Image = global::TransmissionRemoteDotnet.Properties.Resources.net_add, Controls = new ToolStripItem[]{addWebTorrentButton, addTorrentFromUrlToolStripMenuItem} },
                new ToolStripBitmap() { Name = "player_play_all", Image = global::TransmissionRemoteDotnet.Properties.Resources.player_play_all, Controls = new ToolStripItem[]{startTorrentButton, startAllToolStripMenuItem} },
                new ToolStripBitmap() { Name = "player_pause_all", Image = global::TransmissionRemoteDotnet.Properties.Resources.player_pause_all, Controls = new ToolStripItem[]{pauseTorrentButton, stopAllToolStripMenuItem} },
                new ToolStripBitmap() { Name = "player_play", Image = global::TransmissionRemoteDotnet.Properties.Resources.player_play, Controls = new ToolStripItem[]{startToolStripMenuItem} },
                new ToolStripBitmap() { Name = "player_pause", Image = global::TransmissionRemoteDotnet.Properties.Resources.player_pause, Controls = new ToolStripItem[]{pauseToolStripMenuItem} },
                new ToolStripBitmap() { Name = "player_reload", Image = global::TransmissionRemoteDotnet.Properties.Resources.player_reload, Controls = new ToolStripItem[]{recheckTorrentButton, recheckToolStripMenuItem} },
                new ToolStripBitmap() { Name = "properties", Image = global::TransmissionRemoteDotnet.Properties.Resources.properties, Controls = new ToolStripItem[]{configureTorrentButton, propertiesToolStripMenuItem} },
                new ToolStripBitmap() { Name = "remove", Image = global::TransmissionRemoteDotnet.Properties.Resources.remove, Controls = new ToolStripItem[]{removeTorrentButton, removeToolStripMenuItem} },
                new ToolStripBitmap() { Name = "remove_and_delete", Image = global::TransmissionRemoteDotnet.Properties.Resources.remove_and_delete, Controls = new ToolStripItem[]{removeAndDeleteButton, removeDeleteToolStripMenuItem} },
                new ToolStripBitmap() { Name = "reannounce", Image = global::TransmissionRemoteDotnet.Properties.Resources.reannounce, Controls = new ToolStripItem[]{reannounceButton, reannounceToolStripMenuItem} },
                new ToolStripBitmap() { Name = "samba", Image = global::TransmissionRemoteDotnet.Properties.Resources.samba, Controls = new ToolStripItem[]{openNetworkShareButton, openNetworkShareDirToolStripMenuItem} },
                new ToolStripBitmap() { Name = "openterm", Image = global::TransmissionRemoteDotnet.Properties.Resources.openterm, Controls = new ToolStripItem[]{remoteCmdButton} },
                new ToolStripBitmap() { Name = "altspeed_on", Image = global::TransmissionRemoteDotnet.Properties.Resources.altspeed_on, Controls = new ToolStripItem[]{} },
                new ToolStripBitmap() { Name = "altspeed_off", Image = global::TransmissionRemoteDotnet.Properties.Resources.altspeed_off, Controls = new ToolStripItem[]{AltSpeedButton} },
                new ToolStripBitmap() { Name = "configure", Image = global::TransmissionRemoteDotnet.Properties.Resources.configure, Controls = new ToolStripItem[]{localConfigureButton, localSettingsToolStripMenuItem} },
                new ToolStripBitmap() { Name = "netconfigure", Image = global::TransmissionRemoteDotnet.Properties.Resources.netconfigure, Controls = new ToolStripItem[]{remoteConfigureButton, remoteSettingsToolStripMenuItem} },
                new ToolStripBitmap() { Name = "hwinfo", Image = global::TransmissionRemoteDotnet.Properties.Resources.hwinfo, Controls = new ToolStripItem[]{sessionStatsButton, statsToolStripMenuItem} },
                new ToolStripBitmap() { Name = "find", Image = global::TransmissionRemoteDotnet.Properties.Resources.find, Controls = new ToolStripItem[]{findToolStripMenuItem} },
                new ToolStripBitmap() { Name = "rss", Image = global::TransmissionRemoteDotnet.Properties.Resources.feed_icon, Controls = new ToolStripItem[]{RssButton} },
            };
            defaulttoolbarimages = new List<Bitmap>();
            foreach (ToolStripBitmap tsb in initialimages)
            {
                defaulttoolbarimages.Add(tsb.Image);
                toolStripImageList.Images.Add(tsb.Image);
                int idx = defaulttoolbarimages.IndexOf(tsb.Image);
                toolStripImageList.Images.SetKeyName(idx, tsb.Name);
                foreach (ToolStripItem i in tsb.Controls)
                {
                    i.ImageIndex = idx;
                }
            }

            toolStrip.ImageList = menuStrip.ImageList =
                fileToolStripMenuItem.DropDown.ImageList = optionsToolStripMenuItem.DropDown.ImageList =
                torrentToolStripMenuItem.DropDown.ImageList = viewToolStripMenuItem.DropDown.ImageList =
                helpToolStripMenuItem.DropDown.ImageList = toolStripImageList;

            mainVerticalSplitContainer.Panel1Collapsed = true;
            refreshTimer.Interval = Program.Settings.Current.RefreshRate * 1000;
            filesTimer.Interval = Program.Settings.Current.RefreshRate * 1000 * LocalSettingsSingleton.FILES_REFRESH_MULTIPLICANT;
            torrentListView.ListViewItemSorter = lvwColumnSorter = new ListViewItemSorter();
            filesListView.ListViewItemSorter = filesLvwColumnSorter = new FilesListViewItemSorter();
            peersListView.ListViewItemSorter = peersLvwColumnSorter = new PeersListViewItemSorter();
            InitStaticContextMenus();
            InitStateListBox();
            speedResComboBox.Items.AddRange(OtherStrings.SpeedResolutions.Split('|'));
            speedResComboBox.SelectedIndex = Math.Min(2, speedResComboBox.Items.Count - 1);
            RestoreFormProperties();
            CreateProfileMenu();
            //OpenGeoipDatabase();
            LoadSkins();
            peersListView.SmallImageList = GeoIPCountry.FlagImageList;
            PopulateLanguagesMenu();
            OneTorrentsSelected(false, null);
        }

        public void LoadSkins()
        {
            toolStrip.ImageList = menuStrip.ImageList =
                fileToolStripMenuItem.DropDown.ImageList = optionsToolStripMenuItem.DropDown.ImageList =
                torrentToolStripMenuItem.DropDown.ImageList = viewToolStripMenuItem.DropDown.ImageList =
                helpToolStripMenuItem.DropDown.ImageList = null;
            torrentListView.SmallImageList = stateListBox.ImageList = null;
            Toolbox.LoadSkinToImagelist(Program.Settings.ToolbarImagePath, 16, 32, toolStripImageList, defaulttoolbarimages);
            Toolbox.LoadSkinToImagelist(Program.Settings.StateImagePath, 16, 16, stateListBoxImageList, defaultstateimages);
            Toolbox.LoadSkinToImagelist(Program.Settings.InfopanelImagePath, 16, 16, tabControlImageList, defaultinfopanelimages);
            Toolbox.LoadSkinToImagelist(Program.Settings.TrayImagePath, 48, 48, trayIconImageList, defaulttrayimages);
            stateListBoxImageList.Images.Add(tabControlImageList.Images[1]);
            toolStrip.ImageList = menuStrip.ImageList =
                fileToolStripMenuItem.DropDown.ImageList = optionsToolStripMenuItem.DropDown.ImageList =
                torrentToolStripMenuItem.DropDown.ImageList = viewToolStripMenuItem.DropDown.ImageList =
                helpToolStripMenuItem.DropDown.ImageList = toolStripImageList;
            torrentListView.SmallImageList = stateListBox.ImageList = stateListBoxImageList;
        }

        public ToolStripMenuItem CreateProfileMenuItem(string name)
        {
            return new ToolStripMenuItem(name, null, new EventHandler(this.connectButtonprofile_SelectedIndexChanged));
        }

        public void CreateProfileMenu()
        {
            foreach (KeyValuePair<string, TransmissionServer> s in Program.Settings.Servers)
            {
                connectButton.DropDownItems.Add(CreateProfileMenuItem(s.Key));
                ToolStripMenuItem profile = CreateProfileMenuItem(s.Key);
                profile.Click += delegate(object sender, EventArgs e) { connectToolStripMenuItem.PerformClick(); };
                connectToolStripMenuItem.DropDownItems.Add(profile);
            }
        }

        private void InitStateListBox()
        {
            stateListBox.BeginUpdate();
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.All, 0));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Downloading, 1));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Paused, 2));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Checking, 5));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Complete, 3));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Incomplete, 7));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Seeding, 4));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Broken, 6));
            stateListBox.Items.Add(new GListBoxItem(""));
            stateListBox.EndUpdate();
        }

        private void InitStaticContextMenus()
        {
            this.peersListView.ContextMenu = new ContextMenu(new MenuItem[]{
                new MenuItem(OtherStrings.SelectAll, new EventHandler(this.SelectAllPeersHandler)),
                new MenuItem(OtherStrings.CopyAsCSV, new EventHandler(this.PeersToClipboardHandler))
            });
            this.noTorrentSelectionMenu = torrentListView.ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem(OtherStrings.SelectAll, new EventHandler(this.SelectAllTorrentsHandler))
            });
            this.fileSelectionMenu = new ContextMenu(new MenuItem[] {
                new MenuItem(OtherStrings.HighPriority, new EventHandler(this.SetHighPriorityHandler)),
                new MenuItem(OtherStrings.NormalPriority, new EventHandler(this.SetNormalPriorityHandler)),
                new MenuItem(OtherStrings.LowPriority, new EventHandler(this.SetLowPriorityHandler)),
                new MenuItem("-"),
                new MenuItem(OtherStrings.Download, new EventHandler(this.SetWantedHandler)),
                new MenuItem(OtherStrings.Skip, new EventHandler(this.SetUnwantedHandler)),
                new MenuItem("-"),
                new MenuItem(OtherStrings.SelectAll, new EventHandler(this.SelectAllFilesHandler)),
                new MenuItem(OtherStrings.CopyAsCSV, new EventHandler(this.FilesToClipboardHandler))
            });
            this.noFileSelectionMenu = this.filesListView.ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem(OtherStrings.SelectAll, new EventHandler(this.SelectAllFilesHandler))
            });
        }

        private void CreateTorrentSelectionContextMenu()
        {
            this.torrentSelectionMenu = new ContextMenu();
            this.torrentSelectionMenu.MenuItems.Add(OtherStrings.Start, this.startTorrentButton_Click);
            this.torrentSelectionMenu.MenuItems.Add(OtherStrings.Pause, this.pauseTorrentButton_Click);
            this.torrentSelectionMenu.MenuItems.Add(OtherStrings.Remove, this.removeTorrentButton_Click);
            if (Program.DaemonDescriptor.Version >= 1.5)
            {
                this.torrentSelectionMenu.MenuItems.Add(OtherStrings.RemoveAndDelete, this.removeAndDeleteButton_Click);
            }
            this.torrentSelectionMenu.MenuItems.Add(OtherStrings.Recheck, this.recheckTorrentButton_Click);
            if (Program.DaemonDescriptor.RpcVersion >= 5)
            {
                this.torrentSelectionMenu.MenuItems.Add(OtherStrings.Reannounce, this.reannounceButton_ButtonClick);
            }
            if (Program.DaemonDescriptor.RpcVersion >= 6)
            {
                this.torrentSelectionMenu.MenuItems.Add(OtherStrings.MoveTorrentData, this.moveTorrentDataToolStripMenuItem_Click);
            }
            this.torrentSelectionMenu.MenuItems.Add(openNetworkShareMenuItemSep = new MenuItem("-"));
            this.torrentSelectionMenu.MenuItems.Add(openNetworkShareMenuItem = new MenuItem(OtherStrings.OpenNetworkShare, this.openNetworkShare_Click));
            this.torrentSelectionMenu.MenuItems.Add(openNetworkShareDirMenuItem = new MenuItem(OtherStrings.OpenNetworkShareDir, this.openNetworkShareDir_Click));
            this.torrentSelectionMenu.MenuItems.Add("-");
            MenuItem bandwidthAllocationMenu = new MenuItem(OtherStrings.BandwidthAllocation);
            bandwidthAllocationMenu.MenuItems.Add(OtherStrings.High, this.bandwidthPriorityButton_Click).Tag = ProtocolConstants.BANDWIDTH_HIGH;
            bandwidthAllocationMenu.MenuItems.Add(OtherStrings.Normal, this.bandwidthPriorityButton_Click).Tag = ProtocolConstants.BANDWIDTH_NORMAL;
            bandwidthAllocationMenu.MenuItems.Add(OtherStrings.Low, this.bandwidthPriorityButton_Click).Tag = ProtocolConstants.BANDWIDTH_LOW;
            bandwidthAllocationMenu.MenuItems.Add("-");
            bandwidthAllocationMenu.Popup += new EventHandler(this.bandwidth_Opening);
            MenuItem downLimitMenuItem = new MenuItem(OtherStrings.DownloadLimit);
            downLimitMenuItem.MenuItems.Add(OtherStrings.Unlimited, ChangeDownLimit).Tag = -1;
            downLimitMenuItem.MenuItems.Add("-");
            Settings.TransmissionServer settings = Program.Settings.Current;
            foreach (string limit in settings.DownLimit.Split(','))
            {
                try
                {
                    int l = int.Parse(limit);
                    downLimitMenuItem.MenuItems.Add(Toolbox.KbpsString(l), ChangeDownLimit).Tag = l;
                }
                catch { }
            }
            downLimitMenuItem.Popup += new EventHandler(this.downlimit_Opening);
            bandwidthAllocationMenu.MenuItems.Add(downLimitMenuItem);
            MenuItem upLimitMenuItem = new MenuItem(OtherStrings.UploadLimit);
            upLimitMenuItem.MenuItems.Add(OtherStrings.Unlimited, ChangeUpLimit).Tag = -1;
            upLimitMenuItem.MenuItems.Add("-");
            foreach (string limit in settings.UpLimit.Split(','))
            {
                try
                {
                    int l = int.Parse(limit);
                    upLimitMenuItem.MenuItems.Add(Toolbox.KbpsString(l), ChangeUpLimit).Tag = l;
                }
                catch { }
            }
            upLimitMenuItem.Popup += new EventHandler(this.uplimit_Opening);
            bandwidthAllocationMenu.MenuItems.Add(upLimitMenuItem);
            this.torrentSelectionMenu.MenuItems.Add(bandwidthAllocationMenu);
            this.torrentSelectionMenu.MenuItems.Add("-");
            this.torrentSelectionMenu.MenuItems.Add(OtherStrings.Properties, this.ShowTorrentPropsHandler);

            this.torrentSelectionMenu.MenuItems.Add(OtherStrings.CopyAsCSV, this.TorrentsToClipboardHandler);
            //this.torrentSelectionMenu.MenuItems.Add(OtherStrings.InfoObjectToClipboard, this.copyInfoObjectToClipboardToolStripMenuItem_Click);
        }

        private void bandwidth_Opening(object sender, EventArgs e)
        {
            Torrent firstTorrent = (Torrent)torrentListView.SelectedItems[0];
            if (firstTorrent == null)
                return;
            int priority = firstTorrent.BandwidthPriority;
            for (int i = 0; i < ((MenuItem)sender).MenuItems.Count; i++)
            {
                MenuItem m = ((MenuItem)sender).MenuItems[i];
                if (m.Tag != null)
                    m.Checked = ((int)m.Tag == priority);
            }
        }

        private void downlimit_Opening(object sender, EventArgs e)
        {
            Torrent firstTorrent = (Torrent)torrentListView.SelectedItems[0];
            if (firstTorrent == null)
                return;
            int limit = firstTorrent.SpeedLimitDownEnabled ? firstTorrent.SpeedLimitDown : -1;
            foreach (MenuItem menuItem in ((MenuItem)sender).MenuItems)
            {
                if (menuItem.Tag != null)
                    menuItem.Checked = (int)menuItem.Tag == limit;
            }
        }

        private void uplimit_Opening(object sender, EventArgs e)
        {
            Torrent firstTorrent = (Torrent)torrentListView.SelectedItems[0];
            if (firstTorrent == null)
                return;
            int limit = firstTorrent.SpeedLimitUpEnabled ? firstTorrent.SpeedLimitUp : -1;
            foreach (MenuItem menuItem in ((MenuItem)sender).MenuItems)
            {
                if (menuItem.Tag != null)
                    menuItem.Checked = (int)menuItem.Tag == limit;
            }
        }

        private void ChangeDownLimit(object sender, EventArgs e)
        {
            JsonObject request = CreateLimitChangeRequest();
            JsonObject arguments = Requests.GetArgObject(request);
            int limit = (int)((MenuItem)sender).Tag;
            foreach (string key in new string[] { ProtocolConstants.FIELD_SPEEDLIMITDOWNENABLED, ProtocolConstants.FIELD_DOWNLOADLIMITED, ProtocolConstants.FIELD_DOWNLOADLIMITMODE })
            {
                arguments.Put(key, limit != -1 ? 1 : 0);
            }
            foreach (string key in new string[] { ProtocolConstants.FIELD_DOWNLOADLIMIT, ProtocolConstants.FIELD_SPEEDLIMITDOWN })
            {
                arguments.Put(key, limit == -1 ? 0 : limit);
            }
            Program.Form.SetupAction(CommandFactory.RequestAsync(request));
        }

        private JsonObject CreateLimitChangeRequest()
        {
            JsonObject request = Requests.CreateBasicObject(ProtocolConstants.METHOD_TORRENTSET);
            Requests.GetArgObject(request).Put(ProtocolConstants.KEY_IDS, BuildIdArray());
            return request;
        }

        private void ChangeUpLimit(object sender, EventArgs e)
        {
            JsonObject request = CreateLimitChangeRequest();
            JsonObject arguments = Requests.GetArgObject(request);
            int limit = (int)((MenuItem)sender).Tag;
            foreach (string key in new string[] { ProtocolConstants.FIELD_SPEEDLIMITUPENABLED, ProtocolConstants.FIELD_UPLOADLIMITED, ProtocolConstants.FIELD_UPLOADLIMITMODE })
            {
                arguments.Put(key, limit != -1 ? 1 : 0);
            }
            foreach (string key in new string[] { ProtocolConstants.FIELD_UPLOADLIMIT, ProtocolConstants.FIELD_SPEEDLIMITUP })
            {
                arguments.Put(key, limit == -1 ? 0 : limit);
            }
            Program.Form.SetupAction(CommandFactory.RequestAsync(request));
        }

        private void Program_onTorrentCompleted(Torrent t)
        {
            notifyIcon.ShowBalloonTip(LocalSettingsSingleton.BALLOON_TIMEOUT, t.TorrentName, OtherStrings.TheTorrentHasFinishedDownloading, ToolTipIcon.Info);
        }

        private void Program_onTorrentsUpdated(object sender, EventArgs e)
        {
            if (Program.Connected)
            {
                Torrent t = null;
                lock (torrentListView)
                {
                    if (torrentListView.SelectedItems.Count == 1)
                        t = (Torrent)torrentListView.SelectedItems[0];
                }
                if (t != null)
                    UpdateInfoPanel(false, t);
                refreshTimer.Enabled = torrentListView.Enabled = true;
                if (showCategoriesPanelToolStripMenuItem.Checked)
                    mainVerticalSplitContainer.Panel1Collapsed = false;
                FilterByStateOrTracker();
                UpdateTrayIcon();
            }
        }

        private void ChangeSessionDownLimit(object sender, EventArgs e)
        {
            JsonObject request = Requests.CreateBasicObject(ProtocolConstants.METHOD_SESSIONSET);
            JsonObject arguments = Requests.GetArgObject(request);
            int limit = (int)((MenuItem)sender).Tag;
            arguments.Put(ProtocolConstants.FIELD_SPEEDLIMITDOWNENABLED, limit != -1);
            arguments.Put(ProtocolConstants.FIELD_SPEEDLIMITDOWN, limit);
            Program.Form.SetupAction(CommandFactory.RequestAsync(request));
        }

        private void ChangeSessionUpLimit(object sender, EventArgs e)
        {
            JsonObject request = Requests.CreateBasicObject(ProtocolConstants.METHOD_SESSIONSET);
            JsonObject arguments = Requests.GetArgObject(request);
            int limit = (int)((MenuItem)sender).Tag;
            arguments.Put(ProtocolConstants.FIELD_SPEEDLIMITUPENABLED, limit != -1);
            arguments.Put(ProtocolConstants.FIELD_SPEEDLIMITUP, limit);
            Program.Form.SetupAction(CommandFactory.RequestAsync(request));
        }

        private void traydownlimit_Opening(object sender, EventArgs e)
        {
            JsonObject session = (JsonObject)Program.DaemonDescriptor.SessionData;
            int limit = Toolbox.ToBool(session[ProtocolConstants.FIELD_SPEEDLIMITDOWNENABLED]) ? Toolbox.ToInt(session[ProtocolConstants.FIELD_SPEEDLIMITDOWN]) : -1;
            foreach (MenuItem menuItem in ((MenuItem)sender).MenuItems)
            {
                if (menuItem.Tag != null)
                    menuItem.Checked = (int)menuItem.Tag == limit;
            }
        }

        private void trayuplimit_Opening(object sender, EventArgs e)
        {
            JsonObject session = (JsonObject)Program.DaemonDescriptor.SessionData;
            int limit = Toolbox.ToBool(session[ProtocolConstants.FIELD_SPEEDLIMITUPENABLED]) ? Toolbox.ToInt(session[ProtocolConstants.FIELD_SPEEDLIMITUP]) : -1;
            foreach (MenuItem menuItem in ((MenuItem)sender).MenuItems)
            {
                if (menuItem.Tag != null)
                    menuItem.Checked = (int)menuItem.Tag == limit;
            }
        }

        private void CreateTrayContextMenu()
        {
            ContextMenu trayMenu = new ContextMenu();
            if (Program.Connected)
            {
                trayMenu.MenuItems.Add(startAllToolStripMenuItem.Text, new EventHandler(this.startAllMenuItem_Click));
                trayMenu.MenuItems.Add(stopAllToolStripMenuItem.Text, new EventHandler(this.stopAllMenuItem_Click));
                trayMenu.MenuItems.Add("-");

                MenuItem downLimitMenuItem = new MenuItem(OtherStrings.DownloadLimit);
                downLimitMenuItem.MenuItems.Add(OtherStrings.Unlimited, ChangeSessionDownLimit).Tag = -1;
                downLimitMenuItem.MenuItems.Add("-");
                TransmissionServer server = Program.Settings.Current;
                foreach (string limit in server.DownLimit.Split(','))
                {
                    try
                    {
                        int l = int.Parse(limit);
                        downLimitMenuItem.MenuItems.Add(Toolbox.KbpsString(l), ChangeSessionDownLimit).Tag = l;
                    }
                    catch { }
                }
                downLimitMenuItem.Popup += new EventHandler(this.traydownlimit_Opening);
                trayMenu.MenuItems.Add(downLimitMenuItem);

                MenuItem upLimitMenuItem = new MenuItem(OtherStrings.UploadLimit);
                upLimitMenuItem.MenuItems.Add(OtherStrings.Unlimited, ChangeSessionUpLimit).Tag = -1;
                upLimitMenuItem.MenuItems.Add("-");
                foreach (string limit in server.UpLimit.Split(','))
                {
                    try
                    {
                        int l = int.Parse(limit);
                        upLimitMenuItem.MenuItems.Add(Toolbox.KbpsString(l), ChangeSessionUpLimit).Tag = l;
                    }
                    catch { }
                }
                upLimitMenuItem.Popup += new EventHandler(this.trayuplimit_Opening);
                trayMenu.MenuItems.Add(upLimitMenuItem);

                trayMenu.MenuItems.Add("-");
                if (Program.DaemonDescriptor.RpcVersion >= 4)
                {
                    trayMenu.MenuItems.Add(sessionStatsButton.Text, new EventHandler(this.sessionStatsButton_Click));
                }
                trayMenu.MenuItems.Add(OtherStrings.Disconnect, new EventHandler(this.disconnectButton_Click));
            }
            else
            {
                trayMenu.MenuItems.Add(OtherStrings.Connect, new EventHandler(this.connectButton_Click));
            }
            this.notifyIcon.Text = MainWindow.DEFAULT_WINDOW_TITLE;
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add(exitToolStripMenuItem.Text, new EventHandler(this.exitToolStripMenuItem_Click));
            this.notifyIcon.ContextMenu = trayMenu;
        }

        private void Program_connStatusChanged(object sender, EventArgs e)
        {
            bool connected = Program.Connected;
            CreateTrayContextMenu();
            if (connected)
            {
                JsonObject session = (JsonObject)Program.DaemonDescriptor.SessionData;
                CreateTorrentSelectionContextMenu();
                this.toolStripStatusLabel.Text = OtherStrings.ConnectedGettingInfo;
                if (session.Contains("version"))
                {
                    this.toolStripVersionLabel.Visible = true;
                    this.toolStripVersionLabel.Text = (string)session["version"];
                    this.toolStripStatusLabel.Width = 0;
                }
                lvwColumnSorter.SetupColumn(Program.DaemonDescriptor.RpcVersion);
                this.Text = MainWindow.DEFAULT_WINDOW_TITLE + " - " + Program.Settings.Current.Host;
                speedGraph.MaxPeekMagnitude = 100;
                speedGraph.AddLine("Download", Color.Green);
                speedGraph.AddLine("Upload", Color.Yellow);
                speedGraph.Push(0, "Download");
                speedGraph.Push(0, "Upload");
            }
            else
            {
                StatsDialog.CloseIfOpen();
                RemoteSettingsDialog.CloseIfOpen();
                torrentListView.Enabled = false;
                torrentListView.ContextMenu = this.torrentSelectionMenu = null;
                lock (this.torrentListView)
                {
                    this.torrentListView.Items.Clear();
                }
                OneOrMoreTorrentsSelected(false);
                OneTorrentsSelected(false, null);
                this.toolStripStatusLabel.Text = OtherStrings.Disconnected;
                this.toolStripVersionLabel.Visible = false;
                this.mainVerticalSplitContainer.Panel1Collapsed = true;
                this.Text = MainWindow.DEFAULT_WINDOW_TITLE;
                speedGraph.RemoveLine("Download");
                speedGraph.RemoveLine("Upload");
                lock (this.stateListBox)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        ((GListBoxItem)stateListBox.Items[i]).Counter = 0;
                    }
                    if (this.stateListBox.Items.Count > 8)
                    {
                        for (int i = this.stateListBox.Items.Count - 1; i > 8; i--)
                        {
                            stateListBox.Items.RemoveAt(i);
                        }
                    }
                }
                UpdateNotifyIcon("transmission");
            }
            connectButton.Visible = connectButton.Enabled = connectToolStripMenuItem.Enabled
                = !connected;
            disconnectButton.Visible = addTorrentToolStripMenuItem.Enabled
                = addTorrentButton.Visible = addWebTorrentButton.Visible
                = remoteConfigureButton.Visible = pauseTorrentButton.Visible
                = removeTorrentButton.Visible = toolbarToolStripSeparator1.Visible
                = toolbarToolStripSeparator2.Visible = disconnectToolStripMenuItem.Enabled
                = configureTorrentButton.Visible = torrentToolStripMenuItem.Enabled
                = remoteSettingsToolStripMenuItem.Enabled
                = addTorrentFromUrlToolStripMenuItem.Enabled = startTorrentButton.Visible
                = refreshTimer.Enabled = recheckTorrentButton.Visible
                = speedGraph.Enabled = toolbarToolStripSeparator3.Visible
                = FilterTorrentLabel.Visible = FilterTorrentTextBox.Visible
                = connected;
            SetRemoteCmdButtonVisible(connected);
            taskbar.SetConnected(connected);
            TransmissionDaemonDescriptor dd = Program.DaemonDescriptor;
            reannounceButton.Visible = connected && dd.RpcVersion >= 5;
            removeAndDeleteButton.Visible = connected && dd.Version >= 1.5;
            statsToolStripMenuItem.Enabled = sessionStatsButton.Visible = connected && dd.RpcVersion >= 4;
            AltSpeedButton.Visible = toolbarToolStripSeparator4.Visible = connected && dd.RpcVersion >= 5;
            addTorrentWithOptionsToolStripMenuItem.Enabled = (dd.Version < 1.60 || dd.Version >= 1.7) && connected;
        }

        public void SetAltSpeedButtonState(bool enabled)
        {
            AltSpeedButton.ImageIndex = enabled ? toolStripImageList.Images.IndexOfKey("altspeed_on") : toolStripImageList.Images.IndexOfKey("altspeed_off");
            AltSpeedButton.Tag = enabled;
        }

        public void SetRemoteCmdButtonVisible(bool connected)
        {
            LocalSettings settings = Program.Settings;
            remoteCmdButton.Visible = connected && settings.Current.PlinkEnable && settings.Current.PlinkCmd != null && settings.PlinkPath != null && File.Exists(settings.PlinkPath);
            openNetworkShareButton.Visible = connected && settings.Current.SambaShareMappings.Count > 0;
            if (openNetworkShareMenuItemSep != null)
                openNetworkShareMenuItemSep.Visible = openNetworkShareButton.Visible;
            if (openNetworkShareMenuItem != null)
                openNetworkShareMenuItem.Visible = openNetworkShareButton.Visible;
            if (openNetworkShareDirMenuItem != null)
                openNetworkShareDirMenuItem.Visible = openNetworkShareButton.Visible;
        }

        public void ShowTrayTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
        {
            this.notifyIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
        }

        private string LastTrayIcon = "";
        private void UpdateNotifyIcon(string name)
        {
            if (!name.Equals(LastTrayIcon))
            {
                IntPtr Hicon = (trayIconImageList.Images[name] as Bitmap).GetHicon();
                notifyIcon.Icon = (Icon)Icon.FromHandle(Hicon).Clone();
                User32.DestroyIcon(Hicon);
                LastTrayIcon = name;
            }
        }

        private void UpdateTrayIcon()
        {
            int seedcount = 0, downloadcount = 0;
            lock (Program.TorrentIndex)
            {
                foreach (KeyValuePair<string, Torrent> pair in Program.TorrentIndex)
                {
                    if (IfTorrentStatus(pair.Value, ProtocolConstants.STATUS_DOWNLOADING))
                        downloadcount++;
                    if (IfTorrentStatus(pair.Value, ProtocolConstants.STATUS_SEEDING))
                        seedcount++;
                }
            }

            if (Program.Settings.ColorTray)
            {
                if (seedcount == 0 && downloadcount == 0)
                    UpdateNotifyIcon("notransfer");
                else if (seedcount > 0 && downloadcount > 0)
                    UpdateNotifyIcon("downloadseed");
                else if (seedcount > 0)
                    UpdateNotifyIcon("seed");
                else if (downloadcount > 0)
                    UpdateNotifyIcon("download");
                else
                    UpdateNotifyIcon("transmission");
            }
            else
                UpdateNotifyIcon("transmission");
        }

        public void TorrentsToClipboardHandler(object sender, EventArgs e)
        {
            Toolbox.CopyListViewToClipboard(torrentListView);
        }

        public void FilesToClipboardHandler(object sender, EventArgs e)
        {
            Toolbox.CopyListViewToClipboard(filesListView);
        }

        public void PeersToClipboardHandler(object sender, EventArgs e)
        {
            Toolbox.CopyListViewToClipboard(peersListView);
        }

        public void Perform_startAllMenuItem_Click()
        {
            startAllToolStripMenuItem.PerformClick();
        }

        public void startAllMenuItem_Click(object sender, EventArgs e)
        {
            Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.Generic(ProtocolConstants.METHOD_TORRENTSTART, null)));
        }

        public void Perform_stopAllMenuItem_Click()
        {
            stopAllToolStripMenuItem.PerformClick();
        }

        public void stopAllMenuItem_Click(object sender, EventArgs e)
        {
            Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.Generic(ProtocolConstants.METHOD_TORRENTSTOP, null)));
        }

        public void RestoreFormProperties()
        {
            try
            {
                LocalSettings settings = Program.Settings;
                if (settings.Misc.ContainsKey(CONFKEY_MAINWINDOW_HEIGHT) && settings.Misc.ContainsKey(CONFKEY_MAINWINDOW_WIDTH))
                    this.Size = new Size(Toolbox.ToInt(settings.Misc[CONFKEY_MAINWINDOW_WIDTH]), Toolbox.ToInt(settings.Misc[CONFKEY_MAINWINDOW_HEIGHT]));
                if (settings.Misc.ContainsKey(CONFKEY_MAINWINDOW_LOCATION_X) && settings.Misc.ContainsKey(CONFKEY_MAINWINDOW_LOCATION_Y))
                {
                    Point p = new Point(Toolbox.ToInt(settings.GetObject(CONFKEY_MAINWINDOW_LOCATION_X)), Toolbox.ToInt(settings.GetObject(CONFKEY_MAINWINDOW_LOCATION_Y)));
                    if (Toolbox.ScreenExists(p))
                        this.Location = p;
                }
                if (settings.Misc.ContainsKey(CONFKEY_FILTER_SPLITTERDISTANCE))
                    this.mainVerticalSplitContainer.SplitterDistance = Toolbox.ToInt(settings.GetObject(CONFKEY_FILTER_SPLITTERDISTANCE));
                this.showCategoriesPanelToolStripMenuItem.Checked = settings.Misc.ContainsKey(CONFKEY_MAINWINDOW_FILTERSPANEL_COLLAPSED) && Toolbox.ToInt(settings.GetObject(CONFKEY_MAINWINDOW_FILTERSPANEL_COLLAPSED)) == 0;
                this.showDetailsPanelToolStripMenuItem.Checked = !(this.torrentAndTabsSplitContainer.Panel2Collapsed = !settings.Misc.ContainsKey(CONFKEY_MAINWINDOW_DETAILSPANEL_COLLAPSED) || Toolbox.ToInt(settings.GetObject(CONFKEY_MAINWINDOW_DETAILSPANEL_COLLAPSED)) == 1);
                if (settings.Misc.ContainsKey(CONFKEY_MAINWINDOW_STATE))
                {
                    FormWindowState _mainWindowState = (FormWindowState)(Toolbox.ToInt(settings.GetObject(CONFKEY_MAINWINDOW_STATE)));
                    if (_mainWindowState != FormWindowState.Minimized)
                    {
                        this.notifyIcon.Tag = this.WindowState = _mainWindowState;
                    }
                    else
                        this.notifyIcon.Tag = this.WindowState;
                }
                RestoreListViewProperties(torrentListView);
                RestoreListViewProperties(filesListView);
                RestoreListViewProperties(peersListView);
            }
            catch { }
        }

        public void SaveListViewProperties(ListView listView)
        {
            JsonArray widths = new JsonArray();
            JsonArray indexes = new JsonArray();
            foreach (ColumnHeader column in listView.Columns)
            {
                widths.Add(column.Width);
                indexes.Add(column.DisplayIndex);
            }
            LocalSettings settings = Program.Settings;
            settings.SetObject(CONFKEYPREFIX_LISTVIEW_WIDTHS + listView.Name, widths.ToString());
            settings.SetObject(CONFKEYPREFIX_LISTVIEW_INDEXES + listView.Name, indexes.ToString());
            lock (listView)
            {
                IListViewItemSorter listViewItemSorter = (IListViewItemSorter)listView.ListViewItemSorter;
                settings.SetObject(CONFKEYPREFIX_LISTVIEW_SORTINDEX + listView.Name, listViewItemSorter.Order == SortOrder.Descending ? -listViewItemSorter.SortColumn : listViewItemSorter.SortColumn);
            }
        }

        public void RestoreListViewProperties(ListView listView)
        {
            LocalSettings settings = Program.Settings;
            string widthsConfKey = CONFKEYPREFIX_LISTVIEW_WIDTHS + listView.Name,
              indexesConfKey = CONFKEYPREFIX_LISTVIEW_INDEXES + listView.Name,
              sortIndexConfKey = CONFKEYPREFIX_LISTVIEW_SORTINDEX + listView.Name;

            if (settings.ContainsKey(widthsConfKey))
            {
                JsonArray widths = GetListViewPropertyArray(widthsConfKey);
                for (int i = 0; i < widths.Count; i++)
                {
                    listView.Columns[i].Width = Toolbox.ToInt(widths[i]);
                }
            }
            if (settings.ContainsKey(indexesConfKey))
            {
                JsonArray indexes = GetListViewPropertyArray(indexesConfKey);
                for (int i = 0; i < indexes.Count; i++)
                {
                    listView.Columns[i].DisplayIndex = Toolbox.ToInt(indexes[i]);
                }
            }
            if (settings.ContainsKey(sortIndexConfKey))
            {
                IListViewItemSorter sorter = (IListViewItemSorter)listView.ListViewItemSorter;
                int sortIndex = Toolbox.ToInt(settings.GetObject(sortIndexConfKey));
                sorter.Order = sortIndex < 0 ? SortOrder.Descending : SortOrder.Ascending;
                sorter.SortColumn = sortIndex < 0 ? -sortIndex : sortIndex;
            }
        }

        private JsonArray GetListViewPropertyArray(string key)
        {
            return (JsonArray)JsonConvert.Import((string)Program.Settings.GetObject(key));
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            taskbar = new TaskbarHelper();
            LocalSettings settings = Program.Settings;
            if (notifyIcon.Visible = settings.MinToTray)
            {
                foreach (string arg in Environment.GetCommandLineArgs())
                {
                    if (arg.Equals("/m", StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.WindowState = FormWindowState.Minimized;
                        this.minimise = true;
                    }
                }
            }
            if (settings.AutoCheckupdate)
            {
                DoCheckVersion(false);
            }
            if (settings.AutoUpdateGeoip)
            {
                DoCheckGeoip(false);
            }
            if (!settings.AutoConnect.Equals(""))
            {
                Connect();
            }
            else
            {
                if (Program.UploadArgs != null && Program.UploadArgs.Length > 0)
                {
                    ShowMustBeConnectedDialog(Program.UploadArgs, Program.Settings.UploadPrompt);
                }
            }
        }

        private void PopulateLanguagesMenu()
        {
            ToolStripMenuItem englishItem = new ToolStripMenuItem("English");
            englishItem.Click += new EventHandler(this.ChangeUICulture);
            englishItem.Tag = new CultureInfo("en-US", true);
            englishItem.Checked = Program.Settings.Locale.Equals("en-US");
            languageToolStripMenuItem.DropDownItems.Add(englishItem);
            languageToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            DirectoryInfo di = new DirectoryInfo(Toolbox.GetExecuteDirectory());
            foreach (DirectoryInfo subDir in di.GetDirectories())
            {
                string dn = subDir.Name;
                if (dn.IndexOf('-') == 2 && dn.Length == 5)
                {
                    try
                    {
                        CultureInfo cInfo = new CultureInfo(dn.Substring(0, 2).ToLower() + "-" + dn.Substring(3, 2).ToUpper(), true);
                        ToolStripMenuItem item = new ToolStripMenuItem(cInfo.NativeName + " / " + cInfo.EnglishName);
                        item.Tag = cInfo;
                        item.Click += new EventHandler(this.ChangeUICulture);
                        item.Checked = Program.Settings.Locale.Equals(cInfo.Name);
                        languageToolStripMenuItem.DropDownItems.Add(item);
                    }
                    catch (Exception ex)
                    {
                        Program.Log("Unable to load localisation " + dn, ex.Message);
                    }
                }
            }
        }

        private void ChangeUICulture(object sender, EventArgs e)
        {
            try
            {
                LocalSettings settings = Program.Settings;
                ToolStripMenuItem senderMI = sender as ToolStripMenuItem;
                CultureInfo culture = (CultureInfo)senderMI.Tag;
                foreach (ToolStripItem mi in languageToolStripMenuItem.DropDownItems)
                    if (mi.GetType().Equals(typeof(ToolStripMenuItem)))
                        ((ToolStripMenuItem)mi).Checked = false;
                senderMI.Checked = true;
                settings.Locale = culture.Name;
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = culture;
                Program.CultureChanger.ApplyCulture(culture);
                InitStaticContextMenus();
                torrentListView_SelectedIndexChanged(null, null);
                string[] statestrings = new string[] { OtherStrings.All, OtherStrings.Downloading, OtherStrings.Paused, OtherStrings.Checking, OtherStrings.Complete, OtherStrings.Incomplete, OtherStrings.Seeding, OtherStrings.Broken };
                for (int i = 0; i < statestrings.Length; i++)
                {
                    (stateListBox.Items[i] as GListBoxItem).Text = statestrings[i];
                }
                CreateTrayContextMenu();
                foreach (FileListViewItem item in filesListView.Items)
                {
                    item.SubItems[5].Text = item.Wanted ? OtherStrings.No : OtherStrings.Yes;
                    item.SubItems[6].Text = Toolbox.FormatPriority(item.Priority);
                }
                foreach (Torrent item in torrentListView.Items)
                {
                    item.StatusCode = item.StatusCode; //StatusCode field set update the language
                }
                int oldindex = speedResComboBox.SelectedIndex;
                speedResComboBox.Items.Clear();
                speedResComboBox.Items.AddRange(OtherStrings.SpeedResolutions.Split('|'));
                speedResComboBox.SelectedIndex = Math.Min(oldindex, speedResComboBox.Items.Count - 1);
                taskbar.ChangeUICulture();
                filesListView_SelectedIndexChanged(null, null);
                Program_onTorrentsUpdated(null, null);
                this.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to load language pack", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void connectButtonprofile_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocalSettings settings = Program.Settings;
            ToolStripMenuItem profile = (sender as ToolStripMenuItem);
            foreach (ToolStripMenuItem item in connectButton.DropDownItems)
            {
                item.Checked = false;
            }
            profile.Checked = true;
            string selectedProfile = profile.ToString();
            if (!selectedProfile.Equals(settings.CurrentProfile))
            {
                settings.CurrentProfile = selectedProfile;
            }
        }

        private void SelectAllFilesHandler(object sender, EventArgs e)
        {
            Toolbox.SelectAll(filesListView);
        }

        private void SelectAllTorrentsHandler(object sender, EventArgs e)
        {
            Toolbox.SelectAll(torrentListView);
        }

        private void SelectAllPeersHandler(object sender, EventArgs e)
        {
            Toolbox.SelectAll(peersListView);
        }

        private void MainWindow_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void MainWindow_DragDrop(object sender, DragEventArgs e)
        {
            if (Program.Connected)
            {
                Upload((string[])e.Data.GetData(DataFormats.FileDrop), Program.Settings.UploadPrompt);
            }
            else
            {
                ShowMustBeConnectedDialog((string[])e.Data.GetData(DataFormats.FileDrop), Program.Settings.UploadPrompt);
            }
        }

        public void ShowMustBeConnectedDialog(string[] args, bool uploadPrompt)
        {
            if (MessageBox.Show(OtherStrings.MustBeConnected, OtherStrings.NotConnected, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Program.UploadArgs = args;
                Program.UploadPrompt = uploadPrompt;
                Connect();
            }
            else
            {
                Program.UploadArgs = null;
            }
        }

        public TransmissionWebClient SetupAction(TransmissionWebClient twc)
        {
            twc.Completed += new EventHandler<ResultEventArgs>(twc_Completed);
            return twc;
        }

        void twc_Completed(object sender, ResultEventArgs e)
        {
            if (e.Result.GetType() != typeof(ErrorCommand))
            {
                RefreshIfNotRefreshing();
            }
        }

        private JsonArray BuildIdArray()
        {
            JsonArray ids = new JsonArray();
            lock (torrentListView)
            {
                foreach (Torrent t in torrentListView.SelectedItems)
                {
                    ids.Put(t.Id);
                }
            }
            return ids;
        }

        private void RemoveTorrentsPrompt()
        {
            if (torrentListView.SelectedItems.Count == 1
                && MessageBox.Show(String.Format(OtherStrings.ConfirmSingleRemove, torrentListView.SelectedItems[0].Text), OtherStrings.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RemoveTorrents(false);
            }
            else if (torrentListView.SelectedItems.Count > 1
                && MessageBox.Show(String.Format(OtherStrings.ConfirmMultipleRemove, torrentListView.SelectedItems.Count), OtherStrings.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RemoveTorrents(false);
            }
        }

        private void RemoveAndDeleteTorrentsPrompt()
        {
            if (Program.DaemonDescriptor.Version >= 1.5)
            {
                if (torrentListView.SelectedItems.Count == 1
                    && MessageBox.Show(String.Format(OtherStrings.ConfirmSingleRemoveAndDelete, torrentListView.SelectedItems[0].Text, Environment.NewLine + Environment.NewLine), OtherStrings.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RemoveTorrents(true);
                }
                else if (torrentListView.SelectedItems.Count > 1
                    && MessageBox.Show(String.Format(OtherStrings.ConfirmMultipleRemoveAndDelete, torrentListView.SelectedItems.Count, Environment.NewLine + Environment.NewLine), OtherStrings.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RemoveTorrents(true);
                }
            }
        }

        private void RemoveTorrents(bool delete)
        {
            if (torrentListView.SelectedItems.Count > 0)
            {
                Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.RemoveTorrent(BuildIdArray(), delete)));
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.InvokeShow();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutDialog()).ShowDialog();
        }

        private delegate void InvokeShowDelegate();
        public void InvokeShow()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new InvokeShowDelegate(this.InvokeShow));
            }
            else
            {
                this.Show();
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = this.notifyIcon.Tag != null ? (FormWindowState)this.notifyIcon.Tag : FormWindowState.Normal;
                }
                this.Activate();
                this.BringToFront();
            }
        }

        private delegate void ConnectDelegate();
        public void Connect()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ConnectDelegate(this.Connect));
            }
            else
            {
                if (Program.Settings.Current.Host.Equals(""))
                {
                    MessageBox.Show(OtherStrings.NoHostnameSet, OtherStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!Uri.IsWellFormedUriString(Program.Settings.Current.RpcUrl, UriKind.Absolute))
                {
                    MessageBox.Show(OtherStrings.InvalidRPCLocation, OtherStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Program.Connected)
                    Program.Connected = false;
                connectButton.Enabled = connectToolStripMenuItem.Enabled = false;
                toolStripStatusLabel.Text = OtherStrings.Connecting + "...";
                sessionWebClient = CommandFactory.RequestAsync(Requests.SessionGet());
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            RefreshIfNotRefreshing();
        }

        public void RefreshIfNotRefreshing()
        {
            if (!sessionWebClient.IsBusy)
            {
                sessionWebClient = CommandFactory.RequestAsync(Requests.SessionGet());
            }
            if (!refreshWebClient.IsBusy)
            {
                refreshWebClient = CommandFactory.RequestAsync(Requests.TorrentGet());
            }
        }

        private void localConfigureButton_Click(object sender, EventArgs e)
        {
            LocalSettingsDialog ls = new LocalSettingsDialog();
            ls.SetImageNumbers(defaulttoolbarimages.Count, defaultstateimages.Count, defaultinfopanelimages.Count, defaulttrayimages.Count);
            if (ls.ShowDialog() == DialogResult.OK)
            {
                notifyIcon.Visible = Program.Settings.MinToTray;
                connectButton.DropDownItems.Clear();
                connectToolStripMenuItem.DropDownItems.Clear();
                CreateProfileMenu();
                SetRemoteCmdButtonVisible(Program.Connected);
                refreshTimer.Interval = Program.Settings.Current.RefreshRate * 1000;
                filesTimer.Interval = Program.Settings.Current.RefreshRate * 1000 * LocalSettingsSingleton.FILES_REFRESH_MULTIPLICANT;
                Program.UploadPrompt = Program.Settings.UploadPrompt;
                LoadSkins();
                UpdateTrayIcon();
            }
        }

        private void remoteConfigureButton_Click(object sender, EventArgs e)
        {
            if (Program.Connected)
                ClassSingleton<RemoteSettingsDialog>.Instance.ShowDialog();
        }

        private void OneOrMoreTorrentsSelected(bool oneOrMore)
        {
            removeTorrentButton.Enabled = recheckTorrentButton.Enabled
                = removeAndDeleteButton.Enabled = configureTorrentButton.Enabled
                = startToolStripMenuItem.Enabled = pauseToolStripMenuItem.Enabled
                = recheckToolStripMenuItem.Enabled = propertiesToolStripMenuItem.Enabled
                = removeDeleteToolStripMenuItem.Enabled = removeToolStripMenuItem.Enabled
                = reannounceButton.Enabled = reannounceToolStripMenuItem.Enabled
                = moveTorrentDataToolStripMenuItem.Enabled = cSVInfoToClipboardToolStripMenuItem.Enabled = oneOrMore;
            moveTorrentDataToolStripMenuItem.Enabled = oneOrMore && Program.DaemonDescriptor.Version >= 1.7;
            pauseTorrentButton.ImageIndex = oneOrMore && torrentListView.SelectedItems.Count != torrentListView.Items.Count ? toolStripImageList.Images.IndexOfKey("player_pause") : toolStripImageList.Images.IndexOfKey("player_pause_all");
            startTorrentButton.ImageIndex = oneOrMore && torrentListView.SelectedItems.Count != torrentListView.Items.Count ? toolStripImageList.Images.IndexOfKey("player_play") : toolStripImageList.Images.IndexOfKey("player_play_all");
        }

        public void FillfilesListView(Torrent t)
        {
            lock (filesListView)
            {
                filesListView.BeginUpdate();
                IComparer tmp = filesListView.ListViewItemSorter;
                filesListView.ListViewItemSorter = null;
                if (!filesListView.Enabled)
                {
                    filesListView.Enabled = true;
                    filesListView.Items.AddRange(t.Files.ToArray());
                }
                else
                    filesListView.Refresh();
                filesListView.ListViewItemSorter = tmp;
                Toolbox.StripeListView(filesListView);
                filesListView.EndUpdate();
            }
        }

        private void OneTorrentsSelected(bool one, Torrent t)
        {
            if (one)
            {
                UpdateInfoPanel(true, t);
                if (t.Files.Count == 0)
                    Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.FilesAndPriorities(t.Id)));
                else
                    FillfilesListView(t);
            }
            else
            {
                lock (filesListView)
                {
                    filesListView.Items.Clear();
                }
                lock (peersListView)
                {
                    peersListView.Items.Clear();
                }
                lock (trackersListView)
                {
                    trackersListView.Items.Clear();
                }
                generalTorrentInfo.timeElapsed = generalTorrentInfo.downloaded = generalTorrentInfo.downloadSpeed
                    = generalTorrentInfo.downloadLimit = generalTorrentInfo.status = generalTorrentInfo.comment
                    = generalTorrentInfo.remaining = generalTorrentInfo.uploaded = generalTorrentInfo.uploadSpeed
                    = generalTorrentInfo.uploadLimit = generalTorrentInfo.startedAt = generalTorrentInfo.seeders
                    = generalTorrentInfo.leechers = generalTorrentInfo.ratio = generalTorrentInfo.createdAt
                    = generalTorrentInfo.createdBy = generalTorrentInfo.error = percentageLabel.Text
                    = generalTorrentInfo.hash = generalTorrentInfo.piecesInfo = generalTorrentInfo.location
                    = generalTorrentInfo.torrentName = generalTorrentInfo.totalSize = "";
                trackersTorrentNameGroupBox.Text
                   = peersTorrentNameGroupBox.Text = filesTorrentNameGroupBox.Text
                   = "N/A";
                progressBar.Value = 0;
                piecesGraph.ClearBits();
                generalTorrentInfo.errorVisible
                    = filesListView.Enabled = peersListView.Enabled
                    = trackersListView.Enabled = false;
            }
            generalTorrentInfo.Enabled
                    = downloadProgressLabel.Enabled = refreshElapsedTimer.Enabled
                    = filesTimer.Enabled = downloadProgressLabel.Enabled
                    = remoteCmdButton.Enabled = one;
            openNetworkShareButton.Enabled = openNetworkShareDirToolStripMenuItem.Enabled = one && t.HaveTotal > 0 && t.SambaLocation != null;
            openNetworkShareToolStripMenuItem.Enabled = openNetworkShareButton.Enabled && t.Files.Count == 1 && t.Files[0].BytesCompleted == t.Files[0].FileSize;
            if (openNetworkShareMenuItem != null)
                openNetworkShareMenuItem.Enabled = openNetworkShareToolStripMenuItem.Enabled;
            if (openNetworkShareDirMenuItem != null)
                openNetworkShareDirMenuItem.Enabled = openNetworkShareDirToolStripMenuItem.Enabled;
        }

        private void torrentListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool one, oneOrMore;
            Torrent t = null;
            lock (torrentListView)
            {
                if (oneOrMore = torrentListView.SelectedItems.Count > 0)
                    t = (Torrent)torrentListView.SelectedItems[0];
                one = torrentListView.SelectedItems.Count == 1;
            }
            torrentListView.ContextMenu = oneOrMore ? this.torrentSelectionMenu : this.noTorrentSelectionMenu;
            OneOrMoreTorrentsSelected(oneOrMore);
            OneTorrentsSelected(one, t);
            UpdateStatus(GetSummaryStatus(), true);
        }

        private void torrentListView_DoubleClick(object sender, EventArgs e)
        {
            Torrent t = null;
            switch (Program.Settings.DefaultDoubleClickAction)
            {
                case 1:
                    if (openNetworkShareButton.Visible)
                        openNetworkShareButton.PerformClick();
                    else
                        ShowTorrentPropsHandler(sender, e);
                    break;
                case 2:
                    t = (Torrent)torrentListView.SelectedItems[0];
                    if (IfTorrentStatus(t, ProtocolConstants.STATUS_PAUSED))
                        startTorrentButton.PerformClick();
                    else
                        pauseTorrentButton.PerformClick();
                    break;
                case 0:
                default:
                    ShowTorrentPropsHandler(sender, e);
                    break;
            }
        }

        private void ShowTorrentPropsHandler(object sender, EventArgs e)
        {
            lock (torrentListView)
                if (torrentListView.SelectedItems.Count > 0)
                    (new TorrentPropertiesDialog(torrentListView.SelectedItems)).ShowDialog();
        }

        private void removeTorrentButton_Click(object sender, EventArgs e)
        {
            RemoveTorrentsPrompt();
        }

        public void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bandwidthPriorityButton_Click(object sender, EventArgs e)
        {
            JsonObject request = Requests.Generic(ProtocolConstants.METHOD_TORRENTSET, torrentListView.SelectedItems.Count > 0 ? BuildIdArray() : null);
            JsonObject arguments = Requests.GetArgObject(request);
            arguments.Put(ProtocolConstants.FIELD_BANDWIDTHPRIORITY, (int)((MenuItem)sender).Tag);
            Program.Form.SetupAction(CommandFactory.RequestAsync(request));
        }

        private void startTorrentButton_Click(object sender, EventArgs e)
        {
            Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.Generic(ProtocolConstants.METHOD_TORRENTSTART, torrentListView.SelectedItems.Count > 0 ? BuildIdArray() : null)));
        }

        private void pauseTorrentButton_Click(object sender, EventArgs e)
        {
            Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.Generic(ProtocolConstants.METHOD_TORRENTSTOP, torrentListView.SelectedItems.Count > 0 ? BuildIdArray() : null)));
        }

        public void UpdateGraph(int downspeed, int upspeed)
        {
            speedGraph.Push(downspeed, "Download");
            speedGraph.Push(upspeed, "Upload");
            speedGraph.UpdateGraph();
        }

        public string GetSummaryStatus()
        {
            long totalUpload = 0;
            long totalDownload = 0;
            int totalTorrents = 0;
            int totalSeeding = 0;
            int totalDownloading = 0;
            long totalSize = 0;
            long totalDownloadedSize = 0;
            long selectedSize = 0;
            long selectedDownloadedSize = 0;
            int selected = 0;

            int totalPaused = 0;
            int totalPausedFinished = 0;
            decimal activePercentage = 0;

            lock (torrentListView)
            {
                foreach (Torrent t in torrentListView.Items)
                {
                    totalTorrents++;
                    totalUpload += t.UploadRate;
                    totalDownload += t.DownloadRate;
                    totalSize += t.TotalSize;
                    totalDownloadedSize += t.HaveTotal;
                    if (t.Selected)
                    {
                        selected++;
                        selectedSize += t.TotalSize;
                        selectedDownloadedSize += t.HaveTotal;
                    }
                    if (t.StatusCode == ProtocolConstants.STATUS_DOWNLOADING)
                    {
                        totalDownloading++;
                        activePercentage += t.Percentage;
                    }
                    else if (t.StatusCode == ProtocolConstants.STATUS_PAUSED)
                    {
                        if (t.Percentage < 100)
                        {
                            totalPaused++;
                            activePercentage += t.Percentage;
                        }
                        else
                            totalPausedFinished++;
                    }
                    else if (t.StatusCode == ProtocolConstants.STATUS_SEEDING)
                    {
                        totalSeeding++;
                    }
                }
            }
            if (totalPaused + totalDownloading > 0)
            {
                if (totalDownloading == 0)
                {
                    taskbar.SetNormal(true);
                }
                else
                    taskbar.SetNormal(false);

                if (totalPaused + totalPausedFinished == totalTorrents)
                    taskbar.SetPaused();

                taskbar.UpdateProgress(activePercentage / (totalPaused + totalDownloading));
            }
            else
                taskbar.SetNoProgress();

            return String.Format(
                selected > 1 ? "{0} {1}, {2} {3} | {4} {5}: {6} {7}, {8} {9} | {12} {13}: {14} / {15}"
                      : "{0} {1}, {2} {3} | {4} {5}: {6} {7}, {8} {9} | {10} / {11}",
                new object[] {
                        Toolbox.GetSpeed(totalDownload),
                        OtherStrings.Down.ToLower(),
                        Toolbox.GetSpeed(totalUpload),
                        OtherStrings.Up.ToLower(),
                        totalTorrents,
                        OtherStrings.Torrents.ToLower(),
                        totalDownloading,
                        OtherStrings.Downloading.ToLower(),
                        totalSeeding,
                        OtherStrings.Seeding.ToLower(),
                        Toolbox.GetFileSize(totalDownloadedSize),
                        Toolbox.GetFileSize(totalSize),
                        selected, OtherStrings.ItemsSelected,
                        Toolbox.GetFileSize(selectedDownloadedSize),
                        Toolbox.GetFileSize(selectedSize),
                    }
                );
        }

        public void UpdateStatus(string text, bool updatenotify)
        {
            toolStripStatusLabel.Text = text;
            if (updatenotify)
                notifyIcon.Text = text.Length < 64 ? text : text.Substring(0, 63);
        }

        public void addTorrentButton_Click(object sender, EventArgs e)
        {
            if (Program.Connected)
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = OtherStrings.OpenFileFilter;
                openFile.RestoreDirectory = true;
                openFile.Multiselect = true;
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    Upload(openFile.FileNames, Program.Settings.UploadPrompt);
                }
            }
        }

        public void Upload(string[] args, bool uploadprompt)
        {
            foreach (string s in args)
            {
                if (s == null || s.Length == 0)
                    continue;
                if (File.Exists(s))
                {
                    if (uploadprompt)
                    {
                        TorrentLoadDialog dialog = new TorrentLoadDialog(s);
                        dialog.ShowDialog();
                    }
                    else
                    {
                        try
                        {
                            Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.TorrentAddByFile(s, false)));
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, OtherStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.TorrentAddByUrl(s)));
                }
            }
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            if (this.minimise)
            {
                this.minimise = false;
                this.Hide();
            }
        }

        private void torrentListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                lvwColumnSorter.Order = (lvwColumnSorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending);
            }
            else
            {
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }
            this.torrentListView.Sort();
#if !MONO
            this.torrentListView.SetSortIcon(lvwColumnSorter.SortColumn, lvwColumnSorter.Order);
#endif
            Toolbox.StripeListView(torrentListView);
        }

        public void disconnectButton_Click(object sender, EventArgs e)
        {
            if (Program.Connected)
                Program.Connected = false;
            sessionWebClient.CancelAsync();
            refreshWebClient.CancelAsync();
            filesWebClient.CancelAsync();

        }

        public void connectButton_Click(object sender, EventArgs e)
        {
            fileToolStripMenuItem.DropDown.Close();
            if (!Program.Connected)
                Connect();
        }

        private void addWebTorrentButton_Click(object sender, EventArgs e)
        {
            if (Program.Connected)
            {
                UriPromptWindow uriPrompt = new UriPromptWindow();
                uriPrompt.ShowDialog();
            }
        }

        private void stateListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterByStateOrTracker();
        }

        private void FilterTorrentTextBox_TextChanged(object sender, EventArgs e)
        {
            FilterByStateOrTracker();
        }

        static bool FilteringProcess = false;
        private void FilterByStateOrTracker()
        {
            if (FilteringProcess)
                return;
            FilteringProcess = true; /* Race condition is not important, so we not lock */
            try
            {
                torrentListView.BeginUpdate();
                lock (torrentListView)
                {
                    IComparer tmp = torrentListView.ListViewItemSorter;
                    torrentListView.ListViewItemSorter = null;
                    if (stateListBox.SelectedIndex == 1)
                    {
                        FilterTorrent(IfTorrentStatus, ProtocolConstants.STATUS_DOWNLOADING);
                    }
                    else if (stateListBox.SelectedIndex == 2)
                    {
                        FilterTorrent(IfTorrentStatus, ProtocolConstants.STATUS_PAUSED);
                    }
                    else if (stateListBox.SelectedIndex == 3)
                    {
                        FilterTorrent(IfTorrentStatus, (short)(ProtocolConstants.STATUS_CHECKING | ProtocolConstants.STATUS_WAITING_TO_CHECK));
                    }
                    else if (stateListBox.SelectedIndex == 4)
                    {
                        FilterTorrent(IsFinished, null);
                    }
                    else if (stateListBox.SelectedIndex == 5)
                    {
                        FilterTorrent(NotFinished, null);
                    }
                    else if (stateListBox.SelectedIndex == 6)
                    {
                        FilterTorrent(IfTorrentStatus, ProtocolConstants.STATUS_SEEDING);
                    }
                    else if (stateListBox.SelectedIndex == 7)
                    {
                        FilterTorrent(TorrentHasError, null);
                    }
                    else if (stateListBox.SelectedIndex > 8)
                    {
                        FilterTorrent(UsingTracker, stateListBox.SelectedItem.ToString());
                    }
                    else
                    {
                        FilterTorrent(AlwaysTrue, null);
                    }
                    torrentListView.ListViewItemSorter = tmp;
                    Toolbox.StripeListView(torrentListView);
                }
            }
            finally
            {
                torrentListView.EndUpdate();
                FilteringProcess = false;
            }
        }

        private delegate bool FilterCompare(Torrent t, object param);
        private void FilterTorrent(FilterCompare fc, object param)
        {
            lock (Program.TorrentIndex)
            {
                string filterstring = FilterTorrentTextBox.Text.ToLower();
                foreach (KeyValuePair<string, Torrent> pair in Program.TorrentIndex)
                {
                    if (fc(pair.Value, param) && (filterstring.Length == 0 || pair.Value.TorrentName.ToLower().Contains(filterstring)))
                    {
                        pair.Value.Show();
                    }
                    else
                    {
                        pair.Value.RemoveItem();
                    }
                }
            }
        }
        private bool IfTorrentStatus(Torrent t, object statusCode)
        {
            return (t.StatusCode & (short)statusCode) > 0;
        }
        private bool IsFinished(Torrent t, object dummy)
        {
            return t.IsFinished;
        }
        private bool NotFinished(Torrent t, object dummy)
        {
            return !t.IsFinished;
        }
        private bool TorrentHasError(Torrent t, object dummy)
        {
            return t.HasError;
        }
        private bool UsingTracker(Torrent t, object tracker)
        {
            return t.FirstTrackerTrimmed.Equals(tracker);
        }
        private bool AlwaysTrue(Torrent t, object dummy)
        {
            return true;
        }

        private void filesTimer_Tick(object sender, EventArgs e)
        {
            if (!filesWebClient.IsBusy)
            {
                filesTimer.Enabled = false;
                lock (torrentListView)
                {
                    if (torrentListView.SelectedItems.Count == 1)
                    {
                        Torrent t = (Torrent)torrentListView.SelectedItems[0];
                        filesWebClient = CommandFactory.RequestAsync(Requests.Files(t.Id));
                    }
                }
            }
        }

        private void SetFilesItemState(string datatype, int column)
        {
            JsonArray array = new JsonArray();
            lock (filesListView)
            {
                lock (filesListView.Items)
                {
                    foreach (FileListViewItem item in filesListView.SelectedItems)
                    {
                        array.Add(item.FileIndex);
                    }
                }
            }
            DispatchFilesUpdate(datatype, array);
        }

        private void SetHighPriorityHandler(object sender, EventArgs e)
        {
            SetFilesItemState(ProtocolConstants.PRIORITY_HIGH, 6);
        }

        private void SetLowPriorityHandler(object sender, EventArgs e)
        {
            SetFilesItemState(ProtocolConstants.PRIORITY_LOW, 6);
        }

        private void SetNormalPriorityHandler(object sender, EventArgs e)
        {
            SetFilesItemState(ProtocolConstants.PRIORITY_NORMAL, 6);
        }

        private void SetUnwantedHandler(object sender, EventArgs e)
        {
            SetFilesItemState(ProtocolConstants.FILES_UNWANTED, 5);
        }

        private void SetWantedHandler(object sender, EventArgs e)
        {
            SetFilesItemState(ProtocolConstants.FILES_WANTED, 5);
        }

        public void SetAllStateCounters()
        {
            int all = 0;
            int downloading = 0;
            int paused = 0;
            int checking = 0;
            int complete = 0;
            int incomplete = 0;
            int seeding = 0;
            int broken = 0;
            Dictionary<string, int> trackers = new Dictionary<string, int>();
            lock (Program.TorrentIndex)
            {
                all = Program.TorrentIndex.Count;
                foreach (KeyValuePair<string, Torrent> t in Program.TorrentIndex)
                {
                    short statusCode = t.Value.StatusCode;
                    if (t.Value.FirstTrackerTrimmed != null)
                    {
                        if (trackers.ContainsKey(t.Value.FirstTrackerTrimmed))
                            trackers[t.Value.FirstTrackerTrimmed] = trackers[t.Value.FirstTrackerTrimmed] + 1;
                        else
                            trackers[t.Value.FirstTrackerTrimmed] = 1;
                    }
                    if (t.Value.HasError)
                    {
                        broken++;
                    }
                    if (statusCode == ProtocolConstants.STATUS_DOWNLOADING)
                    {
                        downloading++;
                    }
                    else if (statusCode == ProtocolConstants.STATUS_PAUSED)
                    {
                        paused++;
                    }
                    else if (statusCode == ProtocolConstants.STATUS_SEEDING)
                    {
                        seeding++;
                    }
                    else if (statusCode == ProtocolConstants.STATUS_WAITING_TO_CHECK || statusCode == ProtocolConstants.STATUS_CHECKING)
                    {
                        checking++;
                    }

                    if (t.Value.IsFinished)
                    {
                        complete++;
                    }
                    else
                    {
                        incomplete++;
                    }
                }
            }
            SetStateCounter(0, all);
            SetStateCounter(1, downloading);
            SetStateCounter(2, paused);
            SetStateCounter(3, checking);
            SetStateCounter(4, complete);
            SetStateCounter(5, incomplete);
            SetStateCounter(6, seeding);
            SetStateCounter(7, broken);
            foreach (KeyValuePair<string, int> pair in trackers)
            {
                GListBoxItem item = stateListBox.FindItem(pair.Key);
                if (item != null)
                {
                    item.Counter = pair.Value;
                }
            }
            stateListBox.Refresh();
        }

        private void SetStateCounter(int index, int count)
        {
            ((GListBoxItem)stateListBox.Items[index]).Counter = count;
        }

        private void filesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            filesListView.ContextMenu = filesListView.SelectedItems.Count > 0 ? this.fileSelectionMenu : this.noFileSelectionMenu;
        }

        private void DispatchFilesUpdate(string datatype, JsonArray FileList)
        {
            Torrent t;
            lock (torrentListView)
            {
                if (torrentListView.SelectedItems.Count != 1)
                {
                    return;
                }
                t = (Torrent)torrentListView.SelectedItems[0];
            }
            JsonObject request = new JsonObject();
            request.Put(ProtocolConstants.KEY_METHOD, ProtocolConstants.METHOD_TORRENTSET);
            JsonObject arguments = new JsonObject();
            JsonArray ids = new JsonArray();
            ids.Put(t.Id);
            arguments.Put(ProtocolConstants.KEY_IDS, ids);
            if (FileList.Count == filesListView.Items.Count)
            {
                arguments.Put(datatype, new JsonArray());
            }
            else if (FileList.Count > 0)
            {
                arguments.Put(datatype, FileList);
            }
            request.Put(ProtocolConstants.KEY_ARGUMENTS, arguments);
            request.Put(ProtocolConstants.KEY_TAG, (int)ResponseTag.DoNothing);
            Program.Form.SetupAction(CommandFactory.RequestAsync(request)).Completed +=
                delegate(object sender, ResultEventArgs e)
                {
                    if (e.Result.GetType() != typeof(ErrorCommand))
                    {
                        Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.FilesAndPriorities(t.Id)));
                    }
                };
        }

        // lock torrentListView BEFORE calling this method
        public void UpdateInfoPanel(bool first, Torrent t)
        {
            if (first)
            {
                generalTorrentInfo.torrentName = peersTorrentNameGroupBox.Text
                    = trackersTorrentNameGroupBox.Text = filesTorrentNameGroupBox.Text
                    = t.TorrentName;
                generalTorrentInfo.startedAt = t.Added.ToString();
                generalTorrentInfo.createdAt = t.Created;
                generalTorrentInfo.createdBy = t.Creator;
                generalTorrentInfo.hash = string.Join(" ", Toolbox.Split(t.Hash.ToUpper(), 8));
                generalTorrentInfo.comment = t.Comment;
                trackersListView.BeginUpdate();
                trackersListView.Items.Clear();
                foreach (JsonObject tracker in t.Trackers)
                {
                    int tier = Toolbox.ToInt(tracker[ProtocolConstants.TIER]);
                    string announceUrl = (string)tracker[ProtocolConstants.ANNOUNCE];
                    ListViewItem item = new ListViewItem(tier.ToString());
                    item.SubItems.Add(announceUrl);
                    while (item.SubItems.Count < 7)
                        item.SubItems.Add("");
                    item.Name = Toolbox.ToInt(tracker[ProtocolConstants.FIELD_IDENTIFIER], -1).ToString();
                    trackersListView.Items.Add(item);
                }
                Toolbox.StripeListView(trackersListView);
                trackersListView.Enabled = true;
                trackersListView.EndUpdate();
                downloadProgressLabel.Text = ((piecesGraph.Visible = t.Pieces != null) ? OtherStrings.Pieces : OtherStrings.Progress) + ": ";
                progressBar.Visible = !piecesGraph.Visible;
            }
            if (t.TrackerStats != null)
            {
                trackersListView.BeginUpdate();
                foreach (JsonObject trackerstat in t.TrackerStats)
                {
                    int id = Toolbox.ToInt(trackerstat[ProtocolConstants.FIELD_IDENTIFIER], -1);
                    if (id >= 0 && trackersListView.Items.ContainsKey(id.ToString()))
                    {
                        ListViewItem item = trackersListView.Items[id.ToString()];
                        double nat = Toolbox.ToDouble(trackerstat["nextAnnounceTime"]);
                        int seederCount = Toolbox.ToInt(trackerstat["seederCount"]);
                        int leecherCount = Toolbox.ToInt(trackerstat["leecherCount"]);
                        int downloadCount = Toolbox.ToInt(trackerstat["downloadCount"]);
                        item.SubItems[2].Text = (string)trackerstat["lastAnnounceResult"];
                        if (nat > 0.0)
                        {
                            TimeSpan ts = Toolbox.DateFromEpoch(nat).ToLocalTime().Subtract(DateTime.Now);
                            item.SubItems[3].Text = ts.Ticks > 0 ? Toolbox.FormatTimespanLong(ts) : OtherStrings.UnknownNegativeResult;
                        }
                        else
                            item.SubItems[3].Text = "";
                        item.SubItems[4].Text = seederCount >= 0 ? seederCount.ToString() : "";
                        item.SubItems[5].Text = leecherCount >= 0 ? leecherCount.ToString() : "";
                        item.SubItems[6].Text = downloadCount >= 0 ? downloadCount.ToString() : "";
                    }
                }
                trackersListView.EndUpdate();
            }
            generalTorrentInfo.remaining = t.IsFinished ? (t.DoneDate != null ? t.DoneDate.ToString() : "?") : (t.Eta > 0 ? t.LongEta : "");
            generalTorrentInfo.timeLabelText = (t.IsFinished ? torrentCompletedAtCol.Text : torrentEtaCol.Text) + ":";
            generalTorrentInfo.uploaded = t.UploadedString;
            generalTorrentInfo.uploadLimit = t.SpeedLimitUpEnabled ? Toolbox.KbpsString(t.SpeedLimitUp) : "∞";
            generalTorrentInfo.uploadSpeed = t.UploadRateString;
            generalTorrentInfo.seeders = String.Format(OtherStrings.XOfYConnected, t.PeersSendingToUs, t.Seeders < 0 ? "?" : t.Seeders.ToString());
            generalTorrentInfo.leechers = String.Format(OtherStrings.XOfYConnected, t.PeersGettingFromUs, t.Leechers < 0 ? "?" : t.Leechers.ToString());
            generalTorrentInfo.ratio = t.LocalRatioString;
            progressBar.Value = (int)t.Percentage;
            if (t.Pieces != null)
            {
                piecesGraph.ApplyBits(t.Pieces, t.PieceCount);
                generalTorrentInfo.piecesInfo = String.Format(OtherStrings.PiecesInfo, t.PieceCount, Toolbox.GetFileSize(t.PieceSize), t.HavePieces);
            }
            else
                generalTorrentInfo.piecesInfo = String.Format("{0} x {1}", t.PieceCount, Toolbox.GetFileSize(t.PieceSize));
            generalTorrentInfo.location = t.DownloadDir + "/" + t.TorrentName;
            percentageLabel.Text = t.Percentage.ToString() + "%";
            if (t.TotalSize == t.SizeWhenDone)
            {
                generalTorrentInfo.totalSize = String.Format(OtherStrings.TotalDoneValidSize, Toolbox.GetFileSize(t.SizeWhenDone), t.HaveTotalString, Toolbox.GetFileSize(t.HaveValid));
            }
            else
            {
                generalTorrentInfo.totalSize = String.Format(OtherStrings.TotalDoneValidTotalSize, Toolbox.GetFileSize(t.SizeWhenDone), t.HaveTotalString, Toolbox.GetFileSize(t.HaveValid), Toolbox.GetFileSize(t.TotalSize));
            }
            //totalSizeLabel.Text = String.Format(OtherStrings.DownloadedValid, t.HaveTotalString, Toolbox.GetFileSize(t.HaveValid));
            generalTorrentInfo.downloaded = Toolbox.GetFileSize(t.Downloaded);
            generalTorrentInfo.downloadSpeed = t.DownloadRateString;
            generalTorrentInfo.downloadLimit = t.SpeedLimitDownEnabled ? Toolbox.KbpsString(t.SpeedLimitDown) : "∞";
            generalTorrentInfo.status = t.Status;
            generalTorrentInfo.errorVisible = !(generalTorrentInfo.error = t.ErrorString).Equals("");
            RefreshElapsedTimer();
            peersListView.Enabled = t.StatusCode != ProtocolConstants.STATUS_PAUSED;
            if (t.Peers != null && peersListView.Enabled)
            {
                PeerListViewItem.CurrentUpdateSerial++;
                lock (peersListView)
                {
                    peersListView.BeginUpdate();
                    IComparer tmp = peersListView.ListViewItemSorter;
                    peersListView.ListViewItemSorter = null;
                    foreach (JsonObject peer in t.Peers)
                    {
                        PeerListViewItem item = FindPeerItem((string)peer[ProtocolConstants.ADDRESS]);
                        if (item == null)
                        {
                            item = new PeerListViewItem(peer);
                            peersListView.Items.Add(item);
                        }
                        else
                        {
                            item.Update(peer);
                        }
                        item.UpdateSerial = PeerListViewItem.CurrentUpdateSerial;
                    }
                    PeerListViewItem[] peers = (PeerListViewItem[])new ArrayList(peersListView.Items).ToArray(typeof(PeerListViewItem));
                    foreach (PeerListViewItem item in peers)
                    {
                        if (item.UpdateSerial != PeerListViewItem.CurrentUpdateSerial)
                        {
                            peersListView.Items.Remove(item);
                        }
                    }
                    peersListView.ListViewItemSorter = tmp;
                    Toolbox.StripeListView(peersListView);
                    peersListView.EndUpdate();
                }
            }
        }

        private PeerListViewItem FindPeerItem(string address)
        {
            lock (peersListView)
            {
                foreach (PeerListViewItem peer in peersListView.Items)
                {
                    if (peer.Address.Equals(address))
                    {
                        return peer;
                    }
                }
            }
            return null;
        }

        private void refreshElapsedTimer_Tick(object sender, EventArgs e)
        {
            RefreshElapsedTimer();
        }

        private void RefreshElapsedTimer()
        {
            lock (torrentListView)
            {
                if (torrentListView.SelectedItems.Count == 1)
                {
                    Torrent t = (Torrent)torrentListView.SelectedItems[0];
                    TimeSpan ts = DateTime.Now.Subtract(t.Added);
                    generalTorrentInfo.timeElapsed = ts.Ticks > 0 ? Toolbox.FormatTimespanLong(ts) : OtherStrings.UnknownNegativeResult;
                }
                else
                {
                    refreshElapsedTimer.Enabled = false;
                }
            }
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (notifyIcon.Visible)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.Hide();
                }
                else
                {
                    this.notifyIcon.Tag = this.WindowState;
                }
            }
        }

        private void projectSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(PROJECT_SITE);
        }

        private void showErrorLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassSingleton<ErrorLogWindow>.Instance.Show();
            ClassSingleton<ErrorLogWindow>.Instance.BringToFront();
        }

        private void filesListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == filesLvwColumnSorter.SortColumn)
            {
                filesLvwColumnSorter.Order = (filesLvwColumnSorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending);
            }
            else
            {
                filesLvwColumnSorter.SortColumn = e.Column;
                filesLvwColumnSorter.Order = SortOrder.Ascending;
            }
            this.filesListView.Sort();
#if !MONO
            this.filesListView.SetSortIcon(filesLvwColumnSorter.SortColumn, filesLvwColumnSorter.Order);
#endif
            Toolbox.StripeListView(filesListView);
        }

        private void peersListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == peersLvwColumnSorter.SortColumn)
            {
                peersLvwColumnSorter.Order = (peersLvwColumnSorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending);
            }
            else
            {
                peersLvwColumnSorter.SortColumn = e.Column;
                peersLvwColumnSorter.Order = SortOrder.Ascending;
            }
            this.peersListView.Sort();
#if !MONO
            this.peersListView.SetSortIcon(peersLvwColumnSorter.SortColumn, peersLvwColumnSorter.Order);
#endif
            Toolbox.StripeListView(peersListView);
        }

        private void SpeedResComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (speedResComboBox.SelectedIndex)
            {
                case 3:
                    speedGraph.LineInterval = 0.5F;
                    break;
                case 2:
                    speedGraph.LineInterval = 5;
                    break;
                case 1:
                    speedGraph.LineInterval = 15;
                    break;
                default:
                    speedGraph.LineInterval = 30;
                    break;
            }
            speedGraph.UpdateGraph();
        }

        private void torrentListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                Toolbox.SelectAll(torrentListView);
            }
        }


        private void torrentDetailsTabListView_KeyDown(object sender, KeyEventArgs e)
        {
            ListView listView = (ListView)sender;
            if (e.KeyCode == Keys.A && e.Control)
            {
                Toolbox.SelectAll(listView);
            }
            else if (e.KeyCode == Keys.C && e.Control)
            {
                Toolbox.CopyListViewToClipboard(listView);
            }
        }

        private void recheckTorrentButton_Click(object sender, EventArgs e)
        {
            if (torrentListView.SelectedItems.Count > 0)
            {
                string question = torrentListView.SelectedItems.Count == 1 ? String.Format(OtherStrings.ConfirmSingleRecheck, torrentListView.SelectedItems[0].Text) : String.Format(OtherStrings.ConfirmMultipleRecheck, torrentListView.SelectedItems.Count);
                if (MessageBox.Show(question, OtherStrings.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.Generic(ProtocolConstants.METHOD_TORRENTVERIFY, BuildIdArray())));
                }
            }
        }

        private void removeAndDeleteButton_Click(object sender, EventArgs e)
        {
            RemoveAndDeleteTorrentsPrompt();
        }

        private void sessionStatsButton_Click(object sender, EventArgs e)
        {
            ClassSingleton<StatsDialog>.Instance.Show();
            ClassSingleton<StatsDialog>.Instance.BringToFront();
        }

        private void RssButton_Click(object sender, EventArgs e)
        {
            ClassSingleton<RssForm>.Instance.Show();
            ClassSingleton<RssForm>.Instance.BringToFront();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocalSettings settings = Program.Settings;
            if (settings.MinToTray && settings.MinOnClose && e.CloseReason == CloseReason.UserClosing)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
            else
            {
                if (this.WindowState != FormWindowState.Minimized)
                    settings.Misc[CONFKEY_MAINWINDOW_STATE] = (int)this.WindowState;
                else
                    settings.Misc[CONFKEY_MAINWINDOW_STATE] = (int)this.notifyIcon.Tag;
                if (this.WindowState.Equals(FormWindowState.Normal))
                {
                    settings.SetObject(CONFKEY_MAINWINDOW_LOCATION_X, this.Location.X);
                    settings.SetObject(CONFKEY_MAINWINDOW_LOCATION_Y, this.Location.Y);
                    settings.SetObject(CONFKEY_MAINWINDOW_HEIGHT, this.Size.Height);
                    settings.SetObject(CONFKEY_MAINWINDOW_WIDTH, this.Size.Width);
                }
                else
                {
                    /* The value of the RestoreBounds property is valid only when 
                       the WindowState property of the Form class is not equal to Normal. */
                    settings.SetObject(CONFKEY_MAINWINDOW_LOCATION_X, this.RestoreBounds.X);
                    settings.SetObject(CONFKEY_MAINWINDOW_LOCATION_Y, this.RestoreBounds.Y);
                    settings.SetObject(CONFKEY_MAINWINDOW_HEIGHT, this.RestoreBounds.Height);
                    settings.SetObject(CONFKEY_MAINWINDOW_WIDTH, this.RestoreBounds.Width);
                }
                settings.SetObject(CONFKEY_FILTER_SPLITTERDISTANCE, this.mainVerticalSplitContainer.SplitterDistance);
            }
            SaveListViewProperties(torrentListView);
            SaveListViewProperties(filesListView);
            SaveListViewProperties(peersListView);
            settings.SetObject(CONFKEY_MAINWINDOW_FILTERSPANEL_COLLAPSED, this.showCategoriesPanelToolStripMenuItem.Checked ? 0 : 1);
            settings.SetObject(CONFKEY_MAINWINDOW_DETAILSPANEL_COLLAPSED, this.torrentAndTabsSplitContainer.Panel2Collapsed ? 1 : 0);
            settings.Commit();
        }

        private void MainWindow_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                refreshTimer.Interval = Program.Settings.Current.RefreshRate * 1000;
                filesTimer.Interval = Program.Settings.Current.RefreshRate * 1000 * LocalSettingsSingleton.FILES_REFRESH_MULTIPLICANT;
            }
            else
            {
                refreshTimer.Interval = Program.Settings.Current.RefreshRateTray * 1000;
                filesTimer.Interval = Program.Settings.Current.RefreshRateTray * 1000 * LocalSettingsSingleton.FILES_REFRESH_MULTIPLICANT;
            }
        }

        private void checkForNewVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCheckVersion(true);
        }

        private void DoCheckVersion(bool alwaysnotify)
        {
            BackgroundWorker checkVersionWorker = new BackgroundWorker();
            checkVersionWorker.DoWork += new DoWorkEventHandler(checkVersionWorker_DoWork);
            checkVersionWorker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
            {
                checkVersionWorker_RunWorkerCompleted(sender, e, alwaysnotify);
            };
            checkVersionWorker.RunWorkerAsync(alwaysnotify);
        }

        private void checkVersionWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e, bool alwaysnotify)
        {
            if (!e.Cancelled)
            {
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message, OtherStrings.LatestVersionCheckFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.Result.GetType() == typeof(Version))
                {
                    Version latestVersion = (Version)e.Result;
                    Version thisVersion = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
                    if (latestVersion > thisVersion)
                    {
                        if (MessageBox.Show(String.Format(OtherStrings.NewerVersion, latestVersion.Major, latestVersion.Minor), OtherStrings.UpgradeAvailable, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                            == DialogResult.Yes)
                        {
                            Process.Start(DOWNLOADS_PAGE);
                        }
                    }
                    else
                    {
                        if (alwaysnotify)
                            MessageBox.Show(String.Format(OtherStrings.LatestVersion, thisVersion.Major, thisVersion.Minor), OtherStrings.NoUpgradeAvailable, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void checkVersionWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            TransmissionWebClient client = new TransmissionWebClient(false, false);
            string response = client.DownloadString(Program.Settings.UpdateToBeta ? LATEST_VERSION_BETA : LATEST_VERSION);
            if (!response.StartsWith("#LATESTVERSION#"))
                throw new FormatException("Response didn't contain the identification prefix.");
            string[] latestVersion = response.Remove(0, 15).Split('.');
            if (latestVersion.Length != 4)
                throw new FormatException("Incorrect number format");
            e.Result = new Version(Int32.Parse(latestVersion[0]), Int32.Parse(latestVersion[1]), Int32.Parse(latestVersion[2]), Int32.Parse(latestVersion[3]));
        }

        private void updateGeoipDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCheckGeoip(true);
        }

        private void DoCheckGeoip(bool alwaysnotify)
        {
            Directory.CreateDirectory(Toolbox.GetApplicationData());
            TransmissionWebClient client = new TransmissionWebClient(false, false);
            client.DownloadFileCompleted += delegate(object sender, AsyncCompletedEventArgs e)
            {
                geoip_DownloadFileCompleted(e, alwaysnotify);
            };
            client.DownloadFileAsync(new Uri("http://geolite.maxmind.com/download/geoip/database/GeoLiteCountry/GeoIP.dat.gz"), Toolbox.LocateFile(GeoIPCountry.GEOIP_DATABASE_FILE + ".tmp", false, Toolbox.GetApplicationData()));
        }

        void geoip_DownloadFileCompleted(AsyncCompletedEventArgs e, bool alwaysnotify)
        {
            if (!e.Cancelled)
            {
                try
                {
                    if (e.Error != null)
                    {
                        throw e.Error;
                    }
                    else
                    {
                        string dest = Toolbox.LocateFile(GeoIPCountry.GEOIP_DATABASE_FILE, false, Toolbox.GetApplicationData());
                        if (File.Exists(dest))
                        {
                            File.Delete(dest);
                        }
                        File.Move(Toolbox.LocateFile(GeoIPCountry.GEOIP_DATABASE_FILE + ".tmp", false, Toolbox.GetApplicationData()), dest);
                        GeoIPCountry.ReOpen();
                        if (alwaysnotify)
                            MessageBox.Show(OtherStrings.GeoipDatabaseUpdateCompleted, OtherStrings.GeoipDatabase, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ee)
                {
                    if (alwaysnotify)
                        MessageBox.Show(ee.Message, OtherStrings.GeoipDatabaseUpdateFailed, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void showDetailsPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            torrentAndTabsSplitContainer.Panel2Collapsed = !torrentAndTabsSplitContainer.Panel2Collapsed;
            showDetailsPanelToolStripMenuItem.Checked = !torrentAndTabsSplitContainer.Panel2Collapsed;
        }

        private void runCmdButton_Click(object sender, EventArgs e)
        {
            if (torrentListView.SelectedItems.Count > 0)
            {
                try
                {
                    Torrent t = (Torrent)torrentListView.SelectedItems[0];
                    Process.Start(
                        Program.Settings.PlinkPath,
                        String.Format(
                            "-t \"{0}\" \"{1}\"",
                            Program.Settings.Current.Host,
                            String.Format(
                                Program.Settings.Current.PlinkCmd.Replace("$DATA", "{0}").Replace("$TORRENTID", t.Id.ToString()),
                                String.Format("{0}{1}{2}", t.DownloadDir, !t.DownloadDir.EndsWith("/") ? "/" : null, t.TorrentName))
                        ));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, OtherStrings.UnableRunPlink, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void reannounceButton_ButtonClick(object sender, EventArgs e)
        {
            Reannounce(ReannounceMode.Specific);
        }

        private void reannounceAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reannounce(ReannounceMode.All);
        }

        private void Reannounce(ReannounceMode mode)
        {
            Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.Reannounce(mode, mode.Equals(ReannounceMode.Specific) ? BuildIdArray() : null)));
        }

        private void recentlyActiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reannounce(ReannounceMode.RecentlyActive);
        }

        private void openNetworkShareDir_Click(object sender, EventArgs e)
        {
            if (torrentListView.SelectedItems.Count == 1)
            {
                Torrent t = (Torrent)torrentListView.SelectedItems[0];
                string sambaPath = t.SambaLocation;
                if (sambaPath != null)
                {
                    try
                    {
                        BackgroundProcessStart(new ProcessStartInfo(sambaPath));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, OtherStrings.UnableToOpenNetworkShare, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void openNetworkShare_Click(object sender, EventArgs e)
        {
            if (torrentListView.SelectedItems.Count == 1)
            {
                Torrent t = (Torrent)torrentListView.SelectedItems[0];
                string sambaPath = t.SambaLocation;
                if (sambaPath != null)
                {
                    try
                    {
                        BackgroundProcessStart(new ProcessStartInfo(sambaPath + "\\" + t.TorrentName));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, OtherStrings.UnableToOpenNetworkShare, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void categoriesPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showCategoriesPanelToolStripMenuItem.Checked = !showCategoriesPanelToolStripMenuItem.Checked;
            mainVerticalSplitContainer.Panel1Collapsed = !showCategoriesPanelToolStripMenuItem.Checked || !Program.Connected;
        }

        private void moveTorrentDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new MoveDataPrompt(this.torrentListView.SelectedItems)).ShowDialog();
        }

        private void addTorrentWithOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* This crashes 1.6x */
            if (Program.Connected && (Program.DaemonDescriptor.Version < 1.60 || Program.DaemonDescriptor.Version >= 1.7))
            {
                if (openTorrentFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fileName in openTorrentFileDialog.FileNames)
                    {
                        (new TorrentLoadDialog(fileName)).ShowDialog();
                    }
                }
            }
        }

        private void BackgroundProcessStart(ProcessStartInfo info)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync(info);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
                MessageBox.Show(((Exception)e.Result).Message, OtherStrings.UnableToOpen, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Process.Start((ProcessStartInfo)e.Argument);
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        private void filesListView_DoubleClick(object sender, EventArgs e)
        {
            if (filesListView.Enabled && torrentListView.SelectedItems.Count == 1 && filesListView.SelectedItems.Count == 1)
            {
                Torrent t = (Torrent)torrentListView.SelectedItems[0];
                string sambaShare = t.SambaLocation;
                if (sambaShare != null)
                {
                    BackgroundProcessStart(new ProcessStartInfo((bool)filesListView.SelectedItems[0].SubItems[0].Tag ? sambaShare + @"\" + filesListView.SelectedItems[0].SubItems[0].Text.Replace(@"/", @"\") : sambaShare));
                }
            }
        }

        private void connectButton_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripDropDownItem connectitem = sender as ToolStripDropDownItem;
            foreach (ToolStripMenuItem item in connectitem.DropDownItems)
            {
                item.Checked = Program.Settings.CurrentProfile.Equals(item.ToString());
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.Connected)
                if (FindDialog == null)
                {
                    FindDialog = new FindDialog();
                    FindDialog.Torrentlistview = torrentListView;
                    FindDialog.FormClosed += delegate(object s, FormClosedEventArgs ee) { FindDialog = null; };
                    FindDialog.Show();
                }
                else
                {
                    FindDialog.Focus();
                }
        }

        private void AltSpeedButton_Click(object sender, EventArgs e)
        {
            JsonObject request = Requests.CreateBasicObject(ProtocolConstants.METHOD_SESSIONSET);
            JsonObject arguments = Requests.GetArgObject(request);
            arguments.Put(ProtocolConstants.FIELD_ALTSPEEDENABLED, !(bool)AltSpeedButton.Tag);
            CommandFactory.RequestAsync(request).Completed +=
                delegate(object dsender, ResultEventArgs de)
                {
                    if (de.Result.GetType() != typeof(ErrorCommand) && !sessionWebClient.IsBusy)
                    {
                        sessionWebClient = CommandFactory.RequestAsync(Requests.SessionGet());
                    }
                };
        }
        class ToolStripBitmap
        {
            public string Name;
            public Bitmap Image;
            public ToolStripItem[] Controls;
        }

        private void exportLocalSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveSettingsFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileLocalSettingsStore store = new FileLocalSettingsStore();
                store.Save(saveSettingsFileDialog.FileName, Program.Settings.SaveToJson());
            }
        }

        private void importLocalSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openSettingsFileDialog.ShowDialog() == DialogResult.OK)
                try
                {
                    LocalSettings sett = Program.Settings;
                    string originalHost = sett.Current.Host;
                    int originalPort = sett.Current.Port;
                    FileLocalSettingsStore store = new FileLocalSettingsStore();
                    JsonObject jo = store.Load(openSettingsFileDialog.FileName);
                    LocalSettings newsettings = new LocalSettings(jo);

                    // if no error, load to right place
                    Program.Settings.LoadFromJson(jo);
                    if (Program.Connected && (sett.Current.Host != originalHost || sett.Current.Port != originalPort))
                    {
                        Program.Connected = false;
                        Connect();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, OtherStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                };
        }

        private void torrentListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void torrentListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        Pen LightLightGray = new Pen(Color.FromArgb(-1447447));
        private void DrawSubItem(DrawListViewSubItemEventArgs e, decimal Width, bool Focused)
        {
            Rectangle rect, origrect = e.Bounds;
            if (Focused)
            {
                // Draw the background and focus rectangle for a selected item.
                e.Graphics.FillRectangle(SystemBrushes.Highlight, origrect);
            }
            else
            {
                // Draw the background for an unselected item.
                e.Graphics.FillRectangle(new SolidBrush(e.Item.BackColor), origrect);
            }
            origrect.X += 1;
            origrect.Y += 1;
            origrect.Height -= 3;
            origrect.Width -= 3;
            rect = origrect;
            e.Graphics.FillRectangle(new SolidBrush(e.Item.BackColor), rect);
            rect.Width = (int)((double)Width / 100.0 * origrect.Width);

            if (rect.Width > 0 && rect.Height > 0)
            {
                Brush br;
                if (Program.Settings.NoGradientTorrentList)
                    br = new SolidBrush(Color.LimeGreen);
                else
                    br = new LinearGradientBrush(rect,
                        Color.ForestGreen,
                        Color.LightGreen,
                        LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(br, rect);
            }
            e.Graphics.DrawRectangle(LightLightGray, origrect);

            e.DrawText(TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }

        private void torrentListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex != 3)
                e.DrawDefault = true;
            else
            {
                decimal width = (decimal)e.Item.SubItems[3].Tag;
                DrawSubItem(e, width, ((e.ItemState & ListViewItemStates.Focused) != 0) && (torrentListView.SelectedItems.Count > 0));
            }
        }

        private void filesListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex != 4)
                e.DrawDefault = true;
            else
            {
                decimal width = (decimal)e.Item.SubItems[4].Tag;
                DrawSubItem(e, width, ((e.ItemState & ListViewItemStates.Focused) != 0) && (filesListView.SelectedItems.Count > 0));
            }
        }

        private void peersListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex != 5)
                e.DrawDefault = true;
            else
            {
                decimal width = (decimal)e.Item.SubItems[5].Tag;
                DrawSubItem(e, width, ((e.ItemState & ListViewItemStates.Focused) != 0) && (peersListView.SelectedItems.Count > 0));
            }
        }

        public string AddTorrentString
        {
            get { return addTorrentButton.Text; }
        }
    }
}
