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

        public void SaveVMeasurement()
        {
            var measurementBusiness = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            measurementBusiness.SaveVMeasurement(_viewPluginDataService.Measurement);
        }

        public void GetReferenceChartData()
        {
            var projectBusiness = Business.BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            _viewPluginDataService.ReferenceChartData = projectBusiness.GetReferenceChartData(_viewPluginDataService.ProjectId,
                                                                                              _viewPluginDataService.ReferenceChartDisType);
        }

        public void GetMeasurementsOnRunId()
        {
            var measurementBusiness = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            _viewPluginDataService.Measurements = measurementBusiness.GetMeasurementsOnRunId(_viewPluginDataService.OptimizeRunId);
        }

        public void GetVMeasurementsOnProjectId()
        {
            var measurementBusiness = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            _viewPluginDataService.Measurements = measurementBusiness.GetVMeasurementsOnProjectId(_viewPluginDataService.ProjectId);
        }

        public bool DeleteMeasurement()
        {
            var optimizeRunBusiness = Business.BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            return optimizeRunBusiness.DeleteMeasurement(_viewPluginDataService.MeasurementId);
        }

        public bool DeleteVMeasurement()
        {
            var optimizeRunBusiness = Business.BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            return optimizeRunBusiness.DeleteVMeasurement(_viewPluginDataService.MeasurementId);
        }

        public void GetDensityChartsData()
        {
            var optimizeRunBusiness = Business.BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            _viewPluginDataService.DensityChartsData = optimizeRunBusiness.GetDensityChartsData(_viewPluginDataService.OptimizeRunId);
        }
    }
}
