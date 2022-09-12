using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity.CPSyncV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ReferenceChartPresetImpl : IReferenceChartPresetBusiness
    {
        public event ErrorHandler ErrorOccured;

        /// <summary>
        /// GetMetricTargetPresetID
        /// </summary>
        public Dictionary<int, string> GetMetricTargetPresetID(int ReferenceChartTypeID)
        {
            var dstReferenceChartTypeList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsRegion = DataAccessFactory.Instance.GetDsResult("[CPSyncV].[GetMetricTargetPresetForMTP]", new object[] { ReferenceChartTypeID });
                    if (dsRegion != null && dsRegion.Tables.Count > 0)
                    {
                        dstReferenceChartTypeList = (dsRegion.Tables[0].AsEnumerable().Select(
                            ReferenceChartType => new
                            {
                                Value = DataReader.ReadInt(ReferenceChartType, "MetricTargetPresetID"),
                                Text = DataReader.ReadString(ReferenceChartType, "MetricTargetPresetName")
                            })).ToDictionary(region => region.Value, region => region.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMetricTargetPresetID"));
            return dstReferenceChartTypeList;
        }

        /// <summary>
        /// GetReferenceChartType
        /// </summary>
        public Dictionary<int, string> GetReferenceChartType()
        {
            var dstReferenceChartTypeList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsRegion = DataAccessFactory.Instance.GetDsResult("[CP].[GetReferenceChartForMTP]", new object[] { });
                    if (dsRegion != null && dsRegion.Tables.Count > 0)
                    {
                        dstReferenceChartTypeList = (dsRegion.Tables[0].AsEnumerable().Select(
                            ReferenceChartType => new
                            {
                                Value = DataReader.ReadInt(ReferenceChartType, "ReferenceChartTypeID"),
                                Text = DataReader.ReadString(ReferenceChartType, "ChartName")
                            })).ToDictionary(region => region.Value, region => region.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetReferenceChartTypeList"));
            return dstReferenceChartTypeList;
        }

        /// <summary>
        /// InsertReferenceChartPreset
        /// </summary>
        /// <param name="ReferenceChartTypeID"></param>
        /// <param name="ReferenceChartPresets"></param>
        /// <returns></returns>
        public int InsertReferenceChartPreset(int ReferenceChartTypeID, List<ReferenceChartPreset> ReferenceChartPresets)
        {
            var ReferenceChartPresetsXML = new XElement("ReferenceChartPresetList",
                                                          ReferenceChartPresets.
                                                              Select(x =>
                                                                     new XElement("ReferenceChartPreset",
                                                                                  new XAttribute("ReferenceChartTypeID", ReferenceChartTypeID),
                                                                                  new XAttribute("MetricTargetPresetID", x.MetricTargetPresetID)
                                                                         )).ToList());
            var outParam = new Parameters() { ParameterName = "@IsSaved", Type = DbType.Int32 };
            Common.ImplementBusinessLogic(
                () =>
                {
                    DataAccessFactory.
                            Instance.
                            ExecuteProcedure("[CPSyncV].[InsertReferenceChartPreset]",
                                             new List<Parameters>()
                                                 {
                                                 new Parameters() {ParameterName = "@ReferenceChartTypeID", Type = DbType.Int32, Value = ReferenceChartTypeID},
                                                 new Parameters() {ParameterName = "@MetricTargetPresetIDXML", Value = ReferenceChartPresetsXML.ToString()},
                                                 },
                                             new List<Parameters>()
                                                 {
                                                 outParam
                                                 });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertReferenceChartPreset"));
            return Convert.ToInt32(outParam.Value);
        }

        /// <summary>
        /// GetGetReferenceChartPreset
        /// </summary>
        List<int> IReferenceChartPresetBusiness.GetReferenceChartPreset(int ReferenceChartTypeID)
        {
            List<int> dstReferenceChartTypeList = new List<int>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsRegion = DataAccessFactory.Instance.GetDsResult("[CPSyncV].[GetReferenceChartPreset]", new object[] { ReferenceChartTypeID });
                    if (dsRegion != null && dsRegion.Tables.Count > 0)
                    {
                        dstReferenceChartTypeList = (dsRegion.Tables[0].AsEnumerable().Select(
                            ReferenceChartType =>
                            DataReader.ReadInt(ReferenceChartType, "MetricTargetPresetID")
                            )).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetReferenceChartPreset"));
            return dstReferenceChartTypeList;
        }
    }
}
