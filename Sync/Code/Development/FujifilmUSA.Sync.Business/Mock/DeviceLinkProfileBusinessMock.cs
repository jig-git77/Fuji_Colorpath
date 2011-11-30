using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class DeviceLinkProfileBusinessMock : IDeviceLinkProfileBusiness
    {
        #region IDeviceLinkProfileBusiness Members

        public List<DeviceLinkProfile> GetDeviceLinkProfileList()
        {
            return new List<DeviceLinkProfile>();
        }

        #endregion
    }
}
