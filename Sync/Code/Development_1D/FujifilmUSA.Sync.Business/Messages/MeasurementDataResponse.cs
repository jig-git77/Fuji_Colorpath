using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Messages
{
    public class MeasurementDataResponse
    {
        public List<Double> PcsData { get; set; }
        public List<Double> DcsData { get; set; }    
    }
}
