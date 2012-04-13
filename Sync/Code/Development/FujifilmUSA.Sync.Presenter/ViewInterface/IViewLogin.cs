using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewLogin : IViewBase
    {
        string UserName { get; set; }
        string Password { get; set; }
        User CurrentUser { get; set; }
    }
}
