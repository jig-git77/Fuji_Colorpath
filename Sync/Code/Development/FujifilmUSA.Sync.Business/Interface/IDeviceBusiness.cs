using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IDeviceBusiness : ITaskBase
    {
        List<Device> DeviceListById(int companyId, int branchId);
    }
}
