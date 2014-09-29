using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MetricTargetPresetBusinessMockP : IMetricTargetPresetBusiness
    {
        #region  IMetricTargetPresetBusiness Members

        public int InsertMetricTargetPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset)
        {
            return 0;
        }

        public int UpdateMetricTargetPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset)
        {
            return 0;
        }

        public bool DeleteMetricTargetPreset(int metricTargetPresetId)
        {
            return true;
        }

        public List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetPreset(int metricTargetPresetId, int currentUserId)
        {
            return new List<Entity.CPSyncV.MetricTargetPreset>();
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
