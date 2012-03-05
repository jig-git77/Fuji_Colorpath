using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity.Mock
{
    public class LicenseType
    {
        public byte LicenseTypeId { get; set; }
        public string Type { get; set; }
        public List<License> Licenses { get; set; }
    }
}