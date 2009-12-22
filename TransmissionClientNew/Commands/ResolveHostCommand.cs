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
using System.Windows.Forms;
using System.Net;

namespace TransmissionRemoteDotnet.Commmands
{
    public class ResolveHostCommand : ICommand
    {
        private PeerListViewItem item;
        private IPHostEntry host;

        public ResolveHostCommand(PeerListViewItem item)
        {
            this.item = item;
            try
            {
                this.host = Dns.GetHostEntry(item.IpAddress);
            }
            catch { }
        }

        public void Execute()
        {
            if (this.host != null && !host.HostName.Equals(this.item.SubItems[0].Text))
            {
                item.Hostname = host.HostName;
            }
        }
    }
}
