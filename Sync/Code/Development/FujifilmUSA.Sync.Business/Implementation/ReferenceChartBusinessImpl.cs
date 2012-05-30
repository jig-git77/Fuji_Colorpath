using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.DataAccess;
using System.Data;
using System.Xml.Linq;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Business.ProfilerService;
using FujifilmUSA.Sync.Business.CustomExceptions;

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
                                                                                  new XAttribute("CMYK_K", x.CmykK)
                                                                         )).ToList());

                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure("[CP].[CreateReferenceChart]", new object[]
                                                                                {
                                                                                    referenceChartType.ChartName,
                                                                                    referenceChartType.NumOfRows,
                                                                                    referenceChartType.NumOfCols,
                                                                                    referenceChartType.InstrumentTypeId,
                                                                                    referenceChartType.ProjectTypeId,
                                                                                    referenceChartType.ChartPreview,
                                                                                    referenceChartType.OriginalChart,
                                                                                    referenceChartType.IsUsed,
                                                                                    referenceChartData.ToString()
                                                                                });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateReferenceChart"));
            return returnValue;
        }

        public List<ReferenceChart> GetReferenceChartForLabCalculation(short sourceProfileId, short referenceChartTypeId)
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

        public int InsertReferenceLabData(List<ReferenceChart> referenceCharts, byte[] sourceProfile, short sourceProfileId)
        {
            var returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    //call service and retrieve lab data. insert this lab data to database.
                    var referenceLabs = new List<ReferenceLab>();
                    using (var client = new ProfilerClient())
                    {
                        var response = client.ExtractProfileValues(
                            new ExtractProfileValuesRequest()
                            {
                                ExtractType = ProfileExtractType.Xyz,
                                Profile = new IccProfile() { ProfileData = sourceProfile },
                                C_X = referenceCharts.Select(x => Convert.ToDouble(x.CmykC)).ToList(),
                                M_Y = referenceCharts.Select(x => Convert.ToDouble(x.CmykM)).ToList(),
                                Y_Z = referenceCharts.Select(x => Convert.ToDouble(x.CmykY)).ToList(),
                                K = referenceCharts.Select(x => Convert.ToDouble(x.CmykK)).ToList()
                            });
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
                                                                                        referenceLabData.ToString()
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
                                ProjectType = new ProjectType()
                                {
                                    ProjectTypeId = DataReader.ReadShort(x, "ProjectTypeId"),
                                    ProjectTypeName = DataReader.ReadString(x, "ProjectType")
                                },
                                IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                                ChartPreview = DataReader.ReadByteArray(x, "ChartPreview"),
                                OriginalChart = DataReader.ReadByteArray(x, "OriginalChart")    

                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetReferenecChartInformation"));
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
                                         referenceChartType.ProjectTypeId,
                                        referenceChartType.IsUsed,
                                        referenceChartType.ChartPreview,
                                        referenceChartType.OriginalChart
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertReferenceChart" };
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateReferenceChart"));
            return returnValue;
        }

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
                                ChartPreview = DataReader.ReadByteArray(x,"ChartPreview"),
                                OriginalChart = DataReader.ReadByteArray(x,"OriginalChart"),
                                InstrumentType = new InstrumentType()
                                {
                                    InstrumentTypeId = DataReader.ReadShort(x, "InstrumentTypeID"),
                                    InstrumentName = DataReader.ReadString(x, "InstrumentName")
                                },
                                ProjectType = new ProjectType()
                                {
                                    ProjectTypeName = DataReader.ReadString(x, "ProjectType")
                                },
                                IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "GetReferenceChartsForXmf"));
            return referenceCharts;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
