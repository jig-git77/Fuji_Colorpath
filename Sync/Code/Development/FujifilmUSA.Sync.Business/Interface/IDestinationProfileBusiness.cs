using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IDestinationProfileBusiness : ITaskBase
    {
        List<DestinationProfile> GetDestinationProfileList(int syncDeviceId);
        DestinationProfile GetDestinationProfileByDestinationProfileId(int destinationProfileId);
        int InsertDestinationProfile(DestinationProfile objDestinationProfile);
        void UpdateDestinationProfile(int destinationProfileId, byte[] profile);
        bool CreateDestinationProfile(string measurementIds, int destinationId);
    }
}
