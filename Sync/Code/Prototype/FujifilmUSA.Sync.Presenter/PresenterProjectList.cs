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

        public void GetProjectListBySyncDeviceId()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            task.ErrorOccured += _viewProjectList.ErrorOccured; 
            _viewProjectList.Projects = task.GetProjectsBySyncDeviceId(_viewProjectList.Project.SyncDeviceId);
        }

        public void DeleteProject()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            task.ErrorOccured += _viewProjectList.ErrorOccured; 
            task.DeleteProject(_viewProjectList.Project.ProjectId);
        }

        public void GetProject()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            task.ErrorOccured += _viewProjectList.ErrorOccured;  
            _viewProjectList.Project = task.GetProject(_viewProjectList.Project.ProjectId);
        }
    }
}
