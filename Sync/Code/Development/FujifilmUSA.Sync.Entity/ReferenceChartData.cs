using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class ReferenceChartData : ReferenceChartType
    {
        public string MeasureDeviceSrNo { get; set; }
        public ChartSetting ChartSetting { get; set; }
        public string ActivationKey { get; set; }
    }
}
