using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using System.Web.Security;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Header : System.Web.UI.UserControl
    {
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region "Control Events"
        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentUser != null)
            {
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