using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet
{
    public partial class FindDialog : CultureForm
    {
        ListView torrentlistview;

        public ListView Torrentlistview
        {
            set { torrentlistview = value; }
        }

        public FindDialog()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.WParam.ToInt32() == (int)Keys.Escape)
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool CompareString(string s1, string s2)
        {
            return s1.Contains(s2) || (s1.ToLower().Contains(s2) && !CaseSensitiveCheckBox.Checked);
        }

        private ListViewItem Search()
        {
            int act = torrentlistview.SelectedIndices.Count > 0 ? torrentlistview.SelectedIndices[0] : -1;
            string what = findTextbox.Text.ToLower();
            for (int i = act + 1; i < torrentlistview.Items.Count; i++)
            {
                if (CompareString(torrentlistview.Items[i].Text, what))
                    return torrentlistview.Items[i];
            }
            for (int i = 0; i <= act; i++)
            {
                if (CompareString(torrentlistview.Items[i].Text, what))
                    return torrentlistview.Items[i];
            }
            return null;
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            ListViewItem f = Search();
            if (f != null)
            {
                f.Selected = true;
                f.Focused = true;
            }
            else
            {
                foreach (ListViewItem lvi in torrentlistview.SelectedItems)
                {
                    lvi.Selected = false;
                }
            }
            torrentlistview.Invalidate();
            torrentlistview.Focus();
        }
    }
}
