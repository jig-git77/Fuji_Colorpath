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
            var task = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            task.OptimizeDeviceLinkProfile(_viewConfirmSave.ProjectId, _viewConfirmSave.OptimizeRunId, _viewConfirmSave.MeasurementIds);
        }

        public void GetMeasurementsOnRunId()
        {
            var task = BusinessFactory<IMeasurementBusiness>.CreateObject();
            _viewConfirmSave.MeasurementIds = task.GetMeasurementsOnRunId(_viewConfirmSave.OptimizeRunId).Select(x => x.MeasurementId).ToList();
        }

        public void GetProjectDetail()
        {
            var projectTask = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewConfirmSave.Project = projectTask.GetProjectDetail(_viewConfirmSave.ProjectId);
        }

        public void CreateOptimizeRun()
        {
            var task = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            task.CreateOptimizeRun(_viewConfirmSave.ProjectId, Convert.ToInt32(EOptimizeRunStatus.Open), Convert.ToInt32(EOptimizeRunStatus.Closed), DateTime.Now);
        }

        public void CloseOptimizeRun()
        {
            var task = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            task.CloseOptimizeRun(_viewConfirmSave.OptimizeRunId, Convert.ToInt32(EOptimizeRunStatus.Closed), DateTime.Now);
        }
    }
}