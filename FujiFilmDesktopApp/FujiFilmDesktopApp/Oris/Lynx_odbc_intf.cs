using System;
using System.ComponentModel;
using System.Data.Odbc;

namespace Oris
{
	public class Lynx_odbc_intf
	{
		public Lynx_odbc_intf()
		{
		}

		public static int connect(string p_connection_string, ref OdbcConnection p_odbc_connection, ref string p_exception_msg)
		{
			p_odbc_connection = Odbc_intf.db_connect(null, p_connection_string, out p_exception_msg);
			if (p_odbc_connection == null)
			{
				return 0;
			}
			int num = 0;
			string str = null;
			Lynx_odbc_intf.nonquery(p_odbc_connection, "SET @@old_passwords = 0", ref num, ref str);
			return 1;
		}

		public static void disconnect(OdbcConnection p_odbc_connection)
		{
			if (p_odbc_connection != null)
			{
				p_odbc_connection.Close();
				p_odbc_connection.Dispose();
			}
		}

		public static int nonquery(OdbcConnection p_odbc_connection, string p_sql_query, ref int p_affected_rows_count, ref string p_exception_msg)
		{
			string str = null;
			int num = Odbc_intf.db_nonquery_ex(p_odbc_connection, p_sql_query, ref str, null);
			if (str != null)
			{
				p_exception_msg = str;
				return 0;
			}
			p_affected_rows_count = num;
			return 1;
		}

		public static int query(OdbcConnection p_odbc_connection, string p_sql_query, ref Lynx_DB_query p_query_result, ref int p_affected_rows_count, ref string p_exception_msg)
		{
			OdbcDataReader odbcDataReader = Odbc_intf.db_query_ex(p_odbc_connection, p_sql_query, ref p_exception_msg, null);
			if (odbcDataReader == null)
			{
				return 0;
			}
			p_query_result = new Lynx_DB_query(odbcDataReader);
			p_affected_rows_count = odbcDataReader.RecordsAffected;
			return 1;
		}

		public static string Quote(string str)
		{
			char[] chrArray = new char[] { '\\', '\"', '\'' };
			string str1 = null;
			while (true)
			{
				int num = str.IndexOfAny(chrArray);
				if (num >= 0)
				{
					str1 = string.Concat(str1, str.Substring(0, num));
					str1 = string.Concat(str1, "\\");
					int num1 = num;
					num = num1 + 1;
					str1 = string.Concat(str1, str[num1]);
					if (num >= str.Length)
					{
						break;
					}
					str = str.Substring(num);
				}
				else
				{
					str1 = string.Concat(str1, str);
					break;
				}
			}
			return str1;
		}
	}
}