using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TLicensePool")]
	public class NDLG_LicensePool
	{
		public uint m_ClicksFree
		{
			get;
			set;
		}

		public Db_license_costly_ops m_CostlyOps
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

		[Required]
		[StringLength(255)]
		public string m_Description
		{
			get;
			set;
		}

		public uint m_FlatratesFree
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
		[StringLength(255)]
		public string m_Signature
		{
			get;
			set;
		}

		public uint m_TrackingsFree
		{
			get;
			set;
		}

		public NDLG_LicensePool()
		{
		}
	}
}