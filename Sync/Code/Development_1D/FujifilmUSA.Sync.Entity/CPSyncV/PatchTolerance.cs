using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity.CPSyncV
{
    public class PatchTolerance
    {
        public int ReferenceChartID { get; set; }
        public int SampleID { get; set; }
        public string SampleName { get; set; }
        public decimal TargetValue { get; set; }
    }
}
