using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.Home;
            if (!Page.IsPostBack)
            {
                SessionManager.Instance.CurrentSyncDevice = null;
                SessionManager.Instance.CurrentProject = null;
                SessionManager.Instance.CurrentSyncRun = null;
            }

        }
    }
}