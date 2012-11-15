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
        int SaveVMeasurement(Measurement measurement);
        List<Measurement> GetMeasurementsOnRunId(int optimizeRunId);
        List<Measurement> GetVMeasurementsOnProjectId(int projectId);        
        List<Measurement> GetAnalyzeChartMeasurement(int optimizeRunId, string From_ΔE);
        List<Measurement> GetAnalyzeChartVMeasurement(int projectId, string From_ΔE);
        int UpdateMeasurementByAnalyzeChart(Measurement measurement, bool IsVerification = false);
        List<MeasurementPatch_AvgDelta> GetAvgDeltaMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false);
        List<MeasurementPatchDetails> GetPatchDetailsofMeasurement(int MeasurementId, bool IsVerification = false);
        List<AnalyzeChart> GetDeltaEPatchChartData(int MeasurementId, string From_ΔE, bool IsVerification = false);
        List<AnalyzeChart> GetInkPaperChartData(int MeasurementId, string From_ΔE, bool IsVerification = false);
        List<AnalyzeChart_Gray> GetGamutChart1(int MeasurementId, bool IsVerification = false);
        List<AnalyzeChart_Gray> GetGamutChart2(int MeasurementId, bool IsVerification = false);
        List<AnalyzeChart_Gray> GetLABChart(int MeasurementId, bool IsVerification = false);
        DataSet GetReportDataOnMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false);

        List<Measurement> GetVMeasurementSummary(int MeasurementId, string From_ΔE, bool IsVerification = false);

        List<AnalyzeChart_G7> GetGrayFChart(int MeasurementId, bool IsVerification = false);
        List<AnalyzeChart_G7> GetGrayQuartileSummary(int MeasurementId, bool IsVerification = false);
        //List<AnalyzeChart_G7> GetNPDChart1(int MeasurementId, bool IsVerification = false);
        //List<AnalyzeChart_G7> GetNPDChart2(int MeasurementId, bool IsVerification = false);
    }
}
