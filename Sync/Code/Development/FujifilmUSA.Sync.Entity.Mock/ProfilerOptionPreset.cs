namespace FujifilmUSA.Sync.Entity.Mock
{
    public class ProfilerOptionPreset
    {
        public int ProfilerOptionPresetId { get; set; }
        public int DeviceFamilyProfilerOptionId { get; set; }
        public byte? ProfileTypeId { get; set; }
        public byte ProfilerOptionId { get; set; }
        public string ProfilerOptionValue { get; set; }
        public DeviceFamilyProfilerOption DeviceFamilyProfilerOption { get; set; }
        public ProfilerOption ProfilerOption { get; set; }
    }
}