using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMeasurementDeviceBusiness : ITaskBase
    {
        Dictionary<string, string> GetMeasurementDeviceListBySyncDeviceId(int syncDeviceId);
    }
}
