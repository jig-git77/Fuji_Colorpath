using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class Device
    {
        public Device()
        {
            LicenseTypeProject = new Dictionary<ELicenseType, int>();
        }

        public int DeviceId { get; set; }
        public int LicenseId { get; set; }
        public string DeviceName { get; set; }
        public byte DeviceFamilyId { get; set; }
        public bool IsActive { get; set; }
        public DeviceFamily DeviceFamily { get; set; }
        public License License { get; set; }
        public string BranchName { get; set; }
        public Dictionary<ELicenseType,int> LicenseTypeProject { get; set; }
        public bool IsValidation { get; set; }
        public bool IsFlexo { get; set; }
    }
}