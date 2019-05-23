using System;
using System.Collections.Generic;
using System.Linq;

namespace FujifilmUSA.Sync.Entity
{
    public class License
    {
        public License()
        {
            LicenseType = new List<ELicenseType>();
        }
        public int LicenseId { get; set; }
        public int BranchId { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public short NumOfDevice { get; set; }
        public string LicenseDesc { get; set; }
        public short LicenseTypeId { get; set; }
        public bool IsOneOff { get; set; }
        public bool IsActive { get; set; }
        public Branch Branch { get; set; }
        public List<ELicenseType> LicenseType { get; set; }
        public bool IsCustomProfOptions { get; set; }
    }
}