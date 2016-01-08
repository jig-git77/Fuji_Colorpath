using System.Linq;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterReportCMYKDensity
    {
        private IViewReportCMYKDensity _viewReportCMYKDensity;
        public PresenterReportCMYKDensity(IViewReportCMYKDensity viewReportCMYKDensity)
        {
            _viewReportCMYKDensity = viewReportCMYKDensity;
        }

        public void GetDensityData_C()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewReportCMYKDensity.ErrorOccured;
            _viewReportCMYKDensity.DSDensity_C = measurementBusiness.GetDensityData_C(_viewReportCMYKDensity.OptimizeRunId).Tables[0];
            _viewReportCMYKDensity.DSDensity_M = measurementBusiness.GetDensityData_C(_viewReportCMYKDensity.OptimizeRunId).Tables[1];
            _viewReportCMYKDensity.DSDensity_Y = measurementBusiness.GetDensityData_C(_viewReportCMYKDensity.OptimizeRunId).Tables[2];
            _viewReportCMYKDensity.DSDensity_K = measurementBusiness.GetDensityData_C(_viewReportCMYKDensity.OptimizeRunId).Tables[3];
        }

    }
}
