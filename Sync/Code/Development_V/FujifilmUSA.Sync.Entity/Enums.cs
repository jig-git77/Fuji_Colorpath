using System.ComponentModel;
using System;
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
        ValidationSetup = 10,
        Save = 11,
        Measure = 12,
        Analyze = 13,
        Download = 14,
        OptimizeRun = 15,
        Validation = 16
    }

    public enum EOptimizeRunStatus
    {
        Open = 1,
        Closed = 2,
        Certify = 3,
    }

    //public enum EOperation
    //{
    //    [Description("Project List")]
    //    None = 0,
    //    [Description("Alignment Curves")]
    //    AligmentCurves = 1,
    //    [Description("DLP Optimization")]
    //    DlpOptimization = 2,
    //    [Description("Ink Optimization")]
    //    InkOptimization = 3,
    //    [Description("Color Validation")]
    //    ColorValidation = 4
    //}

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

    public enum EUVIncludedMode
    {
        [Description("EUVIncludedMode_UVCut")]//UV Cut
        UVCut = 0,
        [Description("EUVIncludedMode_UV")]//UV Included
        UV = 1
    }

    public enum EObserverType
    {
        [Description("2°")]
        TwoDegree = 0,
        [Description("10°")]
        TenDegree = 1
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
    public enum ERole
    {
        SystemAdmin = 1,
        LicenseAdmin = 2,
        WebsiteAdmin = 3,
        RegionAdmin = 4,
        CompanyAdmin = 5,
        BranchAdmin = 6,
        SiteUser = 7,
        PrintBuyer = 8
    }

    public enum ERolePermission
    {
        [Description("ERolePermission_ManageLicense")]//Manage License
        ManageLicense = 1,
        [Description("ERolePermission_ManageRegion")]//Manage Region
        ManageRegion = 2,
        [Description("ERolePermission_ManageMeasurementDevice")]//Manage Measurement Device
        ManageMeasurementDevice = 3,
        [Description("ERolePermission_ManageCompany")]//Manage Company
        ManageCompany = 4,
        [Description("ERolePermission_ManageBranch")]//Manage Branch
        ManageBranch = 5,
        [Description("ERolePermission_ManageDevice")]//Manage Device
        ManageDevice = 6,
        [Description("ERolePermission_CreateDeleteProject")]//Create Delete Project
        CreateDeleteProject = 7,
        [Description("ERolePermission_ModifyProject")]//Modify Project
        ModifyProject = 8,
        [Description("ERolePermission_ManageUser")]//Manage User
        ManageUser = 9,
        [Description("ERolePermission_ManageSourceProfile")]//Manage SourceProfile
        ManageSourceProfile = 10,
        [Description("ERolePermission_ManageReferenceChart")]//Manage ReferenceChart
        ManageReferenceChart = 11
    }
    public enum ECurrentProjectStatus
    {
        New = 0,
        Edit = 1,
        Run = 2,
    }
    public enum EInstrumentType
    {
        None = 0,

        [Description("EInstrumentType_i1Pro")]//i1-Pro
        i1Pro = 1,

        [Description("EInstrumentType_i1Sis")]//i1-Sis
        i1Sis = 2
    }

    public enum EProfilerOption
    {
        ProfileType = 1,
        ProfileSize = 2,
        ProfileFormat = 3,
        Rendering = 4,
        PerceptualCompression2 = 5,
        PerceptualCompressionMergeFactor = 6,
        MeasurementCorrection = 7,
        BrightenerCorrection = 8,
        AdaptionType = 9,
        WhiteXYZCorrection = 10,
        GridA2B0 = 11,
        GridA2B1 = 12,
        GridA2B2 = 13,
        GridB2A0 = 14,
        GridB2A1 = 15,
        GridB2A2 = 16,
        GridGamut = 17,
        InkMax = 18,
        InkTotal = 19,
        BlackGeneration = 20,
        BlackStart = 21,
        BlackWidth = 22,
        DLType = 23,
        DLExceptions = 24,
        DLBypassBlack = 25,
        DLExceptionRange = 26,
        DLOptimizationLimit = 27,
        DLOptimizeExceptions = 28,
        DLOptimizeInkRange = 29,
        DLApplyInkSave = 30,
        DLReplaceCMY = 31,
        DLAddBlack = 32,
        DLAddCMY = 33,
        Flags = 34,
        Attributes0 = 35,
        Attributes1 = 36,
        SaveProfilerInfo = 37,
        SaveProfilerData = 38,
        Version = 39
    }

    [Flags]
    public enum EProfilerException
    {
        PureBlack = 1,
        BlackOverprint = 2,
        LinearizeGray = 4,
        PureGray = 8,
        PureCMY = 16,
        PurePrimaries = 32,
        PureRGB = 64,
        PureWhite = 128,
        Duplex = 256,
        Triplex = 512,
        FullBlack = 1024,
        PurifyColor = 2048,
        PureSecondaries = 4096,
        LimitPrimaries = 8192,
        PreserveZeroBlack = 16384,
        ClipToEdge = 65536,
        TriplexBelowTAC = 32768,
        FilterException = 524288,
        PureCyan = 1048576,
        PureMagenta = 2097152,
        PureYellow = 4194304,
        PureMY = 8388608,
        PureCY = 16777216,
        PureCM = 33554432,
        MaxCMY = 67108864,
        MaxRGB = 134217728
    }
    public enum EProfileRendering
    {
        [Description("EProfileRendering_icPerceptual")]//Perceptual
        icPerceptual = 0,

        [Description("EProfileRendering_icRelativeColorimetric")]//Relative
        icRelativeColorimetric = 1,

        [Description("EProfileRendering_icSaturation")]//Saturation
        icSaturation = 2,

        [Description("EProfileRendering_icAbsoluteColorimetric")]//Absolute
        icAbsoluteColorimetric = 3,

        [Description("EProfileRendering_eCLBlackCompensationRI")]//BlackCompensation
        eCLBlackCompensationRI = 10,

        [Description("EProfileRendering_eCLAbsolutePerceptualRI")]//AbsolutePerceptual
        eCLAbsolutePerceptualRI = 11,

        [Description("EProfileRendering_eCLAbsLightenerCompensationRI")]//AbsLightenerCompensation
        eCLAbsLightenerCompensationRI = 12,

        [Description("EProfileRendering_eCLGamutCompressionRI")]//GamutCompression
        eCLGamutCompressionRI = 13,

        [Description("EProfileRendering_eCLPerceptualCompressionRI")]//PerceptualCompression
        eCLPerceptualCompressionRI = 14,

        [Description("EProfileRendering_eCLRelativeCompressionRI")]//RelativeCompression
        eCLRelativeCompressionRI = 15,

        [Description("EProfileRendering_eCLAbsoluteCompressionRI")]//AbsoluteCompression
        eCLAbsoluteCompressionRI = 16,

        [Description("EProfileRendering_eCLRelativeRI")]//pRelative
        eCLRelativeRI = 17,

        [Description("EProfileRendering_eCLAbsoluteRI")]//pAbsolute
        eCLAbsoluteRI = 18,

        [Description("EProfileRendering_eCLRelativeLightness")]//RelativeLightness
        eCLRelativeLightness = 19,

        [Description("EProfileRendering_eCLMinimumBlackCompressionRI")]//MinBlackCompression
        eCLMinimumBlackCompressionRI = 26,

        [Description("EProfileRendering_eCLMinimumWhiteCompressionRI")]//MinWhiteCompression
        eCLMinimumWhiteCompressionRI = 27,

        [Description("EProfileRendering_eCLMinimumCompressionRI")]//MinCompression
        eCLMinimumCompressionRI = 28
    }
    public enum UserAuthenticationUpdateType
    {
        FirstTimeSet = 0,
        Reset = 1,
        ForceReSet = 2
    }

    public enum ELicenseType
    {
        [Description("ELicenseType_None")]//INVALID
        None = 0,
        [Description("ELicenseType_Sync1D")]//Alignment Curves
        Sync1D = 1,
        [Description("ELicenseType_DLP")]//DLP Optimization
        DLP = 2,
        [Description("ELicenseType_InkOptimization")]//Ink Optimization
        InkOptimization = 3,
        [Description("ELicenseType_Validation")]//Color Validation
        Validation = 4,
        [Description("ELicenseType_IccProfiling")]//ICC Profiling
        IccProfiling = 5
    }

    public enum UserType
    {
        WebSiteUser = 0,
        XmfColorOrganizerUser = 1
    }

    public enum Template
    {
        Base = 1,
        UserCreatedEmail = 2,
        PasswordResetEmail = 3,
    }

    public enum EReferenceChartDisType
    {
        [Description("EReferenceChartDisType_ProfileChart")]//Profile Chart
        ProfileChart = 1,
        [Description("EReferenceChartDisType_OptimizationChart")]//Optimization Chart
        OptimizationChart = 2,
        [Description("EReferenceChartDisType_ValidationChart")]//Validation Chart
        ValidationChart = 3
    }

    public enum LogActivityType
    {
        [Description("LogActivityType_None")]
        None = 0,
        [Description("LogActivityType_LoggedIn")]
        LoggedIn = 1,
        [Description("LogActivityType_LoggedOut")]
        LoggedOut = 2,
        [Description("LogActivityType_CreateProject")]
        CreateProject = 3,
        [Description("LogActivityType_OptimizeDlp")]
        OptimizeDlp = 4,
        [Description("LogActivityType_CreateDlp")]
        CreateDlp = 5,
        [Description("LogActivityType_ChangeProfile")]
        ChangeProfile = 6,
        [Description("LogActivityType_CreateDlpError")]
        CreateDlpError = 7,
        [Description("LogActivityType_OptimizeDlpError")]
        OptimizeDlpError = 8,
    }

    public enum LogActivitySystemType
    {
        None = 0,
        Web = 1,
        Service = 2,
        ColorPath = 3
    }

    public enum enumReportType
    {
        BranchSummaryReport = 1,
        DeviceReport = 2,
        ShiftVariabilityReport = 3,
        DeviceVariabilityReport = 4,
        ColorDetailReport = 5,
        PatchDetailReport = 6,
        ProductivityReport = 7,
        ReportPerformanceSummary = 8,
        ReportTargetMeasurement = 9,
        ReportJobRunCertificationProcess = 10,
        ReportJobRunCertificationNonProcess = 11,
        ReportPressMakeReady = 12,
        ReportSingleMeasurement = 13,
        CPSPrintStandardsReport = 14,
        CPSPrintStandardsReportISO = 15,
        ReportSingleMeasurement_G7_Vali = 16
    }

    public enum ValidationRuleType
    {
        None = 0,
        NoitificationRule = 1,
        StrictRule = 2
    }
}
