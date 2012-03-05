using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity.Mock
{
    public class DeviceFamilyProfilerOption
    {
        public int DeviceFamilyProfilerOptionId { get; set; }
        public byte DeviceFamilyId { get; set; }
        public string ProfilerOptionsDesc { get; set; }
        public List<ProfilerOptionPreset> ProfilerOptionPresets { get; set; }
        public DeviceFamily DeviceFamily { get; set; }
    }
}