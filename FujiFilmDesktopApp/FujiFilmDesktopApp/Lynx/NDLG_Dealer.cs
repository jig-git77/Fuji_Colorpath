using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TDealer")]
	public class NDLG_Dealer
	{
		[Required]
		[StringLength(255)]
		public string m_AddressSupplement
		{
			get;
			set;
		}

		[Index("CompanyName", 1, is_unique=true)]
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

		public int? m_CustomerLimit
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

		[DefaultValue(true)]
		[Index("CompanyName", 6)]
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

		[ForeignKey("TDealerLic1", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TLicensePool", column="ID")]
		[Index("LicensePoolID", 1, is_unique=true)]
		public int? m_LicensePoolID
		{
			get;
			set;
		}

		[Required]
		[Text]
		public string m_LicenseShopURL
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

		[DefaultValue("1")]
		[ForeignKey("TDealerparent1", Fk_constraint.k_nothing, Fk_constraint.k_nothing, on_update=Fk_constraint.k_cascade, on_delete=Fk_constraint.k_restrict, table="TDealer", column="ID")]
		[Index("CompanyName", 5)]
		public int m_ParentDealerID
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
		public string m_PreLoginMessage
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

		[DefaultValue("0")]
		public int? m_SubDealerLimit
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

		public NDLG_Dealer()
		{
		}
	}
}