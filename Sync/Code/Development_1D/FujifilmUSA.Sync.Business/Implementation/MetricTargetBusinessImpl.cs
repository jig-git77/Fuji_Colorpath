using FujifilmUSA.Sync.Business.CustomExceptions;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSyncV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MetricTargetBusinessImpl : IMetricTargetBusiness
    {
        #region  IMetricTargetBusiness Members
        

        public List<Entity.CPSyncV.MetricSum> GetMetricTargetSumList(int MetricTargetPresetID, int currentUserId)
        {
            var metricTargets = new List<Entity.CPSyncV.MetricSum>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsmetricTarget = DataAccessFactory.Instance.GetDsResult("[CPSyncV].[MetricTargetPresetList]", new object[] { MetricTargetPresetID,currentUserId });
                    if (dsmetricTarget != null && dsmetricTarget.Tables.Count > 0 && dsmetricTarget.Tables[0].Rows.Count > 0)
                    {
                        metricTargets = dsmetricTarget.Tables[0].AsEnumerable().
                            Select(x => new Entity.CPSyncV.MetricSum()
                            {
                                MetricTargetPresetID = DataReader.ReadShort(x, "MetricTargetPresetID"),
                                MetricTargetID= DataReader.ReadShort(x, "MetricTargetID"),
                                MetricSumId = DataReader.ReadShort(x, "MetricSumID"),
                                MetricSumName = DataReader.ReadString(x, "MetricSumName"),
                                TargetValue = DataReader.ReadDecimal(x, "TargetValue")
                            }).ToList();

                        //if (dsmetricTarget.Tables[1].Rows.Count > 0)
                        //{
                        //    foreach (var item in metricTargets)
                        //    {
                        //        var metricSums = new List<MetricSum>();
                        //        metricSums = dsmetricTarget.Tables[1].AsEnumerable().Where(r => r.Field<short>("MetricTargetPresetID") == item.MetricTargetPresetID).
                        //           Select(x => new Entity.CPSyncV.MetricSum()
                        //           {
                        //               MetricTargetID = DataReader.ReadShort(x, "MetricTargetID"),
                        //               MetricTargetPresetID = DataReader.ReadShort(x, "MetricTargetPresetID"),
                        //               MetricSumId = DataReader.ReadShort(x, "MetricSumID"),
                        //               MetricSumName = DataReader.ReadString(x, "MetricSumName"),
                        //               TargetValue = DataReader.ReadDecimal(x, "TargetValue")
                        //           }).ToList();
                        //        item.MetricSumTolerances = metricSums;
                        //    }
                        //}
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMetricTargetPresets"));
            return metricTargets;
        }

        public Entity.CPSyncV.MetricTargetPreset GetMetricTargetPreset(int metricTargetPresetId, int currentUserId)
        {
            var metricTargetPreset = new Entity.CPSyncV.MetricTargetPreset();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsmetricTargetPresets = DataAccessFactory.Instance.GetDsResult("[CPSyncV].[GetMetricTargetPresetForMetricSum]", new object[] { metricTargetPresetId, currentUserId });
                    if (dsmetricTargetPresets != null && dsmetricTargetPresets.Tables.Count > 0 && dsmetricTargetPresets.Tables[0].Rows.Count > 0)
                    {
                        metricTargetPreset = dsmetricTargetPresets.Tables[0].AsEnumerable().
                            Select(x => new Entity.CPSyncV.MetricTargetPreset()
                            {
                                MetricTargetPresetID = DataReader.ReadShort(x, "MetricTargetPresetID"),
                                Company = new Company() { CompanyId = DataReader.ReadInt(x, "CompanyID"), CompanyName = DataReader.ReadString(x, "CompanyName") },
                                Region = new Region() { RegionId = DataReader.ReadShort(x, "RegionID"), RegionName = DataReader.ReadString(x, "Region") },
                                MetricTargetPresetName = DataReader.ReadString(x, "MetricTargetPresetName"),
                                MetricTargetPresetDesc = DataReader.ReadString(x, "MetricTargetPresetDesc"),
                                dEMethod = DataReader.ReadString(x, "dEMethod"),
                                ValidationType = DataReader.ReadInt(x, "ValidationType"),
                                //IsSync1D = DataReader.ReadBoolean(x, "IsSync1D"),
                                //Validation_1D = DataReader.ReadBoolean(x, "Validation_1D"),
                                //Validation_4D = DataReader.ReadBoolean(x, "Validation_4D"),
                                //IsPatchTolerance = DataReader.ReadBoolean(x, "IsPatchTolerance"),
                                IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                            }).First();

                        metricTargetPreset.MetricSumTolerances = dsmetricTargetPresets.Tables[1].AsEnumerable().
                              Select(x => new Entity.CPSyncV.MetricSum()
                              {
                                  MetricTargetID= DataReader.ReadInt(x, "MetricTargetID"),
                                  MetricSumId = DataReader.ReadInt(x, "MetricSumID"),
                                  MetricSumName = DataReader.ReadString(x, "MetricSumName"),
                                  TargetValue = DataReader.ReadDecimal(x, "TargetValue"),
                                  SortOder = DataReader.ReadInt(x, "RankID")
                              }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMetricTargetPresets"));
            return metricTargetPreset;
        }
        
        public int UpdateMetricTargetPreset(int MetricTargetPresetID,List<Entity.CPSyncV.MetricSum> MetricTargets)
        {            
            var metricTargetXML = new XElement("MetricTargetList",
                                                         MetricTargets.
                                                             Select(x =>
                                                                    new XElement("MetricTarget",
                                                                                 new XAttribute("MetricTargetID", x.MetricTargetID),
                                                                                 new XAttribute("MetricSumID", x.MetricSumId),
                                                                                 new XAttribute("TargetValue", x.TargetValue),
                                                                                 new XAttribute("MetricTargetPresetID", x.MetricTargetPresetID),
                                                                                 new XAttribute("RankId", x.SortOder)
                                                                        )).ToList());
            var outParam = new Parameters() { ParameterName = "@IsSaved", Type = DbType.Int32 };
            Common.ImplementBusinessLogic(
                () =>
                {                   
                    DataAccessFactory.
                            Instance.
                            ExecuteProcedure("CPSyncV.SaveMetricTargetSum",
                                             new List<Parameters>()
                                                 {
                                                 new Parameters() {ParameterName = "@MetricTargetPresetID", Type = DbType.Int32, Value = MetricTargetPresetID},
                                                 new Parameters() {ParameterName = "@MetricTargetSumXML", Value = metricTargetXML.ToString()},
                                                 },
                                             new List<Parameters>()
                                                 {
                                                 outParam
                                                 });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateMetricTargetPreset"));
            return Convert.ToInt32(outParam.Value);
        }

        public Dictionary<int, string> GetAllMetricSumList()
        {
            var dsMetricSumList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsmetricSum = DataAccessFactory.Instance.GetDsResult("[CPSyncV].[GetAllMetricSumList]", new object[] {});
                    if (dsmetricSum != null && dsmetricSum.Tables.Count > 0 && dsmetricSum.Tables[0].Rows.Count > 0)
                    {
                        //metricSums = dsmetricSum.Tables[0].AsEnumerable().
                        //    Select(x => new Entity.CPSyncV.MetricSum()
                        //    {
                        //        MetricTargetPresetID = DataReader.ReadShort(x, "MetricSumId"),
                        //        MetricSumName = DataReader.ReadString(x, "MetricSumName")
                        //    }).ToList();

                        dsMetricSumList = (dsmetricSum.Tables[0].AsEnumerable().Select(
                            metric => new
                            {
                                Value = DataReader.ReadInt(metric, "MetricSumId"),
                                Text = DataReader.ReadString(metric, "MetricSumName")
                            })).ToDictionary(metric => metric.Value, metric => metric.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMetricTargetPresets"));
            return dsMetricSumList;
        }

        public int SaveMetricSum(string MetricSumName)
        {
            var outParam = new Parameters() { ParameterName = "@IsSaved", Type = DbType.Int32 };
            Common.ImplementBusinessLogic(
                () =>
                {
                    DataAccessFactory.
                            Instance.
                            ExecuteProcedure("CPSyncV.SaveMetricSum",
                                             new List<Parameters>()
                                                 {
                                                 new Parameters() {ParameterName = "@MetricSumName", Type = DbType.String, Value = MetricSumName},                                                 
                                                 },
                                             new List<Parameters>()
                                                 {
                                                 outParam
                                                 });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateMetricTargetPreset"));
            return Convert.ToInt32(outParam.Value);
        }

        public DataTable GetMetricTargetPresetReferenceCharts(int metricTargetPresetId, int currentUserId)
        {
            DataTable dtRefChart = new DataTable();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsmetricTargetPresets = DataAccessFactory.Instance.GetDsResult("[CPSyncV].[GetMetricTargetPresetReferenceCharts]", new object[] { metricTargetPresetId, currentUserId });
                    if (dsmetricTargetPresets != null && dsmetricTargetPresets.Tables.Count > 0 && dsmetricTargetPresets.Tables[0].Rows.Count > 0)
                    {
                        dtRefChart = dsmetricTargetPresets.Tables[0];
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMetricTargetPresets"));
            return dtRefChart;
        }

        public int UpdateReferenceChartPreset(int MetricTargetPresetID, List<Entity.CPSyncV.ReferenceChartPreset> ReferenceChartPresets)
        {
            var ReferenceChartPresetsXML = new XElement("ReferenceChartPresetList",
                                                         ReferenceChartPresets.
                                                             Select(x =>
                                                                    new XElement("ReferenceChartPreset",
                                                                                 new XAttribute("ReferenceChartTypeID", x.ReferenceChartTypeId),
                                                                                 new XAttribute("MetricTargetPresetID", x.MetricTargetPresetID)
                                                                        )).ToList());
            var outParam = new Parameters() { ParameterName = "@IsSaved", Type = DbType.Int32 };
            Common.ImplementBusinessLogic(
                () =>
                {
                    DataAccessFactory.
                            Instance.
                            ExecuteProcedure("CPSyncV.SaveReferenceChartPreset",
                                             new List<Parameters>()
                                                 {
                                                 new Parameters() {ParameterName = "@MetricTargetPresetID", Type = DbType.Int32, Value = MetricTargetPresetID},
                                                 new Parameters() {ParameterName = "@ReferenceChartPresetXML", Value = ReferenceChartPresetsXML.ToString()},
                                                 },
                                             new List<Parameters>()
                                                 {
                                                 outParam
                                                 });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateMetricTargetPreset"));
            return Convert.ToInt32(outParam.Value);
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
