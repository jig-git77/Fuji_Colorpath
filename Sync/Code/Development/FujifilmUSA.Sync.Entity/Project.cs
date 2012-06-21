using System;
using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public partial class Project
    {
        public int ProjectId { get; set; }
        public int DeviceId { get; set; }
        public byte[] DestinationProfile { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreateDate { get; set; }
        public int CurrDlpId { get; set; }
        public string CurrentDlpName { get; set; }
        public string ProposedDlpName { get; set; }
        public bool IsUsed { get; set; }
        public MeasurementSetting MeasurementSetting { get; set; }
        public List<OptimizeRun> OptimizeRuns { get; set; }
        public PrintCondition PrintCondition { get; set; }
        public List<ProjectOption> ProjectOption { get; set; }
        public SourceProfile SourceProfile { get; set; }
        public int DeviceFamilyProfilerOptionId { get; set; }
        public ECurrentProjectStatus CurrentDisplayStatus { set; get; }
        public ERefChartDisplayType Status { get; set; }
        public int LicenseTypeID { get; set; }
        public int TotalRuns { get; set; }
        public bool HasDLP { get; set; }
    }
}