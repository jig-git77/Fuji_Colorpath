namespace FujifilmUSA.Sync.Entity
{
    public class DeviceLinkProfile : IBaseProfile
    {
        public DeviceLinkProfile()
        {
            SrcProfile = new SourceProfile();
            DestProfile = new DestinationProfile();
            Data = new ProfileData();
        }
        public IBaseProfile SrcProfile { get; set; }
        public IBaseProfile DestProfile { get; set; }

        #region IBaseProfile Members

        public int ProfileId { get; set; }

        public string ProfileName { get; set; }

        public ProfileData Data { get; set; }

        public int SyncDeviceId { get; set; }

        public bool IsUse { get; set; }

        #endregion
    }
}
