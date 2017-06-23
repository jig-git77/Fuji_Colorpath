using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class AnalyzeChart_DotGain : AnalyzeChart_Gray
    {
        public decimal DotGain { get; set; }    
        public decimal TargetDotGain { get; set; }
        public decimal Threshold { get; set; }
        public decimal DotGainSpreadTh { get; set; }
        public decimal TVI { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal TonePct { get; set; }
        public decimal MaxSpread { get; set; }
    }
}
