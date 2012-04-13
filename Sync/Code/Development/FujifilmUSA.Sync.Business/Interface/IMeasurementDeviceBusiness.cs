using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
   public interface  IMeasurementDeviceBusiness : ITaskBase
    {
       int InsertMeasurementDevice(MeasurementDevice measurementDevice);
       int UpdateMeasurementDevice(MeasurementDevice measurementDevice);
       bool DeleteMeasurementDevice(int measurementDeviceId);
       List<MeasurementDevice> GetMeasurementDevice(int measurementDeviceId,int currentUserId);
    }
}
