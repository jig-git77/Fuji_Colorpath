using System.Linq;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterReportGrading
    {
        private IViewReportGrading _viewReportGrading;
        public PresenterReportGrading(IViewReportGrading viewReportGrading)
        {
            _viewReportGrading = viewReportGrading;
        }

        public void GetMeasurementDataForScoring()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewReportGrading.ErrorOccured;
            _viewReportGrading.DSReport = measurementBusiness.GetMeasurementDataForScoring(_viewReportGrading.MeasurementId, _viewReportGrading.From_ΔE, _viewReportGrading.ProjectId, _viewReportGrading.IsAverageOfMeasurements);
        }

    }
}
