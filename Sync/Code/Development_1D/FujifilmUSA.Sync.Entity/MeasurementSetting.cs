using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementSetting
    {
        public int MeasurementSettingId { get; set; }
        public int ProjectId { get; set; }
        public short ObserverType { get; set; }
        public string ObserverTypeName { get; set; }
        public short Illuminant { get; set; }
        public string IlluminantName { get; set; }
        public bool UvIncluded { get; set; }
        public string UvIncludedName { get; set; }
        public string DensityType { get; set; }
        public short? MeasurementDeviceId { get; set; }
        public KeyValuePair<short,string> ReferenceChartType { get; set; }
        public short? VMeasurementDeviceID { get; set; }
        public KeyValuePair<short, string> OptReferenceChartType { get; set; }
        public KeyValuePair<short, string> VReferenceChartType { get; set; }
        public KeyValuePair<int, string> MetricTargetPreset { get; set; }
        public string dEMethod { get; set; }
        public short UVMode { get; set; }
        public short MeasurementCount { get; set; }
        public string ReadMode { get; set; }
        public short? InstrumentTypeId { get; set; }

        public short VUVMode { get; set; }
        public short VMeasurementCount { get; set; }
        public string VReadMode { get; set; }

        public bool PresetWeighted { get; set; }
    }
}