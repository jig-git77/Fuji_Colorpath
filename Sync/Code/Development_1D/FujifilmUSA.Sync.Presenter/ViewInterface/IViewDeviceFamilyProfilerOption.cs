using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewDeviceFamilyProfilerOption : IViewBase
    {
        short DeviceFamilyId { get; set; }
        List<DeviceFamilyProfilerOption> ProfileOption { get; set; }
        int MetricTargetPresetId { get; set; }
        List<Entity.CPSyncV.MetricSum> MetricSumTargetValues { get; set; }
        int PrintStandardTypeId { get; set; }
        List<Entity.CPSync1D.PrintStandardTVI> PrintStandardTVI { get; set; }
        List<Entity.CPSync1D.ProjectTargetDensity> ProjectTargetDensity { get; set; }
        short LicenseTypeId { get; set; }
    }
}
