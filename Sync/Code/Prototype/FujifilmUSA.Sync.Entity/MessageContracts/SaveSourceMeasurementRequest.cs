using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class SaveSourceMeasurementRequest
    {
        public SourceProfile SourceProfile { get; set; }
        public MeasurementLink MeasurementLink { get; set; }
    }
}
