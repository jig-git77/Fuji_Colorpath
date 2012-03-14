using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

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
    }
}
