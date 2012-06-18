using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class ReferenceChartType
    {
        public short ReferenceChartTypeId { get; set; }
        public string ChartName { get; set; }
        public int NumOfRows { get; set; }
        public int NumOfCols { get; set; }
        public int TotalPatches { get; set; }
        public short InstrumentTypeId { get; set; }
        public short ProjectTypeId { get; set; }
        public byte[] ChartPreview { get; set; }
        public byte[] OriginalChart { get; set; }
        public bool? IsUsed { get; set; }
        public List<ReferenceChart> ReferenceCharts { get; set; }
        public InstrumentType InstrumentType { get; set; }
        public ProjectType ProjectType { get; set; }
        public string ChartDisplayType { get; set; }
    }
}