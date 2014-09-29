using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.CustomExceptions;
    

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MetricTargetPresetBusinessImpl: IMetricTargetPresetBusiness
    {
        #region  IMetricTargetPresetBusiness Members

        public int InsertMetricTargetPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset)
        {
            int returnValue = 0;
            Common.
                ImplementBusinessLogic(
                    () =>
                    {
                        returnValue = DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                "[CPSyncV].[InsertMetricTargetPreset]",
                                new object[]
                                        {
                                            MetricTargetPreset.RegionId,
                                            MetricTargetPreset.CompanyId,
                                            MetricTargetPreset.MetricTargetPresetName,
                                            MetricTargetPreset.MetricTargetPresetDesc,
                                            MetricTargetPreset.dEMethod,
                                            MetricTargetPreset.ValidationType,
                                            MetricTargetPreset.IsSync1D,
                                            MetricTargetPreset.Validation_1D,
                                            MetricTargetPreset.Validation_4D,
                                            MetricTargetPreset.IsUsed
                                        });
                        if (returnValue == -1)
                        {
                            throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertMetricTargetPreset" };
                        }
                    },
                    new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertMetricTargetPreset"));
            return returnValue;
        }

        public int UpdateMetricTargetPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CPSyncV].[UpdateMetricTargetPreset]",
                            new object[]
                                    {
                                        MetricTargetPreset.MetricTargetPresetID,
                                        MetricTargetPreset.RegionId,
                                        MetricTargetPreset.CompanyId,
                                        MetricTargetPreset.MetricTargetPresetName,
                                        MetricTargetPreset.MetricTargetPresetDesc,
                                        MetricTargetPreset.dEMethod,
                                        MetricTargetPreset.ValidationType,
                                        MetricTargetPreset.IsSync1D,
                                        MetricTargetPreset.Validation_1D,
                                        MetricTargetPreset.Validation_4D,
                                        MetricTargetPreset.IsUsed
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_UpdateMetricTargetPreset" };
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateMetricTargetPreset"));
            return returnValue;
        }

        public bool DeleteMetricTargetPreset(int metricTargetPresetId)
        {
            Common.ImplementBusinessLogic(
                () =>
                DataAccessFactory.
                    Instance.
                    ExecuteProcedure(
                        "[CPSyncV].[DeleteMetricTargetPreset]",
                        new object[]
                            {
                                metricTargetPresetId
                            }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteMetricTargetPreset"));
            return true;
        }

        public List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetPreset(int metricTargetPresetId, int currentUserId)
        {
            var metricTargetPresets = new List<Entity.CPSyncV.MetricTargetPreset>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsmetricTargetPresets = DataAccessFactory.Instance.GetDsResult("[CPSyncV].[GetMetricTargetPreset]", new object[] { metricTargetPresetId, currentUserId });
                    if (dsmetricTargetPresets != null && dsmetricTargetPresets.Tables.Count > 0 && dsmetricTargetPresets.Tables[0].Rows.Count > 0)
                    {
                        metricTargetPresets = dsmetricTargetPresets.Tables[0].AsEnumerable().
                            Select(x => new Entity.CPSyncV.MetricTargetPreset()
                            {
                                MetricTargetPresetID = DataReader.ReadShort(x, "MetricTargetPresetID"),
                                Company = new Company() { CompanyId = DataReader.ReadInt(x, "CompanyID"), CompanyName = DataReader.ReadString(x, "CompanyName") },
                                Region = new Region() { RegionId = DataReader.ReadShort(x, "RegionID"), RegionName = DataReader.ReadString(x, "Region") },
                                //RegionId = DataReader.ReadShort(x, "RegionID"),
                                //CompanyId = DataReader.ReadShort(x, "CompanyID"),               
                                MetricTargetPresetName = DataReader.ReadString(x, "MetricTargetPresetName"),
                                MetricTargetPresetDesc = DataReader.ReadString(x, "MetricTargetPresetDesc"),
                                dEMethod = DataReader.ReadString(x, "dEMethod"),
                                ValidationType = DataReader.ReadInt(x, "ValidationType"),
                                IsSync1D = DataReader.ReadBoolean(x, "IsSync1D"),
                                Validation_1D = DataReader.ReadBoolean(x, "Validation_1D"),
                                Validation_4D = DataReader.ReadBoolean(x, "Validation_4D"),
                                IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMetricTargetPresets"));
            return metricTargetPresets;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
