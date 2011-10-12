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
using System.IO;
using System.Net;
using TransmissionRemoteDotnet.Commmands;

namespace TransmissionRemoteDotnet
{
    public partial class UriPromptWindow : CultureForm
    {
        private Uri currentUri;

        public UriPromptWindow()
        {
            InitializeComponent();
            addOurCookiesCheckBox.Visible = Program.DaemonDescriptor.RpcVersion >= 13;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool withoption = useTorrentLoadDialogCheckBox.Enabled ? useTorrentLoadDialogCheckBox.Checked : false;
            if (Program.DaemonDescriptor.Version >= 1.50 && !withoption)
            {
                Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.TorrentAddByUrl(urlTextBox.Text, addOurCookiesCheckBox.Checked)));
                this.Close();
            }
            else
            {
                try
                {
                    string target = Path.GetTempFileName();
                    toolStripStatusLabel.Text = OtherStrings.Downloading + "...";
                    downloadProgressBar.Value = 0;
                    downloadProgressBar.Visible = true;
                    okDialogButton.Enabled = false;
                    WebClient webClient = new TransmissionWebClient(false, false);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
                    webClient.DownloadFileAsync(this.currentUri, target, target);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        private void HandleException(Exception ex)
        {
            downloadProgressBar.Visible = false;
            toolStripStatusLabel.Text = ex.Message;
            MessageBox.Show(ex.Message, OtherStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (urlTextBox.Text.Length > 0)
            {
                try
                {
                    this.currentUri = new Uri(urlTextBox.Text);
                    useTorrentLoadDialogCheckBox.Enabled = !currentUri.Scheme.Equals("magnet");
                    toolStripStatusLabel.Text = OtherStrings.InputAccepted;
                    okDialogButton.Enabled = true;
                }
                catch (Exception ex)
                {
                    okDialogButton.Enabled = false;
                    toolStripStatusLabel.Text = ex.Message;
                }
            }
            else
            {
                toolStripStatusLabel.Text = OtherStrings.WaitingForInput + "...";
                okDialogButton.Enabled = false;
            }
        }

        private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                HandleException(e.Error);
                okDialogButton.Enabled = true;
            }
            else
            {
                bool withoption = useTorrentLoadDialogCheckBox.Enabled ? useTorrentLoadDialogCheckBox.Checked : false;
                if (withoption)
                {
                    TorrentLoadDialog dialog = new TorrentLoadDialog((string)e.UserState);
                    dialog.ShowDialog();
                }
                else
                    Program.Form.SetupAction(CommandFactory.RequestAsync(Requests.TorrentAddByFile((string)e.UserState, true)));
                this.Close();
            }
        }

        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadProgressBar.Value = e.ProgressPercentage;
            toolStripStatusLabel.Text = String.Format("{0} ({1}%)...", OtherStrings.Downloading, e.ProgressPercentage);
        }

        private void UriPromptWindow_Load(object sender, EventArgs e)
        {
            try
            {
                Uri uri = new Uri(Clipboard.GetText());
                urlTextBox.Text = uri.ToString();
            }
            catch
            { }
        }

        private void UseTorrentLoadDialogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            addOurCookiesCheckBox.Enabled = !useTorrentLoadDialogCheckBox.Checked && !(new Uri(urlTextBox.Text)).Scheme.Equals("magnet");
        }
    }
}
