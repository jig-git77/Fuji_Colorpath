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

        public bool SaveMeasurement()
        {
            var measurementBusiness = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            return measurementBusiness.SaveMeasurement(_viewPluginDataService.Measurement, _viewPluginDataService.UserID);
        }

        public bool SaveVMeasurement()
        {
            var measurementBusiness = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            return measurementBusiness.SaveVMeasurement(_viewPluginDataService.Measurement, _viewPluginDataService.UserID);
        }

        public bool Save1DMeasurement()
        {
            var measurementBusiness = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            return measurementBusiness.Save1DMeasurement(_viewPluginDataService.Measurement,_viewPluginDataService.UserID);
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

        public void Get1DMeasurementsOnRunId()
        {
            var measurementBusiness = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            _viewPluginDataService.Measurements = measurementBusiness.Get1DMeasurementsOnRunId(_viewPluginDataService.OptimizeRunId);
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

        public bool Delete1DMeasurement()
        {
            var optimizeRunBusiness = Business.BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            return optimizeRunBusiness.Delete1DMeasurement(_viewPluginDataService.MeasurementId);
        }

        public void GetSpotMeasurementsOnRunId()
        {
            var measurementBusiness = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            _viewPluginDataService.Measurements = measurementBusiness.GetSpotMeasurementsOnRunId(_viewPluginDataService.OptimizeRunId);
        }
        public void GetSpotReferenceChartData()
        {
            var projectBusiness = Business.BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            _viewPluginDataService.ReferenceChartData = projectBusiness.GetSpotReferenceChartData(_viewPluginDataService.SpotProjectId,
                                                                                              _viewPluginDataService.ReferenceChartDisType);
        }

        public bool SaveSpotMeasurement()
        {
            var measurementBusiness = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;            
            return measurementBusiness.SaveSpotMeasurement(_viewPluginDataService.Measurement, _viewPluginDataService.UserID);
        }

        public bool DeleteSpotMeasurement()
        {
            var optimizeRunBusiness = Business.BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            return optimizeRunBusiness.DeleteSpotMeasurement(_viewPluginDataService.MeasurementId);
        }

        public void GetPluginReferenceChartData()
        {
            var projectBusiness = Business.BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            _viewPluginDataService.ReferenceChartPlugin = projectBusiness.GetPluginReferenceChartData(_viewPluginDataService.ProjectId,
                                                                                              _viewPluginDataService.ReferenceChartDisType);
        }

        public void GetPluginSpotReferenceChartData()
        {
            var projectBusiness = Business.BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewPluginDataService.ErrorOccured;
            _viewPluginDataService.ReferenceChartPlugin = projectBusiness.GetPluginSpotReferenceChartData(_viewPluginDataService.SpotProjectId,
                                                                                              _viewPluginDataService.ReferenceChartDisType);
        }

        public void GetMeasurementValidationData()
        {            
            var projectMeasurement = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            projectMeasurement.ErrorOccured += _viewPluginDataService.ErrorOccured;
            _viewPluginDataService.dtMeasurementValidation = projectMeasurement.GetMeasurementValidation(_viewPluginDataService.ProjectId, _viewPluginDataService.OptimizeRunId, _viewPluginDataService.ProjectType);
        }
        public void ValidateApplicationMeasurement()
        {
            var projectMeasurement = Business.BusinessFactory<IMeasurementBusiness>.CreateObject();
            projectMeasurement.ErrorOccured += _viewPluginDataService.ErrorOccured;
            _viewPluginDataService.dtMeasurementValidation = projectMeasurement.ValidateApplicationMeasurement(_viewPluginDataService.OptimizeRunId, _viewPluginDataService.ProjectId, _viewPluginDataService.MeaDeviceSerialNo);
        }

    }
}
