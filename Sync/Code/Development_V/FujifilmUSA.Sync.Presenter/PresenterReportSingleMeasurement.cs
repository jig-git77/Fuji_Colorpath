using System.Linq;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterReportSingleMeasurement
    {
        private IViewReportSingleMeasurement _viewReportSingleMeasurement;
        public PresenterReportSingleMeasurement(IViewReportSingleMeasurement viewReportSingleMeasurement)
        {
            _viewReportSingleMeasurement = viewReportSingleMeasurement;
        }

        public void GetReportDataOnMeasurement()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewReportSingleMeasurement.ErrorOccured;
            _viewReportSingleMeasurement.DSReport = measurementBusiness.GetReportDataOnMeasurement(_viewReportSingleMeasurement.MeasurementId, _viewReportSingleMeasurement.From_ΔE, _viewReportSingleMeasurement.IsVerification);
        }
    }

}
