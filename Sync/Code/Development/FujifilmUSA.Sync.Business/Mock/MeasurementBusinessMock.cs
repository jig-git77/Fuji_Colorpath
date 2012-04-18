using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business.Messages;
using FujifilmUSA.Sync.Entity;

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
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
