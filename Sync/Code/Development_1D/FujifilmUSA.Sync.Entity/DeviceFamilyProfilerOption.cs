using System;
using System.Collections.Generic;

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
        public int UserID { get; set; }
        public bool IsCustomOption { get; set; }
     
    }
}