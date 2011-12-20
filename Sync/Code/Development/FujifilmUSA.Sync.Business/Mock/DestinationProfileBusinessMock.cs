using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class DestinationProfileBusinessMock : IDestinationProfileBusiness
    {
        #region IDestinationProfileBusiness Members

        public Dictionary<int, string> GetDestinationProfiles(int SyncDeviceId)
        {
            return new Dictionary<int, string>();
        }
        public List<Entity.DestinationProfile> GetDestinationProfileList(int SyncDeviceId)
        {
            return new List<Entity.DestinationProfile>();
        }

        int IDestinationProfileBusiness.InsertDestinationProfile(Entity.DestinationProfile objDestinationProfile)
        {
            return 0;
        }
        
        public void UpdateDestinationProfile(int destinationProfileId, byte[] profile)
        {
        }
        #endregion



      
    }
}
