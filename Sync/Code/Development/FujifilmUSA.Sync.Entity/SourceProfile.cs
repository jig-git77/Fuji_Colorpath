using System;

namespace FujifilmUSA.Sync.Entity
{
    public class SourceProfile : IBaseProfile
    {
        public SourceProfile()
        {
            Data = new ProfileData();
        }
        #region IBaseProfile Members

        public int ProfileId { get; set; }

        public string ProfileName { get; set; }

        public ProfileData Data { get; set; }

        public int SyncDeviceId { get; set; }

        public bool IsUse { get; set; }

        #endregion
    }
}
