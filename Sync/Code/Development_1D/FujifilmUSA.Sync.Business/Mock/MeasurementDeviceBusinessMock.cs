using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
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

        public List<MeasurementDevice> GetMeasurementDevice(int measurementDeviceId, int currentUserId)
        {
            return new List<MeasurementDevice>();
        }

        public bool UpdateMeasurementDeviceStatus(int measurementDeviceId)
        {
            return true;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion


       
    }
}
