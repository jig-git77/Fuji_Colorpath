using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class SyncDeviceBusinessMock : ISyncDeviceBusiness
    {
        #region ISyncDeviceBusiness Members

        public List<SyncDevice> GetSyncDeviceListByUserID(int TaskeroUserID, int RoleID, int CompanyID, int BranchID)
        {
            return new List<SyncDevice>();
        }

        #endregion
    }
}
