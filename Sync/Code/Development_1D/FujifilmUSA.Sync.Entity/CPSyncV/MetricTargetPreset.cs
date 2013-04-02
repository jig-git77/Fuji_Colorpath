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
        public byte? RegionID { get; set; }
        public int? CompanyID { get; set; }
        public string MetricTargetPresetName { get; set; }
        public string MetricTargetPresetDesc { get; set; }
        public string dEMethod { get; set; }
        public bool IsUsed { get; set; }
        public short ProjectTypeId { get; set; }
    }
}
