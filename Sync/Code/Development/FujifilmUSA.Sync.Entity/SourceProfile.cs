using System;

namespace FujifilmUSA.Sync.Entity
{
    public class SourceProfile : ProfileData
    {
        public int SourceProfileId { get; set; }
        public string SourceProfileName { get; set; }
        public Boolean IsUse { get; set; }
    }
}
