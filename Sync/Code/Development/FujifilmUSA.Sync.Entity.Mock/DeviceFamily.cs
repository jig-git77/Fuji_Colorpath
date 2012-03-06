using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class DeviceFamily
    {
        public byte DeviceFamilyId { get; set; }
        public string DeviceFamilyDesc { get; set; }
        public bool IsActive { get; set; }
        public List<DeviceFamilyProfilerOption> DeviceFamilyProfilerOptions { get; set; }
        public List<Device> Devices { get; set; }
    }
}