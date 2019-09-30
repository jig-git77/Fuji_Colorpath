using System;

namespace Lynx
{
	[Flags]
	public enum Db_devlink_settings
	{
		k_ICCProfileName = 1,
		k_TestchartName = 2,
		k_keep_pure_black = 4,
		k_keep_100p_black = 8,
		k_BlackStartPercent = 16,
		k_GCRLevel = 32,
		k_TotalInkCoverage = 64,
		k_ProfileType = 128,
		k_RGBICCProfileName = 256,
		k_RGBRenderingIntent = 512,
		k_RenderingIntent = 1024,
		k_MeasureMode = 2048,
		k_DeltaEType = 4096,
		k_use_xrite_prism = 8192,
		k_keep_pure_colors = 16384,
		k_PrinterProfileName = 32768,
		k_PrinterName = 65536,
		k_PrintTargetName = 131072,
		k_PaperName = 262144,
		k_use_inksaver = 524288,
		k_dealer_default = 1040383,
		k_user_default = 1040383
	}
}