using System;

namespace Lynx
{
	public enum Db_iter_status
	{
		k_changed_options,
		k_calculated_testchart,
		k_downloaded_testchart,
		k_printed_testchart,
		k_uploaded_measurement,
		k_calculated_colormatch,
		k_optimized_colormatch,
		k_generated_devicelink,
		k_downloaded_devicelink
	}
}