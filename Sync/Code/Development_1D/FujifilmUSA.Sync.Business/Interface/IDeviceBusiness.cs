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
        int InsertCustomDeviceFamilyProfilerOption(DeviceFamilyProfilerOption deviceFamilyProfilerOption, int UserID);
        List<ProfilerOptionPresetsResponse> GetProfilerOptionPresetsList(int DeviceFamilyProfilerOptionID, int currentUserId);
        int UpdateDeviceFamilyProfilerOption(DeviceFamilyProfilerOption DeviceFamilyProfilerOption);
        int UpdateDeviceFamilyWithoutProfilerOption(DeviceFamilyProfilerOption DeviceFamilyProfilerOption);
        bool DeleteProfilerOption(int DeviceFamilyProfilerOptionID);
    }
}
