using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity.CPSyncV
{
    public class PatchTolerance
    {
        public int SampleID { get; set; }
        public string SampleName { get; set; }
        public string HtmlColor { get; set; }
        public decimal Tolerance { get; set; }
        public decimal Actual { get; set; }
        public bool IsNotOK { get; set; }
    }
}
