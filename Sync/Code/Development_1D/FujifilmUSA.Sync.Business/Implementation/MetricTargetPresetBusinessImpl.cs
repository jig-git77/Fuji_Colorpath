using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.CustomExceptions;
using System.Xml.Linq;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MetricTargetPresetBusinessImpl : IMetricTargetPresetBusiness
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
                                            MetricTargetPreset.Weighted,
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
                                        MetricTargetPreset.Weighted,
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
                                MetricTargetPresetName = DataReader.ReadString(x, "MetricTargetPresetName"),
                                MetricTargetPresetDesc = DataReader.ReadString(x, "MetricTargetPresetDesc"),
                                dEMethod = DataReader.ReadString(x, "dEMethod"),
                                ValidationType = DataReader.ReadInt(x, "ValidationType"),
                                IsSync1D = DataReader.ReadBoolean(x, "IsSync1D"),
                                Validation_1D = DataReader.ReadBoolean(x, "Validation_1D"),
                                Validation_4D = DataReader.ReadBoolean(x, "Validation_4D"),
                                IsPatchTolerance = DataReader.ReadBoolean(x, "IsPatchTolerance"),
                                Weighted = DataReader.ReadBoolean(x, "Weighted"),
                                IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMetricTargetPresets"));
            return metricTargetPresets;
        }

        public List<ReferenceChartType> GetReferenceChartTypeList(int MeasurementDeviceID, int ChartDisplayType, int ProjectTypeId, int MetricTargetPresetID = 0, int InstrumentTypeId = 0, bool IsDLPPlus = false, short LicenseTypeId = 0, int DeviceId = 0)
        {
            var presetList = new List<ReferenceChartType>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsPresetList = DataAccessFactory.Instance.GetDsResult("CP.GetReferenceChartTypeList", new object[] { MeasurementDeviceID, ChartDisplayType, ProjectTypeId, MetricTargetPresetID, InstrumentTypeId, IsDLPPlus, LicenseTypeId, DeviceId });
                    if (dsPresetList != null && dsPresetList.Tables.Count > 0)
                    {
                        presetList = dsPresetList.Tables[0].AsEnumerable().Select(
                            x => new ReferenceChartType()
                            {
                                ReferenceChartTypeId = DataReader.ReadShort(x, "ReferenceChartTypeID"),
                                ChartName = DataReader.ReadString(x, "ChartName")
                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetReferenceChartTypeList"));
            return presetList;
        }

        public List<Entity.CPSyncV.MetricSum> GetMetricSumList()
        {
            List<Entity.CPSyncV.MetricSum> metricSumList = new List<Entity.CPSyncV.MetricSum>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMetricTargetPresetList = DataAccessFactory.Instance.GetDsResult("[CPSyncV].[GetMetricSumList]", new object[] { });

                    if (dsMetricTargetPresetList != null && dsMetricTargetPresetList.Tables.Count > 0)
                    {
                        metricSumList = dsMetricTargetPresetList.Tables[0].AsEnumerable().Select(
                        x => new Entity.CPSyncV.MetricSum()
                        {
                            MetricSumId = DataReader.ReadInt(x, "MetricSumId"),
                            MetricSumName = DataReader.ReadString(x, "MetricSumName")
                        }).ToList();
                    }

                }, new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMetricSumList"));
            return metricSumList;
        }

        public List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetPatchPreset(int metricTargetPresetId, int currentUserId)
        {
            var metricTargetPresets = new List<Entity.CPSyncV.MetricTargetPreset>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsmetricTargetPresets = DataAccessFactory.Instance.GetDsResult("[CPSyncV].[GetMetricTargetPatchPreset]", new object[] { metricTargetPresetId, currentUserId });
                    if (dsmetricTargetPresets != null && dsmetricTargetPresets.Tables.Count > 0 && dsmetricTargetPresets.Tables[0].Rows.Count > 0)
                    {
                        metricTargetPresets = dsmetricTargetPresets.Tables[0].AsEnumerable().
                            Select(x => new Entity.CPSyncV.MetricTargetPreset()
                            {
                                MetricTargetPresetID = DataReader.ReadShort(x, "MetricTargetPresetID"),
                                Company = new Company() { CompanyId = DataReader.ReadInt(x, "CompanyID"), CompanyName = DataReader.ReadString(x, "CompanyName") },
                                Region = new Region() { RegionId = DataReader.ReadShort(x, "RegionID"), RegionName = DataReader.ReadString(x, "Region") },
                                MetricTargetPresetName = DataReader.ReadString(x, "MetricTargetPresetName"),
                                MetricTargetPresetDesc = DataReader.ReadString(x, "MetricTargetPresetDesc"),
                                dEMethod = DataReader.ReadString(x, "dEMethod"),
                                ValidationType = DataReader.ReadInt(x, "ValidationType"),
                                IsPatchTolerance = DataReader.ReadBoolean(x, "IsPatchTolerance"),
                                IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                                ReferenceChartTypeId = DataReader.ReadInt(x, "ReferenceChartTypeId"),
                                InstrumentTypeID = DataReader.ReadInt(x, "InstrumentTypeID")
                            }).ToList();
                        if (metricTargetPresetId != 0)
                        {
                            metricTargetPresets[0].MetricSumTolerances = dsmetricTargetPresets.Tables[1].AsEnumerable().
                               Select(x => new Entity.CPSyncV.MetricSum()
                               {
                                   MetricSumId = DataReader.ReadInt(x, "MetricSumID"),
                                   MetricSumName = DataReader.ReadString(x, "MetricSumName"),
                                   TargetValue = DataReader.ReadDecimal(x, "TargetValue")
                               }).ToList();

                            metricTargetPresets[0].PatchTolerances = dsmetricTargetPresets.Tables[2].AsEnumerable().
                              Select(x => new Entity.CPSyncV.PatchTolerance()
                              {
                                  SampleName = DataReader.ReadString(x, "SampleName"),
                                  Tolerance = DataReader.ReadDecimal(x, "TargetValue")
                              }).ToList();
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMetricTargetPresets"));
            return metricTargetPresets;
        }


        public Dictionary<int, string> BindChartPatchList(int ReferenceChartTypeId)
        {
            var dstChartPatchList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsChartPatchList = DataAccessFactory.Instance.GetDsResult("[CP].[GetReferenceChartPatchList]", new object[] { ReferenceChartTypeId });
                    if (dsChartPatchList != null && dsChartPatchList.Tables.Count > 0)
                    {

                        dstChartPatchList = (dsChartPatchList.Tables[0].AsEnumerable().Select(
                            ChartPatchType => new
                            {
                                Value = DataReader.ReadInt(ChartPatchType, "ReferenceChartID"),
                                Text = DataReader.ReadString(ChartPatchType, "SampleName")
                            })).
                            ToDictionary(ChartPatchType => ChartPatchType.Value, ChartPatchType => ChartPatchType.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartPatchList"));
            return dstChartPatchList;
        }

        public int InsertMetricTargetPatchPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset)
        {
            int returnValue = 0;
            Common.
                ImplementBusinessLogic(
                    () =>
                    {
                        var MetricSumToleranceData = new XElement("MetricSumToleranceData",
                                                          MetricTargetPreset.MetricSumTolerances.
                                                              Select(x =>
                                                                     new XElement("MetricSumTolerance",
                                                                                  new XAttribute("MetricSumID", x.MetricSumId),
                                                                                  new XAttribute("MetricSumName", x.MetricSumName),
                                                                                  new XAttribute("TargetValue", x.TargetValue)
                                                                         )).ToList());

                        var PatchToleranceData = new XElement("PatchToleranceData",
                                                         MetricTargetPreset.PatchTolerances.
                                                             Select(x =>
                                                                    new XElement("PatchTolerance",
                                                                                 new XAttribute("SampleName", x.SampleName),
                                                                                 new XAttribute("TargetValue", x.Tolerance)
                                                                        )).ToList());

                        returnValue = DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                "[CPSyncV].[InsertMetricTargetPatchPreset]",
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
                                            MetricTargetPreset.IsUsed,
                                            MetricTargetPreset.ReferenceChartTypeId,
                                            MetricSumToleranceData.ToString(),
                                            PatchToleranceData.ToString(),
                                            MetricTargetPreset.IsPatchTolerance
                                        });
                        if (returnValue == -1)
                        {
                            throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertMetricTargetPreset" };
                        }
                    },
                    new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertMetricTargetPreset"));
            return returnValue;
        }

        public int UpdateMetricTargetPatchPreset(Entity.CPSyncV.MetricTargetPreset MetricTargetPreset)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    var MetricSumToleranceData = new XElement("MetricSumToleranceData",
                                                         MetricTargetPreset.MetricSumTolerances.
                                                             Select(x =>
                                                                    new XElement("MetricSumTolerance",
                                                                                 new XAttribute("MetricSumID", x.MetricSumId),
                                                                                 new XAttribute("MetricSumName", x.MetricSumName),
                                                                                 new XAttribute("TargetValue", x.TargetValue)
                                                                        )).ToList());

                    var PatchToleranceData = new XElement("PatchToleranceData",
                                                     MetricTargetPreset.PatchTolerances.
                                                         Select(x =>
                                                                new XElement("PatchTolerance",
                                                                             new XAttribute("SampleName", x.SampleName),
                                                                             new XAttribute("TargetValue", x.Tolerance)
                                                                    )).ToList());
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CPSyncV].[UpdateMetricTargetPatchPreset]",
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
                                            MetricTargetPreset.IsUsed,
                                            MetricTargetPreset.ReferenceChartTypeId,
                                            MetricSumToleranceData.ToString(),
                                            PatchToleranceData.ToString()
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_UpdateMetricTargetPreset" };
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateMetricTargetPreset"));
            return returnValue;
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
