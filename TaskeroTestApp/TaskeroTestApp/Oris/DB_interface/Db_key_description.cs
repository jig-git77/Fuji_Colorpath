using System;

namespace Oris.DB_interface
{
	public class Db_key_description
	{
		public string m_key_name;

		public Db_key_type m_key_type;

		public string m_index_columns;

		public string m_foreign_table;

		public string m_foreign_column;

		public Fk_constraint m_foreign_on_update;

		public Fk_constraint m_foreign_on_delete;

		public string m_index_name;

		public int m_index_order;

		public Db_key_description()
		{
		}

		public Db_key_description(string p_key_name, Db_key_type p_key_type, string p_index_columns)
		{
			this.m_key_name = p_key_name;
			this.m_key_type = p_key_type;
			this.m_index_columns = p_index_columns;
		}
	}
}