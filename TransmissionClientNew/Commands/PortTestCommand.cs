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

namespace TransmissionRemoteDotnet
{
    class PortTestCommand : ICommand
    {
        private bool port_is_open;

        public PortTestCommand(JsonObject response)
        {
            JsonObject arguments = (JsonObject)response[ProtocolConstants.KEY_ARGUMENTS];
            this.port_is_open = Toolbox.ToBool(arguments[ProtocolConstants.FIELD_PORT_IS_OPEN]);
        }

        public void Execute()
        {
            RemoteSettingsDialog.PortTestReplyArrived();
            MessageBox.Show(port_is_open ? OtherStrings.PortIsOpen : OtherStrings.PortIsClosed, OtherStrings.PortTestResult, MessageBoxButtons.OK, port_is_open ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }
    }
}
