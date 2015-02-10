using System.Collections.Generic;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewPreviewPrintCondition : IViewBase
    {
        int DeviceFamilyProfilerOptionId { get; set; }
        short IlluminantId { get; set; }
        short ObserverTypeId { get; set; }
        short ReferenceChartTypeId { get; set; }
        short? MeasurementDeviceId { get; set; }
        Dictionary<string, string> Data { get; set; }
        short? V_MeasurementDeviceId { get; set; }
        short Opt_ReferenceChartTypeId { get; set; }
        short V_ReferenceChartTypeId { get; set; }
        short MetricTargetPresetId { get; set; }
        int ReTargetProjectID { get; set; }
    }
}
