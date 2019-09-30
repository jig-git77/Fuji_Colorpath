using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TUserPrinterSet")]
	public class NDLG_UserPrinterSet
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

		[ForeignKey("TKnownPrinter4", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TKnownPrinter", column="ID")]
		[Index("UserID", 2)]
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

		[ForeignKey("TKnownPrinterUserGroup4", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUser", column="ID")]
		[Index("UserID", 1, is_unique=true)]
		[NoDefault]
		public int m_UserID
		{
			get;
			set;
		}

		public NDLG_UserPrinterSet()
		{
		}
	}
}