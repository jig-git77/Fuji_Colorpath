using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Entity
{
    public class ProfilerOptionPresetsResponse
    {
        public string PresetName { get; set; }
        public string ProjectType { get; set; }
        public string DeviceFamily { get; set; }  // Public access modifier
        public bool IsActive { get; set; }
        public int DeviceFamilyProfilerOptionID { get; set; }
        public short? DeviceFamilyID { get; set; }
        public short? ProfileTypeId { get; set; }
        public string ProfilerOptionsDesc { get; set; }
        public int IsDeleted { get; set; }
    }
}
