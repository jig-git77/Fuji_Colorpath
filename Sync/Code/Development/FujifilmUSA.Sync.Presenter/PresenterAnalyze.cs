using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterAnalyze
    {
        private IViewAnalyze _viewAnalyze;
        public PresenterAnalyze(IViewAnalyze viewAnalyze)
        {
            _viewAnalyze = viewAnalyze;
        }

        public void CreateDestinationAndDlp()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.
                CreateInitialDestinationAndDeviceLinkProfiles(
                    _viewAnalyze.ProjectId,
                    _viewAnalyze.OptimizeRunId,
                    _viewAnalyze.MeasurementIds);

        }

        public void GetAnalyzeChartMeasurement()
        {
            var measurementbusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            _viewAnalyze.lstMeasurement = measurementbusiness.GetAnalyzeChartMeasurement(_viewAnalyze.OptimizeRunId);
        }
        public int UpdateMeasurementByAnalyzeChart()
        {
            var measurementbusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            return measurementbusiness.UpdateMeasurementByAnalyzeChart(_viewAnalyze.Measurement);
        }


    }
}
