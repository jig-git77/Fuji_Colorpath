#region System
using System;
using System.Web;
using TaskeroWeb.Models;
#endregion

#region Others
#endregion

namespace TaskeroWeb
{
    public partial class Default : PageBase
    {

        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            //========== Set the Cookies ==========//
            HttpCookie objCookie = new HttpCookie("LoginPage");
            objCookie.Value = "Default";
            objCookie.Expires = System.DateTime.Now.AddDays(100);
            Response.SetCookie(objCookie);
        }
        #endregion


    }
}