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
using Jayrock.Json;
using System.Windows.Forms;

namespace TransmissionRemoteDotnet.Commmands
{
    public class TorrentGetCommand : ICommand
    {
        private JsonObject response;

        public TorrentGetCommand(JsonObject response)
        {
            this.response = response;
            Program.DaemonDescriptor.ResetFailCount();
        }

        private delegate void ExecuteDelegate();
        public void Execute()
        {
            MainWindow form = Program.Form;
            if (form.InvokeRequired)
            {
                form.Invoke(new ExecuteDelegate(this.Execute));
            }
            else
            {
                if (!Program.Connected)
                {
                    return;
                }
                long totalUpload = 0;
                long totalDownload = 0;
                int totalTorrents = 0;
                int totalSeeding = 0;
                int totalDownloading = 0;
                long totalSize = 0;
                long totalDownloadedSize = 0;
                JsonObject arguments = (JsonObject)response[ProtocolConstants.KEY_ARGUMENTS];
                JsonArray torrents = (JsonArray)arguments[ProtocolConstants.KEY_TORRENTS];
                Program.DaemonDescriptor.UpdateSerial++;
                form.SuspendTorrentListView();
                bool stateChange = false;
                int oldCount = Program.Form.torrentListView.Items.Count;
                foreach (JsonObject torrent in torrents)
                {
                    string hash = (string)torrent[ProtocolConstants.FIELD_HASHSTRING];
                    totalUpload += Toolbox.ToLong(torrent[ProtocolConstants.FIELD_RATEUPLOAD]);
                    totalDownload += Toolbox.ToLong(torrent[ProtocolConstants.FIELD_RATEDOWNLOAD]);
                    totalSize += Toolbox.ToLong(torrent[ProtocolConstants.FIELD_SIZEWHENDONE]);
                    totalDownloadedSize += Toolbox.ToLong(torrent[ProtocolConstants.FIELD_HAVEVALID]);
                    totalTorrents++;
                    short status = Toolbox.ToShort(torrent[ProtocolConstants.FIELD_STATUS]);
                    if (status == ProtocolConstants.STATUS_DOWNLOADING)
                    {
                        totalDownloading++;
                    }
                    else if (status == ProtocolConstants.STATUS_SEEDING)
                    {
                        totalSeeding++;
                    }
                    if (!Program.Form.torrentListView.Items.ContainsKey(hash))
                    {
                        Torrent t = new Torrent(torrent);
                        Program.Form.torrentListView.Items.Add(t);
                    }
                    else
                    {
                        Torrent t = (Torrent)Program.Form.torrentListView.Items[hash];
                        if (t.Update(torrent))
                            stateChange = true;
                    }
                }
                form.ResumeTorrentListView();
                form.UpdateGraph((int)totalDownload, (int)totalUpload);
                form.UpdateStatus(String.Format(
                    "{0} {1}, {2} {3} | {4} {5}: {6} {7}, {8} {9} | {10} / {11}",
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
                        Toolbox.GetFileSize(totalSize)
                    }
                ));
                Queue<Torrent> removeQueue = null;
                foreach (Torrent t in Program.Form.torrentListView.Items)
                {
                    if (t.UpdateSerial != Program.DaemonDescriptor.UpdateSerial)
                    {
                        if (removeQueue == null)
                        {
                            removeQueue = new Queue<Torrent>();
                        }
                        removeQueue.Enqueue(t);
                    }
                }
                if (removeQueue != null)
                {
                    foreach (Torrent t in removeQueue)
                    {
                        t.RemoveItem();
                    }
                }
                if (oldCount != Program.Form.torrentListView.Items.Count)
                    stateChange = true;

                if (stateChange)
                    form.SetAllStateCounters();
                Program.RaisePostUpdateEvent();
            }
        }
    }
}
