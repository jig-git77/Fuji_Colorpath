using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddMetricTargetPreset: IViewBase
    {       
        Entity.CPSyncV.MetricTargetPreset MetricTargetPreset { get; set; }
        Dictionary<int, string> Companies { get; set; }
        Dictionary<int, string> RegionList { get; set; }
        int RegionId { get; set; }
    }
}
