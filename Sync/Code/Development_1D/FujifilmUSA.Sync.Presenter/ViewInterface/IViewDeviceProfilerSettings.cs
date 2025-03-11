using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewDeviceProfilerSettings : IViewBase
    {
        ProfilerOptionPreset ProfilerOptionPreset { get; set; }
        Dictionary<int, string> DeviceFamilyList { get; set; }
        Dictionary<int, string> ProjectTypeList { get; set; }
        DeviceFamilyProfilerOption DeviceFamilyProfilerOption { get; set; }
        int DeviceFamilyProfilerOptionID { get; set; }
        List<ProfilerOptionPresetsResponse> ProfilerOptionPresetsResponse { get; set; }
        ProfilerOptionPresetsResponse PresetsResponse { get; set; }
    }
}
