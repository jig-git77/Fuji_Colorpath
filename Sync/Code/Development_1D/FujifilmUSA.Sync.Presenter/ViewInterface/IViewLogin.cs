using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewLogin : IViewBase
    {
        string UserName { get; set; }
        string Password { get; set; }
        User CurrentUser { get; set; }

        int UserId { get; set; }
        string OSName { get; set; }
        string BrowserName { get; set; }
        string EntryDate { get; set; }

        string EncyptedTocken { get; set; }

        UserReferenceChart UserReferenceChart { get; set; }
    }
}
