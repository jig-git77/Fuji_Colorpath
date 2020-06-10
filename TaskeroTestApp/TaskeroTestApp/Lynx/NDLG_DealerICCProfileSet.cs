using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TDealerICCProfileSet")]
	public class NDLG_DealerICCProfileSet
	{
		[ForeignKey("TDealer1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TDealer", column="ID")]
		[Index("DealerID", 1, is_unique=true)]
		[Key(Order=2)]
		[NoDefault]
		public int m_DealerID
		{
			get;
			set;
		}

		public int? m_DisplaySortOrder
		{
			get;
			set;
		}

		[ForeignKey("KnownICCProfileID1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TKnownICCProfile", column="ID")]
		[Index("DealerID", 2)]
		[Key(Order=1)]
		[NoDefault]
		public int m_KnownICCProfileID
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

		[StringLength(255)]
		public string m_RuleParameter
		{
			get;
			set;
		}

		public NDLG_DealerICCProfileSet()
		{
		}
	}
}