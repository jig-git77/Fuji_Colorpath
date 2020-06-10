using System;
using System.Runtime.CompilerServices;

namespace Oris.DB_interface
{
	public sealed class StringLengthAttribute : Attribute
	{
		public uint MaximumLength
		{
			get;
			private set;
		}

		public StringLengthAttribute(uint p_max_length)
		{
			this.MaximumLength = p_max_length;
		}
	}
}