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
        Save = 10,
        Measure = 11,
        Analyze = 12,
        Download = 13,
        OptimizeRun = 14
    }

    public enum ERefChartDisplayType
    {
        Open = 1,
        Closed = 2,
        //Certify = 3,
        //NotUsed = 4
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
        [Description("UV Cut")]
        UVCut = 0,
        [Description("UV Included")]
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
        SystemAdmin = 0,
        LicenseAdmin = 1,
        WebsiteAdmin = 2,
        RegionAdmin = 3,
        CompanyAdmin = 4,
        BranchAdmin = 5,
        SiteUser = 6,
        PrintBuyer = 7
    }

    public enum ERolePermission
    {
        [Description("Manage License")]
        ManageLicense = 1,
        [Description("Manage Region")]
        ManageRegion = 2,
        [Description("Manage Measurement Device")]
        ManageMeasurementDevice = 3,
        [Description("Manage Company")]
        ManageCompany = 4,
        [Description("Manage Branch")]
        ManageBranch = 5,
        [Description("Manage Device")]
        ManageDevice = 6,
        [Description("Create Delete Project")]
        CreateDeleteProject = 7,
        [Description("Modify Project")]
        ModifyProject = 8,
        [Description("Manage User")]
        ManageUser = 9,
        [Description("Manage SourceProfile")]
        ManageSourceProfile = 10,
        [Description("Manage ReferenceChart")]
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

        [Description("i1-Pro")]
        i1Pro = 1,

        [Description("i1-Sis")]
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
        [Description("Perceptual")]
        icPerceptual = 0,

        [Description("Relative")]
        icRelativeColorimetric = 1,

        [Description("Saturation")]
        icSaturation = 2,

        [Description("Absolute")]
        icAbsoluteColorimetric = 3,

        [Description("BlackCompensation")]
        eCLBlackCompensationRI = 10,

        [Description("AbsolutePerceptual")]
        eCLAbsolutePerceptualRI = 11,

        [Description("AbsLightenerCompensation")]
        eCLAbsLightenerCompensationRI = 12,

        [Description("GamutCompression")]
        eCLGamutCompressionRI = 13,

        [Description("PerceptualCompression")]
        eCLPerceptualCompressionRI = 14,

        [Description("RelativeCompression")]
        eCLRelativeCompressionRI = 15,

        [Description("AbsoluteCompression")]
        eCLAbsoluteCompressionRI = 16,

        [Description("pRelative")]
        eCLRelativeRI = 17,

        [Description("pAbsolute")]
        eCLAbsoluteRI = 18,

        [Description("RelativeLightness")]
        eCLRelativeLightness = 19,

        [Description("MinBlackCompression")]
        eCLMinimumBlackCompressionRI = 26,

        [Description("MinWhiteCompression")]
        eCLMinimumWhiteCompressionRI = 27,

        [Description("MinCompression")]
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
        [Description("INVALID")]
        None = 0,
        [Description("Alignment Curves")]
        Sync1D = 1,
        [Description("DLP Optimization")]
        DLP = 2,
        [Description("Ink Optimization")]
        InkOptimization = 3,
        [Description("Color Validation")]
        Verification = 4,
        [Description("ICC Profiling")]
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
        [Description("Profile Chart")]
        ProfileChart = 1,
        [Description("Optimization Chart")]
        OptimizationChart = 2
    }
}