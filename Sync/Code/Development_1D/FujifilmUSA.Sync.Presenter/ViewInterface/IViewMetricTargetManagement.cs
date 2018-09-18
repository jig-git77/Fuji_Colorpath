using FujifilmUSA.Sync.Entity.CPSyncV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewMetricTargetManagement : IViewBase
    {
        int MetricTargetPresetID { get; set; }
        Entity.CPSyncV.MetricTargetPreset MetricTargetPreset { get; set; }
        List<Entity.CPSyncV.MetricTargetPreset> MetricTargetPresetList { get; set; }
        List<MetricSum> MetricSumTolerancesList { get; set; }
    }
}
