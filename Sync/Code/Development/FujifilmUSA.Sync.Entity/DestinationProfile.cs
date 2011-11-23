using System;

namespace FujifilmUSA.Sync.Entity
{
    public class DestinationProfile
    {
        public int DestinationProfileId { get; set; }
        public string DestinationProfileName { get; set; }
        public string Profile { get; set; }
        public Boolean IsUse { get; set; }
    }
}
