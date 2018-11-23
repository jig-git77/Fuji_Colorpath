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

        public MeasurementDataResponse GetMeasurementData(List<int> measurementIds, PCSDataTypes PCSDataType)
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
                                        String.Join(",", measurementIds),
                                        Convert.ToInt32(PCSDataType)
                                    });
                    if (dsPcsDcsData.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dsPcsDcsData.Tables[0].Rows)
                        {
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_C"));
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_M"));
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_Y"));
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_K"));

                            if (PCSDataType == PCSDataTypes.Spectral)
                            {
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm380"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm390"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm400"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm410"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm420"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm430"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm440"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm450"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm460"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm470"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm480"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm490"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm500"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm510"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm520"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm530"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm540"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm550"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm560"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm570"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm580"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm590"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm600"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm610"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm620"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm630"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm640"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm650"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm660"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm670"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm680"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm690"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm700"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm710"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm720"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm730"));
                            }
                            else //XYZ data
                            {
                                response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_X"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_Y"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_Z"));
                            }
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

                                                                         //---Spectral Data
                                                                         //new XAttribute("nm380", x.Spectral.nm380),
                                                                         //new XAttribute("nm390", x.Spectral.nm390),
                                                                         //new XAttribute("nm400", x.Spectral.nm400),
                                                                         //new XAttribute("nm410", x.Spectral.nm410),
                                                                         //new XAttribute("nm420", x.Spectral.nm420),
                                                                         //new XAttribute("nm430", x.Spectral.nm430),
                                                                         //new XAttribute("nm440", x.Spectral.nm440),
                                                                         //new XAttribute("nm450", x.Spectral.nm450),
                                                                         //new XAttribute("nm460", x.Spectral.nm460),
                                                                         //new XAttribute("nm470", x.Spectral.nm470),
                                                                         //new XAttribute("nm480", x.Spectral.nm480),
                                                                         //new XAttribute("nm490", x.Spectral.nm490),
                                                                         //new XAttribute("nm500", x.Spectral.nm500),
                                                                         //new XAttribute("nm510", x.Spectral.nm510),
                                                                         //new XAttribute("nm520", x.Spectral.nm520),
                                                                         //new XAttribute("nm530", x.Spectral.nm530),
                                                                         //new XAttribute("nm540", x.Spectral.nm540),
                                                                         //new XAttribute("nm550", x.Spectral.nm550),
                                                                         //new XAttribute("nm560", x.Spectral.nm560),
                                                                         //new XAttribute("nm570", x.Spectral.nm570),
                                                                         //new XAttribute("nm580", x.Spectral.nm580),
                                                                         //new XAttribute("nm590", x.Spectral.nm590),
                                                                         //new XAttribute("nm600", x.Spectral.nm600),
                                                                         //new XAttribute("nm610", x.Spectral.nm610),
                                                                         //new XAttribute("nm620", x.Spectral.nm620),
                                                                         //new XAttribute("nm630", x.Spectral.nm630),
                                                                         //new XAttribute("nm640", x.Spectral.nm640),
                                                                         //new XAttribute("nm650", x.Spectral.nm650),
                                                                         //new XAttribute("nm660", x.Spectral.nm660),
                                                                         //new XAttribute("nm670", x.Spectral.nm670),
                                                                         //new XAttribute("nm680", x.Spectral.nm680),
                                                                         //new XAttribute("nm690", x.Spectral.nm690),
                                                                         //new XAttribute("nm700", x.Spectral.nm700),
                                                                         //new XAttribute("nm710", x.Spectral.nm710),
                                                                         //new XAttribute("nm720", x.Spectral.nm720),
                                                                         //new XAttribute("nm730", x.Spectral.nm730)
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
                                                     new Parameters() {ParameterName = "@PCSDataType", Type = DbType.Int32, Value = Convert.ToInt32(measurement.PCSDataType)}
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
                                MeasurementStatus = DataReader.ReadString(x, "MeasurementStatus"),
                                MeasurementDateString = String.Format("{0:d}", DataReader.ReadDate(x, "MeasurementDate")) + "-" + String.Format("{0:T}", DataReader.ReadDate(x, "MeasurementDate")) //---Formatted Date                                
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

        public int UpdateMeasurementByAnalyzeChart(Measurement measurement, bool IsVerification = false, bool IsSync1DType = false)
        {
            int returnValue = 0;
            string storedProc = "[CPSync4D].[UpdateMeasurementByAnalyzeChart]";
            if (IsSync1DType)
                storedProc = "[CPSync].[UpdateMeasurementByAnalyzeChart]";
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

        public int UpdateMeasurementByAnalyzeChartFromService(int MeasurementId, bool isUsedMeasurement, bool IsVerification = false, bool IsSync1DType = false)
        {
            int returnValue = 0;
            string storedProc = "[CPSync4D].[UpdateMeasurementByAnalyzeChart]";
            if (IsSync1DType)
                storedProc = "[CPSync].[UpdateMeasurementByAnalyzeChart]";
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
                                            MeasurementId,
                                            isUsedMeasurement
                                        });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateMeasurementByAnalyzeChart"));
            return returnValue;

        }

        public List<MeasurementPatch_AvgDelta> GetAvgDeltaMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false)
        {
            string storedProc = "CPSync4D.GetMeasurementDeltasByMeasurementID";
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

        public List<Measurement> GetVMeasurementSummary(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            var VmeasurementSummary = new List<Measurement>();
            string storedProc = "CPSyncV.GetVMeasurementDeltasByMeasurementID";
            if (IsSync1DType)
            {
                storedProc = "CPSync.GetMeasurementDeltasByMeasurementID";
                if (IsAverageOfMeasurements)
                {
                    storedProc = "CPSync.GetMeasurementDeltasByMeasurementID_Avg";
                }
            }
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
                              PageOrSurface = IsSync1DType ? "" : DataReader.ReadString(x, "PageOrSurface"),
                              JDFJobId = IsSync1DType ? "" : DataReader.ReadString(x, "JDFJobId"),
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

                        if (IsSync1DType == false) //----Patch Tolerance is only available for Validation projects
                        {
                            VmeasurementSummary[0].PatchTolerances = dsMeasurements.Tables[2].AsEnumerable().
                               Select(x => new Entity.CPSyncV.PatchTolerance()
                               {
                                   SampleName = DataReader.ReadString(x, "SampleName"),
                                   HtmlColor = DataReader.ReadString(x, "HtmlColor"),
                                   Tolerance = DataReader.ReadDecimal(x, "TargetValue"),
                                   Actual = DataReader.ReadDecimal(x, "MeasuredValue"),
                                   IsNotOK = DataReader.ReadBoolean(x, "IsNotOK")
                               }).ToList();
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetAvgDeltaMeasurement"));
            return VmeasurementSummary;
        }

        public DataTable GetPatchDetailsofMeasurement(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false)
        {
            // var PatchDetails = new List<MeasurementPatchDetails>();
            string storedProc = "CPSync4D.GetPatchDetailsByMeasurementID";
            if (IsSync1DType)
                storedProc = "CPSync.GetPatchDetailsByMeasurementID";
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
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetPatchDetailsofMeasurement"));
            return dtPatchDetails;
        }

        #region Get common chart data
        public List<AnalyzeChart>[] GetChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            var DeltaEPatchDetails = new List<AnalyzeChart>();
            var ChartDetailArray = new List<AnalyzeChart>[3];
            string storedProc = "CPSync4D.GetChartData_InkPaper";
            if (IsSync1DType)
            {
                storedProc = "CPSync.GetChartData_InkPaper";
                if (IsAverageOfMeasurements)
                {
                    storedProc = "CPSync.GetChartData_InkPaper_Avg";
                }
            }
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

                    //DeltaEPatchDetails
                    ChartDetailArray[0] = dsMeasurements.Tables[0].
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
                            Targetb = DataReader.ReadDecimal(x, "Targetb"),
                            PreAvgΔE = DataReader.ReadDecimal(x, "PreAvgΔE")
                        }).ToList();

                    //PaperPatchDetails
                    ChartDetailArray[1] = dsMeasurements.Tables[1].
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

                    //DensityPatchDetails
                    ChartDetailArray[2] = dsMeasurements.Tables[2].
                         AsEnumerable().
                        Select(x => new AnalyzeChart()
                        {
                            HtmlColor = DataReader.ReadString(x, "HtmlColor"),
                            PatchTypeId = DataReader.ReadShort(x, "PatchTypeId"),
                            Density = DataReader.ReadDecimal(x, "Density"),
                            DensityLimit = DataReader.ReadDecimal(x, "DensityLimit"),
                            PreAvgDensity = DataReader.ReadDecimal(x, "PreAvgDensity")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_InkPaper_EPrimaryPatches"));
            return ChartDetailArray;
        }
        #endregion

        public List<AnalyzeChart> GetDeltaEPatchChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false)
        {
            var DeltaEPatchDetails = new List<AnalyzeChart>();

            string storedProc = "CPSync4D.GetChartData_InkPaper";
            if (IsSync1DType)
                storedProc = "CPSync.GetChartData_InkPaper";
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

        public List<AnalyzeChart> GetInkPaperChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false)
        {
            var PaperPatchDetails = new List<AnalyzeChart>();
            string storedProc = "CPSync4D.GetChartData_InkPaper";
            if (IsSync1DType)
                storedProc = "CPSync.GetChartData_InkPaper";
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

        public List<AnalyzeChart> GetDensityChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false)
        {
            var DensityPatchDetails = new List<AnalyzeChart>();
            string storedProc = "CPSync4D.GetChartData_InkPaper";
            if (IsSync1DType)
                storedProc = "CPSync.GetChartData_InkPaper";
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
                    DensityPatchDetails = dsMeasurements.Tables[2].
                         AsEnumerable().
                        Select(x => new AnalyzeChart()
                        {
                            HtmlColor = DataReader.ReadString(x, "HtmlColor"),
                            PatchTypeId = DataReader.ReadShort(x, "PatchTypeId"),
                            Density = DataReader.ReadDecimal(x, "Density"),
                            DensityLimit = DataReader.ReadDecimal(x, "DensityLimit")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_InkPaper_Density"));
            return DensityPatchDetails;
        }

        #region Get common Gamut chart data
        public List<AnalyzeChart_Gray>[] GetGamutChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            var GamutChartDetails = new List<AnalyzeChart_Gray>();
            var GamutChartData = new List<AnalyzeChart_Gray>[3];
            string storedProc = "CPSync4D.GetChartData_Gray";
            if (IsSync1DType)
            {
                storedProc = "CPSync.GetChartData_Gray";
                if (IsAverageOfMeasurements)
                {
                    storedProc = "CPSync.GetChartData_Gray_Avg";
                }
            }
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
                    //GamutChartDetails Char 1
                    GamutChartData[0] = dsMeasurements.Tables[0].
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

                    //GamutChartDetails Char 2
                    GamutChartData[1] = dsMeasurements.Tables[1].
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

                    //Lab chart details
                    GamutChartData[2] = dsMeasurements.Tables[2].
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
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_GrayGamut1"));
            return GamutChartData;
        }
        #endregion

        public List<AnalyzeChart_Gray> GetGamutChart1(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false)
        {
            var GamutChartDetails = new List<AnalyzeChart_Gray>();
            string storedProc = "CPSync4D.GetChartData_Gray";
            if (IsSync1DType)
                storedProc = "CPSync.GetChartData_Gray";
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

        public List<AnalyzeChart_Gray> GetGamutChart2(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false)
        {
            var GamutChartDetails = new List<AnalyzeChart_Gray>();
            string storedProc = "CPSync4D.GetChartData_Gray";
            if (IsSync1DType)
                storedProc = "CPSync.GetChartData_Gray";
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

        public List<AnalyzeChart_Gray> GetLABChart(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false)
        {
            var LABChartDetails = new List<AnalyzeChart_Gray>();
            string storedProc = "CPSync4D.GetChartData_Gray";
            if (IsSync1DType)
                storedProc = "CPSync.GetChartData_Gray";
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

        public DataSet GetReportDataOnMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false, int ValidationType = -1, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            var measurements = new DataSet();
            string storedProc = "Report.GetSingleMeasurement";
            if (IsSync1DType)
            {
                storedProc = "Report.GetSingle1DMeasurement";
                if (IsAverageOfMeasurements)
                {
                    storedProc = "Report.GetSingle1DMeasurement_Avg";
                }
            }
            if (IsVerification)
                storedProc = "Report.GetSingleVMeasurement";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements = new DataSet();
                    if (!IsVerification && !IsSync1DType)
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

        public DataSet GetReportDataOnMeasurement_G7_Vali(int MeasurementId, string From_ΔE, bool IsVerification = false, int ValidationType = -1, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            var measurements = new DataSet();
            string storedProc = "Report.GetSingleVMeasurement";
            if (IsSync1DType)
            {
                storedProc = "Report.GetSingle1DMeasurement";
                if (IsAverageOfMeasurements)
                {
                    storedProc = "Report.GetSingle1DMeasurement_Avg";
                }
            }
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
                                                 new Parameters() {ParameterName = "@PageOrSurface", Type = DbType.String, Value = measurement.PageOrSurface},
                                                 new Parameters() {ParameterName = "@JDFJobId", Type = DbType.String, Value = measurement.JDFJobId}
                                             },
                                         new List<Parameters>()
                                             {
                                                 outParam
                                             });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_SaveMeasurement"));
            return Convert.ToInt32(outParam.Value);
        }

        public List<AnalyzeChart_G7>[] GetG7ChartData(int MeasurementId, bool IsVerification = false, bool IsAverageOfMeasurements = false)
        {
            var G7ChardDAta = new List<AnalyzeChart_G7>[8];

            string storedProc = "[CPSync].[GetChartData_G7]";
            if (IsAverageOfMeasurements)
            {
                storedProc = "CPSync.GetChartData_G7_Avg";
            }
            if (IsVerification)
                storedProc = "[CPSyncV].[GetVChartData_G7]";


            #region Get Data logic for the GrayF
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
                            dr["ΔLimit"] = Convert.ToDecimal(dsMeasurements.Tables[0].Rows[0]["ΔLimit"].ToString());
                            dsMeasurements.Tables[0].Rows.InsertAt(dr, 0);
                        }
                        #endregion

                        G7ChardDAta[0] = dsMeasurements.Tables[0].
                             AsEnumerable().
                            Select(x => new AnalyzeChart_G7()
                            {
                                Cmyk_Value = DataReader.ReadDecimal(x, "CMYK_C"),
                                ΔValue = DataReader.ReadDecimal(x, "ΔF"),
                                wΔValue = DataReader.ReadDecimal(x, "wΔF"),
                                ΔLimit = DataReader.ReadDecimal(x, "ΔLimit")
                            }).ToList();
                    },
                    new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_GrayF"));
            #endregion

            #region Get Data logic for GrayFSummary
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });

                    G7ChardDAta[1] = dsMeasurements.Tables[1].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_G7()
                        {
                            Metric = DataReader.ReadString(x, "MetricSumName"),
                            Tolerance = DataReader.ReadDecimal(x, "TargetValue"),
                            Actual = DataReader.ReadDecimal(x, "MeasuredValue"),
                            IsNotOK = DataReader.ReadBoolean(x, "IsNotOK")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_GrayFSummary"));
            #endregion

            #region Get Data logic for BlackLChart
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
                        dr["ΔLimit"] = Convert.ToDecimal(dsMeasurements.Tables[2].Rows[0]["ΔLimit"].ToString());
                        dsMeasurements.Tables[2].Rows.InsertAt(dr, 0);
                    }
                    #endregion

                    G7ChardDAta[2] = dsMeasurements.Tables[2].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_G7()
                        {
                            Cmyk_Value = DataReader.ReadDecimal(x, "CMYK_K"),
                            ΔValue = DataReader.ReadDecimal(x, "ΔL"),
                            wΔValue = DataReader.ReadDecimal(x, "wΔL"),
                            ΔLimit = DataReader.ReadDecimal(x, "ΔLimit")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_BlackL"));
            #endregion

            #region Get Data logic for BlackLSummary
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });

                    G7ChardDAta[3] = dsMeasurements.Tables[3].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_G7()
                        {
                            Metric = DataReader.ReadString(x, "MetricSumName"),
                            Tolerance = DataReader.ReadDecimal(x, "TargetValue"),
                            Actual = DataReader.ReadDecimal(x, "MeasuredValue"),
                            IsNotOK = DataReader.ReadBoolean(x, "IsNotOK")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_BlackLSummary"));
            #endregion

            #region Get data logic for GetGrayLChart
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
                        dr["ΔLimit"] = Convert.ToDecimal(dsMeasurements.Tables[4].Rows[0]["ΔLimit"].ToString());
                        dsMeasurements.Tables[4].Rows.InsertAt(dr, 0);
                    }
                    #endregion

                    G7ChardDAta[4] = dsMeasurements.Tables[4].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_G7()
                        {
                            Cmyk_Value = DataReader.ReadDecimal(x, "CMYK_C"),
                            ΔValue = DataReader.ReadDecimal(x, "ΔL"),
                            wΔValue = DataReader.ReadDecimal(x, "wΔL"),
                            ΔLimit = DataReader.ReadDecimal(x, "ΔLimit")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_GrayL"));
            #endregion

            #region Get Data logic for the GetGrayLSummary
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });

                    G7ChardDAta[5] = dsMeasurements.Tables[5].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_G7()
                        {
                            Metric = DataReader.ReadString(x, "MetricSumName"),
                            Tolerance = DataReader.ReadDecimal(x, "TargetValue"),
                            Actual = DataReader.ReadDecimal(x, "MeasuredValue"),
                            IsNotOK = DataReader.ReadBoolean(x, "IsNotOK")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_GrayLSummary"));
            #endregion

            #region Get Data logic for the Black NPD Chart
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
                        if (dsMeasurements.Tables[6] != null && dsMeasurements.Tables[6].Rows.Count > 0)
                        {
                            DataRow dr = dsMeasurements.Tables[6].NewRow();
                            dr["CMYK_K"] = 0;
                            dr["G7NPDC"] = 0;
                            dr["G7NPDC_Act"] = 0;
                            dsMeasurements.Tables[6].Rows.InsertAt(dr, 0);
                        }
                        #endregion

                        G7ChardDAta[6] = dsMeasurements.Tables[6].
                             AsEnumerable().
                            Select(x => new AnalyzeChart_G7()
                            {
                                Cmyk_Value = DataReader.ReadDecimal(x, "CMYK_K"),
                                ΔValue = DataReader.ReadDecimal(x, "G7NPDC_Act"),
                                ΔLimit = DataReader.ReadDecimal(x, "G7NPDC")
                            }).ToList();
                    },
                    new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_BlackNPD"));
            #endregion


            #region Get Data logic for the Gray NPD Chart
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
                        if (dsMeasurements.Tables[7] != null && dsMeasurements.Tables[7].Rows.Count > 0)
                        {
                            DataRow dr = dsMeasurements.Tables[7].NewRow();
                            dr["CMYK_C"] = 0;
                            dr["G7NPDC"] = 0;
                            dr["G7NPDC_Act"] = 0;
                            dsMeasurements.Tables[7].Rows.InsertAt(dr, 0);
                        }
                        #endregion

                        G7ChardDAta[7] = dsMeasurements.Tables[7].
                             AsEnumerable().
                            Select(x => new AnalyzeChart_G7()
                            {
                                Cmyk_Value = DataReader.ReadDecimal(x, "CMYK_C"),
                                ΔValue = DataReader.ReadDecimal(x, "G7NPDC_Act"),
                                ΔLimit = DataReader.ReadDecimal(x, "G7NPDC")
                            }).ToList();
                    },
                    new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_G7_CHRT_GrayNPD"));
            #endregion

            return G7ChardDAta;
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
                                                                                  new XAttribute("Density_V", x.Density.Visual),

                                                                                  //---Spectral Data
                                                                                  new XAttribute("nm380", x.Spectral.nm380),
                                                                                  new XAttribute("nm390", x.Spectral.nm390),
                                                                                  new XAttribute("nm400", x.Spectral.nm400),
                                                                                  new XAttribute("nm410", x.Spectral.nm410),
                                                                                  new XAttribute("nm420", x.Spectral.nm420),
                                                                                  new XAttribute("nm430", x.Spectral.nm430),
                                                                                  new XAttribute("nm440", x.Spectral.nm440),
                                                                                  new XAttribute("nm450", x.Spectral.nm450),
                                                                                  new XAttribute("nm460", x.Spectral.nm460),
                                                                                  new XAttribute("nm470", x.Spectral.nm470),
                                                                                  new XAttribute("nm480", x.Spectral.nm480),
                                                                                  new XAttribute("nm490", x.Spectral.nm490),
                                                                                  new XAttribute("nm500", x.Spectral.nm500),
                                                                                  new XAttribute("nm510", x.Spectral.nm510),
                                                                                  new XAttribute("nm520", x.Spectral.nm520),
                                                                                  new XAttribute("nm530", x.Spectral.nm530),
                                                                                  new XAttribute("nm540", x.Spectral.nm540),
                                                                                  new XAttribute("nm550", x.Spectral.nm550),
                                                                                  new XAttribute("nm560", x.Spectral.nm560),
                                                                                  new XAttribute("nm570", x.Spectral.nm570),
                                                                                  new XAttribute("nm580", x.Spectral.nm580),
                                                                                  new XAttribute("nm590", x.Spectral.nm590),
                                                                                  new XAttribute("nm600", x.Spectral.nm600),
                                                                                  new XAttribute("nm610", x.Spectral.nm610),
                                                                                  new XAttribute("nm620", x.Spectral.nm620),
                                                                                  new XAttribute("nm630", x.Spectral.nm630),
                                                                                  new XAttribute("nm640", x.Spectral.nm640),
                                                                                  new XAttribute("nm650", x.Spectral.nm650),
                                                                                  new XAttribute("nm660", x.Spectral.nm660),
                                                                                  new XAttribute("nm670", x.Spectral.nm670),
                                                                                  new XAttribute("nm680", x.Spectral.nm680),
                                                                                  new XAttribute("nm690", x.Spectral.nm690),
                                                                                  new XAttribute("nm700", x.Spectral.nm700),
                                                                                  new XAttribute("nm710", x.Spectral.nm710),
                                                                                  new XAttribute("nm720", x.Spectral.nm720),
                                                                                  new XAttribute("nm730", x.Spectral.nm730)
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
                                                     new Parameters() {ParameterName = "@PCSDataType", Type = DbType.Int32, Value = ((int)measurement.PCSDataType)}

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

        public MeasurementDataResponse Get1DMeasurementData(List<int> measurementIds, PCSDataTypes PCSDataType)
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
                            "CPSync.GetMeasurementData",
                            new object[]
                                    {
                                        String.Join(",", measurementIds),
                                        Convert.ToInt32(PCSDataType)
                                    });
                    if (dsPcsDcsData.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dsPcsDcsData.Tables[0].Rows)
                        {
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_C"));
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_M"));
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_Y"));
                            response.DcsData.Add(DataReader.ReadDouble(row, "CMYK_K"));

                            if (PCSDataType == PCSDataTypes.Density)
                            {
                                response.PcsData.Add(DataReader.ReadDouble(row, "Density_C"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "Density_M"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "Density_Y"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "Density_V"));
                            }
                            else if (PCSDataType == PCSDataTypes.Spectral)
                            {
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm380"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm390"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm400"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm410"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm420"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm430"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm440"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm450"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm460"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm470"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm480"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm490"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm500"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm510"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm520"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm530"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm540"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm550"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm560"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm570"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm580"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm590"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm600"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm610"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm620"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm630"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm640"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm650"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm660"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm670"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm680"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm690"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm700"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm710"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm720"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "nm730"));
                            }
                            else  //XYZ data
                            {
                                response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_X"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_Y"));
                                response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_Z"));
                            }
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementData"));
            return response;
        }

        public Dictionary<string, string> GetTargetTVIData(int projectId, bool Is1DMeasurementSmoothing)
        {
            var TVIOptionsList = new Dictionary<string, string>();

            string AnswerTVI_CMY = "";
            string AnswerTVI_K = "";

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
                    //----Table 0 : Pass CMY TVI values as key value pair
                    if (dsTVIData.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dsTVIData.Tables[0].Rows)
                        {
                            AnswerTVI_CMY += DataReader.ReadInt(row, "TVI") + " " + DataReader.ReadString(row, "TVITarget", true); //Prevent string to convert in localized string version, so passing hard coded USA english version for all conversions during values passing in Profiler in order to resolve error getting by passing wrong style of profiler options during ISO create DLP.

                            if (DataReader.ReadInt(row, "TVI") != 100)
                            {
                                AnswerTVI_CMY += " ";
                            }
                        }
                    }

                    //----Table 1 : Pass K TVI values as key value pair
                    if (dsTVIData.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow row in dsTVIData.Tables[1].Rows)
                        {
                            AnswerTVI_K += DataReader.ReadInt(row, "TVI") + " " + DataReader.ReadString(row, "TVITarget", true); //Prevent string to convert in localized string version, so passing hard coded USA english version for all conversions during values passing in Profiler in order to resolve error getting by passing wrong style of profiler options during ISO create DLP.

                            if (DataReader.ReadInt(row, "TVI") != 100)
                            {
                                AnswerTVI_K += " ";
                            }
                        }
                    }

                    TVIOptionsList.Add("LinearizationTVIChannel0", AnswerTVI_CMY);
                    TVIOptionsList.Add("LinearizationTVIChannel1", AnswerTVI_CMY);
                    TVIOptionsList.Add("LinearizationTVIChannel2", AnswerTVI_CMY);
                    TVIOptionsList.Add("LinearizationTVIChannel3", AnswerTVI_K);
                    if (Is1DMeasurementSmoothing == true)
                    {
                        TVIOptionsList.Add("LinearizationSmoothing", "25");       //---New setting for enable measurement smoothing in Sync1D projects
                    }

                    //----Table 2 : Pass Profiler Options from CPSync.ProjectOption Table
                    if (dsTVIData.Tables[2].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsTVIData.Tables[2].Rows.Count; i++)
                        {
                            TVIOptionsList.Add(dsTVIData.Tables[2].Rows[i]["ProfilerOption"].ToString(), dsTVIData.Tables[2].Rows[i]["ProfilerOptionValue"].ToString());
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetTVIData"));
            return TVIOptionsList;
        }

        public List<AnalyzeChart_DotGain> GetTVIChart(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            var TVIChartDetails = new List<AnalyzeChart_DotGain>();
            string storedProc = "CPSync.GetChartData_DotGain";   //----G7 Sync1D type
            if (IsAverageOfMeasurements)
            {
                storedProc = "CPSync.GetChartData_DotGain_Avg";
            }
            if (IsVerification)
                storedProc = "CPSyncV.GetVChartData_DotGain";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });
                    TVIChartDetails = dsMeasurements.Tables[0].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_DotGain()
                        {
                            CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                            CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                            CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                            CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                            DotGain = DataReader.ReadDecimal(x, "DotGain"),
                            HtmlColor = DataReader.ReadString(x, "HTMLColor")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_TVI"));
            return TVIChartDetails;
        }

        public List<AnalyzeChart_DotGain> GetISOTVIChart(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            var TVIChartDetails = new List<AnalyzeChart_DotGain>();
            string storedProc = "CPSync.GetChartData_ISO_DotGain";  //----ISO Sync1D type  
            if (IsSync1DType)
            {
                if (IsAverageOfMeasurements)
                {
                    storedProc = "CPSync.GetChartData_ISO_DotGain_Avg";
                }
            }
            if (IsVerification)
                storedProc = "CPSyncV.GetVChartData_ISO_DotGain";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });
                    TVIChartDetails = dsMeasurements.Tables[0].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_DotGain()
                        {
                            CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                            CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                            CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                            CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                            DotGain = DataReader.ReadDecimal(x, "DotGain"),
                            HtmlColor = DataReader.ReadString(x, "HTMLColor"),
                            PatchTypeId = DataReader.ReadShort(x, "PatchTypeID"),
                            TargetDotGain = DataReader.ReadDecimal(x, "TargetDotGain"),
                            Threshold = DataReader.ReadDecimal(x, "Threshold")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_ISO_TVI"));
            return TVIChartDetails;
        }

        public List<AnalyzeChart_DotGain> GetSpreadChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            var TVIChartDetails = new List<AnalyzeChart_DotGain>();
            string storedProc = "CPSync.GetChartData_ISO_DotGain";  //----ISO Sync1D type  
            if (IsSync1DType)
            {
                if (IsAverageOfMeasurements)
                {
                    storedProc = "CPSync.GetChartData_ISO_DotGain_Avg";
                }
            }
            if (IsVerification)
                storedProc = "CPSyncV.GetVChartData_ISO_DotGain";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                       DataAccessFactory.
                           Instance.
                           GetDsResult(
                               storedProc,
                               new object[] { MeasurementId });
                    TVIChartDetails = dsMeasurements.Tables[1].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_DotGain()
                        {
                            CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                            CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                            CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                            CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                            DotGain = DataReader.ReadDecimal(x, "DotGain"),
                            HtmlColor = DataReader.ReadString(x, "HTMLColor"),
                            PatchTypeId = DataReader.ReadShort(x, "PatchTypeID"),
                            DotGainSpreadTh = DataReader.ReadDecimal(x, "DotGainSpreadTh")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_ISO_TVI1"));
            return TVIChartDetails;
        }

        public List<AnalyzeChart_DotGain> GetSpreadSplineRangeChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            var TVIChartDetails = new List<AnalyzeChart_DotGain>();
            string storedProc = "CPSync.GetChartData_ISO_DotGain";  //----ISO Sync1D type  
            if (IsSync1DType)
            {
                if (IsAverageOfMeasurements)
                {
                    storedProc = "CPSync.GetChartData_ISO_DotGain_Avg";
                }
            }
            if (IsVerification)
                storedProc = "CPSyncV.GetVChartData_ISO_DotGain";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });
                    TVIChartDetails = dsMeasurements.Tables[2].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_DotGain()
                        {
                            TVI = DataReader.ReadDecimal(x, "TVI"),
                            UpperLimit = DataReader.ReadDecimal(x, "UpperLimit"),
                            LowerLimit = DataReader.ReadDecimal(x, "LowerLimit")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_ISO_TVI2"));
            return TVIChartDetails;
        }

        public DataTable GetMaxSpreadChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            var TVIChartDetails = new DataTable();
            string storedProc = "CPSync.GetChartData_ISO_DotGain";  //----ISO Sync1D type  
            if (IsSync1DType)
            {
                if (IsAverageOfMeasurements)
                {
                    storedProc = "CPSync.GetChartData_ISO_DotGain_Avg";
                }
            }
            if (IsVerification)
                storedProc = "CPSyncV.GetVChartData_ISO_DotGain";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });
                    TVIChartDetails = dsMeasurements.Tables[3];

                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_ISO_TVI3"));
            return TVIChartDetails;
        }

        public List<AnalyzeChart_DotGain> GetSpreadChartData_CMYK(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            var TVIChartDetails = new List<AnalyzeChart_DotGain>();
            string storedProc = "CPSync.GetChartData_ISO_DotGain";  //----ISO Sync1D type  
            if (IsSync1DType)
            {
                if (IsAverageOfMeasurements)
                {
                    storedProc = "CPSync.GetChartData_ISO_DotGain_Avg";
                }
            }
            if (IsVerification)
                storedProc = "CPSyncV.GetVChartData_ISO_DotGain";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                       DataAccessFactory.
                           Instance.
                           GetDsResult(
                               storedProc,
                               new object[] { MeasurementId });
                    TVIChartDetails = dsMeasurements.Tables[4].
                         AsEnumerable().
                        Select(x => new AnalyzeChart_DotGain()
                        {
                            CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                            CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                            CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                            CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                            DotGain = DataReader.ReadDecimal(x, "DotGain"),
                            HtmlColor = DataReader.ReadString(x, "HTMLColor"),
                            PatchTypeId = DataReader.ReadShort(x, "PatchTypeID"),
                            DotGainSpreadTh = DataReader.ReadDecimal(x, "DotGainSpreadTh")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartData_ISO_TVI4"));
            return TVIChartDetails;
        }

        public int ChkAvgMeasurementCompatibility(int optimizeRunId)
        {
            var outParam = new Parameters() { ParameterName = "@CompatibilityNo", Type = DbType.Int32 };
            Common.ImplementBusinessLogic(
                () =>
                {
                    DataAccessFactory.
                        Instance.
                        ExecuteProcedure("CPSync.ChkAvgMeasurementCompatibility",
                                         new List<Parameters>()
                                                 {
                                                     new Parameters() {ParameterName = "@OptimizeRunID", Type = DbType.Int32, Value = optimizeRunId}
                                                 },
                                         new List<Parameters>()
                                                 {
                                                     outParam
                                                 });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_ChkAvgMeasurementCompatibility"));
            return Convert.ToInt32(outParam.Value);
        }


        public DataSet GetMeasurementDataForScoring(int MeasurementId, string From_ΔE, int ProjectId, bool IsAverageOfMeasurements = false)
        {
            var measurements = new DataSet();
            string storedProc = "Report.GetVMeasurementDataForScoring";

            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements = new DataSet();
                    dsMeasurements = DataAccessFactory.
                         Instance.
                         GetDsResult(
                             storedProc,
                             new object[] { MeasurementId, From_ΔE, ProjectId, IsAverageOfMeasurements });

                    measurements = dsMeasurements;
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_Report_GetReportGrading"));
            return measurements;
        }

        public DataSet GetDensityData_C(int OptimizeRunId)
        {
            var measurements = new DataSet();
            string storedProc = "Report.GetCMYKDensityData";

            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements = new DataSet();
                    dsMeasurements = DataAccessFactory.
                         Instance.
                         GetDsResult(
                             storedProc,
                             new object[] { OptimizeRunId });

                    measurements = dsMeasurements;
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_Report_GetCMYKDensityData"));
            return measurements;
        }

        public DataTable GetSpotPatchDetailsofMeasurement(int OptimizeRunId)
        {
            string storedProc = "CPSpot.GetPatchDetailsByOptimizeRunID";

            DataTable dtPatchDetails = new DataTable();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { OptimizeRunId });
                    if (dsMeasurements.Tables.Count > 0)
                        dtPatchDetails = dsMeasurements.Tables[0];
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetPatchDetailsofMeasurement"));
            return dtPatchDetails;
        }



        public int SaveSpotMeasurement(Measurement measurement)
        {
            var outParam = new Parameters() { ParameterName = "@MeasurementID", Type = DbType.Int32 };
            Common.ImplementBusinessLogic(
                () =>
                {
                    var measurementPatches = new XElement("MeasurementPatches",
                                                          measurement.MeasurementPatches.
                                                              Select(x =>
                                                                     new XElement("MeasurementPatch",
                                                                                  new XAttribute("SpotName", x.SampleName),
                                                                                  new XAttribute("Measured_L", x.CielabL),
                                                                                  new XAttribute("Measured_a", x.CielabA),
                                                                                  new XAttribute("Measured_b", x.CielabB)
                                                                             )).ToList());

                    DataAccessFactory.
                        Instance.
                        ExecuteProcedure("CPSpot.CreateMeasurement",
                                         new List<Parameters>()
                                                 {
                                                     new Parameters() {ParameterName = "@MeasurementName", Size = 50, Type = DbType.String, Value = measurement.MeasurementName},
                                                     new Parameters() {ParameterName = "@OptimizeRunID", Type = DbType.Int32, Value = measurement.OptimizeRunId},
                                                     new Parameters() {ParameterName = "@MeasurementDate", Type = DbType.DateTime, Value = measurement.MeasurementDate},
                                                     new Parameters() {ParameterName = "@MeasurementPatches", Value = measurementPatches.ToString()},
                                                     new Parameters() {ParameterName = "@PCSDataType", Type = DbType.Int32, Value = Convert.ToInt32(measurement.PCSDataType)}
                                                 },
                                         new List<Parameters>()
                                                 {
                                                     outParam
                                                 });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_SaveMeasurement"));
            return Convert.ToInt32(outParam.Value);
        }

        public List<Measurement> GetSpotMeasurementsOnRunId(int optimizeRunId)
        {
            var measurements = new List<Measurement>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "CPSpot.GetMeasurementByOptimizeRunID",
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

        public DataTable GetSpotPatchDetailsForCMYKExport(int OptimizeRunId)
        {
            string storedProc = "CPSpot.GetCMYKdataByOptimizeRunID";

            DataTable dtPatchDetails = new DataTable();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { OptimizeRunId });
                    if (dsMeasurements.Tables.Count > 0)
                        dtPatchDetails = dsMeasurements.Tables[0];
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetCMYKdataByOptimizeRunID"));
            return dtPatchDetails;
        }

        public DataTable GetSpotPatchDetailsForChart(int OptimizeRunId)
        {
            string storedProc = "CPSpot.GetSpotPatchChartByOptimizeRunID";

            DataTable dtPatchDetails = new DataTable();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { OptimizeRunId });
                    if (dsMeasurements.Tables.Count > 0)
                        dtPatchDetails = dsMeasurements.Tables[0];
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSpotPatchChartByOptimizeRunID"));
            return dtPatchDetails;
        }
        public DataTable GetSpotProjectSummary(int OptimizeRunId)
        {
            string storedProc = "CPSpot.GetPatchDetailsByOptimizeRunID_Summary";

            DataTable dtPatchDetails = new DataTable();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { OptimizeRunId });
                    if (dsMeasurements.Tables.Count > 0)
                        dtPatchDetails = dsMeasurements.Tables[0];
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSpotProjectSummary"));
            return dtPatchDetails;
        }

        public DataTable GetSpotPatchDetailsForChartBySpotProjectID(int SpotProjectId)
        {
            string storedProc = "CPSpot.GetSpotPatchChartBySpotProjectID";

            DataTable dtPatchDetails = new DataTable();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { SpotProjectId });
                    if (dsMeasurements.Tables.Count > 0)
                        dtPatchDetails = dsMeasurements.Tables[0];
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSpotPatchDetailsForChartBySpotProjectID"));
            return dtPatchDetails;
        }

        public SpotProjectChartSetting GetSpotTargetChartSettingDetails(int SpotProjectId)
        {
            var spotProjectChartSetting = new SpotProjectChartSetting();
            List<SpotProjectChartSetting> listSetting = new List<SpotProjectChartSetting>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "[CPSpot].[GetSpotReferenceChartSetting]",
                                new object[]
                                        {
                                            SpotProjectId
                                        });
                    listSetting =
                        dsMeasurements.
                            Tables[0].
                            AsEnumerable().
                            Select(x => new SpotProjectChartSetting()
                            {
                                SpotProjectChartSettingID = DataReader.ReadInt(x, "SpotProjectChartSettingID"),
                                SpotProjectID = DataReader.ReadInt(x, "SpotProjectID"),
                                ChartName = DataReader.ReadString(x, "ChartName"),
                                NumOfRows = DataReader.ReadInt(x, "NumOfRows"),
                                NumOfCols = DataReader.ReadInt(x, "NumOfCols"),
                                ChartSizeX = DataReader.ReadDecimal(x, "ChartSizeX"),
                                ChartSizeY = DataReader.ReadDecimal(x, "ChartSizeY"),
                                PatchSizeX = DataReader.ReadDecimal(x, "PatchSizeX"),
                                PatchSizeY = DataReader.ReadDecimal(x, "PatchSizeY"),
                                FirstSample = DataReader.ReadString(x, "FirstSample"),
                                ReadDirection = DataReader.ReadString(x, "ReadDirection"),
                                ReadMode = DataReader.ReadString(x, "ReadMode"),
                                Pages = DataReader.ReadInt(x, "Pages"),
                                RowsPerPage = DataReader.ReadInt(x, "RowsPerPage"),
                                ChartBorder = DataReader.ReadInt(x, "ChartBorder"),
                                Vgap = DataReader.ReadInt(x, "Vgap"),
                                Hgap = DataReader.ReadInt(x, "Hgap"),
                                GapLeval = DataReader.ReadInt(x, "GapLeval"),
                                PreviousL = DataReader.ReadInt(x, "PreviousL"),
                                ChartRes = DataReader.ReadInt(x, "ChartRes")
                            }).ToList();

                    if (listSetting.Count > 0)
                        spotProjectChartSetting = listSetting[0];
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetAnalyzeChartMeasurement"));
            return spotProjectChartSetting;
        }

        public bool SaveSpotProjectLABValues(List<SpotProjectLAB> spotProjectLabs)
        {
            string storedProc = "CPSpot.UpdateSpotProjectLAB";
            var outParam = new Parameters() { ParameterName = "@IsSuccess", Type = DbType.Int32 };
            Common.ImplementBusinessLogic(
                () =>
                {
                    var spotProjectLabXML = new XElement("SpotProjectLabs",
                                                          spotProjectLabs.
                                                              Select(x =>
                                                                     new XElement("Labs",
                                                                                  new XAttribute("SpotProjectLABID", x.SpotProjectLABID),
                                                                                  new XAttribute("IsSelected", x.IsSelected)
                                                                         )).ToList());
                    DataAccessFactory.
                        Instance.
                        ExecuteProcedure(storedProc,
                                         new List<Parameters>()
                                                 {
                                                     new Parameters() {ParameterName = "@SpotProjectLabsXML", Value = spotProjectLabXML.ToString()},
                                                 },
                                         new List<Parameters>()
                                                 {
                                                     outParam
                                                 });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_SaveMeasurement"));
            return Convert.ToInt32(outParam.Value) == 1 ? true : false;
        }


        public DataTable GetMeasurementValidation(int ProjectID, int OptimizeRunId, string ProjectType)
        {
            string storedProc = "CP.GetValidateMeasurement";

            DataTable dtProjectDetails = new DataTable();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { ProjectID, OptimizeRunId, ProjectType });
                    if (dsMeasurements.Tables.Count > 0)
                        dtProjectDetails = dsMeasurements.Tables[0];
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetValidateMeasurement"));
            return dtProjectDetails;
        }
        public DataSet GetReportDataOnSpotProject(int OptimizationRunId)
        {
            var measurements = new DataSet();
            string storedProc = "Report.GetSpotProjectOptimizeDetails";
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements = new DataSet();
                    dsMeasurements = DataAccessFactory.
                             Instance.
                             GetDsResult(
                                 storedProc,
                                 new object[] { OptimizationRunId });
                    measurements = dsMeasurements;
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_Report_GetSpotProjectOptimizeDetails"));
            return measurements;
        }

        public Measurement GetMeasurementPatchDetailsForExport(int MeasurementId)
        {
            string storedProc = "CPSync4D.GetMeasurementDetails_FileExport";
            Measurement measurement = new Measurement();
            measurement.MeasurementAppPatches = new List<MeasurementPatchApp>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    DataTable dtPatchDetails = new DataTable();
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });
                    if (dsMeasurements.Tables.Count > 0)
                    {
                        dtPatchDetails = dsMeasurements.Tables[0];
                        foreach (DataRow dr in dtPatchDetails.Rows)
                        {

                            measurement.MeasurementAppPatches.Add(new MeasurementPatchApp()
                            {

                                SampleID = Convert.ToInt32(dr["SampleID"]),
                                SampleName = Convert.ToString(dr["SampleName"]),

                                PrintStandardTypeID = Convert.ToInt32(dr["PrintStandardTypeID"]),
                                InstrumentTypeID = Convert.ToInt32(dr["InstrumentTypeID"]),
                                ReferenceChartTypeID = Convert.ToInt32(dr["ReferenceChartTypeID"]),


                                Ciexyz = new CieXYZ()
                                {
                                    X = Convert.ToString(dr["CIEXYZ_X"]),
                                    Y = Convert.ToString(dr["CIEXYZ_Y"]),
                                    Z = Convert.ToString(dr["CIEXYZ_Z"]),
                                },
                                Cielab = new CieLAB()
                                {
                                    L = Convert.ToString(dr["CIELAB_L"]),
                                    A = Convert.ToString(dr["CIELAB_a"]),
                                    B = Convert.ToString(dr["CIELAB_b"]),
                                },
                                Δe = Convert.ToString(dr["ΔE"]),
                                Δe94 = Convert.ToString(dr["ΔE94"]),
                                Δe2000 = Convert.ToString(dr["ΔE2000"]),

                                Density = new DensityApp()
                                {
                                    Red = Convert.ToString(dr["Density_C"]),
                                    Green = Convert.ToString(dr["Density_M"]),
                                    Blue = Convert.ToString(dr["Density_Y"]),
                                    Visual = Convert.ToString(dr["Density_V"])
                                },
                                Spectral = string.IsNullOrEmpty(Convert.ToString(dr["MeasurementPatchSpectralID"])) ? null : new SpectralApp()
                                {
                                    nm380 = Convert.ToString(dr["nm380"]),
                                    nm390 = Convert.ToString(dr["nm390"]),
                                    nm400 = Convert.ToString(dr["nm400"]),
                                    nm410 = Convert.ToString(dr["nm410"]),
                                    nm420 = Convert.ToString(dr["nm420"]),
                                    nm430 = Convert.ToString(dr["nm430"]),
                                    nm440 = Convert.ToString(dr["nm440"]),
                                    nm450 = Convert.ToString(dr["nm450"]),
                                    nm460 = Convert.ToString(dr["nm460"]),
                                    nm470 = Convert.ToString(dr["nm470"]),
                                    nm480 = Convert.ToString(dr["nm480"]),
                                    nm490 = Convert.ToString(dr["nm490"]),
                                    nm500 = Convert.ToString(dr["nm500"]),
                                    nm510 = Convert.ToString(dr["nm510"]),
                                    nm520 = Convert.ToString(dr["nm520"]),
                                    nm530 = Convert.ToString(dr["nm530"]),
                                    nm540 = Convert.ToString(dr["nm540"]),
                                    nm550 = Convert.ToString(dr["nm550"]),
                                    nm560 = Convert.ToString(dr["nm560"]),
                                    nm570 = Convert.ToString(dr["nm570"]),
                                    nm580 = Convert.ToString(dr["nm580"]),
                                    nm590 = Convert.ToString(dr["nm590"]),
                                    nm600 = Convert.ToString(dr["nm600"]),
                                    nm610 = Convert.ToString(dr["nm610"]),
                                    nm620 = Convert.ToString(dr["nm620"]),
                                    nm630 = Convert.ToString(dr["nm630"]),
                                    nm640 = Convert.ToString(dr["nm640"]),
                                    nm650 = Convert.ToString(dr["nm650"]),
                                    nm660 = Convert.ToString(dr["nm660"]),
                                    nm670 = Convert.ToString(dr["nm670"]),
                                    nm680 = Convert.ToString(dr["nm680"]),
                                    nm690 = Convert.ToString(dr["nm690"]),
                                    nm700 = Convert.ToString(dr["nm700"]),
                                    nm710 = Convert.ToString(dr["nm710"]),
                                    nm720 = Convert.ToString(dr["nm720"]),
                                    nm730 = Convert.ToString(dr["nm730"]),
                                }
                            });
                            if (!string.IsNullOrEmpty(Convert.ToString(dr["MeasurementPatchSpectralID"])))
                            {

                            }
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementPatchDetailsForExport"));
            return measurement;
        }

        public Measurement Get1DMeasurementPatchDetailsForExport(int MeasurementId)
        {
            string storedProc = "CPSync.GetMeasurementDetails_FileExport";
            Measurement measurement = new Measurement();
            measurement.MeasurementAppPatches = new List<MeasurementPatchApp>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    DataTable dtPatchDetails = new DataTable();
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { MeasurementId });
                    if (dsMeasurements.Tables.Count > 0)
                    {
                        dtPatchDetails = dsMeasurements.Tables[0];
                        foreach (DataRow dr in dtPatchDetails.Rows)
                        {
                            measurement.MeasurementAppPatches.Add(new MeasurementPatchApp()
                            {

                                //SampleID = Convert.ToInt32(dr["SampleID"]),
                                SampleName = Convert.ToString(dr["SampleName"]),

                                PrintStandardTypeID = Convert.ToInt32(dr["PrintStandardTypeID"]),
                                InstrumentTypeID = Convert.ToInt32(dr["InstrumentTypeID"]),
                                ReferenceChartTypeID = Convert.ToInt32(dr["ReferenceChartTypeID"]),

                                Ciexyz = new CieXYZ()
                                {
                                    X = Convert.ToString(dr["CIEXYZ_X"]),
                                    Y = Convert.ToString(dr["CIEXYZ_Y"]),
                                    Z = Convert.ToString(dr["CIEXYZ_Z"]),
                                },
                                Cielab = new CieLAB()
                                {
                                    L = Convert.ToString(dr["CIELAB_L"]),
                                    A = Convert.ToString(dr["CIELAB_a"]),
                                    B = Convert.ToString(dr["CIELAB_b"]),
                                },
                                //Δe = Convert.ToString(dr["ΔE"]),
                                //Δe94 = Convert.ToString(dr["ΔE94"]),
                                //Δe2000 = Convert.ToString(dr["ΔE2000"]),

                                Density = new DensityApp()
                                {
                                    Red = Convert.ToString(dr["Density_C"]),
                                    Green = Convert.ToString(dr["Density_M"]),
                                    Blue = Convert.ToString(dr["Density_Y"]),
                                    Visual = Convert.ToString(dr["Density_V"])
                                },
                                Spectral = string.IsNullOrEmpty(Convert.ToString(dr["MeasurementPatchSpectralID"])) ? null : new SpectralApp()
                                {
                                    nm380 = Convert.ToString(dr["nm380"]),
                                    nm390 = Convert.ToString(dr["nm390"]),
                                    nm400 = Convert.ToString(dr["nm400"]),
                                    nm410 = Convert.ToString(dr["nm410"]),
                                    nm420 = Convert.ToString(dr["nm420"]),
                                    nm430 = Convert.ToString(dr["nm430"]),
                                    nm440 = Convert.ToString(dr["nm440"]),
                                    nm450 = Convert.ToString(dr["nm450"]),
                                    nm460 = Convert.ToString(dr["nm460"]),
                                    nm470 = Convert.ToString(dr["nm470"]),
                                    nm480 = Convert.ToString(dr["nm480"]),
                                    nm490 = Convert.ToString(dr["nm490"]),
                                    nm500 = Convert.ToString(dr["nm500"]),
                                    nm510 = Convert.ToString(dr["nm510"]),
                                    nm520 = Convert.ToString(dr["nm520"]),
                                    nm530 = Convert.ToString(dr["nm530"]),
                                    nm540 = Convert.ToString(dr["nm540"]),
                                    nm550 = Convert.ToString(dr["nm550"]),
                                    nm560 = Convert.ToString(dr["nm560"]),
                                    nm570 = Convert.ToString(dr["nm570"]),
                                    nm580 = Convert.ToString(dr["nm580"]),
                                    nm590 = Convert.ToString(dr["nm590"]),
                                    nm600 = Convert.ToString(dr["nm600"]),
                                    nm610 = Convert.ToString(dr["nm610"]),
                                    nm620 = Convert.ToString(dr["nm620"]),
                                    nm630 = Convert.ToString(dr["nm630"]),
                                    nm640 = Convert.ToString(dr["nm640"]),
                                    nm650 = Convert.ToString(dr["nm650"]),
                                    nm660 = Convert.ToString(dr["nm660"]),
                                    nm670 = Convert.ToString(dr["nm670"]),
                                    nm680 = Convert.ToString(dr["nm680"]),
                                    nm690 = Convert.ToString(dr["nm690"]),
                                    nm700 = Convert.ToString(dr["nm700"]),
                                    nm710 = Convert.ToString(dr["nm710"]),
                                    nm720 = Convert.ToString(dr["nm720"]),
                                    nm730 = Convert.ToString(dr["nm730"]),
                                }
                            });
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_Get1DMeasurementPatchDetailsForExport"));
            return measurement;
        }


        public Measurement GetSpotMeasurementPatchDetailsForExport(int OptimizeRunId)
        {
            string storedProc = "CPSpot.GetMeasurementDetails_FileExport";
            Measurement measurement = new Measurement();
            measurement.MeasurementAppPatches = new List<MeasurementPatchApp>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    DataTable dtPatchDetails = new DataTable();
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                storedProc,
                                new object[] { OptimizeRunId });
                    if (dsMeasurements.Tables.Count > 0)
                    {
                        dtPatchDetails = dsMeasurements.Tables[0];

                        foreach (DataRow dr in dtPatchDetails.Rows)
                        {
                            measurement.MeasurementAppPatches.Add(new MeasurementPatchApp()
                            {

                                SampleID = Convert.ToInt32(dr["SpotID"]),
                                SampleName = Convert.ToString(dr["SpotName"]),
                                Cielab = new CieLAB()
                                {
                                    L = Convert.ToString(dr["Measured_L"]),
                                    A = Convert.ToString(dr["Measured_a"]),
                                    B = Convert.ToString(dr["Measured_b"]),
                                }
                            });
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementPatchDetailsForExport"));
            return measurement;
        }

        
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
