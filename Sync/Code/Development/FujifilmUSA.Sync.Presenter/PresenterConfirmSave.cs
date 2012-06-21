using System.Linq;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterConfirmSave
    {
        private readonly IViewConfirmSave _viewConfirmSave;
        public PresenterConfirmSave(IViewConfirmSave viewConfirmSave)
        {
            _viewConfirmSave = viewConfirmSave;
        }
        public void OptimizeDlp()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            optimizeRunBusiness.OptimizeDeviceLinkProfile(_viewConfirmSave.ProjectId, _viewConfirmSave.OptimizeRunId, _viewConfirmSave.MeasurementIds,
                                                          _viewConfirmSave.ProfileDescription, Convert.ToInt32(ERefChartDisplayType.Closed), DateTime.Now);
        }

        public void GetMeasurementsOnRunId()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            _viewConfirmSave.MeasurementIds = measurementBusiness.GetMeasurementsOnRunId(_viewConfirmSave.OptimizeRunId).Where(x => x.IsUsed).Select(x => x.MeasurementId).ToList();
        }

        public void GetProjectDetail()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            _viewConfirmSave.Project = projectBusiness.GetProjectDetail(_viewConfirmSave.ProjectId);
        }

        public void CreateOptimizeRun()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            optimizeRunBusiness.CreateOptimizeRun(_viewConfirmSave.ProjectId, Convert.ToInt32(ERefChartDisplayType.Open), DateTime.Now);
        }

        public void CloseOptimizeRun()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            optimizeRunBusiness.CloseOptimizeRun(_viewConfirmSave.OptimizeRunId, _viewConfirmSave.ProjectId, _viewConfirmSave.MasterRunId, Convert.ToInt32(ERefChartDisplayType.Closed), DateTime.Now);
        }

        public void GetLastRunStatus()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            _viewConfirmSave.LastRunStatus = optimizeRunBusiness.GetLastRunStatus(_viewConfirmSave.OptimizeRunId);
        }
    }
}