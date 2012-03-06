using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class ReferenceChart
    {
        public int ReferenceChartId { get; set; }
        public byte ChartTypeId { get; set; }
        public short? SampleId { get; set; }
        public string SampleName { get; set; }
        public decimal CmykC { get; set; }
        public decimal CmykM { get; set; }
        public decimal CmykY { get; set; }
        public decimal CmykK { get; set; }
        public byte? PatchTypeID { get; set; }
        public bool? IsOuterGamut { get; set; }
        public string HtmlColor { get; set; }
        public List<ReferenceLab> ReferenceLABs { get; set; }
        public ReferenceChartType ReferenceChartType { get; set; }
    }
}