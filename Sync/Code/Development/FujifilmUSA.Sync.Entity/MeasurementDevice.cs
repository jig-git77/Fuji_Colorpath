using System.Collections.Generic;
using System;
namespace FujifilmUSA.Sync.Entity
{
    [Serializable]
    public class MeasurementDevice
    {
        public short MeasurementDeviceId { get; set; }
        public int LicenseId { get; set; }
        public string SerialNumber { get; set; }
        public short InstrumentTypeId { get; set; }
        public bool? IsActive { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set;}
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int LicenseTypeId { get; set; }
        public string LicenseType { get; set; }
        public MeasurementSetting MeasurementSettings { get; set; }
        public InstrumentType InstrumentType { get; set; }
    }
}