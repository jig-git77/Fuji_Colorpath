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
                                            OptimizeRunId = DataReader.ReadInt(x, "OptimizeRunID"),
                                            IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementsOnRunId"));
            return measurements;
        }

        public List<Measurement> GetAnalyzeChartMeasurement(int optimizeRunId)
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
                                            optimizeRunId
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
                                                                             Δe = DataReader.ReadDecimal(y, "ΔE"),
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

        public List<MeasurementPatch_AvgDelta> GetAvgDeltaMeasurement(int optimizeRunId)
        {
            var measurements = new List<MeasurementPatch_AvgDelta>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "CPSync4D.GetMeasurementDeltasByOptimizeRunID",
                                new object[] { optimizeRunId });
                    measurements = dsMeasurements.
                        Tables[0].
                        AsEnumerable().
                        Select(x => new MeasurementPatch_AvgDelta()
                        {
                            MeasurementId = DataReader.ReadInt(x, "MeasurementID"),
                            MeasurementName = DataReader.ReadString(x, "MeasurementName"),
                            MeasurementDate = DataReader.ReadDate(x, "MeasurementDate"),
                            //OptimizeRunId = DataReader.ReadInt(x, "OptimizeRunID"),
                            Avg_ΔE = DataReader.ReadDecimal(x, "Avg_ΔE"),
                            Avg_ΔE94 = DataReader.ReadDecimal(x, "Avg_ΔE94"),
                            Avg_ΔE2000 = DataReader.ReadDecimal(x, "Avg_ΔE2000")
                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementsDeltaOnRunId"));
            return measurements;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}