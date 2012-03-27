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
            //IsLogin();
            //IsRoll();
            base.OnLoad(e);
        }


        private void ValidSession()
        {
            //if (Common.SessionManager.Instance.CurrentNavigation == FujifilmUSA.Sync.Entity.ELeftMenuItemLevel.Home)
            //    HttpContext.Current.Response.Redirect("DeviceList.ascx"); // Redirect to main page.


            //if (GetType().BaseType == typeof(OperationSelection) && GetType().BaseType == typeof(ProjectList) && GetType().BaseType == typeof(AddProject))
            //{
            //    if (Common.SessionManager.Instance.CurrentOperation == FujifilmUSA.Sync.Entity.EOperation.None)
            //        HttpContext.Current.Response.Redirect("DeviceList.ascx"); // Redirect to Main Page.
            //}


            //if (GetType().BaseType == typeof(ProjectList))
            //{
            //    if (Common.SessionManager.Instance.CurrentOperation == FujifilmUSA.Sync.Entity.EOperation.None)
            //        HttpContext.Current.Response.Redirect("DeviceList.ascx"); // Redirect to Main Page.
            //}

            //if (((System.Web.UI.Page)(this)).GetType().BaseType == typeof(Export))
            //{
            //    if (Common.SessionManager.Instance.CurrentProject == null)
            //        HttpContext.Current.Response.Redirect("ProjectList.aspx");
            //}

            //if (Common.SessionManager.Instance.CurrentOptimizeRun == null)
            //    HttpContext.Current.Response.Redirect("ProjectList.aspx");

           
            
        }


        public static void IsLogin()
        {
            if (SessionManager.Instance.CurrentUser == null)
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
            }
        }
        public static void IsRoll()
        {
            if (SessionManager.Instance.CurrentUser.Role == null)
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
            }
        }




    }
}