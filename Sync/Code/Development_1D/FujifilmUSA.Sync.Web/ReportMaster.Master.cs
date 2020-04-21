using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web
{
    public partial class ReportMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            string script = string.Empty;
            if (ApplicationConfigurationManager.Instance().IsSingleUserLoginEnabled && FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser != null)
            {
                int timer = ApplicationConfigurationManager.Instance().KeepUserAliveInSeconds * 1000;
                script = "function MakeUserAliveRequest() { KeepUserAlive('" + FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.LoginId + "'); setInterval(function() { KeepUserAlive('" + FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.LoginId + "') }, " + timer.ToString() + "); };";
            }
            else
            {
                script = "function MakeUserAliveRequest() { };";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "keepalivescript", script, true);

            ImageButton logoImage = (ImageButton)ucHeader.FindControl("imgbtnLogo");
            if (logoImage != null)
                logoImage.Enabled = false;

            //List<User> users = FujifilmUSA.Sync.Common.CacheManager.Instance.ActiveUsers;
            //User user = users.FirstOrDefault(c => c.LoginId == FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.LoginId);
            //if (user != null)
            //    user.LastPingTime = DateTime.Now;
            //FujifilmUSA.Sync.Common.CacheManager.Instance.ActiveUsers = users; 
        }
    }
}