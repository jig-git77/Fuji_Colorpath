using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewConfirmSave : IViewBase
    {
        int ProjectId { get; set; }
        int OptimizeRunId { get; set; }
        int MasterRunId { get; set; }
        List<int> MeasurementIds { get; set; }
        Project Project { get; set; }
        string ProfileDescription { get; set; }
        string LastRunStatus { get; set; }
    }
}
