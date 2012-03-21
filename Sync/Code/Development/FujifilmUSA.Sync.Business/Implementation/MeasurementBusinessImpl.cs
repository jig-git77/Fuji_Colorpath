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
            var dsPcsDcsData = DataAccessFactory.Instance.GetDsResult("CPSync4D.GetMeasurementData", new object[] { String.Join(",", measurementIds) });
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
            return response;
        }

        public int SaveMeasurement(Measurement measurement)
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
            var outParam = new Parameters() { ParameterName = "@MeasurementID", Type = DbType.Int32 };
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
            return Convert.ToInt32(outParam.Value);
        }

        public List<Measurement> GetMeasurementsOnRunId(int optimizeRunId)
        {
            var dsMeasurements = DataAccessFactory.Instance.GetDsResult("CPSync4D.GetMeasurementByOptimizeRunID", new object[] { optimizeRunId });
            var measurements = dsMeasurements.
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
            return measurements;
        }

        public List<Measurement> GetAnalyzeChartMeasurement(int optimizeRunId)
        {
            var dsMeasurements = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[GetAnalyzeChartMeasurement]", new object[] { optimizeRunId });
            var measurements =
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
            return measurements;
        }

        public int UpdateMeasurementByAnalyzeChart(Measurement measurement)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspUpdateMeasurementByAnalyzeChart]", new object[]{
                                                                                                                measurement.MeasurementId,     
                                                                                                                measurement.IsUsed
                                                                                                 });
            return returnValue;

        }



        #endregion
    }
}