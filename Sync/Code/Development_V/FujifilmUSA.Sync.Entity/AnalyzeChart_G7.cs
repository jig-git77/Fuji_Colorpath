using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class AnalyzeChart_G7 : AnalyzeChart
    {
        public decimal Cmyk_Value { get; set; }
        public decimal ΔValue { get; set; }
        public decimal ΔLimit { get; set; }
       
        public string Gray_Quartile { get; set; }
        public decimal AvgΔF { get; set; }
        public decimal MAXΔF { get; set; }
    }
}
