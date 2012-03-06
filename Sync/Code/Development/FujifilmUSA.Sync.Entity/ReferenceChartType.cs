using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class ReferenceChartType
    {
        public byte ReferenceChartTypeId { get; set; }
        public string ChartName { get; set; }
        public byte? NumOfRows { get; set; }
        public byte? NumOfCols { get; set; }
        public byte? InstrumentTypeId { get; set; }
        public byte ProjectTypeId { get; set; }
        public byte[] ChartPreview { get; set; }
        public bool? IsUsed { get; set; }
        public List<ChartSetting> ChartSettings { get; set; }
        public List<ReferenceChart> ReferenceCharts { get; set; }
    }
}