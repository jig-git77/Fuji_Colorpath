using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MetricTargetPresetBusinessMock : IMetricTargetPresetBusiness
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

        public List<ReferenceChartType> GetReferenceChartTypeList(int MeasurementDeviceID, int ChartDisplayType, int ProjectTypeId, int MetricTargetPresetID = 0, int InstrumentTypeID = 0, bool IsDLPPlus = false, short LicenseTypeId = 0, int DeviceId = 0)
        {
            return new List<ReferenceChartType>();
        }

        public List<Entity.CPSyncV.MetricSum> GetMetricSumList()
        {
            return new List<Entity.CPSyncV.MetricSum>();
        }

        public List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetPatchPreset(int metricTargetPresetId, int currentUserId)
        {
            return new List<Entity.CPSyncV.MetricTargetPreset>();
        }

        public Dictionary<int, string> BindChartPatchList(int ReferenceChartTypeId)
        {
            return new Dictionary<int, string>();
        }

        public int InsertMetricTargetPatchPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset)
        {
            return 0;
        }

        public int UpdateMetricTargetPatchPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset)
        {
            return 0;
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
