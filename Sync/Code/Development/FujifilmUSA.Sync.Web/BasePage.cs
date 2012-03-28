using System;
using System.Web;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web
{
    public class BasePage : System.Web.UI.Page
    {
        public bool IsErrorOccured { get; set; }
        protected override void OnLoad(EventArgs e)
        {
            IsLogin();
            //if (GetType().BaseType == typeof(ProjectList))
            //{
            //    if(SessionManager.Instance.CurrentDevice == null)
            //    {
            //        NavigationToSessionEndPage();
            //    }
            //    if(!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.CreateDeleteProject))
            //    {
            //        ((ProjectList)this).SetAddProjectVisibility();
            //    }
            //}


            SessionAudit();
            PermissionAudit();
            base.OnLoad(e);
            IsErrorOccured = false;
        }

        #region "User Login"
        public void IsLogin()
        {
            if (SessionManager.Instance.CurrentUser == null || SessionManager.Instance.CurrentUser.Role == null)
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
            }
        }
        #endregion

        #region "Session Management"
        
        private void SessionAudit()
        {
            if (IsAdminPage)
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
        private void NavigationToSessionEndPage()
        {
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
            SessionManager.Instance.CurrentOperation = EOperation.None;
            Response.RedirectPermanent("SessionEnd.aspx");
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

        #endregion

        #region "User Role Premission"

        public void PermissionAudit()
        {
            ProjectPermission();
            AdminPermission();
        }

        #region "Admin Role Meanagement"

        public void AdminPermission()
        {
            if (!IsAdminPage)
                return;

            if (!( HasManageBranchPermission || 
                    HasManageCompanyPermission ||
                    HasManageDevicePermission ||
                    HasManageLicensePermission ||
                    HasManageSourceProfilePermission ||
                    HasManageUserPermission ||
                    HasManageMeasurementDevicePermission ))
                Response.RedirectPermanent("Permission.aspx");
        }

        public static bool HasAdminRolePermission
        {
            get
            {
                return SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageBranch) ||
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageCompany) ||
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageDevice) ||
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageLicense) ||
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageMeasurementDevice) ||
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageRegion) ||
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageUser) ||
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageSourceProfile);
            }
        }

        public bool HasManageBranchPermission
        {
            get
            {
                return (GetType().BaseType == typeof(Settings) ||
                        GetType().BaseType == typeof (BranchManagement) ||
                        GetType().BaseType == typeof (AddBranchManagement)) &&
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageBranch);
            }
        }

        public bool HasManageCompanyPermission
        {
            get
            {
                return (GetType().BaseType == typeof(Settings) ||
                        GetType().BaseType == typeof(CompanyManagement) ||
                        GetType().BaseType == typeof(AddCompanyManagement)) &&
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageCompany);
            }
        }

        public bool HasManageDevicePermission
        {
            get
            {
                return (GetType().BaseType == typeof(Settings) || 
                        GetType().BaseType == typeof(DeviceManagement) ||
                        GetType().BaseType == typeof(AddDeviceManagement) ||
                        GetType().BaseType == typeof(AddDeviceFamily) ) &&
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageDevice);
            }
        }

        public bool HasManageLicensePermission
        {
            get
            {
                return (GetType().BaseType == typeof(Settings) ||
                        GetType().BaseType == typeof(LicenseManagement) ||
                        GetType().BaseType == typeof(AddLicenseManagement) ||
                        GetType().BaseType == typeof(AddLicenseType)) &&
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageLicense);
            }
        }

        public bool HasManageMeasurementDevicePermission
        {
            get
            {
                return (GetType().BaseType == typeof(Settings) ||
                        GetType().BaseType == typeof(MeasurementDeviceList) ||
                        GetType().BaseType == typeof(AddMeasurementDevice) ) &&
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageMeasurementDevice);
            }
        }

        public bool HasManageSourceProfilePermission
        {
            get
            {
                return (GetType().BaseType == typeof(Settings) ||
                        GetType().BaseType == typeof(MeasurementDeviceList) ||
                        GetType().BaseType == typeof(AddMeasurementDevice)) &&
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageSourceProfile);
            }
        }

        public bool HasManageUserPermission
        {
            get
            {
                return (GetType().BaseType == typeof(Settings) ||
                        GetType().BaseType == typeof(UserManagement) ||
                        GetType().BaseType == typeof(AddUserManagement)) &&
                       SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageUser);
            }
        }

        #endregion

        #region "Project Role Management"

        public void ProjectPermission()
        {
            if (IsAdminPage)
                return;

            if (!HasProjectPermission)
                Response.RedirectPermanent("Permission.aspx");
        }
        
        public bool HasCreateDeleteProjectPermission
        {
            get
            {
                return GetType().BaseType == typeof(ProjectList) && SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.CreateDeleteProject);
            }
        }

        public bool HasModifyProjectPermission
        {
            get
            {
                return GetType().BaseType == typeof(ProjectList) && SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject);
            }
        }

        public bool HasProjectPermission
        {
            get
            {
                if (
                                GetType().BaseType == typeof(Measurement) ||
                                GetType().BaseType == typeof(Analyze) ||
                                GetType().BaseType == typeof(PopupConfirmSave) ||
                                GetType().BaseType == typeof(Export)
                        )
                    return (
                               SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.CreateDeleteProject) ||
                               SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject)
                           );
                else
                {
                    return true;
                }
            }
        }
        #endregion

        #endregion

        //find base page type
        private bool IsAdminPage
        {
            get
            {
                return 
                        GetType().BaseType != typeof (Index) &&
                        GetType().BaseType != typeof (OperationSelection) &&
                        GetType().BaseType != typeof (PopupClosePrintCondition) &&
                        GetType().BaseType != typeof (PopupOpenPrintCondition) &&
                        GetType().BaseType != typeof (AddProduct) &&
                        GetType().BaseType != typeof (PopupSaveProject) &&
                        GetType().BaseType != typeof (Measurement) &&
                        GetType().BaseType != typeof (Analyze) &&
                        GetType().BaseType != typeof (PopupConfirmSave) &&
                        GetType().BaseType != typeof (Export);
            }
        }


    }
}
