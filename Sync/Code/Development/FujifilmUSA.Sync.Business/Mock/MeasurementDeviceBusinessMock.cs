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

        public Entity.MeasurementDevice GetSyncMeasurementDeviceByMeasurementDeviceId(int measurementDeviceId)
        {
            return new Entity.MeasurementDevice();
        }

        public int DeleteSyncMeasurementDevice(int syncMeasurementDeviceId)
        {
            return 0;
        }
        #endregion


        public List<Entity.MeasurementDevice> GetSyncMeasurementDeviceList()
        {
            return new List<Entity.MeasurementDevice>();
        }


        public int InsertSyncMeasurementDevice(Entity.MeasurementDevice measurementDevice)
        {
            return 0;
        }

        public int UpdateSyncMeasurementDevice(Entity.MeasurementDevice measurementDevice)
        {
            return 0;
        }


        public Dictionary<int, string> GetDongleList(int branchId)
        {
            Dictionary<int, string> dongleList = new Dictionary<int, string>();
            dongleList.Add(1, "Demo");
            return dongleList;
        }


        public Dictionary<int, string> GetMeasurementDeviceTypeList()
        {
            Dictionary<int, string> measurementDeviceTypeList = new Dictionary<int, string>();
            measurementDeviceTypeList.Add(1, "Demo");
            return measurementDeviceTypeList;
        }
    }
}
