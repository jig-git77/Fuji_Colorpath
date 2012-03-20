using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MeasurementDeviceBusinessMock : IMeasurementDeviceBusiness
    {
        #region  IMeasurementDevice Members

        public int InsertMeasurementDevice(MeasurementDevice company)
        {
            return 0;
        }

        public int UpdateMeasurementDevice(MeasurementDevice company)
        {
            return 0;
        }

        public bool DeleteMeasurementDevice(int companyId)
        {
            return true;
        }

        public List<MeasurementDevice> GetMeasurementDevices()
        {
            return new List<MeasurementDevice>();
        }

        public MeasurementDevice GetMeasurementDevice(int iMeasurementDeviceID)
        {
            return new MeasurementDevice();
        }

        #endregion
    }
}
