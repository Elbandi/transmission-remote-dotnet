
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
	public interface IOpmlBody
	{
		/// <summary>Gets the document that the outline is assigned to.</summary>
		IOpmlDocument Document
		{
			get; 
		}
		
		/// <summary>Outline elements.</summary>
		System.Collections.ICollection Items
		{
			get; 
		}
		
		// end Items
	}
	
	/// <summary>Opml is an XML element, with a single required attribute, version; a head element and a body element, both of which are required. The version attribute is a version string, of the form, x.y, where x and y are both numeric strings.</summary>
	public interface IOpmlDocument
	:	IPersistentManager
	{
		/// <summary>A head contains zero or more optional elements</summary>
		IOpmlHead Head
		{
			get; 
			set; 
		}
		
		// end Head
		
		/// <summary>A body contains one or more outline elements</summary>
		IOpmlBody Body
		{
			get; 
			set; 
		}
		
		// end
	}
	
	/// <summary>A head contains zero or more optional elements</summary>
	public interface IOpmlHead
	{
		/// <summary>Gets the document that the outline is assigned to.</summary>
		IOpmlDocument Document
		{
			get; 
		}
		
		/// <summary>The title of the document.</summary>
		string Title
		{
			get; 
			set; 
		}
		
		// end Title
		
		/// <summary>date-time, indicating when the document was created.</summary>
		DateTime DateCreated
		{
			get; 
			set; 
		}
		
		// end DateCreated
		
		/// <summary>Date-time, indicating when the document was last modified.</summary>
		DateTime DateModified
		{
			get; 
			set; 
		}
		
		// end DateModified
		
		/// <summary>the owner of the document.</summary>
		string OwnerName
		{
			get; 
			set; 
		}
		
		// end OwnerName
		
		/// <summary>the email address of the owner of the document.</summary>
		string OwnerEmail
		{
			get; 
			set; 
		}
		
		// end OwnerEmail
		
		/// <summary>comma-separated list of line numbers that are expanded. The line numbers in the list tell you which headlines to expand. The order is important. For each element in the list, X, starting at the first summit, navigate flatdown X times and expand. Repeat for each element in the list</summary>
		string ExpansionState
		{
			get; 
			set; 
		}
		
		// end ExpansionState
		
		/// <summary>is a number, saying which line of the outline is displayed on the top line of the window. This number is calculated with the expansion state already applied.</summary>
		int VertScrollState
		{
			get; 
			set; 
		}
		
		// end VertScrollState
		
		/// <summary>is a number, the pixel location of the top edge of the window.</summary>
		int WindowTop
		{
			get; 
			set; 
		}
		
		// end WindowTop
		
		/// <summary>is a number, the pixel location of the left edge of the window.</summary>
		int WindowLeft
		{
			get; 
			set; 
		}
		
		// end WindowLeft
		
		/// <summary>is a number, the pixel location of the bottom edge of the window</summary>
		int WindowBottom
		{
			get; 
			set; 
		}
		
		// end WindowBottom
		
		/// <summary>is a number, the pixel location of the right edge of the window</summary>
		int WindowRight
		{
			get; 
			set; 
		}
		
		// end WindowRight
	}
	
	/// <summary>An outline is an XML element, possibly containing one or more attributes, and containing any number of outline sub-elements.</summary>
	public interface IOpmlOutline
	{
		/// <summary>Gets the document that the outline is assigned to.</summary>
		IOpmlDocument Document
		{
			get; 
		}
		
		/// <summary>Gets the outline that this outline is assigned to.</summary>
		/// <remarks>
		/// If the outline is at the root level, the Parent property returns null. 
		/// </remarks>
		IOpmlOutline Parent
		{
			get; 
		}
		
		/// <summary>Text is the string of characters that's displayed when the outline is being browsed or edited. There is no specific limit on the length of the text attribute.</summary>
		string Text
		{
			get; 
			set; 
		}
		
		// end Text
		
		/// <summary>Type is a string, it says how the other attributes of the outline are interpreted</summary>
		string Type
		{
			get; 
			set; 
		}
		
		// end Type
		
		/// <summary></summary>
		string Description
		{
			get; 
			set; 
		}
		
		// end Description
		
		/// <summary>Gets or sets the favorite url.</summary>
		string XmlUrl
		{
			get; 
			set; 
		}
		
		// end XmlUrl
		
		/// <summary></summary>
		string HtmlUrl
		{
			get; 
			set; 
		}
		
		// end HtmlUrl
		
		/// <summary>IsComment is a string, either true or false, indicating whether the outline is commented or not. By convention if an outline is commented, all subordinate outlines are considered to be commented as well. If it's not present, the value is false.</summary>
		bool IsComment
		{
			get; 
			set; 
		}
		
		// end IsComment
		
		/// <summary>IsBreakpoint is a string, either true or false, indicating whether a breakpoint is set on this outline. This attribute is mainly necessary for outlines used to edit scripts that execute. If it's not present, the value is false.</summary>
		bool IsBreakpoint
		{
			get; 
			set; 
		}
		
		// end IsBreakpoint
		
		/// <summary>Outline elements.</summary>
		System.Collections.ICollection Items
		{
			get; 
		}
		
		// end Items
	}
	
	/// <summary>
	/// Abstract Factory for IOpmlDocument implementation's
	/// </summary>
	public abstract class OpmlFactory
	{
		public abstract IOpmlDocument GetDocument (Uri uri);
	}
}