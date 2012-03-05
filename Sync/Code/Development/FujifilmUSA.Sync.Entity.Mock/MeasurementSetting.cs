namespace FujifilmUSA.Sync.Entity.Mock
{
    public class MeasurementSetting
    {
        public int MeasurementSettingId { get; set; }
        public int ProjectId { get; set; }
        public byte ObserverType { get; set; }
        public byte Illuminant { get; set; }
        public bool UvIncluded { get; set; }
        public short? MeasurementDeviceId { get; set; }
        public byte ReferenceChartTypeId { get; set; }
        public Project Project { get; set; }
        public MeasurementDevice MeasurementDevice { get; set; }
    }
}