using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SyncDeviceID { get; set; }
        public int ΔEType { get; set; }
        public int DLPID { get; set; }
        public Boolean IsUsed { get; set; }
        public Boolean UseDLP { get; set; }
        public int ReTargetID { get; set; }
        public int ProjectTypeID { get; set; }
        public int OutputTypeID { get; set; }
        public Enums.CPSyncRunStatus ProjectStatus { get; set; }       
        
    }
}
