using System;
using System.Web.Security;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Header : BaseControl
    {
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.Instance.CurrentUser != null)
                    lbtnSettings.Visible = SessionManager.Instance.CurrentUser.HasAdminPermission();
            }
        }
        #endregion

        #region "Control Events"
        protected void imgbtnLogo_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentOptimizeRun = null;
            SessionManager.Instance.CurrentProject = null;
            SessionManager.Instance.CurrentDevice = null;
            SessionManager.Instance.CurrentOperation = ELicenseType.None;
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
            Response.RedirectPermanent("Index.aspx");
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentUser != null)
            {
                WebLogActivityManager.LogActivity(LogActivityType.LoggedOut, null, Request.UserHostAddress);

                #region Clear Session Information
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                FormsAuthentication.SignOut();
                #endregion
                Response.Redirect("Login.aspx");
            }
        }
        #endregion

    }
}