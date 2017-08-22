using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterProjectList
    {
        private readonly IViewProjectList _viewProjectList;

        public PresenterProjectList(IViewProjectList viewProjectList)
        {
            _viewProjectList = viewProjectList;
        }

        public void GetSourceProfileByCompany()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewProjectList.ErrorOccured;
            _viewProjectList.SourceProfile = miscDataBusiness.GetSourceProfileByCompany(_viewProjectList.CompanyId);
        }

        public void GetProjectListByDeviceId()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewProjectList.ErrorOccured;
            _viewProjectList.Projects = projectBusiness.GetProjectsByDeviceId(_viewProjectList.DeviceId,_viewProjectList.LicenseTypeID);
        }

        public void DeleteProject()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewProjectList.ErrorOccured;
            projectBusiness.DeleteProject(_viewProjectList.ProjectId);
        }

        public void GetProjectDetailWithVarification()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewProjectList.ErrorOccured;
            _viewProjectList.Project = projectBusiness.GetProjectDetail(_viewProjectList.ProjectId, true);
        }

        public void GetSpotLibraryListByProjectId()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewProjectList.ErrorOccured;
            _viewProjectList.SpotProjectLibraries = projectBusiness.GetSpotLibraryListByProjectId(_viewProjectList.ProjectId);
        }

        public void DeleteSpotProject()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewProjectList.ErrorOccured;
            projectBusiness.DeleteSpotProject(_viewProjectList.SpotProjectId);
        }
    }
}
