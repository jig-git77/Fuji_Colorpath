using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Entity;
using System;
using System.Linq;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterClosePrintCondition
    {
        private readonly IViewClosePrintCondition _viewClosePrintCondition;

        public PresenterClosePrintCondition(IViewClosePrintCondition viewClosePrintCondition)
        {
            _viewClosePrintCondition = viewClosePrintCondition;
        }

        public void CreateOptimizeRun()
        {
            var optimizeRun = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRun.ErrorOccured += _viewClosePrintCondition.ErrorOccured;
            optimizeRun.CreateOptimizeRun(
                _viewClosePrintCondition.ProjectId,
                Convert.ToInt32(EOptimizeRunStatus.Open),              
                DateTime.Now);
        }

        public void GetProjectDetail()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewClosePrintCondition.ErrorOccured;
            _viewClosePrintCondition.Project = projectBusiness.GetProjectDetail(_viewClosePrintCondition.ProjectId);
        }

        public void GetCurrentDeviceLinkProfile()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewClosePrintCondition.ErrorOccured;
            _viewClosePrintCondition.CurrentDlp = projectBusiness.GetLastOptimizedDlp(_viewClosePrintCondition.ProjectId);
        }

        public void ReOptimizeDeviceLinkProfile()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewClosePrintCondition.ErrorOccured;
            var lastOptimizeRunId = optimizeRunBusiness.GetLastOptimizeRunId(_viewClosePrintCondition.ProjectId);
            optimizeRunBusiness.ErrorOccured -= _viewClosePrintCondition.ErrorOccured;

            if (lastOptimizeRunId > 0)
            {
                var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
                measurementBusiness.ErrorOccured += _viewClosePrintCondition.ErrorOccured;
                var measurementIds = measurementBusiness.GetMeasurementsOnRunId(lastOptimizeRunId).Where(x => x.IsUsed).Select(x => x.MeasurementId).ToList();
                measurementBusiness.ErrorOccured -= _viewClosePrintCondition.ErrorOccured;

                if (measurementIds.Count > 0)
                {
                    //To get current dlp name to pass as description...
                    var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
                    projectBusiness.ErrorOccured += _viewClosePrintCondition.ErrorOccured;
                    var project = projectBusiness.GetProjectDetail(_viewClosePrintCondition.ProjectId);
                    projectBusiness.ErrorOccured -= _viewClosePrintCondition.ErrorOccured;

                    optimizeRunBusiness.ErrorOccured += _viewClosePrintCondition.ErrorOccured;
                    optimizeRunBusiness.
                        OptimizeDeviceLinkProfile(_viewClosePrintCondition.ProjectId, lastOptimizeRunId, measurementIds,
                                                  _viewClosePrintCondition.DeviceName + " " + project.ProposedDlpName, Convert.ToInt32(EOptimizeRunStatus.Closed), DateTime.Now);
                }
            }
        }

        public void Create1DOptimizeRun()
        {
            var optimizeRun = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRun.ErrorOccured += _viewClosePrintCondition.ErrorOccured;
            optimizeRun.Create1DOptimizeRun(
                _viewClosePrintCondition.ProjectId,
                Convert.ToInt32(EOptimizeRunStatus.Open),
                DateTime.Now);
        }

        public void Get1DCurrentDeviceLinkProfile()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewClosePrintCondition.ErrorOccured;
            _viewClosePrintCondition.CurrentDlp = projectBusiness.Get1DLastOptimizedDlp(_viewClosePrintCondition.ProjectId);
        }
    }
}
