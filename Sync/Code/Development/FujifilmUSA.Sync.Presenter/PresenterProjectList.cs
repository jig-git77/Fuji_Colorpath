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

        public void GetProjectListBySyncDeviceId()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            _viewProjectList.GetProjectListBySyncDeviceId = task.GetProjectListBySyncDeviceId(_viewProjectList.SyncDeviceId);
        }

        public void DeleteProject()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            _viewProjectList.DeleteProject = task.DeleteProject(_viewProjectList.ProjectId);
        }

        public void GetProjectByProjectId()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            _viewProjectList.GetProjectByProjectId = task.GetProjectByProjectId(_viewProjectList.ProjectId);

        }
        public void GetMeasurementSettingByProjectID()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            _viewProjectList.GetMeasurementSettingByProjectID = task.GetMeasurementSettingByProjectID(_viewProjectList.ProjectId);
        }

    }
}
