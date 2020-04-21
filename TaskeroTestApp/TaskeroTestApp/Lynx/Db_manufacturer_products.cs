using System;

namespace Lynx
{
	[Flags]
	public enum Db_manufacturer_products
	{
		k_other = 1,
		k_printer = 2,
		k_paper = 4,
		k_ink = 8
	}
}