using System;
using System.Runtime.CompilerServices;

namespace Oris.DB_interface
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
	public sealed class IndexAttribute : Attribute
	{
		public bool is_unique
		{
			get;
			set;
		}

		public string Name
		{
			get;
			private set;
		}

		public int Order
		{
			get;
			private set;
		}

		public IndexAttribute()
		{
			this.Order = -1;
		}

		public IndexAttribute(string p_index_name, int p_order)
		{
			this.Name = p_index_name;
			this.Order = p_order;
		}
	}
}