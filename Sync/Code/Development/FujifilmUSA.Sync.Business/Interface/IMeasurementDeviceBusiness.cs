using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMeasurementDeviceBusiness : ITaskBase
    {
        Dictionary<int, string> GetMeasurementDeviceListBySyncDeviceId(int syncDeviceId);
    }
}
