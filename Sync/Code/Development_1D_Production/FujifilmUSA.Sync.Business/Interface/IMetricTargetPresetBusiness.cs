using FujifilmUSA.Sync.Business.Messages;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using System.Data;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMetricTargetPresetBusiness: ITaskBase
    {
       // List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetPresetList(int currentUserId, int ProjectTypeId, bool isVarification = false);
        //List<Entity.CPSyncV.MetricSum> GetMetricSumTargetValues(int metricTargetPresetId);

        int InsertMetricTargetPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset);
        int UpdateMetricTargetPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset);
        bool DeleteMetricTargetPreset(int metricTargetPresetId);
        List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetPreset(int metricTargetPresetId, int currentUserId);
    }
}
