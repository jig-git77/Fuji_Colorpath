using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterAddProject
    {
        private readonly IViewAddProject _viewAddProject;
        public PresenterAddProject(IViewAddProject viewAddProject)
        {
            this._viewAddProject = viewAddProject;
        }

        public void GetDeviceLinkProfileList()
        {
            var task = (IDeviceLinkProfileBusiness)new BusinessFactory<IDeviceLinkProfileBusiness>().CreateObject();
            _viewAddProject.DeviceLinkProfiles = task.GetDeviceLinkProfiles(_viewAddProject.SourceProfileId, _viewAddProject.DestinationProfileId);

        }

        public void InsertProjectAndMeasurementSetting()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            _viewAddProject.Project = task.InsertProjectAndMeasurementSetting(_viewAddProject.Project, _viewAddProject.MeasurementSetting);
        }

        public void GetDeviceLinkProfile()
        {
            var task = (IDeviceLinkProfileBusiness)new BusinessFactory<IDeviceLinkProfileBusiness>().CreateObject();
            var deviceLinkProfile = task.GetDeviceLinkProfile(_viewAddProject.Project.DeviceLinkProfileId);
            _viewAddProject.SourceProfileId = deviceLinkProfile.SrcProfile.ProfileId;
            _viewAddProject.DestinationProfileId = deviceLinkProfile.DestProfile.ProfileId;
        }

        public void GetMeasurementSetting()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            _viewAddProject.MeasurementSetting = task.GetMeasurementSettingByProjectId(_viewAddProject.Project.ProjectId);
        }
    }
}
