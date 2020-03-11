using System;

namespace Lynx
{
	[Flags]
	public enum Db_company_link_event
	{
		k_StatusChange = 1,
		k_PurposeChange = 2,
		k_NewOpenerSecret = 4,
		k_BadOpenerSecret = 8,
		k_ProviderNotesChange = 16,
		k_AcceptorNotesChange = 32
	}
}