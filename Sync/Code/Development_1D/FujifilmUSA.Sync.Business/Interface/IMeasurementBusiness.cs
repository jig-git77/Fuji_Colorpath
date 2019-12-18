using FujifilmUSA.Sync.Business.Messages;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using System.Data;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMeasurementBusiness : ITaskBase
    {
        MeasurementDataResponse GetMeasurementData(List<int> measurementIds, PCSDataTypes PCSDataType);
        bool SaveMeasurement(Measurement measurement, int UserID);
        bool SaveVMeasurement(Measurement measurement, int UserID);
        List<Measurement> GetMeasurementsOnRunId(int optimizeRunId);
        List<Measurement> GetVMeasurementsOnProjectId(int projectId);
        List<Measurement> GetAnalyzeChartMeasurement(int optimizeRunId, string From_ΔE, bool IsSync1DType = false);
        List<Measurement> GetAnalyzeChartVMeasurement(int projectId, string From_ΔE);
        int UpdateMeasurementByAnalyzeChart(Measurement measurement, bool IsVerification = false, bool IsSync1DType = false);
        List<MeasurementPatch_AvgDelta> GetAvgDeltaMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false);
            
        DataTable GetPatchDetailsofMeasurement(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);

        List<AnalyzeChart>[] GetChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false);
        List<AnalyzeChart> GetDeltaEPatchChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart> GetInkPaperChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart> GetDensityChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false);

        List<AnalyzeChart_Gray>[] GetGamutChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false);
        List<AnalyzeChart_Gray> GetGamutChart1(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart_Gray> GetGamutChart2(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);
        List<AnalyzeChart_Gray> GetLABChart(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false);
        DataSet GetReportDataOnMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false, int ValidationType = -1, bool IsSync1DType = false, bool IsAverageOfMeasurements = false);
        DataSet GetReportDataOnMeasurement_G7_Vali(int MeasurementId, string From_ΔE, bool IsVerification = false, int ValidationType = -1, bool IsSync1DType = false, bool IsAverageOfMeasurements = false);

        List<Measurement> GetVMeasurementSummary(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false);

        List<AnalyzeChart_G7>[] GetG7ChartData(int MeasurementId, bool IsVerification = false, bool IsAverageOfMeasurements = false);

    
        bool Save1DMeasurement(Measurement measurement, int UserID);
        List<Measurement> Get1DMeasurementsOnRunId(int optimizeRunId);
        MeasurementDataResponse Get1DMeasurementData(List<int> measurementIds, PCSDataTypes PCSDataType);

        Dictionary<string, string> GetTargetTVIData(int projectId, bool is1DMeasurementSmoothing);

        List<AnalyzeChart_DotGain> GetTVIChart(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false);
        List<AnalyzeChart_DotGain> GetISOTVIChart(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false);
        List<AnalyzeChart_DotGain> GetSpreadChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false);
        List<AnalyzeChart_DotGain> GetSpreadChartData_CMYK(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false);
        List<AnalyzeChart_DotGain> GetSpreadSplineRangeChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false);
        DataTable GetMaxSpreadChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false);

        int ChkAvgMeasurementCompatibility(int optimizeRunId);
        DataSet GetMeasurementDataForScoring(int MeasurementId, string From_ΔE, int ProjectId, bool IsAverageOfMeasurements = false);
        DataSet GetDensityData_C(int OptimizeRunId);
        DataTable GetSpotPatchDetailsofMeasurement(int OptimizeRunId);
        bool SaveSpotMeasurement(Measurement measurement, int UserID);
        List<Measurement> GetSpotMeasurementsOnRunId(int optimizeRunId);
        int UpdateMeasurementByAnalyzeChartFromService(int MeasurementId, bool isUsedMeasurement, bool IsVerification = false, bool IsSync1DType = false);
        DataTable GetSpotPatchDetailsForCMYKExport(int OptimizeRunId);
        DataTable GetSpotPatchDetailsForChart(int OptimizeRunId);
        DataTable GetSpotProjectSummary(int OptimizeRunId);
        DataTable GetSpotPatchDetailsForChartBySpotProjectID(int SpotProjectId);
        SpotProjectChartSetting GetSpotTargetChartSettingDetails(int SpotProjectId);
        bool SaveSpotProjectLABValues(List<SpotProjectLAB> spotProjectLabs);
        DataTable GetMeasurementValidation(int ProjectID, int OptimizeRunId, string ProjectType);
        DataSet GetReportDataOnSpotProject(int OptimizationRunId);
        Measurement GetMeasurementPatchDetailsForExport(int MeasurementId);
        Measurement Get1DMeasurementPatchDetailsForExport(int MeasurementId);
        Measurement GetSpotMeasurementPatchDetailsForExport(int OptimizeRunId);
        Measurement GetValidationMeasurementPatchDetailsForExport(int MeasurementId);
        int ImportMeasurement(Measurement measurement, int LicenseTypeID);
        List<MeasurementLog> GetMeasurementErrorLogList();
        DataTable GetSpotPatchDetailsForCMYKExportBySpotProject(string spotProjectIds);
        DataTable ValidateApplicationMeasurement(int optimizationRunId, int projectId, string SerialNo);
        void SaveMeasurementErrorFile(int optimizeRunId, int projectid, string requestMeasurement);
    }
}
