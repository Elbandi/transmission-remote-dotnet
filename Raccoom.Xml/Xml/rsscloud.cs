
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
	/// <summary>Is an optional sub-element of channel.  It specifies a web service that supports the rssCloud interface which can be implemented in HTTP-POST, XML-RPC or SOAP 1.1.</summary>
	[System.Runtime.InteropServices.ComVisible(true), System.Runtime.InteropServices.Guid("026FF54F-96DF-4879-A355-880832C49A1C")]
	[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
	[System.Runtime.InteropServices.ProgId("Raccoom.RssCloud")]
	[System.Xml.Serialization.XmlTypeAttribute("cloud")]
	[Serializable]
	[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
	public class RssCloud
	:	IRssCloud
	{
		#region fields
		
		///<summary>A PropertyChanged event is raised when a property is changed on a component. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		/// <summary>Domain</summary>
		private string _domain;
		/// <summary>Port</summary>
		private int _port;
		/// <summary>Path</summary>
		private string _path;
		/// <summary>RegisterProcedure</summary>
		private string _registerProcedure;
		/// <summary>Protocol</summary>
		private CloudProtocol _protocol;
		
		#endregion
		
		#region constructors
		
		/// <summary>Initializes a new instance of RssCloud with default values</summary>
		public RssCloud ()
		{
		
		}
		
		/// <summary>Initializes a new instance of RssCloud</summary>
		public RssCloud (System.Xml.XmlReader xmlTextReader)
		{
			if(!xmlTextReader.HasAttributes) return;
			//
			System.Reflection.PropertyInfo propertyInfo = null;
			//
			while(xmlTextReader.MoveToNextAttribute())			
			{				
				// find related property by name
				propertyInfo = GetType().GetProperty(xmlTextReader.Name, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
				if(propertyInfo!=null)
				{
					// Protocol enum needs some conversion before the typeconverter can set the values that contains - char
					if(propertyInfo.Name == "Protocol")
					{						
						propertyInfo.SetValue(this, System.ComponentModel.TypeDescriptor.GetConverter(propertyInfo.PropertyType).ConvertFromString(xmlTextReader.ReadInnerXml().Trim().Replace("-","")),null);
					}
					else
					{
						XmlSerializationUtil.DecodeXmlTextReaderValue(this, xmlTextReader);
					}
				}				
			}
		}
		
		#endregion
		
		#region public interface
		
		/// <summary></summary>
		[System.ComponentModel.Category("RssCloud"), System.ComponentModel.Description("")]
		[System.Xml.Serialization.XmlAttribute("domain")]
		public string Domain
		{
			get
			{
				return _domain;
			}
			
			set
			{
				bool changed = !object.Equals(_domain, value);
				_domain = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Domain));
			}
		}
		
		// end Domain
		
		/// <summary></summary>
		[System.ComponentModel.Category("RssCloud"), System.ComponentModel.Description("")]
		[System.Xml.Serialization.XmlAttribute("port")]
		public int Port
		{
			get
			{
				return _port;
			}
			
			set
			{
				bool changed = !object.Equals(_port, value);
				_port = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Port));
			}
		}
		
		// end Port
		
		/// <summary>
		/// Instructs the XmlSerializer whether or not to generate the XML element
		/// </summary>
		[System.Xml.Serialization.XmlIgnoreAttribute]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public bool PortSpecified
		{
			get
			{
				return _port>0;
			}
			
			set
			{
			
			}
		}
		
		/// <summary></summary>
		[System.ComponentModel.Category("RssCloud"), System.ComponentModel.Description("")]
		[System.Xml.Serialization.XmlAttribute("path")]
		public string Path
		{
			get
			{
				return _path;
			}
			
			set
			{
				bool changed = !object.Equals(_path, value);
				_path = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Path));
			}
		}
		
		// end Path
		
		/// <summary></summary>
		[System.ComponentModel.Category("RssCloud"), System.ComponentModel.Description("")]
		[System.Xml.Serialization.XmlAttribute("registerProcedure")]
		public string RegisterProcedure
		{
			get
			{
				return _registerProcedure;
			}
			
			set
			{
				bool changed = !object.Equals(_registerProcedure, value);
				_registerProcedure = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.RegisterProcedure));
			}
		}
		
		// end RegisterProcedure
		
		/// <summary></summary>
		[System.ComponentModel.Category("RssCloud"), System.ComponentModel.Description(""), System.ComponentModel.DefaultValue(CloudProtocol.None)]
		[System.Xml.Serialization.XmlAttribute("protocol")]
		public CloudProtocol Protocol
		{
			get
			{
				return _protocol;
			}
			
			set
			{
				bool changed = !object.Equals(_protocol, value);
				_protocol = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Protocol));
			}
		}
		
		// end Protocol
		
		/// <summary>
		/// Instructs the XmlSerializer whether or not to generate the XML element
		/// </summary>
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.Xml.Serialization.XmlIgnoreAttribute]
		public bool ProtocolSpecified
		{
			get
			{
				return _protocol!=CloudProtocol.None;
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
			return this.Domain + this.Port + this.Path + this.RegisterProcedure;
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
			public const string Domain = "Domain";
			public const string Port = "Port";
			public const string Path = "Path";
			public const string RegisterProcedure = "RegisterProcedure";
			public const string Protocol = "Protocol";
		}
		
		#endregion
	}
}