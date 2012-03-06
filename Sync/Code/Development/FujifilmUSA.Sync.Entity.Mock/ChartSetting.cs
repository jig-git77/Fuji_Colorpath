using System.Xml;

namespace FujifilmUSA.Sync.Entity
{
    public class ChartSetting
    {
        public byte ChartSettingId { get; set; }
        public XmlElement Setting { get; set; }
        public byte ReferenceChartTypeId { get; set; }
        public ReferenceChartType ReferenceChartType { get; set; }
    }
}