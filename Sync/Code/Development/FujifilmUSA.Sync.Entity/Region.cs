using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class Region
    {
        public short RegionId { get; set; }
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
        public List<SourceProfile> SourceProfiles { get; set; }
        public List<Company> Companies { get; set; }
    }
}