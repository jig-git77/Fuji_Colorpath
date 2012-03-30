using System;
namespace FujifilmUSA.Sync.Entity
{
    [Serializable]
    public class ProfilerOptionPreset
    {
        public int ProfilerOptionPresetId { get; set; }
        public int DeviceFamilyProfilerOptionId { get; set; }
        public short ProfileTypeId { get; set; }
        public string ProfilerOptionValue { get; set; }
        public ProfilerOption ProfilerOption { get; set; }
    }
}