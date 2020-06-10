using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TUserGroupTestchartSet")]
	public class NDLG_UserGroupTestchartSet
	{
		public int? m_DisplaySortOrder
		{
			get;
			set;
		}

		[ForeignKey("KnownTestchartID2", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TKnownTestchart", column="ID")]
		[Index("UserGroupID", 2)]
		[Key(Order=1)]
		[NoDefault]
		public int m_KnownTestchartID
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

		[ForeignKey("TUserGroup4", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUserGroup", column="ID")]
		[Index("UserGroupID", 1, is_unique=true)]
		[Key(Order=2)]
		[NoDefault]
		public int m_UserGroupID
		{
			get;
			set;
		}

		public NDLG_UserGroupTestchartSet()
		{
		}
	}
}