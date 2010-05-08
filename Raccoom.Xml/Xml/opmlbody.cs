
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
	/// <summary>A body contains one or more outline elements</summary>
	[Serializable]
	[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
	public class OpmlBody
	:	IOpmlBody
	{
		#region fields
		
		///<summary>A PropertyChanged event is raised when a property is changed on a component. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		/// <summary>Items</summary>
		private OpmlOutlineCollection _items;
		/// <summary>the document that the body is assigned to.</summary>
		private OpmlDocument _document;
		
		#endregion
		
		#region constructors
		
		/// <summary>Initializes a new instance of OpmlBody</summary>
		public OpmlBody ()
		{
			this._items = new OpmlOutlineCollection(this);
		}
		
		#endregion
		
		#region public interface
		#if DEBUG
			[System.ComponentModel.Browsable(true)]
		#else
			[System.ComponentModel.Browsable(false)]
		#endif
		/// <summary>Gets the document that the body is assigned to.</summary>
		[System.Xml.Serialization.XmlIgnore]
		[System.ComponentModel.Category("Data"), System.ComponentModel.Description("Gets the document that the body is assigned to.")]
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
			this._items.SetDocument(value);
		}
		
		/// <summary>Gets the document that the outline is assigned to.</summary>
		[System.Xml.Serialization.XmlIgnore]
		IOpmlDocument IOpmlBody.Document
		{
			get
			{
				return Document;
			}
		}
		
		/// <summary>Outline elements.</summary>
		[System.ComponentModel.Category("OpmlBody"), System.ComponentModel.Description("Outline elements.")]
		[System.Xml.Serialization.XmlElementAttribute("outline")]
		public virtual OpmlOutlineCollection Items
		{
			get
			{
				return _items;
			}
		}
		
		// end Items
		
		System.Collections.ICollection IOpmlBody.Items
		{
			get
			{
				return Items as System.Collections.ICollection;
			}
		}
		
		// end Items
		
		/// <summary>
		/// Obtains the String representation of this instance. 
		/// </summary>
		/// <returns>The friendly name</returns>
		public override string ToString ()
		{
			return "Outlines: " +_items.Count.ToString();
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
			public const string Items = "Items";
		}
		
		#endregion
		
		#region events
		
		///<summary>A PropertyChanged event is raised when a sub property is changed. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
		protected internal virtual void OnSubItemPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if(PropertyChanged!=null) PropertyChanged(sender, e);	
		}
		
		#endregion
	}
}