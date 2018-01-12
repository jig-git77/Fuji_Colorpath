using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class ChartContent
    {
        public int? MeasurementId { get; set; }
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
    }
}
