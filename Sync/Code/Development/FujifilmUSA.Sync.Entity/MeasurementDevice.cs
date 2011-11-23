using System;

namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementDevice
    {
        public int MeasurementDeviceId { get; set; }
        public int CPDongleId { get; set; }
        public string SerialNumber { get; set; }
        public Boolean IsActive { get; set; }
        public MeasurementDeviceType MeasurementDeviceTypeDetail { get; set; }
    }
}
