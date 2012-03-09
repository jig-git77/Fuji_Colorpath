using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class SyncDeviceBusinessMock : ISyncDeviceBusiness
    {
        #region ISyncDeviceBusiness Members

        public List<Device> GetSyncDevicesByUserId(int taskeroUserId, int roleId, int companyId, int branchId)
        {
            return new List<Device>();
        }

        #endregion
    }
}
