using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity.CPSyncV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MetricTargetBusinessImpl : IMetricTargetBusiness
    {
        #region  IMetricTargetBusiness Members
        public List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetList(int currentUserId)
        {
            var metricTargets = new List<Entity.CPSyncV.MetricTargetPreset>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsmetricTarget = DataAccessFactory.Instance.GetDsResult("[CPSyncV].[MetricTargetPresetList]", new object[] { currentUserId });
                    if (dsmetricTarget != null && dsmetricTarget.Tables.Count > 0 && dsmetricTarget.Tables[0].Rows.Count > 0)
                    {
                        metricTargets = dsmetricTarget.Tables[0].AsEnumerable().
                            Select(x => new Entity.CPSyncV.MetricTargetPreset()
                            {
                                MetricTargetPresetID = DataReader.ReadShort(x, "MetricTargetPresetID"),
                                //Company = new Company() { CompanyId = DataReader.ReadInt(x, "CompanyID"), CompanyName = DataReader.ReadString(x, "CompanyName") },
                                //Region = new Region() { RegionId = DataReader.ReadShort(x, "RegionID"), RegionName = DataReader.ReadString(x, "Region") },
                                MetricTargetPresetName = DataReader.ReadString(x, "MetricTargetPresetName")
                                //MetricTargetPresetDesc = DataReader.ReadString(x, "MetricTargetPresetDesc"),
                                //dEMethod = DataReader.ReadString(x, "dEMethod"),
                                //ValidationType = DataReader.ReadInt(x, "ValidationType"),
                                //IsSync1D = DataReader.ReadBoolean(x, "IsSync1D"),
                                //Validation_1D = DataReader.ReadBoolean(x, "Validation_1D"),
                                //Validation_4D = DataReader.ReadBoolean(x, "Validation_4D"),
                                //IsPatchTolerance = DataReader.ReadBoolean(x, "IsPatchTolerance"),
                                //IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                            }).ToList();

                        if (dsmetricTarget.Tables[1].Rows.Count > 0)
                        {
                            foreach (var item in metricTargets)
                            {
                                var metricSums = new List<MetricSum>();
                                metricSums = dsmetricTarget.Tables[1].AsEnumerable().Where(r => r.Field<short>("MetricTargetPresetID") == item.MetricTargetPresetID).
                                   Select(x => new Entity.CPSyncV.MetricSum()
                                   {
                                       MetricTargetID = DataReader.ReadShort(x, "MetricTargetID"),
                                       MetricTargetPresetID = DataReader.ReadShort(x, "MetricTargetPresetID"),
                                       MetricSumId = DataReader.ReadShort(x, "MetricSumID"),
                                       MetricSumName = DataReader.ReadString(x, "MetricSumName"),
                                       TargetValue = DataReader.ReadDecimal(x, "TargetValue")
                                   }).ToList();
                                item.MetricSumTolerances = metricSums;
                            }
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMetricTargetPresets"));
            return metricTargets;
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
