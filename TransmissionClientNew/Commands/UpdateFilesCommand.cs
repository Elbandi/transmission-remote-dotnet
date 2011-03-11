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
using System.Threading;
using System.Collections;

namespace TransmissionRemoteDotnet.Commmands
{
    public class UpdateFilesCommand : ICommand
    {
        public delegate Torrent GetTorrentDelegate();

        public UpdateFilesCommand(JsonObject response)
        {
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Program.Settings.Locale, true);
            Program.DaemonDescriptor.ResetFailCount();
            MainWindow form = Program.Form;
            JsonObject arguments = (JsonObject)response[ProtocolConstants.KEY_ARGUMENTS];
            JsonArray torrents = (JsonArray)arguments[ProtocolConstants.KEY_TORRENTS];
            if (torrents.Count != 1)
            {
                return;
            }
            JsonObject torrent = (JsonObject)torrents[0];
            JsonArray files = (JsonArray)torrent[ProtocolConstants.FIELD_FILES];
            if (files == null)
            {
                return;
            }
            int id = Toolbox.ToInt(torrent[ProtocolConstants.FIELD_ID]);
            Torrent t = (Torrent)form.Invoke(new GetTorrentDelegate(delegate()
            {
                lock (Program.TorrentIndex)
                {
                    foreach (KeyValuePair<string, Torrent> st in Program.TorrentIndex)
                    {
                        if (st.Value.Id == id)
                            return st.Value;
                    }
                }
                return null;
            }));
            if (t == null)
            {
                return;
            }
            JsonArray priorities = (JsonArray)torrent[ProtocolConstants.FIELD_PRIORITIES];
            JsonArray wanted = (JsonArray)torrent[ProtocolConstants.FIELD_WANTED];
            bool havepriority = (priorities != null && wanted != null);
            ImageList imgList = Program.Form.fileIconImageList;
            for (int i = 0; i < files.Length; i++)
            {
                JsonObject file = (JsonObject)files[i];
                string name = Toolbox.TrimPath((string)file[ProtocolConstants.FIELD_NAME]);
                FileListViewItem item = t.Files.Find(name);
                if (item == null)
                {
                    item = new FileListViewItem(file, imgList, i, wanted, priorities);
                    t.Files.Add(item);
                }
                else
                {
                    Program.Form.Invoke(new MethodInvoker(delegate()
                    {
                        item.Update(file, wanted, priorities);
                    }));
                }
            }
        }

        public delegate void ExecuteDelegate();
        public void Execute()
        {
            MainWindow form = Program.Form;
            if (form.InvokeRequired)
            {
                form.Invoke(new ExecuteDelegate(Execute));
            }
            else
            {
                Torrent t = null;
                ListView torrentListView = form.torrentListView;
                lock (torrentListView)
                {
                    if (torrentListView.SelectedItems.Count == 1)
                    {
                        t = (Torrent)torrentListView.SelectedItems[0];
                    }
                }
                if (t == null)
                    return;
                form.FillfilesListView(t);
                form.filesTimer.Enabled = true;
            }
        }
    }
}
