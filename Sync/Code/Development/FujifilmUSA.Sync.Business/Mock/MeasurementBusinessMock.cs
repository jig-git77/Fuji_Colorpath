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

        public List<Measurement> GetAnalyzeChartMeasurement(int optimizeRunId)
        {
            return new List<Measurement>();
        }

        public int UpdateMeasurementByAnalyzeChart(Measurement measurement)
        {
            return 0;
        }

        public List<MeasurementPatch_AvgDelta> GetAvgDeltaMeasurement(int optimizeRunId)
        {
            return new List<MeasurementPatch_AvgDelta>();
        }

        public List<MeasurementPatchDetails> GetPatchDetailsofMeasurement(int optimizeRunId, int MeasurementId)
        {
            return new List<MeasurementPatchDetails>();
        }

        public List<AnalyzeChart> GetDeltaEPatchChartData(int optimizeRunId, int MeasurementId)
        {
            return new List<AnalyzeChart>();
        }

        public List<AnalyzeChart> GetInkPaperChartData(int optimizeRunId, int MeasurementId)
        {
            return new List<AnalyzeChart>();
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
