using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity.CPSyncV
{
    [Serializable]
    public class MetricTargetPreset
    {
        public int MetricTargetPresetID { get; set; }
        public Region Region { get; set; }
        public Company Company { get; set; }
        public short? RegionId { get; set; }
        public int? CompanyId { get; set; }
        public string MetricTargetPresetName { get; set; }
        public string MetricTargetPresetDesc { get; set; }
        public string dEMethod { get; set; }       
        public int ValidationType { get; set; }
        public bool IsSync1D { get; set; }
        public bool Validation_1D { get; set; }
        public bool Validation_4D { get; set; }
        public bool IsUsed { get; set; }
        public int ReferenceChartTypeId { get; set; }
        public List<MetricSum> MetricSumTolerances { get; set; }
        public List<PatchTolerance> PatchTolerances { get; set; }
    }
}
