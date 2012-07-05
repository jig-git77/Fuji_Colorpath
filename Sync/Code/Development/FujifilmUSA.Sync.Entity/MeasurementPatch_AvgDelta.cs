using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementPatch_AvgDelta     //-------Dhruti's code for Avg Delta display
    {
        public int MeasurementId { get; set; }
        public string MeasurementName { get; set; }
        public int OptimizeRunId { get; set; }
        public DateTime MeasurementDate { get; set; }

        public decimal Avg_ΔE { get; set; }
        public decimal Avg_ΔE94 { get; set; }
        public decimal Avg_ΔE2000 { get; set; }

        public decimal Max_ΔE { get; set; }
        public decimal Max_ΔE94 { get; set; }
        public decimal Max_ΔE2000 { get; set; }
    }
}
