using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class AnalyzeChart_G7 : AnalyzeChart
    {
        public decimal CmykC { get; set; }
        public decimal ΔF { get; set; }
        public decimal ΔFLimit { get; set; }
       
        public string Gray_Quartile { get; set; }
        public decimal AvgΔF { get; set; }
        public decimal MAXΔF { get; set; }
    }
}
