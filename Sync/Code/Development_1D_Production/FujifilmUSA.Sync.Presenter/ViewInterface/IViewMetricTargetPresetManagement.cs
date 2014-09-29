using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewMetricTargetPresetManagement : IViewBase
    {
        Entity.CPSyncV.MetricTargetPreset MetricTargetPreset { get; set; }
        List<Entity.CPSyncV.MetricTargetPreset> MetricTargetPresetList { get; set; }
    }
}
