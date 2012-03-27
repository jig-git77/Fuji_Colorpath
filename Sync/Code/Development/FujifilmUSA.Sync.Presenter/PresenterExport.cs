using System;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterExport
    {
        private readonly IViewExport _viewExport;
        public PresenterExport(IViewExport viewExport)
        {
            _viewExport = viewExport;
        }
        public void GetDeviceLinkProfile()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewExport.ErrorOccured;
            _viewExport.DeviceLinkProfile = optimizeRunBusiness.GetDeviceLinkProfile(_viewExport.OptimizeRunId);
        }
        public void GetChartPreview()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewExport.ErrorOccured;
            _viewExport.ChartPreview = projectBusiness.GetChartPreviewOnProjectId(_viewExport.ProjectId);
        }
        public void GetSourceProfile()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewExport.ErrorOccured;
            _viewExport.SourceProfile = projectBusiness.GetSourceProfile(_viewExport.ProjectId);
        }
        public void CreateOptimizeRun()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewExport.ErrorOccured;
            optimizeRunBusiness.CreateOptimizeRun(_viewExport.ProjectId, Convert.ToInt32(EOptimizeRunStatus.Open), Convert.ToInt32(EOptimizeRunStatus.Closed), DateTime.Now);
        }
        public void GetProjectDetail()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewExport.ErrorOccured;
            _viewExport.Project = projectBusiness.GetProjectDetail(_viewExport.ProjectId);
        }
    }
}
