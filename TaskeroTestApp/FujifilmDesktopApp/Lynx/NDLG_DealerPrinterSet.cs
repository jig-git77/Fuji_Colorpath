using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TDealerPrinterSet")]
	public class NDLG_DealerPrinterSet
	{
		[ForeignKey("TKnownPrinterDealer2", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TDealer", column="ID")]
		[Index("DealerID", 1, is_unique=true)]
		[NoDefault]
		public int m_DealerID
		{
			get;
			set;
		}

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

		[ForeignKey("TKnownPrinter2", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TKnownPrinter", column="ID")]
		[Index("DealerID", 2)]
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

		public NDLG_DealerPrinterSet()
		{
		}
	}
}