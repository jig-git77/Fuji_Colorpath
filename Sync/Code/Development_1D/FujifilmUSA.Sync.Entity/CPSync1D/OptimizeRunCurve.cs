using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity.CPSync1D
{
    public class OptimizeRunCurve
    {
        public int OptimizeRunCurveID { get; set; }
        public int OptimizeRunID { get; set; }
        public short ReferenceNo { get; set; }
        public decimal CMYK_C { get; set; }
        public decimal CMYK_M { get; set; }
        public decimal CMYK_Y { get; set; }
        public decimal CMYK_K { get; set; }
    }
}
