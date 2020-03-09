using System;
using System.Runtime.CompilerServices;

namespace Oris.DB_interface
{
	public sealed class DefaultValueAttribute : Attribute
	{
		public object Default
		{
			get;
			private set;
		}

		public DefaultValueAttribute(object p_default_value)
		{
			this.Default = p_default_value;
		}
	}
}