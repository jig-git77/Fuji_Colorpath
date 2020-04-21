using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TKnownPrinterType")]
	public class NDLG_KnownPrinterType
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

		[ForeignKey("TDedicatedPrinterType", Fk_constraint.k_nothing, Fk_constraint.k_nothing, on_update=Fk_constraint.k_cascade, on_delete=Fk_constraint.k_cascade, table="TknownPrinter", column="ID")]
		[Index(is_unique=true)]
		public int? m_DedicatedPrinterID
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_DFEType
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

		[Index("Name", 2)]
		public bool m_IsGlobal
		{
			get;
			set;
		}

		[ForeignKey("TPrinterManufacturer1", Fk_constraint.k_nothing, Fk_constraint.k_nothing, on_update=Fk_constraint.k_cascade, on_delete=Fk_constraint.k_restrict, table="TKnownManufacturer", column="Name")]
		[Index("Manufacturer", 1)]
		[Required]
		[StringLength(100)]
		public string m_Manufacturer
		{
			get;
			set;
		}

		[Index("Manufacturer", 2)]
		[Required]
		[StringLength(200)]
		public string m_Model
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

		[Index("Name", 1, is_unique=true)]
		[Required]
		[StringLength(255)]
		public string m_Name
		{
			get;
			set;
		}

		public Db_preferred_profiler? m_PreferredProfiler
		{
			get;
			set;
		}

		[DefaultValue("1")]
		[Required]
		[StringLength(255)]
		public string m_RuleParameter
		{
			get;
			set;
		}

		public NDLG_KnownPrinterType()
		{
		}
	}
}