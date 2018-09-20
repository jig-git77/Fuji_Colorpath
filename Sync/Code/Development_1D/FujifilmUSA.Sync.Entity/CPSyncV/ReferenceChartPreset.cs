using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity.CPSyncV
{
    public class ReferenceChartPreset
    {
        public int ReferenceChartPresetID { get; set; }
        public int ReferenceChartTypeId { get; set; }
        public int MetricTargetPresetID { get; set; }
    }
}
