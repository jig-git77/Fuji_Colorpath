using FujifilmUSA.Sync.Business.Messages;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using System.Data;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMeasurementBusiness : ITaskBase
    {
        MeasurementDataResponse GetMeasurementData(List<int> measurementIds);
        int SaveMeasurement(Measurement measurement);
        List<Measurement> GetMeasurementsOnRunId(int optimizeRunId);
        List<Measurement> GetAnalyzeChartMeasurement(int optimizeRunId);
        int UpdateMeasurementByAnalyzeChart(Measurement measurement);
        List<MeasurementPatch_AvgDelta> GetAvgDeltaMeasurement(int optimizeRunId);
        List<MeasurementPatchDetails> GetPatchDetailsofMeasurement(int optimizeRunId, int MeasurementId);
        List<AnalyzeChart> GetDeltaEPatchChartData(int optimizeRunId, int MeasurementId);
        List<AnalyzeChart> GetInkPaperChartData(int optimizeRunId, int MeasurementId);
        List<AnalyzeChart_Gray> GetGamutChart1(int optimizeRunId, int MeasurementId);
        List<AnalyzeChart_Gray> GetGamutChart2(int optimizeRunId, int MeasurementId);
    }
}
