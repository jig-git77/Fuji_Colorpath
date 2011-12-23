using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MeasurementDeviceBusinessMock : IMeasurementDeviceBusiness
    {
        #region IMeasurementDeviceBusiness Members

        public MeasurementDevice GetSyncMeasurementDevice(int measurementDeviceId)
        {
            return new MeasurementDevice();
        }

        public bool DeleteSyncMeasurementDevice(int syncMeasurementDeviceId)
        {
            return true;
        }

        public List<MeasurementDevice> GetSyncMeasurementDevices()
        {
            return new List<MeasurementDevice>();
        }


        public int InsertSyncMeasurementDevice(MeasurementDevice measurementDevice)
        {
            return 0;
        }

        public int UpdateSyncMeasurementDevice(MeasurementDevice measurementDevice)
        {
            return 0;
        }
        #endregion
    }
}
