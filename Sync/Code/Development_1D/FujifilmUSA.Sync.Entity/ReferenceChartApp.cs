using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Entity
{
    public class ReferenceChartApp : ReferenceChartType
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int OptimizationRunID { get; set; }
        public ChartSetting ChartSetting { get; set; }
        public MeasurementSettingApp MeasurementSettings { get; set; }
    }

    public class MeasurementSettingApp
    {
        public int ProjectId { get; set; }
        public string DensityType { get; set; }
        public short Illuminant { get; set; }
        public string IlluminantName { get; set; }
        public short MeasurementCount { get; set; }
        public short ObserverType { get; set; }
        public string ObserverTypeName { get; set; }
        public short UVMode { get; set; }
        public bool UvIncluded { get; set; }
        public string UvIncludedName { get; set; }
        public short VMeasurementCount { get; set; }
    }
}
