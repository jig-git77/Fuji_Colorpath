using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IDestinationProfileBusiness : ITaskBase
    {
        List<DestinationProfile> GetDestinationProfileList(int syncDeviceId);
    }
}
