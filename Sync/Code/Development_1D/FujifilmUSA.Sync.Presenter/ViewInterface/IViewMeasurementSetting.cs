using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewMeasurementSetting : IViewBase
    {
        List<Illuminant> Illumin { get; set; }
        List<ObserverType> Observer { get; set; }
        int DeviceId { get; set; }
        List<MeasurementDevice> MeasurementDevices { get; set; }
        int MeasurementDeviceId { get; set; }
        List<ReferenceChartType> ReferenceChartType { get; set; }
        short ReferenceChartTypeId { get; set; }
        byte[] ReferenceChartTypeImage { get; set; }
        int ProjectId { get; set; }
        Project Project { get; set; }
        int ChartDisplayType { get; set; }
        List<Entity.CPSyncV.MetricTargetPreset> MetricTargetPreset { get; set; }
        int MetricTargetPresetID { get; set; }
        ELicenseType LicenseType { get; }
        int ProjectTypeID { get; }
        Dictionary<int, string> ReTargetProjectList { get; set; }
        byte[] ChartPreview { get; set; }
    }
}
