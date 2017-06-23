using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementPatchDetails    //-------Dhruti's code for Patch Details display
    {
        public int SampleId { get; set; }        
        public decimal CmykC { get; set; }
        public decimal CmykM { get; set; }
        public decimal CmykY { get; set; }
        public decimal CmykK { get; set; }       
        public string HtmlColor { get; set; }
        public decimal TargetL { get; set; }
        public decimal Targeta { get; set; }
        public decimal Targetb { get; set; }
        public decimal CielabL { get; set; }
        public decimal CielabA { get; set; }
        public decimal CielabB { get; set; }
        public decimal? ΔE { get; set; }
        public decimal? ΔE94 { get; set; }
        public decimal? ΔE2000 { get; set; }       
    }
}
