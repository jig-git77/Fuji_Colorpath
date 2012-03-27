using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterPluginDataService
    {
        private readonly IViewPluginDataService _viewPluginDataService;

        public PresenterPluginDataService(IViewPluginDataService viewPluginDataService)
        {
            _viewPluginDataService = viewPluginDataService;
        }

        public void SaveMeasurement()
        {
            var measurementBusiness = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            measurementBusiness.SaveMeasurement(_viewPluginDataService.Measurement);
        }

        public void GetReferenceChartData()
        {
            var projectBusiness = Business.BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            _viewPluginDataService.ReferenceChartData = projectBusiness.GetReferenceChartData(_viewPluginDataService.ProjectId);
        }

        public void GetMeasurementsOnRunId()
        {
            var measurementBusiness = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            _viewPluginDataService.Measurements = measurementBusiness.GetMeasurementsOnRunId(_viewPluginDataService.OptimizeRunId);
        }
    }
}
