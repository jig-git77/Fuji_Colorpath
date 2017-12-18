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
        public EOptimizeRunStatus Status { get; set; }        
        public int TotalRuns { get; set; }
        public bool HasDLP { get; set; }
        public KeyValuePair<short, string> Measurements { get; set; }
        public bool EnabledValidation { get; set; }
                
        public ValidationStatus ValidationStatus { get; set; }
        public ELicenseType LicenseType { get; set; }

        public bool IsWeightedTolerances { get; set; }
        public List<CPSync1D.PrintStandardTVI> PrintStandardTVI { get; set; }
        public int MetricTargetPresetId { get; set; }
        public short ProjectTypeId { get; set; }
        public CPSync1D.PrintStandardType PrintStandardType { get; set; }

        public List<CPSync1D.ProjectTargetLAB> ProjectTargetLAB { get; set; }
        public List<CPSync1D.ProjectTargetDensity> ProjectTargetDensity { get; set; }

        public DensityTypes DensityType { get; set; }
        public PCSDataTypes PCSDataTypes { get; set; }
        public int ReTargetProjectID { get; set; }
        public short CurveTypeID { get; set; }
        public bool Is1DMeasurementSmoothing { get; set; }
        
       // public List<CPSpot.SpotProjectLibrary> SpotProjectLibraryList { get; set; }
        public CPSpot.SpotProjectLibrary SpotTargetProject { get; set; }
        public int SpotTargetMasterProjectID { get; set; }
        public bool HasSpotProject { get; set; }

        public bool EnabledPaperScaling { get; set; }
        public decimal NewPaper_White_L { get; set; }
        public decimal NewPaper_White_a { get; set; }
        public decimal NewPaper_White_b { get; set; }
        public decimal NewPaper_Black_L { get; set; }
        public decimal NewPaper_Black_a { get; set; }
        public decimal NewPaper_Black_b { get; set; }
    }
}
