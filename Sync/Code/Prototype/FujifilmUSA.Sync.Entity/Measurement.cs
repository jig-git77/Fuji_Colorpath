using System;
using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class Measurement
    {
        public int MeasurementId { get; set; }
        public string MeasurementName { get; set; }
        public int SyncRunId { get; set; }
        public DateTime MeasurementDate { get; set; }
        public bool IsUse { get; set; }
        public string MeasurementFile { get; set; }
        public short MeasurementTypeId { get; set; }
        public List<MeasurementPatch> Patches { get; set; }
    }
}