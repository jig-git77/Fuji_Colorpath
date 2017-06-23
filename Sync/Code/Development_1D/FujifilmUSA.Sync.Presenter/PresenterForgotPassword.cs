using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterForgotPassword
    {
        private readonly IViewForgotPassword _viewForgotPassword;

        public PresenterForgotPassword(IViewForgotPassword viewForgotPassword)
        {
            _viewForgotPassword = viewForgotPassword;
        }

        public void UpdatePreResetPasswordInformation()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewForgotPassword.ErrorOccured;
            _viewForgotPassword.ResetPasswordInfo = userBusiness.UpdatePreResetPasswordInformation(_viewForgotPassword.ResetPasswordInfo.LoginId);
        }
    }
}
