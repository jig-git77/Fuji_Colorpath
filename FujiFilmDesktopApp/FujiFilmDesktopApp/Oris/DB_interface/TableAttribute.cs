using System;
using System.Runtime.CompilerServices;

namespace Oris.DB_interface
{
	public sealed class TableAttribute : Attribute
	{
		public string Name
		{
			get;
			private set;
		}

		public TableAttribute(string p_table_name)
		{
			this.Name = p_table_name;
		}
	}
}