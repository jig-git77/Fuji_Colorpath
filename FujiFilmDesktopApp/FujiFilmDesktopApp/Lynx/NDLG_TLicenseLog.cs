using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TLicenseLog")]
	public class NDLG_TLicenseLog
	{
		public int m_ClickAmount
		{
			get;
			set;
		}

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
		public int? m_CreateUserID
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

		[ForeignKey("TLicenseLogAppl", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TDeviceLinkLog", column="ID")]
		[Index(is_unique=true)]
		public int? m_DeviceLinkLogID
		{
			get;
			set;
		}

		public int m_FlatratesAmount
		{
			get;
			set;
		}

		public uint? m_FlatratesAssignedAndInUse
		{
			get;
			set;
		}

		public DateTime? m_FlatratesAssignedEndTime
		{
			get;
			set;
		}

		public int? m_FlatratesAssignedToUserID
		{
			get;
			set;
		}

		[Index("LicensePoolID", 2)]
		[Key(Order=1)]
		public int m_ID
		{
			get;
			set;
		}

		[Date]
		public DateTime? m_InvoiceDate
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_InvoiceNumber
		{
			get;
			set;
		}

		[ForeignKey(table="TLicenseFlatrate", column="ID")]
		[Index]
		public int? m_LicenseFlatrateID
		{
			get;
			set;
		}

		[ForeignKey("TLicenseLogPool", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TLicensePool", column="ID")]
		[Index("LicensePoolID", 1, is_unique=true)]
		[Key(Order=2)]
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

		public DateTime? m_NewFlatrateEndTime
		{
			get;
			set;
		}

		public uint? m_OldClickLimit
		{
			get;
			set;
		}

		public uint m_OldClicksFree
		{
			get;
			set;
		}

		[NoDefault]
		public Db_license_costly_ops m_OldCostlyOps
		{
			get;
			set;
		}

		public DateTime? m_OldFlatrateEndTime
		{
			get;
			set;
		}

		public uint m_OldFlatratesFree
		{
			get;
			set;
		}

		public DateTime? m_OldTrackingEndTime
		{
			get;
			set;
		}

		public uint? m_OldTrackingLimit
		{
			get;
			set;
		}

		public uint m_OldTrackingsFree
		{
			get;
			set;
		}

		[ForeignKey("TLicenseLogOtherID", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TLicenseLog", column="ID")]
		public int? m_OtherLicenseLogID
		{
			get;
			set;
		}

		[ForeignKey("TLicenseLogOtherPool", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TLicensePool", column="ID")]
		public int? m_OtherLicensePoolID
		{
			get;
			set;
		}

		public int m_TrackingAmount
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

		public DateTime? m_TrackingLimit
		{
			get;
			set;
		}

		[Index]
		public DateTime? m_ValuedTime
		{
			get;
			set;
		}

		public NDLG_TLicenseLog()
		{
		}
	}
}