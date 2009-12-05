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
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.DaemonDescriptor.Revision >= 7744)
            {
                Program.Form.CreateActionWorker().RunWorkerAsync(Requests.TorrentAddByUrl(textBox1.Text));
                this.Close();
            }
            else
            {
                try
                {
                    string target = Path.GetTempFileName();
                    toolStripStatusLabel1.Text = OtherStrings.Downloading+"...";
                    toolStripProgressBar1.Value = 0;
                    toolStripProgressBar1.Visible = true;
                    button1.Enabled = false;
                    WebClient webClient = new TransmissionWebClient(false);
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
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Text = ex.Message;
            MessageBox.Show(ex.Message, OtherStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                try
                {
                    this.currentUri = new Uri(textBox1.Text);
                    toolStripStatusLabel1.Text = OtherStrings.InputAccepted;
                    button1.Enabled = true;
                }
                catch (Exception ex)
                {
                    button1.Enabled = false;
                    toolStripStatusLabel1.Text = ex.Message;
                }
            }
            else
            {
                toolStripStatusLabel1.Text = OtherStrings.WaitingForInput+"...";
                button1.Enabled = false;
            }
        }

        private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                HandleException(e.Error);
                button1.Enabled = true;
            }
            else
            {
                Program.Form.CreateActionWorker().RunWorkerAsync(Requests.TorrentAddByFile((string)e.UserState, true));
                this.Close();
            }
        }

        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            toolStripStatusLabel1.Text = String.Format("{0} ({1}%)...", OtherStrings.Downloading, e.ProgressPercentage);
        }

        private void UriPromptWindow_Load(object sender, EventArgs e)
        {
            try
            {
                Uri uri = new Uri(Clipboard.GetText());
                textBox1.Text = uri.ToString();
            }
            catch
            { }
        }
    }
}
