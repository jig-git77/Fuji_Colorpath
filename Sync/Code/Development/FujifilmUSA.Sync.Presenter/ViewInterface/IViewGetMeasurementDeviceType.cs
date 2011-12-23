using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewGetMeasurementDeviceType : IViewBaseSyncDevice
    {
        Dictionary<string, string> MeasurementDevices { get; set; }
    }
}
