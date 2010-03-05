
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
    /// <summary>Opml is an XML element, with a single required attribute, version; a head element and a body element, both of which are required. The version attribute is a version string, of the form, x.y, where x and y are both numeric strings.</summary>
    [System.Xml.Serialization.XmlRoot()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "opml")]
    [Serializable]
    [System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public class OpmlDocument
    : IOpmlDocument
    {
        #region fields

        ///<summary>A PropertyChanged event is raised when a property is changed on a component. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>Head</summary>
        private OpmlHead _head;
        /// <summary>Body</summary>
        private OpmlBody _body;
        /// <summary>Returns detailed information about exceptions occured against the used schema (XmlValidationReader)</summary>
        private System.Collections.ArrayList _ValidationEventArgsList = null;

        #endregion

        #region constructors

        /// <summary>Initializes a new instance of OpmlDocument with default values</summary>
        public OpmlDocument()
        {
            this.Body = new OpmlBody();
            this.Head = new OpmlHead();
            _ValidationEventArgsList = new System.Collections.ArrayList();
        }

        /// <summary>
        /// Initializes a new instance of OpmlDocument from <c>Uri</c> using default proxy settings.
        /// </summary>
        /// <param name="uri">The URI of the resource to receive the data.</param>
        public OpmlDocument(Uri uri)
            : this(uri, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of OpmlDocument from <c>Uri</c> using the specified <c>WebProxy</c>.
        /// </summary>
        /// <param name="uri">The URI of the resource to receive the data.</param>
        /// <param name="proxy">Network proxy that the request uses to access the Internet resource</param>
        public OpmlDocument(Uri uri, System.Net.WebProxy proxy)
            : this()
        {
            System.Net.WebRequest webRequest = System.Net.HttpWebRequest.Create(uri);
            //
            if (proxy != null)
            {
                webRequest.Proxy = proxy;
            }
            using (System.Net.WebResponse webResponse = webRequest.GetResponse())
            {
                using (System.IO.Stream stream = webResponse.GetResponseStream())
                {
                    this.Parse(this, new System.Xml.XmlTextReader(stream));
                    stream.Close();
                }
                webResponse.Close();
            }
        }

        /// <summary>
        /// Initializes a new instance of OpmlDocument from a stream.
        /// </summary>
        /// <param name="stream">The stream containing the XML data to read.</param>
        public OpmlDocument(System.IO.Stream stream)
            : this()
        {
            this.Parse(this, new System.Xml.XmlTextReader(stream));
        }

        /// <summary>
        /// Initializes a new instance of OpmlDocument from <c>XmlTextReader</c>
        /// </summary>
        /// <param name="xmlReader">The <c>XmlTextReader</c> containing the XML data to read.</param>
        public OpmlDocument(System.Xml.XmlTextReader xmlReader)
            : this()
        {
            this.Parse(this, xmlReader);
        }

        #endregion

        #region public interface

        /// <summary>
        /// Saves the object to a local file.
        /// </summary>
        /// <param name="filename">The path and name of the file to create.</param>
        public virtual void Save(string filename)
        {
            Save(filename, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// Saves the object to a local file.
        /// </summary>
        /// <param name="filename">The path and name of the file to create.</param>
        public virtual void Save(string filename, System.Text.Encoding encoding)
        {
            System.IO.Stream stream = null;
            try
            {
                stream = System.IO.File.Open(filename, System.IO.FileMode.Create);
                Save(stream, encoding);
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }

        /// <summary>
        /// Write the object to the specified stream.
        /// </summary>
        /// <param name="stream">The Stream used to write the XML document.</param>
        public virtual void Save(System.IO.Stream stream)
        {
            Save(stream, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// Write the object to the specified stream.
        /// </summary>
        /// <param name="stream">The Stream used to write the XML document.</param>
        public virtual void Save(System.IO.Stream stream, System.Text.Encoding encoding)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(stream, encoding);
                //Use indenting for readability.
                writer.Formatting = Formatting.Indented;
                //Write the XML delcaration. 
                writer.WriteStartDocument();
                writer.WriteComment("Created by Raccoom.Xml OPML Framework classes Version " + this.GetType().Assembly.GetName().Version.ToString() + ", Copyright © 2004 by Christoph Richner. All rights reserved. Website http://raccoom.sytes.net, Email chrisdarebell@msn.com");
                // determine type of Outline
                //
                System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(this.GetType());
                ser.Serialize(writer, this);
                writer.WriteEndDocument();
                writer.Flush();
            }
            finally
            {
                //if(writer!=null) writer.Close();
            }
        }

        /// <summary>
        /// Publish the opml document to the specified location.
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
        public virtual void Publish(Uri uri, System.Net.WebProxy proxy, string method, System.Net.NetworkCredential networkCredential)
        {
            System.IO.Stream stream = null;
            try
            {
                // TODO: webproxy support
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.Credentials = networkCredential;
                stream = wc.OpenWrite(uri.AbsoluteUri, method);
                Save(stream);
            }
            finally
            {
                if (stream != null) stream.Close();
            }
        }

        /// <summary>
        /// Transforms the XML data using XSLT stylesheet.
        /// </summary>
        /// <param name="styleSheet">An XmlReader object that contains the XSLT stylesheet.</param>
        /// <returns>An MemoryStream containing the results of the transform.</returns>
        public virtual System.IO.MemoryStream Transform(System.Xml.XmlReader styleSheet)
        {
            System.IO.MemoryStream xmlStream = null;
            System.IO.MemoryStream xsltStream = null;
            try
            {
                xmlStream = new System.IO.MemoryStream();
                // get xml content
                Save(xmlStream);
                xmlStream.Seek(0, System.IO.SeekOrigin.Begin);
                // transform
                xsltStream = Transform(styleSheet, xmlStream);
            }
            finally
            {
                if (xmlStream != null) xmlStream.Close();
            }
            return xsltStream;

        }

        /// <summary>
        /// Transforms the XML data using XSLT stylesheet to an output file (html)
        /// </summary>
        /// <param name="styleSheet">An XmlReader object that contains the XSLT stylesheet.</param>
        /// <param name="fileNameHtmlOutput">Filename of the html output file.</param>
        public virtual void Transform(System.Xml.XmlReader styleSheet, string fileNameHtmlOutput)
        {
            Transform(styleSheet, null, fileNameHtmlOutput);
        }

        /// <summary>
        /// Transforms the XML data using XSLT stylesheet to an output file (xml, html)
        /// </summary>
        /// <param name="styleSheet">An XmlReader object that contains the XSLT stylesheet.</param>
        /// <param name="fileNameXmlOutput">Filename of the xml output file.</param>
        /// <param name="fileNameHtmlOutput">Filename of the html output file.</param>
        public virtual void Transform(System.Xml.XmlReader styleSheet, string fileNameXmlOutput, string fileNameHtmlOutput)
        {
            lock (this)
            {
                System.IO.FileStream xmlFileStream = null;
                System.IO.FileStream htmlFileStream = null;
                //
                try
                {
                    using (System.IO.MemoryStream xmlStream = new System.IO.MemoryStream())
                    {
                        // get xml stream
                        this.Save(xmlStream);
                        xmlStream.Seek(0, System.IO.SeekOrigin.Begin);
                        // write xml stream to disk (xml)
                        if (fileNameXmlOutput != null)
                        {
                            xmlFileStream = System.IO.File.Create(fileNameXmlOutput);
                            xmlStream.WriteTo(xmlFileStream);
                            xmlFileStream.Close();

                        }
                        // transform xml stream and write to disk (html)
                        if (fileNameHtmlOutput != null)
                        {
                            htmlFileStream = System.IO.File.Create(fileNameHtmlOutput);
                            using (System.IO.MemoryStream htmlStream = Transform(styleSheet, xmlStream))
                            {
                                htmlStream.WriteTo(htmlFileStream);
                                htmlStream.Close();
                                htmlFileStream.Close();
                            }
                        }
                        xmlStream.Close();
                    }
                }
                finally
                {
                    if (htmlFileStream != null) htmlFileStream.Close();
                    if (xmlFileStream != null) xmlFileStream.Close();
                }
            }
        }

        /// <summary>
        /// Transforms the XML stream using XSLT stylesheet.
        /// </summary>
        /// <param name="styleSheet">An XmlReader object that contains the XSLT stylesheet.</param>
        /// <param name="xmlStream">The data to transform</param>
        /// <returns>An MemoryStream containing the results of the transform.</returns>
        /// <example>
        /// This sample shows how to consume and transform (XSLT/CSS) the code project feed.
        /// <code>
        /// // consume rss feed
        /// RssChannel myChannel = new RssChannel(new Uri("http://www.codeproject.com/webservices/articlerss.aspx?cat=3"));
        /// // transform to stream
        ///	System.IO.MemoryStream memoryStream = myChannel.Transform(new System.Xml.XmlTextReader("transform.xslt"));
        ///	// transform to html output file
        ///	myChannel.Transform(new System.Xml.XmlTextReader("transform.xslt"), "myChannel.htm");
        ///	// transform to html and xml output file
        ///	myChannel.Transform(new System.Xml.XmlTextReader("transform.xslt"), "channel.xml", "channel.htm");
        ///	</code>
        /// </example>
        public virtual System.IO.MemoryStream Transform(System.Xml.XmlReader styleSheet, System.IO.Stream xmlStream)
        {
            // stream for transformed content
            System.IO.MemoryStream xsltStream = new System.IO.MemoryStream();
            //
            try
            {
                //Create a new XslTransform object.
                System.Xml.Xsl.XslCompiledTransform xslt = new System.Xml.Xsl.XslCompiledTransform();
                // Load the stylesheet.
                xslt.Load(styleSheet);
                // Create a new XPathDocument and load the XML data to be transformed.
                System.Xml.XmlReader reader = System.Xml.XmlReader.Create(xmlStream, new System.Xml.XmlReaderSettings() { CloseInput = true });
                // Transform the data
                xslt.Transform(reader, new System.Xml.Xsl.XsltArgumentList(), xsltStream);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (xmlStream != null) xmlStream.Close();
            }
            //
            return xsltStream;
        }

        /// <summary>
        /// Gets detailed information about all exceptions occured during parsing against the used schema (Only with XmlValidationReader)
        /// </summary>
        [System.ComponentModel.Category("Validation"), System.ComponentModel.Description("Gets detailed information about all exceptions occured during parsing against the used schema (Only with XmlValidationReader)")]
        [System.Xml.Serialization.XmlIgnore]
        public System.Collections.IList XmlSchemaExceptionList
        {
            get
            {
                return System.Collections.ArrayList.ReadOnly(this._ValidationEventArgsList);
            }
        }

        /// <summary>
        /// Internal property used to generate readonly attribute version
        /// </summary>
        [System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [XmlAttribute("version")]
        public virtual string Version
        {
            get
            {
                return "1.0";
            }

            set
            {
                // do nothing
            }
        }

        /// <summary>A head contains zero or more optional elements</summary>
        [System.ComponentModel.Category("Required elements"), System.ComponentModel.Description("A head contains zero or more optional elements")]
        [System.Xml.Serialization.XmlElementAttribute("head")]
        public virtual OpmlHead Head
        {
            get
            {
                return _head;
            }

            set
            {
                bool changed = !object.Equals(_head, value);
                if (changed && _head != null)
                {
                    _head.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
                    _head.SetDocument(null);
                }
                _head = value;
                if (changed)
                {
                    OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Head));
                    if (_head != null)
                    {
                        _head.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
                        _head.SetDocument(this);
                    }
                }
            }
        }

        // end Head

        IOpmlHead IOpmlDocument.Head
        {
            get
            {
                return this.Head;
            }

            set
            {
                this.Head = value as OpmlHead;
            }
        }

        // end Head

        /// <summary>A body contains one or more outline elements</summary>
        [System.ComponentModel.Category("Required elements"), System.ComponentModel.Description("A body contains one or more outline elements")]
        [System.Xml.Serialization.XmlElementAttribute("body")]
        public virtual OpmlBody Body
        {
            get
            {
                return _body;
            }

            set
            {
                bool changed = !object.Equals(_body, value);
                if (changed && _body != null)
                {
                    _body.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
                    _body.SetDocument(null);
                }
                _body = value;
                if (changed)
                {
                    OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Body));
                    if (_body != null)
                    {
                        _body.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
                        _body.SetDocument(this);
                    }
                }
            }
        }

        // end Body

        IOpmlBody IOpmlDocument.Body
        {
            get
            {
                return this.Body;
            }

            set
            {
                this.Body = value as OpmlBody;
            }
        }

        /// <summary>
        /// Obtains the String representation of this instance. 
        /// </summary>
        /// <returns>The friendly name</returns>
        public override string ToString()
        {
            return Head.Title;
        }

        #endregion

        #region protected interface
        /// <summary>
        /// Parse the data from specified Uri into a document.
        /// </summary>
        /// <param name="document">The document instance to store the gained data in.</param>
        /// <param name="xmlTextReader">XmlTextReader instance</param>
        protected virtual void Parse(OpmlDocument document, System.Xml.XmlReader xmlTextReader)
        {
            try
            {
                System.Diagnostics.Debug.Assert(xmlTextReader != null);
                //
                xmlTextReader.MoveToContent();
                if (xmlTextReader.Name != "opml") throw new FormatException(xmlTextReader.BaseURI + " is no valid Opml File");
                // read the stream forward while not end of file		
                int currentDepth = -1;
                System.Collections.Hashtable nodeLevels = new System.Collections.Hashtable();
                //
                while (!xmlTextReader.EOF)
                {
                    // process head
                    if (xmlTextReader.Name == "head" && xmlTextReader.NodeType == XmlNodeType.Element)
                    {
                        document.Head = new OpmlHead(xmlTextReader);
                    }
                    // process outline and child outlines
                    else if (xmlTextReader.Name == "outline" && xmlTextReader.NodeType == XmlNodeType.Element)
                    {
                        currentDepth = xmlTextReader.Depth;
                        //
                        OpmlOutline o = OnCreateOutline(xmlTextReader);
                        if (currentDepth == 2)
                        {
                            document.Body.Items.Add(o);
                            // new node
                            nodeLevels.Clear();
                        }
                        else
                        {
                            ((OpmlOutline)nodeLevels[xmlTextReader.Depth - 1]).Items.Add(o);
                        }
                        nodeLevels[xmlTextReader.Depth] = o;
                    }
                    else
                    {
                        xmlTextReader.Read();
                        xmlTextReader.MoveToContent();
                    }
                }
            }
            finally
            {
                if (xmlTextReader != null) xmlTextReader.Close();
            }
        }

        ///<summary>A PropertyChanged event is raised when a property is changed on a component. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
        protected virtual void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                // update modified date
                this.Head._dateModified = DateTime.Now;
                // fire event
                PropertyChanged(this, e);
            }
        }

        protected virtual OpmlOutline OnCreateOutline(System.Xml.XmlReader xmlTextReader)
        {
            return new OpmlOutline(xmlTextReader);
        }

        #endregion

        #region events

        /// <summary>
        /// Collects detailed information related to the XmlValidatingReader 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnValidation(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Severity + Environment.NewLine + e.Message);
        }

        ///<summary>A PropertyChanged event is raised when a sub property is changed. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
        protected internal virtual void OnSubItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) PropertyChanged(sender, e);
        }

        #endregion

        #region nested classes

        /// <summary>
        /// public writeable class properties
        /// </summary>		
        internal struct Fields
        {
            public const string Head = "Head";
            public const string Body = "Body";
        }

        #endregion
    }

    /// <summary>
    /// Factory for OpmlDocument's
    /// </summary>
    public class OpmlDocumentFactory
    : OpmlFactory
    {
        #region fields

        private System.Net.WebProxy _webProxy;

        #endregion

        #region public interface

        public override IOpmlDocument GetDocument(Uri uri)
        {
            return new OpmlDocument(uri, _webProxy);
        }

        public System.Net.WebProxy Proxy
        {
            get
            {
                return _webProxy;
            }

            set
            {
                _webProxy = value;
            }
        }

        #endregion
    }
}