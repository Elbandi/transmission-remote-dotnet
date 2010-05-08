
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
	/// <see cref="OpmlOutline"/> strong typed collecton.
	/// </summary>
	[Serializable]
	public class OpmlOutlineCollection
	:	System.Collections.CollectionBase
	{
		#region fields
		
		private OpmlDocument _document;
		private OpmlBody _body;
		private OpmlOutline _outline;
		
		#endregion
		
		#region constructors
		
		/// <summary>Initializes a new instance of OpmlOutlineCollection</summary>
		public OpmlOutlineCollection ()
		{
		
		}
		
		/// <summary>Initializes a new instance of OpmlOutlineCollection</summary>
		public OpmlOutlineCollection (OpmlBody body)
		{
			_body = body;
		}
		
		/// <summary>Initializes a new instance of OpmlOutlineCollection</summary>
		public OpmlOutlineCollection (OpmlOutline outline)
		{
			_outline = outline;
		}
		
		#endregion
		
		#region internal interface
		
		internal void SetDocument (OpmlDocument document)
		{
			this._document = document;
			foreach(OpmlOutline item in this.List)
			{
				item.SetDocument(document);
			}
		}
		
		#endregion
		
		#region public interface
		
		/// <summary>Adds an item to the IOpmlOutlineCollection.</summary>
		public int Add (OpmlOutline value)
		{
			return base.List.Add(value as object);
		}
		
		/// <summary>Removes an item to the OpmlOutlineCollection.</summary>
		public void Remove (OpmlOutline value)
		{
			base.List.Remove(value as object);
		}
		
		/// <summary>Inserts an IOpmlOutline to the OpmlOutlineCollection at the specified position.</summary>
		public void Insert (int index, OpmlOutline value)
		{
			base.List.Insert(index, value as object);
		}
		
		/// <summary>Determines whether the OpmlOutlineCollection contains a specific value.</summary>
		public bool Contains (OpmlOutline value)
		{
			return base.List.Contains(value as object);
		}
		
		/// <summary>Gets the IOpmlOutline at the specified index.</summary>
		public OpmlOutline this [ int index ]
		{
			get
			{
				return (base.List[index] as OpmlOutline); 
			}
		}
		
		/// <summary>Determines the index of a specific item i</summary>
		public int IndexOf (IOpmlOutline value)
		{
			return( List.IndexOf( value ) );
		}
		
		/// <summary>Copies the elements of the Collection to an Array, starting at a particular Array index.</summary>
		public void CopyTo (Array array, int index)
		{
			List.CopyTo(array, index);
		}
		
		#endregion
		
		#region internal interface
		
		/// <summary>
		/// Performs additional custom processes
		/// </summary>
		protected override void OnInsertComplete (int index, object value)
		{
			base.OnInsertComplete (index, value);
			// attach item
			AttachItem(value as OpmlOutline);
			//if(item!=null && _parent!=null && !item.PropertyChangedEventAttached) item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_parent.OnSubItemPropertyChanged);			
		}
		
		/// <summary>
		/// Performs additional custom processes
		/// </summary>
		protected override void OnRemoveComplete (int index, object value)
		{
			base.OnRemoveComplete (index, value);
			// attach item
			DetachItem(value as OpmlOutline);
			//if(item!=null && _parent!=null && item.PropertyChangedEventAttached) item.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(_parent.OnSubItemPropertyChanged);			
		}
		
		/// <summary>
		/// Performs additional custom processes
		/// </summary>
		protected override void OnClear ()
		{
			base.OnClear ();
			// detach all items
			foreach(OpmlOutline item in List)
			{
				DetachItem(item);
			}
			// Dirty state
			SetDirtyState();
		}
		
		#endregion
		
		#region private interface
		
		private void SetDirtyState ()
		{
			if(_body!=null)
			{
				_body.OnSubItemPropertyChanged(_body, new System.ComponentModel.PropertyChangedEventArgs(OpmlBody.Fields.Items));
			} 
			else if(_outline!=null)
			{
				_outline.OnSubItemPropertyChanged(_outline, new System.ComponentModel.PropertyChangedEventArgs(OpmlBody.Fields.Items));
			}
		}
		
		private void AttachItem (OpmlOutline item)
		{
			System.Diagnostics.Debug.Assert(item!=null);
			//
			if(_body!=null)
			{	
				item.SetDocument(_document);
				item.SetParent(null);
				item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_body.OnSubItemPropertyChanged);			
			} 
			else
			{
				item.SetParent(this._outline);
				item.SetDocument(this._outline.Document);
				item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_outline.OnSubItemPropertyChanged);
			}
			SetDirtyState();			
		}
		
		private void DetachItem (OpmlOutline item)
		{
			System.Diagnostics.Debug.Assert(item!=null);
			//
			if(_body!=null)
			{					
				item.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(_body.OnSubItemPropertyChanged);			
			} 
			else
			{
				item.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(_outline.OnSubItemPropertyChanged);
			}
			item.SetDocument(null);
			item.SetParent(null);
			//
			SetDirtyState();
		}
		
		#endregion
	}
	
	/// <summary>An outline is an XML element, possibly containing one or more attributes, and containing any number of outline sub-elements.</summary>
	[System.Xml.Serialization.XmlTypeAttribute("outline")]
	[Serializable]
	[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
	public class OpmlOutline
	:	IOpmlOutline
	{
		#region fields
		
		///<summary>A PropertyChanged event is raised when a property is changed on a component. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		/// <summary>Text</summary>
		private string _text;
		/// <summary>Type</summary>
		private string _type;
		/// <summary>Description</summary>
		private string _description;
		/// <summary>XmlUrl</summary>
		private string _xmlUrl;
		/// <summary>HtmlUrl</summary>
		private string _htmlUrl;
		/// <summary>IsComment</summary>
		private bool _isComment;
		/// <summary>IsBreakpoint</summary>
		private bool _isBreakpoint;
		/// <summary>Items</summary>
		private OpmlOutlineCollection _items;
		/// <summary>the document that the outline is assigned to.</summary>
		private OpmlDocument _document;
		/// <summary>the parent outline that the outline is assigned to.</summary>
		private OpmlOutline _outline;
		/// <summary>outline which references another opml file</summary>
		public const string OpmlTypeLinkReference = "text/x-opml";
		
		#endregion
		
		#region constructors
		
		/// <summary>Initializes a new instance of OpmlOutline with default values.</summary>
		public OpmlOutline ()
		{
			_items = new OpmlOutlineCollection(this);
		}
		
		/// <summary>Initializes a new instance of OpmlOutline</summary>
		public OpmlOutline (System.Xml.XmlReader xmlTextReader): this()
		{
			if(!xmlTextReader.HasAttributes) return;
			// get attributes
			System.Reflection.PropertyInfo propertyInfo = null;
			for(int i =0;i<xmlTextReader.AttributeCount;i++)
			{
				xmlTextReader.MoveToAttribute(i);
				// try to find some common used alias names for attributes
				string attributeName = xmlTextReader.Name;
				if(attributeName.IndexOf("url")!=-1) attributeName = "xmlUrl";
				if(attributeName.IndexOf("title")!=-1) attributeName = "text";
				// find related property by name
				propertyInfo = GetType().GetProperty(attributeName, System.Reflection.BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
				if(propertyInfo!=null)
				{					
					propertyInfo.SetValue(this, System.ComponentModel.TypeDescriptor.GetConverter(propertyInfo.PropertyType).ConvertFromString(xmlTextReader.ReadInnerXml().Trim()),null);
				}
			}
		}
		
		#endregion
		
		#region public interface
		
		/// <summary>
		/// Removes the current outline from the document.
		/// </summary>
		/// <remarks>
		/// When the Remove method is called, the outline and any child outline items assigned to the document are removed from the document. The removed child outlines are removed from the document , but are still attached to this outline item.
		/// </remarks>
		public void Remove ()
		{
			if(this.Parent != null) Parent.Items.Remove(this);
			else if(this.Document!=null) this.Document.Body.Items.Remove(this);
		}
		
		/// <summary>Gets the document that the outline is assigned to.</summary>
		[System.ComponentModel.Category("Data"), System.ComponentModel.Description("Gets the document that the outline is assigned to.")]		
		[System.Xml.Serialization.XmlIgnore]
		
			[System.ComponentModel.Browsable(false)]
		public OpmlDocument Document
		{
			get
			{
				return _document;
			}
		}
		
		/// <summary>Gets the document that the outline is assigned to.</summary>
		[System.ComponentModel.Category("Data"), System.ComponentModel.Description("Gets the document that the outline is assigned to.")]		
		[System.Xml.Serialization.XmlIgnore]
		IOpmlDocument IOpmlOutline.Document
		{
			get
			{
				return this.Document;
			}
		}
		
		/// <summary>Sets the document that the outline is assigned to.</summary>
		internal void SetDocument (OpmlDocument value)
		{
			this._document= value;
			this._items.SetDocument(value);
		}
		
		/// <summary>Gets the outline that this outline is assigned to.</summary>
		/// <remarks>
		/// If the outline is at the root level, the Parent property returns null. 
		/// </remarks>		
		[System.ComponentModel.Category("Data"), System.ComponentModel.Description("Gets the outline that this outline is assigned to.")]
		#if DEBUG
			[System.ComponentModel.Browsable(true)]
		#else
			[System.ComponentModel.Browsable(false)]
		#endif
		[System.Xml.Serialization.XmlIgnore]
		public OpmlOutline Parent
		{
			get
			{
				return _outline;
			}
		}
		
		/// <summary>Gets the outline that this outline is assigned to.</summary>
		/// <remarks>
		/// If the outline is at the root level, the Parent property returns null. 
		/// </remarks>		
		[System.ComponentModel.Category("Data"), System.ComponentModel.Description("Gets the outline that this outline is assigned to."), System.ComponentModel.Browsable(false)]
		[System.Xml.Serialization.XmlIgnore]
		IOpmlOutline IOpmlOutline.Parent
		{
			get
			{
				return this.Parent;
			}
		}
		
		/// <summary>Sets the outline that this outline is assigned to.</summary>
		internal void SetParent (OpmlOutline value)
		{
			this._outline = value;
		}
		
		/// <summary>Text is the string of characters that's displayed when the outline is being browsed or edited. There is no specific limit on the length of the text attribute.</summary>
		[System.ComponentModel.Category("Required outline elements"), System.ComponentModel.Description("Text is the string of characters that's displayed when the outline is being browsed or edited. There is no specific limit on the length of the text attribute.")]
		[System.Xml.Serialization.XmlAttribute("text")]
		public string Text
		{
			get
			{
				return _text;
			}
			
			set
			{
				bool changed = !object.Equals(_text, value);
				_text = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Text));
			}
		}
		
		// end Text
		
		/// <summary>Type is a string, it says how the other attributes of the outline are interpreted</summary>
		[System.ComponentModel.Category("Optional outline elements"), System.ComponentModel.Description("Type is a string, it says how the other attributes of the outline are interpreted")]
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
		
		/// <summary></summary>
		[System.ComponentModel.Category("Optional outline elements"), System.ComponentModel.Description("")]
		[System.Xml.Serialization.XmlAttribute("description")]
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
		
		/// <summary>Gets or sets the favorite url.</summary>
		[System.ComponentModel.Category("Required outline elements"), System.ComponentModel.Description("Gets or sets the favorite url.")]
		[System.Xml.Serialization.XmlAttribute("xmlUrl")]
		public string XmlUrl
		{
			get
			{
				return _xmlUrl;
			}
			
			set
			{
				bool changed = !object.Equals(_xmlUrl, value);
				_xmlUrl = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.XmlUrl));
			}
		}
		
		// end XmlUrl
		
		/// <summary></summary>
		[System.ComponentModel.Category("Optional outline elements"), System.ComponentModel.Description("")]
		[System.Xml.Serialization.XmlAttribute("htmlUrl")]
		public string HtmlUrl
		{
			get
			{
				return _htmlUrl;
			}
			
			set
			{
				bool changed = !object.Equals(_htmlUrl, value);
				_htmlUrl = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.HtmlUrl));
			}
		}
		
		// end HtmlUrl
		
		/// <summary>IsComment is a string, either true or false, indicating whether the outline is commented or not. By convention if an outline is commented, all subordinate outlines are considered to be commented as well. If it's not present, the value is false.</summary>
		[System.ComponentModel.Category("Optional outline elements"), System.ComponentModel.Description("IsComment is a string, either true or false, indicating whether the outline is commented or not. By convention if an outline is commented, all subordinate outlines are considered to be commented as well. If it's not present, the value is false.")]
		[System.Xml.Serialization.XmlAttribute("isComment")]
		public bool IsComment
		{
			get
			{
				return _isComment;
			}
			
			set
			{
				bool changed = !object.Equals(_isComment, value);
				_isComment = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.IsComment));
			}
		}
		
		// end IsComment
		
		/// <summary>IsBreakpoint is a string, either true or false, indicating whether a breakpoint is set on this outline. This attribute is mainly necessary for outlines used to edit scripts that execute. If it's not present, the value is false.</summary>
		[System.ComponentModel.Category("Optional outline elements"), System.ComponentModel.Description("IsBreakpoint is a string, either true or false, indicating whether a breakpoint is set on this outline. This attribute is mainly necessary for outlines used to edit scripts that execute. If it's not present, the value is false.")]
		[System.Xml.Serialization.XmlAttribute("isBreakpoint")]
		public bool IsBreakpoint
		{
			get
			{
				return _isBreakpoint;
			}
			
			set
			{
				bool changed = !object.Equals(_isBreakpoint, value);
				_isBreakpoint = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.IsBreakpoint));
			}
		}
		
		// end IsBreakpoint
		
		/// <summary>Outline elements.</summary>
		[System.ComponentModel.Category("Optional outline elements"), System.ComponentModel.Description("Outline elements.")]
		[System.Xml.Serialization.XmlElementAttribute("outline")]
		public OpmlOutlineCollection Items
		{
			get
			{
				return _items;
			}
		}
		
		// end Items
		
		System.Collections.ICollection IOpmlOutline.Items
		{
			get
			{
				return _items;
			}
		}
		
		/// <summary>
		/// Obtains the String representation of this instance. 
		/// </summary>
		/// <returns>The friendly name</returns>
		public override string ToString ()
		{
			return Text;
		}
		
		#endregion
		
		#region protected interface
		
		internal bool PropertyChangedEventAttached
		{
			get
			{
				return PropertyChanged!=null;
			}
		}
		
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
			public const string Text = "Text";
			public const string Type = "Type";
			public const string Description = "Description";
			public const string XmlUrl = "XmlUrl";
			public const string HtmlUrl = "HtmlUrl";
			public const string IsComment = "IsComment";
			public const string IsBreakpoint = "IsBreakpoint";
			public const string Items = "Items";
		}
		
		#endregion
		
		#region events
		
		///<summary>A PropertyChanged event is raised when a sub property is changed. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
		protected internal void OnSubItemPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if(PropertyChanged!=null) PropertyChanged(sender, e);	
		}
		
		#endregion
	}
}