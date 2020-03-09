using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TDeviceLinkIter")]
	public class NDLG_DeviceLinkIter
	{
		[Required]
		[StringLength(255)]
		public string m_Comment
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

		public float? m_DeltaChAverage
		{
			get;
			set;
		}

		public float? m_DeltaEAverage
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_DeltaEMaxColor
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_DeltaEMaxPrimColor
		{
			get;
			set;
		}

		public float? m_DeltaEMaxPrimValue
		{
			get;
			set;
		}

		public float? m_DeltaEMaxValue
		{
			get;
			set;
		}

		public float? m_DeltaEStdDev
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkIterGen", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TDeviceLinkGen", column="ID")]
		[Key(Order=2)]
		[NoDefault]
		public int m_DeviceLinkGenID
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkLicense", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TLicenseLog", column="ID")]
		[Index(is_unique=true)]
		public int? m_DLP_LicenseID
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_DLP_Signature
		{
			get;
			set;
		}

		[Index(is_unique=true)]
		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_DLP_UUID
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_FilenameCC
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_FilenameDAT
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_FilenameDLProfile
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_FilenameMeasurement
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_FilenameTestchart
		{
			get;
			set;
		}

		[Key(Order=1)]
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

		public Db_iter_options m_Options
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProgressErrorMsg
		{
			get;
			set;
		}

		[DefaultValue("100")]
		[Required]
		public int m_ProgressPercent
		{
			get;
			set;
		}

		[NoDefault]
		public Db_iter_status m_Status
		{
			get;
			set;
		}

		public NDLG_DeviceLinkIter()
		{
		}
	}
}