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
    public class ListViewTorrentInt32Comparer : IComparer
    {
        string jsonKey;

        public ListViewTorrentInt32Comparer(string jsonKey)
        {
            this.jsonKey = jsonKey;
        }

        int IComparer.Compare(object x, object y)
        {
            ListViewItem lx = (ListViewItem)x;
            ListViewItem ly = (ListViewItem)y;
            Torrent tx = (Torrent)lx.Tag;
            Torrent ty = (Torrent)ly.Tag;

            if (jsonKey.Equals(ProtocolConstants.FIELD_LEECHERS))
            {
                return tx.Leechers.CompareTo(ty.Leechers);
            }
            else if (jsonKey.Equals(ProtocolConstants.FIELD_SEEDERS))
            {
                return tx.Seeders.CompareTo(ty.Seeders);
            }
            else
            {
                int nx = Toolbox.ToInt(tx.Info[jsonKey]);
                int ny = Toolbox.ToInt(ty.Info[jsonKey]);
                return nx.CompareTo(ny);
            }
        }
    }
}
