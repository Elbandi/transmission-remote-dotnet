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
        private int oldCount;
        private bool stateChange = false;
        private long totalUpload = 0, totalDownload = 0;
        private List<Torrent> UpdateTorrents = new List<Torrent>();

        public TorrentGetCommand(JsonObject response)
        {
            Program.DaemonDescriptor.ResetFailCount();
            if (!Program.Connected)
            {
                return;
            }
            JsonObject arguments = (JsonObject)response[ProtocolConstants.KEY_ARGUMENTS];
            JsonArray torrents = (JsonArray)arguments[ProtocolConstants.KEY_TORRENTS];
            Program.DaemonDescriptor.UpdateSerial++;
            oldCount = Program.TorrentIndex.Count;
            UpdateTorrents.Clear();
            for (int i = 0; i < torrents.Count; i++)
            {
                JsonObject torrent = (JsonObject)torrents[i];
                string hash = (string)torrent[ProtocolConstants.FIELD_HASHSTRING];
                Torrent t = null;
                lock (Program.TorrentIndex)
                {
                    if (!Program.TorrentIndex.ContainsKey(hash))
                    {
                        t = new Torrent(torrent);
                    }
                    else
                    {
                        t = Program.TorrentIndex[hash];
                        if (t.Update(torrent, false))
                            stateChange = true;
                    }
                    UpdateTorrents.Add(t);
                }
                totalUpload += t.UploadRate;
                totalDownload += t.DownloadRate;
            }
        }

        public void Execute()
        {
            if (!Program.Connected)
            {
                return;
            }
            MainWindow form = Program.Form;
            foreach (Torrent t in UpdateTorrents)
            {
                if (t.ListView != null)
                {
                    t.UpdateUi(false);
                }
            }
            UpdateTorrents.Clear();
            string[] keys = (string[])new ArrayList(Program.TorrentIndex.Keys).ToArray(typeof(string));
            foreach (string key in keys)
            {
                Torrent t = Program.TorrentIndex[key];
                if (t.UpdateSerial != Program.DaemonDescriptor.UpdateSerial)
                {
                    Program.TorrentIndex.Remove(key);
                    t.RemoveItem();
                }
            }

            if (oldCount != Program.TorrentIndex.Count)
                stateChange = true;

            if (stateChange)
                form.SetAllStateCounters();
            form.UpdateStatus(true);
            form.UpdateGraph((int)totalDownload, (int)totalUpload);
            Program.RaisePostUpdateEvent();
        }
    }
}
