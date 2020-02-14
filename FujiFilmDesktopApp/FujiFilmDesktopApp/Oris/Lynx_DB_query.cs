using System;
using System.Data.Odbc;

namespace Oris
{
	public class Lynx_DB_query
	{
		private OdbcDataReader m_reader;

		private bool m_have_data;

		public Lynx_DB_query(OdbcDataReader p_reader)
		{
			this.m_reader = p_reader;
			this.m_have_data = this.m_reader.Read();
		}

		private int Fetch(int iCol, ref string p_str)
		{
			object item = this.m_reader[iCol];
			if (!(item is string))
			{
				if (item == DBNull.Value)
				{
					p_str = string.Empty;
					return 0;
				}
				if (!(item is DateTime))
				{
					p_str = string.Empty;
				}
				else
				{
					p_str = ((DateTime)item).ToString("yyyy-MM-dd HH:mm:ss");
				}
			}
			else
			{
				p_str = (string)item;
			}
			return 1;
		}

		private int Fetch(int iCol, ref int val)
		{
			object item = this.m_reader[iCol];
			if (item is int)
			{
				val = (int)item;
			}
			else if (!(item is uint))
			{
				if (item == DBNull.Value)
				{
					val = 0;
					return 0;
				}
				if (item is long)
				{
					val = (int)((long)item);
				}
				else if (item is ulong)
				{
					val = (int)((ulong)item);
				}
				else if (item is byte)
				{
					val = (byte)item;
				}
				else if (item is sbyte)
				{
					val = (sbyte)item;
				}
				else if (item is short)
				{
					val = (short)item;
				}
				else if (item is ushort)
				{
					val = (ushort)item;
				}
				else if (item is float)
				{
					val = (int)((float)item);
				}
				else if (item is double)
				{
					val = (int)((double)item);
				}
				else if (item is decimal)
				{
					val = (int)((decimal)item);
				}
				else if (item is string)
				{
					int.TryParse((string)item, out val);
				}
				else if (!(item is DateTime))
				{
					val = 0;
				}
				else
				{
					TimeSpan dateTime = (DateTime)item - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
					val = (int)dateTime.TotalSeconds;
				}
			}
			else
			{
				val = (int)item;
			}
			return 1;
		}

		private int Fetch(int iCol, ref float val)
		{
			object item = this.m_reader[iCol];
			if (item is float)
			{
				val = (float)item;
			}
			else if (item is double)
			{
				val = (float)((double)item);
			}
			else if (item is decimal)
			{
				val = (float)((float)((decimal)item));
			}
			else if (item is int)
			{
				val = (float)item;
			}
			else if (!(item is uint))
			{
				if (item == DBNull.Value)
				{
					val = 0f;
					return 0;
				}
				if (item is byte)
				{
					val = (float)((byte)item);
				}
				else if (item is short)
				{
					val = (float)((short)item);
				}
				else if (item is ushort)
				{
					val = (float)((ushort)item);
				}
				else if (!(item is string))
				{
					val = 0f;
				}
				else
				{
					float.TryParse((string)item, out val);
				}
			}
			else
			{
				val = (float)((float)((uint)item));
			}
			return 1;
		}

		private int Fetch(string p_field_name, ref string p_str)
		{
			return this.Fetch(this.m_reader.GetOrdinal(p_field_name), ref p_str);
		}

		private int Fetch(string p_field_name, ref int val)
		{
			return this.Fetch(this.m_reader.GetOrdinal(p_field_name), ref val);
		}

		private int Fetch(string p_field_name, ref float val)
		{
			return this.Fetch(this.m_reader.GetOrdinal(p_field_name), ref val);
		}

		private int GetAffectedRowsCount()
		{
			return this.m_reader.RecordsAffected;
		}

		private int GetRecordCount()
		{
			return this.m_reader.RecordsAffected;
		}

		private int IsEOF()
		{
			if (this.m_have_data)
			{
				return 0;
			}
			return 1;
		}

		private void MoveNext()
		{
			this.m_have_data = this.m_reader.Read();
		}
	}
}