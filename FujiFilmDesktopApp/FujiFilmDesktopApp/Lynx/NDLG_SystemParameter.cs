using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TSystemParameter")]
	public class NDLG_SystemParameter
	{
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

		[Key]
		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_Name
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

		public NDLG_SystemParameter()
		{
		}
	}
}