using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewClosePrintCondition : IViewBase
    {
        int ProjectId { get; set; }
        byte[] CurrentDlp { get; set; }
        Project Project { get; set; }
        string ProfileDescription { get; set; }
    }
}
