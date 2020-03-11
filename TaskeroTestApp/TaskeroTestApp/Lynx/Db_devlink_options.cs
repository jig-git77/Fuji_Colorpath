using System;

namespace Lynx
{
	[Flags]
	public enum Db_devlink_options
	{
		k_keep_pure_black = 1,
		k_keep_100p_black = 2,
		k_measure_use_xrga = 4,
		k_measure_uv_cut = 8,
		k_use_xrite_prism = 16,
		k_keep_pure_colors = 32,
		k_use_inksaver = 64
	}
}