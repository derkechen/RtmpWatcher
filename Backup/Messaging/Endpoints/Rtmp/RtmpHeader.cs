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

namespace com.TheSilentGroup.Fluorine.Messaging.Endpoints.Rtmp
{
	/// <summary>
	/// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
	/// </summary>
	sealed class RtmpHeader
	{
		byte _channelId = 0;
		int _timer = 0;
		int _size = 0;
		DataType _headerDataType = 0;
		int _streamId = 0;
		bool _timerRelative = true;

		public RtmpHeader()
		{
		}

		public byte ChannelId
		{
			get{ return _channelId; }
			set{ _channelId = value; }
		}

		public DataType DataType
		{
			get{ return _headerDataType; }
			set{ _headerDataType = value; }
		}

		public int Size
		{
			get{ return _size; }
			set{ _size = value; }
		}

		public int StreamId
		{
			get{ return _streamId; }
			set{ _streamId = value; }
		}

		public int Timer
		{
			get{ return _timer; }
			set{ _timer = value; }
		}

		public bool IsTimerRelative
		{
			get{ return _timerRelative; }
			set{ _timerRelative = value; }
		}

		public static int GetHeaderLength(HeaderType headerType) 
		{
			switch (headerType) 
			{
				case HeaderType.HeaderNew:
					return 12;
				case HeaderType.HeaderSameSource:
					return 8;
				case HeaderType.HeaderTimerChange:
					return 4;
				case HeaderType.HeaderContinue:
					return 1;
				default:
					return -1;
			}
		}
	}
}
