using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IDeviceBusiness : ITaskBase
    {
        List<Device> DeviceListById(int companyId, int branchId);
        List<Device> DeviceList();
        int InsertDevice(Device device);
        int UpdateDevice(Device device);
        bool DeleteDevice(int deviceId);
        int InsertDeviceFamily(DeviceFamily deviceFamily);
    }
}
