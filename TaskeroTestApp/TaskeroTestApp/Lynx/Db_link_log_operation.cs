using System;

namespace Lynx
{
	public enum Db_link_log_operation
	{
		k_created_genjob,
		k_locked_genjob,
		k_changed_options,
		k_calculated_testchart,
		k_downloaded_testchart,
		k_printed_testchart,
		k_uploaded_measurement,
		k_calculated_colormatch,
		k_optimized_colormatch,
		k_generated_devicelink,
		k_downloaded_devicelink,
		k_allocated_license,
		k_dismissed_colormatch,
		k_unlocked_genjob,
		k_deleted_genjob,
		k_renamed_genjob,
		k_created_template,
		k_changed_template,
		k_deleted_template,
		k_applied_template
	}
}