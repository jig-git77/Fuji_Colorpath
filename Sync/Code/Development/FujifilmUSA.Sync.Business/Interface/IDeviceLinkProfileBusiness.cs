using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IDeviceLinkProfileBusiness : ITaskBase
    {
        List<DeviceLinkProfile> GetDeviceLinkProfiles(int sourceProfileId, int destinationProfileId);
        List<DeviceLinkProfile> GetDeviceLinkProfilesOnSyncDeviceId(int syncDeviceId);
        bool CreateDeviceLinkProfile(DeviceLinkProfile objDeviceLinkProfile);
        DeviceLinkProfile GetDeviceLinkProfile(int dlpId);
        int DeleteDeviceLinkProfile(int dlpId);
    }
}
