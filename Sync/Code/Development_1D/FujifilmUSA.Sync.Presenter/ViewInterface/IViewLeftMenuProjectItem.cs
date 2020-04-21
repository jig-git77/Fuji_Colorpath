using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewLeftMenuProjectItem : IViewBase
    {
        int ProjectId { get; set; }
        Project Project { get; set; }
        int OptimizeRunID { get; set; }
        int MasterRunID { get; set; }
        EOptimizeRunStatus RunStatus { get; set; }
        int SpotProjectId { get; set; }
    }
}
