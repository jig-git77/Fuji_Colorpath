using System;
using System.Web;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Threading;
using FujifilmUSA.Sync.Presenter;

namespace FujifilmUSA.Sync.Web
{
    public class BasePage : System.Web.UI.Page
    {
        #region Page Events
        protected override void InitializeCulture()
        {
            if (SessionManager.Instance.CurrentUser != null && !string.IsNullOrEmpty(SessionManager.Instance.CurrentUser.LanguageCultureCode))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(SessionManager.Instance.CurrentUser.LanguageCultureCode);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(SessionManager.Instance.CurrentUser.LanguageCultureCode);
                Page.ClientScript.RegisterClientScriptInclude("JSGlobalResource", ResolveUrl("JS/GlobalResource." + System.Threading.Thread.CurrentThread.CurrentUICulture.ToString() + ".js"));
                //Page.ClientScript.RegisterClientScriptInclude("KendoJSGlobalResource", ResolveUrl("Scripts/kendo/2018.1.221/messages/kendo.messages." + System.Threading.Thread.CurrentThread.CurrentUICulture.ToString() + ".min.js"));
            }
            else //Load default English (en-US) .js file
            {
                Page.ClientScript.RegisterClientScriptInclude("JSGlobalResource", ResolveUrl("JS/GlobalResource.en-US.js"));
                //Page.ClientScript.RegisterClientScriptInclude("KednoJSGlobalResource", ResolveUrl("Scripts/kendo/2018.1.221/messages/kendo.messages.en-US.min.js"));
            }

            base.InitializeCulture();
        }
        protected override void OnLoad(EventArgs e)
        {
            IsLogin();

            #region Display error message in Statusbar control
            if (!string.IsNullOrEmpty(SessionManager.Instance.PageErrorMessage))
            {
                System.Web.UI.Control control = this.FindStatusControl() as System.Web.UI.Control;
                if (control != null)
                {
                    if (control.GetType().BaseType == typeof(FujifilmUSA.Sync.Web.UserControl.Statusbar))
                        ((FujifilmUSA.Sync.Web.UserControl.Statusbar)control).DisplayMessage(SessionManager.Instance.PageErrorMessage, false);
                    else if (control.GetType().BaseType == typeof(FujifilmUSA.Sync.Web.UserControl.AdminStatusbar))
                        ((FujifilmUSA.Sync.Web.UserControl.AdminStatusbar)control).DisplayMessage(SessionManager.Instance.PageErrorMessage, false);
                }
            }
            else
            {
                System.Web.UI.Control control = this.FindStatusControl() as System.Web.UI.Control;
                if (control != null)
                {
                    if (control.GetType().BaseType == typeof(FujifilmUSA.Sync.Web.UserControl.Statusbar))
                        ((FujifilmUSA.Sync.Web.UserControl.Statusbar)control).Visible = false;
                    else if (control.GetType().BaseType == typeof(FujifilmUSA.Sync.Web.UserControl.AdminStatusbar))
                        ((FujifilmUSA.Sync.Web.UserControl.AdminStatusbar)control).Visible = false;

                }

            }
            #endregion
            //Flush error message
            SessionManager.Instance.PageErrorMessage = null;

            #region Admin Pages
            if (GetType().BaseType == typeof(AddBranchManagement))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageBranch))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(AddCompanyManagement))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageCompany))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(AddDeviceFamily))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageDevice))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(AddDeviceManagement))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageDevice))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(AddLicenseManagement))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageLicense))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(AddLicenseType))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageLicense))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(AddMeasurementDevice))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageMeasurementDevice))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(AddSourceProfile))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageSourceProfile))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(AddUserManagement))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageUser))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(BranchManagement))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageBranch))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(CompanyManagement))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageCompany))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(DeviceManagement))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageDevice))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(LicenseManagement))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageLicense))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(MeasurementDeviceList))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageMeasurementDevice))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(SourceProfileManagement))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageSourceProfile))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(UserManagement))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageUser))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(ApplicationConfiguration))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageApplicationConfiguration))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(ActivityValidationRules))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageActivityValidationRules))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(AddDeviceProfilerSettings))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageDeviceProfileSettings))
                {
                    NavigationToNoPermissionPage();
                }
            }
            else if (GetType().BaseType == typeof(SpotTargetManagement))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageSpotTarget))
                {
                    NavigationToNoPermissionPage();
                }
            }
            #endregion
            #region Regular Pages
            else if (GetType().BaseType == typeof(OperationSelection))
            {
                if (SessionManager.Instance.CurrentDevice == null)
                {
                    NavigationToSessionEndPage();
                }
            }
            else if (GetType().BaseType == typeof(ProjectList))
            {
                if (SessionManager.Instance.CurrentDevice == null)
                {
                    NavigationToSessionEndPage();
                }
            }
            else if (GetType().BaseType == typeof(AddProject))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.CreateDeleteProject))
                {
                    NavigationToNoPermissionPage();
                }
                if (SessionManager.Instance.CurrentDevice == null ||
                    SessionManager.Instance.CurrentProject == null)
                {
                    NavigationToSessionEndPage();
                }
            }
            else if (GetType().BaseType == typeof(Measurement))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject))
                {
                    NavigationToNoPermissionPage();
                }
                if (SessionManager.Instance.CurrentDevice == null ||
                    SessionManager.Instance.CurrentProject == null ||
                    SessionManager.Instance.CurrentOptimizeRun == null)
                {
                    NavigationToSessionEndPage();
                }
            }
            else if (GetType().BaseType == typeof(Analyze))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject))
                {
                    NavigationToNoPermissionPage();
                }
                if (SessionManager.Instance.CurrentDevice == null ||
                    SessionManager.Instance.CurrentProject == null ||
                    SessionManager.Instance.CurrentOptimizeRun == null)
                {
                    NavigationToSessionEndPage();
                }
            }
            else if (GetType().BaseType == typeof(Export))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject))
                {
                    NavigationToNoPermissionPage();
                }
                if (SessionManager.Instance.CurrentDevice == null ||
                    SessionManager.Instance.CurrentProject == null ||
                    SessionManager.Instance.CurrentOptimizeRun == null)
                {
                    NavigationToSessionEndPage();
                }
            }
            else if (GetType().BaseType == typeof(PopupClosePrintCondition))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject))
                {
                    NavigationToNoPermissionPage();
                }
                if (SessionManager.Instance.CurrentDevice == null)
                {
                    NavigationToSessionEndPage();
                }
            }
            else if (GetType().BaseType == typeof(PopupOpenPrintCondition))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject))
                {
                    NavigationToNoPermissionPage();
                }
                if (SessionManager.Instance.CurrentDevice == null)
                {
                    NavigationToSessionEndPage();
                }
            }
            else if (GetType().BaseType == typeof(PopupConfirmSave))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject))
                {
                    NavigationToNoPermissionPage();
                }
                if (SessionManager.Instance.CurrentDevice == null ||
                    SessionManager.Instance.CurrentProject == null ||
                    SessionManager.Instance.CurrentOptimizeRun == null)
                {
                    NavigationToSessionEndPage();
                }
            }
            else if (GetType().BaseType == typeof(PopupSaveProject))
            {
                if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.CreateDeleteProject))
                {
                    NavigationToNoPermissionPage();
                }
                if (SessionManager.Instance.CurrentDevice == null ||
                    SessionManager.Instance.CurrentProject == null)
                {
                    NavigationToSessionEndPage();
                }
            }
            #endregion
            //No need to check anything for this page...
            //else if (GetType().BaseType == typeof(Settings))
            //{
            //    //Do nothing
            //}
            //No need to check anything for this page...
            //else if (GetType().BaseType == typeof(Index))
            //{
            //    //Do nothing
            //}
            //No need to check anything for this page...
            //else if (GetType().BaseType == typeof(UserControl.AddProject.ShowImage))
            //{
            //    //Do Nothing
            //}
            base.OnLoad(e);
        }
        protected override void OnError(EventArgs e)
        {
            var ex = Server.GetLastError();
            if (ex is ThreadAbortException)
                return;

            var presenterGlobal = new PresenterGlobal();
            presenterGlobal.LogApplicationError(ex.GetBaseException());

            SessionManager.Instance.PageErrorMessage = ex.Message;

            Server.ClearError();
            Response.Redirect(this.Request.Url.ToString());
        }
        #endregion

        #region Methods
        private void IsLogin()
        {
            if (SessionManager.Instance.CurrentUser == null)
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
            }
        }
        private void NavigationToSessionEndPage()
        {
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
            SessionManager.Instance.CurrentProject = null;
            Response.Redirect("SessionEnd.aspx");
        }
        private void NavigationToNoPermissionPage()
        {
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
            SessionManager.Instance.CurrentProject = null;
            Response.RedirectPermanent("Permission.aspx");
        }
        private System.Web.UI.Control FindStatusControl()
        {
            var targetControlID = "fujiStatusbar";
            System.Web.UI.Control targetControl = null;
            foreach (System.Web.UI.Control item in this.Controls)
            {
                targetControl = FindControlFromNestedCollection(targetControlID, item);
                if (targetControl != null && targetControl.ID == targetControlID)
                    break;
            }
            return targetControl;
        }
        private System.Web.UI.Control FindControlFromNestedCollection(string targetControlID, System.Web.UI.Control control)
        {
            if (control.ID == targetControlID)
            {
                return control;
            }
            else
            {
                foreach (System.Web.UI.Control item in control.Controls)
                {
                    if (item.HasControls())
                    {
                        System.Web.UI.Control lookFor = FindControlFromNestedCollection(targetControlID, item);
                        if (lookFor != null && lookFor.ID == targetControlID)
                            return lookFor;
                    }
                }

            }
            return null;
        }
        #endregion
    }
}
