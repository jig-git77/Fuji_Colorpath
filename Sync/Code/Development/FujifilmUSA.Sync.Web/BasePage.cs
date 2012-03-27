using System;
using System.Web;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            IsLogin();
            IsRoll();
            ValidSession();
            base.OnLoad(e);
        }


        private void ValidSession()
        {
            //if (GetType().BaseType == typeof(ProjectList))
            //{
            //    Common.SessionManager.Instance.CurrentDevice = null;
            //}
            if (GetType().BaseType == typeof(Index))
            {
            }
            if (GetType().BaseType == typeof(OperationSelection))
            {
                ValidDevice();
            }
            if (GetType().BaseType == typeof(ProjectList) || GetType().BaseType == typeof(PopupClosePrintCondition) || GetType().BaseType == typeof(PopupOpenPrintCondition))
            {
                ValidDevice();
                ValidOpration();
            }
            if (GetType().BaseType == typeof(AddProduct) || GetType().BaseType == typeof(PopupSaveProject))
            {
                ValidDevice();
                ValidOpration();
            }
            if (GetType().BaseType == typeof(Measurement))
            {
                ValidDevice();
                ValidOpration();
                ValidProject();
            }
            if (GetType().BaseType == typeof(Analyze) || GetType().BaseType == typeof(PopupConfirmSave))
            {
                ValidDevice();
                ValidOpration();
                ValidProject();
                ValidOptimizeRun();
            }
            if (GetType().BaseType == typeof(Export))
            {
                ValidDevice();
                ValidOpration();
                ValidProject();
                ValidOptimizeRun();
            }

        }

        private void ValidDevice()
        {
            if (Common.SessionManager.Instance.CurrentDevice == null)
            {
                Common.SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
                Common.SessionManager.Instance.CurrentOperation = EOperation.None;
                Response.RedirectPermanent("SessionEnd.aspx");
            }
        }
        private void ValidOpration()
        {
            if (Common.SessionManager.Instance.CurrentOperation == FujifilmUSA.Sync.Entity.EOperation.None)
            {
                Common.SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
                Common.SessionManager.Instance.CurrentOperation = EOperation.None;
                Response.RedirectPermanent("SessionEnd.aspx");
            }
        }
        private void ValidProject()
        {
            if (Common.SessionManager.Instance.CurrentProject == null)
            {
                Common.SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
                Common.SessionManager.Instance.CurrentOperation = EOperation.None;
                Response.RedirectPermanent("SessionEnd.aspx");
            }
        }
        private void ValidOptimizeRun()
        {
            if (Common.SessionManager.Instance.CurrentOptimizeRun == null)
            {
                Common.SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
                Common.SessionManager.Instance.CurrentOperation = EOperation.None;
                Response.RedirectPermanent("SessionEnd.aspx");
            }
        }



        public void IsLogin()
        {
            if (SessionManager.Instance.CurrentUser == null)
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
            }
        }
        public void IsRoll()
        {
            if (SessionManager.Instance.CurrentUser.Role == null)
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
            }
        }




    }
}