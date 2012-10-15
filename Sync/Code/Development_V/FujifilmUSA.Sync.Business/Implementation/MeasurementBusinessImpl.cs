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
                                                                                  new XAttribute("CIELAB_b", x.CielabB)
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

        public List<Measurement> GetAnalyzeChartMeasurement(int optimizeRunId, string From_ΔE)
        {
            var measurements = new List<Measurement>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "[CPSync4D].[GetAnalyzeChartMeasurement]",
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

        public int UpdateMeasurementByAnalyzeChart(Measurement measurement)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue =
                        DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                "[CPSync4D].[UpdateMeasurementByAnalyzeChart]",
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

        public List<MeasurementPatchDetails> GetPatchDetailsofMeasurement(int MeasurementId, bool IsVerification = false)
        {
            var PatchDetails = new List<MeasurementPatchDetails>();
            string storedProc = "CPSync4D.GetPatchDetailsByMeasurementID";
            if (IsVerification)
                storedProc = "CPSyncV.GetVPatchDetailsByMeasurementID";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });
                    PatchDetails = dsMeasurements.
                        Tables[0].
                        AsEnumerable().
                        Select(x => new MeasurementPatchDetails()
                        {
                            SampleId = DataReader.ReadInt(x, "SampleID"),
                            HtmlColor = DataReader.ReadString(x, "HTMLColor"),
                            CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                            CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                            CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                            CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                            TargetL = DataReader.ReadDecimal(x, "TargetL"),
                            Targeta = DataReader.ReadDecimal(x, "Targeta"),
                            Targetb = DataReader.ReadDecimal(x, "Targetb"),
                            CielabL = DataReader.ReadDecimal(x, "CIELAB_L"),
                            CielabA = DataReader.ReadDecimal(x, "CIELAB_a"),
                            CielabB = DataReader.ReadDecimal(x, "CIELAB_b"),
                            ΔE = DataReader.ReadDecimal(x, "ΔE"),
                            ΔE94 = DataReader.ReadDecimal(x, "ΔE94"),
                            ΔE2000 = DataReader.ReadDecimal(x, "ΔE2000")
                        }).ToList();

                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetPatchDetailsofMeasurement"));
            return PatchDetails;
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
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_InkPaper"));
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
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_InkPaper"));
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

        public DataSet GetReportDataOnMeasurement(int MeasurementId, string From_ΔE)
        {
            var measurements = new DataSet();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "Report.GetSingleMeasurement",
                                new object[] { MeasurementId, From_ΔE });
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
                                                                                  new XAttribute("CIELAB_b", x.CielabB)
                                                                         )).ToList());

                    DataAccessFactory.
                        Instance.
                        ExecuteProcedure("CPSyncV.CreateMeasurement",
                                         new List<Parameters>()
                                                 {
                                                     new Parameters() {ParameterName = "@MeasurementName", Size = 50, Type = DbType.String, Value = measurement.MeasurementName},
                                                     new Parameters() {ParameterName = "@ProjectID", Type = DbType.Int32, Value = measurement.ProjectId},
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

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}