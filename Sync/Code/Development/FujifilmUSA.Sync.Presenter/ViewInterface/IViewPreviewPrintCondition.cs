using System.Collections.Generic;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewPreviewPrintCondition : IViewError
    {
        int DeviceFamilyProfilerOptionId { get; set; }
        short IlluminantId { get; set; }
        short ObserverTypeId { get; set; }
        short ReferenceChartTypeId { get; set; }
        short? MeasurementDeviceId { get; set; }
        Dictionary<string, string> Data { get; set; }
    }
}
