using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TKnownPaper")]
	public class NDLG_KnownPaper
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

		[Key]
		public int m_ID
		{
			get;
			set;
		}

		[Index("Kind", 1, is_unique=true)]
		public Db_name_or_rule m_Kind
		{
			get;
			set;
		}

		[ForeignKey("TPaperManufacturer1", Fk_constraint.k_nothing, Fk_constraint.k_nothing, on_update=Fk_constraint.k_cascade, on_delete=Fk_constraint.k_restrict, table="TKnownManufacturer", column="ID")]
		public int? m_ManufacturerID
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

		[Index("Kind", 2)]
		[NoDefault]
		[Required]
		[StringLength(255)]
		public string m_Name
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_PaperColor
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_PaperWeight
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

		public NDLG_KnownPaper()
		{
		}
	}
}