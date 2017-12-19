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
        public string ABComparisionSpiderChart { get; set; }
        public string InkPaperPatchChart { get; set; }
        public string ABActualTargetChart { get; set; }
        public string LComparisionChart { get; set; }
        public string LGrayBalanceChart { get; set; }
        public string AGrayBalanceChart { get; set; }
        public string BGrayBalanceChart { get; set; }
    }
}
