using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;

namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class ForgotPassword : BaseControl, IViewForgotPassword
    {
        #region Variable Declarations
        private PresenterForgotPassword _presenterForgotPassword;
        #endregion

        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterForgotPassword = new PresenterForgotPassword(this);
        }
        #endregion

        #region Control Events
        protected void lbtnRetrievePassword_Click(object sender, EventArgs e)
        {
            ResetPasswordInfo = new User() {LoginId = txtUserName.Text.Trim()};
            _presenterForgotPassword.UpdatePreResetPasswordInformation();
            if(string.IsNullOrEmpty(ResetPasswordInfo.Email))
            {
                fujiStatusbar.DisplayMessage("LoginId not exists", false);
            }
            else
            {
                fujiStatusbar.DisplayMessage("Email has been sent!", false);
            }
        }
        #endregion

        #region IViewForgotPassword Members
        public User ResetPasswordInfo { get; set; }
        #endregion
    }
}