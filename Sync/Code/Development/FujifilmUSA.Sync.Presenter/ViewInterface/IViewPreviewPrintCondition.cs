using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewPreviewPrintCondition
    {
        int DeviceFamilyProfilerOptionID { get; set; }
        short IlluminantID { get; set; }
        short ObserverTypeID { get; set; }
        short ReferenceChartTypeId { get; set; }
        short? MeasurementDeviceId { get; set; }
        Dictionary<string, string> Data { get; set; }
    }
}
