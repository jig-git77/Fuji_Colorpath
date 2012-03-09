namespace FujifilmUSA.Sync.Entity
{
    public enum ENavigation
    {
        Home = 1,
        Device = 2,
        OperationOption = 3,
        PrintConditionSettings = 4,
        PrintConditionEdit = 5
    }

    public enum ENavigationPrintRunOperation
    {
        Measure = 1,
        Analyze = 2,
        Download = 3
    }

    public enum ENavigationPrintConditionSetting
    {
        Description = 1,
        TargetColorSpace = 2,
        ProfilerOption = 3,
        MeasurementSetup = 4,
        Save = 5,
        Measure = 6,
        Analyze = 7,
        Download = 8
    }

    public enum LeftMenuItemLevel
    {
        Home = 1,
        Device = 2,
        Project = 3,
        ProjectSetup = 4,
        Description = 5,
        TargetColorSpace = 6,
        ProfilerOption = 7,
        MeasurementSetup = 8,
        Save = 9,
        Measure = 10,
        Analyze = 11,
        Download = 12,
        SyncRun = 13
    }

    public enum OptimizeRunStatus
    {
        Open = 1,
        Closed = 2,
        Certify = 3,
        NotUsed = 4
    }
}
