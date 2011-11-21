using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Mock;
using FujifilmUSA.Sync.Business.Implementation;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterTaskeroUser
    {
        private readonly IViewTaskeroUser _ViewTaskeroUser;

        public PresenterTaskeroUser(IViewTaskeroUser ViewTaskeroUser)
        {
            this._ViewTaskeroUser = ViewTaskeroUser;
        }
        public void TaskeroUserLogin()
        {
            ITaskeroUserBusiness task = (ITaskeroUserBusiness)new BusinessFactory<ITaskeroUserBusiness>().CreateObject();
            _ViewTaskeroUser.GetTaskeroUser = task.UserLogin(_ViewTaskeroUser.UserName, _ViewTaskeroUser.Password,_ViewTaskeroUser.AllowedIP);
        }
    }
}
