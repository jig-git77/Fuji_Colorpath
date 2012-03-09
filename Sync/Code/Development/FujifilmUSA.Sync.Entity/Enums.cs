namespace FujifilmUSA.Sync.Entity
{
    public enum LeftMenuItemLevel
    {
        Home = 1,
        Device = 2,
        Operation=3,
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

    public enum OptimizeRunStatus
    {
        Open = 1,
        Closed = 2,
        Certify = 3,
        NotUsed = 4
    }

    public enum Operation
    {
        None = 0,
        ColorValidation = 1,
        DlpOptimization = 2,
        AligmentCurves = 3
    }
}
