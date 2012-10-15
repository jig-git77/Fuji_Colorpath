using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business.Messages;
using FujifilmUSA.Sync.Entity;
using System.Data;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MeasurementBusinessMock : IMeasurementBusiness
    {
        #region IMeasurementBusiness Members

        public MeasurementDataResponse GetMeasurementData(List<int> measurementIds)
        {
            return new MeasurementDataResponse();
        }

        public int SaveMeasurement(Measurement measurement)
        {
            return 0;
        }

        public List<Measurement> GetMeasurementsOnRunId(int optimizeRunId)
        {
            return new List<Measurement>();
        }

        public List<Measurement> GetAnalyzeChartMeasurement(int optimizeRunId, string From_ΔE)
        {
            return new List<Measurement>();
        }

        public int UpdateMeasurementByAnalyzeChart(Measurement measurement)
        {
            return 0;
        }

        public List<MeasurementPatch_AvgDelta> GetAvgDeltaMeasurement(int MeasurementId, string From_ΔE)
        {
            return new List<MeasurementPatch_AvgDelta>();
        }

        public List<MeasurementPatchDetails> GetPatchDetailsofMeasurement(int MeasurementId)
        {
            return new List<MeasurementPatchDetails>();
        }

        public List<AnalyzeChart> GetDeltaEPatchChartData(int MeasurementId, string From_ΔE)
        {
            return new List<AnalyzeChart>();
        }

        public List<AnalyzeChart> GetInkPaperChartData(int MeasurementId, string From_ΔE)
        {
            return new List<AnalyzeChart>();
        }

        public List<AnalyzeChart_Gray> GetGamutChart1(int MeasurementId)
        {
            return new List<AnalyzeChart_Gray>();
        }
        public List<AnalyzeChart_Gray> GetGamutChart2(int MeasurementId)
        {
            return new List<AnalyzeChart_Gray>();
        }

        public List<AnalyzeChart_Gray> GetLABChart(int MeasurementId)
        {
            return new List<AnalyzeChart_Gray>();
        }

        public DataSet GetReportDataOnMeasurement(int MeasurementId, string From_ΔE)
        {
            return new DataSet();
        }

        public List<Measurement> GetVMeasurementsOnProjectId(int projectId)
        {
            return new List<Measurement>();
        }

        public int SaveVMeasurement(Measurement measurement)
        {
            return 0;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
