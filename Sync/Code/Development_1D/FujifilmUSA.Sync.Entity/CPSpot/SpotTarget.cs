using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity.CPSpot
{
    [Serializable]
    public class SpotTarget
    {
        public int SpotTargetID { get; set; }
        public string SpotTargetName { get; set; }
        public Company Company { get; set; }
        public int MeasurementMode { get; set; }
        public string Description { get; set; }
        public bool IsUsed { get; set; }
        public SpotProjectChartSetting spotTargetSetting {get;set;}
        public List<SpotTargetLAB> spotTargetLab { get; set; }
        public int ReTargetSpotProjectID { get; set; }
    }
}
