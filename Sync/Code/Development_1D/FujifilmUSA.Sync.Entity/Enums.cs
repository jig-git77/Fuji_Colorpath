using System;
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

    public enum EProjectType
    {
        G7 = 1,
        ISO = 2,
        DeviceLinkProfile = 3,
        InkOptimization = 4
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
        [Description("EUVIncludedMode_UV")]//M0
        UV = 0,
        [Description("EUVIncludedMode_PlusUV")]//M1
        UVPlus = 1,
        [Description("EUVIncludedMode_UVCut")]//M2
        UVCut = 2
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
        Gray_75 = 30,

        //Newly added PatchTypes into CP.PatchType_Sync1D table
        Cyan_10 = 112,
        Cyan_20 = 113,
        Cyan_25 = 114,
        Cyan_30 = 115,
        Cyan_40 = 116,
        Cyan_50 = 117,
        Cyan_60 = 118,
        Cyan_70 = 119,
        Cyan_75 = 120,
        Cyan_80 = 121,
        Cyan_90 = 122,

        Magenta_10 = 123,
        Magenta_20 = 124,
        Magenta_25 = 125,
        Magenta_30 = 126,
        Magenta_40 = 127,
        Magenta_50 = 128,
        Magenta_60 = 129,
        Magenta_70 = 130,
        Magenta_75 = 131,
        Magenta_80 = 132,
        Magenta_90 = 133,

        Yellow_10 = 134,
        Yellow_20 = 135,
        Yellow_25 = 136,
        Yellow_30 = 137,
        Yellow_40 = 138,
        Yellow_50 = 139,
        Yellow_60 = 140,
        Yellow_70 = 141,
        Yellow_75 = 142,
        Yellow_80 = 143,
        Yellow_90 = 144,

        Black_10 = 145,
        Black_20 = 146,
        Black_25 = 147,
        Black_30 = 148,
        Black_40 = 149,
        Black_50 = 150,
        Black_60 = 151,
        Black_70 = 152,
        Black_75 = 153,
        Black_80 = 154,
        Black_90 = 155

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
        PrintBuyer = 8,
        CSRUser = 9
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
        ManageReferenceChart = 11,
        [Description("ERolePermission_ManageMetricTargetPreset")]//Manage MetricTargetPreset
        ManageMetricTargetPreset = 12,
        [Description("ERolePermission_ManagePrintStandardType")]//Manage PrintStandardType
        ManagePrintStandardType = 13,
        [Description("ERolePermission_ManagePatchTolerance")] //Manage Patch Tolerance
        ManagePatchTolerance = 14,
        [Description("ERolePermission_ManageSync1DTVILABDensity")] //Manage Sync1D TVILABDensity
        ManageSync1DTVILABDensity = 15,
        [Description("ERolePermission_ManageApplicationConfiguration")] //Manage Application Configuration
        ManageApplicationConfiguration = 16,
        [Description("ERolePermission_ManageActivityValidationRules")] //Manage Activity Validation Rules
        ManageActivityValidationRules = 17,
        [Description("ERolePermission_ManageDeviceProfileSettings")] //Manage Device Profiler Settings
        ManageDeviceProfileSettings = 18,
        [Description("ERolePermission_ManageSpotTarget")] //Manage Spot Target/Brand Color Optimization
        ManageSpotTarget = 19,
        [Description("ERolePermission_ManageCustomReports")] //Manage Custom Reports
        ManageCustomReports = 20,
        [Description("ERolePermission_ManageMeasurementLog")] //Manage Measurement Logs
        ManageMeasurementLogs = 21,
        [Description("ManageMToolVersion")] //Manage MTool Version
        ManageMToolVersion = 22
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
        i1Sis = 2,

        [Description("EInstrumentType_i1IO")]//i1-IO
        i1IO = 3,

        [Description("EInstrumentType_i1Pro2")]//i1-Pro2
        i1Pro2 = 4,

        [Description("EInstrumentType_i1IOPro2")]//i1-IO-Pro2
        i1IOPro2 = 5,

        [Description("EInstrumentType_LFP")]//LFP
        LFP = 6,

        [Description("EInstrumentType_eXact")]//eXact
        eXact = 7,

        [Description("EInstrumentType_i1Sis2")]//i1-Sis2
        i1Sis2 = 8,

        [Description("EInstrumentType_i1Pro3")]//i1-Pro3
        i1Pro3 = 9,

        [Description("EInstrumentType_i1IOPro3")]//i1-IO-Pro3
        i1IOPro3 = 10
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
        [Description("ELicenseType_InkOptimization_Source")]//Ink Optimization Source
        InkOptimizationSource = 4,
        [Description("ELicenseType_Sync4DCertification")]//Sync4D Certification
        Sync4DCertification = 5,
        [Description("ELicenseType_CustomSourceProfile")]//Custom Source Profile
        CustomSourceProfile = 6,
        [Description("ELicenseType_ReProfileCustomSourceProfile")]//ReProfile Custom Source
        ReProfileCustomSource = 7,
        [Description("ELicenseType_FullGamutCustomSourceProfile")]//Full Gamut Custom Source
        FullGamutCustomSource = 8,
        [Description("ELicenseType_MultiColorCustomSourceProfile")]//Multi Color Custom Source
        MultiColorCustomSource = 9,
        [Description("ELicenseType_SpotLibrary")]//Spot Color Library
        SpotLibrary = 10,
        [Description("ELicenseType_Validation")]//Color Validation
        Validation = 51,
        [Description("ELicenseType_IccProfiling")]//ICC Profiling
        IccProfiling = 52,
        [Description("ELicenseType_DLP_PLUS")]//DLP Plus
        DLPPlus = 11,
        [Description("ELicenseType_DLP_MULTI_COLOR")]//DLPMutliColor
        DLPMultiColor = 12,
        [Description("ELicenseType_Sync1D_MULTI_COLOR")]//Sync1DMultiColor
        Sync1DMultiColor = 13,
    }

    public enum UserType
    {
        WebSiteUser = 0,
        XmfColorOrganizerUser = 1,
        RevoriaColorOrganizerUser = 2
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
        //BranchSummaryReport = 1,
        //DeviceReport = 2,
        //ShiftVariabilityReport = 3,
        //DeviceVariabilityReport = 4,
        //ColorDetailReport = 5,
        //PatchDetailReport = 6,
        //ProductivityReport = 7,
        //ReportPerformanceSummary = 8,
        //ReportTargetMeasurement = 9,
        //ReportJobRunCertificationProcess = 10,
        //ReportJobRunCertificationNonProcess = 11,
        //ReportPressMakeReady = 12,
        ReportSingleMeasurement = 13,
        //CPSPrintStandardsReport = 14,
        //CPSPrintStandardsReportISO = 15,
        ReportSingleMeasurement_G7 = 16,
        ReportSingleMeasurement_ISO = 17,
        ReportSingleMeasurement_Default = 18,
        ReportScoringMeasurement_Default = 19,
        ReportSpotColorProject = 20
    }

    public enum ValidationRuleType
    {
        None = 0,
        NoitificationRule = 1,
        StrictRule = 2
    }

    public enum ValidationStatus
    {
        [Description("ValidationStatus_None")]
        None = 0,
        [Description("ValidationStatus_NotCertified")]
        NotCertified = 1,//Validation_Not_Certified | Open/Close
        [Description("ValidationStatus_Disabled")]
        Disabled = 2, //Validation_Not_Active  
        [Description("ValidationStatus_NoMeasurement")]
        NoMeasurement = 3, //Certified but no measurement found
        [Description("ValidationStatus_LastOK")]
        LastOK = 4, // Validation_Last_OK
        [Description("ValidationStatus_LastFailed")]
        LastFailed = 5 //Validation_Last_Failed

    }

    public enum PCSDataTypes
    {
        [Description("PCSDataTypes_Density")]
        Density = 0,
        [Description("PCSDataTypes_XYZ")]
        XYZ = 1,
        [Description("PCSDataTypes_Spectral")]
        Spectral = 2
    }

    public enum DensityTypes
    {
        [Description("DensityTypes_StatusT")]
        Status_T = 0,
        [Description("DensityTypes_StatusE")]
        Status_E = 1
    }

    public enum EInkOrder
    {
        [Description("EInkOrder_KCMY")]//K C M Y
        KCMY = 0,
        [Description("EInkOrder_CMYK")]//C M Y K
        CMYK = 1,
        [Description("EInkOrder_YMCK")]//Y M C K
        YMCK = 2,
        [Description("EInkOrder_CKMY")]//C K M Y
        CKMY = 3,
        [Description("EInkOrder_CKYM")]//C K Y M
        CKYM = 4,
        [Description("EInkOrder_CMKY")]//C M K Y
        CMKY = 5,
        [Description("EInkOrder_CYKM")]//C Y K M
        CYKM = 6,
        [Description("EInkOrder_CYMK")]//C Y M K
        CYMK = 7,
        [Description("EInkOrder_KCYM")]//K C Y M
        KCYM = 8,
        [Description("EInkOrder_KMCY")]//K M C Y
        KMCY = 9,
        [Description("EInkOrder_KMYC")]//K M Y C
        KMYC = 10,
        [Description("EInkOrder_KYCM")]//K Y C M
        KYCM = 11,
        [Description("EInkOrder_KYMC")]//K Y M C
        KYMC = 12,
        [Description("EInkOrder_MCKY")]//M C K Y
        MCKY = 13,
        [Description("EInkOrder_MCYK")]//M C Y K
        MCYK = 14,
        [Description("EInkOrder_MKCY")]//M K C Y
        MKCY = 15,
        [Description("EInkOrder_MKYC")]//M K Y C
        MKYC = 16,
        [Description("EInkOrder_MYCK")]//M Y C K
        MYCK = 17,
        [Description("EInkOrder_MYKC")]//M Y K C
        MYKC = 18,
        [Description("EInkOrder_YCKM")]//Y C K M
        YCKM = 19,
        [Description("EInkOrder_YCMK")]//Y C M K
        YCMK = 20,
        [Description("EInkOrder_YKCM")]//Y K C M
        YKCM = 21,
        [Description("EInkOrder_YKMC")]//Y K M C
        YKMC = 22,
        [Description("EInkOrder_YMKC")]//Y M K C
        YMKC = 23
    }

    public enum EPrintingTypes
    {
        [Description("EPrintingTypes_Offset")]
        Offset = 0,
        [Description("EPrintingTypes_Flexo")]
        Flexo = 1
    }

    public enum ESourceProfileCategory
    {
        [Description("ESourceProfileCategory_All")] //All
        All = 0,
        [Description("ESourceProfileCategory_NorthAmericanStandards")] //North American Standards
        NorthAmericanStandards = 1,
        [Description("ESourceProfileCategory_EuropeanStandards")] //European Standards
        EuropianStandards = 2,
        [Description("ESourceProfileCategory_JapanStandards")] //Japan Standards
        JapanStandards = 3,
        [Description("ESourceProfileCategory_CustomStandards")] //Japan Standards
        CustomSourceProfile = 4,
        [Description("ESourceProfileCategory_Other")] //Other
        Other = 11
    }

    public enum EMeasureTool
    {
        [Description("EMeasureTool_Both")] //Both
        BOTH = 0,
        [Description("ESourceProfileCategory_NPAPIPlugin")] //NPAPIPlugin
        NPAPIPlugin = 1,
        [Description("ESourceProfileCategory_SYNCMeasureApp")] //SYNCMeasureApp
        SYNCMeasureApp = 2

    }

    /// <summary>
    /// 
    /// </summary>
    public enum MultiColorChannelName
    {
        Red=1,
        Green = 2,
        Blue = 3,
        Orange = 4,
        Violet = 5
    }

}
