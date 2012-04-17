using System.Linq;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;

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
                    _viewAnalyze.DlpDescription);

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
            _viewAnalyze.Measurements = measurementBusiness.GetAnalyzeChartMeasurement(_viewAnalyze.OptimizeRunId);
        }
        public void UpdateMeasurementByAnalyzeChart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            measurementBusiness.UpdateMeasurementByAnalyzeChart(_viewAnalyze.Measurement);
        }
    }
}
