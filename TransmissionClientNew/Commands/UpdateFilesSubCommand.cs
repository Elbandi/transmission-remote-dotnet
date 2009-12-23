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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections;
#if !MONO
using Etier.IconHelper;
#endif

namespace TransmissionRemoteDotnet.Commands
{
    class UpdateFilesUpdateSubCommand : ICommand
    {
        private ListViewItem item;
        private long bytesCompleted;
        private string bytesCompletedStr;
        private decimal progress;
        private bool wanted;
        private JsonNumber priority;

        public UpdateFilesUpdateSubCommand(ListViewItem item, long bytesCompleted)
        {
            this.item = item;
            this.bytesCompleted = bytesCompleted;
            this.bytesCompletedStr = Toolbox.GetFileSize(bytesCompleted);
            this.progress = Toolbox.CalcPercentage(bytesCompleted, (long)item.SubItems[2].Tag);
        }
        public UpdateFilesUpdateSubCommand(ListViewItem item, bool wanted,
            JsonNumber priority, long bytesCompleted)
            : this(item, bytesCompleted)
        {
            this.wanted = wanted;
            this.priority = priority;
        }

        public void Execute()
        {
        }
    }

    class UpdateFilesCreateSubCommand : ICommand
    {
        private ListViewItem item;
#if !MONO
        private string extension;
#endif

        public UpdateFilesCreateSubCommand(string name, long length, bool wanted,
            JsonNumber priority, long bytesCompleted, ImageList img, int mainHandle)
        {
        }

        public void Execute()
        {
#if !MONO
            if (extension != null)
                item.ImageKey = extension;
#endif
            Program.Form.filesListView.Items.Add(item);
        }
    }
}
