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
        private const string _recloginIdNotExistsText = "loginIdNotExistsText";
        private const string _recemailSentText = "emailSentText";
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterForgotPassword = new PresenterForgotPassword(this);
        }
        #endregion

        #region Control Events
        protected void lbtnRetrievePassword_Click(object sender, EventArgs e)
        {
            ResetPasswordInfo = new User() { LoginId = txtUserName.Text.Trim() };
            _presenterForgotPassword.UpdatePreResetPasswordInformation();
            if (string.IsNullOrEmpty(ResetPasswordInfo.Email))
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recloginIdNotExistsText), false);
            }
            else
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recemailSentText), false);
            }
        }
        #endregion

        #region IViewForgotPassword Members
        public User ResetPasswordInfo { get; set; }
        #endregion
    }
}