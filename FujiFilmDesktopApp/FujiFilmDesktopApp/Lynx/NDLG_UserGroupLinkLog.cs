using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TUserGroupLinkLog")]
	public class NDLG_UserGroupLinkLog
	{
		[Timestamp]
		public DateTime m_CreateTime
		{
			get;
			set;
		}

		[ForeignKey("TUserGroupLinkLog2", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TUserGroup", column="ID")]
		public int? m_CreateUserGroupID
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

		[Key]
		public int m_ID
		{
			get;
			set;
		}

		public Db_company_link_status m_LinkStatus
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

		public Db_company_link_event m_SpecialEvents
		{
			get;
			set;
		}

		[ForeignKey("TUserGroupLinkLog1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUserGroupCompanyLink", column="ID")]
		public int m_UserGroupCompanyLinkID
		{
			get;
			set;
		}

		[ForeignKey("TUserGroupLinkLog3", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUserGroupLinkItem", column="ID")]
		public int? m_UserGroupLinkItemID
		{
			get;
			set;
		}

		public Db_company_link_op m_UserGroupLinkItemOP
		{
			get;
			set;
		}

		public NDLG_UserGroupLinkLog()
		{
		}
	}
}