using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TCPMeasurement")]
	public class NDLG_CPMeasurement
	{
		public Db_certification_actions_done m_ActionsDone
		{
			get;
			set;
		}

		public Db_certification_delta_e_type? m_CertificateDeltaEType
		{
			get;
			set;
		}

		public Db_certification_result? m_CertificateResult
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ClientName
		{
			get;
			set;
		}

		[Required]
		[Text]
		public string m_Comment
		{
			get;
			set;
		}

		[ForeignKey("TCPMeasurement1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TCPPrintObject", column="ID")]
		[Index]
		public int? m_CPPrintObject_ID
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_CPSerialNumber
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

		[Index]
		public int? m_Job_ID
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_JobName
		{
			get;
			set;
		}

		[Required]
		[Text]
		public string m_MeasureChartGeometryData
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_MeasureChartGeometryName
		{
			get;
			set;
		}

		public DateTime? m_MeasureDeadlineTime
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_MeasureDeviceProperties
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_MeasureDeviceType
		{
			get;
			set;
		}

		public uint m_MeasureFinishedCount
		{
			get;
			set;
		}

		[NoDefault]
		public DateTime m_MeasureFinishedTime
		{
			get;
			set;
		}

		[Index("MeasureID", 2)]
		[Required]
		[StringLength(255)]
		public string m_MeasureHost
		{
			get;
			set;
		}

		[Index("MeasureID", 1)]
		public int m_MeasureID
		{
			get;
			set;
		}

		[DefaultValue("CURRENT_TIMESTAMP")]
		public DateTime m_ModificationTime
		{
			get;
			set;
		}

		[Index]
		[Required]
		[StringLength(255)]
		public string m_OperatorName
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_Originator
		{
			get;
			set;
		}

		public int? m_PatchFieldsPerLine
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProofCalibrationName
		{
			get;
			set;
		}

		public int m_ProofCutterCurLevel
		{
			get;
			set;
		}

		public int m_ProofCutterMaxLevel
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProofCutterName
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProofIccProfileNameInputYmck
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProofIccProfileNameProof
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProofMediaName
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProofORISdatName
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProofPrintedTimeFormatted
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProofPrinterModel
		{
			get;
			set;
		}

		[Index]
		[Required]
		[StringLength(255)]
		public string m_ProofPrinterName
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProofPrinterSerialNumber
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProofPrinterState
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProofScgName
		{
			get;
			set;
		}

		public int? m_ProofSequenceNumber
		{
			get;
			set;
		}

		public NDLG_CPMeasurement()
		{
		}
	}
}