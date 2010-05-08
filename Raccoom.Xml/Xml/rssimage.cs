
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
	/// <summary>Image is an optional sub-element of channel, which contains three required and three optional sub-elements.</summary>
	[System.Runtime.InteropServices.ComVisible(true), System.Runtime.InteropServices.Guid("026FF54F-96DF-4879-A355-880832C49A2C")]
	[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
	[System.Runtime.InteropServices.ProgId("Raccoom.RssImage")]
	[Serializable]
	[System.Xml.Serialization.XmlTypeAttribute("image")]
	[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
	public class RssImage
	:	IRssImage
	{
		#region fields
		
		///<summary>A PropertyChanged event is raised when a property is changed on a component. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		/// <summary>Title</summary>
		private string _title;
		/// <summary>Url</summary>
		private string _url;
		/// <summary>Link</summary>
		private string _link;
		/// <summary>Description</summary>
		private string _description;
		/// <summary>Width</summary>
		private int _width;
		/// <summary>Height</summary>
		private int _height;
		
		#endregion
		
		#region constructors
		
		/// <summary>Initializes a new instance with default values</summary>
		public RssImage ()
		{
		
		}
		
		/// <summary>Initializes a new instance</summary>
		public RssImage (System.Xml.XmlReader xmlTextReader)
		{
			if(xmlTextReader.IsEmptyElement) return;
			//						
			bool supressRead = false;
			while(!(xmlTextReader.Name == "image" && xmlTextReader.NodeType == XmlNodeType.EndElement))
			{
				// Continue read
				if(!supressRead) xmlTextReader.Read();
				xmlTextReader.MoveToContent();
				supressRead = false;	
				if( xmlTextReader.NodeType != XmlNodeType.Element) continue;									
				// find related property by name
				supressRead = XmlSerializationUtil.DecodeXmlTextReaderValue(this, xmlTextReader);							
			}
		}
		
		#endregion
		
		#region public interface
		
		/// <summary>Describes the image, it's used in the ALT attribute of the HTML img tag when the channel is rendered in HTML. </summary>
		[System.ComponentModel.Category("RssImage"), System.ComponentModel.Description("Describes the image, it's used in the ALT attribute of the HTML img tag when the channel is rendered in HTML. ")]
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
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Title));
			}
		}
		
		// end Title
		
		/// <summary>The URL of a GIF, JPEG or PNG image. </summary>
		[System.ComponentModel.Category("RssImage"), System.ComponentModel.Description("The URL of a GIF, JPEG or PNG image. ")]
		[System.Xml.Serialization.XmlElementAttribute("url")]
		public string Url
		{
			get
			{
				return _url;
			}
			
			set
			{
				bool changed = !object.Equals(_url, value);
				_url = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Url));
			}
		}
		
		// end Url
		
		/// <summary>the URL of the site, when the channel is rendered, the image is a link to the site.</summary>
		[System.ComponentModel.Category("RssImage"), System.ComponentModel.Description("the URL of the site, when the channel is rendered, the image is a link to the site.")]
		[System.Xml.Serialization.XmlElementAttribute("link")]
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
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Link));
			}
		}
		
		// end Link
		
		/// <summary>Contains text that is included in the TITLE attribute of the link formed around the image in the HTML rendering.</summary>
		[System.ComponentModel.Category("RssImage"), System.ComponentModel.Description("Contains text that is included in the TITLE attribute of the link formed around the image in the HTML rendering.")]
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
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Description));
			}
		}
		
		// end Description
		
		/// <summary>Maximum value for width is 144, default value is 88.</summary>
		[System.ComponentModel.Category("RssImage"), System.ComponentModel.Description("Maximum value for width is 144, default value is 88."),System.ComponentModel.DefaultValue(88)]
		[System.Xml.Serialization.XmlElementAttribute("width")]
		public int Width
		{
			get
			{
				return _width;
			}
			
			set
			{
				bool changed = !object.Equals(_width, value);
				_width = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Width));
			}
		}
		
		// end Width
		
		/// <summary>
		/// Instructs the XmlSerializer whether or not to generate the XML element
		/// </summary>
		[System.Xml.Serialization.XmlIgnoreAttribute]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public bool WidthSpecified
		{
			get
			{
				return _width>0;
			}
			
			set
			{
			
			}
		}
		
		/// <summary>Maximum value for height is 400, default value is 31.</summary>
		[System.ComponentModel.Category("RssImage"), System.ComponentModel.Description("Maximum value for height is 400, default value is 31."),System.ComponentModel.DefaultValue(31)]
		[System.Xml.Serialization.XmlElementAttribute("height")]
		public int Height
		{
			get
			{
				return _height;
			}
			
			set
			{
				bool changed = !object.Equals(_height, value);
				_height = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Height));
			}
		}
		
		// end Height
		
		/// <summary>
		/// Instructs the XmlSerializer whether or not to generate the XML element
		/// </summary>
		[System.Xml.Serialization.XmlIgnoreAttribute]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public bool HeightSpecified
		{
			get
			{
				return _height>0;
			}
			
			set
			{
			
			}
		}
		
		/// <summary>
		/// Obtains the String representation of this instance. 
		/// </summary>
		/// <returns>The friendly name</returns>
		public override string ToString ()
		{
			return this.Description;
		}
		
		#endregion
		
		#region protected interface
		
		///<summary>A PropertyChanged event is raised when a property is changed on a component. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
		protected virtual void OnPropertyChanged (System.ComponentModel.PropertyChangedEventArgs e)
		{
			if(PropertyChanged!=null) PropertyChanged(this, e);			
		}
		
		#endregion
		
		#region nested classes
		
		/// <summary>
		/// public writeable class properties
		/// </summary>		
		internal struct Fields
		{
			public const string Title = "Title";
			public const string Url = "Url";
			public const string Link = "Link";
			public const string Description = "Description";
			public const string Width = "Width";
			public const string Height = "Height";
		}
		
		#endregion
	}
}