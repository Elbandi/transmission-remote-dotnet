using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Etier.IconHelper;
using Jayrock.Json;

namespace TransmissionRemoteDotnet
{
    public partial class TorrentLoadDialog : CultureForm
    {
        private string path;
        private MonoTorrent.Common.Torrent torrent;
        private ContextMenu torrentSelectionMenu, noTorrentSelectionMenu;

        private void SelectAllHandler(object sender, EventArgs e)
        {
            Toolbox.SelectAll(listView1);
        }

        private void HighPriorityHandler(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                item.SubItems[3].Text = OtherStrings.High;
            }
        }

        private void LowPriorityHandler(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                item.SubItems[3].Text = OtherStrings.Low;
            }
        }

        private void NormalPriorityHandler(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                item.SubItems[3].Text = OtherStrings.Normal;
            }
        }

        private void DownloadHandler(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                item.Checked = true;
            }
        }

        private void SkipHandler(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                item.Checked = false;
            }
        }

        public TorrentLoadDialog(string path)
        {
            InitializeComponent();
#if !MONO
            listView1.SmallImageList = new ImageList();
#endif
            this.noTorrentSelectionMenu = new ContextMenu();
            noTorrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.SelectAll, new EventHandler(this.SelectAllHandler)));
            this.torrentSelectionMenu = this.listView1.ContextMenu = new ContextMenu();
            torrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.Download, new EventHandler(this.DownloadHandler)));
            torrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.Skip, new EventHandler(this.SkipHandler)));
            torrentSelectionMenu.MenuItems.Add(new MenuItem("-"));
            torrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.HighPriority, new EventHandler(this.HighPriorityHandler)));
            torrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.NormalPriority, new EventHandler(this.NormalPriorityHandler)));
            torrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.LowPriority, new EventHandler(this.LowPriorityHandler)));
            torrentSelectionMenu.MenuItems.Add(new MenuItem("-"));
            torrentSelectionMenu.MenuItems.Add(new MenuItem(OtherStrings.SelectAll, new EventHandler(this.SelectAllHandler)));
            this.path = path;
            this.toolStripStatusLabel1.Text = this.Text = String.Format(OtherStrings.LoadingFile, path);
            checkBox3.Checked = !Program.Settings.Current.StartPaused;
            foreach (string s in Program.Settings.Current.DestPathHistory)
            {
                comboBox1.Items.Add(s);
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void TorrentLoadDialog_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
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
                        if (listView1.SmallImageList.Images.ContainsKey(extension) || IconReader.AddToImgList(extension, listView1.SmallImageList))
                        {
                            item.ImageKey = extension;
                            item.SubItems.Add(IconReader.GetTypeName(extension));
                        }
                        else
                        {
                            item.SubItems.Add("");
                        }
                    }
#else
                    item.SubItems.Add("");
#endif
                    item.SubItems.Add(Toolbox.GetFileSize(file.Length));
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result.GetType().Equals(typeof(List<ListViewItem>)))
            {
                listView1.SuspendLayout();
                foreach (ListViewItem item in (List<ListViewItem>)e.Result)
                {
                    listView1.Items.Add(item);
                }
                Toolbox.StripeListView(listView1);
                listView1.Enabled = button1.Enabled = checkBox1.Enabled = checkBox2.Enabled = checkBox3.Enabled = true;
                listView1.ResumeLayout();
                this.Text = torrent.Name;
                this.toolStripStatusLabel1.Text = "";
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = checkBox1.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = checkBox2.Checked;
        }

        private void TorrentLoadDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
                Toolbox.SelectAll(listView1);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.ContextMenu = listView1.SelectedItems.Count > 0 ? torrentSelectionMenu : noTorrentSelectionMenu;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JsonArray wanted = new JsonArray();
            JsonArray unwanted = new JsonArray();
            JsonArray high = new JsonArray();
            JsonArray normal = new JsonArray();
            JsonArray low = new JsonArray();
            foreach (ListViewItem item in listView1.Items)
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
                checkBox1.Checked ? comboBox1.Text : null,
                checkBox2.Checked ? (int)numericUpDown1.Value : -1,
                checkBox3.Checked
            );
            Program.Settings.Current.AddDestinationPath(comboBox1.Text);
            Program.Form.SetupAction(CommandFactory.RequestAsync(request));
            this.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = checkBox2.Checked;
        }
    }
}
