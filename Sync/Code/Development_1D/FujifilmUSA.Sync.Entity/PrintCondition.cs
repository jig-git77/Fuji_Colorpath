namespace FujifilmUSA.Sync.Entity
{
    public class PrintCondition
    {
        public int PrintConditionId { get; set; }
        public int ProjectId { get; set; }
        public string Media { get; set; }
        public string Colorant { get; set; }
        public string RipSettings { get; set; }
        public string TechnicianName { get; set; }
        public string Notes { get; set; }
        public EInkOrder InkOrder { get; set; }
        public string FountainSolution { get; set; }
        public string Blanket { get; set; }
        public string BlanketAge { get; set; }
        public string Screening { get; set; }
        public string Coating { get; set; }
        //public Project Project { get; set; }
    }
}