using System.ComponentModel;
namespace FujifilmUSA.Sync.Entity
{
    public enum ELeftMenuItemLevel
    {
        Home = 1,
        Device = 2,
        Operation = 3,
        Project = 4,
        ProjectSetup = 5,
        Description = 6,
        TargetColorSpace = 7,
        ProfilerOption = 8,
        MeasurementSetup = 9,
        Save = 10,
        Measure = 11,
        Analyze = 12,
        Download = 13,
        OptimizeRun = 14
    }

    public enum EOptimizeRunStatus
    {
        Open = 1,
        Closed = 2,
        Certify = 3,
        NotUsed = 4
    }

    public enum EOperation
    {
        None = 0,
        ColorValidation = 1,
        DlpOptimization = 2,
        AligmentCurves = 3
    }

    public enum EProfileType
    {
        SourceProfile = 1,
        DestinationProfile = 2,
        DeviceLinkProfile = 3,
        OptimizeDlp = 4
    }

    public enum EDensityType
    {
        DIN = 1,
        DINNB = 2,
        ANSIA = 3,
        ANSIE = 4,
        ANSII = 5,
        ANSIT = 6
    }

    public enum EIlluminationMode
    {
        UV = 1,           //UV Included
        UVCut = 2         //No UV i.e. UVCut
    }

    public enum CPPatchType
    {
        Cyan = 1,
        Magenta = 2,
        Yellow = 3,
        Black = 4,
        Overprint_Red = 5,
        Overprint_Green = 6,
        Overprint_Blue = 7,
        Paper = 8,
        Triplet_Solid_Gray = 9,
        Gray_C_M_Y = 10,
        Other = 11,
        Cyan_Tint = 12,
        Magenta_Tint = 13,
        Yellow_Tint = 14,
        Black_Tint = 15,
        Cyan_Quarter_Tone = 16,
        Cyan_Half_Tone = 17,
        Cyan_Three_Quarter_Tone = 18,
        Magenta_Quarter_Tone = 19,
        Magenta_Half_Tone = 20,
        Magenta_Three_Quarter_Tone = 21,
        Yellow_Quarter_Tone = 22,
        Yellow_Half_Tone = 23,
        Yellow_Three_Quarter_Tone = 24,
        Black_Quarter_Tone = 25,
        Black_Half_Tone = 26,
        Black_Three_Quarter_Tone = 27,
        Gray_25 = 28,
        Gray_50 = 29,
        Gray_75 = 30
    }
    public enum ERolePermission
    {
        [Description("Manage License")]
        ManageLicense = 1,

        [Description("Manage Region")]
        ManageRegion =2,

        [Description("Manage Measurement Device")]
        ManageMeasurementDevice =3,

        [Description("Manage Company")]
        ManageCompany =4,

        [Description("Manage Branch")]
        ManageBranch =5,

        [Description("Manage Device")]
        ManageDevice =6,

        [Description("Create Delete Project")]
        CreateDeleteProject =7,

        [Description("Modify Project")]
        ModifyProject = 8,

        [Description("Manage User")]
        ManageUser = 9,

        [Description("Manage SourceProfile")]
        ManageSourceProfile = 10
          

    }
}
