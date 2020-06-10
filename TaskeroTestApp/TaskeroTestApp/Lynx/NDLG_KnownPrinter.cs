using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TKnownPrinter")]
	public class NDLG_KnownPrinter
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

		[ForeignKey("TCPPrinterDealer1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TDealer", column="ID")]
		[Index("PrNameDlr", 1, is_unique=true)]
		public int? m_DealerID
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

		[Index("PrNameGbl", 1, is_unique=true)]
		public bool m_IsGlobal
		{
			get;
			set;
		}

		[Index("Name", 2)]
		[Index("PrNameDlr", 2)]
		[Index("PrNameGbl", 2)]
		[Index("PrNameGrp", 2)]
		[Index("PrNameUsr", 2)]
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

		[ForeignKey(table="TUser", column="ID")]
		public int? m_ModifyUserID
		{
			get;
			set;
		}

		[Index("Name", 1)]
		[Index("PrNameDlr", 3)]
		[Index("PrNameGbl", 3)]
		[Index("PrNameGrp", 3)]
		[Index("PrNameUsr", 3)]
		[Required]
		[StringLength(120)]
		public string m_Name
		{
			get;
			set;
		}

		[ForeignKey("TPrinterType1", Fk_constraint.k_cascade, Fk_constraint.k_restrict, table="TKnownPrinterType", column="ID")]
		[Index("PrSerNum", 1, is_unique=true)]
		public int? m_PrinterTypeID
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

		[Index("PrSerNum", 2, is_unique=true)]
		[StringLength(255)]
		public string m_SerialNumber
		{
			get;
			set;
		}

		[ForeignKey("TCPPrinterUserGroup1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUserGroup", column="ID")]
		[Index("PrNameGrp", 1, is_unique=true)]
		public int? m_UserGroupID
		{
			get;
			set;
		}

		[ForeignKey("TCPPrinterUser1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUser", column="ID")]
		[Index("PrNameUsr", 1, is_unique=true)]
		public int? m_UserID
		{
			get;
			set;
		}

		public NDLG_KnownPrinter()
		{
		}
	}
}