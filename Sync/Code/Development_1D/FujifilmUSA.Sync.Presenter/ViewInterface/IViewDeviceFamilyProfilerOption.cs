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
        List<Entity.CPSync1D.ProjectTargetHueAngle> ProjectTargetHueAngle { get; set; }
        short LicenseTypeId { get; set; }
        List<Device> Devices { get; set; }
        int CompanyID { get; set; }
        int BranchID { get; set; }
        int DeviceID { get; set; }
        int RetargetProjectID { get; set; }
        string RetargetPrintStandardTypeName { get; set; }
        string RetargetMetricTargetPresetName { get; set; }

        ProfilerOptionPreset ProfilerOptionPreset { get; set; }
        DeviceFamilyProfilerOption DeviceFamilyProfilerOption { get; set; }
        int DeviceFamilyProfilerOptionID { get; set; }

        int UserID { get; set; }
        bool IsCustomPreset { get; set; }
    }
}
