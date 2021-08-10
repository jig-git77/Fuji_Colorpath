using FujifilmUSA.Sync.Business.CustomExceptions;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business.ProfilerServiceClient;
using FujifilmUSA.Sync.Business.ProfilerServiceClient.Request;
using FujifilmUSA.Sync.Business.ProfilerServiceClient.Response;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ReferenceChartBusinessImpl : IReferenceChartBusiness
    {
        #region IReferenceChartBusiness Members

        public int CreateReferenceChart(ReferenceChartType referenceChartType)
        {
            var returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    var referenceChartData = new XElement("ReferenceChartData",
                                                          referenceChartType.ReferenceCharts.
                                                              Select(x =>
                                                                     new XElement("ReferenceChart",
                                                                                  new XAttribute("SampleID", x.SampleId),
                                                                                  new XAttribute("SampleName", x.SampleName),
                                                                                  new XAttribute("CMYK_C", x.CmykC),
                                                                                  new XAttribute("CMYK_M", x.CmykM),
                                                                                  new XAttribute("CMYK_Y", x.CmykY),
                                                                                  new XAttribute("CMYK_K", x.CmykK),
                                                                                  new XAttribute("HTMLColor", x.HtmlColor),
                                                                                  new XAttribute("Channel_5", x.Channel_5),
                                                                                  new XAttribute("Channel_6", x.Channel_6),
                                                                                  new XAttribute("Channel_7", x.Channel_7),
                                                                                  new XAttribute("Channel_8", x.Channel_8),
                                                                                  new XAttribute("Channel_9", x.Channel_9)
                                                                         )).ToList());

                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure("[CP].[CreateReferenceChart]", new object[]
                                                                                {
                                                                                    referenceChartType.ChartName,
                                                                                    referenceChartType.NumOfRows,
                                                                                    referenceChartType.NumOfCols,
                                                                                    referenceChartType.InstrumentTypeId,
                                                                                    referenceChartType.ChartPreview,
                                                                                    referenceChartType.OriginalChart,
                                                                                    referenceChartType.IsUsed,
                                                                                    referenceChartData.ToString(),
                                                                                    referenceChartType.IsSync1D_ISO,
                                                                                    referenceChartType.IsSync1D_G7,
                                                                                    referenceChartType.Validation_1D,
                                                                                    referenceChartType.IsSync4D,
                                                                                    referenceChartType.Validation_4D ,
                                                                                    referenceChartType.IsSync4D_Plus,
                                                                                    referenceChartType.Validation_4DPlus,
                                                                                    referenceChartType.IsDLPMultiColor,
                                                                                    referenceChartType.Validation_4D_Mutli_Color,
                                                                                    referenceChartType.IsSupportMulticolor,
                                                                                    referenceChartType.TotalNoColor,
                                                                                    referenceChartType.Channel_5,
                                                                                    referenceChartType.Channel_6,
                                                                                    referenceChartType.Channel_7,
                                                                                    referenceChartType.Channel_8,
                                                                                    referenceChartType.Channel_9,
                                                                                    referenceChartType.ChartSettingXML
                                                                                });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertReferenceChart" };
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateReferenceChart"));
            return returnValue;
        }

        public List<ReferenceChart> GetReferenceChartForLabCalculation(int sourceProfileId, short referenceChartTypeId)
        {
            var referenceChartData = new List<ReferenceChart>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsResult = DataAccessFactory.
                        Instance.
                        GetDsResult("[CP].[GetReferenceChartForLabCalculation]", new object[]
                                                                                         {
                                                                                             sourceProfileId,
                                                                                             referenceChartTypeId
                                                                                         });
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        referenceChartData = dsResult.Tables[0].
                            AsEnumerable().
                            Select(x => new ReferenceChart()
                            {
                                ReferenceChartId = DataReader.ReadInt(x, "ReferenceChartID"),
                                CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                                CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                                CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                                CmykK = DataReader.ReadDecimal(x, "CMYK_K")
                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetReferenceChartForLabCalculation"));
            return referenceChartData;
        }

        public int InsertReferenceLabData(List<ReferenceChart> referenceCharts, byte[] sourceProfile, short sourceProfileId, int CompanyId, int ProjectId)
        {
            var returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    //call service and retrieve lab data. insert this lab data to database.
                    var referenceLabs = new List<ReferenceLab>();
                    ExtractProfileValuesRequest extractProfileValuesRequest = new ExtractProfileValuesRequest()
                    {
                        ExtractType = Enums.ProfileExtractType.Xyz,
                        Profile = new IccProfile() { ProfileData = sourceProfile },
                        C_X = referenceCharts.Select(x => Convert.ToDouble(x.CmykC)).ToList(),
                        M_Y = referenceCharts.Select(x => Convert.ToDouble(x.CmykM)).ToList(),
                        Y_Z = referenceCharts.Select(x => Convert.ToDouble(x.CmykY)).ToList(),
                        K = referenceCharts.Select(x => Convert.ToDouble(x.CmykK)).ToList()
                    };
                    //var response = client.ExtractProfileValues(extractProfileValuesRequest);
                    var response = Utilities.GetProfilerServiceClientCall<ExtractProfileValuesRequest, ExtractProfileValuesResponse>(extractProfileValuesRequest, Utilities.ProfilerCallMethod.ExtractProfile, Utilities.ProfilerRequestCall.HttpPost);
                    for (int i = 0; i < referenceCharts.Count; i++)
                    {
                        referenceLabs.Add(new ReferenceLab()
                        {
                            ReferenceChartId = referenceCharts[i].ReferenceChartId,
                            SourceProfileId = sourceProfileId,
                            TargetL = Convert.ToDecimal(response.C_X[i]),
                            Targeta = Convert.ToDecimal(response.M_Y[i]),
                            Targetb = Convert.ToDecimal(response.Y_Z[i]),
                        });
                    }


                    var referenceLabData = new XElement("ReferenceLabData",
                                                        referenceLabs.
                                                            Select(x =>
                                                                   new XElement("ReferenceLab",
                                                                                new XAttribute("SourceProfileId", x.SourceProfileId),
                                                                                new XAttribute("ReferenceChartId", x.ReferenceChartId),
                                                                                new XAttribute("TargetL", x.TargetL),
                                                                                new XAttribute("TargetA", x.Targeta),
                                                                                new XAttribute("TargetB", x.Targetb)
                                                                       )).ToList());

                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure("CPSync4D.CreateReferenceLabData", new object[]
                                                                                    {
                                                                                        referenceLabData.ToString(),
                                                                                        sourceProfileId,
                                                                                        CompanyId,
                                                                                         ProjectId
                                                                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertReferenceLabData"));
            return returnValue;
        }

        public List<ReferenceChartType> GetReferenceCharts(int RefChartID)
        {
            var ReferenceCharts = new List<ReferenceChartType>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsReferenceChart = DataAccessFactory.Instance.GetDsResult("[CP].[GetReferenceChartList]", new object[] { RefChartID });
                    if (dsReferenceChart != null && dsReferenceChart.Tables.Count > 0 && dsReferenceChart.Tables[0].Rows.Count > 0)
                    {
                        ReferenceCharts = dsReferenceChart.Tables[0].AsEnumerable().
                            Select(x => new ReferenceChartType()
                            {
                                ReferenceChartTypeId = DataReader.ReadShort(x, "ReferenceChartTypeId"),
                                ChartName = DataReader.ReadString(x, "ChartName"),
                                NumOfRows = DataReader.ReadInt(x, "NumOfRows"),
                                NumOfCols = DataReader.ReadInt(x, "NumOfCols"),
                                TotalPatches = DataReader.ReadInt(x, "TotalPatches"),
                                InstrumentType = new InstrumentType()
                                {
                                    InstrumentTypeId = DataReader.ReadShort(x, "InstrumentTypeID"),
                                    InstrumentName = DataReader.ReadString(x, "InstrumentName")
                                },
                                IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                                ChartPreview = DataReader.ReadByteArray(x, "ChartPreview"),
                                OriginalChart = DataReader.ReadByteArray(x, "OriginalChart"),
                                IsSync1D_ISO = DataReader.ReadShort(x, "IsSync1D_ISO"),
                                IsSync1D_G7 = DataReader.ReadShort(x, "IsSync1D_G7"),
                                Validation_1D = DataReader.ReadShort(x, "Validation_1D"),
                                IsSync4D = DataReader.ReadShort(x, "IsSync4D"),
                                Validation_4D = DataReader.ReadShort(x, "Validation_4D"),
                                ChartSettingXML = DataReader.ReadString(x, "ChartSetting"),
                                IsSync4D_Plus = DataReader.ReadShort(x, "IsSync4D_Plus"),
                                Validation_4DPlus = DataReader.ReadShort(x, "Validation_4DPlus"),
                                IsDLPMultiColor = DataReader.ReadShort(x, "IsDLPMultiColor"),
                                Validation_4D_Mutli_Color = DataReader.ReadShort(x, "Validation_4D_Mutli_Color"),
                                IsSupportMulticolor = DataReader.ReadBoolean(x, "IsSupportMulticolor"),
                                TotalNoColor = DataReader.ReadShort(x, "TotalNoColor"),
                                Channel_5 = DataReader.ReadShort(x, "Channel_5"),
                                Channel_6 = DataReader.ReadShort(x, "Channel_6"),
                                Channel_7 = DataReader.ReadShort(x, "Channel_7"),
                                Channel_8 = DataReader.ReadShort(x, "Channel_8"),
                                Channel_9 = DataReader.ReadShort(x, "Channel_9")

                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetReferenceCharts"));
            return ReferenceCharts;
        }

        public int UpdateReferenceChart(ReferenceChartType referenceChartType)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdateReferenceChart]",
                            new object[]
                                    {
                                        referenceChartType.ReferenceChartTypeId,
                                        referenceChartType.ChartName,
                                        referenceChartType.NumOfRows,
                                        referenceChartType.NumOfCols,
                                        referenceChartType.InstrumentTypeId,
                                        referenceChartType.IsUsed,
                                        referenceChartType.ChartPreview,
                                        referenceChartType.OriginalChart,
                                        referenceChartType.IsSync1D_ISO,
                                        referenceChartType.IsSync1D_G7,
                                        referenceChartType.Validation_1D,
                                        referenceChartType.IsSync4D,
                                        referenceChartType.Validation_4D,
                                        referenceChartType.IsSync4D_Plus,
                                        referenceChartType.Validation_4DPlus,
                                        referenceChartType.IsDLPMultiColor,
                                        referenceChartType.Validation_4D_Mutli_Color,
                                        referenceChartType.IsSupportMulticolor,
                                        referenceChartType.TotalNoColor,
                                        referenceChartType.Channel_5,
                                        referenceChartType.Channel_6,
                                        referenceChartType.Channel_7,
                                        referenceChartType.Channel_8,
                                        referenceChartType.Channel_9,
                                        referenceChartType.ChartSettingXML
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_UpdateReferenceChart" };
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateReferenceChart"));
            return returnValue;
        }

        /// <summary>
        /// Provide list of reference chart without preview and original byte[] data...
        /// </summary>
        /// <returns></returns>
        public List<ReferenceChartType> GetReferenceChartsForXmf()
        {
            var referenceCharts = new List<ReferenceChartType>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsReferenceChart = DataAccessFactory.Instance.GetDsResult("[CP].[GetReferenceChartListForXmf]");
                    if (dsReferenceChart != null && dsReferenceChart.Tables.Count > 0 && dsReferenceChart.Tables[0].Rows.Count > 0)
                    {
                        referenceCharts = dsReferenceChart.Tables[0].AsEnumerable().
                            Select(x => new ReferenceChartType()
                            {
                                ReferenceChartTypeId = DataReader.ReadShort(x, "ReferenceChartTypeId"),
                                ChartName = DataReader.ReadString(x, "ChartName"),
                                NumOfRows = DataReader.ReadInt(x, "NumOfRows"),
                                NumOfCols = DataReader.ReadInt(x, "NumOfCols"),
                                TotalPatches = DataReader.ReadInt(x, "TotalPatches"),
                                InstrumentType = new InstrumentType()
                                {
                                    InstrumentTypeId = DataReader.ReadShort(x, "InstrumentTypeID"),
                                    InstrumentName = DataReader.ReadString(x, "InstrumentName")
                                },
                                ProjectType = new ProjectType()
                                {
                                    ProjectTypeId = DataReader.ReadShort(x, "ProjectTypeId"),
                                    ProjectTypeName = DataReader.ReadString(x, "ProjectType")
                                },
                                IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                                ChartDisplayType = DataReader.ReadInt(x, "ChartDisplayType")
                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetReferenceChartsForXmf"));
            return referenceCharts;
        }

        /// <summary>
        /// Provide preview and original data for single reference chart based on chartid...
        /// </summary>
        /// <returns></returns>
        public ReferenceChartType GetReferenceChartDataForXmf(int refChartId)
        {
            var referenceChart = new ReferenceChartType();
            Common.ImplementBusinessLogic(
                () =>
                    {
                        var dsReferenceChart = DataAccessFactory.Instance.GetDsResult("[CP].[GetReferenceChartDataForXmf]", new object[] { refChartId });
                        if (dsReferenceChart != null && dsReferenceChart.Tables.Count > 0 && dsReferenceChart.Tables[0].Rows.Count > 0)
                        {
                            referenceChart.ReferenceChartTypeId = DataReader.ReadShort(dsReferenceChart.Tables[0].Rows[0], "ReferenceChartTypeID");
                            referenceChart.ChartPreview = DataReader.ReadByteArray(dsReferenceChart.Tables[0].Rows[0], "ChartPreview");
                            referenceChart.OriginalChart = DataReader.ReadByteArray(dsReferenceChart.Tables[0].Rows[0], "OriginalChart");
                        }
                    },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetReferenceChartsForXmf"));
            return referenceChart;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
