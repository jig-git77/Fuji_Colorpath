using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity.CPSyncV
{
    public class MetricSum
    {        
        public int MetricSumId { get; set; }
        public string MetricSumName { get; set; }
        public decimal TargetValue { get; set; }        
        public int MetricTargetPresetID { get; set; }
        public int MetricTargetID { get; set; }
        public int SortOrder { get; set; }
    }
}
