using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementLog
    {
        public int MeasurementLogID { get; set; }
        public int UserID { get; set; }
        public int InstrumentTypeID { get; set; }
        public int MeasurementDeviceID { get; set; }
        public int ProjectID { get; set; }
        public int MeasurementID { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDescription { get; set; }
        public DateTime EntryDate { get; set; }
        public string UserName { get; set; }
        public string InstrumentType { get; set; }
        public string SerialNumber { get; set; }

    }
}
