using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TUserAndGroupLog")]
	public class NDLG_UserAndGroupLog
	{
		[StringLength(255)]
		public string m_ClientHostIP
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_ClientHostName
		{
			get;
			set;
		}

		[Index]
		[Timestamp]
		public DateTime m_CreateTime
		{
			get;
			set;
		}

		[ForeignKey(table="TUser", column="ID")]
		[Index]
		public int? m_CreateUserID
		{
			get;
			set;
		}

		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_CreateUserLoginName
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

		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_ModifyUserLoginName
		{
			get;
			set;
		}

		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_OpDealerCompany
		{
			get;
			set;
		}

		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_OpDealerDepartment
		{
			get;
			set;
		}

		[ForeignKey(table="TDealer", column="ID")]
		[Index]
		public int? m_OpDealerID
		{
			get;
			set;
		}

		[Required]
		[Text]
		public string m_OpDetail1
		{
			get;
			set;
		}

		[Required]
		[Text]
		public string m_OpDetail2
		{
			get;
			set;
		}

		[Required]
		[Text]
		public string m_OpDetail3
		{
			get;
			set;
		}

		public Db_user_log_operation? m_Operation
		{
			get;
			set;
		}

		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_OpUserCompany
		{
			get;
			set;
		}

		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_OpUserGroupCompany
		{
			get;
			set;
		}

		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_OpUserGroupDepartment
		{
			get;
			set;
		}

		[ForeignKey(table="TUserGroup", column="ID")]
		[Index]
		public int? m_OpUserGroupID
		{
			get;
			set;
		}

		[ForeignKey(table="TUser", column="ID")]
		[Index]
		public int? m_OpUserId
		{
			get;
			set;
		}

		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_OpUserLoginName
		{
			get;
			set;
		}

		public NDLG_UserAndGroupLog()
		{
		}
	}
}