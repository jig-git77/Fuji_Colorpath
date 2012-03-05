namespace FujifilmUSA.Sync.Entity
{
    public class DestinationProfile : IBaseProfile
    {
        public DestinationProfile()
        {
            Data = new ProfileData();
        }
        #region IBaseProfile Members
        public int ProfileId { get; set; }
        public string ProfileName{ get; set; }
        public ProfileData Data{ get; set; }
        public int SyncDeviceId{ get; set; }
        public bool IsUse{ get; set; }
        public int ProfileOptionId { get; set; }

        public int ReferenceChartTypeId { get; set; }
        public string SerialNumber { get; set; }
        #endregion
    }
}
