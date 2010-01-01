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
using System.Collections;
using System.Text;
using System.Windows.Forms;
using Jayrock.Json;

namespace TransmissionRemoteDotnet.Comparers
{
    public class ListViewItemInt32Comparer : IComparer
    {
        int column;

        public ListViewItemInt32Comparer(int column)
        {
            this.column = column;
        }

        int IComparer.Compare(object x, object y)
        {
            ListViewItem tx = (ListViewItem)x;
            ListViewItem ty = (ListViewItem)y;
            if (tx.SubItems[column].Tag == null && ty.SubItems[column].Tag == null)
            {
                return 0;
            }
            else if (tx.SubItems[column].Tag == null)
            {
                return 1;
            }
            else if (ty.SubItems[column].Tag == null)
            {
                return -1;
            }
            else
            {
                int ix = (int)tx.SubItems[column].Tag;
                int iy = (int)ty.SubItems[column].Tag;
                return ix.CompareTo(iy);
            }
        }
    }
}
