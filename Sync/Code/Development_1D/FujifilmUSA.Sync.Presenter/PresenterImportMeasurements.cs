using System.Linq;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterImportMeasurements
    {
        private readonly IViewImportMeasurements _viewImportMeasurements;

        public PresenterImportMeasurements(IViewImportMeasurements viewImportMeasurements)
        {
            _viewImportMeasurements = viewImportMeasurements;
        }
        public void GetProjectList()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewImportMeasurements.ErrorOccured;
            _viewImportMeasurements.Projects = projectBusiness.GetProjectsForImportMeasurement(_viewImportMeasurements.DeviceId, _viewImportMeasurements.LicenseTypeID, _viewImportMeasurements.ProjectId);
        }

        public void GetMeasurementIDsbyProjectID()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewImportMeasurements.ErrorOccured;
            _viewImportMeasurements.Measurements = projectBusiness.GetMeasurementIDsbyProjectID(_viewImportMeasurements.DeviceId, _viewImportMeasurements.LicenseTypeID, _viewImportMeasurements.ProjectId);
        }
        public void ImportMeasurements()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewImportMeasurements.ErrorOccured;
            projectBusiness.ImportMeasurements(_viewImportMeasurements.MeasurementID, _viewImportMeasurements.OptimizeRunID);
        }

        public int ImportDummyMeasurement()
        {
            int OptimizationRunID = 0;
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewImportMeasurements.ErrorOccured;
            OptimizationRunID = projectBusiness.AddDummyData(_viewImportMeasurements.ProjectId, _viewImportMeasurements.OptimizeRunID);
            return OptimizationRunID;
        }

        public void CreateSpotProjectInitialOptimization()
        {            
            var projectOptimization = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            projectOptimization.ErrorOccured += _viewImportMeasurements.ErrorOccured;
            projectOptimization.CreateSpotNamedColorProfile(_viewImportMeasurements.ProjectId, _viewImportMeasurements.OptimizeRunID);            
        }
    }
}
