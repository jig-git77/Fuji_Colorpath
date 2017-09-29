using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewOpenPrintCondition : IViewBase
    {
        int ProjectId { get; set; }
        Project Project { get; set; }
        int SpotProjectId { get; set; }
    }
}
