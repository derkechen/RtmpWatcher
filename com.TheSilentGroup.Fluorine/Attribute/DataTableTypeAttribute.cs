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
using System.Data;
using System.Collections;
using System.Reflection;
using com.TheSilentGroup.Fluorine.Invocation;

namespace com.TheSilentGroup.Fluorine
{
	/// <summary>
	/// The DataTableTypeAttribute specifies the type of data in a DataTable.
	/// Rows will be serialized as a collection of strongly typed ASObjects (columns as properties).
	/// </summary>
	[AttributeUsage(AttributeTargets.Method , AllowMultiple = true)]
	public class DataTableTypeAttribute : System.Attribute, IInvocationCallback
	{
		string	_remoteClass;
		string	_tableName;
		string	_propertyName;

		/// <summary>
		/// Initializes a new instance of the DataTableTypeAttribute class.
		/// </summary>
		public DataTableTypeAttribute(string remoteClass)
		{
			_remoteClass = remoteClass;
		}

		public DataTableTypeAttribute(string tableName, string remoteClass)
		{
			_remoteClass = remoteClass;
			_tableName = tableName;
		}

		public DataTableTypeAttribute(string tableName, string propertyName, string remoteClass)
		{
			_remoteClass = remoteClass;
			_tableName = tableName;
			_propertyName = propertyName;
		}

		public string RemoteClass{ get{ return _remoteClass; } }

		#region IInvocationCallback Members

		public void OnInvoked(IInvocationManager invocationManager, MethodInfo methodInfo, object obj, object[] arguments, object result)
		{
			if( result is DataSet )
			{
				if( _tableName != null )
				{
					DataSet dataSet = result as DataSet;
					if( dataSet.Tables.Contains(_tableName) )
					{
						DataTable dataTable = dataSet.Tables[_tableName];
						if( _propertyName != null )
							dataTable.ExtendedProperties.Add("alias", _propertyName);
						ArrayList list = ConvertDataTable(dataTable);
						invocationManager.Properties[dataTable] = list;
					}
				}
			}
			if( result is DataTable )
			{
				DataTable dataTable = result as DataTable;
				ArrayList list = ConvertDataTable(dataTable);
				invocationManager.Result = list;
			}
		}

		#endregion

		private ArrayList ConvertDataTable(DataTable dataTable)
		{
			ArrayList result = new ArrayList(dataTable.Rows.Count);
			for(int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				ASObject aso = new ASObject(_remoteClass);
				for(int j = 0; j < dataTable.Columns.Count; j++)
				{
					DataColumn column = dataTable.Columns[j];
					object value = null;
					if( !dataRow.IsNull(column) )
						value = dataRow[column];
					aso.Add(column.ColumnName, dataRow[column]);
				}
				result.Add(aso);
			}
			return result;
		}
	}
}
