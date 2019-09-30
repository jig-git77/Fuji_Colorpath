using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TDeviceLinkLog")]
	public class NDLG_DeviceLinkLog
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

		[Index]
		[NoDefault]
		public int m_DeviceLinkGenID
		{
			get;
			set;
		}

		[ForeignKey("DeviceLinkLogIter", Fk_constraint.k_cascade, Fk_constraint.k_set_null, table="TDeviceLinkIter", column="ID")]
		[Index]
		public int? m_DeviceLinkIterID
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

		[NoDefault]
		public Db_link_log_operation m_Operation
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
		public int m_ProgressPercent
		{
			get;
			set;
		}

		public NDLG_DeviceLinkLog()
		{
		}
	}
}