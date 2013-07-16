namespace FujifilmUSA.Sync.Entity
{
    public class ReferenceLab
    {
        public int ReferenceLabid { get; set; }
        public short SourceProfileId { get; set; }
        public int ReferenceChartId { get; set; }
        public decimal TargetL { get; set; }
        public decimal Targeta { get; set; }
        public decimal Targetb { get; set; }
    }
}