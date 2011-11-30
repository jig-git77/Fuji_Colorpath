using System;

namespace FujifilmUSA.Sync.Entity
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SyncDeviceId { get; set; }
        public int ΔEType { get; set; }
        public int DeviceLinkProfileId { get; set; }
        public Boolean IsUsed { get; set; }
        public Boolean UseDLP { get; set; }
        public int ReTargetId { get; set; }
        public int ProjectTypeId { get; set; }
        public int OutputTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public int SyncRunId { get; set; }
        public Enums.CPSyncRunStatus ProjectStatus { get; set; }       

        
    }
}
