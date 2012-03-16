using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewMeasurementSetting
    {
        List<Illuminant> Illumin { get; set; }

        List<ObserverType> Observer { get; set; }

        int deviceId { get; set; }
        Dictionary<int, string> Measurement { get; set; }

        int MeasurementDeviceID { get; set; }
        List<ReferenceChartType> ReferenceChartType { get; set; }
    }
}
