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
        public string UploadDate { get; set; }
        public int ProjectId { get; set; }
        public string Notes { get; set; }
        public string Customer { get; set; }
        public string PageOrSurface { get; set; }
        public string JDFJobId { get; set; }

        public string dEMethod { get; set; }
        public List<ValidationMetrics> ValidationMetrics { get; set; }
        public string MeasurementStatus { get; set; }
        public string MetricTargetPresetName { get; set; }
        public int ValidationType { get; set; }
        public string PrintStandardType { get; set; }
        public int PCSDataType { get; set; }
        public List<Entity.CPSyncV.PatchTolerance> PatchTolerances { get; set; }
        public string MeasurementDateString { get; set; }
    }
}