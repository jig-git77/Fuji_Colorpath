using System.Collections.Generic;
using System.Linq;
using System.Data;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System;
using System.Security.Cryptography;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ReferenceChartBusinessImpl : IReferenceChartBusiness
    {
        #region IReferenceChartTypeBusiness Members

        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId(int measurementDeviceId)
        {
            var dstReferenceChartTypeList = new Dictionary<int, string>();
            var dsReferenceChartTypeList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetReferenceChartTypeByMeasurementDeviceID]", new object[] { measurementDeviceId });

            if (dsReferenceChartTypeList != null && dsReferenceChartTypeList.Tables.Count > 0)
            {
                dstReferenceChartTypeList = (dsReferenceChartTypeList.Tables[0].AsEnumerable().
                    Select(referenceChartType => new
                                                     {
                                                         Value = DsReader.ReadInt(referenceChartType, "ReferenceChartTypeID"),
                                                         Text = DsReader.ReadString(referenceChartType, "ChartName")
                                                     })).ToDictionary(referenceChartType => referenceChartType.Value, referenceChartType => referenceChartType.Text);

            }
            return dstReferenceChartTypeList;
        }

        public ReferenceChartData GetReferenceChartData(int projectId)
        {
            var chartData = new ReferenceChartData();
            var dsReferenceChart = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetReferenceChart]", new object[] { projectId });

            if (dsReferenceChart.Tables[0].Rows.Count == 0)
                return chartData;

            chartData.ChartName = DsReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "ChartName");
            chartData.MeasurementDeviceTypeId = DsReader.ReadInt(dsReferenceChart.Tables[0].Rows[0], "MeasurementDeviceTypeID");
            chartData.NumOfCols = DsReader.ReadInt(dsReferenceChart.Tables[0].Rows[0], "NumOfCols");
            chartData.NumOfRows = DsReader.ReadInt(dsReferenceChart.Tables[0].Rows[0], "NumOfRows");
            chartData.TotalPatches = DsReader.ReadInt(dsReferenceChart.Tables[0].Rows[0], "TotalPatches");
            chartData.MeasureDeviceSrNo = DsReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "MeasureDeviceSrNo");
            //Apply security on device serial no.
            chartData.ActivationKey =
                Utilities.GetMd5Hash(DateTime.Now.Year + DateTime.Now.DayOfYear + "+yg92s+" + chartData.MeasureDeviceSrNo);
            chartData.ChartPatches = dsReferenceChart.Tables[1].AsEnumerable().
                Select(x =>
                       new ReferenceChart()
                           {
                               ReferenceChartTypeId = DsReader.ReadShort(x, "ReferenceChartTypeID"),
                               SampleName = DsReader.ReadString(x, "SampleName"),
                               CmykC = DsReader.ReadDecimal(x, "CMYK_C"),
                               CmykM = DsReader.ReadDecimal(x, "CMYK_M"),
                               CmykY = DsReader.ReadDecimal(x, "CMYK_Y"),
                               CmykK = DsReader.ReadDecimal(x, "CMYK_K"),
                               PatchTypeId = DsReader.ReadShort(x, "PatchTypeID"),
                               HtmlColor = DsReader.ReadString(x, "HTMLColor")
                           }).ToList();
            if (chartData.MeasurementDeviceTypeId > System.Convert.ToInt32(EMeasurementDeviceType.I1Pro))
            {
                var reader = DataAccessFactory.Instance.GetXmlResult("CPSync4D.uspGetChartSettings", new object[] { chartData.ReferenceChartTypeId });
                var dsChartSettings = new DataSet();
                dsChartSettings.ReadXml(reader);
                if (dsChartSettings.Tables.Count > 0)
                {

                    if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeX"))
                        chartData.ChartSetting.ChartSizeX = DsReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeX");
                    if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeY"))
                        chartData.ChartSetting.ChartSizeY = DsReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeY");
                    if (dsChartSettings.Tables[0].Columns.Contains("BarSizeX"))
                        chartData.ChartSetting.BarSizeX = DsReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeX");
                    if (dsChartSettings.Tables[0].Columns.Contains("BarSizeY"))
                        chartData.ChartSetting.BarSizeY = DsReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeY");
                    if (dsChartSettings.Tables[0].Columns.Contains("MarkerDistance"))
                        chartData.ChartSetting.MarkerDistance = DsReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "MarkerDistance");
                    if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeX"))
                        chartData.ChartSetting.PatchSizeX = DsReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeX");
                    if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeY"))
                        chartData.ChartSetting.PatchSizeY = DsReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeY");
                    if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstLineY"))
                        chartData.ChartSetting.DistToFirstLineY = DsReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstLineY");
                    if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstField"))
                        chartData.ChartSetting.DistToFirstField = DsReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstField");
                    if (dsChartSettings.Tables[0].Columns.Contains("DistToBar"))
                        chartData.ChartSetting.DistToBar = DsReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToBar");
                }
            }
            return chartData;
        }

        #endregion
    }
}