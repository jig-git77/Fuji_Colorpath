using OrisWeb.Models;
using System;
using System.Web;

namespace OrisWeb
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