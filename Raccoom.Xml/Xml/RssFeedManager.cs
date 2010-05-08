
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
	/// Rss Cache Manager handles the following things
	/// <list type="bullet">
	/// <item>Rss xml cache</item>
	/// <item>Opml based favorites</item>
	/// <item>Opml based favorite history</item>
	/// </list>
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(true), System.Runtime.InteropServices.Guid("028FF54F-98DF-4879-A355-880832C49A1C")]
	[System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
	[System.Runtime.InteropServices.ProgId("Raccoom.RssFeedManager")]
	public class RssFeedManager
	:	ConcreteRssFactory
	{
		#region fields
		
		internal delegate void OutlineCallbackDelegate (OpmlOutlineFavorite item);
		
		/// <summary>opml based favorite's document</summary>
		private readonly OpmlDocumentFavorites _favoritesDocument;
		private readonly OpmlDocumentFavorites _historyDocument = new OpmlDocumentFavorites ( );
		/// <summary>non persistent rss channel cache list</summary>
		private System.Collections.Hashtable _rssChannelCacheList = new System.Collections.Hashtable ( );
		private string _cachePath = string.Empty;
		
		#endregion
		
		#region constructors
		
		/// <summary>
		/// Initializes a new instance of RssFeedManager using the specified favorites.
		/// </summary>
		/// <param name="opmlDocument">The opml document which holds the managed favorites</param>
		public RssFeedManager (string cacheRootPath, OpmlDocumentFavorites favoriteDocument)
		{
			_cachePath = System.IO.Path.Combine(cacheRootPath, "Cache");
			if(!System.IO.Directory.Exists(_cachePath)) System.IO.Directory.CreateDirectory(_cachePath);
			//
			_favoritesDocument = favoriteDocument;
			//
			BuildHistory();
		}
		
		#endregion
		
		#region public interface
		
		/// <summary>
		/// RssFeedManager implements the operations to create <see cref="RssChannel"/> objects.
		/// This class is responsible to manage the cache and is build to work with <see cref="OpmlOutlineFavorite"/> outline.
		/// </summary>
		/// <param name="outline">The <see cref="OpmlOutlineFavorite"/> of the resource to receive the data.</param>
		/// <returns>The requested see cref="IRssChannel" instance</returns>		
		public IRssChannel GetFeed (OpmlOutlineFavorite outline)
		{
			Uri uri = new Uri(outline.XmlUrl);
			IRssChannel rssChannel = null;
			//System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			try
			{
				// never fetched before
				if(outline.ExpireDate == DateTime.MinValue || outline.ExpireDate < DateTime.Now)
				{
					// fetch channel from web
					rssChannel = InternalGetFeed(uri);
					// save channel to cache
					rssChannel.Save(GetCachePathFromUri(uri));					
					//
					if(rssChannel.Ttl > 0)
					{
						outline.ExpireDate = DateTime.Now.AddMinutes(rssChannel.Ttl);					
					}
					else
					{
						outline.ExpireDate = DateTime.Now.AddMinutes(3600);				
					}
					//
					outline.State = CacheState.Cached;
					//
					// add/refresh loaded channel to internal non persistent cache list
					_rssChannelCacheList[outline] = rssChannel;
				}
				// check cache
				else
				{				
					// cached and not out of date
					if (_rssChannelCacheList.Contains(outline))
					{
						rssChannel = _rssChannelCacheList[outline] as IRssChannel;
					}
						// cached but out of date, refresh
					else
					{
						// fetch channel from web
						rssChannel = InternalGetFeed(new Uri(GetCachePathFromUri(uri)));
						outline.State = CacheState.Cached;
						// update cache list
						_rssChannelCacheList[outline] = rssChannel;
					}
				}
			} 
			catch (System.Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				outline.State = CacheState.Error;
				throw e;
			}
			finally
			{	
				//System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			}
			//
			outline.TimesVisited++;
			outline.LastVisited = DateTime.Now;
			UpdateHistory(outline);
			//
			return rssChannel;
		}
		
		/// <summary>
		/// Refresh all managed favorite's
		/// </summary>
		public void Refresh ()
		{
			new RecursiveWorkerThread(_favoritesDocument.Body.Items, new OutlineCallbackDelegate(RefreshOutline));
		}
		
		/// <summary>
		/// Reset all managed favorite's to it's default values
		/// </summary>
		public void Reset ()
		{
			new RecursiveWorkerThread(_favoritesDocument.Body.Items, new OutlineCallbackDelegate(ResetOutline));
		}
		
		public System.IO.DirectoryInfo CachePath
		{
			get
			{
				return new System.IO.DirectoryInfo(_cachePath);
			}
		}
		
		#endregion
		
		#region private interface
		
		/// <summary>
		/// Reset the favorite to it's default values.
		/// </summary>
		/// <param name="item"></param>
		private void ResetOutline (OpmlOutlineFavorite item)
		{
			System.IO.File.Delete( GetCachePathFromUri(new Uri(item.XmlUrl)));
			item.State = CacheState.None;
			item.TimesVisited = 0;
			item.LastVisited = DateTime.MinValue;
		}
		
		private void RefreshOutline (OpmlOutlineFavorite item)
		{
			if(item.State == CacheState.Cached) return;
			//
			this.GetFeed(item);
		}
		
		/// <summary>
		/// Creates the histroy for the current favorites
		/// </summary>
		private void BuildHistory ()
		{
			try
			{				
				new RecursiveWorkerThread(this._favoritesDocument.Body.Items, new OutlineCallbackDelegate(UpdateHistory));
			} 
			catch (System.Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}
		
		/// <summary>
		/// Updates the history with the specified item
		/// </summary>
		/// <param name="item"></param>
		private void UpdateHistory (OpmlOutlineFavorite item)
		{
			if(item.State!= CacheState.Cached) return;
			//
			if(FindOutline(item, _historyDocument.Body.Items)) return;
			//			
			OpmlOutlineFavorite historyDayItem = null;
			foreach(OpmlOutlineFavorite historyItem in _historyDocument.Body.Items)
			{
				TimeSpan span = historyItem.LastVisited.Subtract(item.LastVisited);
				if(span.Days==0)
				{
					historyDayItem = historyItem;
					break;
				}
			}
			// create on demand
			if(historyDayItem==null)
			{
				historyDayItem = new OpmlOutlineFavorite();
				historyDayItem.LastVisited = item.LastVisited;
				historyDayItem.Text = item.LastVisited.ToString("dddd");				
		 		//
				_historyDocument.Body.Items.Add(historyDayItem);
			}
			// add new item			
			historyDayItem.Items.Add(item);			
		}
		
		/// <summary>
		/// Get the feed from the origin uri
		/// </summary>
		/// <param name="uri">The URI of the resource to receive the data.</param>
		/// <returns>IRssChannel instance</returns>
		private IRssChannel InternalGetFeed (Uri uri)
		{
			return base.GetFeed(uri);
		}
		
		/// <summary>
		/// Opml document that holds the favorites
		/// </summary>
		internal OpmlDocumentFavorites Favorites
		{
			get
			{
				return _favoritesDocument;
			}
		}
		
		/// <summary>
		/// Opml document that holds the histroy
		/// </summary>
		public OpmlDocumentFavorites History
		{
			get
			{
				return _historyDocument;
			}
		}
		
		/// <summary>
		/// Recursive contains over the specified collection to find the outline item.
		/// </summary>
		/// <param name="uri">The URI of the outline item to find.</param>
		/// <param name="collection">RootCollection to search in.</param>
		/// <returns>True if the collection's hold this instance, otherwise False if not found.</returns>
		private bool FindOutline (OpmlOutlineFavorite item, OpmlOutlineCollection collection)
		{
			if(collection.Contains(item)) return true;
			//
			foreach(OpmlOutlineFavorite o in collection)
			{				
				// recursive call
				if(FindOutline(item,o.Items)) return true;				
			}
			return false;
		}
		
		/// <summary>
		/// Find the outline item that holds the requested uri.
		/// </summary>
		/// <param name="uri">The URI of the outline item to find.</param>
		/// <param name="collection">RootCollection to search in.</param>
		/// <returns></returns>
		private OpmlOutlineFavorite FindOutlineByUri (Uri uri, System.Collections.ICollection collection)
		{
			foreach(OpmlOutlineFavorite o in collection)
			{
				if(object.Equals(o.XmlUrl, uri.AbsoluteUri))
				{
					return o;
				}
				// recursive call
				OpmlOutlineFavorite outline = FindOutlineByUri(uri, o.Items);
				if(outline!=null) return outline;
			}
			return null;
		}
		
		/// <summary>
		/// Gets the local cache path mapping from uri
		/// </summary>
		/// <param name="uri">The URI of the resource to receive the data.</param>
		/// <returns></returns>
		private string GetCachePathFromUri (System.Uri uri)
		{
			string filename = uri.GetHashCode().ToString();
			filename = filename.Substring(1, filename.Length-1);
			return System.IO.Path.Combine(_cachePath, filename+".xml");
		}
		
		#endregion
		
		#region nested classes
		
		internal class RecursiveWorkerThread
		{
			private OpmlOutlineCollection _collection;
			private OutlineCallbackDelegate _callback;
			private System.Collections.Hashtable _exceptionTable = new System.Collections.Hashtable ( );
			private System.Threading.Thread thread = null;
			
			internal RecursiveWorkerThread (OpmlOutlineCollection collection, OutlineCallbackDelegate callback)
			{
				_collection = collection;
				_callback = callback;
				// let's go
				//System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Start));
				System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(Run));
				thread.IsBackground = true;
				thread.Start();
			}
			
			private void Run ()
			{
				RecursiveWorker(_collection, _callback);			
			}
			
			public bool IsAlive
			{
				get
				{
					return thread.IsAlive;
				}
			}
			
			/// <summary>
			/// Calls the specified delegate recursively for each outline element
			/// </summary>
			/// <param name="collection">Collection with outline items</param>
			/// <param name="callback">Delegate to call for each item</param>
			private void RecursiveWorker (OpmlOutlineCollection collection, OutlineCallbackDelegate callback)
			{
				//
				for(int i = 0;i<collection.Count;i++)
				//foreach(OpmlOutlineFavorite o in collection)
				{
					OpmlOutlineFavorite o = (OpmlOutlineFavorite) collection[i];
					//
					if(o.Items.Count > 0)
					{				
						RecursiveWorker(o.Items, callback);				
					} 
					else
					{
						try
						{
							callback(o);
						}
						catch(System.Exception e)
						{
							_exceptionTable.Add(o, e);
						}
					}
				}
			}
		}
		
		#endregion
	}
	
	/// <summary>
	/// Opml Document to hold favorite rss feed url's
	/// </summary>
	[XmlRoot("opml"), Serializable]
	public class OpmlDocumentFavorites
	:	OpmlDocument
	{
		/// <summary>
		/// Initializes a new instance of OpmlDocumentFavorites
		/// </summary>
		public OpmlDocumentFavorites (): base()
		{
		
		}
		
		/// <summary>
		/// Initializes a new instance of OpmlDocumentFavorites
		/// </summary>
		/// <param name="uri">The URI of the resource to receive the data.</param>
		public OpmlDocumentFavorites (Uri uri): base(uri)
		{
		
		}
		
		/// <summary>
		/// Write the object to the specified stream.
		/// </summary>
		/// <param name="stream">The Stream used to write the XML document.</param>
		public override void Save (System.IO.Stream stream)
		{
			try
			{	
				XmlTextWriter writer = new XmlTextWriter (stream, System.Text.Encoding.UTF8);
				//Use indenting for readability.
				writer.Formatting = Formatting.Indented;
				//Write the XML delcaration. 
				writer.WriteStartDocument();
				// determine type of Outline
				//
				System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(this.GetType(), new Type[] {typeof(OpmlOutlineFavorite)});
				ser.Serialize(writer, this);												
				writer.WriteEndDocument();
				writer.Flush();
			} 
			finally
			{				
				//if(writer!=null) writer.Close();
			}
		}
		
		/// <summary>
		/// Initializes a new instance of OpmlOutlineFavorite
		/// </summary>
		/// <param name="xmlTextReader"></param>
		/// <returns></returns>
		protected override OpmlOutline OnCreateOutline (XmlReader xmlTextReader)
		{
			return new OpmlOutlineFavorite(xmlTextReader);
		}
	}
	
	/// <summary>
	/// Favorite rss feed outline item
	/// </summary>
	[Serializable]
	public class OpmlOutlineFavorite
	:	OpmlOutline
	{
		#region fields
		
		private CacheState _cacheState;
		private DateTime _expireDate;
		private DateTime _tastVisited;
		private int _timesVisited;
		
		#endregion
		
		#region constructors
		
		/// <summary>
		/// Initializes a new instance of OpmlOutlineFavorite
		/// </summary>		
		public OpmlOutlineFavorite (): base()
		{
			Reset();
		}
		
		/// <summary>
		/// Initializes a new instance of OpmlDocumentFavorites
		/// </summary>
		public OpmlOutlineFavorite (XmlReader xmlTextReader): base(xmlTextReader)
		{
		
		}
		
		#endregion
		
		#region public interface
		
		/// <summary>
		/// Gets or sets the state cache state for this rss channel.
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("cacheState")]
		public CacheState State
		{
			get
			{
				if(DateTime.Now > this.ExpireDate)
				{
					_cacheState = CacheState.Expired;
				}
				return _cacheState;
			}
			
			set
			{
				_cacheState = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the expire date for this rss channel.
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("expireDate")]
		public DateTime ExpireDate
		{
			get
			{
				return _expireDate;
			}
			
			set
			{
				bool changed = !object.Equals(_expireDate, value);
				_expireDate = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.ExpireDate));
			}
		}
		
		/// <summary>
		/// Gets or sets the last visited date for this rss channel.
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("lastVisited")]
		public DateTime LastVisited
		{
			get
			{
				return _tastVisited;
			}
			
			set
			{
				bool changed = !object.Equals(_expireDate, value);
				_tastVisited = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.LastVisited));
			}
		}
		
		/// <summary>
		/// Gets or sets the times visited counter for this rss channel
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("timesVisited")]
		public int TimesVisited
		{
			get
			{
				return _timesVisited;
			}
			
			set
			{
				bool changed = !object.Equals(_timesVisited, value);
				_timesVisited = value;
				if(changed) OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(Fields.ClickCounter));
			}
		}
		
		protected internal virtual void Reset ()
		{
			this.ExpireDate = DateTime.MinValue;
			this.LastVisited = DateTime.MinValue;
			this.State = CacheState.None;
		}
		
		#endregion
		
		#region nested classes
		
		/// <summary>
		/// public writeable class properties
		/// </summary>		
		internal new struct Fields
		{
			public const string ClickCounter = "ClickCounter";
			public const string ExpireDate = "ExpireDate";
			public const string LastVisited = "LastVisited";
		}
		
		#endregion
	}
	
	/// <summary>
	/// Defines states for <see cref="OpmlOutlineFavorite"/>.
	/// </summary>
	public enum CacheState
	{
		/// <summary>The item is stored in the cache</summary>
		Cached,
		/// <summary>There was an error while loading</summary>
		Error,
		/// <summary>The cached item is out of date</summary>
		Expired,
		/// <summary>The item is untouched</summary>
		None
	}
}