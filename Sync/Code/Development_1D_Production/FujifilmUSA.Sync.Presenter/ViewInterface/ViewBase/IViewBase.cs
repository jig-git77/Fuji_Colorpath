using FujifilmUSA.Sync.Entity;
namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewBase
    {
        int CurrentUserId { get; set; }
        void ErrorOccured(object sender, ErrorArgs e);
    }
}
