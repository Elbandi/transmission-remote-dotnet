using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;

namespace TransmissionRemoteDotnet
{
    class PersistentCookies
    {
        // to get persistent cookies for us
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InternetGetCookie(string url, string name, StringBuilder data, ref int dataSize);

        private static string RetrieveIECookiesForUrl(string url)
        {
            StringBuilder cookieHeader = new StringBuilder(new String(' ', 256), 256);
            int datasize = cookieHeader.Length;
            if (!InternetGetCookie(url, null, cookieHeader, ref datasize))
            {
                if (datasize < 0)
                    return String.Empty;
                cookieHeader = new StringBuilder(datasize); // resize with new datasize
                InternetGetCookie(url, null, cookieHeader, ref datasize);
            }
            // result is like this: "KEY=Value; KEY2=what ever"
            return cookieHeader.ToString().Replace(";", ",");
        }

        public static CookieContainer GetCookieContainerForUrl(string url)
        {
            return GetCookieContainerForUrl(new Uri(url));
        }

        public static CookieContainer GetCookieContainerForUrl(Uri url)
        {
            CookieContainer container = new CookieContainer();
            string cookieHeaders = RetrieveIECookiesForUrl(url.AbsoluteUri);
            if (cookieHeaders.Length > 0)
            {
                try
                {
                    container.SetCookies(url, cookieHeaders);
                }
                catch (CookieException) { }
            }
            return container;
        }
    }
}
