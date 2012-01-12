using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class DestinationProfileBusinessMock : IDestinationProfileBusiness
    {
        #region IDestinationProfileBusiness Members

        public List<DestinationProfile> GetDestinationProfiles(int syncDeviceId)
        {
            return new List<Entity.DestinationProfile>();
        }

        int IDestinationProfileBusiness.InsertDestinationProfile(DestinationProfile destinationProfile)
        {
            return 0;
        }
        
        public void UpdateDestinationProfile(int destinationProfileId, byte[] profile)
        {

        }
        
        public bool CreateDestinationProfile(string measurementIds, int destinationId,int profilerOptionId)
        {
            return true;
        }

        public DestinationProfile GetDestinationProfile(int destinationProfileId)
        {
            return new DestinationProfile();
        }

        public int DeleteDestinationProfile(int destinationProfileId)
        {
            return 0;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
