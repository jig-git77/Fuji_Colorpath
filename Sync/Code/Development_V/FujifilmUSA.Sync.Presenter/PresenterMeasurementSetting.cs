using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Entity;
using System;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterMeasurementSetting
    {
        private readonly IViewMeasurementSetting _viewMeasurementSetting;

        public PresenterMeasurementSetting(IViewMeasurementSetting viewMeasurementSetting)
        {
            _viewMeasurementSetting = viewMeasurementSetting;
        }

        public void GetObserverType()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            _viewMeasurementSetting.Observer = projectBusiness.GetObserverTypeList();
        }

        public void GetIlluminant()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            _viewMeasurementSetting.Illumin = projectBusiness.GetIlluminantList();
        }

        public void GetMeasurementDevice()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            _viewMeasurementSetting.MeasurementDevices = projectBusiness.GetMeasurementDeviceList(_viewMeasurementSetting.DeviceId);
        }

        public void GetReferenceChartType()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            _viewMeasurementSetting.ReferenceChartType = projectBusiness.GetReferenceChartTypeList(_viewMeasurementSetting.MeasurementDeviceId, _viewMeasurementSetting.ChartDisplayType);
        }

        public void GetReferenceChartTypeImage()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            _viewMeasurementSetting.ReferenceChartTypeImage = projectBusiness.GetChartPreviewOnReferenceChartTypeId(_viewMeasurementSetting.ReferenceChartTypeId, "Preview");
        }

        public void GetProjectDetail()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            _viewMeasurementSetting.Project = projectBusiness.GetProjectDetail(_viewMeasurementSetting.ProjectId);
        }

        public void GetProjectDetailWithVerification()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            _viewMeasurementSetting.Project = projectBusiness.GetProjectDetail(_viewMeasurementSetting.ProjectId, true);
        }

        public void CreateOptimizeRun()
        {
            var optimizeRun = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRun.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            optimizeRun.CreateOptimizeRun(
                _viewMeasurementSetting.ProjectId,
                Convert.ToInt32(EOptimizeRunStatus.Open),
                DateTime.Now);
        }

        public void SaveProject()
        {
            #region code for source profile and referencechart relation in referenceLab

            var referenceChartBusiness = BusinessFactory<IReferenceChartBusiness>.CreateObject();
            referenceChartBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            var referenceChartData = referenceChartBusiness.GetReferenceChartForLabCalculation(_viewMeasurementSetting.Project.SourceProfile.SourceProfileId,
                                                                                               _viewMeasurementSetting.Project.MeasurementSetting.ReferenceChartType.Key);
            if (referenceChartData.Count > 0)
            {
                var sourceProfileBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
                sourceProfileBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
                var profile = sourceProfileBusiness.GetIccProfileBySourceProfile(_viewMeasurementSetting.Project.SourceProfile.SourceProfileId);

                referenceChartBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
                referenceChartBusiness.InsertReferenceLabData(referenceChartData, profile, _viewMeasurementSetting.Project.SourceProfile.SourceProfileId);
            }

            #endregion

            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewMeasurementSetting.ErrorOccured;
            _viewMeasurementSetting.ProjectId = projectBusiness.SaveProject(_viewMeasurementSetting.Project);
        }
    }
}
