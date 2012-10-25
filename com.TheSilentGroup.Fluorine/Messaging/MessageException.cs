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

using com.TheSilentGroup.Fluorine.Messaging.Messages;

namespace com.TheSilentGroup.Fluorine.Messaging
{
	/// <summary>
	/// The MessageException class is used to report exceptions within the messaging system.
	/// </summary>
	public class MessageException : ApplicationException
	{
		ASObject _extendedData;
		string _faultCode = "Server.Processing";
		object _rootCause;

		/// <summary>
		/// Initializes a new instance of the MessageException class.
		/// </summary>
		public MessageException()
		{
			_extendedData = new ASObject();
		}
		/// <summary>
		/// Initializes a new instance of the MessageException class.
		/// </summary>
		/// <param name="extendedData">Additional information.</param>
		public MessageException(ASObject extendedData)
		{
			_extendedData = extendedData;
		}
		/// <summary>
		/// Initializes a new instance of the MessageException class.
		/// </summary>
		/// <param name="inner">Reference to the inner exception that is the cause of this exception.</param>
		public MessageException(Exception inner):base(inner.Message, inner)
		{
			_extendedData = new ASObject();
			_rootCause = inner;
		}
		/// <summary>
		/// Initializes a new instance of the MessageException class.
		/// </summary>
		/// <param name="inner">Reference to the inner exception that is the cause of this exception.</param>
		/// <param name="extendedData">Additional information.</param>
		public MessageException(Exception inner, ASObject extendedData):base(inner.Message, inner)
		{
			_extendedData = extendedData;
			_rootCause = inner;
		}
		/// <summary>
		/// Initializes a new instance of the MessageException class with a specified error message.
		/// </summary>
		/// <param name="extendedData">Additional information.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>			
		public MessageException(ASObject extendedData, string message) : base(message)
		{
			_extendedData = extendedData;
		}
		/// <summary>
		/// Initializes a new instance of the MessageException class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="extendedData">Additional information.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">Reference to the inner exception that is the cause of this exception.</param>
		/// <remarks>An exception that is thrown as a direct result of a previous exception should include a reference to the previous exception in the InnerException property. The InnerException property returns the same value that is passed into the constructor, or a null reference (Nothing in Visual Basic) if the InnerException property does not supply the inner exception value to the constructor.</remarks>			
		public MessageException(ASObject extendedData, string message, Exception inner) : base(message, inner)																 
		{																 
			_extendedData = extendedData;
			_rootCause = inner;
		}

		/// <summary>
		/// Gets or sets the fault code for the error.
		/// </summary>
		public string FaultCode
		{
			get{ return _faultCode; }
			set{ _faultCode = value; }
		}
		/// <summary>
		/// Root cause for the error.
		/// </summary>
		public object RootCause
		{
			get{ return _rootCause; }
			set{ _rootCause = value; }
		}

		/// <summary>
		/// Return additional information to the client as part of a message exception.
		/// </summary>
		public ASObject ExtendedData
		{
			get{ return _extendedData; }
		}

		internal ErrorMessage GetErrorMessage()
		{
			ErrorMessage errorMessage = new ErrorMessage();
			errorMessage.faultCode = this.FaultCode;
			errorMessage.faultString = this.Message;
			if( this.InnerException != null )
			{
				errorMessage.faultDetail = this.InnerException.StackTrace;
				if( this.ExtendedData != null )
					this.ExtendedData["FluorineStackTrace"] = this.StackTrace;
			}
			else
				errorMessage.faultDetail = this.StackTrace;
			errorMessage.extendedData = this.ExtendedData;
			if( this.RootCause != null )
				errorMessage.rootCause = this.RootCause;
			return errorMessage;
		}
	}
}