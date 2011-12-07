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

        public Dictionary<string, string> GetMeasurementDeviceListBySyncDeviceId(int syncDeviceId)
        {
            return new Dictionary<string, string>();
        }

        #endregion
    }
}
