using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TUserGroupMembership")]
	public class NDLG_UserGroupMembership
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

		[ForeignKey("TUserGroup1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUserGroup", column="ID")]
		[Index("UserGroupID", 1, is_unique=true)]
		[Key(Order=2)]
		[NoDefault]
		public int m_UserGroupID
		{
			get;
			set;
		}

		[ForeignKey("TUser1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUser", column="ID")]
		[Index("UserGroupID", 2)]
		[Key(Order=1)]
		[NoDefault]
		public int m_UserID
		{
			get;
			set;
		}

		public NDLG_UserGroupMembership()
		{
		}
	}
}