using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet
{
    public partial class MoveDataPrompt : CultureForm
    {
        private ListView.SelectedListViewItemCollection selections;

        public MoveDataPrompt(ListView.SelectedListViewItemCollection selections)
        {
            InitializeComponent();
            this.selections = selections;
            if (selections.Count < 1)
            {
                this.Close();
            }
            else if (selections.Count == 1)
            {
                Torrent t = (Torrent)selections[0].Tag;
                this.Text = String.Format(OtherStrings.MoveX, t.Name);
            }
            else
            {
                this.Text = OtherStrings.MoveMultipleTorrents;
            }
            foreach (string s in LocalSettingsSingleton.Instance.DestPathHistory)
            {
                comboBox1.Items.Add(s);
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LocalSettingsSingleton.Instance.AddDestinationPath(comboBox1.Text);
            Program.Form.CreateActionWorker().RunWorkerAsync(Requests.TorrentSetLocation(Toolbox.ListViewSelectionToIdArray(selections), comboBox1.Text, true));
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ValidateInput()
        {
            button1.Enabled = comboBox1.Text.IndexOf('/') >= 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }
    }
}
