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
            this._viewProjectList = viewProjectList;
        }

        public void GetSourceProfileByCompany()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewProjectList.SourceProfile = task.SourceProfileByCompany(_viewProjectList.CompanyId);
        }

        public void GetProjectListByDeviceId()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewProjectList.Projects = task.GetProjectsByDeviceId(_viewProjectList.Project.DeviceId);
        }

        public void DeleteProject()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            task.DeleteProject(_viewProjectList.Project.ProjectId);
        }

        

        public void GetDeviceFamilyProfilerOptionPreset()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewProjectList.SourceProfile = task.GetDeviceFamilyProfilerOptionPreset();
        }
    }
}
