using System;
using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class License
    {
        public int LicenseId { get; set; }
        public int BranchId { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public short NumOfDevice { get; set; }
        public string LicenseDesc { get; set; }
        public short LicenseTypeId { get; set; }
        public bool IsActive { get; set; }
        public List<UserLicense> UserLicenses { get; set; }
        public List<Device> Devices { get; set; }
        public Branch Branch { get; set; }
        public LicenseType LicenseType { get; set; }
    }
}