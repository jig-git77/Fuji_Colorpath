using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Entity;

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
            var task = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            _viewExport.DeviceLinkProfile = task.GetDeviceLinkProfile(_viewExport.OptimizeRunId);
        }
        public void GetChartPreview()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewExport.ChartPreview = task.GetChartPreviewOnProjectId(_viewExport.ProjectId);
        }
        public void GetSourceProfile()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewExport.SourceProfile = task.GetSourceProfile(_viewExport.ProjectId);
        }
        public void CreateOptimizeRun()
        {
            var task = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            task.CreateOptimizeRun(_viewExport.ProjectId, Convert.ToInt32(EOptimizeRunStatus.Open), Convert.ToInt32(EOptimizeRunStatus.Closed), DateTime.Now);
        }
        public void GetProjectDetail()
        {
            var projectTask = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewExport.Project = projectTask.GetProjectDetail(_viewExport.ProjectId);
        }
    }
}
