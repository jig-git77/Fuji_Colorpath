using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewOpenPrintCondition
    {
        int ProjectId { get; set; }
        Project Project { get; set; }
    }
}
