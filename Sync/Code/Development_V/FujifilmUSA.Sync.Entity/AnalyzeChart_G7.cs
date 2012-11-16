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

        public int MetricMeasuredID { get; set; }
        public string Metric { get; set; }
        public decimal Tolerance { get; set; }
        public decimal Actual { get; set; }
        public bool IsNotOK { get; set; }
    }
}
