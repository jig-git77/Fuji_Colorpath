using System;

namespace Oris.DB_interface
{
	public enum Column_type
	{
		k_unknown,
		k_in_attributes,
		k_int32,
		k_uint32,
		k_int64,
		k_decimal,
		k_float,
		k_guid,
		k_bool,
		k_date,
		k_date_time,
		k_mysql_timestamp,
		k_byte_array,
		k_string,
		k_mysql_enum,
		k_mysql_set,
		k_int8,
		k_uint8
	}
}