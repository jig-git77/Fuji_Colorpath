using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMetricTargetBusiness : ITaskBase
    {
        Dictionary<int, string> GetAllMetricSumList();
        Entity.CPSyncV.MetricTargetPreset GetMetricTargetPreset(int metricTargetPresetId, int currentUserId);
        List<Entity.CPSyncV.MetricSum> GetMetricTargetSumList(int MetricTargetPresetID, int currentUserId);
        int UpdateMetricTargetPreset(int MetricTargetPresetID, List<Entity.CPSyncV.MetricSum> MetricTargets);
        int SaveMetricSum(string MetricSumName);
    }
}
