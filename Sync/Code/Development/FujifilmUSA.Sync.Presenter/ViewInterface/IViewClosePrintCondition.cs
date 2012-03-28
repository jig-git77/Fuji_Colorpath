using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewClosePrintCondition : IViewError
    {
        int ProjectId { get; set; }
        byte[] CurrentDlp { get; set; }
        Project Project { get; set; }
    }
}
