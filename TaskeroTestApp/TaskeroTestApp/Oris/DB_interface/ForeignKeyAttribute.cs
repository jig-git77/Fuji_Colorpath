using System;
using System.Runtime.CompilerServices;

namespace Oris.DB_interface
{
	public sealed class ForeignKeyAttribute : Attribute
	{
		public string column
		{
			get;
			set;
		}

		public string Name
		{
			get;
			private set;
		}

		public Fk_constraint on_delete
		{
			get;
			set;
		}

		public Fk_constraint on_update
		{
			get;
			set;
		}

		public string table
		{
			get;
			set;
		}

		public ForeignKeyAttribute()
		{
			this.Name = null;
			this.on_update = Fk_constraint.k_nothing;
			this.on_delete = Fk_constraint.k_nothing;
		}

		public ForeignKeyAttribute(string p_constraint_name = null, Fk_constraint on_update = 0, Fk_constraint on_delete = 0)
		{
			this.Name = p_constraint_name;
			this.on_update = on_update;
			this.on_delete = on_delete;
		}
	}
}