using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewConfirmSave
    {
        string ProjectName { get; set; }
        Project Project { get; set; }
    }
}
