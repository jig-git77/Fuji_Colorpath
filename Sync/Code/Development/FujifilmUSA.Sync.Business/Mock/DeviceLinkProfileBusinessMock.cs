using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class DeviceLinkProfileBusinessMock : IDeviceLinkProfileBusiness
    {
        #region IDeviceLinkProfileBusiness Members

        public List<DeviceLinkProfile> GetDeviceLinkProfiles(int sourceProfileId, int destinationProfileId)
        {
            return new List<DeviceLinkProfile>();
        }
        public List<DeviceLinkProfile> GetDeviceLinkProfilesOnSyncDeviceId(int SyncDeivceId)
        {
            return new List<DeviceLinkProfile>();
        }
        public DeviceLinkProfile GetDeviceLinkProfile(int DLPID)
        {
            return new DeviceLinkProfile();
        }
        public bool CreateDeviceLinkProfile(DeviceLinkProfile objDeviceLinkProfile)
        {
            return true;
        }

        #endregion
    }


}

