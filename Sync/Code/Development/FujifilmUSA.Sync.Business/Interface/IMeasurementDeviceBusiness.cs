using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMeasurementDeviceBusiness : ITaskBase
    {
        int InsertSyncMeasurementDevice(MeasurementDevice measurementDevice);
        int UpdateSyncMeasurementDevice(MeasurementDevice measurementDevice);
        MeasurementDevice GetSyncMeasurementDeviceByMeasurementDeviceId(int measurementDeviceId);
        List<MeasurementDevice> GetSyncMeasurementDeviceList();
        int DeleteSyncMeasurementDevice(int syncMeasurementDeviceId);
    }
}
