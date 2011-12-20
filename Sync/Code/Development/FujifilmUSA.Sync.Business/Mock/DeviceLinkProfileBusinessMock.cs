using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class DeviceLinkProfileBusinessMock : IDeviceLinkProfileBusiness
    {
        #region IDeviceLinkProfileBusiness Members

        public List<DeviceLinkProfile> GetDeviceLinkProfileList(int sourceProfileId, int destinationProfileId)
        {
            return new List<DeviceLinkProfile>();
        }
        public List<DeviceLinkProfile> GetDeviceLinkProfileListOnSyncDeviceId(int SyncDeivceId)
        {
            return new List<DeviceLinkProfile>();
        }
        public int InsertDeviceLinkProfile(DeviceLinkProfile objDeviceLinkProfile)
        {
            return 0;
        }
        public void UpdateDestinationProfile(int deviceLinkProfileId, byte[] profile)
        {
        }
        #endregion





      
    }


}

