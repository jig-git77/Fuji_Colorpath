using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TKnownTestchart")]
	public class NDLG_KnownTestchart
	{
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

		[Key]
		public int m_ID
		{
			get;
			set;
		}

		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_ImgFileName
		{
			get;
			set;
		}

		[Index("Kind", 2)]
		[Index("Kind_2", 2)]
		[NoDefault]
		[Required]
		[StringLength(80)]
		public string m_Instrument
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
		[StringLength(80)]
		public string m_Name
		{
			get;
			set;
		}

		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_RefFileName
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

		public NDLG_KnownTestchart()
		{
		}
	}
}