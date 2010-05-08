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
using System.Net;

namespace TransmissionRemoteDotnet.Commmands
{
    public class SessionCommand : ICommand
    {
        private void ParseVersionAndRevisionResponse(string str, TransmissionDaemonDescriptor descriptor)
        {
            try
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if ((str[i] < (int)'0' || str[i] > (int)'9') && str[i] != (int)'.')
                    {
                        descriptor.Version = Double.Parse(str.Substring(0, i), Toolbox.NUMBER_FORMAT);
                        break;
                    }
                }
            }
            catch { }
            try
            {
                int spaceIndex = str.IndexOf(' ');
                descriptor.Revision = Int32.Parse(str.Substring(spaceIndex + 2, str.Length - spaceIndex - 3));
            }
            catch { }
        }

        public SessionCommand(JsonObject response, WebHeaderCollection headers)
        {
            TransmissionDaemonDescriptor descriptor = new TransmissionDaemonDescriptor();
            JsonObject arguments = (JsonObject)response[ProtocolConstants.KEY_ARGUMENTS];
            if (arguments.Contains("version"))
            {
                ParseVersionAndRevisionResponse((string)arguments["version"], descriptor);
            }
            else if (headers.Get("Server") != null)
            {
                descriptor.Version = 1.40;
            }
            else
            {
                descriptor.Version = 1.39;
            }
            if (arguments.Contains("rpc-version"))
                descriptor.RpcVersion = Toolbox.ToInt(arguments["rpc-version"]);
            if (arguments.Contains("rpc-version-minimum"))
                descriptor.RpcVersionMin = Toolbox.ToInt(arguments["rpc-version-minimum"]);
            descriptor.SessionData = (JsonObject)response[ProtocolConstants.KEY_ARGUMENTS];
            Program.DaemonDescriptor = descriptor;
        }

        private delegate void ExecuteDelegate();
        public void Execute()
        {
            MainWindow form = Program.Form;
            if (form.InvokeRequired)
            {
                form.Invoke(new ExecuteDelegate(this.Execute));
            }
            else
            {
                if (!Program.Connected)
                {
                    TransmissionDaemonDescriptor descriptor = Program.DaemonDescriptor;
                    Program.Log(
                        String.Format("({0}) {1}", OtherStrings.Info, OtherStrings.ConnectedTo),
                        String.Format("{0}={1}, {2}={3}, {4}={5}, {6}={7}, {8}={9}",
                            new object[] {
                                OtherStrings.Host,
                                Program.Settings.Current.Host,
                                OtherStrings.Version,
                                descriptor.Version,
                                OtherStrings.Revision,
                                descriptor.Revision,
                                OtherStrings.RpcVersion,
                                descriptor.RpcVersion > 0 ? descriptor.RpcVersion.ToString() : "unspecified",
                                OtherStrings.RpcVersionMinimum,
                                descriptor.RpcVersionMin > 0 ? descriptor.RpcVersionMin.ToString() : "unspecified"
                            }));
                    Program.Connected = true;
                    form.RefreshIfNotRefreshing();
                    if (Program.UploadArgs != null)
                    {
                        form.Upload(Program.UploadArgs, Program.UploadPrompt);
                        Program.UploadArgs = null;
                    }
                }
                else
                {
                    form.SetAltSpeedButtonState(Toolbox.ToBool(Program.DaemonDescriptor.SessionData[ProtocolConstants.FIELD_ALTSPEEDENABLED]));
                }
            }
        }
    }
}
