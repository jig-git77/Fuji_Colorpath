using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class SaveDestinationMeasurementRequest
    {
        public DestinationProfile DestinationProfile { get; set; }
        public MeasurementLink MeasurementLink { get; set; }
    }
}
