using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TCPMeasureValue")]
	public class NDLG_CPMeasureValue
	{
		public float m_Chan0
		{
			get;
			set;
		}

		public float m_Chan1
		{
			get;
			set;
		}

		public float m_Chan2
		{
			get;
			set;
		}

		public float m_Chan3
		{
			get;
			set;
		}

		[DefaultValue(Db_chan_usage.k_lab)]
		public Db_chan_usage m_ChanUsage
		{
			get;
			set;
		}

		[ForeignKey("TCPMeasureValue1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TCPMeasurement", column="ID")]
		[Key(Order=1)]
		public int m_CPMeasurement_ID
		{
			get;
			set;
		}

		[ForeignKey("TCPMeasureValue2", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TCPMeasurePatchField", column="ID")]
		[Index]
		[Key(Order=2)]
		public int m_CPMeasurePatchField_ID
		{
			get;
			set;
		}

		[Index]
		public float? m_DeltaChroma
		{
			get;
			set;
		}

		[Index]
		public float? m_DeltaChromaGray
		{
			get;
			set;
		}

		[Index]
		public float? m_DeltaE
		{
			get;
			set;
		}

		[Index]
		public float? m_DeltaE2000
		{
			get;
			set;
		}

		[Index]
		public float? m_DeltaE94
		{
			get;
			set;
		}

		[Index]
		public float? m_DeltaEcmc_1_1
		{
			get;
			set;
		}

		[Index]
		public float? m_DeltaEcmc_2_1
		{
			get;
			set;
		}

		public float? m_DeltaEToleranceOK
		{
			get;
			set;
		}

		public float? m_DeltaEToleranceWarn
		{
			get;
			set;
		}

		[Index]
		public float? m_DeltaHue
		{
			get;
			set;
		}

		public float? m_DeltaHueToleranceOK
		{
			get;
			set;
		}

		public float? m_DeltaHueToleranceWarn
		{
			get;
			set;
		}

		public float? m_Density
		{
			get;
			set;
		}

		public NDLG_CPMeasureValue()
		{
		}
	}
}