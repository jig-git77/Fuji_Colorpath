using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewOperationSelection : IViewBase
    {
        int DeviceId { get; set; }
        Device Device { get; set; }
    }
}
