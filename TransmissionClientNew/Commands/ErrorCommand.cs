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
using System.Text;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet.Commmands
{
    public class ErrorCommand : ICommand
    {
        private static bool dialogLock = false;
        private const int MAX_MESSAGE_DIALOG_LENGTH = 500;
        private const int MAX_MESSAGE_STATUSBAR_LENGTH = 120;

        private string title;
        private string body;
        private bool showDontCount;

        public ErrorCommand(string title, string body, bool showDontCount)
        {
            this.title = title;
            this.body = body;
            this.showDontCount = showDontCount;
        }

        public ErrorCommand(Exception ex, bool showDontCount)
        {
            this.title = OtherStrings.Error;
            this.body = ex.Message;
            this.showDontCount = showDontCount;
        }

        private void ShowErrorBox(string title, string body)
        {
            if (!dialogLock)
            {
                dialogLock = true;
                MessageBox.Show(TrimText(body, MAX_MESSAGE_DIALOG_LENGTH), title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                dialogLock = false;
            }
        }

        private delegate void ExecuteDelegate();
        public void Execute()
        {
            MainWindow form = Program.Form;
            if (Program.Form.InvokeRequired)
            {
                form.Invoke(new ExecuteDelegate(this.Execute));
            }
            else
            {
                Program.UploadArgs = null;
                if (!Program.Connected)
                {
                    form.toolStripStatusLabel.Text = this.StatusBarMessage;
                    Program.Connected = false;
                    ShowErrorBox(this.title, this.body);
                }
                else if (showDontCount)
                {
                    ShowErrorBox(this.title, this.body);
                }
                else if (++Program.DaemonDescriptor.FailCount > LocalSettingsSingleton.Instance.RetryLimit && LocalSettingsSingleton.Instance.RetryLimit >= 0)
                {
                    Program.Connected = false;
                    form.toolStripStatusLabel.Text = OtherStrings.DisconnectedExceeded;
                    ShowErrorBox(this.title, this.body);
                }
                else
                {
                    form.toolStripStatusLabel.Text = String.Format("{0} #{1}: {2}", OtherStrings.FailedRequest, Program.DaemonDescriptor.FailCount, this.StatusBarMessage);
                }
                Program.Log(this.title, this.body);
            }
        }

        private string StatusBarMessage
        {
            get
            {
                return !this.title.Equals(OtherStrings.Error) ? this.title : TrimText(this.body, MAX_MESSAGE_STATUSBAR_LENGTH);
            }
        }

        private string TrimText(string s, int len)
        {
            return s.Length < len ? s : s.Substring(0, len) + "...";
        }
    }
}
