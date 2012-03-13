namespace FujifilmUSA.Sync.Entity
{
    public class ProjectOption
    {
        public int ProjectOptionId { get; set; }
        public int ProjectId { get; set; }
        public short ProfileTypeId { get; set; }
        public short ProfilerOptionId { get; set; }
        public string ProfilerOptionValue { get; set; }
        public ProfilerOption ProfilerOption { get; set; }
        public ProfileType ProfileType { get; set; }
    }
}