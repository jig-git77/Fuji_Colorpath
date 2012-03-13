using System.Xml;

namespace FujifilmUSA.Sync.Entity
{
    public class ChartSetting
    {
        public short ChartSettingId { get; set; }
        public XmlElement Setting { get; set; }
        public short ReferenceChartTypeId { get; set; }
        public ReferenceChartType ReferenceChartType { get; set; }
    }
}