namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementSetting
    {
        public int MeasurementSettingId { get; set; }
        public int ProjectId { get; set; }
        public short ObserverType { get; set; }
        public short Illuminant { get; set; }
        public bool UvIncluded { get; set; }
        public short? MeasurementDeviceId { get; set; }
        public short ReferenceChartTypeId { get; set; }
        public Project Project { get; set; }
        public MeasurementDevice MeasurementDevice { get; set; }
    }
}