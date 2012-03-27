using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewLogin : IViewError
    {
        string UserName { get; set; }
        string Password { get; set; }
        User CurrentUser { get; set; }
    }
}
