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
using com.TheSilentGroup.Fluorine.Messaging.Endpoints.Rtmp.Api.Persistence;
using com.TheSilentGroup.Fluorine.Messaging.Endpoints.Rtmp.Api.Service;

namespace com.TheSilentGroup.Fluorine.Messaging.Endpoints.Rtmp.Api
{
	/// <summary>
	/// Summary description for IContext.
	/// </summary>
	public interface IContext
	{
		/// <summary>
		/// Returns scope by path. You can think of IScope as of tree items, used to
		/// separate context and resources between users.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		IScope ResolveScope(string path);
		/// <summary>
		/// Returns global scope reference.
		/// </summary>
		/// <returns></returns>
		IScope GetGlobalScope();
		/// <summary>
		/// Get client registry. Client registry is a place where all clients are registered.
		/// </summary>
		/// <returns></returns>
		IClientRegistry GetClientRegistry();
		/// <summary>
		/// Gets persistence store object, a storage for persistent objects like
		/// persistent SharedObjects.
		/// </summary>
		/// <returns></returns>
		IPersistenceStore GetPersistanceStore();
		/// <summary>
		/// Returns scope handler (object that handler all actions related to the scope) by path.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		IScopeHandler LookupScopeHandler(string path);
		/// <summary>
		/// Returns service invoker object. Service invokers are objects that make
		/// service calls to client side NetConnection objects.
		/// </summary>
		/// <returns></returns>
		IServiceInvoker GetServiceInvoker();
	}
}