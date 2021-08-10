namespace FujifilmUSA.Sync.Entity
{
    public class ReferenceChart
    {
        public int ReferenceChartId { get; set; }
        public short ChartTypeId { get; set; }
        public short SampleId { get; set; }
        public string SampleName { get; set; }
        public decimal CmykC { get; set; }
        public decimal CmykM { get; set; }
        public decimal CmykY { get; set; }
        public decimal CmykK { get; set; }
        public short PatchTypeId { get; set; }
        public bool? IsOuterGamut { get; set; }
        public string HtmlColor { get; set; }
        public decimal LabL { get; set; }
        public decimal LabA { get; set; }
        public decimal LabB { get; set; }
        public decimal Channel_5 { get; set; }
        public decimal Channel_6 { get; set; }
        public decimal Channel_7 { get; set; }
        public decimal Channel_8 { get; set; }
        public decimal Channel_9 { get; set; }



        //public ReferenceLab ReferenceLABs { get; set; }
        //public ReferenceChartType ReferenceChartType { get; set; }
    }
}