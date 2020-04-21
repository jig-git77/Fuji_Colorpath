using System;

namespace Oris.DB_interface
{
	public enum Fk_constraint
	{
		k_nothing,
		k_cascade,
		k_restrict,
		k_set_null
	}
}