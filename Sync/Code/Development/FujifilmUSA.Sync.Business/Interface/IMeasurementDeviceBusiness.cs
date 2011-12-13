using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMeasurementDeviceBusiness : ITaskBase
    {
        Dictionary<string, string> GetMeasurementDeviceListBySyncDeviceId(int syncDeviceId);
        int InsertSyncMeasurementDevice(MeasurementDevice measurementDevice);
        int UpdateSyncMeasurementDevice(MeasurementDevice measurementDevice);
        MeasurementDevice GetSyncMeasurementDeviceByMeasurementDeviceId(int measurementDeviceId);
        List<MeasurementDevice> GetSyncMeasurementDeviceList();
        int DeleteSyncMeasurementDevice(int syncMeasurementDeviceId);
        Dictionary<int, string> GetDongleList(int branchId);
    }
}
