namespace FujifilmUSA.Sync.Entity
{
    public class ReferenceChart
    {
        public int ReferenceChartId { get; set; }
        public short ReferenceChartTypeId { get; set; }
        public string SampleName { get; set; }
        public decimal CmykC { get; set; }
        public decimal CmykM { get; set; }
        public decimal CmykY { get; set; }
        public decimal CmykK { get; set; }
        public short PatchTypeId { get; set; }
        public string HtmlColor { get; set; }
    }
}
