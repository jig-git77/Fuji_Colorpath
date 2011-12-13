using System;
namespace FujifilmUSA.Sync.Entity
{
    public class SyncRun : ProfileData
    {
        public int SyncRunId { get; set; }
        public string SyncRunName { get; set; }
        public int ProjectId { get; set; }
        public short Status { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int MasterSyncRunId { get; set; }
        public bool HasMeasurements { get; set; }
        public bool HasProfile { get; set; }
    }
}
