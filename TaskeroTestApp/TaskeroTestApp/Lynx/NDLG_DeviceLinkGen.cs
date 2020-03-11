using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TDeviceLinkGen")]
	public class NDLG_DeviceLinkGen
	{
		public float m_BlackStartPercent
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

		[Required]
		[StringLength(255)]
		public string m_DebugFlags
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkDealer1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TDealer", column="ID")]
		[Index(is_unique=true)]
		public int? m_DefaultForDealerID
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkUserGroup2", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUserGroup", column="ID")]
		[Index(is_unique=true)]
		public int? m_DefaultForGroupID
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkUser2", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUser", column="ID")]
		[Index(is_unique=true)]
		public int? m_DefaultForUserID
		{
			get;
			set;
		}

		public Db_delta_e_type m_DeltaEType
		{
			get;
			set;
		}

		[Required]
		[Text]
		public string m_Description
		{
			get;
			set;
		}

		public float m_GCRLevel
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_ICCProfileCustomName
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ICCProfileName
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

		[DefaultValue(true)]
		[Index("UserGroupID", 3)]
		public bool m_IsValid
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_LockedByHostIP
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_LockedBySession
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkUserLock", Fk_constraint.k_cascade, Fk_constraint.k_set_null, table="TUser", column="ID")]
		public int? m_LockedByUserID
		{
			get;
			set;
		}

		public DateTime? m_LockedSinceTime
		{
			get;
			set;
		}

		public DateTime? m_LockedUntilTime
		{
			get;
			set;
		}

		public uint m_LockRefreshCount
		{
			get;
			set;
		}

		public Db_measure_mode m_MeasureMode
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

		[Index("TemplateForDealerID", 2)]
		[Index("TemplateForGroupID", 2)]
		[Index("TemplateForUserID", 2)]
		[Index("UserGroupID", 2)]
		[Required]
		[StringLength(255)]
		public string m_Name
		{
			get;
			set;
		}

		public Db_devlink_options m_Options
		{
			get;
			set;
		}

		[DefaultValue(Db_devlink_settings.k_user_default)]
		public Db_devlink_settings m_OptionsEditableByDealer
		{
			get;
			set;
		}

		[DefaultValue(Db_devlink_settings.k_user_default)]
		public Db_devlink_settings m_OptionsEditableByUser
		{
			get;
			set;
		}

		[DefaultValue(Db_devlink_settings.k_user_default)]
		public Db_devlink_settings m_OptionsVisibleByDealer
		{
			get;
			set;
		}

		[DefaultValue(Db_devlink_settings.k_user_default)]
		public Db_devlink_settings m_OptionsVisibleByUser
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_OutputName
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkPaperName1", Fk_constraint.k_cascade, Fk_constraint.k_set_null, table="TKnownPaper", column="ID")]
		public int? m_PaperID
		{
			get;
			set;
		}

		[Index]
		[Required]
		[StringLength(255)]
		public string m_PaperName
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkPrinter1", Fk_constraint.k_cascade, Fk_constraint.k_set_null, table="TKnownPrinter", column="ID")]
		public int? m_PrinterID
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_PrinterName
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_PrinterProfileCustomName
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_PrinterProfileName
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkPrintTarget1", Fk_constraint.k_cascade, Fk_constraint.k_set_null, table="TCPPrintObjectRealm", column="ID")]
		public int? m_PrintTargetID
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_PrintTargetName
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_PrintTargetSupplier
		{
			get;
			set;
		}

		public Db_profile_type m_ProfileType
		{
			get;
			set;
		}

		public DateTime? m_ProtectionsFollowLastTime
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkFollowProt", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TDeviceLinkGen", column="ID")]
		public int? m_ProtectionsFollowTemplateID
		{
			get;
			set;
		}

		[DefaultValue(Db_rendering_intent.k_relative)]
		public Db_rendering_intent m_RenderingIntent
		{
			get;
			set;
		}

		[DefaultValue("ECI-RGB.V1.0.ICC")]
		[Required]
		[StringLength(255)]
		public string m_RGBICCProfileName
		{
			get;
			set;
		}

		[DefaultValue(Db_rendering_intent.k_perceptual)]
		public Db_rendering_intent m_RGBRenderingIntent
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_RootFolderName
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkDealer3", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TDealer", column="ID")]
		[Index("TemplateForDealerID", 1, is_unique=true)]
		public int? m_TemplateForDealerID
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkUserGroup3", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUserGroup", column="ID")]
		[Index("TemplateForGroupID", 1, is_unique=true)]
		public int? m_TemplateForGroupID
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkUser3", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUser", column="ID")]
		[Index("TemplateForUserID", 1, is_unique=true)]
		public int? m_TemplateForUserID
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_TestchartName
		{
			get;
			set;
		}

		[DefaultValue("400")]
		public float m_TotalInkCoverage
		{
			get;
			set;
		}

		public DateTime? m_TrackingEndTime
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkTrackingLicensePool", Fk_constraint.k_cascade, Fk_constraint.k_set_null, table="TLicensePool", column="ID")]
		public int? m_TrackingLicensePoolID
		{
			get;
			set;
		}

		public uint? m_TrackingLimit
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkUserGroup1", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TUserGroup", column="ID")]
		[Index("UserGroupID", 1, is_unique=true)]
		public int? m_UserGroupID
		{
			get;
			set;
		}

		public DateTime? m_ValuesFollowLastTime
		{
			get;
			set;
		}

		[ForeignKey("TDeviceLinkFollowValue", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TDeviceLinkGen", column="ID")]
		public int? m_ValuesFollowTemplateID
		{
			get;
			set;
		}

		[Required]
		[Text]
		public string m_XmlParameters
		{
			get;
			set;
		}

		public NDLG_DeviceLinkGen()
		{
		}
	}
}