using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TUserGroupCompanyLink")]
	public class NDLG_UserGroupCompanyLink
	{
		public DateTime? m_AcceptedTime
		{
			get;
			set;
		}

		[ForeignKey(table="TUser", column="ID")]
		public int? m_AcceptedUserID
		{
			get;
			set;
		}

		[ForeignKey("TUserGroupCompanyLink3", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TUserGroup", column="ID")]
		[Index]
		[Index("ProviderGroupID", 2, is_unique=true)]
		public int? m_AcceptorGroupID
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_AcceptorNotes
		{
			get;
			set;
		}

		public DateTime? m_ClosedTime
		{
			get;
			set;
		}

		[ForeignKey(table="TUser", column="ID")]
		public int? m_ClosedUserID
		{
			get;
			set;
		}

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

		public DateTime? m_ExaminedTime
		{
			get;
			set;
		}

		[ForeignKey(table="TUser", column="ID")]
		public int? m_ExaminedUserID
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

		[Required]
		[StringLength(255)]
		public string m_InvitedEmail
		{
			get;
			set;
		}

		[Index("ProviderGroupID", 3, is_unique=true)]
		public Db_company_link_purpose m_LinkPurpose
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

		[Index("OpenerSecret", 2, is_unique=true)]
		public int m_MultiLinkInvitation
		{
			get;
			set;
		}

		[Index("OpenerSecret", 1, is_unique=true)]
		[StringLength(50)]
		public string m_OpenerSecret
		{
			get;
			set;
		}

		public DateTime m_PlannedTime
		{
			get;
			set;
		}

		[ForeignKey(table="TUser", column="ID")]
		public int m_PlannedUserID
		{
			get;
			set;
		}

		[ForeignKey("TUserGroupCompanyLink1", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TUserGroup", column="ID")]
		[Index("ProviderGroupID", 1, is_unique=true)]
		public int m_PlanningGroupID
		{
			get;
			set;
		}

		[ForeignKey("TUserGroupCompanyLink2", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TUserGroup", column="ID")]
		public int? m_ProviderGroupID
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProviderNotes
		{
			get;
			set;
		}

		public DateTime? m_RefusedTime
		{
			get;
			set;
		}

		[ForeignKey(table="TUser", column="ID")]
		public int? m_RefusedUserID
		{
			get;
			set;
		}

		public DateTime? m_UtilizedTime
		{
			get;
			set;
		}

		[ForeignKey(table="TUser", column="ID")]
		public int? m_UtilizedUserID
		{
			get;
			set;
		}

		public NDLG_UserGroupCompanyLink()
		{
		}
	}
}