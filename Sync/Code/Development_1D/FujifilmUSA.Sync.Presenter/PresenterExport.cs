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
            _viewExport.ChartPreview = projectBusiness.GetChartPreviewOnReferenceChartTypeId(_viewExport.ReferenceChartTypeId, "Download");
        }
        public void GetDestinationProfile()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewExport.ErrorOccured;
            _viewExport.SourceProfile = projectBusiness.GetDestinationProfile(_viewExport.ProjectId);
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
            optimizeRunBusiness.CreateOptimizeRun(_viewExport.ProjectId, Convert.ToInt32(EOptimizeRunStatus.Open), DateTime.Now);
        }
        public void GetProjectDetail()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewExport.ErrorOccured;
            _viewExport.Project = projectBusiness.GetProjectDetail(_viewExport.ProjectId);
        }

        public void Create1DOptimizeRun()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewExport.ErrorOccured;
            optimizeRunBusiness.Create1DOptimizeRun(_viewExport.ProjectId, Convert.ToInt32(EOptimizeRunStatus.Open), DateTime.Now, _viewExport.CurveTypeId);
        }

        public void GetCurveData()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewExport.ErrorOccured;
            _viewExport.CurveData = optimizeRunBusiness.GetCurveData(_viewExport.OptimizeRunId, _viewExport.CurveTypeId);
        }

        public void Get1DDeviceLinkProfile()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewExport.ErrorOccured;
            _viewExport.DeviceLinkProfile = optimizeRunBusiness.Get1DDeviceLinkProfile(_viewExport.OptimizeRunId);
        }

        public void GetNamedColorProfile()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewExport.ErrorOccured;
            _viewExport.SourceProfile = projectBusiness.GetNamedColorProfile(_viewExport.OptimizeRunId);
        }

        public void CreateOptimizeRun_SpotProject()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewExport.ErrorOccured;
            optimizeRunBusiness.CreateSpotProjectOptimizeRun(_viewExport.SpotProjectId, Convert.ToInt32(EOptimizeRunStatus.Open), DateTime.Now);
        }

        public void GetProjectDetail_SpotProject()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewExport.ErrorOccured;
            _viewExport.Project = projectBusiness.GetProjectDetail_SpotProject(_viewExport.SpotProjectId);
        }

        public void GetSpotPatchDetailsForChart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewExport.ErrorOccured;
            _viewExport.dtPatchDetails = measurementBusiness.GetSpotPatchDetailsForChart(_viewExport.OptimizeRunId);
        }

        public void GetSpotDestinationProfile()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewExport.ErrorOccured;
            _viewExport.SourceProfile = projectBusiness.GetSpotDestinationProfile(_viewExport.ProjectId);
        }
        public void GetSpotPatchDetailsForCMYKExport()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewExport.ErrorOccured;
            _viewExport.dtPatchDetails = measurementBusiness.GetSpotPatchDetailsForCMYKExport(_viewExport.OptimizeRunId);
        }

    }
}
