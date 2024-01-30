using OrisWeb.Models;
using System;
using System.Web;


#region Others
#endregion

namespace OrisWeb
{

    public partial class UserLogin : PageBase
    {

        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Set the Cookies ==========//
                HttpCookie objCookie = new HttpCookie("LoginPage");
                objCookie.Value = "UserLogin";
                objCookie.Expires = System.DateTime.Now.AddDays(100);
                Response.SetCookie(objCookie);
            }
            catch (Exception Ex)
            {
                HandleException(Ex);
            }
        }
        #endregion


    }
}