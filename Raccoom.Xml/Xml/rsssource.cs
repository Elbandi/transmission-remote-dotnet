
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
	/// <summary>Optional sub-element of item. Its value is the name of the RSS channel that the item came from, derived from its title. It has one required attribute, url, which links to the XMLization of the source.The purpose of this element is to propagate credit for links, to publicize the sources of news items. It can be used in the Post command of an aggregator. It should be generated automatically when forwarding an item from an aggregator to a weblog authoring tool.</summary>
	[System.Runtime.InteropServices.ComVisible(true), System.Runtime.InteropServices.Guid("048FF54F-94DF-4879-A355-880832C49A2C")]
	[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
	[System.Runtime.InteropServices.ProgId("Raccoom.RssSource")]
	[System.Xml.Serialization.XmlTypeAttribute("source")]
	[Serializable]	
	[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
	public class RssSource
	:	IRssSource
	{
		#region fields
		
		///<summary>A PropertyChanged event is raised when a property is changed on a component. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		/// <summary>IsPermaLink</summary>
		private string _uri;
		/// <summary>Guid</summary>
		private string _value;
		
		#endregion
		
		#region constructors
		
		/// <summary>Initializes a new instance of RssGuid with default values</summary>
		public RssSource ()
		{
		
		}
		
		/// <summary>Initializes a new instance of RssGuid</summary>
		public RssSource (System.Xml.XmlReader xmlTextReader): this()
		{
			bool emptyElement = xmlTextReader.IsEmptyElement;
			while(xmlTextReader.MoveToNextAttribute())			
			{				
				XmlSerializationUtil.DecodeXmlTextReaderValue(this, xmlTextReader);
			}
			if(emptyElement) return;
			//							
			xmlTextReader.MoveToElement();
			XmlSerializationUtil.DecodeXmlTextReaderValue(this,this.GetType().GetProperty("Value"), xmlTextReader);
		}
		
		#endregion
		
		#region public interface
		
		/// <summary>The RSS channel url that the item came from.</summary>
		[System.ComponentModel.Category("RssSource"), System.ComponentModel.Description("The RSS channel url that the item came from.")]
		[System.Xml.Serialization.XmlAttribute("url")]
		public string Url
		{
			get
			{
				return _uri;
			}
			
			set
			{
				bool changed = !object.Equals(_uri, value);
				_uri = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Url));
			}
		}
		
		// end IsPermaLink
		
		/// <summary>The RSS channel name that the item came from.</summary>
		[System.ComponentModel.Category("RssSource"), System.ComponentModel.Description("The RSS channel name that the item came from.")]
		[System.Xml.Serialization.XmlTextAttribute]
		public string Value
		{
			get
			{
				return _value;
			}
			
			set
			{
				bool changed = !object.Equals(_value, value);
				_value = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Value));
			}
		}
		
		// end Guid
		
		/// <summary>
		/// Obtains the String representation of this instance. 
		/// </summary>
		/// <returns>The friendly name</returns>
		public override string ToString ()
		{
			return Value;
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
			public const string Value = "_value";
		}
		
		#endregion
	}
}