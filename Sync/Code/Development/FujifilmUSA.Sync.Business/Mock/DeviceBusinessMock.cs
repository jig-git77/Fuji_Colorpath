using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class DeviceBusinessMock : IDeviceBusiness
    {
        #region IDeviceBusiness Members

        public List<Device> GetDeviceListById(int companyId, int branchId)
        {
            return new List<Device>();
        }

        public List<Device> GetDevice(int deviceId,int currentUserId)
        {
            return new List<Device>();
        }

        public int InsertDevice(Device device)
        {
            return 0;
        }

        public int UpdateDevice(Device device)
        {
            return 0;
        }

        public bool DeleteDevice(int deviceId)
        {
            return false;
        }

        public int InsertDeviceFamily(DeviceFamily deviceFamily)
        {
            return 0;
        }


        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion


       
    }
}
