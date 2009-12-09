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
        /// <summary>
        /// Returns a response stream, optionally decompressed
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static Stream GetResponseStream(HttpWebResponse response)
        {
            Stream compressedStream = null;

            // select right decompression stream (or null if content is not compressed)
            if (response.ContentEncoding.Equals("gzip"))
            {
                compressedStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
            }
            else if (response.ContentEncoding.Equals("deflate"))
            {
                compressedStream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress);
            }

            if (compressedStream != null)
            {
                // decompress
                MemoryStream decompressedStream = new MemoryStream();

                int size = 2048;
                byte[] writeData = new byte[2048];
                while (true)
                {
                    size = compressedStream.Read(writeData, 0, size);
                    if (size > 0)
                    {
                        decompressedStream.Write(writeData, 0, size);
                    }
                    else
                    {
                        break;
                    }
                }
                decompressedStream.Seek(0, SeekOrigin.Begin);
                return decompressedStream;
            }
            else
                return response.GetResponseStream();
        }

        public static ICommand Request(JsonObject data)
        {
            return Request(data, true);
        }

        public static ICommand Request(JsonObject data, bool allowRecursion)
        {
            string str_response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(LocalSettingsSingleton.Instance.RpcUrl);
                TransmissionWebClient.SetupWebRequest(request, true);
                request.Method = "POST";
                request.ContentType = "application/json";
                string json = data.ToString();
                request.ContentLength = json.Length;
                StreamWriter stOut = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                stOut.Write(json);
                stOut.Close();
                HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
                StreamReader stIn = new StreamReader(GetResponseStream(webResponse), Encoding.UTF8);
                str_response = stIn.ReadToEnd();
                stIn.Close();
                JsonObject jsonResponse = (JsonObject)JsonConvert.Import(str_response);
                if ((string)jsonResponse["result"] != "success")
                {
                    return new ErrorCommand(OtherStrings.UnsuccessfulRequest, (string)jsonResponse["result"], true);
                }
                switch (Toolbox.ToShort(jsonResponse[ProtocolConstants.KEY_TAG]))
                {
                    case (short)ResponseTag.TorrentGet:
                        return new TorrentGetCommand(jsonResponse);
                    case (short)ResponseTag.SessionGet:
                        return new SessionCommand(jsonResponse, webResponse.Headers);
                    case (short)ResponseTag.SessionStats:
                        return new SessionStatsCommand(jsonResponse);
                    case (short)ResponseTag.UpdateFiles:
                        return new UpdateFilesCommand(jsonResponse);
                    case (short)ResponseTag.PortTest:
                        return new PortTestCommand(jsonResponse);
                    case (short)ResponseTag.UpdateBlocklist:
                        return new UpdateBlocklistCommand(jsonResponse);
                    case (short)ResponseTag.DoNothing:
                        return new NoCommand();
                }
            }
            catch (InvalidCastException)
            {
                return new ErrorCommand(OtherStrings.UnableToParse, str_response != null ? str_response : "Null", false);
            }
            catch (JsonException ex)
            {
                return new ErrorCommand(String.Format("{0} ({1})", OtherStrings.UnableToParse, ex.GetType()), str_response, false);
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;
                    if (response.StatusCode == HttpStatusCode.Conflict && allowRecursion)
                    {
                        try
                        {
                            string sessionid = ex.Response.Headers["X-Transmission-Session-Id"];
                            if (sessionid != null && sessionid.Length > 0)
                            {
                                TransmissionWebClient.X_transmission_session_id = sessionid;
                                return Request(data, false);
                            }
                        }
                        catch { }
                    }
                }
                return new ErrorCommand(ex, false);
            }
            catch (Exception ex)
            {
                return new ErrorCommand(ex, false);
            }
            return new ErrorCommand(OtherStrings.UnknownResponseTag, str_response != null ? str_response : "null", false);
        }
    }
}