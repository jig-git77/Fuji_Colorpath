using System;
using System.Web;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web
{
    public class BasePage : System.Web.UI.Page
    {
        #region Page Events
        protected override void InitializeCulture()
        {
            if (SessionManager.Instance.CurrentLanguage != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(SessionManager.Instance.CurrentLanguage);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(SessionManager.Instance.CurrentLanguage);
            }

            base.InitializeCulture();
        }
        protected override void OnLoad(EventArgs e)
        {
            IsLogin();
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
                if (SessionManager.Instance.CurrentDevice == null || SessionManager.Instance.CurrentOperation == ELicenseType.None)
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
                    SessionManager.Instance.CurrentOperation == ELicenseType.None ||
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
                    SessionManager.Instance.CurrentOperation == ELicenseType.None ||
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
                    SessionManager.Instance.CurrentOperation == ELicenseType.None ||
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
                    SessionManager.Instance.CurrentOperation == ELicenseType.None ||
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
                if (SessionManager.Instance.CurrentDevice == null ||
                    SessionManager.Instance.CurrentOperation == ELicenseType.None)
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
                if (SessionManager.Instance.CurrentDevice == null ||
                    SessionManager.Instance.CurrentOperation == ELicenseType.None)
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
                    SessionManager.Instance.CurrentOperation == ELicenseType.None ||
                    SessionManager.Instance.CurrentProject == null||
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
                    SessionManager.Instance.CurrentOperation == ELicenseType.None ||
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
            SessionManager.Instance.CurrentOperation = ELicenseType.None;
            Response.RedirectPermanent("SessionEnd.aspx");
        }
        private void NavigationToNoPermissionPage()
        {
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
            SessionManager.Instance.CurrentOperation = ELicenseType.None;
            Response.RedirectPermanent("Permission.aspx");
        }
        #endregion
    }
}
