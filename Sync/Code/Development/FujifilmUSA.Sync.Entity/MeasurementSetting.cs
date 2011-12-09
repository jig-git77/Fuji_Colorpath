using System;

namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementSetting
    {
        public int MeasurementSettingId { get; set; }
        public int ProjectId { get; set; }
        public Boolean UsePlugin { get; set; }
        public int Observer { get; set; }
        public int Illuminant { get; set; }
        public int DensityType { get; set; }
        public Boolean UVIncluded { get; set; }
        public int ReferenceChartTypeId { get; set; }
        public int IlluminationMode { get; set; }
        public int InstrumentTypeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
