using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class DeviceBusinessMock : IDeviceBusiness
    {
        #region IDeviceBusiness Members

        public List<Device> GetDeviceListById(int companyId, int branchId, int currentUserId)
        {
            return new List<Device>();
        }

        public List<Device> GetDevice(int deviceId, int currentUserId)
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

        public int InsertDeviceFamilyProfilerOption(DeviceFamilyProfilerOption DeviceFamilyProfilerOption)
        {
            return 0;
        }
        public int InsertCustomDeviceFamilyProfilerOption(DeviceFamilyProfilerOption deviceFamilyProfilerOption, int UserID)
        {
            return 0;
        }
        public List<ProfilerOptionPresetsResponse> GetProfilerOptionPresetsList(int DeviceFamilyProfilerOptionID, int currentUserId)
        {
            return new List<ProfilerOptionPresetsResponse>();
        }
        public int UpdateDeviceFamilyProfilerOption(DeviceFamilyProfilerOption deviceFamilyProfilerOption)
        {
            return 0;
        }
        public int UpdateDeviceFamilyWithoutProfilerOption(DeviceFamilyProfilerOption DeviceFamilyProfilerOption)
        {
            return 0;
        }
        public bool DeleteProfilerOption(int DeviceFamilyProfilerOptionID)
        {
            return true;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion


       
    }
}
