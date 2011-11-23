using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MeasurementDeviceBusinessMock : IMeasurementDeviceBusiness
    {
        #region IMeasurementDeviceBusiness Members

        public Dictionary<int, string> GetMeasurementDeviceListBySyncDeviceId(int syncDeviceId)
        {
            return new Dictionary<int, string>();
        }

        #endregion
    }
}
