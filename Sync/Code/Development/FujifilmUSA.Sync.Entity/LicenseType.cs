using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class LicenseType
    {
        public short LicenseTypeId { get; set; }
        public string Type { get; set; }
        public List<License> Licenses { get; set; }
    }
}