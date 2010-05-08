
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
	/// <summary>A channel may optionally contain a textInput sub-element, which contains four required sub-elements.</summary>
	[System.Runtime.InteropServices.ComVisible(true), System.Runtime.InteropServices.Guid("026FF54F-94DF-4879-A355-880835C49A2C")]
	[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
	[System.Runtime.InteropServices.ProgId("Raccoom.RssTextInput")]
	[System.Xml.Serialization.XmlTypeAttribute("textInput")]
	[Serializable]
	[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
	public class RssTextInput
	:	IRssTextInput
	{
		#region fields
		
		///<summary>A PropertyChanged event is raised when a property is changed on a component. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		/// <summary>Title</summary>
		private string _title;
		/// <summary>Description</summary>
		private string _description;
		/// <summary>Name</summary>
		private string _name;
		/// <summary>Link</summary>
		private string _link;
		
		#endregion
		
		#region constructors
		
		/// <summary>Initializes a new instance of RssTextInput with default values</summary>
		public RssTextInput ()
		{
		
		}
		
		/// <summary>Initializes a new instance of RssTextInput</summary>
		public RssTextInput (System.Xml.XmlReader xmlTextReader)
		{
			if(xmlTextReader.IsEmptyElement) return;
			//
			bool supressRead = false;
			while(!(xmlTextReader.Name == "textInput" && xmlTextReader.NodeType == XmlNodeType.EndElement))
			{
				// Continue read
				if(!supressRead) xmlTextReader.Read();							
				xmlTextReader.MoveToContent();
				//
				supressRead = false;
				if( xmlTextReader.NodeType != XmlNodeType.Element) continue;
				supressRead = XmlSerializationUtil.DecodeXmlTextReaderValue(this,xmlTextReader);							
			}
		}
		
		#endregion
		
		#region public interface
		
		/// <summary>The label of the Submit button in the text input area. </summary>
		[System.ComponentModel.Category("RssTextInput"), System.ComponentModel.Description("The label of the Submit button in the text input area. ")]
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
		
		/// <summary>Explains the text input area. </summary>
		[System.ComponentModel.Category("RssTextInput"), System.ComponentModel.Description("Explains the text input area. ")]
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
		
		/// <summary> The name of the text object in the text input area. </summary>
		[System.ComponentModel.Category("RssTextInput"), System.ComponentModel.Description(" The name of the text object in the text input area. ")]
		[System.Xml.Serialization.XmlElementAttribute("name")]
		public string Name
		{
			get
			{
				return _name;
			}
			
			set
			{
				bool changed = !object.Equals(_name, value);
				_name = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Name));
			}
		}
		
		// end Name
		
		/// <summary>The URL of the CGI script that processes text input requests. </summary>
		[System.ComponentModel.Category("RssTextInput"), System.ComponentModel.Description("The URL of the CGI script that processes text input requests. ")]
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
			public const string Description = "Description";
			public const string Name = "Name";
			public const string Link = "Link";
		}
		
		#endregion
	}
}