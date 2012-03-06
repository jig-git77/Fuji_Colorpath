using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class InstrumentType
    {
        public byte InstrumentTypeId { get; set; }
        public string InstrumentType1 { get; set; }
        public bool IsUsed { get; set; }
        public List<MeasurementDevice> MeasurementDevices { get; set; }
    }
}