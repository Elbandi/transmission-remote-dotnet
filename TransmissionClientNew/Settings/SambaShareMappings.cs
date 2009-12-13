using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransmissionRemoteDotnet.Settings
{
    class SambaShareMappings
    {
        public string UnixPathPrefix;
        public string SambaShare;
        public SambaShareMappings(string UnixPathPrefix, string SambaShare)
        {
            this.UnixPathPrefix = UnixPathPrefix;
            this.SambaShare = SambaShare;
        }

        public override string ToString()
        {
            return String.Format("{0} => {1}", UnixPathPrefix, SambaShare);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is SambaShareMappings)
                return UnixPathPrefix.Equals((obj as SambaShareMappings).UnixPathPrefix);
            else if (obj is string)
                return UnixPathPrefix.Equals(obj as string);
            else
                return false;
        }
    }
}
