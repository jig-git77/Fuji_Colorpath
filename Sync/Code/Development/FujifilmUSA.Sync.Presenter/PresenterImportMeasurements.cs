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

    }
}
