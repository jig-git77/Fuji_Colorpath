using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TKnownICCProfile")]
	public class NDLG_KnownICCProfile
	{
		[Index("Kind", 2)]
		[Index("Kind_2", 2)]
		[NoDefault]
		public Db_icc_color_space m_ColorSpace
		{
			get;
			set;
		}

		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_Comment
		{
			get;
			set;
		}

		[Index("Kind_2", 3)]
		[StringLength(255)]
		public string m_DisplayName
		{
			get;
			set;
		}

		public int? m_DisplaySortOrder
		{
			get;
			set;
		}

		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_FileName
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

		[Index("Kind", 1, is_unique=true)]
		[Index("Kind_2", 1, is_unique=true)]
		public Db_name_or_rule m_Kind
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

		[Index("Kind", 3)]
		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_Name
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_RuleParameter
		{
			get;
			set;
		}

		public NDLG_KnownICCProfile()
		{
		}
	}
}