using System;

namespace Lynx
{
	[Flags]
	public enum Db_user_log_operation
	{
		k_other = 1,
		k_created = 2,
		k_deleted = 4,
		k_purged = 8,
		k_recovered = 16,
		k_login = 32,
		k_logout = 64,
		k_joining = 128,
		k_leaving = 256,
		k_updated = 512,
		k_login_failure = 1024,
		k_suspended = 2048,
		k_resumed = 4096
	}
}