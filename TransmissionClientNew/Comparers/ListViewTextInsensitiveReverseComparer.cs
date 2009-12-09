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

namespace TransmissionRemoteDotnet.Comparers
{
    public class ListViewTextInsensitiveReverseComparer : IComparer
    {
        int column;
        static CaseInsensitiveComparer casecomparer = new CaseInsensitiveComparer();

        public ListViewTextInsensitiveReverseComparer(int column)
        {
            this.column = column;
        }

        int IComparer.Compare(object x, object y)
        {
            ListViewItem lx = (ListViewItem)x;
            ListViewItem ly = (ListViewItem)y;
            string sx = ReverseString(lx.SubItems[column].Text);
            string sy = ReverseString(ly.SubItems[column].Text);
            return casecomparer.Compare(sx, sy);
        }

        string ReverseString(string str)
        {
            char[] strArray = str.ToCharArray();
            Array.Reverse(strArray);
            return new string(strArray);
        }
    }
}
