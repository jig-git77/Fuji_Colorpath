using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class DeviceFamilyProfilerOption
    {
        public int DeviceFamilyProfilerOptionId { get; set; }
        public short DeviceFamilyId { get; set; }
        public string ProfilerOptionsDesc { get; set; }
        public List<ProfilerOptionPreset> ProfilerOptionPresets { get; set; }
        public DeviceFamily DeviceFamily { get; set; }
    }
}