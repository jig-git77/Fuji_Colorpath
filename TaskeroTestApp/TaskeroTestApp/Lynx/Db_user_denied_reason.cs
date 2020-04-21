using System;

namespace Lynx
{
	public enum Db_user_denied_reason
	{
		k_internal_error,
		k_client_host,
		k_license_shop,
		k_reject_msg,
		k_abstime,
		k_reltime,
		k_logincount,
		k_no_license
	}
}