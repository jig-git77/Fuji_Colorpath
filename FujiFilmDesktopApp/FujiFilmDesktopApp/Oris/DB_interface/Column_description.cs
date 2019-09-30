using System;

namespace Oris.DB_interface
{
	public class Column_description
	{
		public string m_name;

		public Column_type m_datatype;

		public string m_datatype_name;

		public uint m_column_size;

		public string m_set_enum_values;

		public bool m_allow_null;

		public string m_default_value;

		public Column_description()
		{
		}
	}
}