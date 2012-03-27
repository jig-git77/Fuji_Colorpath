using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewDeviceManagement : IViewError
    {
        Device Device { get; set; }
        List<Device> DeviceList { get; set; }
    }
}
