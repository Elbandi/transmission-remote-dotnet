using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
#if !MONO
using Etier.IconHelper;
#endif
using Jayrock.Json;
using TransmissionRemoteDotnet.Comparers;

namespace TransmissionRemoteDotnet
{
    public partial class TorrentLoadDialog : CultureForm
    {
        private string path;
        private MonoTorrent.Common.Torrent torrent;
        private TorrentFilesListViewItemSorter filesLvwColumnSorter;
        private ContextMenu torrentSelectionMenu, noTorrentSelectionMenu;

        private void SelectAllHandler(object sender, EventArgs e)
        {
            Toolbox.SelectAll(filesListView);
        }

        private void SelectNoneHandler(object sender, EventArgs e)
        {
            Toolbox.SelectNone(filesListView);
        }

        private void SelectInvertHandler(object sender, EventArgs e)
        {
            Toolbox.SelectInvert(filesListView);
        }

        private void HighPriorityHandler(object sender, EventArgs e)
        {
            foreach (ListViewItem item in filesListView.SelectedItems)
            {
                item.SubItems[3].Text = OtherStrings.High;
            }
        }

        private void LowPriorityHandler(object sender, EventArgs e)
        {
            foreach (ListViewItem item in filesListView.SelectedItems)
            {
                item.SubItems[3].Text = OtherStrings.Low;
            }
        }

        private void NormalPriorityHandler(object sender, EventArgs e)
        {
            foreach (ListViewItem item in filesListView.SelectedItems)
            {
                item.SubItems[3].Text = OtherStrings.Normal;
            }
        }

        private void DownloadHandler(object sender, EventArgs e)
        {
            foreach (ListViewItem item in filesListView.SelectedItems)
            {
                item.Checked = true;
            }
        }

        private void SkipHandler(object sender, EventArgs e)
        {
            foreach (ListViewItem item in filesListView.SelectedItems)
            {
                item.Checked = false;
            }
        }

        public TorrentLoadDialog(string path)
        {
            InitializeComponent();
#if !MONO
            filesListView.SmallImageList = new ImageList();
#endif
            this.noTorrentSelectionMenu = new ContextMenu();
            noTorrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.SelectAll, new EventHandler(this.SelectAllHandler)));
            this.torrentSelectionMenu = this.filesListView.ContextMenu = new ContextMenu();
            torrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.Download, new EventHandler(this.DownloadHandler)));
            torrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.Skip, new EventHandler(this.SkipHandler)));
            torrentSelectionMenu.MenuItems.Add(new MenuItem("-"));
            torrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.HighPriority, new EventHandler(this.HighPriorityHandler)));
            torrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.NormalPriority, new EventHandler(this.NormalPriorityHandler)));
            torrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.LowPriority, new EventHandler(this.LowPriorityHandler)));
            torrentSelectionMenu.MenuItems.Add(new MenuItem("-"));
            torrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.SelectAll, new EventHandler(this.SelectAllHandler)));
            filesListView.ListViewItemSorter = filesLvwColumnSorter = new TorrentFilesListViewItemSorter();
            this.path = path;
            this.toolStripStatusLabel.Text = this.Text = String.Format(OtherStrings.LoadingFile, path);
            startTorrentCheckBox.Checked = !Program.Settings.Current.StartPaused;
            foreach (string s in Program.Settings.Current.DestPathHistory)
            {
                destinationComboBox.Items.Add(s);
            }
            JsonObject session = (JsonObject)Program.DaemonDescriptor.SessionData;
            string ddir = (string)session[ProtocolConstants.DOWNLOAD_DIR];
            if (!destinationComboBox.Items.Contains(ddir))
                destinationComboBox.Items.Insert(0, ddir);
            if (destinationComboBox.Items.Count > 0)
                destinationComboBox.SelectedIndex = 0;
        }

        private void TorrentLoadDialog_Load(object sender, EventArgs e)
        {
            TorrentLoadBackgroundWorker.RunWorkerAsync();
            this.OkButton.Select();
        }

        private void TorrentLoadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<ListViewItem> items = new List<ListViewItem>();
                torrent = MonoTorrent.Common.Torrent.Load(new FileStream(path, FileMode.Open, FileAccess.Read));
                foreach (MonoTorrent.Common.TorrentFile file in torrent.Files)
                {
                    ListViewItem item = new ListViewItem(file.Path);
#if !MONO
                    string[] split = file.Path.Split('.');
                    if (split.Length > 1)
                    {
                        string extension = split[split.Length - 1].ToLower();
                        if (filesListView.SmallImageList.Images.ContainsKey(extension) || IconReader.AddToImgList(extension, filesListView.SmallImageList))
                        {
                            item.ImageKey = extension;
                            item.SubItems.Add(IconReader.GetTypeName(extension));
                        }
                        else
                        {
                            item.SubItems.Add("");
                        }
                    }
                    else
                    {
                        item.SubItems.Add("");
                    }
#else
                    item.SubItems.Add("");
#endif
                    item.SubItems.Add(Toolbox.GetFileSize(file.Length)).Tag = file.Length;
                    item.SubItems.Add(OtherStrings.Normal);
                    item.Checked = true;
                    items.Add(item);
                }
                e.Result = items;
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        private void TorrentLoadBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result.GetType().Equals(typeof(List<ListViewItem>)))
            {
                filesListView.BeginUpdate();
                foreach (ListViewItem item in (List<ListViewItem>)e.Result)
                {
                    filesListView.Items.Add(item);
                }
                Toolbox.StripeListView(filesListView);
                filesListView.Enabled = OkButton.Enabled = altDestDirCheckBox.Enabled = altPeerLimitCheckBox.Enabled = startTorrentCheckBox.Enabled = true;
                filesListView.EndUpdate();
                NameLabel.Text = torrent.Name;
                CommentLabel.Text = torrent.Comment;
                SizeLabel.Text = string.Format("{0} ({1} x {2})", Toolbox.GetFileSize(torrent.Size), torrent.Pieces.Count, Toolbox.GetFileSize(torrent.PieceLength));
                DateLabel.Text = torrent.CreationDate.ToString();
                this.Text = torrent.Name;
                this.toolStripStatusLabel.Text = "";
            }
            else
            {
                Exception ex = (Exception)e.Result;
                MessageBox.Show(ex.Message, OtherStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void altDestDirCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            destinationComboBox.Enabled = altDestDirCheckBox.Checked;
        }

        private void peerLimitValue_ValueChanged(object sender, EventArgs e)
        {
            peerLimitValue.Enabled = altPeerLimitCheckBox.Checked;
        }

        private void TorrentLoadDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
                Toolbox.SelectAll(filesListView);
        }

        private void filesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            filesListView.ContextMenu = filesListView.SelectedItems.Count > 0 ? torrentSelectionMenu : noTorrentSelectionMenu;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JsonArray wanted = new JsonArray();
            JsonArray unwanted = new JsonArray();
            JsonArray high = new JsonArray();
            JsonArray normal = new JsonArray();
            JsonArray low = new JsonArray();
            foreach (ListViewItem item in filesListView.Items)
            {
                if (!item.Checked)
                {
                    unwanted.Add(item.Index);
                }
                else
                {
                    wanted.Add(item.Index);
                }
                if (item.SubItems[3].Text.Equals(OtherStrings.High))
                {
                    high.Add(item.Index);
                }
                else if (item.SubItems[3].Text.Equals(OtherStrings.Low))
                {
                    low.Add(item.Index);
                }
                else
                {
                    normal.Add(item.Index);
                }
            }
            JsonObject request = Requests.TorrentAddByFile(
                path,
                Program.Settings.DeleteTorrentWhenAdding,
                high.Count > 0 ? high : null,
                normal.Count > 0 ? normal : null,
                low.Count > 0 ? low : null,
                wanted.Count > 0 ? wanted : null,
                unwanted.Count > 0 ? unwanted : null,
                altDestDirCheckBox.Checked ? destinationComboBox.Text : null,
                altPeerLimitCheckBox.Checked ? (int)peerLimitValue.Value : -1,
                startTorrentCheckBox.Checked
            );
            Program.Settings.Current.AddDestinationPath(destinationComboBox.Text);
            Program.Form.SetupAction(CommandFactory.RequestAsync(request));
            this.Close();
        }

        private void altPeerLimitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            peerLimitValue.Enabled = altPeerLimitCheckBox.Checked;
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
    }
}
