using Oris;
using System;
using System.Data.Odbc;
using System.Reflection;

namespace Oris.DB_interface
{
	public class Oris_db_context
	{
		public Oris_db_context(string p_odbc_connection_string)
		{
			Odbc_intf.set_odbc_connection_string(p_odbc_connection_string, null);
			PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < (int)properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				Type propertyType = propertyInfo.PropertyType;
                //if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Oris_db_set<>))
                //{
                //    ConstructorInfo constructor = propertyType.GetConstructor(new Type[] { typeof(Oris_db_context) });
                //    object[] objArray = new object[] { this };
                //    propertyInfo.SetValue(this, constructor.Invoke(objArray), null);
                //}
			}
		}

		public string enum_to_string<T>(T p_enum_value)
		{
			Type genericArguments = typeof(T);
			if (genericArguments.BaseType != typeof(Enum))
			{
				if (!genericArguments.IsGenericType || !(genericArguments.GetGenericTypeDefinition() == typeof(Nullable<>)) || !(genericArguments.GetGenericArguments()[0].BaseType == typeof(Enum)))
				{
					return null;
				}
				if (p_enum_value == null)
				{
					return null;
				}
				genericArguments = genericArguments.GetGenericArguments()[0];
			}
			//
            return null;
                //Column_manager.gen_odbc_enum_name(genericArguments.GetEnumName(p_enum_value));
		}

		public bool simple_non_query(string p_query, out int p_rows_affected, params object[] p_params)
		{
			string str;
			p_rows_affected = 0;
			bool flag = true;
			OdbcConnection persistentConnection = Odbc_intf.get_persistent_connection(out str);
			p_rows_affected = Odbc_intf.db_nonquery_ex(persistentConnection, p_query, ref str, p_params);
			if (!string.IsNullOrEmpty(str))
			{
				flag = false;
			}
			Odbc_intf.return_persistent_connection(persistentConnection);
			return flag;
		}

		public bool simple_non_query(string p_query, params object[] p_params)
		{
			int num;
			return this.simple_non_query(p_query, out num, p_params);
		}

		public bool simple_non_query(string p_query)
		{
			int num;
			return this.simple_non_query(p_query, out num, null);
		}
	}
}