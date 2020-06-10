using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TLicenseFlatrate")]
	public class NDLG_LicenseFlatrate
	{
		[ForeignKey("TLicenseFlatrateUser", Fk_constraint.k_cascade, Fk_constraint.k_set_null, table="TUser", column="ID")]
		[Index]
		public int? m_AssignedToUserID
		{
			get;
			set;
		}

		[NoDefault]
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

		public uint m_FlatrateAmount
		{
			get;
			set;
		}

		[NoDefault]
		public DateTime m_FlatrateEndTime
		{
			get;
			set;
		}

		public DateTime? m_FlatrateStartTime
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

		[ForeignKey("TLicenseFlatratePool", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TLicensePool", column="ID")]
		[Index]
		[NoDefault]
		public int m_LicensePoolID
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

		public uint m_TrackingAmount
		{
			get;
			set;
		}

		public uint? m_TrackingDurationDays
		{
			get;
			set;
		}

		public uint? m_TrackingDurationMonths
		{
			get;
			set;
		}

		public DateTime? m_TrackingEndTime
		{
			get;
			set;
		}

		public uint? m_TrackingLimit
		{
			get;
			set;
		}

		public NDLG_LicenseFlatrate()
		{
		}
	}
}