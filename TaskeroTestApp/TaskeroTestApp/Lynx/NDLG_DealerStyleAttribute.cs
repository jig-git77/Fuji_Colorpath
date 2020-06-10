using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TDealerStyleAttribute")]
	public class NDLG_DealerStyleAttribute
	{
		[Timestamp]
		public DateTime m_CreateTime
		{
			get;
			set;
		}

		[ForeignKey(table="TUser", column="ID")]
		public int? m_CreateUserID
		{
			get;
			set;
		}

		[ForeignKey("TDealerStyle1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TDealer", column="ID")]
		[Key(Order=1)]
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

		[Key(Order=2)]
		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_Name
		{
			get;
			set;
		}

		[LongText]
		[NoDefault]
		[Required]
		public string m_Value
		{
			get;
			set;
		}

		public NDLG_DealerStyleAttribute()
		{
		}
	}
}