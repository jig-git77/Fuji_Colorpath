using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IDestinationProfileBusiness : ITaskBase
    {
        Dictionary<int, string> GetDestinationProfiles(int SyncDeviceId);
        List<DestinationProfile> GetDestinationProfileList(int SyncDeviceId);
    }
    
    
}
