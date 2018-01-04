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

        public MeasurementDataResponse GetMeasurementData(List<int> measurementIds, PCSDataTypes PCSDataType)
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

        public List<Measurement> GetAnalyzeChartMeasurement(int optimizeRunId, string From_ΔE, bool IsSync1DType = false)
        {
            return new List<Measurement>();
        }

        public List<Measurement> GetAnalyzeChartVMeasurement(int projectId, string From_ΔE)
        {
            return new List<Measurement>();
        }

        public int UpdateMeasurementByAnalyzeChart(Measurement measurement, bool IsVerification = false, bool IsSync1DType = false)
        {
            return 0;
        }

        public List<MeasurementPatch_AvgDelta> GetAvgDeltaMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false)
        {
            return new List<MeasurementPatch_AvgDelta>();
        }


        public DataTable GetPatchDetailsofMeasurement(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false)
        {
            return new DataTable();
        }

        public List<AnalyzeChart>[] GetChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            throw new NotImplementedException();
        }

        public List<AnalyzeChart_Gray>[] GetGamutChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            throw new NotImplementedException();
        }

        public List<AnalyzeChart_G7>[] GetG7ChartData(int MeasurementId, bool IsVerification = false, bool IsAverageOfMeasurements = false)
        {
            throw new NotImplementedException();
        }

        public List<AnalyzeChart> GetDeltaEPatchChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false)
        {
            return new List<AnalyzeChart>();
        }

        public List<AnalyzeChart> GetInkPaperChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false)
        {
            return new List<AnalyzeChart>();
        }

        public List<AnalyzeChart> GetDensityChartData(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false)
        {
            return new List<AnalyzeChart>();
        }

        public List<AnalyzeChart_Gray> GetGamutChart1(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false)
        {
            return new List<AnalyzeChart_Gray>();
        }
        public List<AnalyzeChart_Gray> GetGamutChart2(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false)
        {
            return new List<AnalyzeChart_Gray>();
        }

        public List<AnalyzeChart_Gray> GetLABChart(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false)
        {
            return new List<AnalyzeChart_Gray>();
        }

        public DataSet GetReportDataOnMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false, int ValidationType = -1, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            return new DataSet();
        }

        public DataSet GetReportDataOnMeasurement_G7_Vali(int MeasurementId, string From_ΔE, bool IsVerification = false, int ValidationType = -1, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
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


        public List<Measurement> GetVMeasurementSummary(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            return new List<Measurement>();
        }

        public int Save1DMeasurement(Measurement measurement)
        {
            return 0;
        }

        public List<Measurement> Get1DMeasurementsOnRunId(int optimizeRunId)
        {
            return new List<Measurement>();
        }

        public MeasurementDataResponse Get1DMeasurementData(List<int> measurementIds, PCSDataTypes PCSDataType)
        {
            return new MeasurementDataResponse();
        }

        public Dictionary<string, string> GetTargetTVIData(int projectId, bool Is1DMeasurementSmoothing)
        {
            return new Dictionary<string, string>();
        }

        public List<AnalyzeChart_DotGain> GetTVIChart(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            return new List<AnalyzeChart_DotGain>();
        }
        public List<AnalyzeChart_DotGain> GetISOTVIChart(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            return new List<AnalyzeChart_DotGain>();
        }
        public List<AnalyzeChart_DotGain> GetSpreadChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            return new List<AnalyzeChart_DotGain>();
        }
        public List<AnalyzeChart_DotGain> GetSpreadChartData_CMYK(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            return new List<AnalyzeChart_DotGain>();
        }
        public List<AnalyzeChart_DotGain> GetSpreadSplineRangeChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            return new List<AnalyzeChart_DotGain>(); 
        }
        public DataTable GetMaxSpreadChartData(int MeasurementId, bool IsVerification = false, bool IsSync1DType = false, bool IsAverageOfMeasurements = false)
        {
            return new DataTable();
        }
        public int ChkAvgMeasurementCompatibility(int optimizeRunId)
        {
            return 0;
        }
        public DataSet GetMeasurementDataForScoring(int MeasurementId, string From_ΔE, int ProjectId, bool IsAverageOfMeasurements = false)
        {
            return new DataSet();
        }
        public DataSet GetDensityData_C(int OptimizeRunId)
        {
            return new DataSet();
        }
        public DataTable GetSpotPatchDetailsofMeasurement(int OperationID)
        {
            return new DataTable();
        }
        public int SaveSpotMeasurement(Measurement measurement)
        {
            return 0;
        }
        public List<Measurement> GetSpotMeasurementsOnRunId(int optimizeRunId)
        {
            return new List<Measurement>();
        }
        public int UpdateMeasurementByAnalyzeChartFromService(int MeasurementId, bool isUsedMeasurement, bool IsVerification = false, bool IsSync1DType = false)
        {
            return 0;
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
