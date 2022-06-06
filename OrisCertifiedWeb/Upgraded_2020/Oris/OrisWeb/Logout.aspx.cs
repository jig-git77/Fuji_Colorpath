using Oris.Business;
using Oris.Common;
using OrisWeb.Models;
using System;


namespace OrisWeb
{
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

                    if (Sessions.Username != null && Sessions.Username.Length != 0)
                    {
                        #region Update User Logout
                        //========== Declare object of the UserLog ===========//
                        UserLogData ObjUserLog = new UserLogData();

                        //========== Assign the user log information to object of the user log ===========//
                        ObjUserLog.TaskeroUserID = Sessions.UserID;
                        ObjUserLog.IPAddress = Request.UserHostAddress;
                        ObjUserLog.uspUpdateUserLogOut();
                        #endregion

                        #region Clear Session Information
                        Session.Clear();
                        Session.RemoveAll();
                        Session.Abandon();
                        #endregion
                    }

                    CommonFunctions.PageRedirect(RedirectPage, "");
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
    }
}