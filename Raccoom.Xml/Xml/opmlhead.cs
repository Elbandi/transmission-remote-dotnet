
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
	/// <summary>A head contains zero or more optional elements</summary>
	[Serializable]
	[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
	public class OpmlHead
	:	IOpmlHead
	{
		#region fields
		
		///<summary>A PropertyChanged event is raised when a property is changed on a component. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		/// <summary>Title</summary>
		private string _title;
		/// <summary>DateCreated</summary>
		private DateTime _dateCreated;
		/// <summary>DateModified</summary>
		internal DateTime _dateModified;
		/// <summary>OwnerName</summary>
		private string _ownerName;
		/// <summary>OwnerEmail</summary>
		private string _ownerEmail;
		/// <summary>ExpansionState</summary>
		private string _expansionState;
		/// <summary>VertScrollState</summary>
		private int _vertScrollState;
		/// <summary>WindowTop</summary>
		private int _windowTop;
		/// <summary>WindowLeft</summary>
		private int _windowLeft;
		/// <summary>WindowBottom</summary>
		private int _windowBottom;
		/// <summary>WindowRight</summary>
		private int _windowRight;
		/// <summary>the document that the head is assigned to.</summary>
		private OpmlDocument _document;
		
		#endregion
		
		#region constructors
		
		/// <summary>Initializes a new instance of OpmlHead with default values</summary>
		public OpmlHead ()
		{
			this._dateCreated = DateTime.Now;
			this._dateModified = DateTime.Now;
		}
		
		/// <summary>Initializes a new instance of OpmlHead</summary>
		public OpmlHead (System.Xml.XmlReader xmlTextReader): this()
		{
			bool supressRead = false;		
			System.Reflection.PropertyInfo propertyInfo = null;			
			while(!(xmlTextReader.Name == "head" && xmlTextReader.NodeType == XmlNodeType.EndElement))
			{
				// Continue read
				if(!supressRead)
				{
					xmlTextReader.Read();					
				}
				xmlTextReader.MoveToContent();
				// find related property by name
				propertyInfo = GetType().GetProperty(xmlTextReader.Name, System.Reflection.BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
				if(propertyInfo!=null)
				{
					supressRead = XmlSerializationUtil.DecodeXmlTextReaderValue(this, xmlTextReader);					
				}				
			}
		}
		
		#endregion
		
		#region public interface
		
		/// <summary>Gets the document that the head is assigned to.</summary>
		[System.Xml.Serialization.XmlIgnore]
		[System.ComponentModel.Category("Data"), System.ComponentModel.Description("Gets the document that the head is assigned to.")]		
		[System.ComponentModel.Browsable(false)]
		public OpmlDocument Document
		{
			get
			{
				return _document;
			}
		}
		
		internal void SetDocument (OpmlDocument value)
		{
			this._document = value;			
		}
		
		/// <summary>Gets the document that the outline is assigned to.</summary>
		[System.Xml.Serialization.XmlIgnore]
		IOpmlDocument IOpmlHead.Document
		{
			get
			{
				return Document;
			}
		}
		
		/// <summary>The title of the document.</summary>
		[System.ComponentModel.Category("Required elements"), System.ComponentModel.Description("The title of the document.")]
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
		
		/// <summary>date-time, indicating when the document was created.</summary>
		[System.ComponentModel.Category("Required elements"), System.ComponentModel.Description("date-time, indicating when the document was created.")]
		[System.Xml.Serialization.XmlIgnoreAttribute]
		public DateTime DateCreated
		{
			get
			{
				return _dateCreated;
			}
			
			set
			{
				bool changed = !object.Equals(_dateCreated, value);
				_dateCreated = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.DateCreated));
			}
		}
		
		// end DateCreated
		
		/// <summary>
		/// Internal, gets the DateTime RFC822 format
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("dateCreated")]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public string DateCreatedRfc
		{
			get
			{
				return DateCreated.ToUniversalTime().ToString("r");
			}
			
			set
			{
			
			}
		}
		
		/// <summary>Date-time, indicating when the document was last modified.</summary>
		/// <remarks>DateModified is updated automatically every time the PropertyChanged event is fired.</remarks>
		[System.ComponentModel.Category("Optional elements"), System.ComponentModel.Description("Date-time, indicating when the document was last modified."), System.ComponentModel.Browsable(false)]
		[System.Xml.Serialization.XmlIgnore]
		public DateTime DateModified
		{
			get
			{
				return _dateModified;
			}
			
			set
			{
				bool changed = !object.Equals(_dateModified, value);
				_dateModified = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.DateModified));
			}
		}
		
		// end DateModified
		
		/// <summary>
		/// Internal, gets the DateTime RFC822 format
		/// </summary>
		[System.Xml.Serialization.XmlElementAttribute("dateModified")]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public string DateModifiedRfc
		{
			get
			{
				return DateModified.ToUniversalTime().ToString("r");
			}
			
			set
			{
			
			}
		}
		
		/// <summary>the owner of the document.</summary>
		[System.ComponentModel.Category("Optional elements"), System.ComponentModel.Description("the owner of the document.")]
		[System.Xml.Serialization.XmlElementAttribute("ownerName")]
		public string OwnerName
		{
			get
			{
				return _ownerName;
			}
			
			set
			{
				bool changed = !object.Equals(_ownerName, value);
				_ownerName = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.OwnerName));
			}
		}
		
		// end OwnerName
		
		/// <summary>the email address of the owner of the document.</summary>
		[System.ComponentModel.Category("Optional elements"), System.ComponentModel.Description("the email address of the owner of the document.")]
		[System.Xml.Serialization.XmlElementAttribute("ownerEmail")]
		public string OwnerEmail
		{
			get
			{
				return _ownerEmail;
			}
			
			set
			{
				bool changed = !object.Equals(_ownerEmail, value);
				_ownerEmail = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.OwnerEmail));
			}
		}
		
		// end OwnerEmail
		
		/// <summary>comma-separated list of line numbers that are expanded. The line numbers in the list tell you which headlines to expand. The order is important. For each element in the list, X, starting at the first summit, navigate flatdown X times and expand. Repeat for each element in the list</summary>
		[System.ComponentModel.Category("Optional elements"), System.ComponentModel.Description("comma-separated list of line numbers that are expanded. The line numbers in the list tell you which headlines to expand. The order is important. For each element in the list, X, starting at the first summit, navigate flatdown X times and expand. Repeat for each element in the list")]
		[System.Xml.Serialization.XmlElementAttribute("expansionState")]
		public string ExpansionState
		{
			get
			{
				return _expansionState;
			}
			
			set
			{
				bool changed = !object.Equals(_expansionState, value);
				_expansionState = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.ExpansionState));
			}
		}
		
		// end ExpansionState
		
		/// <summary>is a number, saying which line of the outline is displayed on the top line of the window. This number is calculated with the expansion state already applied.</summary>
		[System.ComponentModel.Category("Optional elements"), System.ComponentModel.Description("is a number, saying which line of the outline is displayed on the top line of the window. This number is calculated with the expansion state already applied.")]
		[System.Xml.Serialization.XmlElementAttribute("vertScrollState")]
		public int VertScrollState
		{
			get
			{
				return _vertScrollState;
			}
			
			set
			{
				bool changed = !object.Equals(_vertScrollState, value);
				_vertScrollState = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.VertScrollState));
			}
		}
		
		// end VertScrollState
		
		/// <summary>
		/// Instructs the XmlSerializer whether or not to generate the XML element
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public bool VertScrollStateSpecified
		{
			get
			{
				return VertScrollState>0;
			}
			
			set
			{
			
			}
		}
		
		/// <summary>is a number, the pixel location of the top edge of the window.</summary>
		[System.ComponentModel.Category("Optional elements"), System.ComponentModel.Description("is a number, the pixel location of the top edge of the window.")]
		[System.Xml.Serialization.XmlElementAttribute("windowTop")]
		public int WindowTop
		{
			get
			{
				return _windowTop;
			}
			
			set
			{
				bool changed = !object.Equals(_windowTop, value);
				_windowTop = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.WindowTop));
			}
		}
		
		// end WindowTop
		
		/// <summary>
		/// Instructs the XmlSerializer whether or not to generate the XML element
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public bool WindowTopSpecified
		{
			get
			{
				return WindowTop>0;
			}
			
			set
			{
			
			}
		}
		
		/// <summary>is a number, the pixel location of the left edge of the window.</summary>
		[System.ComponentModel.Category("Optional elements"), System.ComponentModel.Description("is a number, the pixel location of the left edge of the window.")]
		[System.Xml.Serialization.XmlElementAttribute("windowLeft")]
		public int WindowLeft
		{
			get
			{
				return _windowLeft;
			}
			
			set
			{
				bool changed = !object.Equals(_windowLeft, value);
				_windowLeft = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.WindowLeft));
			}
		}
		
		// end WindowLeft
		
		/// <summary>
		/// Instructs the XmlSerializer whether or not to generate the XML element
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public bool WindowLeftSpecified
		{
			get
			{
				return WindowLeft>0;
			}
			
			set
			{
			
			}
		}
		
		/// <summary>is a number, the pixel location of the bottom edge of the window</summary>
		[System.ComponentModel.Category("Optional elements"), System.ComponentModel.Description("is a number, the pixel location of the bottom edge of the window")]
		[System.Xml.Serialization.XmlElementAttribute("windowBottom")]
		public int WindowBottom
		{
			get
			{
				return _windowBottom;
			}
			
			set
			{
				bool changed = !object.Equals(_windowBottom, value);
				_windowBottom = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.WindowBottom));
			}
		}
		
		// end WindowBottom
		
		/// <summary>
		/// Instructs the XmlSerializer whether or not to generate the XML element
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public bool WindowBottomSpecified
		{
			get
			{
				return WindowBottom>0;
			}
			
			set
			{
			
			}
		}
		
		/// <summary>is a number, the pixel location of the right edge of the window</summary>
		[System.ComponentModel.Category("Optional elements"), System.ComponentModel.Description("is a number, the pixel location of the right edge of the window")]
		[System.Xml.Serialization.XmlElementAttribute("windowRight")]
		public int WindowRight
		{
			get
			{
				return _windowRight;
			}
			
			set
			{
				bool changed = !object.Equals(_windowRight, value);
				_windowRight = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.WindowRight));
			}
		}
		
		// end WindowRight
		
		/// <summary>
		/// Instructs the XmlSerializer whether or not to generate the XML element
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public bool WindowRightSpecified
		{
			get
			{
				return WindowRight>0;
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
			return Title;
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
			public const string DateCreated = "DateCreated";
			public const string DateModified = "DateModified";
			public const string OwnerName = "OwnerName";
			public const string OwnerEmail = "OwnerEmail";
			public const string ExpansionState = "ExpansionState";
			public const string VertScrollState = "VertScrollState";
			public const string WindowTop = "WindowTop";
			public const string WindowLeft = "WindowLeft";
			public const string WindowBottom = "WindowBottom";
			public const string WindowRight = "WindowRight";
		}
		
		#endregion
	}
}