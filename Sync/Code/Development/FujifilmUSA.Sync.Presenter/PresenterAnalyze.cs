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
            _viewAnalyze.Measurements = measurementBusiness.GetAnalyzeChartMeasurement(_viewAnalyze.OptimizeRunId,_viewAnalyze.From_ΔE);
        }
        
        public void UpdateMeasurementByAnalyzeChart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            measurementBusiness.UpdateMeasurementByAnalyzeChart(_viewAnalyze.Measurement);
        }

        public void GetAvgDeltaMeasurement()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.MeasurementsDelta = measurementBusiness.GetAvgDeltaMeasurement(_viewAnalyze.MeasurementId, _viewAnalyze.From_ΔE);
        }

        public void GetPatchDetailsofMeasurement()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.PatchDetails = measurementBusiness.GetPatchDetailsofMeasurement(_viewAnalyze.MeasurementId);
        }

        public void GetInkPaperChart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.DTDeltaEPrimaryPatches = measurementBusiness.GetDeltaEPatchChartData(_viewAnalyze.MeasurementId, _viewAnalyze.From_ΔE);
            _viewAnalyze.DTPaperPatches = measurementBusiness.GetInkPaperChartData(_viewAnalyze.MeasurementId, _viewAnalyze.From_ΔE);
        }

        public void GetGrayChart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.DTGaumatChart1 = measurementBusiness.GetGamutChart1(_viewAnalyze.MeasurementId);
            _viewAnalyze.DTGaumatChart2 = measurementBusiness.GetGamutChart2(_viewAnalyze.MeasurementId);
            _viewAnalyze.DTLABChart = measurementBusiness.GetLABChart(_viewAnalyze.MeasurementId);
        }    
    }
}
