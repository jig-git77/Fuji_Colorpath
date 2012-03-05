using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity.Mock
{
    public class Region
    {
        public byte RegionId { get; set; }
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
        public List<SourceProfile> SourceProfiles { get; set; }
        public List<Company> Companies { get; set; }
    }
}