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

namespace TransmissionRemoteDotnet
{
    class TransmissionDaemonDescriptor
    {
        private double version = 1.41;

        public double Version
        {
            get { return version; }
            set { version = value; }
        }
        private int revision = -1;

        public int Revision
        {
            get { return revision; }
            set { revision = value; }
        }
        private int rpcVersion = -1;

        public int RpcVersion
        {
            get { return rpcVersion; }
            set { rpcVersion = value; }
        }
        private int rpcVersionMin = -1;

        public int RpcVersionMin
        {
            get { return rpcVersionMin; }
            set { rpcVersionMin = value; }
        }
        private JsonObject sessionData;

        public JsonObject SessionData
        {
            get { return sessionData; }
            set { sessionData = value; }
        }
        private long updateSerial = 0;

        public long UpdateSerial
        {
            get { return updateSerial; }
            set { updateSerial = value; }
        }
        private int failCount = 0;

        public int FailCount
        {
            get { return failCount; }
            set { failCount = value; }
        }

        public void ResetFailCount()
        {
            failCount = 0;
        }
    }
}
