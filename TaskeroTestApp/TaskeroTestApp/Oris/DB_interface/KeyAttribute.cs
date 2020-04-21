using System;
using System.Runtime.CompilerServices;

namespace Oris.DB_interface
{
	public sealed class KeyAttribute : Attribute
	{
		public int Order
		{
			get;
			set;
		}

		public KeyAttribute()
		{
			this.Order = 0;
		}
	}
}