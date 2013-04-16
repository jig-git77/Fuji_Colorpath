using System;
using System.Linq;
using System.Data;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business.Messages;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using System.Xml.Linq;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MeasurementBusinessImpl : IMeasurementBusiness
    {
        #region IMeasurementBusiness Members

        public MeasurementDataResponse GetMeasurementData(List<int> measurementIds)
        {
            var response = new MeasurementDataResponse()
                               {
                                   DcsData = new List<double>(),
                                   PcsData = new List<double>()
                               };
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsPcsDcsData = DataAccessFactory.
                        Instance.
                        GetDsResult(
                            "CPSync4D.GetMeasurementData",
                            new object[]
                                    {
                                        String.Join(",", measurementIds)
                                    });
                    if (dsPcsDcsData.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dsPcsDcsData.Tables[0].Rows)
                        {
                            response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_X"));
                            response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_Y"));
                            response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_Z"));

                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_C"));
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_M"));
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_Y"));
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_K"));
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementData"));
            return response;
        }

        public int SaveMeasurement(Measurement measurement)
        {
            var outParam = new Parameters() { ParameterName = "@MeasurementID", Type = DbType.Int32 };
            Common.ImplementBusinessLogic(
                () =>
                {
                    var measurementPatches = new XElement("MeasurementPatches",
                                                          measurement.MeasurementPatches.
                                                              Select(x =>
                                                                     new XElement("MeasurementPatch",
                                                                                  new XAttribute("SampleName", x.SampleName),
                                                                                  new XAttribute("CIEXYZ_X", x.CiexyzX),
                                                                                  new XAttribute("CIEXYZ_Y", x.CiexyzY),
                                                                                  new XAttribute("CIEXYZ_Z", x.CiexyzZ),
                                                                                  new XAttribute("CIELAB_L", x.CielabL),
                                                                                  new XAttribute("CIELAB_a", x.CielabA),
                                                                                  new XAttribute("CIELAB_b", x.CielabB),
                                                                                  new XAttribute("Density_C", x.Density.Red),
                                                                                  new XAttribute("Density_M", x.Density.Green),
                                                                                  new XAttribute("Density_Y", x.Density.Blue),
                                                                                  new XAttribute("Density_V", x.Density.Visual)
                                                                         )).ToList());

                    DataAccessFactory.
                        Instance.
                        ExecuteProcedure("CPSync4D.CreateMeasurement",
                                         new List<Parameters>()
                                                 {
                                                     new Parameters() {ParameterName = "@MeasurementName", Size = 50, Type = DbType.String, Value = measurement.MeasurementName},
                                                     new Parameters() {ParameterName = "@OptimizeRunID", Type = DbType.Int32, Value = measurement.OptimizeRunId},
                                                     new Parameters() {ParameterName = "@MeasurementDate", Type = DbType.DateTime, Value = measurement.MeasurementDate},
                                                     new Parameters() {ParameterName = "@ReferenceChartTypeID", Type = DbType.Int16, Value = measurement.ReferenceChartTypeId},
                                                     new Parameters() {ParameterName = "@MeasurementPatches", Value = measurementPatches.ToString()},
                                                 },
                                         new List<Parameters>()
                                                 {
                                                     outParam
                                                 });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_SaveMeasurement"));
            return Convert.ToInt32(outParam.Value);
        }

        public List<Measurement> GetMeasurementsOnRunId(int optimizeRunId)
        {
            var measurements = new List<Measurement>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "CPSync4D.GetMeasurementByOptimizeRunID",
                                new object[] { optimizeRunId });
                    measurements = dsMeasurements.
                        Tables[0].
                        AsEnumerable().
                        Select(x => new Measurement()
                                        {
                                            MeasurementId = DataReader.ReadInt(x, "MeasurementID"),
                                            MeasurementName = DataReader.ReadString(x, "MeasurementName"),
                                            MeasurementDate = DataReader.ReadDate(x, "MeasurementDate"),
                                            UploadDate = DataReader.ReadString(x, "MeasurementDate"),
                                            OptimizeRunId = DataReader.ReadInt(x, "OptimizeRunID"),
                                            IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementsOnRunId"));
            return measurements;
        }

        public List<Measurement> GetAnalyzeChartMeasurement(int optimizeRunId, string From_ΔE, bool IsSync1DType = false)
        {
            var measurements = new List<Measurement>();

            string storedProc = "[CPSync4D].[GetAnalyzeChartMeasurement]";
            if (IsSync1DType)
                storedProc = "[CPSync].[GetAnalyzeChartMeasurement]";

            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[]
                                        {
                                            optimizeRunId, From_ΔE
                                        });
                    measurements =
                        dsMeasurements.
                            Tables[0].
                            AsEnumerable().
                            Select(x => new Measurement()
                                            {
                                                MeasurementId = DataReader.ReadInt(x, "MeasurementID"),
                                                MeasurementName = DataReader.ReadString(x, "MeasurementName"),
                                                IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                                            }).ToList();

                    measurements.ForEach(x =>
                                             {
                                                 x.MeasurementPatches =
                                                     dsMeasurements.Tables[1].AsEnumerable().
                                                         Where(y => DataReader.ReadInt(y, "MeasurementID") == x.MeasurementId).
                                                         Select(y => new MeasurementPatch()
                                                                         {
                                                                             MeasurementId = DataReader.ReadInt(y, "MeasurementID"),
                                                                             MeasurementPatchId = DataReader.ReadShort(y, "PatchTypeID"),
                                                                             HtmlColor = DataReader.ReadString(y, "HtmlColor"),
                                                                             Δe2000 = DataReader.ReadDecimal(y, "ΔE2000"),
                                                                         }).ToList();
                                             });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetAnalyzeChartMeasurement"));
            return measurements;
        }

        public List<Measurement> GetAnalyzeChartVMeasurement(int projectId, string From_ΔE)
        {
            var measurements = new List<Measurement>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "[CPSyncV].[GetAnalyzeChartVMeasurement]",
                                new object[]
                                        {
                                            projectId, From_ΔE
                                        });
                    measurements =
                        dsMeasurements.
                            Tables[0].
                            AsEnumerable().
                            Select(x => new Measurement()
                            {
                                MeasurementId = DataReader.ReadInt(x, "MeasurementID"),
                                MeasurementName = DataReader.ReadString(x, "MeasurementName"),
                                MeasurementDate = DataReader.ReadDate(x, "MeasurementDate"),
                                IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                                dEMethod = DataReader.ReadString(x, "dEMethod"),
                                MeasurementStatus = DataReader.ReadString(x, "MeasurementStatus")
                            }).ToList();

                    measurements.ForEach(x =>
                    {
                        x.MeasurementPatches =
                            dsMeasurements.Tables[1].AsEnumerable().
                                Where(y => DataReader.ReadInt(y, "MeasurementID") == x.MeasurementId).
                                Select(y => new MeasurementPatch()
                                {
                                    MeasurementId = DataReader.ReadInt(y, "MeasurementID"),
                                    MeasurementPatchId = DataReader.ReadShort(y, "PatchTypeID"),
                                    HtmlColor = DataReader.ReadString(y, "HtmlColor"),
                                    Δe2000 = DataReader.ReadDecimal(y, "ΔE2000")                                   
                                }).ToList();
                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetAnalyzeChartMeasurement"));
            return measurements;
        }

        public int UpdateMeasurementByAnalyzeChart(Measurement measurement, bool IsVerification = false)
        {
            int returnValue = 0;
            string storedProc = "[CPSync4D].[UpdateMeasurementByAnalyzeChart]";
            if (IsVerification)
                storedProc = "[CPSyncV].[UpdateVMeasurementByAnalyzeChart]";
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue =
                        DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                storedProc,
                                new object[]
                                        {
                                            measurement.MeasurementId,
                                            measurement.IsUsed
                                        });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateMeasurementByAnalyzeChart"));
            return returnValue;

        }

        public List<MeasurementPatch_AvgDelta> GetAvgDeltaMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false)
        {
            string storedProc = "CPSync4D.GetMeasurementDeltasByMeasurementID";
            if (IsVerification)
                storedProc = "CPSyncV.GetVMeasurementDeltasByMeasurementID";
            var measurements = new List<MeasurementPatch_AvgDelta>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId, From_ΔE });
                    measurements = dsMeasurements.
                        Tables[0].
                        AsEnumerable().
                        Select(x => new MeasurementPatch_AvgDelta()
                        {
                            MeasurementId = DataReader.ReadInt(x, "MeasurementID"),
                            MeasurementName = DataReader.ReadString(x, "MeasurementName"),
                            MeasurementDate = DataReader.ReadDate(x, "MeasurementDate"),
                            Avg_ΔE = DataReader.ReadDecimal(x, "Avg_ΔE"),
                            Max_ΔE = DataReader.ReadDecimal(x, "Max_ΔE"),
                            Pri_ΔE = DataReader.ReadDecimal(x, "PriΔE"),
                            Pri_ΔH = DataReader.ReadDecimal(x, "PriΔH"),
                            Gray_ΔE = DataReader.ReadDecimal(x, "GrayΔE"),
                            PercΔE_10 = DataReader.ReadDecimal(x, "TenΔE"),
                            PercΔE_90 = DataReader.ReadDecimal(x, "NinetyΔE"),
                            PercΔE_95 = DataReader.ReadDecimal(x, "NinetyFiveΔE")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetAvgDeltaMeasurement"));
            return measurements;
        }

        public List<Measurement> GetVMeasurementSummary(int MeasurementId, string From_ΔE, bool IsVerification = false)
        {
            var VmeasurementSummary = new List<Measurement>();
            string storedProc = "CPSyncV.GetVMeasurementDeltasByMeasurementID";
          
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId, From_ΔE });
                    if (dsMeasurements.Tables[0].Rows.Count > 0)
                    {
                        VmeasurementSummary = dsMeasurements.Tables[0].AsEnumerable().
                          Select(x => new Measurement()
                          {
                              MeasurementId = DataReader.ReadInt(x, "MeasurementID"),
                              MeasurementName = DataReader.ReadString(x, "MeasurementName"),
                              MeasurementDate = DataReader.ReadDate(x, "MeasurementDate"),
                              Customer = DataReader.ReadString(x, "Customer"),
                              Notes = DataReader.ReadString(x, "Notes"),
                              dEMethod = DataReader.ReadString(x, "dEMethod"),
                              MeasurementStatus = DataReader.ReadString(x, "MeasurementStatus"),
                              MetricTargetPresetName = DataReader.ReadString(x, "MetricTargetPresetName"),
                              ValidationType = DataReader.ReadInt(x, "ValidationType"),
                              PrintStandardType = DataReader.ReadString(x, "PrintStandard")
                          }).ToList();
                        VmeasurementSummary[0].ValidationMetrics = dsMeasurements.Tables[1].AsEnumerable().
                           Select(x => new ValidationMetrics()
                           {
                               MetricMeasuredID = DataReader.ReadInt(x, "MetricMeasuredID"),
                               MetricSumID = DataReader.ReadInt(x, "MetricSumID"),
                               Metric = DataReader.ReadString(x, "MetricSumName"),
                               Tolerance = DataReader.ReadDecimal(x, "TargetValue"),
                               Actual = DataReader.ReadDecimal(x, "MeasuredValue"),
                               IsNotOK = DataReader.ReadBoolean(x, "IsNotOK")
                           }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetAvgDeltaMeasurement"));
            return VmeasurementSummary;
        }

        public DataTable GetPatchDetailsofMeasurement(int MeasurementId, bool IsVerification = false)
        {
            var PatchDetails = new List<MeasurementPatchDetails>();
            string storedProc = "CPSync4D.GetPatchDetailsByMeasurementID";
            if (IsVerification)
                storedProc = "CPSyncV.GetVPatchDetailsByMeasurementID";
            DataTable dtPatchDetails = new DataTable();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });
                    if (dsMeasurements.Tables.Count > 0)
                        dtPatchDetails = dsMeasurements.Tables[0];
                    //PatchDetails = dsMeasurements.
                    //    Tables[0].
                    //    AsEnumerable().
                    //    Select(x => new MeasurementPatchDetails()
                    //    {
                    //        SampleId = DataReader.ReadInt(x, "SampleID"),
                    //        HtmlColor = DataReader.ReadString(x, "HTMLColor"),
                    //        CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                    //        CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                    //        CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                    //        CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                    //        TargetL = DataReader.ReadDecimal(x, "TargetL"),
                    //        Targeta = DataReader.ReadDecimal(x, "Targeta"),
                    //        Targetb = DataReader.ReadDecimal(x, "Targetb"),
                    //        CielabL = DataReader.ReadDecimal(x, "CIELAB_L"),
                    //        CielabA = DataReader.ReadDecimal(x, "CIELAB_a"),
                    //        CielabB = DataReader.ReadDecimal(x, "CIELAB_b"),
                    //        ΔE = DataReader.ReadDecimal(x, "ΔE"),
                    //        ΔE94 = DataReader.ReadDecimal(x, "ΔE94"),
                    //        ΔE2000 = DataReader.ReadDecimal(x, "ΔE2000")
                    //    }).ToList();

                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetPatchDetailsofMeasurement"));
            return dtPatchDetails;
        }

        public List<AnalyzeChart> GetDeltaEPatchChartData(int MeasurementId, string From_ΔE, bool IsVerification = false)
        {
            var DeltaEPatchDetails = new List<AnalyzeChart>();
            string storedProc = "CPSync4D.GetChartData_InkPaper";
            if (IsVerification)
                storedProc = "CPSyncV.GetVChartData_InkPaper";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId, From_ΔE });
                    DeltaEPatchDetails = dsMeasurements.Tables[0].
                         AsEnumerable().
                        Select(x => new AnalyzeChart()
                        {
                            HtmlColor = DataReader.ReadString(x, "HtmlColor"),
                            PatchTypeId = DataReader.ReadShort(x, "PatchTypeId"),
                            ΔE = DataReader.ReadDecimal(x, "ΔE"),
                            ΔELimit = DataReader.ReadDecimal(x, "ΔELimit"),
                            CielabL = DataReader.ReadDecimal(x, "CIELAB_L"),
                            CielabA = DataReader.ReadDecimal(x, "CIELAB_a"),
                            CielabB = DataReader.ReadDecimal(x, "CIELAB_b"),
                            TargetL = DataReader.ReadDecimal(x, "TargetL"),
                            Targeta = DataReader.ReadDecimal(x, "Targeta"),
                            Targetb = DataReader.ReadDecimal(x, "Targetb")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_InkPaper_EPrimaryPatches"));
            return DeltaEPatchDetails;
        }

        public List<AnalyzeChart> GetInkPaperChartData(int MeasurementId, string From_ΔE, bool IsVerification = false)
        {
            var PaperPatchDetails = new List<AnalyzeChart>();
            string storedProc = "CPSync4D.GetChartData_InkPaper";
            if (IsVerification)
                storedProc = "CPSyncV.GetVChartData_InkPaper";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                               storedProc,
                                new object[] { MeasurementId, From_ΔE });
                    PaperPatchDetails = dsMeasurements.Tables[1].
                         AsEnumerable().
                        Select(x => new AnalyzeChart()
                        {
                            CielabL = DataReader.ReadDecimal(x, "CIELAB_L"),
                            CielabA = DataReader.ReadDecimal(x, "CIELAB_a"),
                            CielabB = DataReader.ReadDecimal(x, "CIELAB_b"),
                            TargetL = DataReader.ReadDecimal(x, "TargetL"),
                            Targeta = DataReader.ReadDecimal(x, "Targeta"),
                            Targetb = DataReader.ReadDecimal(x, "Targetb"),
                            ΔL = DataReader.ReadDecimal(x, "ΔL"),
                            Δa = DataReader.ReadDecimal(x, "Δa"),
                            Δb = DataReader.ReadDecimal(x, "Δb")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_InkPaper_PaperLAB"));
            return PaperPatchDetails;
        }

        public List<AnalyzeChart_Gray> GetGamutChart1(int MeasurementId, bool IsVerification = false)
        {
            var GamutChartDetails = new List<AnalyzeChart_Gray>();
            string storedProc = "CPSync4D.GetChartData_Gray";
            if (IsVerification)
                storedProc = "CPSyncV.GetVChartData_Gray";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });
                    GamutChartDetails = dsMeasurements.Tables[0].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_Gray()
                        {
                            HtmlColor = DataReader.ReadString(x, "HtmlColor"),
                            PatchType = DataReader.ReadString(x, "PatchType"),
                            CielabL = DataReader.ReadDecimal(x, "CIELAB_L"),
                            CielabA = DataReader.ReadDecimal(x, "CIELAB_a"),
                            CielabB = DataReader.ReadDecimal(x, "CIELAB_b"),
                            TargetL = DataReader.ReadDecimal(x, "TargetL"),
                            Targeta = DataReader.ReadDecimal(x, "Targeta"),
                            Targetb = DataReader.ReadDecimal(x, "Targetb"),
                            ΔL = DataReader.ReadDecimal(x, "ΔL"),
                            Δa = DataReader.ReadDecimal(x, "Δa"),
                            Δb = DataReader.ReadDecimal(x, "Δb"),
                            ΔE = DataReader.ReadDecimal(x, "ΔE")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_GrayGamut1"));
            return GamutChartDetails;
        }

        public List<AnalyzeChart_Gray> GetGamutChart2(int MeasurementId, bool IsVerification = false)
        {
            var GamutChartDetails = new List<AnalyzeChart_Gray>();
            string storedProc = "CPSync4D.GetChartData_Gray";
            if (IsVerification)
                storedProc = "CPSyncV.GetVChartData_Gray";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });
                    GamutChartDetails = dsMeasurements.Tables[1].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_Gray()
                        {
                            HtmlColor = DataReader.ReadString(x, "HtmlColor"),
                            PatchType = DataReader.ReadString(x, "PatchType"),
                            CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                            CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                            CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                            CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                            CielabL = DataReader.ReadDecimal(x, "CIELAB_L"),
                            CielabA = DataReader.ReadDecimal(x, "CIELAB_a"),
                            CielabB = DataReader.ReadDecimal(x, "CIELAB_b")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_GrayGamut2"));
            return GamutChartDetails;
        }

        public List<AnalyzeChart_Gray> GetLABChart(int MeasurementId, bool IsVerification = false)
        {
            var LABChartDetails = new List<AnalyzeChart_Gray>();
            string storedProc = "CPSync4D.GetChartData_Gray";
            if (IsVerification)
                storedProc = "CPSyncV.GetVChartData_Gray";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });
                    LABChartDetails = dsMeasurements.Tables[2].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_Gray()
                        {
                            CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                            CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                            CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                            CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                            CielabL = DataReader.ReadDecimal(x, "CIELAB_L"),
                            CielabA = DataReader.ReadDecimal(x, "CIELAB_a"),
                            CielabB = DataReader.ReadDecimal(x, "CIELAB_b"),
                            TargetL = DataReader.ReadDecimal(x, "TargetL"),
                            Targeta = DataReader.ReadDecimal(x, "Targeta"),
                            Targetb = DataReader.ReadDecimal(x, "Targetb"),
                            ΔL = DataReader.ReadDecimal(x, "ΔL"),
                            Δa = DataReader.ReadDecimal(x, "Δa"),
                            Δb = DataReader.ReadDecimal(x, "Δb"),
                            ΔE = DataReader.ReadDecimal(x, "ΔE")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_LABChart"));
            return LABChartDetails;
        }

        public DataSet GetReportDataOnMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false, int ValidationType = -1)
        {
            var measurements = new DataSet();
            string storedProc = "Report.GetSingleMeasurement";
            if (IsVerification)
                storedProc = "Report.GetSingleVMeasurement";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements = new DataSet();
                    if (!IsVerification)
                    {
                        dsMeasurements = DataAccessFactory.
                             Instance.
                             GetDsResult(
                                 storedProc,
                                 new object[] { MeasurementId, From_ΔE });
                    }
                    else
                    {
                        dsMeasurements = DataAccessFactory.
                             Instance.
                             GetDsResult(
                                 storedProc,
                                 new object[] { MeasurementId, From_ΔE, ValidationType });
                    }
                    measurements = dsMeasurements;
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_Report_GetSingleMeasurement"));
            return measurements;
        }

        public DataSet GetReportDataOnMeasurement_G7_Vali(int MeasurementId, string From_ΔE, bool IsVerification = false, int ValidationType = -1)
        {
            var measurements = new DataSet();
            string storedProc = "Report.GetSingleVMeasurement";          
        
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements = DataAccessFactory.
                             Instance.
                             GetDsResult(
                                 storedProc,
                                 new object[] { MeasurementId, From_ΔE, ValidationType });
                  
                    measurements = dsMeasurements;
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_Report_GetSingleMeasurement"));
            return measurements;
        }

        public List<Measurement> GetVMeasurementsOnProjectId(int projectId)
        {
            var measurements = new List<Measurement>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "CPSyncV.GetVMeasurementByProjectID",
                                new object[] { projectId });
                    measurements = dsMeasurements.
                        Tables[0].
                        AsEnumerable().
                        Select(x => new Measurement()
                                        {
                                            MeasurementId = DataReader.ReadInt(x, "MeasurementID"),
                                            MeasurementName = DataReader.ReadString(x, "MeasurementName"),
                                            MeasurementDate = DataReader.ReadDate(x, "MeasurementDate"),
                                            UploadDate = DataReader.ReadString(x, "MeasurementDate"),
                                            ProjectId = DataReader.ReadInt(x, "ProjectID"),
                                            IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementsOnRunId"));
            return measurements;
        }

        public int SaveVMeasurement(Measurement measurement)
        {
            var outParam = new Parameters() { ParameterName = "@MeasurementID", Type = DbType.Int32 };
            Common.ImplementBusinessLogic(
                () =>
                {
                    var measurementPatches = new XElement("MeasurementPatches",
                                                          measurement.MeasurementPatches.
                                                              Select(x =>
                                                                     new XElement("MeasurementPatch",
                                                                                  new XAttribute("SampleName", x.SampleName),
                                                                                  new XAttribute("CIEXYZ_X", x.CiexyzX),
                                                                                  new XAttribute("CIEXYZ_Y", x.CiexyzY),
                                                                                  new XAttribute("CIEXYZ_Z", x.CiexyzZ),
                                                                                  new XAttribute("CIELAB_L", x.CielabL),
                                                                                  new XAttribute("CIELAB_a", x.CielabA),
                                                                                  new XAttribute("CIELAB_b", x.CielabB),
                                                                                  new XAttribute("Density_C", x.Density.Red),
                                                                                  new XAttribute("Density_M", x.Density.Green),
                                                                                  new XAttribute("Density_Y", x.Density.Blue),
                                                                                  new XAttribute("Density_V", x.Density.Visual)
                                                                         )).ToList());

                    DataAccessFactory.
                        Instance.
                        ExecuteProcedure("CPSyncV.CreateMeasurement",
                                         new List<Parameters>()
                                             {
                                                 new Parameters()
                                                     {ParameterName = "@MeasurementName", Size = 50, Type = DbType.String, Value = measurement.MeasurementName},
                                                 new Parameters() {ParameterName = "@ProjectID", Type = DbType.Int32, Value = measurement.ProjectId},
                                                 new Parameters()
                                                     {ParameterName = "@MeasurementDate", Type = DbType.DateTime, Value = measurement.MeasurementDate},
                                                 new Parameters()
                                                     {ParameterName = "@ReferenceChartTypeID", Type = DbType.Int16, Value = measurement.ReferenceChartTypeId},
                                                 new Parameters() {ParameterName = "@MeasurementPatches", Value = measurementPatches.ToString()},
                                                 new Parameters() {ParameterName = "@Notes", Type = DbType.String, Value = measurement.Notes},
                                                 new Parameters() {ParameterName = "@Customer", Type = DbType.String, Value = measurement.Customer},
                                             },
                                         new List<Parameters>()
                                             {
                                                 outParam
                                             });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_SaveMeasurement"));
            return Convert.ToInt32(outParam.Value);
        }

        public List<AnalyzeChart_G7> GetGrayFChart(int MeasurementId, bool IsVerification = false)
        {
            var G7ChartDetails = new List<AnalyzeChart_G7>();
            string storedProc = "CPSyncV.GetVChartData_G7";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });

                    #region add Paper patch (CMYK = 0) at 0 index
                    if (dsMeasurements.Tables[0] != null && dsMeasurements.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dsMeasurements.Tables[0].NewRow();
                        dr["CMYK_C"] = 0;
                        dr["ΔF"] = 0;
                        dr["ΔLimit"] = dsMeasurements.Tables[0].Rows[0]["ΔLimit"].ToString();
                        dsMeasurements.Tables[0].Rows.InsertAt(dr, 0);
                    }
                    #endregion

                    G7ChartDetails = dsMeasurements.Tables[0].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_G7()
                        {                          
                            Cmyk_Value = DataReader.ReadDecimal(x, "CMYK_C"),
                            ΔValue = DataReader.ReadDecimal(x, "ΔF"),
                            ΔLimit = DataReader.ReadDecimal(x, "ΔLimit")                            
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_GrayF"));
            return G7ChartDetails;
        }

        public List<AnalyzeChart_G7> GetGrayFSummary(int MeasurementId, bool IsVerification = false)
        {
            var G7ChartDetails = new List<AnalyzeChart_G7>();
            string storedProc = "CPSyncV.GetVChartData_G7";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });

                    G7ChartDetails = dsMeasurements.Tables[1].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_G7()
                        {
                            MetricMeasuredID = DataReader.ReadInt(x, "MetricMeasuredID"),
                            Metric = DataReader.ReadString(x, "MetricSumName"),
                            Tolerance = DataReader.ReadDecimal(x, "TargetValue"),
                            Actual = DataReader.ReadDecimal(x, "MeasuredValue"),
                            IsNotOK = DataReader.ReadBoolean(x, "IsNotOK")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_GrayFSummary"));
            return G7ChartDetails;
        }

        public List<AnalyzeChart_G7> GetBlackLChart(int MeasurementId, bool IsVerification = false)
        {
            var G7ChartDetails = new List<AnalyzeChart_G7>();
            string storedProc = "CPSyncV.GetVChartData_G7";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });

                    #region add Paper patch (CMYK = 0) at 0 index
                    if (dsMeasurements.Tables[2] != null && dsMeasurements.Tables[2].Rows.Count > 0)
                    {
                        DataRow dr = dsMeasurements.Tables[2].NewRow();
                        dr["CMYK_K"] = 0;
                        dr["ΔL"] = 0;
                        dr["ΔLimit"] = dsMeasurements.Tables[2].Rows[0]["ΔLimit"].ToString();
                        dsMeasurements.Tables[2].Rows.InsertAt(dr, 0);
                    }
                    #endregion

                    G7ChartDetails = dsMeasurements.Tables[2].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_G7()
                        {
                            Cmyk_Value = DataReader.ReadDecimal(x, "CMYK_K"),
                            ΔValue = DataReader.ReadDecimal(x, "ΔL"),
                            ΔLimit = DataReader.ReadDecimal(x, "ΔLimit")     
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_BlackL"));
            return G7ChartDetails;
        }

        public List<AnalyzeChart_G7> GetBlackLSummary(int MeasurementId, bool IsVerification = false)
        {
            var G7ChartDetails = new List<AnalyzeChart_G7>();
            string storedProc = "CPSyncV.GetVChartData_G7";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });

                    G7ChartDetails = dsMeasurements.Tables[3].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_G7()
                        {
                            MetricMeasuredID = DataReader.ReadInt(x, "MetricMeasuredID"),
                            Metric = DataReader.ReadString(x, "MetricSumName"),
                            Tolerance = DataReader.ReadDecimal(x, "TargetValue"),
                            Actual = DataReader.ReadDecimal(x, "MeasuredValue"),
                            IsNotOK = DataReader.ReadBoolean(x, "IsNotOK")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_BlackLSummary"));
            return G7ChartDetails;
        }

        public List<AnalyzeChart_G7> GetGrayLChart(int MeasurementId, bool IsVerification = false)
        {
            var G7ChartDetails = new List<AnalyzeChart_G7>();
            string storedProc = "CPSyncV.GetVChartData_G7";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });

                    #region add Paper patch (CMYK = 0) at 0 index
                    if (dsMeasurements.Tables[4] != null && dsMeasurements.Tables[4].Rows.Count > 0)
                    {
                        DataRow dr = dsMeasurements.Tables[4].NewRow();
                        dr["CMYK_C"] = 0;
                        dr["ΔL"] = 0;
                        dr["ΔLimit"] = dsMeasurements.Tables[4].Rows[0]["ΔLimit"].ToString();
                        dsMeasurements.Tables[4].Rows.InsertAt(dr, 0);
                    }
                    #endregion

                    G7ChartDetails = dsMeasurements.Tables[4].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_G7()
                        {
                            Cmyk_Value = DataReader.ReadDecimal(x, "CMYK_C"),
                            ΔValue = DataReader.ReadDecimal(x, "ΔL"),
                            ΔLimit = DataReader.ReadDecimal(x, "ΔLimit")     
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_GrayL"));
            return G7ChartDetails;
        }

        public List<AnalyzeChart_G7> GetGrayLSummary(int MeasurementId, bool IsVerification = false)
        {
            var G7ChartDetails = new List<AnalyzeChart_G7>();
            string storedProc = "CPSyncV.GetVChartData_G7";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });

                    G7ChartDetails = dsMeasurements.Tables[5].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_G7()
                        {
                            MetricMeasuredID = DataReader.ReadInt(x, "MetricMeasuredID"),
                            Metric = DataReader.ReadString(x, "MetricSumName"),
                            Tolerance = DataReader.ReadDecimal(x, "TargetValue"),
                            Actual = DataReader.ReadDecimal(x, "MeasuredValue"),
                            IsNotOK = DataReader.ReadBoolean(x, "IsNotOK")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_GrayLSummary"));
            return G7ChartDetails;
        }

        public int Save1DMeasurement(Measurement measurement)
        {
            var outParam = new Parameters() { ParameterName = "@MeasurementID", Type = DbType.Int32 };
            Common.ImplementBusinessLogic(
                () =>
                {
                    var measurementPatches = new XElement("MeasurementPatches",
                                                          measurement.MeasurementPatches.
                                                              Select(x =>
                                                                     new XElement("MeasurementPatch",
                                                                                  new XAttribute("SampleName", x.SampleName),
                                                                                  new XAttribute("CIEXYZ_X", x.CiexyzX),
                                                                                  new XAttribute("CIEXYZ_Y", x.CiexyzY),
                                                                                  new XAttribute("CIEXYZ_Z", x.CiexyzZ),
                                                                                  new XAttribute("CIELAB_L", x.CielabL),
                                                                                  new XAttribute("CIELAB_a", x.CielabA),
                                                                                  new XAttribute("CIELAB_b", x.CielabB),
                                                                                  new XAttribute("Density_C", x.Density.Red),
                                                                                  new XAttribute("Density_M", x.Density.Green),
                                                                                  new XAttribute("Density_Y", x.Density.Blue),
                                                                                  new XAttribute("Density_V", x.Density.Visual)
                                                                         )).ToList());

                    DataAccessFactory.
                        Instance.
                        ExecuteProcedure("CPSync.CreateMeasurement",
                                         new List<Parameters>()
                                                 {
                                                     new Parameters() {ParameterName = "@MeasurementName", Size = 50, Type = DbType.String, Value = measurement.MeasurementName},
                                                     new Parameters() {ParameterName = "@OptimizeRunID", Type = DbType.Int32, Value = measurement.OptimizeRunId},
                                                     new Parameters() {ParameterName = "@MeasurementDate", Type = DbType.DateTime, Value = measurement.MeasurementDate},
                                                     new Parameters() {ParameterName = "@ReferenceChartTypeID", Type = DbType.Int16, Value = measurement.ReferenceChartTypeId},
                                                     new Parameters() {ParameterName = "@MeasurementPatches", Value = measurementPatches.ToString()},
                                                 },
                                         new List<Parameters>()
                                                 {
                                                     outParam
                                                 });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_SaveMeasurement"));
            return Convert.ToInt32(outParam.Value);
        }

        public List<Measurement> Get1DMeasurementsOnRunId(int optimizeRunId)
        {
            var measurements = new List<Measurement>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "CPSync.GetMeasurementByOptimizeRunID",
                                new object[] { optimizeRunId });
                    measurements = dsMeasurements.
                        Tables[0].
                        AsEnumerable().
                        Select(x => new Measurement()
                        {
                            MeasurementId = DataReader.ReadInt(x, "MeasurementID"),
                            MeasurementName = DataReader.ReadString(x, "MeasurementName"),
                            MeasurementDate = DataReader.ReadDate(x, "MeasurementDate"),
                            UploadDate = DataReader.ReadString(x, "MeasurementDate"),
                            OptimizeRunId = DataReader.ReadInt(x, "OptimizeRunID"),
                            IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementsOnRunId"));
            return measurements;
        }

        public MeasurementDataResponse Get1DMeasurementData(List<int> measurementIds)
        {
            var response = new MeasurementDataResponse()
            {
                DcsData = new List<double>(),
                PcsData = new List<double>(),
                DensityData = new List<double>()
            };
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsPcsDcsData = DataAccessFactory.
                        Instance.
                        GetDsResult(
                            "CPSync.GetMeasurementData",
                            new object[]
                                    {
                                        String.Join(",", measurementIds)
                                    });
                    if (dsPcsDcsData.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dsPcsDcsData.Tables[0].Rows)
                        {
                            response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_X"));
                            response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_Y"));
                            response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_Z"));

                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_C"));
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_M"));
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_Y"));
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_K"));

                            response.DensityData.Add(DataReader.ReadDouble(row, "Density_C"));
                            response.DensityData.Add(DataReader.ReadDouble(row, "Density_M"));
                            response.DensityData.Add(DataReader.ReadDouble(row, "Density_Y"));
                            response.DensityData.Add(DataReader.ReadDouble(row, "Density_V"));
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementData"));
            return response;
        }

        public TargetTVIDataResponse GetTargetTVIData(int projectId)
        {
            var response = new TargetTVIDataResponse()
            {
                TVI25 = "",
                TVI50 = "",
                TVI75 = "",

            };
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsTVIData = DataAccessFactory.
                        Instance.
                        GetDsResult(
                            "[CPSync].[GetTVIData]",
                            new object[]
                                    {
                                     projectId
                                    });
                    if (dsTVIData.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dsTVIData.Tables[0].Rows)
                        {
                            switch (DataReader.ReadInt(row, "TVI"))
                            {
                                case 25:
                                    response.TVI25 = DataReader.ReadString(row, "TVITarget");
                                    break;
                                case 50:
                                    response.TVI50 = DataReader.ReadString(row, "TVITarget");
                                    break;
                                case 75:
                                    response.TVI75 = DataReader.ReadString(row, "TVITarget");
                                    break;
                            }                           
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetTVIData"));
            return response;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
