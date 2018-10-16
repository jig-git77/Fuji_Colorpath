using System.Collections.Generic;

namespace FujifilmUSA.Sync.Entity
{
    public class ReferenceChartData : ReferenceChartType
    {
        public string MeasureDeviceSrNo { get; set; }
        public ChartSetting ChartSetting { get; set; }
        public string ActivationKey { get; set; }
        public MeasurementDevice DeviceSetting { get; set; }      
    }

    public class ReferenceChartPlugin
    {
        public string MeasureDeviceSrNo { get; set; }
        public ChartSetting ChartSetting { get; set; }
        public string ActivationKey { get; set; }
        public MeasurementDevicePlugin DeviceSetting { get; set; }
        //
        public short ReferenceChartTypeId { get; set; }
        public string ChartName { get; set; }
        public int NumOfRows { get; set; }
        public int NumOfCols { get; set; }
        public int TotalPatches { get; set; }
        public short InstrumentTypeId { get; set; }
        public List<ReferenceChart> ChartPatches { get; set; }
    }

    public class MeasurementDevicePlugin
    {        
        public string SerialNumber { get; set; }
        public short InstrumentTypeId { get; set; }        
        public MeasurementSettingPlugin MeasurementSettings { get; set; }        
    }

    public class MeasurementSettingPlugin
    {        
        public string ObserverTypeName { get; set; }
        public string IlluminantName { get; set; }
        public string UvIncludedName { get; set; }
        public short UVMode { get; set; }
        public string ReadMode { get; set; }
        public short MeasurementCount { get; set; }
        public string DensityType { get; set; }
    }
}
