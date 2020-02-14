using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TUser")]
	public class NDLG_User
	{
		[Required]
		[StringLength(255)]
		public string m_AddressSupplement
		{
			get;
			set;
		}

		[StringLength(60)]
		public string m_ALTPWD
		{
			get;
			set;
		}

		public DateTime? m_ALTPWDExpireTime
		{
			get;
			set;
		}

		public DateTime? m_ALTPWDMailedTime
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_ALTPWDMailedTo
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_CertWebUserID
		{
			get;
			set;
		}

		public uint? m_ClickLimit
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_CompanyName
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_CompanyName2
		{
			get;
			set;
		}

		public uint m_ConcurrentLogins
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_Country
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

		[ForeignKey("TUserGroup2", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TUserGroup", column="ID")]
		public int? m_DefaultGroupID
		{
			get;
			set;
		}

		public uint m_DeniedLoginCount
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_DeniedLoginHost
		{
			get;
			set;
		}

		public Db_user_denied_reason? m_DeniedLoginReason
		{
			get;
			set;
		}

		public DateTime? m_DeniedLoginTime
		{
			get;
			set;
		}

		public uint m_DeniedLoginTotal
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_Department
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_Description
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_eMail
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_EvaluationToolURL
		{
			get;
			set;
		}

		public Db_user_expire_type? m_ExpirationPeriodType
		{
			get;
			set;
		}

		public uint m_ExpirationPeriodValue
		{
			get;
			set;
		}

		public DateTime? m_ExpirationTime
		{
			get;
			set;
		}

		public uint m_FailedLoginCount
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_FailedLoginHost
		{
			get;
			set;
		}

		public DateTime? m_FailedLoginTime
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_FaxNumber
		{
			get;
			set;
		}

		public DateTime? m_FirstLoginTime
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_FirstName
		{
			get;
			set;
		}

		public uint m_Flags
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_FonNumber
		{
			get;
			set;
		}

		[ForeignKey("TUserDealer2", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TDealer", column="ID")]
		public int? m_HomeDealerID
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

		[ForeignKey("TUserDealer1", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TDealer", column="ID")]
		public int? m_IsDealerID
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_IsTestAccount
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_LastLoginHost
		{
			get;
			set;
		}

		public DateTime? m_LastLoginTime
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_LastName
		{
			get;
			set;
		}

		public uint m_LoginCount
		{
			get;
			set;
		}

		[Index(is_unique=true)]
		[Required]
		[StringLength(255)]
		public string m_LoginName
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_MobileNumber
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

		public uint m_OldFailedLoginCount
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_OldFailedLoginHost
		{
			get;
			set;
		}

		public DateTime? m_OldFailedLoginTime
		{
			get;
			set;
		}

		[Required]
		[StringLength(60)]
		public string m_Password
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_PostalCode
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_PostLoginMessage
		{
			get;
			set;
		}

		[Required]
		[StringLength(32)]
		public string m_PrefLanguage
		{
			get;
			set;
		}

		public DateTime? m_PWDLastChange
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_RejectLoginMessage
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_Salutation
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_SelfRegLicenseCode
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_State
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_Street
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_Town
		{
			get;
			set;
		}

		public Db_user_privileges m_UserPrivileges
		{
			get;
			set;
		}

		public NDLG_User()
		{
		}
	}
}