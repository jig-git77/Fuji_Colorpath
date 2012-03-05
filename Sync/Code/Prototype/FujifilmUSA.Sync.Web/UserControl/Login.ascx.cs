using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Web.Security;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Login : System.Web.UI.UserControl, IViewTaskeroUser
    {
        #region "Declaration"
        private PresenterTaskeroUser _presentertaskerouser;
        public string btnLoginId
        {
            get { return bntLogin.UniqueID; }
        }
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presentertaskerouser = new PresenterTaskeroUser(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentUser != null)
                Response.Redirect("Index.aspx");

            if (!IsPostBack)
            {
                if ((Request.Cookies["SyncFujiLogin"] == null || Request.Cookies["SyncFujiLogin"].Value == "") &&
                    (Request.Cookies["SyncFujiPass"] == null || Request.Cookies["SyncFujiPass"].Value == ""))
                {
                    chkRememberMe.Checked = false;
                }
                else
                {
                    chkRememberMe.Checked = true;
                    txtUserName.Text = Request.Cookies["SyncFujiLogin"].Value;
                    txtPassword.Attributes.Add("value", Request.Cookies["SyncFujiPass"].Value);

                }
            }

        }
        protected void bntLogin_Click(object sender, EventArgs e)
        {
                UserName = txtUserName.Text.Trim();
                Password = txtPassword.Text.Trim();
                _presentertaskerouser.TaskeroUserLogin();
                if (GetTaskeroUser != null && GetTaskeroUser.LoginId != null)
                {
                    SessionManager.Instance.CurrentUser = GetTaskeroUser;
                    if (chkRememberMe.Checked == true)
                    {
                        var objCookie = new HttpCookie("SyncFujiLogin") { Value = txtUserName.Text, Expires = System.DateTime.Now.AddDays(20) };
                        Response.SetCookie(objCookie);

                        objCookie = new HttpCookie("SyncFujiPass") { Value = Password, Expires = System.DateTime.Now.AddDays(20) };
                        Response.SetCookie(objCookie);
                    }
                    else
                    {
                        Response.Cookies["SyncFujiLogin"].Value = "";
                        Response.Cookies["SyncFujiPass"].Value = "";
                    }

                    FormsAuthentication.RedirectFromLoginPage(UserName, false);
                    //FormsAuthentication.RedirectToLoginPage();
                    Response.Redirect("Index.aspx");
                }
                else
                {
                    ucStatusbar1.DisplayMessage(ResourceManager.GetResourceMessage("Login_InvalidLogin"), false);
                }
        }
        #endregion

        #region Error Handling Method
        public void ErrorOccured(object sender, Business.Interface.ErrorArgs e)
        {
            ucStatusbar1.DisplayMessage(e.ErrorMessage, false);
        }
        #endregion

        //*******************************************************************//

        #region IViewTaskeroUser Members
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AllowedIp { get; set; }
        public TaskeroUser GetTaskeroUser { get; set; }
        #endregion
    }
}