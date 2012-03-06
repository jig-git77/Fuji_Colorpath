using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class Device
    {
        public int DeviceId { get; set; }
        public int LicenseId { get; set; }
        public string DeviceName { get; set; }
        public byte DeviceFamilyId { get; set; }
        public bool IsActive { get; set; }
        public List<Project> Projects { get; set; }
        public DeviceFamily DeviceFamily { get; set; }
        public License License { get; set; }
    }
}