using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class ValidationMetrics
    {
        public int MetricMeasuredID { get; set; }
        public int MetricSumID { get; set; }
        public string Metric { get; set; }
        public decimal Tolerance { get; set; }
        public decimal Actual { get; set; }
        public bool IsNotOK { get; set; }
    }
}
