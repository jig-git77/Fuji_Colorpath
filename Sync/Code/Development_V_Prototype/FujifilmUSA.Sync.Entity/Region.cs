using System;
namespace FujifilmUSA.Sync.Entity
{
    [Serializable]
    public class Region
    {
        public short RegionId { get; set; }
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
        //public List<SourceProfile> SourceProfiles { get; set; }
        //public List<Company> Companies { get; set; }
    }
}