using System.Linq;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using FujifilmUSA.Sync.Entity;
using System.Data;

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
            DataSet dsResult = measurementBusiness.GetDensityData_C(_viewReportCMYKDensity.OptimizeRunId);
            if (dsResult != null)
            {
                _viewReportCMYKDensity.DSDensity_C = dsResult.Tables[0];
                _viewReportCMYKDensity.DSDensity_M = dsResult.Tables[1];
                _viewReportCMYKDensity.DSDensity_Y = dsResult.Tables[2];
                _viewReportCMYKDensity.DSDensity_K = dsResult.Tables[3];
            }
        }

    }
}
