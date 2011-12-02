using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentUser != null)
            {
                #region Clear Session Information
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                #endregion
                Response.Redirect("Login.aspx");
            }
        }
    }
}