#region Using System
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
using Telerik.WebControls;
#endregion

public partial class Logout : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                string RedirectPage = "Default";

                //========== Send to Default Page ==========//
                if (Request.Cookies["LoginPage"] != null)
                { RedirectPage = Request.Cookies["LoginPage"].Value; }                

                if (Fuji.Common.Sessions.Username != null && Fuji.Common.Sessions.Username.Length != 0)
                {
                    #region Update User Logout
                    //========== Declare object of the UserLog ===========//
                    UserLogData ObjUserLog = new UserLogData();

                    //========== Assign the user log information to object of the user log ===========//
                    ObjUserLog.TaskeroUserID = Fuji.Common.Sessions.UserID;
                    ObjUserLog.IPAddress = Request.UserHostAddress;
                    ObjUserLog.uspUpdateUserLogOut();
                    #endregion

                    #region Clear Session Information
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    #endregion
                }

                Fuji.Common.CommonFunctions.PageRedirect(RedirectPage, "");
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
}
