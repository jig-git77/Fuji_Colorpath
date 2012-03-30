namespace FujifilmUSA.Sync.Entity
{
    public class ReferenceChartData : ReferenceChartType
    {
        public string MeasureDeviceSrNo { get; set; }
        public ChartSetting ChartSetting { get; set; }
        public string ActivationKey { get; set; }
        public MeasurementDevice DeviceSetting { get; set; }
    }
}
