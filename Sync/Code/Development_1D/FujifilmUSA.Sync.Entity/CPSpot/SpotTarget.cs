using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity.CPSpot
{
    //[Serializable]
    public class SpotTarget
    {
        public short SpotTargetID { get; set; }
        public string SpotTargetName { get; set; }
        public Company Company { get; set; }
        public int MeasurementMode { get; set; }
        public string OwnerName { get; set; }
       // public List<CPSync1D.ProjectTargetLAB> PrintStandardLAB { get; set; }
    }
}
