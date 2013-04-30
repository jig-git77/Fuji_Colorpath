using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class AnalyzeChart_DotGain : AnalyzeChart_Gray
    {
        //public decimal CmykC { get; set; }
        //public decimal CmykM { get; set; }
        //public decimal CmykY { get; set; }
        //public decimal CmykK { get; set; }
        //public string PatchType { get; set; }

        public decimal DotGain { get; set; }
        public string HTMLColor { get; set; }
    }
}
