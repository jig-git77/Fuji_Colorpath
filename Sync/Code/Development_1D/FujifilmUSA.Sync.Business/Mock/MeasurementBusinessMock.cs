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

        public List<Measurement> GetAnalyzeChartMeasurement(int optimizeRunId, string From_ΔE, bool IsSync1DType = false)
        {
            return new List<Measurement>();
        }

        public List<Measurement> GetAnalyzeChartVMeasurement(int projectId, string From_ΔE)
        {
            return new List<Measurement>();
        }

        public int UpdateMeasurementByAnalyzeChart(Measurement measurement, bool IsVerification = false)
        {
            return 0;
        }

        public List<MeasurementPatch_AvgDelta> GetAvgDeltaMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false, bool IsSync1DType = false)
        {
            return new List<MeasurementPatch_AvgDelta>();
        }

        public DataTable GetPatchDetailsofMeasurement(int MeasurementId, bool IsVerification = false)
        {
            return new DataTable();
        }

        public List<AnalyzeChart> GetDeltaEPatchChartData(int MeasurementId, string From_ΔE, bool IsVerification = false)
        {
            return new List<AnalyzeChart>();
        }

        public List<AnalyzeChart> GetInkPaperChartData(int MeasurementId, string From_ΔE, bool IsVerification = false)
        {
            return new List<AnalyzeChart>();
        }

        public List<AnalyzeChart_Gray> GetGamutChart1(int MeasurementId,bool IsVerification = false)
        {
            return new List<AnalyzeChart_Gray>();
        }
        public List<AnalyzeChart_Gray> GetGamutChart2(int MeasurementId, bool IsVerification = false)
        {
            return new List<AnalyzeChart_Gray>();
        }

        public List<AnalyzeChart_Gray> GetLABChart(int MeasurementId, bool IsVerification = false)
        {
            return new List<AnalyzeChart_Gray>();
        }

        public DataSet GetReportDataOnMeasurement(int MeasurementId, string From_ΔE, bool IsVerification = false, int ValidationType = -1)
        {
            return new DataSet();
        }

        public DataSet GetReportDataOnMeasurement_G7_Vali(int MeasurementId, string From_ΔE, bool IsVerification = false, int ValidationType = -1)
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


        public List<Measurement> GetVMeasurementSummary(int MeasurementId, string From_ΔE, bool IsVerification = false)
        {
            return new List<Measurement>();
        }

        public List<AnalyzeChart_G7> GetGrayFChart(int MeasurementId, bool IsVerification = false)
        {
            return new List<AnalyzeChart_G7>();
        }
        public List<AnalyzeChart_G7> GetGrayFSummary(int MeasurementId, bool IsVerification = false)
        {
            return new List<AnalyzeChart_G7>();
        }
        public List<AnalyzeChart_G7> GetBlackLChart(int MeasurementId, bool IsVerification = false)
        {
            return new List<AnalyzeChart_G7>();
        }
        public List<AnalyzeChart_G7> GetBlackLSummary(int MeasurementId, bool IsVerification = false)
        {
            return new List<AnalyzeChart_G7>();
        }
        public List<AnalyzeChart_G7> GetGrayLChart(int MeasurementId, bool IsVerification = false)
        {
            return new List<AnalyzeChart_G7>();
        }
        public List<AnalyzeChart_G7> GetGrayLSummary(int MeasurementId, bool IsVerification = false)
        {
            return new List<AnalyzeChart_G7>();
        }
        //public List<AnalyzeChart_G7> GetBlackNPDChart(int MeasurementId, bool IsVerification = false)
        //{
        //    return new List<AnalyzeChart_G7>();
        //}
        //public List<AnalyzeChart_G7> GetGrayNPDChart(int MeasurementId, bool IsVerification = false)
        //{
        //    return new List<AnalyzeChart_G7>();
        //}

        public int Save1DMeasurement(Measurement measurement)
        {
            return 0;
        }

        public List<Measurement> Get1DMeasurementsOnRunId(int optimizeRunId)
        {
            return new List<Measurement>();
        }

        public MeasurementDataResponse Get1DMeasurementData(List<int> measurementIds)
        {
            return new MeasurementDataResponse();
        }

        public TargetTVIDataResponse GetTargetTVIData(int projectId)
        {
            return new TargetTVIDataResponse();
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
