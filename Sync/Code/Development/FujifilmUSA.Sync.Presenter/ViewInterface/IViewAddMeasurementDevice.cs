using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddMeasurementDevice
    {
      MeasurementDevice MeasurementDevice { get; set; }
      Dictionary<int, string> MeasurementDevices { get; set; }
    }
}
