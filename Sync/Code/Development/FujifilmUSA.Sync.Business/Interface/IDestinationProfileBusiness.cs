using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IDestinationProfileBusiness : ITaskBase
    {
        List<DestinationProfile> GetDestinationProfiles(int syncDeviceId);
        DestinationProfile GetDestinationProfile(int destinationProfileId);
        int InsertDestinationProfile(DestinationProfile destinationProfile);
        void UpdateDestinationProfile(int destinationProfileId, byte[] profile);
        bool CreateDestinationProfile(string measurementIds, int destinationId);
        int DeleteDestinationProfile(int destinationProfileId);
    }
}
