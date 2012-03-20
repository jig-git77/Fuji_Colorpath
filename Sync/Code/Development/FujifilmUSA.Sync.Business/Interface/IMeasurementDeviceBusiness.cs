using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
   public interface  IMeasurementDeviceBusiness : ITaskBase
    {
       int InsertMeasurementDevice(MeasurementDevice MeasurementDevice);
       int UpdateMeasurementDevice(MeasurementDevice MeasurementDevice);
       bool DeleteMeasurementDevice(int MeasurementDeviceID);
       List<MeasurementDevice> GetMeasurementDevices();
       MeasurementDevice GetMeasurementDevice(int iMeasurementDeviceID);
    }
}
