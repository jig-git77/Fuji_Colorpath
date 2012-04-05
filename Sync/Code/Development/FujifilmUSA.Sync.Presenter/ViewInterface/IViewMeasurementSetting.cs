using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewMeasurementSetting : IViewError
    {
        List<Illuminant> Illumin { get; set; }
        List<ObserverType> Observer { get; set; }
        int DeviceId { get; set; }
        List<MeasurementDevice> MeasurementDevices { get; set; }
        int MeasurementDeviceId { get; set; }
        List<ReferenceChartType> ReferenceChartType { get; set; }
        int ReferenceChartTypeId { get; set; }
        byte[] ReferenceChartTypeImage { get; set; }
    }
}
