using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TTestAccountPurpose")]
	public class NDLG_TestAccountPurpose
	{
		[Key(Order=2)]
		[NoDefault]
		[Required]
		[StringLength(128)]
		public string m_Attribute
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

		[Key(Order=1)]
		[NoDefault]
		[Required]
		[StringLength(128)]
		public string m_PurposeName
		{
			get;
			set;
		}

		[Required]
		[Text]
		public string m_Value
		{
			get;
			set;
		}

		public NDLG_TestAccountPurpose()
		{
		}
	}
}