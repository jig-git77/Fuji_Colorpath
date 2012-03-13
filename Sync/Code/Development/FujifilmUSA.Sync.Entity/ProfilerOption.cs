using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class ProfilerOption
    {
        public short ProfilerOptionId { get; set; }
        public string Option { get; set; }
        public List<ProfilerOptionPreset> ProfilerOptionPresets { get; set; }
        public List<ProjectOption> ProjectOptions { get; set; }
    }
}