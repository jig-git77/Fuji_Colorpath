using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterLogin
    {
        private readonly IViewLogin _view;
        public PresenterLogin(IViewLogin view)
        {
            _view = view;
        }

        public void UserLogin()
        {
            var task = BusinessFactory<IUserBusiness>.CreateObject();
            //task.ErrorOccured += _viewTaskeroUser.ErrorOccured;
            _view.CurrentUser = task.UserLogin(_view.UserName, _view.Password);

        }



    }
}
