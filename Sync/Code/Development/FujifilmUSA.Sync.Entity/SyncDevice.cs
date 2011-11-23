using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class SyncDevice
    {
        public int SyncDeviceId { get; set; }
        public int CpDongleId { get; set; }
        public string CpSerialNum { get; set; }
        public string DeviceName { get; set; }
        public string DeviceManufacturer { get; set; }
        public int NumOfInkUnits { get; set; }
        public int DeviceTypeId { get; set; }

    }
}
