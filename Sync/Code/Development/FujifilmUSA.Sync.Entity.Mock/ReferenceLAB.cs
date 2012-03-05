namespace FujifilmUSA.Sync.Entity.Mock
{
    public class ReferenceLab
    {
        public int ReferenceLabid { get; set; }
        public short SourceProfileId { get; set; }
        public int ReferenceChartId { get; set; }
        public decimal TargetL { get; set; }
        public decimal Targeta { get; set; }
        public decimal Targetb { get; set; }
        public SourceProfile SourceProfile { get; set; }
        public ReferenceChart ReferenceChart { get; set; }
    }
}