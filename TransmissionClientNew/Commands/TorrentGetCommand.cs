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
using System.Linq;

namespace TransmissionRemoteDotnet.Commmands
{
    public class TorrentGetCommand : ICommand
    {
        private int oldCount;
        private string statusBarUpdate;
        private bool stateChange = false;
        private int totalUploadInt, totalDownloadInt;
        private bool[] isNew;
        private Torrent[] updateItems;

        public TorrentGetCommand(JsonObject response)
        {
            Program.DaemonDescriptor.ResetFailCount();
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
            oldCount = Program.TorrentIndex.Count;
            updateItems = new Torrent[torrents.Count];
            isNew = new bool[torrents.Count];
            for (int i = 0; i < torrents.Count; i++)
            {
                JsonObject torrent = (JsonObject)torrents[i];
                string hash = (string)torrent[ProtocolConstants.FIELD_HASHSTRING];
                totalTorrents++;
                Torrent t = null;
                lock (Program.TorrentIndex)
                {
                    if (!Program.TorrentIndex.ContainsKey(hash))
                    {
                        isNew[i] = true;
                        t = updateItems[i] = new Torrent(torrent);
                    }
                    else
                    {
                        t = updateItems[i] = Program.TorrentIndex[hash];
                        if (t.Update(torrent, false))
                            stateChange = true;
                        isNew[i] = false;
                    }
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
            this.totalDownloadInt = (int)totalDownload;
            this.totalUploadInt = (int)totalUpload;

            this.statusBarUpdate = String.Format(
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
            );
        }

        public void Execute()
        {
            if (!Program.Connected)
            {
                return;
            }
            MainWindow form = Program.Form;
            form.SuspendTorrentListView();
            IComparer tmp = Program.Form.torrentListView.ListViewItemSorter;
            Program.Form.torrentListView.ListViewItemSorter = null;

            for (int i = 0; i < isNew.Length; i++)
            {
                if (isNew[i])
                    Program.Form.torrentListView.Items.Add(updateItems[i]);
                else
                    updateItems[i].UpdateUi(false);
            }

            foreach (string key in Enumerable.ToArray<string>(Program.TorrentIndex.Keys))
            {
                Torrent t = Program.TorrentIndex[key];
                if (t.UpdateSerial != Program.DaemonDescriptor.UpdateSerial)
                {
                    Program.TorrentIndex.Remove(key);
                    t.RemoveItem();
                }
            }

            if (oldCount != Program.Form.torrentListView.Items.Count)
                stateChange = true;

            Program.Form.torrentListView.ListViewItemSorter = tmp;
            form.ResumeTorrentListView();

            if (stateChange)
                form.SetAllStateCounters();
            form.UpdateStatus(this.statusBarUpdate);
            form.UpdateGraph(totalDownloadInt, totalUploadInt);
            Program.RaisePostUpdateEvent();
        }
    }
}
