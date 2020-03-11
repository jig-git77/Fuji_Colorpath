using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TCPPrintObject")]
	public class NDLG_CPPrintObject
	{
		[Required]
		[Text]
		public string m_Comment
		{
			get;
			set;
		}

		public DateTime m_CreateTime
		{
			get;
			set;
		}

		public Db_certification_delta_e_type m_DeltaEType
		{
			get;
			set;
		}

		[Index("Name", 3)]
		[Index("RealmID", 2)]
		public int m_History
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

		[DefaultValue(true)]
		[Index("Name", 4)]
		public bool m_IsGlobal
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

		[Index("Name", 1, is_unique=true)]
		[Required]
		[StringLength(120)]
		public string m_Name
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_OutputChemistryBatch
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_OutputChemistryManufacturer
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_OutputChemistrySKU
		{
			get;
			set;
		}

		public float? m_OutputDensityTargetC
		{
			get;
			set;
		}

		public float? m_OutputDensityTargetK
		{
			get;
			set;
		}

		public float? m_OutputDensityTargetM
		{
			get;
			set;
		}

		public float? m_OutputDensityTargetY
		{
			get;
			set;
		}

		public float? m_OutputDensityThreshold
		{
			get;
			set;
		}

		public float? m_OutputDotgainTargetC
		{
			get;
			set;
		}

		public float? m_OutputDotgainTargetK
		{
			get;
			set;
		}

		public float? m_OutputDotgainTargetM
		{
			get;
			set;
		}

		public float? m_OutputDotgainTargetY
		{
			get;
			set;
		}

		public float? m_OutputDotgainThreshold
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_OutputDotShape
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_OutputInkBatch
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_OutputInkManufacturer
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_OutputInkSKU
		{
			get;
			set;
		}

		public float? m_OutputLineScreen
		{
			get;
			set;
		}

		[DefaultValue("1")]
		public float m_OutputLineScreenFactorToLPI
		{
			get;
			set;
		}

		public float? m_OutputPrintContrastTargetC
		{
			get;
			set;
		}

		public float? m_OutputPrintContrastTargetK
		{
			get;
			set;
		}

		public float? m_OutputPrintContrastTargetM
		{
			get;
			set;
		}

		public float? m_OutputPrintContrastTargetY
		{
			get;
			set;
		}

		public float? m_OutputPrintContrastThreshold
		{
			get;
			set;
		}

		public Db_process_color? m_OutputPrintFirstInk
		{
			get;
			set;
		}

		public Db_process_color? m_OutputPrintFourthInk
		{
			get;
			set;
		}

		public Db_process_color? m_OutputPrintSecondInk
		{
			get;
			set;
		}

		public Db_process_color? m_OutputPrintThirdInk
		{
			get;
			set;
		}

		public float? m_OutputResolution
		{
			get;
			set;
		}

		[DefaultValue("1")]
		public float m_OutputResolutionFactorToDPI
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_OutputSubstrateBatch
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_OutputSubstrateManufacturer
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_OutputSubstrateSKU
		{
			get;
			set;
		}

		public float? m_OutputTrapTargetB
		{
			get;
			set;
		}

		public float? m_OutputTrapTargetG
		{
			get;
			set;
		}

		public float? m_OutputTrapTargetR
		{
			get;
			set;
		}

		public float? m_OutputTrapThreshold
		{
			get;
			set;
		}

		public int m_PatchFieldsPerLine
		{
			get;
			set;
		}

		public Db_proof_standard_code m_ProofStandardCode
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ProofStandardName
		{
			get;
			set;
		}

		[Index("Name", 2)]
		[Required]
		[StringLength(80)]
		public string m_ProofSupplierName
		{
			get;
			set;
		}

		[ForeignKey("TCPPrintObjectRealm1", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TCPPrintObjectRealm", column="ID")]
		[Index("RealmID", 1, is_unique=true)]
		public int? m_RealmID
		{
			get;
			set;
		}

		public uint m_RefValueFileSize
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_RefValueIdent
		{
			get;
			set;
		}

		public DateTime m_RefValueTimestamp
		{
			get;
			set;
		}

		public uint m_TargetValueFileSize
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_TargetValueIdent
		{
			get;
			set;
		}

		public DateTime m_TargetValueTimestamp
		{
			get;
			set;
		}

		public float? m_Tolerance95Quantile
		{
			get;
			set;
		}

		public float? m_Tolerance95QuantileWarn
		{
			get;
			set;
		}

		[DefaultValue("10")]
		public float? m_ToleranceAbsMaximum
		{
			get;
			set;
		}

		[DefaultValue("10")]
		public float? m_ToleranceAbsMaximumWarn
		{
			get;
			set;
		}

		[DefaultValue("5")]
		public float? m_ToleranceGray
		{
			get;
			set;
		}

		[DefaultValue("2.5")]
		public float? m_ToleranceGrayChroma
		{
			get;
			set;
		}

		[DefaultValue("2.5")]
		public float? m_ToleranceGrayChromaWarn
		{
			get;
			set;
		}

		[DefaultValue("1.5")]
		public float? m_ToleranceGrayHue
		{
			get;
			set;
		}

		[DefaultValue("1.5")]
		public float? m_ToleranceGrayHueWarn
		{
			get;
			set;
		}

		[DefaultValue("6")]
		public float? m_ToleranceGrayWarn
		{
			get;
			set;
		}

		[DefaultValue("1.5")]
		public float? m_ToleranceHue
		{
			get;
			set;
		}

		[DefaultValue("1.5")]
		public float? m_ToleranceHueWarn
		{
			get;
			set;
		}

		[DefaultValue("3")]
		public float? m_ToleranceMeanGray
		{
			get;
			set;
		}

		[DefaultValue("4.5")]
		public float? m_ToleranceMeanGrayChroma
		{
			get;
			set;
		}

		[DefaultValue("4.5")]
		public float? m_ToleranceMeanGrayChromaWarn
		{
			get;
			set;
		}

		[DefaultValue("4")]
		public float? m_ToleranceMeanGrayWarn
		{
			get;
			set;
		}

		[DefaultValue("4")]
		public float? m_ToleranceMeanOuterGamut
		{
			get;
			set;
		}

		[DefaultValue("4")]
		public float? m_ToleranceMeanOuterGamutWarn
		{
			get;
			set;
		}

		[DefaultValue("3")]
		public float? m_ToleranceMeanSkin
		{
			get;
			set;
		}

		[DefaultValue("4")]
		public float? m_ToleranceMeanSkinWarn
		{
			get;
			set;
		}

		[DefaultValue("3")]
		public float? m_ToleranceMeanSpot
		{
			get;
			set;
		}

		[DefaultValue("4")]
		public float? m_ToleranceMeanSpotWarn
		{
			get;
			set;
		}

		[DefaultValue("4")]
		public float? m_ToleranceMeanValue
		{
			get;
			set;
		}

		[DefaultValue("4")]
		public float? m_ToleranceMeanValueWarn
		{
			get;
			set;
		}

		[DefaultValue("3")]
		public float? m_TolerancePaperSimul
		{
			get;
			set;
		}

		[DefaultValue("3")]
		public float? m_TolerancePaperSimulWarn
		{
			get;
			set;
		}

		[DefaultValue("2")]
		public float? m_TolerancePaperWhiteA
		{
			get;
			set;
		}

		[DefaultValue("2")]
		public float? m_TolerancePaperWhiteAWarn
		{
			get;
			set;
		}

		[DefaultValue("2")]
		public float? m_TolerancePaperWhiteB
		{
			get;
			set;
		}

		[DefaultValue("2")]
		public float? m_TolerancePaperWhiteBWarn
		{
			get;
			set;
		}

		[DefaultValue("3")]
		public float? m_TolerancePaperWhiteL
		{
			get;
			set;
		}

		[DefaultValue("3")]
		public float? m_TolerancePaperWhiteLWarn
		{
			get;
			set;
		}

		[DefaultValue("8")]
		public float? m_TolerancePriaSecColor
		{
			get;
			set;
		}

		[DefaultValue("8")]
		public float? m_TolerancePriaSecColorWarn
		{
			get;
			set;
		}

		[DefaultValue("6.5")]
		public float? m_TolerancePriaSecHue
		{
			get;
			set;
		}

		[DefaultValue("6.5")]
		public float? m_TolerancePriaSecHueWarn
		{
			get;
			set;
		}

		[DefaultValue("5")]
		public float? m_TolerancePrimColor
		{
			get;
			set;
		}

		[DefaultValue("5")]
		public float? m_TolerancePrimColorWarn
		{
			get;
			set;
		}

		[DefaultValue("5")]
		public float? m_ToleranceSkin
		{
			get;
			set;
		}

		[DefaultValue("6")]
		public float? m_ToleranceSkinWarn
		{
			get;
			set;
		}

		[DefaultValue("5")]
		public float? m_ToleranceSpot
		{
			get;
			set;
		}

		[DefaultValue("6")]
		public float? m_ToleranceSpotWarn
		{
			get;
			set;
		}

		public NDLG_CPPrintObject()
		{
		}
	}
}