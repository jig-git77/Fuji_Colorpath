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
            _viewReportSingleMeasurement.DSReport = measurementBusiness.GetReportDataOnMeasurement(_viewReportSingleMeasurement.MeasurementId, _viewReportSingleMeasurement.From_ΔE, _viewReportSingleMeasurement.IsVerification, _viewReportSingleMeasurement.ValidationType, _viewReportSingleMeasurement.IsSync1DType, _viewReportSingleMeasurement.IsAverageOfMeasurements, _viewReportSingleMeasurement.LicenseTypeId);
        }

        public void GetReportDataOnMeasurement_G7_Vali()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewReportSingleMeasurement.ErrorOccured;
            _viewReportSingleMeasurement.DSReport_G7_Vali = measurementBusiness.GetReportDataOnMeasurement_G7_Vali(_viewReportSingleMeasurement.MeasurementId, _viewReportSingleMeasurement.From_ΔE, _viewReportSingleMeasurement.IsVerification, _viewReportSingleMeasurement.ValidationType, _viewReportSingleMeasurement.IsSync1DType, _viewReportSingleMeasurement.IsAverageOfMeasurements, _viewReportSingleMeasurement.LicenseTypeId);
        }

        public void GetReportDataOnSpotProject()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewReportSingleMeasurement.ErrorOccured;
            _viewReportSingleMeasurement.DSReport = measurementBusiness.GetReportDataOnSpotProject(_viewReportSingleMeasurement.OptimizeRunId);
        }
    }

}
