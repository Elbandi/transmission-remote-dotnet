
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
	/// <see cref="IRssItem"/> strong typed collecton.
	/// </summary>
	[Serializable]
	public class RssItemCollection
	:	System.Collections.CollectionBase
	{
		#region fields
		
		private RssChannel _parent;
		
		#endregion
		
		#region constructor
		
		/// <summary>
		/// Create a new collection for the specified channel
		/// </summary>
		/// <param name="parent">Channel for which this collection holds the data.</param>
		public RssItemCollection (RssChannel parent)
		{
			_parent = parent;			
		}
		
		#endregion
		
		#region public interface
		
		/// <summary>Adds an item to the IRssItemCollection.</summary>
		public int Add (RssItem value)
		{
			return base.List.Add(value as object);
		}
		
		/// <summary>Removes an item to the RssItemCollection.</summary>
		public void Remove (RssItem value)
		{
			base.List.Remove(value as object);
		}
		
		/// <summary>Inserts an IRssItem to the RssItemCollection at the specified position.</summary>
		public void Insert (int index, RssItem value)
		{
			base.List.Insert(index, value as object);
		}
		
		/// <summary>Determines whether the RssItemCollection contains a specific value.</summary>
		public bool Contains (RssItem value)
		{
			return base.List.Contains(value as object);
		}
		
		/// <summary>Gets the IRssItem at the specified index.</summary>
		public RssItem this [ int index ]
		{
			get
			{
				return (base.List[index] as RssItem); 
			}
		}
		
		/// <summary>Determines the index of a specific item i</summary>
		public int IndexOf (IRssItem value)
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
			AttachItem(value as RssItem);
			//if(item!=null && _parent!=null && !item.PropertyChangedEventAttached) item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_parent.OnSubItemPropertyChanged);			
		}
		
		/// <summary>
		/// Performs additional custom processes
		/// </summary>
		protected override void OnRemoveComplete (int index, object value)
		{
			base.OnRemoveComplete (index, value);
			// attach item
			DetachItem(value as RssItem);
			//if(item!=null && _parent!=null && item.PropertyChangedEventAttached) item.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(_parent.OnSubItemPropertyChanged);			
		}
		
		/// <summary>
		/// Performs additional custom processes
		/// </summary>
		protected override void OnClear ()
		{
			base.OnClear ();
			// detach all items
			foreach(RssItem item in List)
			{
				DetachItem(item);
			}
			// Dirty state
			if(_parent!=null) _parent.OnSubItemPropertyChanged(_parent, new System.ComponentModel.PropertyChangedEventArgs(RssChannel.Fields.Items));
		}
		
		/// <summary>
		/// Performs additional custom processes after inserting a new element into the CollectionBase instance
		/// </summary>
		/// <param name="value"></param>
		private void AttachItem (RssItem value)
		{
			System.Diagnostics.Debug.Assert(value!=null);
			// set parent channel
			value.SetChannel(this._parent);
			// subscribe dirty event
			if(value!=null && _parent!=null) value.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_parent.OnSubItemPropertyChanged);
		}
		
		/// <summary>
		/// Performs additional custom processes after removing a new element from the CollectionBase instance
		/// </summary>
		/// <param name="item"></param>
		private void DetachItem (RssItem item)
		{
			System.Diagnostics.Debug.Assert(item!=null);
			// reset parent channel
			item.SetChannel(null);
			// describe dirty event
			if(item!=null && _parent!=null) item.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(_parent.OnSubItemPropertyChanged);
		}
		
		#endregion
	}
	
	/// <summary>An item may represent a "story" -- much like a story in a newspaper or magazine; if so its description is a synopsis of the story, and the link points to the full story. An item may also be complete in itself, if so, the description contains the text (entity-encoded HTML is allowed), and the link and title may be omitted. All elements of an item are optional, however at least one of title or description must be present.<see cref="IRssItem"/></summary>
	[System.Runtime.InteropServices.ComVisible(true), System.Runtime.InteropServices.Guid("026FF54F-94DF-4879-A355-880832C49A2C")]
	[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
	[System.Runtime.InteropServices.ProgId("Raccoom.RssItem")]
	[Serializable]
	[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
	public class RssItem
	:	IRssItem
	{
		#region fields
		
		/// <summary>Parent channel that the item is assigned to.</summary>
		private RssChannel _rssChannel;
		
		///<summary>A PropertyChanged event is raised when a property is changed on a component. A PropertyChangedEventArgs object specifies the name of the property that changed.</summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		/// <summary>Title</summary>
		private string _title;
		/// <summary>Description</summary>
		private string _description;
		/// <summary>Link</summary>
		private string _link;
		/// <summary>Author</summary>
		private string _author;
		/// <summary>Category</summary>
		private string _category;
		/// <summary>PubDate</summary>
		private DateTime _pubDate;
		/// <summary>Comments</summary>
		private string _comments;
		/// <summary>Enclosure</summary>
		private RssEnclosure _enclosure;
		/// <summary>Guid</summary>
		private RssGuid _guid;
		/// <summary>Source</summary>
		private RssSource _source;
		
		#endregion
		
		#region constructors
		
		/// <summary>Initializes a new instance with default values</summary>
		public RssItem ()
		{
			Enclosure = new RssEnclosure();
			Guid = new RssGuid();
			Source = new RssSource();
			this.PubDate = DateTime.Now;
		}
		
		/// <summary>Initializes a new instance</summary>
		public RssItem (System.Xml.XmlReader xmlTextReader): this()
		{
			if(xmlTextReader.IsEmptyElement) return;
			//
			System.Diagnostics.Debug.Assert(!xmlTextReader.IsEmptyElement);
			bool supressRead = false;
			// fill new item with the provided data
			while(!(xmlTextReader.Name == "item" && xmlTextReader.NodeType == XmlNodeType.EndElement))
			{	
				// Continue read
				if(!supressRead) xmlTextReader.Read();
				xmlTextReader.MoveToContent();
				//
				supressRead = false;
				if(xmlTextReader.NodeType!= System.Xml.XmlNodeType.Element) continue;
				// find related property by name
				if(xmlTextReader.Name == "enclosure")
				{
					if(!xmlTextReader.HasAttributes) continue;
					this.Enclosure = new RssEnclosure(xmlTextReader);
				} 
				else if(xmlTextReader.Name == "guid")
				{					
					supressRead = !xmlTextReader.IsEmptyElement;
					this.Guid = new RssGuid(xmlTextReader);
				} 
				else if(xmlTextReader.Name == "source")
				{	
					supressRead = !xmlTextReader.IsEmptyElement;
					this.Source= new RssSource(xmlTextReader);					
				} 
				else if(!xmlTextReader.IsEmptyElement)
				{
					supressRead = XmlSerializationUtil.DecodeXmlTextReaderValue(this, xmlTextReader);					
				}
			}
		}
		
		#endregion
		
		#region public interface
		
		/// <summary>
		/// Removes the current item from the channel.
		/// </summary>
		/// <remarks>
		/// When the Remove method is called, the item is removed from the channel.
		/// </remarks>
		public void Remove ()
		{
			if(this._rssChannel == null) return;
			this._rssChannel.Items.Remove(this);
		}
		
		/// <summary>
		/// Gets the parent channel that the item is assigned to.
		/// </summary>
		[System.ComponentModel.Category("Data"), System.ComponentModel.Description("Gets the parent channel that the item is assigned to."), System.ComponentModel.Browsable(false)]
		[System.Xml.Serialization.XmlIgnore]
		public RssChannel Channel
		{
			get
			{
				return _rssChannel;
			}
		}
		
		/// <summary>
		/// Gets the parent channel that the item is assigned to.
		/// </summary>
		[System.ComponentModel.Category("Data"), System.ComponentModel.Description("Gets the parent channel that the item is assigned to.")]
		IRssChannel IRssItem.Channel
		{
			get
			{
				return Channel;
			}
		}
		
		/// <summary>
		/// Sets the parent channel that the item is assigned to.
		/// </summary>
		/// <remarks>
		/// This internal method is called when the item is assigned to <see cref="RssItemCollection"/>. Do not call directly!
		/// </remarks>
		internal void SetChannel (RssChannel value)
		{
			_rssChannel = value;			
		}
		
		/// <summary>The title of the item.</summary>
		[System.ComponentModel.Category("Required item elements"), System.ComponentModel.Description("The title of the item.")]
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
		
		/// <summary>The item synopsis.</summary>
		[System.ComponentModel.Category("Required item elements"), System.ComponentModel.Description("The item synopsis.")]
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
		
		/// <summary>The URL of the item.</summary>
		[System.ComponentModel.Category("Optional item elements"), System.ComponentModel.Description("The URL of the item.")]
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
		
		/// <summary>Email address of the author of the item.</summary>
		[System.ComponentModel.Category("Optional item elements"), System.ComponentModel.Description("Email address of the author of the item.")]
		[System.Xml.Serialization.XmlElementAttribute("author")]
		public string Author
		{
			get
			{
				return _author;
			}
			
			set
			{
				bool changed = !object.Equals(_author, value);
				_author = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Author));
			}
		}
		
		// end Author
		
		/// <summary>Includes the item in one or more categories.</summary>
		[System.ComponentModel.Category("Optional item elements"), System.ComponentModel.Description("Includes the item in one or more categories.")]
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
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Category));
			}
		}
		
		// end Category
		
		/// <summary>Indicates when the item was published.</summary>
		[System.ComponentModel.Category("Optional item elements"), System.ComponentModel.Description("Indicates when the item was published.")]
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
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.PubDate));
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
		
		/// <summary>URL of a page for comments relating to the item. </summary>
		[System.ComponentModel.Category("Optional item elements"), System.ComponentModel.Description("URL of a page for comments relating to the item. ")]
		[System.Xml.Serialization.XmlElementAttribute("comments")]
		public string Comments
		{
			get
			{
				return _comments;
			}
			
			set
			{
				bool changed = !object.Equals(_comments, value);
				_comments = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Comments));
			}
		}
		
		// end Comments
		
		/// <summary>Describes a media object that is attached to the item. </summary>
		[System.Xml.Serialization.XmlElementAttribute("enclosure")]
		[System.ComponentModel.Category("Optional item elements"), System.ComponentModel.Description("Describes a media object that is attached to the item. ")]
		public RssEnclosure Enclosure
		{
			get
			{
				return _enclosure;
			}
			
			set
			{
				bool changed = !object.Equals(_enclosure, value);
				if(changed && _enclosure!=null) _enclosure.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
				_enclosure = value;
				if(changed) 
				{
					OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Enclosure));
					if(_enclosure!=null) _enclosure.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
				}
			}
		}
		
		// end Enclosure
		
		/// <summary>Describes a media object that is attached to the item. </summary>
		[System.Xml.Serialization.XmlElementAttribute("enclosure")]
		[System.ComponentModel.Category("Optional item elements"), System.ComponentModel.Description("Describes a media object that is attached to the item. ")]
		IRssEnclosure IRssItem.Enclosure
		{
			get
			{
				return Enclosure;
			}
			
			set
			{
				Enclosure = value as RssEnclosure;
			}
		}
		
		// end Enclosure
		
		/// <summary>
		/// Instructs the XmlSerializer whether or not to generate the XML element
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public bool EnclosureSpecified
		{
			get
			{
				return (_enclosure.Url != null && _enclosure.Url.Length>0) || (_enclosure.Type!=null&&_enclosure.Type.Length>0) || _enclosure.LengthSpecified;
			}
			
			set
			{
			
			}
		}
		
		/// <summary>A string that uniquely identifies the item.</summary>
		[System.ComponentModel.Category("Optional item elements"), System.ComponentModel.Description("A string that uniquely identifies the item.")]
		[System.Xml.Serialization.XmlElementAttribute("guid")]
		public RssGuid Guid
		{
			get
			{
				return _guid;
			}
			
			set
			{
				bool changed = !object.Equals(_guid, value);
				if(changed && _guid!=null) _guid.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
				_guid = value;
				if(changed)
				{
					OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Guid));
					if(_guid!=null) _guid.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
				}
			}
		}
		
		// end Guid
		
		/// <summary>A string that uniquely identifies the item.</summary>
		[System.ComponentModel.Category("Optional item elements"), System.ComponentModel.Description("A string that uniquely identifies the item.")]
		[System.Xml.Serialization.XmlElementAttribute("guid")]
		IRssGuid IRssItem.Guid
		{
			get
			{
				return this.Guid;
			}
			
			set
			{
				this.Guid = value as RssGuid;
			}
		}
		
		/// <summary>
		/// Instructs the XmlSerializer whether or not to generate the XML element
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public bool GuidSpecified
		{
			get
			{
				return _guid.Value != null && _guid.Value.Length>0;
			}
			
			set
			{
			
			}
		}
		
		/// <summary>The RSS channel that the item came from.</summary>
		[System.ComponentModel.Category("Optional item elements"), System.ComponentModel.Description("The RSS channel that the item came from.")]
		[System.Xml.Serialization.XmlElementAttribute("source")]
		public RssSource Source
		{
			get
			{
				return _source;
			}
			
			set
			{
				bool changed = !object.Equals(_source, value);
				if(changed && _source!=null) _source.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
				_source = value;
				if(changed)
				{
					OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.Source));
					if(_source!=null) _source.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OnSubItemPropertyChanged);
				}
			}
		}
		
		// end Source
		
		/// <summary>The RSS channel that the item came from.</summary>
		[System.ComponentModel.Category("Optional item elements"), System.ComponentModel.Description("The RSS channel that the item came from.")]
		[System.Xml.Serialization.XmlElementAttribute("source")]
		IRssSource IRssItem.Source
		{
			get
			{
				return Source;
			}
			
			set
			{
				Source = value as RssSource;
			}
		}
		
		/// <summary>
		/// Instructs the XmlSerializer whether or not to generate the XML element
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		[System.ComponentModel.Browsable(false), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public bool SourceSpecified
		{
			get
			{
				return (_source.Url != null && _source.Url.Length>0) || (_source.Value!=null&&_source.Value.Length>0);
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
			public const string Title = "Title";
			public const string Description = "Description";
			public const string Link = "Link";
			public const string Author = "Author";
			public const string Category = "Category";
			public const string PubDate = "PubDate";
			public const string Comments = "Comments";
			public const string Enclosure = "Enclosure";
			public const string Guid = "Guid";
			public const string Source = "Source";
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