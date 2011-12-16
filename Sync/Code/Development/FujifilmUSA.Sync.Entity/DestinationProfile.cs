using System;

namespace FujifilmUSA.Sync.Entity
{
    public class DestinationProfile : ProfileData
    {
        public int DestinationProfileId { get; set; }
        public string DestinationProfileName { get; set; }
        public Boolean IsUse { get; set; }
        public int SyncDeviceId { get; set; }
    }
}
