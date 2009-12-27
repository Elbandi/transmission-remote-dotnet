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
        public UpdateFilesCommand(JsonObject response)
        {
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Program.Settings.Locale);
            Program.DaemonDescriptor.ResetFailCount();
            MainWindow form = Program.Form;
            JsonObject arguments = (JsonObject)response[ProtocolConstants.KEY_ARGUMENTS];
            JsonArray torrents = (JsonArray)arguments[ProtocolConstants.KEY_TORRENTS];
            if (torrents.Count != 1)
            {
                return;
            }
            JsonObject torrent = (JsonObject)torrents[0];
            int id = Toolbox.ToInt(torrent[ProtocolConstants.FIELD_ID]);
            Torrent t = null;
            form.Invoke(new MethodInvoker(delegate()
            {
                ListView torrentListView = form.torrentListView;
                lock (torrentListView)
                {
                    if (torrentListView.SelectedItems.Count == 1)
                    {
                        t = (Torrent)torrentListView.SelectedItems[0];
                    }
                }
            }));
            if (t == null || t.Id != id)
            {
                return;
            }
            JsonArray files = (JsonArray)torrent[ProtocolConstants.FIELD_FILES];
            if (files == null)
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
                long bytesCompleted = Toolbox.ToLong(file[ProtocolConstants.FIELD_BYTESCOMPLETED]);
                long length = Toolbox.ToLong(file[ProtocolConstants.FIELD_LENGTH]);
                FileItem item = t.Files.Find(name);
                if (item == null)
                {
                    FileItem fileItem = new FileItem(file, imgList, i, wanted, priorities);
                    t.Files.Add(fileItem);
                }
                else
                {
                    item.Update(file, wanted, priorities);
                }
            }
        }

        public void Execute()
        {
            MainWindow form = Program.Form;
            // TODO: need a real locking: torrent and filelist !!!
            Torrent t = null;
            form.Invoke(new MethodInvoker(delegate()
            {
                ListView torrentListView = form.torrentListView;
                lock (torrentListView)
                {
                    if (torrentListView.SelectedItems.Count == 1)
                    {
                        t = (Torrent)torrentListView.SelectedItems[0];
                    }
                }
            }));
            if (t == null)
                return;
            lock (form.filesListView)
            {
                form.filesListView.SuspendLayout();
                IComparer tmp = form.filesListView.ListViewItemSorter;
                form.filesListView.ListViewItemSorter = null;
                form.filesListView.Enabled = true;
                foreach (FileItem item in t.Files)
                {
                    ListViewItem litem;
                    if (!form.filesListView.Items.ContainsKey(item.FileName))
                    {
                        litem = form.filesListView.Items.Add(new ListViewItem());
                    }
                    else
                    {
                        litem = form.filesListView.Items[item.FileName];
                    }
                    item.UpdateListviewItem(litem);
                }
                form.filesListView.ListViewItemSorter = tmp;
                form.filesListView.Sort();
                Toolbox.StripeListView(form.filesListView);
                form.filesListView.ResumeLayout();
            }
            form.filesTimer.Enabled = true;
        }
    }
}
