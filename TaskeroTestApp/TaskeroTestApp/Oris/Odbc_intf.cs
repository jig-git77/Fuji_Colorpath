using Oris.DB_interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.Threading;

namespace Oris
{
	public class Odbc_intf
	{
		private static string odbc_connection_string;

		private static List<OdbcConnection> odbc_list;

		private static List<DateTime> odbc_idle_since_list;

		private static Timer odbc_timer;

		private static TimeSpan odbc_connection_timeout;

		static Odbc_intf()
		{
			Odbc_intf.odbc_connection_string = null;
			Odbc_intf.odbc_list = new List<OdbcConnection>();
			Odbc_intf.odbc_idle_since_list = new List<DateTime>();
			Odbc_intf.odbc_connection_timeout = TimeSpan.FromSeconds(600);
		}

		public Odbc_intf()
		{
		}

		public static OdbcConnection db_connect(string p_config_conn_string, string p_writer_conn_string, out string p_error_msg)
		{
			return Odbc_intf.db_connect(p_config_conn_string, p_writer_conn_string, null, out p_error_msg);
		}

		public static OdbcConnection db_connect(string p_config_conn_string, string p_writer_conn_string, string p_reader_conn_string, out string p_error_msg)
		{
			p_error_msg = null;
			bool flag = false;
			if (Odbc_intf.odbc_connection_string == null)
			{
				flag = true;
				Odbc_intf.set_odbc_connection_string(p_config_conn_string, p_writer_conn_string);
			}
			OdbcConnection odbcConnection = null;
			try
			{
				odbcConnection = new OdbcConnection(Odbc_intf.odbc_connection_string);
				odbcConnection.Open();
			}
			catch (Exception exception)
			{
				p_error_msg = exception.Message;
				if (odbcConnection != null)
				{
					odbcConnection.Close();
				}
				odbcConnection = null;
			}
			if (odbcConnection == null && p_reader_conn_string != null && flag)
			{
				try
				{
					odbcConnection = new OdbcConnection(p_reader_conn_string);
					odbcConnection.Open();
				}
				catch (Exception exception1)
				{
					p_error_msg = exception1.Message;
					if (odbcConnection != null)
					{
						odbcConnection.Close();
					}
					odbcConnection = null;
				}
				if (odbcConnection != null)
				{
					Odbc_intf.odbc_connection_string = p_reader_conn_string;
				}
			}
			return odbcConnection;
		}

		public static int db_nonquery(OdbcConnection p_connection, string p_nonquery, params object[] p_params)
		{
			string str = null;
			return Odbc_intf.db_nonquery_ex(p_connection, p_nonquery, ref str, p_params);
		}

		public static int db_nonquery_ex(OdbcConnection p_connection, string p_nonquery, ref string p_error_msg, params object[] p_params)
		{
			int num;
			p_error_msg = null;
			using (OdbcCommand odbcCommand = new OdbcCommand(p_nonquery, p_connection))
			{
				Odbc_intf.set_sql_parameters(odbcCommand, p_params);
				int num1 = 0;
				bool flag = false;
				try
				{
					num1 = odbcCommand.ExecuteNonQuery();
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					p_error_msg = exception.Message;
					if (!(exception is InvalidOperationException) || p_connection.State == ConnectionState.Open)
					{
						num1 = 0;
					}
					else
					{
						flag = true;
					}
				}
				if (flag)
				{
					try
					{
						p_connection.Close();
						p_connection.Open();
						if (p_connection.State == ConnectionState.Open)
						{
							Odbc_intf.db_nonquery_ex(p_connection, "SET @@old_passwords = 0", ref p_error_msg, null);
						}
					}
					catch
					{
					}
					try
					{
						num1 = odbcCommand.ExecuteNonQuery();
					}
					catch (Exception exception2)
					{
						p_error_msg = exception2.Message;
						num1 = 0;
					}
				}
				num = num1;
			}
			return num;
		}

		public static string db_obj_to_string(object p_name_obj)
		{
			if (p_name_obj is string)
			{
				return (string)p_name_obj;
			}
			if (!(p_name_obj is byte[]))
			{
				if (p_name_obj == DBNull.Value)
				{
					return null;
				}
				return "";
			}
			byte[] pNameObj = (byte[])p_name_obj;
			return Encoding.UTF8.GetString(pNameObj, 0, (int)pNameObj.Length);
		}

		public static OdbcDataReader db_query(OdbcConnection p_connection, string p_query, params string[] p_params)
		{
			string str = null;
			return Odbc_intf.db_query_ex(p_connection, p_query, ref str, p_params);
		}

		public static OdbcDataReader db_query_ex(OdbcConnection p_connection, string p_query, ref string p_error_msg, params string[] p_params)
		{
			OdbcDataReader odbcDataReader;
			p_error_msg = null;
			using (OdbcCommand odbcCommand = new OdbcCommand(p_query, p_connection))
			{
				Odbc_intf.set_sql_parameters(odbcCommand, p_params);
				OdbcDataReader odbcDataReader1 = null;
				bool flag = false;
				try
				{
					odbcDataReader1 = odbcCommand.ExecuteReader();
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					p_error_msg = exception.Message;
					if (!(exception is InvalidOperationException) || p_connection.State == ConnectionState.Open)
					{
						odbcDataReader1 = null;
					}
					else
					{
						flag = true;
					}
				}
				if (flag)
				{
					try
					{
						p_connection.Close();
						p_connection.Open();
						if (p_connection.State == ConnectionState.Open)
						{
							Odbc_intf.db_nonquery_ex(p_connection, "SET @@old_passwords = 0", ref p_error_msg, null);
						}
					}
					catch
					{
					}
					try
					{
						odbcDataReader1 = odbcCommand.ExecuteReader();
					}
					catch (Exception exception2)
					{
						p_error_msg = exception2.Message;
						odbcDataReader1 = null;
					}
				}
				odbcDataReader = odbcDataReader1;
			}
			return odbcDataReader;
		}

		public static object db_scalar_query(OdbcConnection p_connection, string p_query, ref string p_error_msg)
		{
			object obj;
			p_error_msg = null;
			object obj1 = null;
			using (OdbcCommand odbcCommand = new OdbcCommand(p_query, p_connection))
			{
				bool flag = false;
				try
				{
					obj1 = odbcCommand.ExecuteScalar();
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					p_error_msg = exception.Message;
					if (!(exception is InvalidOperationException) || p_connection.State == ConnectionState.Open)
					{
						obj1 = null;
					}
					else
					{
						flag = true;
					}
				}
				if (flag)
				{
					try
					{
						p_connection.Close();
						p_connection.Open();
						if (p_connection.State == ConnectionState.Open)
						{
							Odbc_intf.db_nonquery_ex(p_connection, "SET @@old_passwords = 0", ref p_error_msg, null);
						}
					}
					catch
					{
					}
					try
					{
						obj1 = odbcCommand.ExecuteScalar();
					}
					catch (Exception exception2)
					{
						p_error_msg = exception2.Message;
						obj1 = null;
					}
				}
				obj = obj1;
			}
			return obj;
		}

		public static bool get_column_list(OdbcConnection p_connection, string p_table_name, List<Column_description> p_column_list)
		{
			bool flag;
			p_column_list.Clear();
			bool flag1 = false;
			string str = null;
			using (OdbcDataReader odbcDataReader = Odbc_intf.db_query_ex(p_connection, string.Concat("SHOW TABLES LIKE '", p_table_name, "'"), ref str, new string[0]))
			{
				if (!string.IsNullOrEmpty(str))
				{
					flag = false;
					return flag;
				}
				else if (odbcDataReader.HasRows)
				{
					flag1 = true;
				}
			}
			if (!flag1)
			{
				return true;
			}
			using (OdbcDataReader odbcDataReader1 = Odbc_intf.db_query_ex(p_connection, string.Concat("SHOW COLUMNS FROM ", p_table_name), ref str, new string[0]))
			{
				if (string.IsNullOrEmpty(str))
				{
					while (odbcDataReader1.Read())
					{
						Column_description columnDescription = new Column_description()
						{
							m_name = Odbc_intf.db_obj_to_string(odbcDataReader1["Field"]),
							m_default_value = Odbc_intf.db_obj_to_string(odbcDataReader1["Default"])
						};
						if (Odbc_intf.db_obj_to_string(odbcDataReader1["Null"]) == "YES")
						{
							columnDescription.m_allow_null = true;
						}
						string str1 = Odbc_intf.db_obj_to_string(odbcDataReader1["Type"]);
						string str2 = Odbc_intf.db_obj_to_string(odbcDataReader1["Extra"]);
						if (str1.StartsWith("int"))
						{
							if (!str1.EndsWith("unsigned"))
							{
								columnDescription.m_datatype = Column_type.k_int32;
								columnDescription.m_column_size = 4;
								if (str2 == "auto_increment")
								{
									columnDescription.m_default_value = "auto_increment";
								}
							}
							else
							{
								columnDescription.m_datatype = Column_type.k_uint32;
								columnDescription.m_column_size = 4;
							}
						}
						else if (str1.StartsWith("tinyint"))
						{
							if (!str1.EndsWith("unsigned"))
							{
								columnDescription.m_datatype = Column_type.k_int8;
								columnDescription.m_column_size = 1;
							}
							else
							{
								columnDescription.m_datatype = Column_type.k_uint8;
								columnDescription.m_column_size = 1;
							}
						}
						else if (str1 == "datetime")
						{
							columnDescription.m_datatype = Column_type.k_date_time;
							columnDescription.m_column_size = 16;
						}
						else if (str1 == "date")
						{
							columnDescription.m_datatype = Column_type.k_date;
							columnDescription.m_column_size = 16;
						}
						else if (str1 == "timestamp")
						{
							columnDescription.m_datatype = Column_type.k_mysql_timestamp;
							columnDescription.m_column_size = 16;
							columnDescription.m_allow_null = false;
						}
						else if (str1 == "float")
						{
							columnDescription.m_datatype = Column_type.k_float;
						}
						else if (str1 == "text")
						{
							columnDescription.m_datatype = Column_type.k_string;
							columnDescription.m_column_size = 65535;
						}
						else if (str1 == "longtext")
						{
							columnDescription.m_datatype = Column_type.k_string;
							//columnDescription.m_column_size = -1;
						}
						else if (str1 == "blob")
						{
							columnDescription.m_datatype = Column_type.k_byte_array;
						}
						else if (str1.StartsWith("varchar("))
						{
							columnDescription.m_datatype = Column_type.k_string;
							uint.TryParse(str1.Substring(8, str1.Length - 9), out columnDescription.m_column_size);
						}
						else if (str1.StartsWith("enum("))
						{
							columnDescription.m_datatype = Column_type.k_mysql_enum;
							columnDescription.m_set_enum_values = str1.Substring(5, str1.Length - 6);
						}
						else if (!str1.StartsWith("set("))
						{
							columnDescription.m_datatype = Column_type.k_unknown;
						}
						else
						{
							columnDescription.m_datatype = Column_type.k_mysql_set;
							columnDescription.m_set_enum_values = str1.Substring(4, str1.Length - 5);
						}
						columnDescription.m_datatype_name = str1;
						p_column_list.Add(columnDescription);
					}
					return true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		public static bool get_column_list_odbc_via_schema(OdbcConnection p_connection, string p_table_name, List<Column_description> p_column_list)
		{
			DataTable schemaTable = null;
			using (OdbcCommand odbcCommand = new OdbcCommand(string.Concat("SELECT * FROM ", p_table_name), p_connection))
			{
				using (OdbcDataReader odbcDataReader = odbcCommand.ExecuteReader(CommandBehavior.SchemaOnly))
				{
					schemaTable = odbcDataReader.GetSchemaTable();
				}
			}
			if (schemaTable == null)
			{
				return false;
			}
			foreach (DataRow row in schemaTable.Rows)
			{
				Column_description columnDescription = new Column_description()
				{
					m_name = row["ColumnName"] as string
				};
				object item = row["ColumnOrdinal"];
				object obj = row["ColumnSize"];
				object item1 = row["NumericPrecision"];
				object obj1 = row["NumericScale"];
				object item2 = row["ProviderType"];
				object obj2 = row["IsLong"];
				object item3 = row["AllowDBNull"];
				object obj3 = row["IsReadOnly"];
				object item4 = row["IsRowVersion"];
				object obj4 = row["IsUnique"];
				object item5 = row["IsKey"];
				object obj5 = row["IsAutoIncrement"];
				object item6 = row["BaseSchemaName"];
				object obj6 = row["BaseCatalogName"];
				object item7 = row["BaseTableName"];
				object obj7 = row["BaseColumnName"];
				Type type = row["DataType"] as Type;
				if (type == typeof(int))
				{
					columnDescription.m_datatype = Column_type.k_int32;
				}
				else if (type == typeof(long))
				{
					columnDescription.m_datatype = Column_type.k_int64;
				}
				else if (type == typeof(float))
				{
					columnDescription.m_datatype = Column_type.k_float;
				}
				else if (type == typeof(DateTime))
				{
					columnDescription.m_datatype = Column_type.k_date_time;
				}
				else if (type == typeof(byte[]))
				{
					columnDescription.m_datatype = Column_type.k_byte_array;
				}
				else if (type != typeof(string))
				{
					columnDescription.m_datatype = Column_type.k_unknown;
				}
				else
				{
					columnDescription.m_datatype = Column_type.k_string;
				}
				columnDescription.m_column_size = (uint)row["ColumnSize"];
				p_column_list.Add(columnDescription);
			}
			return true;
		}

		public static bool get_db_key_list(OdbcConnection p_connection, string p_table_name, List<Db_key_description> p_db_key_list)
		{
			bool flag;
			string str = null;
			using (OdbcDataReader odbcDataReader = Odbc_intf.db_query_ex(p_connection, string.Concat("SHOW INDEX FROM ", p_table_name), ref str, new string[0]))
			{
				if (string.IsNullOrEmpty(str))
				{
					while (odbcDataReader.Read())
					{
						object item = odbcDataReader["Non_unique"];
						byte num = 0;
						num = (!(item is long) ? (byte)item : (byte)((long)item));
						string str1 = Odbc_intf.db_obj_to_string(odbcDataReader["Key_name"]);
						object obj = odbcDataReader["Seq_in_index"];
						if (!(obj is long))
						{
							byte num1 = (byte)obj;
						}
						else
						{
							long num2 = (long)obj;
						}
						string str2 = Odbc_intf.db_obj_to_string(odbcDataReader["Column_name"]);
						Db_key_description dbKeyDescription = null;
						int num3 = 0;
						while (num3 < p_db_key_list.Count)
						{
							if (p_db_key_list[num3].m_key_name != str1)
							{
								num3++;
							}
							else
							{
								dbKeyDescription = p_db_key_list[num3];
								break;
							}
						}
						if (dbKeyDescription != null)
						{
							Db_key_description dbKeyDescription1 = dbKeyDescription;
							dbKeyDescription1.m_index_columns = string.Concat(dbKeyDescription1.m_index_columns, ",", str2);
						}
						else
						{
							dbKeyDescription = new Db_key_description();
							if (str1 == "PRIMARY")
							{
								dbKeyDescription.m_key_type = Db_key_type.k_primary_key;
							}
							else if (num != 0)
							{
								dbKeyDescription.m_key_type = Db_key_type.k_index;
							}
							else
							{
								dbKeyDescription.m_key_type = Db_key_type.k_unique_key;
							}
							dbKeyDescription.m_key_name = str1;
							dbKeyDescription.m_index_columns = str2;
							p_db_key_list.Add(dbKeyDescription);
						}
					}
					return true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		public static string get_odbc_connection_string()
		{
			return Odbc_intf.odbc_connection_string;
		}

		public static OdbcConnection get_persistent_connection(out string p_error_msg)
		{
			p_error_msg = null;
			if (Odbc_intf.odbc_connection_string == null)
			{
				p_error_msg = "get_persistent_connection - no connection string";
				return null;
			}
			OdbcConnection item = null;
			lock (Odbc_intf.odbc_list)
			{
				int num = 0;
				while (num < Odbc_intf.odbc_list.Count)
				{
					if (Odbc_intf.odbc_list[num] == null)
					{
						num++;
					}
					else
					{
						item = Odbc_intf.odbc_list[num];
						Odbc_intf.odbc_list[num] = null;
						break;
					}
				}
			}
			if (item != null)
			{
				return item;
			}
			try
			{
				item = new OdbcConnection(Odbc_intf.odbc_connection_string);
				item.Open();
				if (item.State == ConnectionState.Open)
				{
					Odbc_intf.db_nonquery_ex(item, "SET @@old_passwords = 0", ref p_error_msg, null);
				}
			}
			catch (Exception exception)
			{
				p_error_msg = exception.Message;
				if (item != null)
				{
					item.Close();
				}
				item = null;
			}
			return item;
		}

		public static void get_tables(OdbcConnection p_connection, List<string> p_table_list)
		{
			foreach (DataRow row in p_connection.GetSchema("Tables").Rows)
			{
				if (row["TABLE_TYPE"] as string != "TABLE")
				{
					continue;
				}
				p_table_list.Add(row["TABLE_NAME"] as string);
			}
		}

		private static void odbc_timer_action(object p_state)
		{
			while (true)
			{
				TimeSpan zero = TimeSpan.Zero;
				OdbcConnection item = null;
				lock (Odbc_intf.odbc_list)
				{
					for (int i = 0; i < Odbc_intf.odbc_list.Count; i++)
					{
						if (Odbc_intf.odbc_list[i] != null)
						{
							TimeSpan now = DateTime.Now - Odbc_intf.odbc_idle_since_list[i];
							if (now < Odbc_intf.odbc_connection_timeout)
							{
								TimeSpan odbcConnectionTimeout = Odbc_intf.odbc_connection_timeout - now;
								if (zero == TimeSpan.Zero || odbcConnectionTimeout < zero)
								{
									zero = odbcConnectionTimeout;
								}
							}
							else
							{
								item = Odbc_intf.odbc_list[i];
								Odbc_intf.odbc_list[i] = null;
								break;
							}
						}
					}
					if (item == null)
					{
						if (zero == TimeSpan.Zero)
						{
							zero = Odbc_intf.odbc_connection_timeout;
						}
						Odbc_intf.odbc_timer.Change((int)zero.TotalMilliseconds, -1);
					}
				}
				if (item == null)
				{
					break;
				}
				item.Close();
				item.Dispose();
			}
		}

		public static void return_persistent_connection(OdbcConnection p_odbc_connection)
		{
			OdbcConnection pOdbcConnection = p_odbc_connection;
			if (pOdbcConnection == null)
			{
				return;
			}
			lock (Odbc_intf.odbc_list)
			{
				int now = 0;
				while (now < Odbc_intf.odbc_list.Count)
				{
					if (Odbc_intf.odbc_list[now] != null)
					{
						now++;
					}
					else
					{
						Odbc_intf.odbc_list[now] = pOdbcConnection;
						Odbc_intf.odbc_idle_since_list[now] = DateTime.Now;
						pOdbcConnection = null;
						break;
					}
				}
				if (pOdbcConnection != null)
				{
					Odbc_intf.odbc_list.Add(pOdbcConnection);
					Odbc_intf.odbc_idle_since_list.Add(DateTime.Now);
				}
			}
		}

		public static void set_odbc_connection_string(string p_config_conn_string, string p_writer_conn_string)
		{
			if (Odbc_intf.odbc_connection_string != null)
			{
				return;
			}
			if (p_config_conn_string != null)
			{
				ConnectionStringSettings item = ConfigurationManager.ConnectionStrings[p_config_conn_string];
				if (item != null)
				{
					Odbc_intf.odbc_connection_string = item.ConnectionString;
				}
			}
			if (Odbc_intf.odbc_connection_string == null)
			{
				Odbc_intf.odbc_connection_string = p_writer_conn_string;
			}
			lock (Odbc_intf.odbc_list)
			{
				if (Odbc_intf.odbc_timer == null)
				{
					Odbc_intf.odbc_timer = new Timer(new TimerCallback(Odbc_intf.odbc_timer_action), null, (int)Odbc_intf.odbc_connection_timeout.TotalMilliseconds, -1);
				}
			}
		}

		private static void set_sql_parameters(OdbcCommand p_odbc_command, params object[] p_params)
		{
			if (p_params != null)
			{
				for (int i = 0; i < (int)p_params.Length; i++)
				{
					if (p_params[i] == null)
					{
						int value = i + 1;
						p_odbc_command.Parameters.Add(string.Concat("param", value.ToString()), OdbcType.VarChar).Value = DBNull.Value;
					}
					else if (p_params[i] is string)
					{
						int pParams = i + 1;
						p_odbc_command.Parameters.Add(string.Concat("param", pParams.ToString()), OdbcType.VarChar).Value = p_params[i];
					}
					else if (!(p_params[i] is DateTime))
					{
						int num = i + 1;
						p_odbc_command.Parameters.Add(string.Concat("param", num.ToString()), OdbcType.Int).Value = p_params[i];
					}
					else
					{
						int pParams1 = i + 1;
						p_odbc_command.Parameters.Add(string.Concat("param", pParams1.ToString()), OdbcType.DateTime).Value = p_params[i];
					}
				}
			}
		}
	}
}