using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddMetricTarget : IViewBase
    {
        Entity.CPSyncV.MetricTargetPreset MetricTargetPreset { get; set; }
        //Dictionary<int, string> Companies { get; set; }
        //Dictionary<int, string> RegionList { get; set; }
        //int RegionId { get; set; }
        //List<ReferenceChartType> ReferenceChartType { get; set; }
        //Dictionary<int, string> InstrumentTypeList { get; set; }
        //int InstrumentId { get; set; }
        List<Entity.CPSyncV.MetricSum> MetricSumList { get; set; }
        Dictionary<int, string> MetricSums { get; set; }
        string MetricSumName { get; set; }
        List<Entity.CPSyncV.MetricTargetPreset> MetricTargetPresetList { get; set; }
        DataTable dtPresetRefChart { get; set; } 
        //int ReferenceChartTypeId { get; set; }
        //Dictionary<int, string> ChartPatchList { get; set; }
    }
}
