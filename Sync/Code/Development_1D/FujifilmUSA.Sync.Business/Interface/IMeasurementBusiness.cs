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
        List<Measurement> GetAnalyzeChartMeasurement(int optimizeRunId, string From_ΔE, bool IsSync1DType = false);
        List<Measurement> GetAnalyzeChartVMeasurement(int projectId, string From_ΔE);
        int UpdateMeasurementByAnalyzeChart(Measurement measurement, bool IsVerification = false);
        List<MeasurementPatch_AvgDelta> GetAvgDeltaMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false);
            
        DataTable GetPatchDetailsofMeasurement(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);

        List<AnalyzeChart>[] GetChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart> GetDeltaEPatchChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart> GetInkPaperChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart> GetDensityChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false);

        List<AnalyzeChart_Gray>[] GetGamutChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart_Gray> GetGamutChart1(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart_Gray> GetGamutChart2(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart_Gray> GetLABChart(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);
        DataSet GetReportDataOnMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false, int ValidationType = -1,  bool IsSync1DType = false);
        DataSet GetReportDataOnMeasurement_G7_Vali(int MeasurementId, string From_ΔE, bool IsVerification = false, int ValidationType = -1, bool IsSync1DType = false);

        List<Measurement> GetVMeasurementSummary(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false);

        List<AnalyzeChart_G7>[] GetG7ChartData(int MeasurementId, bool IsVerification = false);

        List<AnalyzeChart_G7> GetGrayFChart(int MeasurementId, bool IsVerification = false);
        List<AnalyzeChart_G7> GetGrayFSummary(int MeasurementId, bool IsVerification = false);       
        List<AnalyzeChart_G7> GetBlackLChart(int MeasurementId, bool IsVerification = false);
        List<AnalyzeChart_G7> GetBlackLSummary(int MeasurementId, bool IsVerification = false);       
        List<AnalyzeChart_G7> GetGrayLChart(int MeasurementId, bool IsVerification = false);
        List<AnalyzeChart_G7> GetGrayLSummary(int MeasurementId, bool IsVerification = false);       
    
        int Save1DMeasurement(Measurement measurement);
        List<Measurement> Get1DMeasurementsOnRunId(int optimizeRunId);
        MeasurementDataResponse Get1DMeasurementData(List<int> measurementIds);
      
        Dictionary<string, string> GetTargetTVIData(int projectId);

        List<AnalyzeChart_DotGain> GetTVIChart(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart_DotGain> GetISOTVIChart(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart_DotGain> GetSpreadChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart_DotGain> GetSpreadSplineRangeChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart_DotGain> GetMaxSpreadChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);
    }
}
