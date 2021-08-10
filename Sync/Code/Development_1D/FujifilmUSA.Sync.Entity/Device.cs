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
        public Dictionary<ELicenseType, int> LicenseTypeProject { get; set; }
        public bool IsValidation { get; set; }
        public bool IsFlexo { get; set; }
        public bool IsSupportMulticolor { get; set; }
        public short TotalNoColor { get; set; }
        public short Channel_5 { get; set; }
        public short Channel_6 { get; set; }
        public short Channel_7 { get; set; }
        public short Channel_8 { get; set; }
        public short Channel_9 { get; set; }

        public string Channel_5_Name { get; set; }
        public string Channel_6_Name { get; set; }
        public string Channel_7_Name { get; set; }
        public string Channel_8_Name { get; set; }
        public string Channel_9_Name { get; set; }
    }
}