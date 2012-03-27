using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewSaveWindow : IViewError
    {
        Project Project { get; set; }
    }
}
