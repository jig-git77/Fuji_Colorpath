using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TUserGroup")]
	public class NDLG_UserGroup
	{
		[Required]
		[StringLength(255)]
		public string m_AddressSupplement
		{
			get;
			set;
		}

		[Index("CompanyName", 1)]
		[Required]
		[StringLength(140)]
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

		[Index("CompanyName", 3)]
		[Required]
		[StringLength(70)]
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

		[ForeignKey("TUserGroupDealer1", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TDealer", column="ID")]
		public int? m_CustomersOfDealerID
		{
			get;
			set;
		}

		[Index("CompanyName", 2)]
		[Required]
		[StringLength(60)]
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
		public string m_DeviceSerialNumbers
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_DeviceTypeNames
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

		[Required]
		[StringLength(255)]
		public string m_FaxNumber
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

		[Required]
		[StringLength(255)]
		public string m_FonNumber
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
		public string m_IdentCode
		{
			get;
			set;
		}

		[Index(is_unique=true)]
		[StringLength(255)]
		public string m_InternalName
		{
			get;
			set;
		}

		[DefaultValue(true)]
		[Index("CompanyName", 5)]
		public bool m_IsValid
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

		public sbyte m_LicenseIsSuspended
		{
			get;
			set;
		}

		[ForeignKey("TUserGroupLic1", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TLicensePool", column="ID")]
		[Index(is_unique=true)]
		public int? m_LicensePoolID
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

		[Required]
		[StringLength(255)]
		public string m_Salutation
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

		[Index("CompanyName", 4)]
		[Required]
		[StringLength(70)]
		public string m_Town
		{
			get;
			set;
		}

		public NDLG_UserGroup()
		{
		}
	}
}