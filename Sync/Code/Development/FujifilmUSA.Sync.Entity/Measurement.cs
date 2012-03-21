using System.Collections.Generic;
using System;
namespace FujifilmUSA.Sync.Entity
{
    public class Measurement
    {
        public int MeasurementId { get; set; }
        public string MeasurementName { get; set; }
        public int OptimizeRunId { get; set; }
        public DateTime MeasurementDate { get; set; }
        public short ReferenceChartTypeId { get; set; }
        public bool IsUsed { get; set; }
        public List<MeasurementPatch> MeasurementPatches { get; set; }
        //public OptimizeRun OptimizeRun { get; set; }

        //For Anaylze Chart
        public decimal? Δe { get; set; }
        public int MeasurementNo { get; set; }
        public ReferenceChart ReferenceChart { get; set; } 

    }
}