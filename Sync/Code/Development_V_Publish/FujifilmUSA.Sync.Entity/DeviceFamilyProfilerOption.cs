using System.Collections.Generic;
using System;

namespace FujifilmUSA.Sync.Entity
{
    [Serializable]
    public class DeviceFamilyProfilerOption
    {
        public int DeviceFamilyProfilerOptionId { get; set; }
        public short DeviceFamilyId { get; set; }
        public string ProfilerOptionsName { get; set; }
        public string ProfilerOptionsDesc { get; set; }
        public List<ProfilerOptionPreset> ProfilerOptionPresets { get; set; }
        public int ProjectTypeID { get; set; }
    }
}