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

        public UpdateFilesUpdateSubCommand(ListViewItem item, long bytesCompleted)
        {
            this.item = item;
            this.bytesCompleted = bytesCompleted;
            this.bytesCompletedStr = Toolbox.GetFileSize(bytesCompleted);
            this.progress = Toolbox.CalcPercentage(bytesCompleted, (long)item.SubItems[2].Tag);
        }

        public void Execute()
        {
            item.SubItems[3].Tag = bytesCompleted;
            item.SubItems[3].Text = bytesCompletedStr;
            item.SubItems[4].Tag = progress;
            item.SubItems[4].Text = progress + "%";
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
            this.item = new ListViewItem(Toolbox.TrimPath(name));
            item.SubItems[0].Tag = item.SubItems[0].Text.Length != name.Length;
            string[] split = name.Split('.');
            string typeName = "";
            if (split.Length > 1)
            {
                string extension = split[split.Length - 1].ToLower();
#if !MONO
                if (img.Images.ContainsKey(extension) || IconReader.AddToImgList(extension, img))
                {
                    this.extension = extension;
                    typeName = IconReader.GetTypeName(extension);
                }
#else
                typeName = extension;
#endif
            }
            item.Name = item.ToolTipText = name;
            item.SubItems.Add(typeName);
            item.SubItems.Add(Toolbox.GetFileSize(length));
            item.SubItems[2].Tag = length;
            item.SubItems.Add(Toolbox.GetFileSize(bytesCompleted));
            item.SubItems[3].Tag = bytesCompleted;
            decimal progress = Toolbox.CalcPercentage(bytesCompleted, length);
            item.SubItems.Add(progress + "%");
            item.SubItems[4].Tag = progress;
            item.SubItems.Add(wanted ? OtherStrings.No : OtherStrings.Yes);
            item.SubItems.Add(FormatPriority(priority));
            lock (Program.Form.FileItems)
            {
                Program.Form.FileItems.Add(item);
            }
        }

        public void Execute()
        {
#if !MONO
            if (extension != null)
                item.ImageKey = extension;
#endif
            Program.Form.filesListView.Items.Add(item);
        }

        private string FormatPriority(JsonNumber n)
        {
            short s = n.ToInt16();
            if (s < 0)
            {
                return OtherStrings.Low;
            }
            else if (s > 0)
            {
                return OtherStrings.High;
            }
            else
            {
                return OtherStrings.Normal;
            }
        }
    }
}
