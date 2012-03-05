using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ISyncDeviceBusiness : ITaskBase
    {
        List<SyncDevice> GetSyncDevicesByUserId(int taskeroUserId, int roleId, int companyId, int branchId);
    }
}
