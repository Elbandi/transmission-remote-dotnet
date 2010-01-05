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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using TransmissionRemoteDotnet.Commmands;
using TransmissionRemoteDotnet.Comparers;
using TransmissionRemoteDotnet.Settings;
using Jayrock.Json;
using MaxMind;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
using System.Globalization;
using System.Threading;
using Jayrock.Json.Conversion;
using System.Collections;

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
            CONFKEY_MAINWINDOW_DETAILSPANEL_COLLAPSED = "mainwindow-detailspanel-collapsed",
            PROJECT_SITE = "http://code.google.com/p/transmission-remote-dotnet/",
            LATEST_VERSION = "http://transmission-remote-dotnet.googlecode.com/svn/wiki/latest_version.txt",
            DOWNLOADS_PAGE = "http://code.google.com/p/transmission-remote-dotnet/downloads/list";

        private Boolean minimise = false;
        private ListViewItemSorter lvwColumnSorter;
        private FilesListViewItemSorter filesLvwColumnSorter;
        private PeersListViewItemSorter peersLvwColumnSorter;
        private ContextMenu torrentSelectionMenu;
        private ContextMenu noTorrentSelectionMenu;
        private ContextMenu fileSelectionMenu;
        private ContextMenu noFileSelectionMenu;
        private MenuItem openNetworkShareMenuItem;
        private WebClient sessionWebClient;
        private WebClient refreshWebClient = new WebClient();
        private WebClient filesWebClient = new WebClient();
        private static FindDialog FindDialog;

        public MainWindow()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(Program.Settings.Locale);
            }
            catch { }
            Program.OnConnStatusChanged += new EventHandler(Program_connStatusChanged);
            Program.OnTorrentsUpdated += new EventHandler(Program_onTorrentsUpdated);
            InitializeComponent();
            CreateTrayContextMenu();
            tabControlImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources.folder16);
            filesTabPage.ImageIndex = 0;
            tabControlImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources.peer16);
            peersTabPage.ImageIndex = 1;
            tabControlImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources.server16);
            trackersTabPage.ImageIndex = 2;
            tabControlImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources.pipe16);
            speedTabPage.ImageIndex = 3;
            tabControlImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources.info16);
            generalTabPage.ImageIndex = 4;
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
            peersListView.SmallImageList = GeoIPCountry.FlagImageList;
            PopulateLanguagesMenu();
            OneTorrentsSelected(false, null);
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
            stateListBox.SuspendLayout();
            ImageList stateListBoxImageList = new ImageList();
            stateListBoxImageList.ColorDepth = ColorDepth.Depth32Bit;
            stateListBoxImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources._16x16_ledpurple);
            stateListBoxImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources.down16);
            stateListBoxImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources.pause16);
            stateListBoxImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources.apply16);
            stateListBoxImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources.up16);
            stateListBoxImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources.player_reload16);
            stateListBoxImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources.warning);
            stateListBoxImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources.incomplete);
            stateListBoxImageList.Images.Add(global::TransmissionRemoteDotnet.Properties.Resources.server16);
            torrentListView.SmallImageList = stateListBox.ImageList = stateListBoxImageList;
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.All, 0));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Downloading, 1));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Paused, 2));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Checking, 5));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Complete, 3));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Incomplete, 7));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Seeding, 4));
            stateListBox.Items.Add(new GListBoxItem(OtherStrings.Broken, 6));
            stateListBox.Items.Add(new GListBoxItem(""));
            stateListBox.ResumeLayout();
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
            this.torrentSelectionMenu.MenuItems.Add(openNetworkShareMenuItem = new MenuItem(OtherStrings.OpenNetworkShare, this.openNetworkShareButton_Click));
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
                if (categoriesPanelToolStripMenuItem.Checked)
                    mainVerticalSplitContainer.Panel1Collapsed = false;
                FilterByStateOrTracker();
                torrentListView.Sort();
                Toolbox.StripeListView(torrentListView);
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
                CreateTorrentSelectionContextMenu();
                this.toolStripStatusLabel.Text = OtherStrings.ConnectedGettingInfo;
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
            }
            connectButton.Visible = connectToolStripMenuItem.Enabled
                = mainVerticalSplitContainer.Panel1Collapsed = !connected;
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
                = categoriesPanelToolStripMenuItem.Checked = connected;
            SetRemoteCmdButtonVisible(connected);
            TransmissionDaemonDescriptor dd = Program.DaemonDescriptor;
            reannounceButton.Visible = connected && dd.RpcVersion >= 5;
            removeAndDeleteButton.Visible = connected && dd.Version >= 1.5;
            statsToolStripMenuItem.Enabled = sessionStatsButton.Visible = connected && dd.RpcVersion >= 4;
            AltSpeedButton.Visible = toolbarToolStripSeparator4.Visible = connected && dd.RpcVersion >= 5;
            addTorrentWithOptionsToolStripMenuItem.Enabled = (dd.Version < 1.60 || dd.Version >= 1.7) && connected;
        }

        public void SetAltSpeedButtonState(bool enabled)
        {
            AltSpeedButton.Image = enabled ? global::TransmissionRemoteDotnet.Properties.Resources.altspeed_on : global::TransmissionRemoteDotnet.Properties.Resources.altspeed_off;
            AltSpeedButton.Tag = enabled;
        }

        public void SetRemoteCmdButtonVisible(bool connected)
        {
            LocalSettings settings = Program.Settings;
            remoteCmdButton.Visible = connected && settings.Current.PlinkEnable && settings.Current.PlinkCmd != null && settings.PlinkPath != null && File.Exists(settings.PlinkPath);
            //openNetworkShareToolStripMenuItem.Visible = openNetworkShareButton.Visible = connected && settings.SambaShareEnabled && settings.SambaShare != null && settings.SambaShare.Length > 5;
            openNetworkShareButton.Visible = openNetworkShareToolStripMenuItem.Enabled = connected && settings.Current.SambaShareMappings.Count > 0;
            if (openNetworkShareMenuItem != null)
                openNetworkShareMenuItem.Visible = openNetworkShareButton.Visible;
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

        public void startAllMenuItem_Click(object sender, EventArgs e)
        {
            Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.Generic(ProtocolConstants.METHOD_TORRENTSTART, null)));
        }

        public void stopAllMenuItem_Click(object sender, EventArgs e)
        {
            Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.Generic(ProtocolConstants.METHOD_TORRENTSTOP, null)));
        }

        public void SuspendTorrentListView()
        {
            torrentListView.SuspendLayout();
        }

        public void ResumeTorrentListView()
        {
            torrentListView.ResumeLayout();
        }

        public void RestoreFormProperties()
        {
            try
            {
                LocalSettings settings = Program.Settings;
                if (settings.Misc.ContainsKey(CONFKEY_MAINWINDOW_HEIGHT) && settings.Misc.ContainsKey(CONFKEY_MAINWINDOW_WIDTH))
                    this.Size = new Size((int)settings.Misc[CONFKEY_MAINWINDOW_WIDTH], (int)settings.Misc[CONFKEY_MAINWINDOW_HEIGHT]);
                if (settings.Misc.ContainsKey(CONFKEY_MAINWINDOW_LOCATION_X) && settings.Misc.ContainsKey(CONFKEY_MAINWINDOW_LOCATION_Y))
                    this.Location = new Point((int)settings.GetObject(CONFKEY_MAINWINDOW_LOCATION_X), (int)settings.GetObject(CONFKEY_MAINWINDOW_LOCATION_Y));
                if (settings.Misc.ContainsKey(CONFKEY_SPLITTERDISTANCE))
                    this.torrentAndTabsSplitContainer.SplitterDistance = (int)settings.GetObject(CONFKEY_SPLITTERDISTANCE);
                this.showDetailsPanelToolStripMenuItem.Checked = !(this.torrentAndTabsSplitContainer.Panel2Collapsed = !settings.Misc.ContainsKey(CONFKEY_MAINWINDOW_DETAILSPANEL_COLLAPSED) || (int)settings.GetObject(CONFKEY_MAINWINDOW_DETAILSPANEL_COLLAPSED) == 1);
                if (settings.Misc.ContainsKey(CONFKEY_MAINWINDOW_STATE))
                {
                    FormWindowState _mainWindowState = (FormWindowState)((int)settings.GetObject(CONFKEY_MAINWINDOW_STATE));
                    if (_mainWindowState != FormWindowState.Minimized)
                    {
                        this.WindowState = _mainWindowState;
                    }
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
            IListViewItemSorter listViewItemSorter = (IListViewItemSorter)listView.ListViewItemSorter;
            settings.SetObject(CONFKEYPREFIX_LISTVIEW_SORTINDEX + listView.Name, listViewItemSorter.Order == SortOrder.Descending ? -listViewItemSorter.SortColumn : listViewItemSorter.SortColumn);
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
                int sortIndex = (int)settings.GetObject(sortIndexConfKey);
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
            if (!settings.AutoConnect.Equals(""))
            {
                Connect();
            }
            else
            {
                if (Program.UploadArgs != null && Program.UploadArgs.Length > 0)
                {
                    ShowMustBeConnectedDialog(Program.UploadArgs);
                }
            }
        }

        private void PopulateLanguagesMenu()
        {
            ToolStripMenuItem englishItem = new ToolStripMenuItem("English");
            englishItem.Click += new EventHandler(this.ChangeUICulture);
            englishItem.Tag = new CultureInfo("en-GB");
            englishItem.Checked = Program.Settings.Locale.Equals("en-GB");
            languageToolStripMenuItem.DropDownItems.Add(englishItem);
            languageToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            foreach (DirectoryInfo subDir in di.GetDirectories())
            {
                string dn = subDir.Name;
                if (dn.IndexOf('-') == 2 && dn.Length == 5)
                {
                    try
                    {
                        CultureInfo cInfo = new CultureInfo(dn.Substring(0, 2).ToLower() + "-" + dn.Substring(3, 2).ToUpper());
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
                Upload((string[])e.Data.GetData(DataFormats.FileDrop));
            }
            else
            {
                ShowMustBeConnectedDialog((string[])e.Data.GetData(DataFormats.FileDrop));
            }
        }

        public void ShowMustBeConnectedDialog(string[] args)
        {
            if (MessageBox.Show(OtherStrings.MustBeConnected, OtherStrings.NotConnected, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Program.UploadArgs = args;
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
                if (Program.Connected)
                    Program.Connected = false;
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
            if ((new LocalSettingsDialog()).ShowDialog() == DialogResult.OK)
            {
                connectButton.DropDownItems.Clear();
                connectToolStripMenuItem.DropDownItems.Clear();
                CreateProfileMenu();
                refreshTimer.Interval = Program.Settings.Current.RefreshRate * 1000;
                filesTimer.Interval = Program.Settings.Current.RefreshRate * 1000 * LocalSettingsSingleton.FILES_REFRESH_MULTIPLICANT;
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
                = moveTorrentDataToolStripMenuItem.Enabled = openNetworkShareToolStripMenuItem.Enabled
                = cSVInfoToClipboardToolStripMenuItem.Enabled = oneOrMore;
            moveTorrentDataToolStripMenuItem.Enabled = oneOrMore && Program.DaemonDescriptor.Revision >= 8385;
            pauseTorrentButton.Image = oneOrMore && torrentListView.SelectedItems.Count != torrentListView.Items.Count ? global::TransmissionRemoteDotnet.Properties.Resources.player_pause : global::TransmissionRemoteDotnet.Properties.Resources.player_pause_all;
            startTorrentButton.Image = oneOrMore && torrentListView.SelectedItems.Count != torrentListView.Items.Count ? global::TransmissionRemoteDotnet.Properties.Resources.player_play1 : global::TransmissionRemoteDotnet.Properties.Resources.player_play_all;
        }

        public void FillfilesListView(Torrent t)
        {
            lock (filesListView)
            {
                filesListView.SuspendLayout();
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
                filesListView.Sort();
                Toolbox.StripeListView(filesListView);
                filesListView.ResumeLayout();
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
                timeElapsedLabel.Text = downloadedLabel.Text = downloadSpeedLabel.Text
                    = downloadLimitLabel.Text = statusLabel.Text = commentLabel.Text
                    = remainingLabel.Text = uploadedLabel.Text = uploadRateLabel.Text
                    = uploadLimitLabel.Text = startedAtLabel.Text = seedersLabel.Text
                    = leechersLabel.Text = ratioLabel.Text = createdAtLabel.Text
                    = createdByLabel.Text = errorLabel.Text = percentageLabel.Text
                    = hashLabel.Text = piecesInfoLabel.Text = locationLabel.Text
                    = generalTorrentNameGroupBox.Text = "";
                trackersTorrentNameGroupBox.Text
                   = peersTorrentNameGroupBox.Text = filesTorrentNameGroupBox.Text
                   = "N/A";
                progressBar.Value = 0;
                piecesGraph.ClearBits();
                labelForErrorLabel.Visible = errorLabel.Visible
                    = filesListView.Enabled = peersListView.Enabled
                    = trackersListView.Enabled = false;
            }
            generalTorrentNameGroupBox.Enabled
                    = downloadProgressLabel.Enabled = refreshElapsedTimer.Enabled
                    = filesTimer.Enabled = downloadProgressLabel.Enabled
                    = generalTorrentNameGroupBox.Enabled
                    = remoteCmdButton.Enabled
                    = openNetworkShareButton.Enabled = one;
            openNetworkShareButton.Enabled = openNetworkShareToolStripMenuItem.Enabled = one && t.SambaLocation != null;
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

        public void UpdateStatus(string text)
        {
            toolStripStatusLabel.Text = text;
            notifyIcon.Text = text.Length < 64 ? text : text.Substring(0, 63);
        }

        private void addTorrentButton_Click(object sender, EventArgs e)
        {
            if (Program.Connected)
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = OtherStrings.OpenFileFilter;
                openFile.RestoreDirectory = true;
                openFile.Multiselect = true;
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    Upload(openFile.FileNames);
                }
            }
        }

        public void Upload(string[] args)
        {
            foreach (string s in args)
            {
                if (s == null || s.Length == 0)
                    continue;
                if (File.Exists(s))
                {
                    if (Program.Settings.UploadPrompt)
                    {
                        TorrentLoadDialog dialog = new TorrentLoadDialog(s);
                        dialog.ShowDialog();
                    }
                    else
                    {
                        Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.TorrentAddByFile(s, false)));
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
            torrentListView.Sort();
            Toolbox.StripeListView(torrentListView);
        }

        private void FilterByStateOrTracker()
        {
            SuspendTorrentListView();
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
            ResumeTorrentListView();
        }

        private delegate bool FilterCompare(Torrent t, object param);
        private void FilterTorrent(FilterCompare fc, object param)
        {
            lock (Program.TorrentIndex)
            {
                foreach (KeyValuePair<string, Torrent> pair in Program.TorrentIndex)
                {
                    if (fc(pair.Value, param))
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
                generalTorrentNameGroupBox.Text = peersTorrentNameGroupBox.Text
                    = trackersTorrentNameGroupBox.Text = filesTorrentNameGroupBox.Text
                    = t.TorrentName;
                startedAtLabel.Text = t.Added.ToString();
                createdAtLabel.Text = t.Created;
                createdByLabel.Text = t.Creator;
                hashLabel.Text = string.Join(" ", Toolbox.Split(t.Hash.ToUpper(), 8));
                commentLabel.Text = t.Comment;
                trackersListView.SuspendLayout();
                trackersListView.Items.Clear();
                foreach (JsonObject tracker in t.Trackers)
                {
                    int tier = Toolbox.ToInt(tracker[ProtocolConstants.TIER]);
                    string announceUrl = (string)tracker[ProtocolConstants.ANNOUNCE];
                    string scrapeUrl = (string)tracker[ProtocolConstants.SCRAPE];
                    ListViewItem item = new ListViewItem(tier.ToString());
                    item.SubItems.Add(announceUrl);
                    item.SubItems.Add(scrapeUrl);
                    trackersListView.Items.Add(item);
                }
                Toolbox.StripeListView(trackersListView);
                trackersListView.ResumeLayout();
                peersListView.Enabled = trackersListView.Enabled
                    = true;
                downloadProgressLabel.Text = ((piecesGraph.Visible = t.Pieces != null) ? OtherStrings.Pieces : OtherStrings.Progress) + ": ";
                progressBar.Visible = !piecesGraph.Visible;
            }
            remainingLabel.Text = t.IsFinished ? (t.DoneDate != null ? t.DoneDate.ToString() : "?") : t.LongEta;
            label4.Text = (t.IsFinished ? columnHeader19.Text : columnHeader14.Text) + ":";
            uploadedLabel.Text = t.UploadedString;
            uploadLimitLabel.Text = t.SpeedLimitUpEnabled ? Toolbox.KbpsString(t.SpeedLimitUp) : "∞";
            uploadRateLabel.Text = t.UploadRateString;
            seedersLabel.Text = String.Format(OtherStrings.XOfYConnected, t.PeersSendingToUs, t.Seeders < 0 ? "?" : t.Seeders.ToString());
            leechersLabel.Text = String.Format(OtherStrings.XOfYConnected, t.PeersGettingFromUs, t.Leechers < 0 ? "?" : t.Leechers.ToString());
            ratioLabel.Text = t.LocalRatioString;
            progressBar.Value = (int)t.Percentage;
            if (t.Pieces != null)
            {
                piecesGraph.ApplyBits(t.Pieces, t.PieceCount);
            }
            piecesInfoLabel.Text = String.Format(OtherStrings.PiecesInfo, t.PieceCount, Toolbox.GetFileSize(t.PieceSize), t.HavePieces);
            locationLabel.Text = t.DownloadDir + "/" + t.TorrentName;
            percentageLabel.Text = t.Percentage.ToString() + "%";
            if (t.IsFinished)
            {
                downloadedLabel.Text = t.HaveTotalString;
            }
            else
            {
                downloadedLabel.Text = String.Format(OtherStrings.DownloadedValid, t.HaveTotalString, Toolbox.GetFileSize(t.HaveValid));
            }
            downloadSpeedLabel.Text = t.DownloadRateString;
            downloadLimitLabel.Text = t.SpeedLimitDownEnabled ? Toolbox.KbpsString(t.SpeedLimitDown) : "∞";
            statusLabel.Text = t.Status;
            labelForErrorLabel.Visible = errorLabel.Visible = !(errorLabel.Text = t.ErrorString).Equals("");
            RefreshElapsedTimer();
            if (t.Peers != null)
            {
                peersListView.Enabled = t.StatusCode != ProtocolConstants.STATUS_PAUSED;
                PeerListViewItem.CurrentUpdateSerial++;
                peersListView.SuspendLayout();
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
                lock (peersListView)
                {
                    PeerListViewItem[] peers = (PeerListViewItem[])new ArrayList(peersListView.Items).ToArray(typeof(PeerListViewItem));
                    foreach (PeerListViewItem item in peers)
                    {
                        if (item.UpdateSerial != PeerListViewItem.CurrentUpdateSerial)
                        {
                            peersListView.Items.Remove(item);
                        }
                    }
                }
                peersListView.Sort();
                Toolbox.StripeListView(peersListView);
                peersListView.ResumeLayout();
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
                    TimeSpan ts = DateTime.Now.ToUniversalTime().Subtract(t.Added);
                    timeElapsedLabel.Text = ts.Ticks > 0 ? Toolbox.FormatTimespanLong(ts) : OtherStrings.UnknownNegativeResult;
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
                Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.Generic(ProtocolConstants.METHOD_TORRENTVERIFY, BuildIdArray())));
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

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocalSettings settings = Program.Settings;
            if (settings.MinToTray && settings.MinOnClose && e.CloseReason == CloseReason.UserClosing)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
            else if (this.WindowState != FormWindowState.Minimized)
            {
                settings.Misc[CONFKEY_MAINWINDOW_STATE] = (int)this.WindowState;
                if (this.WindowState != FormWindowState.Maximized)
                {
                    settings.SetObject(CONFKEY_MAINWINDOW_LOCATION_X, this.Location.X);
                    settings.SetObject(CONFKEY_MAINWINDOW_LOCATION_Y, this.Location.Y);
                    settings.SetObject(CONFKEY_SPLITTERDISTANCE, this.torrentAndTabsSplitContainer.SplitterDistance);
                    settings.SetObject(CONFKEY_MAINWINDOW_HEIGHT, this.Size.Height);
                    settings.SetObject(CONFKEY_MAINWINDOW_WIDTH, this.Size.Width);
                }
            }
            SaveListViewProperties(torrentListView);
            SaveListViewProperties(filesListView);
            SaveListViewProperties(peersListView);
            settings.SetObject(CONFKEY_MAINWINDOW_DETAILSPANEL_COLLAPSED, this.torrentAndTabsSplitContainer.Panel2Collapsed ? 1 : 0);
            settings.Commit();
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
            TransmissionWebClient client = new TransmissionWebClient(false);
            string response = client.DownloadString(LATEST_VERSION);
            if (!response.StartsWith("#LATESTVERSION#"))
                throw new FormatException("Response didn't contain the identification prefix.");
            string[] latestVersion = response.Remove(0, 15).Split('.');
            if (latestVersion.Length != 4)
                throw new FormatException("Incorrect number format");
            e.Result = new Version(Int32.Parse(latestVersion[0]), Int32.Parse(latestVersion[1]), Int32.Parse(latestVersion[2]), Int32.Parse(latestVersion[3]));
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
                            "\"{0}\" \"{1}\"",
                            Program.Settings.Current.Host,
                            String.Format(
                                Program.Settings.Current.PlinkCmd.Replace("$DATA", "{0}"),
                                String.Format("{0}{1}{2}", t.DownloadDir, !t.DownloadDir.EndsWith("/") ? "/" : null, t.TorrentName))
                        ));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to run plink", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void openNetworkShareButton_Click(object sender, EventArgs e)
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
                        MessageBox.Show(ex.Message, "Unable to open network share", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void categoriesPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainVerticalSplitContainer.Panel1Collapsed = !mainVerticalSplitContainer.Panel1Collapsed;
            categoriesPanelToolStripMenuItem.Checked = !mainVerticalSplitContainer.Panel1Collapsed;
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
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fileName in openFileDialog1.FileNames)
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
            foreach (ToolStripMenuItem item in connectButton.DropDownItems)
            {
                item.Checked = Program.Settings.CurrentProfile.Equals(item.ToString());
            }
            foreach (ToolStripMenuItem item in connectToolStripMenuItem.DropDownItems)
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
    }
}
