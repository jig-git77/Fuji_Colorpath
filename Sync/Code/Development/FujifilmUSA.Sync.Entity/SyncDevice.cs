using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class SyncDevice
    {
        public int SyncDeviceID { get; set; }
        public int CPDongleID { get; set; }
        public string CPSerialNUM { get; set; }
        public string DeviceName { get; set; }
        public string DeviceManufacturer { get; set; }
        public int NumOfInkUnits { get; set; }
        public int DeviceTypeID { get; set; }

    }
}
