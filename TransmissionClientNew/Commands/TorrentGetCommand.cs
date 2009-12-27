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
using System.Collections;

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
                int oldCount = Program.TorrentIndex.Count;
                IComparer tmp = Program.Form.torrentListView.ListViewItemSorter;
                Program.Form.torrentListView.ListViewItemSorter = null;
                foreach (JsonObject torrent in torrents)
                {
                    string hash = (string)torrent[ProtocolConstants.FIELD_HASHSTRING];
                    totalTorrents++;
                    Torrent t = null;
                    if (!Program.TorrentIndex.ContainsKey(hash))
                    {
                        t = new Torrent(torrent);
                        Program.Form.torrentListView.Items.Add(t);
                    }
                    else
                    {
                        lock (Program.TorrentIndex)
                            t = Program.TorrentIndex[hash];
                        if (t.Update(torrent))
                            stateChange = true;
                    }
                    totalUpload += t.UploadRate;
                    totalDownload += t.DownloadRate;
                    totalSize += t.TotalSize;
                    totalDownloadedSize += t.HaveTotal;
                    if (t.StatusCode == ProtocolConstants.STATUS_DOWNLOADING)
                    {
                        totalDownloading++;
                    }
                    else if (t.StatusCode == ProtocolConstants.STATUS_SEEDING)
                    {
                        totalSeeding++;
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
                Queue<KeyValuePair<string, Torrent>> removeQueue = null;
                foreach (KeyValuePair<string, Torrent> pair in Program.TorrentIndex)
                {
                    if (pair.Value.UpdateSerial != Program.DaemonDescriptor.UpdateSerial)
                    {
                        if (removeQueue == null)
                        {
                            removeQueue = new Queue<KeyValuePair<string, Torrent>>();
                        }
                        removeQueue.Enqueue(pair);
                    }
                }
                if (removeQueue != null)
                {
                    foreach (KeyValuePair<string, Torrent> t in removeQueue)
                    {
                        Program.TorrentIndex.Remove(t.Key);
                        t.Value.RemoveItem();
                    }
                }
                if (oldCount != Program.Form.torrentListView.Items.Count)
                    stateChange = true;

                if (stateChange)
                    form.SetAllStateCounters();
                Program.Form.torrentListView.ListViewItemSorter = tmp;
                Program.RaisePostUpdateEvent();
            }
        }
    }
}
