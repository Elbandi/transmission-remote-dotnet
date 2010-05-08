
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
	/// <summary>Enclosure is an optional sub-element of item.</summary>
	[System.Runtime.InteropServices.ComVisible(true), System.Runtime.InteropServices.Guid("026FF52F-96DF-4879-A355-880832C49A1C")]
	[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
	[System.Runtime.InteropServices.ProgId("Raccoom.RssEnclosure")]
	[System.Xml.Serialization.XmlTypeAttribute("enclosure")]
	[Serializable]
	[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
	public class RssEnclosure
	:	IRssEnclosure
	{
		#region fields
		
		///<summary>A PropertyChanged event is raised when a property is changed on a component. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		/// <summary>Url</summary>
		private string _url;
		/// <summary>Length</summary>
		private int _length;
		/// <summary>Type</summary>
		private string _type;
		
		#endregion
		
		#region constructors
		
		/// <summary>Initializes a new instance of RssEnclosure with default values</summary>
		public RssEnclosure ()
		{
		
		}
		
		/// <summary>Initializes a new instance of RssEnclosure</summary>
		public RssEnclosure (System.Xml.XmlReader xmlTextReader)
		{
			System.Diagnostics.Debug.Assert(xmlTextReader.HasAttributes);						
			//
			while(xmlTextReader.MoveToNextAttribute())			
			{				
				XmlSerializationUtil.DecodeXmlTextReaderValue(this, xmlTextReader);							
			}
		}
		
		#endregion
		
		#region public interface
		
		/// <summary> The url must be an http url.</summary>
		[System.ComponentModel.Category("RssEnclosure"), System.ComponentModel.Description(" The url must be an http url.")]
		[System.Xml.Serialization.XmlAttribute("url", DataType="anyURI")]
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
		
		/// <summary>length says how big it is in bytes,</summary>
		[System.ComponentModel.Category("RssEnclosure"), System.ComponentModel.Description("length says how big it is in bytes,")]
		[System.Xml.Serialization.XmlAttribute("length")]
		public int Length
		{
			get
			{
				return _length;
			}
			
			set
			{
				bool changed = !object.Equals(_length, value);
				_length = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Length));
			}
		}
		
		// end Length
		
		/// <summary>
		/// Instructs the XmlSerializer whether or not to generate the XML element
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public bool LengthSpecified
		{
			get
			{
				return _length>0;
			}
			
			set
			{
			
			}
		}
		
		/// <summary>type says what its type is, a standard MIME type.</summary>
		[System.ComponentModel.Category("RssEnclosure"), System.ComponentModel.Description("type says what its type is, a standard MIME type.")]
		[System.Xml.Serialization.XmlAttribute("type")]
		public string Type
		{
			get
			{
				return _type;
			}
			
			set
			{
				bool changed = !object.Equals(_type, value);
				_type = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Type));
			}
		}
		
		// end Type
		
		/// <summary>
		/// Obtains the String representation of this instance. 
		/// </summary>
		/// <returns>The friendly name</returns>
		public override string ToString ()
		{
			return this.Url;
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
			public const string Url = "Url";
			public const string Length = "Length";
			public const string Type = "Type";
			public const string Path = "Path";
		}
		
		#endregion
	}
}