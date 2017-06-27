using System;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Entity.CPSpot
{
    public partial class SpotProjectLibrary
    {
        public int SpotProjectId { get; set; }
        public int ProjectId { get; set; }
      
   
        public string ProjectName { get; set; }
        public DateTime CreateDate { get; set; }
        public int CurrDlpId { get; set; }
    
        public bool IsUsed { get; set; }
  
        public List<OptimizeRun> OptimizeRuns { get; set; }
       
        public CPSpot.SpotTarget SpotTarget { get; set; }
     
        public ECurrentProjectStatus CurrentDisplayStatus { set; get; }
        public EOptimizeRunStatus Status { get; set; }
        public int TotalRuns { get; set; }
      
        public KeyValuePair<short, string> Measurements { get; set; }
     

    }
}
