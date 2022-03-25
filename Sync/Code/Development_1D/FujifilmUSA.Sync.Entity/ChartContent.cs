using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class ChartContent
    {
        public int? MeasurementId { get; set; }
        public int? ProjectId { get; set; }
        public bool? IsAverageOfMeasurements { get; set; }
        public string IsVerificationProject { get; set; }
        public string IsSync1DTypeProject { get; set; }
        public string fromDeltaE { get; set; }
        public int validationType { get; set; }
        public int CurrentUserId { get; set; }
        public string ReportDLPName { get; set; }
        public string ABComparisionSpiderChart { get; set; }
        public string InkPaperPatchChart { get; set; }
        public string ABActualTargetChart { get; set; }
        public string LComparisionChart { get; set; }
        public string LGrayBalanceChart { get; set; }
        public string AGrayBalanceChart { get; set; }
        public string BGrayBalanceChart { get; set; }
        public string DeltaSummaryChart { get; set; }

        public string InkPaperLAB1Chart { get; set; }
        public string InkPaperLAB2Chart { get; set; }
        public string InkPaperDansityChart { get; set; }

        public string TVI_ISO_CMYK_Chart { get; set; }
        public string TVI_ISO_Cyan_Chart { get; set; }
        public string TVI_ISO_Magenta_Chart { get; set; }
        public string TVI_ISO_Yellow_Chart { get; set; }
        public string TVI_ISO_Black_Chart { get; set; }

        public string TVI_Summary_Chart { get; set; }

        public string GrayBalanceDeltaCH_Chart { get; set; }
        public string GrayBalanceDeltaL_Chart { get; set; }
        public string BlackBalanceDeltaL_Chart { get; set; }

        public string BlackNPDChart { get; set; }
        public string GrayNPDChart { get; set; }

        public ReportGrading grading { get; set; }
        public string ProjectLicenseType { get; set; }
        public short LicenceType { get; set; }
    }

    public class SpotChartContent
    {
        public int? SpotProjectId { get; set; }
        public int? MeasurementId { get; set; }
        public int? OptimizeRunId { get; set; }
        public int CurrentUserId { get; set; }
        public bool hasMeasurement { get; set; }
        public List<SpotPatchDetails> chartDetailList { get; set; }
        public string SpotProjectName { get; set; }
    }

    public class SpotPatchDetails
    {
        public string spotName { get; set; }

        public decimal TargetL { get; set; }
        public decimal Targeta { get; set; }
        public decimal Targetb { get; set; }

        public decimal CIELAB_L { get; set; }
        public decimal CIELAB_a { get; set; }
        public decimal CIELAB_b { get; set; }

        public decimal Measured_L { get; set; }
        public decimal Measured_a { get; set; }
        public decimal Measured_b { get; set; }


        public decimal deltaE { get; set; }
        public decimal deltaE_M_P { get; set; }
        public decimal deltaE_M_T { get; set; }
        public decimal deltaE_T_P { get; set; }

        public string HTMLColor { get; set; }

        public string patchChart_AB { get; set; }
        public string patchChart_L { get; set; }
    }
}
