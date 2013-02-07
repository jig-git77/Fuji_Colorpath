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
        public DateTime MeasurementDate { get; set; }

        public decimal Avg_ΔE { get; set; } 
        public decimal Max_ΔE { get; set; }
        public decimal Pri_ΔE { get; set; }
        public decimal Pri_ΔH { get; set; }
        public decimal Gray_ΔE { get; set; }

        public decimal PercΔE_10 { get; set; }
        public decimal PercΔE_90 { get; set; }
        public decimal PercΔE_95 { get; set; }
    }
}
