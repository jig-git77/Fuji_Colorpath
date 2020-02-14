using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TCPMeasurePatchField")]
	public class NDLG_CPMeasurePatchField
	{
		[ForeignKey("TCPMeasurePatchField1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TCPPrintObject", column="ID")]
		[Index("CPPrintObject_ID", 1, is_unique=true)]
		public int m_CPPrintObject_ID
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

		public bool m_IsGray
		{
			get;
			set;
		}

		public bool m_IsSkin
		{
			get;
			set;
		}

		public bool m_IsSpot
		{
			get;
			set;
		}

		[Index("CPPrintObject_ID", 2)]
		public int m_Job_ID
		{
			get;
			set;
		}

		[Index("CPPrintObject_ID", 3)]
		[Required]
		[StringLength(255)]
		public string m_Name
		{
			get;
			set;
		}

		public float m_RefChan0
		{
			get;
			set;
		}

		public float m_RefChan1
		{
			get;
			set;
		}

		public float m_RefChan2
		{
			get;
			set;
		}

		public float m_RefChan3
		{
			get;
			set;
		}

		[DefaultValue(Db_chan_usage.k_cmyk)]
		public Db_chan_usage m_RefChanUsage
		{
			get;
			set;
		}

		[DefaultValue("100")]
		public float m_SpotDensityPercent
		{
			get;
			set;
		}

		public float m_TargetChan0
		{
			get;
			set;
		}

		public float m_TargetChan1
		{
			get;
			set;
		}

		public float m_TargetChan2
		{
			get;
			set;
		}

		public float m_TargetChan3
		{
			get;
			set;
		}

		[DefaultValue(Db_chan_usage.k_lab)]
		public Db_chan_usage m_TargetChanUsage
		{
			get;
			set;
		}

		public NDLG_CPMeasurePatchField()
		{
		}
	}
}