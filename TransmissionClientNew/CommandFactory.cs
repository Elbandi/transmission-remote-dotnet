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

/* This factory class is responsible for dispatching JSON requests and
 * torrent uploads, and also creating an object using the command design
 * pattern which contains the logic for updating the UI. */

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Jayrock.Json;
using Jayrock.Json.Conversion;
using System.IO;
using System.IO.Compression;
using TransmissionRemoteDotnet.Commmands;

namespace TransmissionRemoteDotnet
{
    public class CommandFactory
    {
        public static TransmissionWebClient RequestAsync(JsonObject data)
        {
            return RequestAsync(data, true);
        }

        public static TransmissionWebClient RequestAsync(JsonObject data, bool allowRecursion)
        {
            TransmissionWebClient wc = new TransmissionWebClient(true);
            wc.UploadStringCompleted += new UploadStringCompletedEventHandler(wc_UploadStringCompleted);
            wc.UploadStringAsync(new Uri(LocalSettingsSingleton.Instance.RpcUrl), null, data.ToString(), new TransmissonRequest(data, allowRecursion));
            return wc;
        }

        static void wc_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            ICommand cmd;
            if (!e.Cancelled)
            {
                if (e.Error != null)
                {
                    WebException ex = e.Error as WebException;
                    if (ex.Response != null)
                    {
                        HttpWebResponse response = (HttpWebResponse)ex.Response;
                        if (response.StatusCode == HttpStatusCode.Conflict && ((TransmissonRequest)e.UserState).allowRecursion)
                        {
                            try
                            {
                                string sessionid = ex.Response.Headers["X-Transmission-Session-Id"];
                                if (sessionid != null && sessionid.Length > 0)
                                {
                                    TransmissionWebClient.X_transmission_session_id = sessionid;
                                    (sender as TransmissionWebClient).UploadStringAsync(new Uri(LocalSettingsSingleton.Instance.RpcUrl), null, ((TransmissonRequest)e.UserState).data.ToString(), new TransmissonRequest(((TransmissonRequest)e.UserState).data, false));
                                    return;
                                }
                            }
                            catch { }
                        }
                    }
                    cmd = new ErrorCommand(ex, false);
                }
                else
                {
                    try
                    {
                        JsonObject jsonResponse = (JsonObject)JsonConvert.Import(e.Result);
                        if ((string)jsonResponse["result"] != "success")
                        {
                            cmd = new ErrorCommand(OtherStrings.UnsuccessfulRequest, (string)jsonResponse["result"], true);
                        }
                        else
                        {
                            switch (Toolbox.ToShort(jsonResponse[ProtocolConstants.KEY_TAG]))
                            {
                                case (short)ResponseTag.TorrentGet:
                                    cmd = new TorrentGetCommand(jsonResponse);
                                    break;
                                case (short)ResponseTag.SessionGet:
                                    cmd = new SessionCommand(jsonResponse, (sender as WebClient).Headers);
                                    break;
                                case (short)ResponseTag.SessionStats:
                                    cmd = new SessionStatsCommand(jsonResponse);
                                    break;
                                case (short)ResponseTag.UpdateFiles:
                                    cmd = new UpdateFilesCommand(jsonResponse);
                                    break;
                                case (short)ResponseTag.PortTest:
                                    cmd = new PortTestCommand(jsonResponse);
                                    break;
                                case (short)ResponseTag.UpdateBlocklist:
                                    cmd = new UpdateBlocklistCommand(jsonResponse);
                                    break;
                                case (short)ResponseTag.DoNothing:
                                    cmd = new NoCommand();
                                    break;
                                default:
                                    cmd = new ErrorCommand(OtherStrings.UnknownResponseTag, e.Result != null ? e.Result : "null", false);
                                    break;
                            }
                        }
                    }
                    catch (InvalidCastException)
                    {
                        cmd = new ErrorCommand(OtherStrings.UnableToParse, e.Result != null ? e.Result : "Null", false);
                    }
                    catch (JsonException ex)
                    {
                        cmd = new ErrorCommand(String.Format("{0} ({1})", OtherStrings.UnableToParse, ex.GetType()), e.Result, false);
                    }
                    catch (Exception ex)
                    {
                        cmd = new ErrorCommand(ex, false);
                    }
                }
                try
                {
                    cmd.Execute();
                }
                catch (Exception ee)
                { // just for debugging...
                    Console.WriteLine(ee.Message);
                }
                (sender as TransmissionWebClient).OnCompleted(cmd);
            }
        }

    }
    struct TransmissonRequest
    {
        public JsonObject data;
        public bool allowRecursion;
        public TransmissonRequest(JsonObject data, bool allowRecursion)
        {
            this.data = data;
            this.allowRecursion = allowRecursion;
        }
    }
}