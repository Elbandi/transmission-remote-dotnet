
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
    /// <seealso cref="IRssChannel"/> strong typed collecton.
    /// </summary>
    [Serializable]
    public class RssChannelCollection
    : System.Collections.CollectionBase
    {
        /// <summary>Adds an item to the IRssChannelCollection.</summary>
        public int Add(RssChannel value)
        {
            return base.List.Add(value as object);
        }

        /// <summary>Removes an item to the RssChannelCollection.</summary>
        public void Remove(RssChannel value)
        {
            base.List.Remove(value as object);
        }

        /// <summary>Inserts an IRssChannel to the RssChannelCollection at the specified position.</summary>
        public void Insert(int index, RssChannel value)
        {
            base.List.Insert(index, value as object);
        }

        /// <summary>Determines whether the RssChannelCollection contains a specific value.</summary>
        public bool Contains(RssChannel value)
        {
            return base.List.Contains(value as object);
        }

        /// <summary>Gets the IRssChannel at the specified index.</summary>
        public RssChannel this[int index]
        {
            get
            {
                return (base.List[index] as RssChannel);
            }
        }

        /// <summary>Determines the index of a specific item i</summary>
        public int IndexOf(IRssChannel value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>Copies the elements of the Collection to an Array, starting at a particular Array index.</summary>
        public void CopyTo(Array array, int index)
        {
            List.CopyTo(array, index);
        }
    }

    /// <summary>
    /// RSS Channel element which contains information about the channel (metadata) and its contents.
    /// </summary>
    /// <remarks><a href="http://blogs.law.harvard.edu/tech/rss" target="_blank">RSS 2.0 Specification</a></remarks>		
    /// <example>
    /// <h4>Consume feeds</h4>
    /// This sample shows how to consume the code project "Last 10 updates (category: All Topics)" rss feed.
    /// <code>
    /// Raccoom.Xml.RssChannel myRssChannel = new Raccoom.Xml.RssChannel(new Uri("http://www.codeproject.com/webservices/articlerss.aspx?cat=1"));
    /// // write the channel title to the standard output stream. 
    /// System.Console.WriteLine(myRssChannel.Title);
    /// // write each item's title to the standard output stream. 
    /// foreach(Raccoom.Xml.RssItem item in myRssChannel.Items)
    /// {
    /// 	System.Console.WriteLine(item.Title);
    /// }
    /// </code>
    /// This sample shows how to create rss feeds
    /// <code>
    /// Raccoom.Xml.RssChannel myRssChannel = new Raccoom.Xml.RssChannel();
    /// myRssChannel.Title = "Sample rss feed";
    /// myRssChannel.Copyright = "(c) 2003 by Christoph Richner";
    /// // add item to channel
    /// Raccoom.Xml.RssItem item = new Raccoom.Xml.RssItem();
    /// item.Title = "Raccoom RSS 2.0 Framework announced";
    /// item.Link = "http://jerrymaguire.sytes.net";
    /// myRssChannel.Items.Add(item);
    /// </code>
    /// <h4>Save feeds</h4>
    /// This example saves the channel to a file
    /// <code>
    /// // save feed to local storage
    /// myRssChannel.Save(@"c:\cp.xml");
    /// </code>
    /// This example saves the channel to <c>System.IO.Stream</c>.
    /// <code>
    /// // create stream
    /// System.IO.MemoryStream stream = new System.IO.MemoryStream();
    /// myRssChannel.Write(stream);
    /// stream.Close();
    /// </code>	
    /// This sample shows how to publish your feed (Default Proxy)
    /// <code>
    /// // password-based authentication for web resource
    /// System.Net.NetworkCredential providerCredential = new System.Net.NetworkCredential("username", "password", "domain");
    /// // use default system proxy
    /// Uri uri = new Uri("http://domain.net");
    /// myChannel.Publish(uri, null, "POST", providerCredential);
    /// </code>
    /// This sample shows how to publish your feed (Custom Proxy)
    /// <code>
    /// // password-based authentication for web resource
    /// System.Net.NetworkCredential providerCredential = new System.Net.NetworkCredential("username", "password", "domain");
    /// // password-based authentication for web proxy
    /// System.Net.NetworkCredential proxyCredential = new System.Net.NetworkCredential("username", "password", "domain");
    /// // create custom proxy
    /// System.Net.WebProxy webProxy = new System.Net.WebProxy("http://proxyurl:8080",false);
    /// webProxy.Credentials = proxyCredential;
    /// // publish
    /// myChannel.Publish(uri, webProxy, "POST", providerCredential);
    /// </code>
    /// <h4>Transform feeds</h4>
    /// This sample shows how to consume and transform (XSLT/CSS) the code project feed, where transform.xslt is a custom xslt file.
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
    [System.Runtime.InteropServices.ComVisible(true), System.Runtime.InteropServices.Guid("026FF54F-98DF-4879-A355-880832C49A1C")]
    [System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
    [System.Runtime.InteropServices.ProgId("Raccoom.RssChannel")]
    [System.Xml.Serialization.XmlRoot()]
    [System.Xml.Serialization.XmlTypeAttribute("channel")]
    [Serializable]
    [System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public class RssChannel
    : IRssChannel
    {
        #region fields

        private string _version;

        ///<summary>A PropertyChanged event is raised when a property is changed on a component. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>Title</summary>
        private string _title;
        /// <summary>Description</summary>
        private string _description;
        /// <summary>Link</summary>
        private string _link;
        /// <summary>Language</summary>
        private System.Globalization.CultureInfo _language;
        /// <summary>Copyright</summary>
        private string _copyright;
        /// <summary>ManagingEditor</summary>
        private string _managingEditor;
        /// <summary>WebMaster</summary>
        private string _webMaster;
        /// <summary>PubDate</summary>
        private DateTime _pubDate;
        /// <summary>LastBuildDate</summary>
        private DateTime _lastBuildDate;
        /// <summary>Category</summary>
        private string _category;
        /// <summary>Generator</summary>
        private string _generator;
        /// <summary>Docs</summary>
        private string _docs;
        /// <summary>Cloud</summary>
        private RssCloud _cloud;
        /// <summary>TTL</summary>
        private int _tTL;
        /// <summary>Image</summary>
        private RssImage _image;
        /// <summary>Rating</summary>
        private string _rating;
        /// <summary>TextInput</summary>
        private RssTextInput _textInput;
        /// <summary>SkipHours</summary>
        private int[] _skipHours = new int[0];
        /// <summary>SkipDays</summary>
        private SkipDays _skipDays;
        /// <summary>Items</summary>
        private RssItemCollection _items;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the RssChannel class and set default values.
        /// </summary>		
        public RssChannel()
        {
            Cloud = new RssCloud();
            Image = new RssImage();
            TextInput = new RssTextInput();
            _items = new RssItemCollection(this);
            SetVersion("2.0");
            //			
            this.PubDate = DateTime.Now;
            this.LastBuildDate = PubDate;
            this.Language = System.Globalization.CultureInfo.CurrentCulture;
        }

        /// <summary>
        /// Initializes a new instance of the RssChannel class with the specified url.
        /// </summary>
        /// <param name="uri">The URI of the resource to receive the data. </param>
        public RssChannel(Uri uri)
            : this(uri, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of RssChannel from <c>Uri</c> using the specified <c>WebProxy</c>.
        /// </summary>
        /// <param name="uri">The URI of the resource to receive the data.</param>
        /// <param name="proxy">Network proxy that the request uses to access the Internet resource</param>
        public RssChannel(Uri uri, System.Net.WebProxy proxy)
            : this()
        {
            System.Net.WebRequest webRequest = System.Net.HttpWebRequest.Create(uri);
            if (proxy != null)
            {
                webRequest.Proxy = proxy;
            }
            using (System.Net.WebResponse webResponse = webRequest.GetResponse())
            {
                using (System.IO.Stream stream = webResponse.GetResponseStream())
                {
                    this.Parse(new System.Xml.XmlTextReader(stream));
                    stream.Close();
                }
                webResponse.Close();
            }
        }

        /// <summary>
        /// Initializes a new instance of RssChannel from a stream.
        /// </summary>
        /// <param name="stream">The stream containing the XML data to read.</param>
        public RssChannel(System.IO.Stream stream)
            : this()
        {
            this.Parse(new System.Xml.XmlTextReader(stream));
        }

        /// <summary>
        /// Initializes a new instance of RssChannel from <c>XmlTextReader</c>
        /// </summary>
        /// <param name="xmlReader">The <c>XmlTextReader</c> containing the XML data to read.</param>
        public RssChannel(System.Xml.XmlTextReader xmlReader)
            : this()
        {
            this.Parse(xmlReader);
        }

        #endregion

        #region public interface

        /// <summary>
        /// Gets the version if the version was specified
        /// </summary>
        [System.ComponentModel.Browsable(false), System.Xml.Serialization.XmlIgnore()]
        public string Version
        {
            get
            {
                return _version;
            }
        }

        private void SetVersion(string value)
        {
            _version = value;
        }

        /// <summary>
        /// Publish the rss channel to the specified location.
        /// </summary>
        /// <param name="uri">The URI of the resource to receive the data. </param>
        /// <param name="method">The method used to send the data to the resource. (POST)</param>
        /// <param name="proxy">HTTP proxy settings for the WebRequest class.</param>
        /// <param name="networkCredential">Credentials for password-based authentication schemes such as basic, digest, NTLM, and Kerberos authentication.</param>
        /// <example>
        /// This sample shows how to publish your feed (Default Proxy)
        /// <code>
        /// // password-based authentication for web resource
        /// System.Net.NetworkCredential providerCredential = new System.Net.NetworkCredential("username", "password", "domain");
        /// // use default system proxy
        /// Uri uri = new Uri("http://domain.net");
        /// Publish(uri, null, "POST", providerCredential);
        /// </code>
        /// This sample shows how to publish your feed (Custom Proxy)
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
        public void Publish(Uri uri, System.Net.WebProxy proxy, string method, System.Net.NetworkCredential networkCredential)
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
        /// Saves the rss channel to a local file.
        /// </summary>        
        public void Save(string filename)
        {
            Save(filename, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// Saves the rss channel to a local file.
        /// </summary>        
        public void Save(string filename, System.Text.Encoding encoding)
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
        /// Write the rss channel to the specified stream.
        /// </summary>
        /// <param name="stream">The Stream used to write the XML document.</param>        
        public void Save(System.IO.Stream stream)
        {
            Save(stream, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// Write the rss channel to the specified stream.
        /// </summary>
        /// <param name="stream">The Stream used to write the XML document.</param>
        /// <param name="encoding">The encoding used by the underlying xml text writer</param>
        public void Save(System.IO.Stream stream, System.Text.Encoding encoding)
        {
            XmlTextWriter writer = null;
            try
            {
                this._generator = "Created by Raccoom.Xml RSS Framework classes Version " + this.GetType().Assembly.GetName().Version.ToString() + ", Copyright © 2004 by Christoph Richner. All rights reserved. Website http://raccoom.sytes.net, Email chrisdarebell@msn.com";
                writer = new XmlTextWriter(stream, encoding);
                //Use indenting for readability.
                writer.Formatting = Formatting.Indented;
                //Write the XML delcaration. 
                writer.WriteStartDocument();
                writer.WriteComment(this._generator);
                writer.WriteStartElement("rss");
                writer.WriteAttributeString(null, "version", null, "2.0");
                // serialize the content
                System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(this.GetType());
                ser.Serialize(writer, this);
                //
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                //if(writer!=null) writer.Close();
            }
        }

        /// <summary>
        /// Transforms the XML data using XSLT stylesheet.
        /// </summary>
        /// <param name="styleSheet">An XmlReader object that contains the XSLT stylesheet.</param>
        /// <returns>An MemoryStream containing the results of the transform.</returns>
        public System.IO.MemoryStream Transform(System.Xml.XmlReader styleSheet)
        {
            System.IO.MemoryStream xmlStream = null;
            System.IO.MemoryStream xsltStream = null;
            try
            {
                xmlStream = new System.IO.MemoryStream();
                // get xml content
                Save(xmlStream);
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
        public void Transform(System.Xml.XmlReader styleSheet, string fileNameHtmlOutput)
        {
            Transform(styleSheet, null, fileNameHtmlOutput);
        }

        /// <summary>
        /// Transforms the XML data using XSLT stylesheet to an output file (xml, html)
        /// </summary>
        /// <param name="styleSheet">An XmlReader object that contains the XSLT stylesheet.</param>
        /// <param name="fileNameXmlOutput">Filename of the xml output file.</param>
        /// <param name="fileNameHtmlOutput">Filename of the html output file.</param>
        public void Transform(System.Xml.XmlReader styleSheet, string fileNameXmlOutput, string fileNameHtmlOutput)
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
                            htmlFileStream.Close();
                        }
                    }
                }
            }
            finally
            {
                if (htmlFileStream != null) htmlFileStream.Close();
                if (xmlFileStream != null) xmlFileStream.Close();
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
        public System.IO.MemoryStream Transform(System.Xml.XmlReader styleSheet, System.IO.Stream xmlStream)
        {
            xmlStream.Seek(0, System.IO.SeekOrigin.Begin);
            // stream for transformed content
            System.IO.MemoryStream xsltStream = new System.IO.MemoryStream();
            using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(xmlStream, new XmlReaderSettings() { CloseInput = true }))
            {
                //Create a new XslTransform object.
                System.Xml.Xsl.XslCompiledTransform xslt = new System.Xml.Xsl.XslCompiledTransform();
                // Load the stylesheet.
                xslt.Load(styleSheet);
                // Create a new XPathDocument and load the XML data to be transformed.

                // Transform the data
                xslt.Transform(reader, new System.Xml.Xsl.XsltArgumentList(), xsltStream);
            }
            //
            return xsltStream;
        }

        /// <summary>The name of the channel. It's how people refer to your service. If you have an HTML website that contains the same information as your RSS file, the title of your channel should be the same as the title of your website.</summary>
        [System.ComponentModel.Category("Required channel elements"), System.ComponentModel.Description("The name of the channel. It's how people refer to your service. If you have an HTML website that contains the same information as your RSS file, the title of your channel should be the same as the title of your website.")]
        [System.Xml.Serialization.XmlElementAttribute("title")]
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                bool changed = !object.Equals(_title, value);
                _title = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Title));
            }
        }

        // end Title

        /// <summary>Phrase or sentence describing the channel.</summary>
        [System.ComponentModel.Category("Required channel elements"), System.ComponentModel.Description("Phrase or sentence describing the channel.")]
        [System.Xml.Serialization.XmlElementAttribute("description")]
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                bool changed = !object.Equals(_description, value);
                _description = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Description));
            }
        }

        // end Description

        /// <summary>The URL to the HTML website corresponding to the channel.</summary>
        [System.ComponentModel.Category("Required channel elements"), System.ComponentModel.Description("The URL to the HTML website corresponding to the channel.")]
        [System.Xml.Serialization.XmlElementAttribute("link", DataType = "anyURI")]
        public string Link
        {
            get
            {
                return _link;
            }

            set
            {
                bool changed = !object.Equals(_link, value);
                _link = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Link));
            }
        }

        // end Link

        /// <summary>The language the channel is written in. This allows aggregators to group all Italian language sites, for example, on a single page. A list of allowable values for this element, as provided by Netscape, is here. You may also use values defined by the W3C.</summary>
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("The language the channel is written in. This allows aggregators to group all Italian language sites, for example, on a single page. A list of allowable values for this element, as provided by Netscape, is here. You may also use values defined by the W3C.")]
        [System.Xml.Serialization.XmlIgnore]
        public System.Globalization.CultureInfo Language
        {
            get
            {
                return _language;
            }

            set
            {
                bool changed = !object.Equals(_language, value);
                _language = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Language));
            }
        }

        // end Language

        /// <summary>
        /// Internal, gets the CultureInfo ISO Code
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("language", DataType = "language")]
        [System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public string LanguageIso
        {
            get
            {
                return this.Language.ToString();
            }

            set
            {

            }
        }

        /// <summary>Copyright notice for content in the channel.</summary>
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("Copyright notice for content in the channel.")]
        [System.Xml.Serialization.XmlElementAttribute("copyright")]
        public string Copyright
        {
            get
            {
                return _copyright;
            }

            set
            {
                bool changed = !object.Equals(_copyright, value);
                _copyright = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Copyright));
            }
        }

        // end Copyright

        /// <summary> Email address for person responsible for editorial content.</summary>
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description(" Email address for person responsible for editorial content.")]
        [System.Xml.Serialization.XmlElementAttribute("managingEditor")]
        public string ManagingEditor
        {
            get
            {
                return _managingEditor;
            }

            set
            {
                bool changed = !object.Equals(_managingEditor, value);
                _managingEditor = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.ManagingEditor));
            }
        }

        // end ManagingEditor

        /// <summary>Email address for person responsible for technical issues relating to channel.</summary>
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("Email address for person responsible for technical issues relating to channel.")]
        [System.Xml.Serialization.XmlElementAttribute("webMaster")]
        public string WebMaster
        {
            get
            {
                return _webMaster;
            }

            set
            {
                bool changed = !object.Equals(_webMaster, value);
                _webMaster = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.WebMaster));
            }
        }

        // end WebMaster

        /// <summary>The publication date for the content in the channel. For example, the New York Times publishes on a daily basis, the publication date flips once every 24 hours. That's when the pubDate of the channel changes. All date-times in RSS conform to the Date and Time Specification of RFC 822, with the exception that the year may be expressed with two characters or four characters (four preferred). </summary>
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("The publication date for the content in the channel. For example, the New York Times publishes on a daily basis, the publication date flips once every 24 hours. That's when the pubDate of the channel changes. All date-times in RSS conform to the Date and Time Specification of RFC 822, with the exception that the year may be expressed with two characters or four characters (four preferred). ")]
        [System.Xml.Serialization.XmlIgnore]
        public DateTime PubDate
        {
            get
            {
                return _pubDate;
            }

            set
            {
                bool changed = !object.Equals(_pubDate, value);
                _pubDate = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.PubDate));
            }
        }

        // end PubDate

        /// <summary>
        /// Internal, gets the DateTime in RFC822 format
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Xml.Serialization.XmlElementAttribute("pubDate")]
        public string PubDateRfc
        {
            get
            {
                return this.PubDate.ToUniversalTime().ToString("r");
            }

            set
            {

            }
        }

        /// <summary>The last time the content of the channel changed.</summary>
        /// <remarks>LastBuildDate is updated automatically every time the PropertyChanged event is fired.</remarks>
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("The last time the content of the channel changed."), System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public DateTime LastBuildDate
        {
            get
            {
                return _lastBuildDate;
            }

            set
            {
                bool changed = !object.Equals(_lastBuildDate, value);
                _lastBuildDate = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.LastBuildDate));
            }
        }

        // end LastBuildDate

        /// <summary>
        /// Internal, gets the DateTime in RFC822 format
        /// </summary>				
        [System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Xml.Serialization.XmlElementAttribute("lastBuildDate")]
        public string LastBuildDateRfc
        {
            get
            {
                return this.LastBuildDate.ToUniversalTime().ToString("r");
            }

            set
            {

            }
        }

        /// <summary>Specify one or more categories that the channel belongs to. Follows the same rules as the item-level category element.</summary>
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("Specify one or more categories that the channel belongs to. Follows the same rules as the item-level category element.")]
        [System.Xml.Serialization.XmlElementAttribute("category")]
        public string Category
        {
            get
            {
                return _category;
            }

            set
            {
                bool changed = !object.Equals(_category, value);
                _category = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Category));
            }
        }

        // end Category

        /// <summary>A string indicating the program used to generate the channel.</summary>
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("A string indicating the program used to generate the channel.")]
        [System.Xml.Serialization.XmlElementAttribute("generator"), System.ComponentModel.Browsable(false)]
        public string Generator
        {
            get
            {
                return _generator;
            }

            set
            {
                bool changed = !object.Equals(_generator, value);
                _generator = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Generator));
            }
        }

        // end Generator

        /// <summary>A URL that points to the documentation for the format used in the RSS file. It's probably a pointer to this page. It's for people who might stumble across an RSS file on a Web server 25 years from now and wonder what it is.</summary>
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("A URL that points to the documentation for the format used in the RSS file. It's probably a pointer to this page. It's for people who might stumble across an RSS file on a Web server 25 years from now and wonder what it is.")]
        [System.Xml.Serialization.XmlElementAttribute("docs")]
        public string Docs
        {
            get
            {
                return _docs;
            }

            set
            {
                bool changed = !object.Equals(_docs, value);
                _docs = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Docs));
            }
        }

        // end Docs

        /// <summary>Allows processes to register with a cloud to be notified of updates to the channel, implementing a lightweight publish-subscribe protocol for RSS feeds. </summary>
        [System.Xml.Serialization.XmlElementAttribute("cloud")]
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("Allows processes to register with a cloud to be notified of updates to the channel, implementing a lightweight publish-subscribe protocol for RSS feeds. ")]
        public RssCloud Cloud
        {
            get
            {
                return _cloud;
            }

            set
            {
                bool changed = !object.Equals(_cloud, value);
                if (changed && _cloud != null) _cloud.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
                _cloud = value;
                if (changed)
                {
                    OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Cloud));
                    if (_cloud != null) _cloud.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
                }
            }
        }

        // end Cloud

        /// <summary>Allows processes to register with a cloud to be notified of updates to the channel, implementing a lightweight publish-subscribe protocol for RSS feeds. </summary>
        [System.Xml.Serialization.XmlElementAttribute("cloud")]
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("Allows processes to register with a cloud to be notified of updates to the channel, implementing a lightweight publish-subscribe protocol for RSS feeds. ")]
        IRssCloud IRssChannel.Cloud
        {
            get
            {
                return this.Cloud;
            }

            set
            {
                this.Cloud = (RssCloud)value;
            }
        }

        // end Cloud

        /// <summary>
        /// Instructs the XmlSerializer whether or not to generate the XML element
        /// </summary>
        [System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool CloudSpecified
        {
            get
            {
                return _cloud.PortSpecified || _cloud.ProtocolSpecified || (_cloud.RegisterProcedure != null && _cloud.RegisterProcedure.Length > 0) || (_cloud.Domain != null && _cloud.Domain.Length > 0) || (_cloud.Path != null && _cloud.Path.Length > 0);
            }

            set
            {

            }
        }

        /// <summary>ttl stands for time to live. It's a number of minutes that indicates how long a channel can be cached before refreshing from the source.</summary>
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("ttl stands for time to live. It's a number of minutes that indicates how long a channel can be cached before refreshing from the source.")]
        [System.Xml.Serialization.XmlElementAttribute("ttl")]
        public int Ttl
        {
            get
            {
                return _tTL;
            }

            set
            {
                bool changed = !object.Equals(_tTL, value);
                _tTL = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Ttl));
            }
        }

        // end TTL

        /// <summary>
        /// Instructs the XmlSerializer whether or not to generate the XML element
        /// </summary>
        [System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool TtlSpecified
        {
            get
            {
                return _tTL > 0;
            }

            set
            {

            }
        }

        /// <summary>Specifies a GIF, JPEG or PNG image that can be displayed with the channel.</summary>
        [System.Xml.Serialization.XmlElementAttribute("image")]
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("Specifies a GIF, JPEG or PNG image that can be displayed with the channel.")]
        public RssImage Image
        {
            get
            {
                return _image;
            }

            set
            {
                bool changed = !object.Equals(_image, value);
                if (changed && _image != null) _image.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
                _image = value;
                if (changed)
                {
                    OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Image));
                    if (_image != null) _image.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
                }
            }
        }

        // end Image

        /// <summary>Specifies a GIF, JPEG or PNG image that can be displayed with the channel.</summary>
        [System.Xml.Serialization.XmlElementAttribute("image")]
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("Specifies a GIF, JPEG or PNG image that can be displayed with the channel.")]
        IRssImage IRssChannel.Image
        {
            get
            {
                return this.Image;
            }

            set
            {
                this.Image = value as RssImage;
            }
        }

        // end Image

        /// <summary>
        /// Instructs the XmlSerializer whether or not to generate the XML element
        /// </summary>
        [System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool ImageSpecified
        {
            get
            {
                return _image.HeightSpecified || _image.WidthSpecified || (_image.Title != null && _image.Title.Length > 0) || (_image.Url != null && _image.Url.Length > 0) || (_image.Link != null && _image.Link.Length > 0);
            }

            set
            {

            }
        }

        /// <summary>The PICS rating for the channel</summary>
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("The PICS rating for the channel")]
        [System.Xml.Serialization.XmlElementAttribute("rating")]
        public string Rating
        {
            get
            {
                return _rating;
            }

            set
            {
                bool changed = !object.Equals(_rating, value);
                _rating = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Rating));
            }
        }

        // end Rating

        /// <summary>Specifies a text input box that can be displayed with the channel.</summary>
        [System.Xml.Serialization.XmlElementAttribute("textInput")]
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("Specifies a text input box that can be displayed with the channel.")]
        public RssTextInput TextInput
        {
            get
            {
                return _textInput;
            }

            set
            {
                bool changed = !object.Equals(_textInput, value);
                if (changed && _textInput != null) _textInput.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
                _textInput = value;
                if (changed)
                {
                    OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.TextInput));
                    if (_textInput != null) _textInput.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
                }
            }
        }

        // end TextInput

        /// <summary>Specifies a text input box that can be displayed with the channel.</summary>
        [System.Xml.Serialization.XmlElementAttribute("textInput")]
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("Specifies a text input box that can be displayed with the channel.")]
        IRssTextInput IRssChannel.TextInput
        {
            get
            {
                return this.TextInput;
            }

            set
            {
                this.TextInput = value as RssTextInput;
            }
        }

        // end TextInput

        /// <summary>
        /// Instructs the XmlSerializer whether or not to generate the XML element
        /// </summary>
        [System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool TextInputSpecified
        {
            get
            {
                return (_textInput.Description != null && _textInput.Description.Length > 0) || (_textInput.Link != null && _textInput.Link.Length > 0) || (_textInput.Name != null && _textInput.Name.Length > 0) || (_textInput.Title != null && _textInput.Title.Length > 0);
            }

            set
            {

            }
        }

        /// <summary>A hint for aggregators telling them which hours they can skip. </summary>
        /// <remarks>
        /// Contains up to 24  sub-elements whose value is a number between 0 and 23, representing a time in GMT, when aggregators, if they support the feature, may not read the channel on hours listed in the skipHours element. The hour beginning at midnight is hour zero.
        /// </remarks>
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("A hint for aggregators telling them which hours they can skip. Contains up to 24  sub-elements whose value is a number between 0 and 23, representing a time in GMT, when aggregators, if they support the feature, may not read the channel on hours listed in the skipHours element. The hour beginning at midnight is hour zero.")]
        [Obsolete]
        [System.Xml.Serialization.XmlArray("skipHours")]
        [System.Xml.Serialization.XmlArrayItemAttribute("hour")]
        public int[] SkipHours
        {
            get
            {
                return _skipHours;
            }

            set
            {
                bool changed = !object.Equals(_skipHours, value);
                _skipHours = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.SkipHours));
            }
        }

        // end SkipHours

        /// <summary>A hint for aggregators telling them which days they can skip.</summary>		
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("A hint for aggregators telling them which days they can skip. ")]
        [System.Xml.Serialization.XmlIgnore()]
        //[System.ComponentModel.Editor(typeof(Raccoom.Windows.Forms.Design.FlagsEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Obsolete]
        public SkipDays SkipDays
        {
            get
            {
                return _skipDays;
            }

            set
            {
                bool changed = !object.Equals(_skipDays, value);
                _skipDays = value;
                if (changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.SkipDays));
            }
        }

        // end SkipDays

        /// <summary>
        /// Internal conversion between <see cref="SkipDays"/> enum and day elements
        /// </summary>
        [System.Xml.Serialization.XmlArray("skipDays")]
        [System.Xml.Serialization.XmlArrayItemAttribute("day", typeof(string))]
        [System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public string[] SkipDaysArray
        {
            get
            {
                if ((int)_skipDays == 0) return null;
                //
                int enumValue = (int)Convert.ChangeType(_skipDays, typeof(int));
                System.Collections.ArrayList list = new System.Collections.ArrayList();
                foreach (int intVal in Enum.GetValues(typeof(SkipDays)))
                {
                    if ((enumValue & intVal) > 0)
                    {
                        list.Add(Enum.GetName(typeof(SkipDays), intVal));
                    }
                }
                string[] days = new string[list.Count];
                list.CopyTo(days);
                return days;
            }

            set
            {

            }
        }

        /// <summary>A channel may contain any number of items. An item may represent a "story" -- much like a story in a newspaper or magazine; if so its description is a synopsis of the story, and the link points to the full story. An item may also be complete in itself, if so, the description contains the text (entity-encoded HTML is allowed), and the link and title may be omitted.</summary>
        [System.Xml.Serialization.XmlElementAttribute("item")]
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("A channel may contain any number of items. An item may represent a \"story\" -- much like a story in a newspaper or magazine; if so its description is a synopsis of the story, and the link points to the full story. An item may also be complete in itself, if so, the description contains the text (entity-encoded HTML is allowed), and the link and title may be omitted.")]
        public RssItemCollection Items
        {
            get
            {
                return _items;
            }
        }

        // end Items

        /// <summary>A channel may contain any number of items. An item may represent a "story" -- much like a story in a newspaper or magazine; if so its description is a synopsis of the story, and the link points to the full story. An item may also be complete in itself, if so, the description contains the text (entity-encoded HTML is allowed), and the link and title may be omitted.</summary>
        [System.Xml.Serialization.XmlElementAttribute("item")]
        [System.ComponentModel.Category("Optional channel elements"), System.ComponentModel.Description("A channel may contain any number of items. An item may represent a \"story\" -- much like a story in a newspaper or magazine; if so its description is a synopsis of the story, and the link points to the full story. An item may also be complete in itself, if so, the description contains the text (entity-encoded HTML is allowed), and the link and title may be omitted.")]
        System.Collections.ICollection IRssChannel.Items
        {
            get
            {
                return _items;
            }
        }

        // end Items

        /// <summary>
        /// Obtains the String representation of this instance. 
        /// </summary>
        /// <returns>The friendly name</returns>
        public override string ToString()
        {
            return Title;
        }

        #endregion

        #region protected interface

        /// <summary>
        /// Parser use XmlTextReader as input for data
        /// </summary>
        protected virtual void Parse(System.Xml.XmlReader xmlTextReader)
        {
            try
            {
                // initalize
                System.Reflection.PropertyInfo propertyInfo = null;
                bool supressRead = false;
                // try to read the rss header
                xmlTextReader.MoveToContent();
                if (xmlTextReader.Name == "rss")
                {
                    this.SetVersion(xmlTextReader.GetAttribute("version"));
                }
                else
                {
                    this.SetVersion("");
                }
                xmlTextReader.Read();
                // process channel
                while (!xmlTextReader.EOF)
                {
                    // if no ReadInnerXml() call was done, read
                    if (!supressRead) xmlTextReader.Read();
                    // Move to content
                    xmlTextReader.MoveToContent();
                    // set default for next loop
                    supressRead = false;
                    //
                    if (xmlTextReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmlTextReader.Name == "item")
                        {
                            if (xmlTextReader.IsEmptyElement) continue;
                            // add new item to item collection
                            this.Items.Add(new RssItem(xmlTextReader));
                        }
                        // image has sub elements
                        else if (xmlTextReader.Name == "image")
                        {
                            if (xmlTextReader.IsEmptyElement) continue;
                            this.Image = new RssImage(xmlTextReader);
                        }
                        // image has sub elements
                        else if (xmlTextReader.Name == "textInput")
                        {
                            if (xmlTextReader.IsEmptyElement) continue;
                            this.TextInput = new RssTextInput(xmlTextReader);
                        }
                        else if (xmlTextReader.Name == "cloud")
                        {
                            if (!xmlTextReader.HasAttributes) continue;
                            this.Cloud = new RssCloud(xmlTextReader);
                        }
                        else if (xmlTextReader.Name == "skipDays")
                        {
                            if (xmlTextReader.IsEmptyElement) continue;
                            // find related property by name
                            propertyInfo = this.GetType().GetProperty(xmlTextReader.Name, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                            if (propertyInfo != null)
                            {
                                // find days and parse SkipDays enum
                                int result = 0;
                                while (!(xmlTextReader.Name == "skipDays" && xmlTextReader.NodeType == XmlNodeType.EndElement))
                                {
                                    xmlTextReader.Read();
                                    if (xmlTextReader.NodeType != XmlNodeType.Element) continue;
                                    //
                                    xmlTextReader.Read();
                                    result += (int)Enum.Parse(typeof(SkipDays), xmlTextReader.Value, true);
                                }
                                // set value
                                propertyInfo.SetValue(this, Enum.ToObject(typeof(SkipDays), result), null);
                            }
                        }
                        else if (xmlTextReader.Name == "skipHours")
                        {
                            if (xmlTextReader.IsEmptyElement) continue;
                            // find related property by name
                            propertyInfo = this.GetType().GetProperty(xmlTextReader.Name, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                            if (propertyInfo != null)
                            {
                                // read all hours elements
                                System.Collections.ArrayList list = new System.Collections.ArrayList();
                                while (!(xmlTextReader.Name == "skipHours" && xmlTextReader.NodeType == XmlNodeType.EndElement))
                                {
                                    xmlTextReader.Read();
                                    if (xmlTextReader.NodeType != XmlNodeType.Element) continue;
                                    //
                                    xmlTextReader.Read();
                                    list.Add(Convert.ToInt32(xmlTextReader.Value));
                                }
                                // set value
                                propertyInfo.SetValue(this, list.ToArray(typeof(int)), null);
                            }
                        }
                        // items is the item collection, skip if specified
                        else if (xmlTextReader.Name.ToLower() == "items")
                        {
                        }
                        // otherwise fill channel element
                        else
                        {
                            supressRead = XmlSerializationUtil.DecodeXmlTextReaderValue(this, xmlTextReader);
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                throw e;
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
                this._lastBuildDate = DateTime.Now;
                PropertyChanged(this, e);
            }
        }

        #endregion

        #region nested classes

        /// <summary>
        /// public writeable class properties
        /// </summary>		
        internal struct Fields
        {
            public const string Title = "Title";
            public const string Description = "Description";
            public const string Link = "Link";
            public const string Language = "Language";
            public const string Copyright = "Copyright";
            public const string ManagingEditor = "ManagingEditor";
            public const string WebMaster = "WebMaster";
            public const string PubDate = "PubDate";
            public const string LastBuildDate = "LastBuildDate";
            public const string Category = "Category";
            public const string Generator = "Generator";
            public const string Docs = "Docs";
            public const string Cloud = "Cloud";
            public const string Ttl = "Ttl";
            public const string Image = "Image";
            public const string Rating = "Rating";
            public const string TextInput = "TextInput";
            public const string SkipHours = "SkipHours";
            public const string SkipDays = "SkipDays";
            public const string Items = "Items";
        }

        #endregion

        #region events

        ///<summary>A PropertyChanged event is raised when a sub property is changed. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
        protected internal virtual void OnSubItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) PropertyChanged(sender, e);
        }

        #endregion
    }
}