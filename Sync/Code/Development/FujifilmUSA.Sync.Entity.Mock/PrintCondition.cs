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
        public Project Project { get; set; }
    }
}