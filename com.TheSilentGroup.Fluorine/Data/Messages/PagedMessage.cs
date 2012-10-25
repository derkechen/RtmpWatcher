/*
	Fluorine .NET Flash Remoting Gateway open source library 
	Copyright (C) 2005 Zoltan Csibi, zoltan@TheSilentGroup.com
	
	This library is free software; you can redistribute it and/or
	modify it under the terms of the GNU Lesser General Public
	License as published by the Free Software Foundation; either
	version 2.1 of the License, or (at your option) any later version.
	
	This library is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
	Lesser General Public License for more details.
	
	You should have received a copy of the GNU Lesser General Public
	License along with this library; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/

using System;

namespace com.TheSilentGroup.Fluorine.Data.Messages
{
	/// <summary>
	/// This messsage provides information about a partial sequence result.
	/// When paging is enabled for a destination and DataService.fill() or a page request 
	/// is made the remote destination will return this message as a response.
	/// The body property is an Array containing the items for the requested page with a 
	/// length of the configured page size.
	/// </summary>
	public class PagedMessage : SequencedMessage
	{
		int _pageCount;
		int _pageIndex;

		public PagedMessage()
		{
		}

		/// <summary>
		/// Provides access to the number of total pages in a sequence based on the current page size.
		/// </summary>
		public int pageCount
		{
			get{ return _pageCount; }
			set{ _pageCount = value; }
		}
		/// <summary>
		/// Provides access to the index of the current page in a sequence.
		/// </summary>
		public int pageIndex
		{
			get{ return _pageIndex; }
			set{ _pageIndex = value; }
		}
	}
}
