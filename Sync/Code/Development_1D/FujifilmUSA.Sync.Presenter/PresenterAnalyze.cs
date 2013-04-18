using System.Linq;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterAnalyze
    {
        private IViewAnalyze _viewAnalyze;
        public PresenterAnalyze(IViewAnalyze viewAnalyze)
        {
            _viewAnalyze = viewAnalyze;
        }

        public void CreateDestinationAndDlp()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.
                CreateInitialDestinationAndDeviceLinkProfiles(
                    _viewAnalyze.ProjectId,
                    _viewAnalyze.OptimizeRunId,
                    _viewAnalyze.MeasurementIds,
                    _viewAnalyze.DestinationProfileDescription,
                    _viewAnalyze.DlpDescription,
                    Convert.ToInt32(EOptimizeRunStatus.Closed),
                    DateTime.Now);
        }

        public void GetMeasurementsOnRunId()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.MeasurementIds = measurementBusiness.GetMeasurementsOnRunId(_viewAnalyze.OptimizeRunId).Where(x => x.IsUsed).Select(x => x.MeasurementId).ToList();
        }

        public void GetAnalyzeChartMeasurement()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.Measurements = measurementBusiness.GetAnalyzeChartMeasurement(_viewAnalyze.OptimizeRunId, _viewAnalyze.From_ΔE, _viewAnalyze.IsSync1DType);
        }

        public void GetAnalyzeChartVMeasurement()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.Measurements = measurementBusiness.GetAnalyzeChartVMeasurement(_viewAnalyze.ProjectId, _viewAnalyze.From_ΔE);
        }

        public void UpdateMeasurementByAnalyzeChart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            measurementBusiness.UpdateMeasurementByAnalyzeChart(_viewAnalyze.Measurement, _viewAnalyze.IsVerification);
        }

        public void GetAvgDeltaMeasurement_Optimization()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.MeasurementsDelta = measurementBusiness.GetAvgDeltaMeasurement(_viewAnalyze.MeasurementId, _viewAnalyze.From_ΔE, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType);
        }

        public void GetAvgDeltaMeasurement_Validation()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.MeasurementSummary = measurementBusiness.GetVMeasurementSummary(_viewAnalyze.MeasurementId, _viewAnalyze.From_ΔE, _viewAnalyze.IsVerification);
        }

        public void GetPatchDetailsofMeasurement()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.dtPatchDetails = measurementBusiness.GetPatchDetailsofMeasurement(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification);
        }

        public void GetInkPaperChart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.DTDeltaEPrimaryPatches = measurementBusiness.GetDeltaEPatchChartData(_viewAnalyze.MeasurementId, _viewAnalyze.From_ΔE, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType);
            _viewAnalyze.DTPaperPatches = measurementBusiness.GetInkPaperChartData(_viewAnalyze.MeasurementId, _viewAnalyze.From_ΔE, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType);
        }

        public void GetGrayChart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.DTGaumatChart1 = measurementBusiness.GetGamutChart1(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification);
            _viewAnalyze.DTGaumatChart2 = measurementBusiness.GetGamutChart2(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification);
            _viewAnalyze.DTLABChart = measurementBusiness.GetLABChart(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification);
        }

        public void GetG7Chart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.GrayFChart = measurementBusiness.GetGrayFChart(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification);
            _viewAnalyze.GrayFSummary = measurementBusiness.GetGrayFSummary(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification);
            _viewAnalyze.BlackLChart = measurementBusiness.GetBlackLChart(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification);
            _viewAnalyze.BlackLSummary = measurementBusiness.GetBlackLSummary(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification);
            _viewAnalyze.GrayLChart = measurementBusiness.GetGrayLChart(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification);
            _viewAnalyze.GrayLSummary = measurementBusiness.GetGrayLSummary(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification);
            //_viewAnalyze.BlackNPDChart = measurementBusiness.GetBlackNPDChart(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification);
            //_viewAnalyze.GrayNPDChart = measurementBusiness.GetGrayNPDChart(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification);
        }

        public void Get1DMeasurementsOnRunId()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.MeasurementIds = measurementBusiness.Get1DMeasurementsOnRunId(_viewAnalyze.OptimizeRunId).Where(x => x.IsUsed).Select(x => x.MeasurementId).ToList();
        }

        public void CreateG7AlignmentCurve()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.CreateG7AlignmentCurve(
                _viewAnalyze.ProjectId,
                _viewAnalyze.OptimizeRunId,
                _viewAnalyze.MeasurementIds,
                _viewAnalyze.useDLP,              
                Convert.ToInt32(EOptimizeRunStatus.Closed),
                    DateTime.Now);

        }

        public void CreateISOAlignmentCurve()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.CreateISOAlignmentCurve(
                _viewAnalyze.ProjectId,
                 _viewAnalyze.OptimizeRunId,
                _viewAnalyze.MeasurementIds,
                _viewAnalyze.useDLP,                
                Convert.ToInt32(EOptimizeRunStatus.Closed),
                    DateTime.Now);

        }
    }
}
