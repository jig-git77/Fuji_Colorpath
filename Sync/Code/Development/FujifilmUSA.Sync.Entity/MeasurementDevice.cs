using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementDevice
    {
        public short MeasurementDeviceId { get; set; }
        public int LicenseId { get; set; }
        public string SerialNumber { get; set; }
        public short InstrumentTypeId { get; set; }
        public bool? IsActive { get; set; }
        public List<MeasurementSetting> MeasurementSettings { get; set; }
        public InstrumentType InstrumentType { get; set; }
    }
}