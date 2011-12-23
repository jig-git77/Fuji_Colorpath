using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IDeviceLinkProfileBusiness : ITaskBase
    {
        List<DeviceLinkProfile> GetDeviceLinkProfileList(int sourceProfileId, int destinationProfileId);
        List<DeviceLinkProfile> GetDeviceLinkProfileListOnSyncDeviceId(int syncDeviceId);
        bool CreateDeviceLinkProfile(DeviceLinkProfile objDeviceLinkProfile);
        DeviceLinkProfile GetDeviceLinkProfileListByDLPID(int DLPID);
    }
}
