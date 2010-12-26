using System;
using System.Collections.Generic;
using System.Text;

namespace TransmissionRemoteDotnet
{
    class TrackerListItem
    {
        string announce;
        string origannounce;
        int id;
        public TrackerListItem(string announce, int id)
        {
            this.origannounce = this.announce = announce;
            this.id = id;
        }
        public int Id
        {
            get { return id; }
        }
        public string Announce
        {
            get { return announce; }
            set { announce = value; }
        }
        public bool Changed
        {
            get { return !origannounce.Equals(announce); }
        }
        public override string ToString()
        {
            return announce;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is TrackerListItem)
                return announce.Equals((obj as TrackerListItem).announce);
            else if (obj is string)
                return announce.Equals(obj as string);
            else if (obj is int)
                return id.Equals((int)obj);
            else
                return false;
        }
    }
}
