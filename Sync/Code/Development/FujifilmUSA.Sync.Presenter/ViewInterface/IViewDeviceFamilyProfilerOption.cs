using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewDeviceFamilyProfilerOption : IViewError
    {
        short DeviceFamilyId { get; set; }
        List<DeviceFamilyProfilerOption> ProfileOption { get; set; }
    }
}
