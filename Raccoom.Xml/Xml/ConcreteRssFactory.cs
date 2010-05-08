
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
	/// ConcreteRssFactory implements the operations to create <see cref="RssChannel"/> objects
	/// </summary>
	public class ConcreteRssFactory
	:	RssFactory
	{
		#region fields
		
		/// <summary>web proxy to use when get feeds from da web</summary>
		private System.Net.WebProxy _webProxy;
		
		#endregion
		
		#region constructors
		
		/// <summary>Initializes a new instance of ConcreteRssFactory</summary>
		public ConcreteRssFactory ()
		{
		
		}
		
		#endregion
		
		#region public interface
		
		/// <summary>
		/// Gets or sets the WebProxy that is used to connect to the network, can be null
		/// </summary>
		public System.Net.WebProxy Proxy
		{
			get
			{
				return _webProxy;
			}
			
			set
			{
				_webProxy = value;
			}
		}
		
		/// <summary>
		/// Gets (create) the requested <see cref="RssChannel"/> instance
		/// </summary>
		/// <param name="uri">The URI of the resource to receive the data.</param>
		/// <returns>The requested see cref="IRssChannel" instance</returns>		
		public override IRssChannel GetFeed (Uri uri)
		{
			return new RssChannel(uri, this._webProxy);
		}
		
		#endregion
	}
}