using System;
using System.Web.Security;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using System.Linq;

namespace FujifilmUSA.Sync.Web
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            string script = string.Empty;
            if (ApplicationConfigurationManager.Instance().IsSingleUserLoginEnabled)
            {
                int timer = ApplicationConfigurationManager.Instance().KeepUserAliveInSeconds * 1000;                
                script = "function MakeUserAliveRequest() { setInterval(function() { KeepUserAlive('" + FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.LoginId + "') }, " + timer.ToString() + "); };";                
            }
            else
            {
                script = "function MakeUserAliveRequest() { //do nothing };";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "keepalivescript", script, true);
            
            List<User> users = FujifilmUSA.Sync.Common.CacheManager.Instance.ActiveUsers;
            User user = users.FirstOrDefault(c => c.LoginId == FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentUser.LoginId);
            if (user != null)
                user.LastPingTime = DateTime.Now;
            FujifilmUSA.Sync.Common.CacheManager.Instance.ActiveUsers = users;
        }


    }
}
