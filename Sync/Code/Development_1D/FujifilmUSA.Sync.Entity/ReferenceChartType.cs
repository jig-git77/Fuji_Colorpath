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
        public byte[] ChartPreview { get; set; }
        public byte[] OriginalChart { get; set; }      
        public List<ReferenceChart> ReferenceCharts { get; set; }
        public InstrumentType InstrumentType { get; set; }
        public short? IsSync1D_ISO { get; set; }
        public short? IsSync1D_G7 { get; set; }
        public short? Validation_1D { get; set; }
        public short? IsSync4D { get; set; }
        public short? Validation_4D { get; set; }      
        public bool? IsUsed { get; set; }
        public string ChartSettingXML { get; set; }
    }
}