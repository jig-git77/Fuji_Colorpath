using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if(this.GetType() == typeof(Index))
            {
                //this is test condition
                if(SessionManager.Instance.CurrentUser.Role == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
            base.OnLoad(e);
        }
    }
}