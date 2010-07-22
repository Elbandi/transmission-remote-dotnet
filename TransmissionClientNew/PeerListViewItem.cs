using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Jayrock.Json;
using System.Net;
using MaxMind;
using System.Drawing;
using System.ComponentModel;
using TransmissionRemoteDotnet.Commmands;

namespace TransmissionRemoteDotnet
{
    public class PeerListViewItem : ListViewItem
    {
        public static int CurrentUpdateSerial = 0;

        public void Update(JsonObject peerObj)
        {
            this.FlagStr = (string)peerObj[ProtocolConstants.FIELD_FLAGSTR];
            this.Progress = Toolbox.ToProgress(peerObj[ProtocolConstants.FIELD_PROGRESS]);
            if (Program.DaemonDescriptor.Revision >= 10937)
            {
                this.RateToClient = (long)(Toolbox.ToDouble(peerObj[ProtocolConstants.FIELD_RATETOCLIENT]) * 1000);
                this.RateToPeer = (long)(Toolbox.ToDouble(peerObj[ProtocolConstants.FIELD_RATETOPEER]) * 1000);
            }
            else
            {
                this.RateToClient = Toolbox.ToLong(peerObj[ProtocolConstants.FIELD_RATETOCLIENT]);
                this.RateToPeer = Toolbox.ToLong(peerObj[ProtocolConstants.FIELD_RATETOPEER]);
            }
        }

        public PeerListViewItem(JsonObject peerObj)
            : base((string)peerObj[ProtocolConstants.ADDRESS])
        {
            for (int i = 0; i < 7; i++)
                base.SubItems.Add("");
            this.Address = base.Name = base.Text;
            int countryIndex = -1;
            if (!GeoIPCountry.Disabled)
            {
                try
                {
                    countryIndex = GeoIPCountry.Instance.FindIndex(this.IpAddress);
                }
                catch { }
            }
            if (countryIndex > 0)
                this.Country = GeoIPCountry.CountryNames[countryIndex];
            this.ClientName = (string)peerObj[ProtocolConstants.FIELD_CLIENTNAME];
            this.Update(peerObj);
            if (countryIndex > 0)
            {
                base.ImageIndex = GeoIPCountry.FlagImageList.Images.IndexOfKey("flags_" + GeoIPCountry.CountryCodes[countryIndex].ToLower());
            }
            Dns.BeginGetHostEntry(this.IpAddress, new AsyncCallback(GetHostEntryCallback), this);
        }

        private delegate void SetHostNameDelegate(IPHostEntry host);
        private void SetHostName(IPHostEntry host)
        {
            if (this.ListView != null && this.ListView.InvokeRequired)
            {
                this.ListView.Invoke(new SetHostNameDelegate(this.SetHostName), host);
            }
            else
                if (host != null && !host.HostName.Equals(this.SubItems[0].Text))
                {
                    this.Hostname = host.HostName;
                }
        }

        private static void GetHostEntryCallback(IAsyncResult ar)
        {
            PeerListViewItem item = (PeerListViewItem)ar.AsyncState;
            try
            {
                IPHostEntry host = Dns.EndGetHostEntry(ar);
                item.SetHostName(host);
            }
            catch
            {
            }
        }

        private void SetText(int idx, string str)
        {
            if (!str.Equals(base.SubItems[idx].Text))
                base.SubItems[idx].Text = str;
        }

        public string Hostname
        {
            get { return base.SubItems[1].Text; }
            set
            {
                SetText(1, base.ToolTipText = value);
            }
        }

        public int UpdateSerial
        {
            get;
            set;
        }

        public string Address
        {
            get { return base.Text; }
            set
            {
                base.Text = base.ToolTipText = value;
                this.IpAddress = IPAddress.Parse(value);
            }
        }

        public IPAddress IpAddress
        {
            get;
            set;
        }

        public decimal Progress
        {
            get { return (decimal)base.SubItems[5].Tag; }
            set
            {
                SetText(5, value + "%");
                base.SubItems[5].Tag = value;
            }
        }

        public long RateToPeer
        {
            get { return (long)base.SubItems[7].Tag; }
            set
            {
                SetText(7, Toolbox.GetSpeed(value));
                base.SubItems[7].Tag = value;
            }
        }

        public long RateToClient
        {
            get { return (long)base.SubItems[6].Tag; }
            set
            {
                SetText(6, Toolbox.GetSpeed(value));
                base.SubItems[6].Tag = value;
            }
        }

        public string Country
        {
            get { return base.SubItems[2].Text; }
            set { SetText(2, value); }
        }

        public string FlagStr
        {
            get { return base.SubItems[3].Text; }
            set { SetText(3, value); }
        }

        public string ClientName
        {
            get { return base.SubItems[4].Text; }
            set { SetText(4, value); }
        }
    }
}
