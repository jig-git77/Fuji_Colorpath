using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class DeviceBusinessMock : IDeviceBusiness
    {
        #region ISyncDeviceBusiness Members

        public List<Device> DeviceListById(int companyId, int branchId)
        {
            return new List<Device>();
        }

        #endregion
    }
}
