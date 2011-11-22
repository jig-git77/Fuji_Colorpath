using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterProject
    {
        private readonly IViewProjectList _viewProjectList;

        public PresenterProject(IViewProjectList viewProjectList)
        {
            _viewProjectList = viewProjectList;
        }

        public void GetProjectListBySyncDeviceId()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            _viewProjectList.GetProjectListBySyncDeviceId = task.GetProjectListBySyncDeviceId(_viewProjectList.SyncDeviceId);
        }
    }
}
