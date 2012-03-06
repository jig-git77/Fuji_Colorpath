namespace FujifilmUSA.Sync.Entity
{
    public class ProjectOption
    {
        public int ProjectOptionId { get; set; }
        public int ProjectId { get; set; }
        public byte ProfileTypeId { get; set; }
        public byte ProfilerOptionId { get; set; }
        public string ProfilerOptionValue { get; set; }
        public ProfilerOption ProfilerOption { get; set; }
        public Project Project { get; set; }
        public ProfileType ProfileType { get; set; }
    }
}