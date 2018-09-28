#region System
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Fuji.Business;
using Fuji.Common;
#endregion

#region Others
#endregion

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