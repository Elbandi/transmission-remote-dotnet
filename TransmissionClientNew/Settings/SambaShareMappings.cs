using System;
using System.Collections.Generic;
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
                return UnixPathPrefix.StartsWith((obj as SambaShareMappings).UnixPathPrefix) || (obj as SambaShareMappings).UnixPathPrefix.StartsWith(UnixPathPrefix);
            else if (obj is string)
                return UnixPathPrefix.StartsWith(obj as string) || (obj as string).StartsWith(UnixPathPrefix);
            else
                return false;
        }
    }
}
