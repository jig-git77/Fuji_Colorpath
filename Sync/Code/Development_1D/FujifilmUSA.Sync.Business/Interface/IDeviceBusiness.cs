using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IDeviceBusiness : ITaskBase
    {
        List<Device> GetDeviceListById(int companyId, int branchId,int currentUserId);
        List<Device> GetDevice(int deviceId,int currentUserId);
        int InsertDevice(Device device);
        int UpdateDevice(Device device);
        bool DeleteDevice(int deviceId);
        int InsertDeviceFamily(DeviceFamily deviceFamily);

        int InsertDeviceFamilyProfilerOption(DeviceFamilyProfilerOption DeviceFamilyProfilerOption);
        List<Device> GetDeviceListForRetargetSpotProject(int companyId, int branchId, int currentUserId);
        List<Device> GetDeviceListForRetarget1DProject(int companyId, int branchId, int currentUserId);
    }
}
