
// Copyright © 2009 by Christoph Richner. All rights are reserved.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//
// website http://www.raccoom.net, email support@raccoom.net, msn chrisdarebell@msn.com

using System;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using System.Net;

namespace Raccoom.Xml
{	
	/// <summary>
	/// IPersistentManager is responsible for saving and publishing.
	/// </summary>
	public interface IPersistentManager
	{
		/// <summary>
		/// Publish the object to the specified location.
		/// </summary>
		/// <param name="uri">The URI of the resource to receive the data. </param>
		/// <param name="method">The method used to send the data to the resource. (POST)</param>
		/// <param name="proxy">HTTP proxy settings for the WebRequest class.</param>
		/// <param name="networkCredential">Credentials for password-based authentication schemes such as basic, digest, NTLM, and Kerberos authentication.</param>
		/// <example>
		/// This sample shows how to publish (Default Proxy)
		/// <code>
		/// // password-based authentication for web resource
		/// System.Net.NetworkCredential providerCredential = new System.Net.NetworkCredential("username", "password", "domain");
		/// // use default system proxy
		/// Uri uri = new Uri("http://domain.net");
		/// Publish(uri, null, "POST", providerCredential);
		/// </code>
		/// This sample shows how to publish (Custom Proxy)
		/// <code>
		/// // password-based authentication for web resource
		/// System.Net.NetworkCredential providerCredential = new System.Net.NetworkCredential("username", "password", "domain");
		/// // password-based authentication for web proxy
		/// System.Net.NetworkCredential proxyCredential = new System.Net.NetworkCredential("username", "password", "domain");
		/// // create custom proxy
		/// System.Net.WebProxy webProxy = new System.Net.WebProxy("http://proxyurl:8080",false);
		/// webProxy.Credentials = proxyCredential;
		/// // publish
		/// Publish(uri, webProxy, "POST", providerCredential);
		/// </code>
		/// </example>
		void Publish (Uri uri, System.Net.WebProxy proxy, string method, System.Net.NetworkCredential networkCredential);
		/// <summary>
		/// Saves the object to a local file.
		/// </summary>
		/// <param name="filename">The path and name of the file to create.</param>
		void Save (string filename);
        /// <summary>
        /// Saves the object to a local file.
        /// </summary>
        void Save(string filename, System.Text.Encoding encoding);
		/// <summary>
		/// Write the object to the specified stream.
		/// </summary>
		/// <param name="stream">The Stream used to write the XML document.</param>
		void Save (System.IO.Stream stream);
        /// <summary>
        /// Saves the object to a local file.
        /// </summary>
        void Save(System.IO.Stream stream, System.Text.Encoding encoding);
		/// <summary>
		/// Transforms the XML data using XSLT stylesheet.
		/// </summary>
		/// <param name="styleSheet">An XmlReader object that contains the XSLT stylesheet.</param>
		/// <returns>An MemoryStream containing the results of the transform.</returns>
		System.IO.MemoryStream Transform (System.Xml.XmlReader styleSheet);
		/// <summary>
		/// Transforms the XML data using XSLT stylesheet to an output file (html)
		/// </summary>
		/// <param name="styleSheet">An XmlReader object that contains the XSLT stylesheet.</param>
		/// <param name="fileNameHtmlOutput">Filename of the html output file.</param>
		/// <example>
		/// This sample shows how to transform (XSLT/CSS) where transform.xslt is a custom xslt file.
		/// <code>
		/// // transform to stream
		///	System.IO.MemoryStream memoryStream = Transform(new System.Xml.XmlTextReader("transform.xslt"));
		///	// transform to html output file
		///	Transform(new System.Xml.XmlTextReader("transform.xslt"), "myChannel.htm");
		///	// transform to html and xml output file
		///	Transform(new System.Xml.XmlTextReader("transform.xslt"), "channel.xml", "channel.htm");
		///	</code>
		/// </example>
		void Transform (System.Xml.XmlReader styleSheet, string fileNameHtmlOutput);
		/// <summary>
		/// Transforms the XML data using XSLT stylesheet to an output file (xml, html)		
		/// </summary>
		/// <param name="styleSheet">An XmlReader object that contains the XSLT stylesheet.</param>
		/// <param name="fileNameXmlOutput">Filename of the xml output file.</param>
		/// <param name="fileNameHtmlOutput">Filename of the html output file.</param>
		void Transform (System.Xml.XmlReader styleSheet, string fileNameXmlOutput, string fileNameHtmlOutput);
		/// <summary>
		/// Transforms the XML stream using XSLT stylesheet.
		/// </summary>
		/// <param name="styleSheet">An XmlReader object that contains the XSLT stylesheet.</param>
		/// <param name="xmlStream">The data to transform</param>
		/// <returns>An MemoryStream containing the results of the transform.</returns>	
		System.IO.MemoryStream Transform (System.Xml.XmlReader styleSheet, System.IO.Stream xmlStream);
	}
}