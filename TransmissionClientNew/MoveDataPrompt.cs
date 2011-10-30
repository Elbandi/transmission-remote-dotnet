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
                Torrent t = (Torrent)selections[0];
                this.Text = String.Format(OtherStrings.MoveX, t.Text);
            }
            else
            {
                this.Text = OtherStrings.MoveMultipleTorrents;
            }
            foreach (string s in Program.Settings.Current.DestPathHistory)
            {
                destinationComboBox.Items.Add(s);
            }
            if (destinationComboBox.Items.Count > 0)
                destinationComboBox.SelectedIndex = 0;
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            Program.Settings.Current.AddDestinationPath(destinationComboBox.Text);
            Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.TorrentSetLocation(Toolbox.ListViewSelectionToIdArray(selections), destinationComboBox.Text, true)));
            this.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ValidateInput()
        {
            moveButton.Enabled = destinationComboBox.Text.IndexOf('/') >= 0;
        }

        private void destinationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }
    }
}
