using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TKnownManufacturer")]
	public class NDLG_KnownManufacturer
	{
		[Key]
		public int m_ID
		{
			get;
			set;
		}

		[Index(is_unique=true)]
		[NoDefault]
		[Required]
		[StringLength(100)]
		public string m_Name
		{
			get;
			set;
		}

		[NoDefault]
		public Db_manufacturer_products m_Products
		{
			get;
			set;
		}

		public NDLG_KnownManufacturer()
		{
		}
	}
}