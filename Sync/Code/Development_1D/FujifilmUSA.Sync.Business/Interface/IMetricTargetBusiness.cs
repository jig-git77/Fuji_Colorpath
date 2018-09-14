using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMetricTargetBusiness : ITaskBase
    {
        // List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetPresetList(int currentUserId, int ProjectTypeId, bool isVarification = false);
        //List<Entity.CPSyncV.MetricSum> GetMetricSumTargetValues(int metricTargetPresetId);

        //int InsertMetricTargetPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset);
        //int UpdateMetricTargetPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset);
        //bool DeleteMetricTargetPreset(int metricTargetPresetId);
        //List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetPreset(int metricTargetPresetId, int currentUserId);
        List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetList(int currentUserId);
        //List<ReferenceChartType> GetReferenceChartTypeList(int MeasurementDeviceID, int ChartDisplayType, int ProjectTypeId, int MetricTargetPresetID = 0, int InstrumentTypeID = 0);
        //List<Entity.CPSyncV.MetricSum> GetMetricSumList();
        //List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetPatchPreset(int metricTargetPresetId, int currentUserId);
        //Dictionary<int, string> BindChartPatchList(int ReferenceChartTypeId);
        //int InsertMetricTargetPatchPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset);
        //int UpdateMetricTargetPatchPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset);
    }
}
