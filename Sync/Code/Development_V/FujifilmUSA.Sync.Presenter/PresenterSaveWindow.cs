using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterSaveWindow
    {
        private readonly IViewSaveWindow _viewSaveWindow;

        public PresenterSaveWindow(IViewSaveWindow viewSaveWindow)
        {
            _viewSaveWindow = viewSaveWindow;
        }

        public void SaveProject()
        {
            #region code for source profile and referencechart relation in referenceLab

            //check for profile chart
            if (_viewSaveWindow.Project.MeasurementSetting.ReferenceChartType.Key > 0)
            {
                CreateReferenceLabData(_viewSaveWindow.Project.SourceProfile.SourceProfileId,
                                       _viewSaveWindow.Project.MeasurementSetting.ReferenceChartType.Key);
            }

            //check for optimization chart
            if (_viewSaveWindow.Project.MeasurementSetting.OptReferenceChartType.Key > 0)
            {
                CreateReferenceLabData(_viewSaveWindow.Project.SourceProfile.SourceProfileId,
                                       _viewSaveWindow.Project.MeasurementSetting.OptReferenceChartType.Key);
            }

            //check for verification chart
            if (_viewSaveWindow.Project.MeasurementSetting.VReferenceChartType.Key > 0)
            {
                CreateReferenceLabData(_viewSaveWindow.Project.SourceProfile.SourceProfileId, 
                    _viewSaveWindow.Project.MeasurementSetting.VReferenceChartType.Key);
            }

            #endregion

            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewSaveWindow.ErrorOccured;
            _viewSaveWindow.ProjectId = projectBusiness.SaveProject(_viewSaveWindow.Project);
        }

        public void GetProjectDetail()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewSaveWindow.ErrorOccured;
            _viewSaveWindow.Project = projectBusiness.GetProjectDetail(_viewSaveWindow.ProjectId);
        }

        public void GetProjectDetailWithVerification()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewSaveWindow.ErrorOccured;
            _viewSaveWindow.Project = projectBusiness.GetProjectDetail(_viewSaveWindow.ProjectId, true);
        }

        public void GetChartPreview()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewSaveWindow.ErrorOccured;
            _viewSaveWindow.ChartPreview = projectBusiness.GetChartPreviewOnReferenceChartTypeId(_viewSaveWindow.ReferenceChartTypeId, "Download");
        }

        private void CreateReferenceLabData(short sourceProfileId, short referenceChartTypeId)
        {
            var referenceChartBusiness = BusinessFactory<IReferenceChartBusiness>.CreateObject();
            referenceChartBusiness.ErrorOccured += _viewSaveWindow.ErrorOccured;
            var referenceChartData = referenceChartBusiness.GetReferenceChartForLabCalculation(sourceProfileId,
                                                                                               referenceChartTypeId);
            if (referenceChartData.Count > 0)
            {
                var sourceProfileBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
                sourceProfileBusiness.ErrorOccured += _viewSaveWindow.ErrorOccured;
                var profile = sourceProfileBusiness.GetIccProfileBySourceProfile(sourceProfileId);

                referenceChartBusiness.ErrorOccured += _viewSaveWindow.ErrorOccured;
                referenceChartBusiness.InsertReferenceLabData(referenceChartData, profile, sourceProfileId);
            }
        }
    }
}
