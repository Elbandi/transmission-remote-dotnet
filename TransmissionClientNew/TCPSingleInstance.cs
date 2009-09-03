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

#if !DOTNET35
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using Jayrock.Json;
using Jayrock.Json.Conversion;

namespace TransmissionRemoteDotnet
{
    class TCPSingleInstance : IDisposable
    {
        private int port;
        private TcpListener listener;
        private bool isFirstInstance;

        public TCPSingleInstance(int port)
        {
            try
            {
                this.listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                listener.Start();
                this.isFirstInstance = true;
            }
            catch
            {
                this.port = port;
                this.isFirstInstance = false;
            }
        }

        #region ISingleInstance Members

        public event EventHandler<ArgumentsReceivedEventArgs> ArgumentsReceived;

        public bool IsFirstInstance
        {
            get
            {
                return this.isFirstInstance;
            }
        }

        public void ListenForArgumentsFromSuccessiveInstances()
        {
            if (!isFirstInstance)
                throw new InvalidOperationException("This is not the first instance.");
            ThreadPool.QueueUserWorkItem(new WaitCallback(ListenForArguments));
        }

        private void ListenForArguments(Object state)
        {
            StreamReader reader = null;
            try
            {
                Stream clientStream = listener.AcceptTcpClient().GetStream();
                reader = new StreamReader(clientStream);
                List<string> arguments = new List<string>();
                foreach (string arg in (JsonArray)JsonConvert.Import(reader.ReadLine()))
                {
                    if (arg != null && arg.Length > 0)
                        arguments.Add(arg);
                }
                ThreadPool.QueueUserWorkItem(new WaitCallback(CallOnArgumentsReceived), arguments.ToArray());
            }
            catch
            { }
            finally
            {
                if (reader != null)
                    reader.Close();
                ListenForArguments(null);
            }
        }

        private void CallOnArgumentsReceived(Object state)
        {
            if (ArgumentsReceived != null)
                ArgumentsReceived(this, new ArgumentsReceivedEventArgs() { Args = (string[])state });
        }

        public bool PassArgumentsToFirstInstance(string[] arguments)
        {
            if (isFirstInstance)
                throw new InvalidOperationException("This is the first instance.");
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", this.port);
            StreamWriter writer = new StreamWriter(client.GetStream());
            writer.WriteLine((new JsonArray(arguments)).ToString());
            writer.Close();
            return true;
        }

        #endregion

        #region IDisposable
        private Boolean disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                this.listener.Stop();
                disposed = true;
            }
        }

        ~TCPSingleInstance()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
#endif