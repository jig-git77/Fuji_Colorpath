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
        List<MeasurementPatch_AvgDelta> GetAvgDeltaMeasurement(int MeasurementId);
        List<MeasurementPatchDetails> GetPatchDetailsofMeasurement(int MeasurementId);
        List<AnalyzeChart> GetDeltaEPatchChartData(int MeasurementId);
        List<AnalyzeChart> GetInkPaperChartData(int MeasurementId);
        List<AnalyzeChart_Gray> GetGamutChart1(int MeasurementId);
        List<AnalyzeChart_Gray> GetGamutChart2(int MeasurementId);
        List<AnalyzeChart_Gray> GetLABChart(int MeasurementId);
    }
}
