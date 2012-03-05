using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddDeviceLinkProfile : IViewError
    {
        DeviceLinkProfile DeviceLinkProfile { get; set; }
        Dictionary<int, string> GetProfileOption { get; set; }
    }
}
