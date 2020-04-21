using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TUserGroupLinkItem")]
	public class NDLG_UserGroupLinkItem
	{
		public DateTime? m_AcceptedAtTime
		{
			get;
			set;
		}

		[ForeignKey("TUserGroupLinkItem4", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TUser", column="ID")]
		public int? m_AcceptedByUserID
		{
			get;
			set;
		}

		[ForeignKey("TUserGroupLinkItem9", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TCPMeasurement", column="ID")]
		[Index("CPMeasurementID", 1, is_unique=true)]
		public int? m_CPMeasurementID
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_CPMeasurementOrigin
		{
			get;
			set;
		}

		[ForeignKey("TUserGroupLinkItem10", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TCPPrintObject", column="ID")]
		public int? m_CPPrintObjectID
		{
			get;
			set;
		}

		[StringLength(120)]
		public string m_CPPrintObjectName
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

		[ForeignKey("TUserGroupLinkItem2", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TUserGroup", column="ID")]
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

		[ForeignKey("TUserGroupLinkItem8", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TDeviceLinkGen", column="ID")]
		[Index("DeviceLinkGenID", 1, is_unique=true)]
		public int? m_DeviceLinkGenID
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_DeviceLinkGenName
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_DeviceLinkGenOrigin
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

		[Text]
		public string m_Notification
		{
			get;
			set;
		}

		public DateTime? m_ProvidedAtTime
		{
			get;
			set;
		}

		[ForeignKey("TUserGroupLinkItem3", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TUser", column="ID")]
		public int? m_ProvidedByUserID
		{
			get;
			set;
		}

		public DateTime? m_ReleasedAtTime
		{
			get;
			set;
		}

		[ForeignKey(table="TUser", column="ID")]
		public int? m_ReleasedByUserID
		{
			get;
			set;
		}

		public DateTime? m_RevokedAtTime
		{
			get;
			set;
		}

		[ForeignKey(table="TUser", column="ID")]
		public int? m_RevokedByUserID
		{
			get;
			set;
		}

		[ForeignKey("TUserGroupLinkItem1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUserGroupCompanyLink", column="ID")]
		[Index("CPMeasurementID", 2, is_unique=true)]
		[Index("DeviceLinkGenID", 2, is_unique=true)]
		public int m_UserGroupCompanyLinkID
		{
			get;
			set;
		}

		public NDLG_UserGroupLinkItem()
		{
		}
	}
}