using System;

namespace FujifilmUSA.Sync.Entity
{
    public class DeviceLinkProfile : ProfileData
    {
        public int DeviceLinkProfileId { get; set; }
        public string DeviceLinkProfileName { get; set; }
        public Boolean IsUse { get; set; }
        public SourceProfile SrcProfile { get; set; }
        public DestinationProfile DestProfile { get; set; }
    }
}
