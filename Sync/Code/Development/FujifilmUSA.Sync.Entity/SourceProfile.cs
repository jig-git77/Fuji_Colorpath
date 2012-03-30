using System.Collections.Generic;
using System;
namespace FujifilmUSA.Sync.Entity
{
    [Serializable]
    public class SourceProfile
    {
        public short SourceProfileId { get; set; }
        public short RegionId { get; set; }
        public int? CompanyId { get; set; }
        public string ProfileName { get; set; }
        public string ProfileDesc { get; set; }
        public byte[] Profile { get; set; }
        public bool IsUsed { get; set; }
        public Company Company { get; set; }
        public Region Region { get; set; }
    }
}