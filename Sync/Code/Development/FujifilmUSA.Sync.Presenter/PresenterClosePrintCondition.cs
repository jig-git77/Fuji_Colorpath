using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Entity;
using System;

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
                Convert.ToInt32(EOptimizeRunStatus.Closed),
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
    }
}
