using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TUserGroupPrinterSet")]
	public class NDLG_UserGroupPrinterSet
	{
		[DefaultValue("CURRENT_TIMESTAMP")]
		public DateTime m_ModifyTime
		{
			get;
			set;
		}

		[ForeignKey(table="TUser", column="ID")]
		public int? m_ModifyUserID
		{
			get;
			set;
		}

		[ForeignKey("TKnownPrinter3", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TKnownPrinter", column="ID")]
		[Index("UserGroupID", 2)]
		[NoDefault]
		public int m_PrinterID
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_RuleParameter
		{
			get;
			set;
		}

		[ForeignKey("TKnownPrinterUserGroup3", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUserGroup", column="ID")]
		[Index("UserGroupID", 1, is_unique=true)]
		[NoDefault]
		public int m_UserGroupID
		{
			get;
			set;
		}

		public NDLG_UserGroupPrinterSet()
		{
		}
	}
}