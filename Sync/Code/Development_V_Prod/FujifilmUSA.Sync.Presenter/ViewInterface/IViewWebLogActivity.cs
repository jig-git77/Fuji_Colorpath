using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewWebLogActivity : IViewBase
    {
        LogActivityType ActivityType { get; set; }
        object[] Paramaters { get; set; }
        string IpAddress { get; set; }
    }
}
