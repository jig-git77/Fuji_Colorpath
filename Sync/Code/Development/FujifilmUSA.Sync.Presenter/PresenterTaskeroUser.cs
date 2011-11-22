using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterTaskeroUser
    {
        private readonly IViewTaskeroUser _viewTaskeroUser;

        public PresenterTaskeroUser(IViewTaskeroUser viewTaskeroUser)
        {
            this._viewTaskeroUser = viewTaskeroUser;
        }
        public void TaskeroUserLogin()
        {
            var task = (ITaskeroUserBusiness)new BusinessFactory<ITaskeroUserBusiness>().CreateObject();
            _viewTaskeroUser.GetTaskeroUser = task.UserLogin(_viewTaskeroUser.UserName, _viewTaskeroUser.Password,_viewTaskeroUser.AllowedIp);
        }
    }
}
