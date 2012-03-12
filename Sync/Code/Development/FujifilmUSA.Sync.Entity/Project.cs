using System;
using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int DeviceId { get; set; }
        public short SourceProfileId { get; set; }
        public byte[] DestinationProfile { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CurrDlpId { get; set; }
        public bool IsUsed { get; set; }
        public List<MeasurementSetting> MeasurementSettings { get; set; }
        public List<OptimizeRun> OptimizeRuns { get; set; }
        public List<PrintCondition> PrintConditions { get; set; }
        public List<ProjectOption> ProjectOptions { get; set; }
        //public SourceProfile SourceProfile { get; set; }
        //public Device Device { get; set; }
        public bool IsNewlyCreated { set; get; }
        public OptimizeRunStatus Status { get; set; }
        public int ReferenceChartTypeId { get; set; }
    }
}