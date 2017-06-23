using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class AnalyzeChart  //-------Dhruti's code for Chart display on Analyze Page (Ink Paper Tab)
    {
        public string HtmlColor { get; set; }
        public short PatchTypeId { get; set; }
        
        public decimal ΔE { get; set; }     
        public decimal ΔELimit { get; set; }

        public decimal CielabL { get; set; }
        public decimal CielabA { get; set; }
        public decimal CielabB { get; set; }

        public decimal TargetL { get; set; }
        public decimal Targeta { get; set; }
        public decimal Targetb { get; set; }
              
        public decimal ΔL { get; set; }
        public decimal Δa { get; set; }
        public decimal Δb { get; set; }

        public decimal Density { get; set; }
        public decimal DensityLimit { get; set; }

        public decimal PreAvgΔE { get; set; }
        public decimal PreAvgDensity { get; set; }   
    }
}
