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
        public short? IsSync4D_Plus { get; set; }
        public short? IsDLPMultiColor { get; set; }
        public short? Validation_4DPlus { get; set; }
        public short? Validation_4D_Mutli_Color { get; set; }
        public bool? IsSupportMulticolor { get; set; }
        public short TotalNoColor { get; set; }
        public short? Channel_5 { get; set; }
        public short? Channel_6 { get; set; }
        public short? Channel_7 { get; set; }
        public short? Channel_8 { get; set; }
        public short? Channel_9 { get; set; }
        public short? DeviceFamilyId { get; set; }

        public bool? IsUsed { get; set; }
        public string ChartSettingXML { get; set; }

        public ProjectType ProjectType { get; set; }
        public int ChartDisplayType { get; set; }
    }
}