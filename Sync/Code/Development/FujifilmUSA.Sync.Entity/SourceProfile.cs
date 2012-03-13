using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class SourceProfile
    {
        public short SourceProfileId { get; set; }
        public short RegionId { get; set; }
        public int? CompanyId { get; set; }
        public string ProfileName { get; set; }
        public string ProfileDesc { get; set; }
        public System.Data.Linq.Binary Profile { get; set; }
        public bool IsUsed { get; set; }
        public List<Project> Projects { get; set; }
        public List<ReferenceLab> ReferenceLABs { get; set; }
        public Company Company { get; set; }
        public Region Region { get; set; }
    }
}