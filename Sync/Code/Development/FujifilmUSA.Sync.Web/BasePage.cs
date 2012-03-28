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
            SessionAudit();
            base.OnLoad(e);
        }

        private void SessionAudit()
        {
            if (    GetType().BaseType != typeof(Index) || 
                    GetType().BaseType != typeof(OperationSelection) || 
                    GetType().BaseType != typeof(ProjectList) || 
                    GetType().BaseType != typeof(PopupClosePrintCondition) || 
                    GetType().BaseType != typeof(PopupOpenPrintCondition) ||
                    GetType().BaseType != typeof(AddProduct) || 
                    GetType().BaseType != typeof(PopupSaveProject) ||
                    GetType().BaseType != typeof(Measurement) ||
                    GetType().BaseType != typeof(Analyze) || 
                    GetType().BaseType != typeof(PopupConfirmSave) ||
                    GetType().BaseType != typeof(Export) )
                return;

            //if (GetType().BaseType == typeof(ProjectList))
            //{
            //    SessionManager.Instance.CurrentDevice = null;
            //}

            if (GetType().BaseType == typeof(Index))
            {
            }
            else if (GetType().BaseType == typeof(OperationSelection))
            {
                IsDeviceSession();
            }
            else if (GetType().BaseType == typeof(ProjectList) || GetType().BaseType == typeof(PopupClosePrintCondition) || GetType().BaseType == typeof(PopupOpenPrintCondition))
            {
                IsDeviceSession();
                ISOprationSession();
            }
            else if (GetType().BaseType == typeof(AddProduct) || GetType().BaseType == typeof(PopupSaveProject))
            {
                IsDeviceSession();
                ISOprationSession();
            }
            else if (GetType().BaseType == typeof(Measurement))
            {
                IsDeviceSession();
                ISOprationSession();
                IsProjectSession();
            }
            else if (GetType().BaseType == typeof(Analyze) || GetType().BaseType == typeof(PopupConfirmSave))
            {
                IsDeviceSession();
                ISOprationSession();
                IsProjectSession();
                IsOptimizeRunSession();
            }
            else if (GetType().BaseType == typeof(Export))
            {
                IsDeviceSession();
                ISOprationSession();
                IsProjectSession();
                IsOptimizeRunSession();
            }
        }

        private void IsDeviceSession()
        {
            if (SessionManager.Instance.CurrentDevice == null)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
                SessionManager.Instance.CurrentOperation = EOperation.None;
                Response.RedirectPermanent("SessionEnd.aspx");
            }
        }
        private void ISOprationSession()
        {
            if (SessionManager.Instance.CurrentOperation == EOperation.None)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
                SessionManager.Instance.CurrentOperation = EOperation.None;
                Response.RedirectPermanent("SessionEnd.aspx");
            }
        }
        private void IsProjectSession()
        {
            if (SessionManager.Instance.CurrentProject == null)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
                SessionManager.Instance.CurrentOperation = EOperation.None;
                Response.RedirectPermanent("SessionEnd.aspx");
            }
        }
        private void IsOptimizeRunSession()
        {
            if (SessionManager.Instance.CurrentOptimizeRun == null)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
                SessionManager.Instance.CurrentOperation = EOperation.None;
                Response.RedirectPermanent("SessionEnd.aspx");
            }
        }

        public void IsLogin()
        {
            if (SessionManager.Instance.CurrentUser == null || SessionManager.Instance.CurrentUser.Role == null)
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
            }
        }




    }
}