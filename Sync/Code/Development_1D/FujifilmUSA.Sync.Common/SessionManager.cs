using System.Web;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Common
{
    public class SessionManager
    {
        #region Private Variables
        private const string _userSession = "UserSession";
        private const string _deviceSession = "DeviceSession";
        private const string _projectSession = "ProjectSession";
        private const string _optimizeRunSession = "OptimizeRunSession";
        private const string _navigationSession = "NavigationSession";
        private const string _operationSession = "OperationSession";
        private const string _dlpSubOperation = "DLPSubOperation";
        private const string _branchNameSession = "BranchNameSession";
        private const string _projLimitSession = "ProjectLimitSession";
        private const string _colorOrgRequest = "ColorOrgRequest";        
        private const string _selectedCompany = "SelectedCompany";
        private const string _selectedBranch = "SelectedBranch";
        private const string _pageErrorMessage = "PageErrorMessage";
        private const string _projectListSession = "projectListSession";
        private const string _curveExportView = "CurveExportView";
        private const string _curveDeltaRefView = "CurveDeltaRefView";
        private const string _curveDeltaPrevRunView = "CurveDeltaPrevRunView";
        
        
        private const string _validationRequest = "ValidationRequest";
        private const string _coErrorMessage = "COErrorMessage";
        #endregion

        #region Singleton

        private SessionManager() { }

        private static readonly SessionManager _instance = new SessionManager();

        public static SessionManager Instance
        {
            get { return _instance; }
        }

        //public static void CheckLogin()
        //{
        //    if (SessionManager.Instance.CurrentUser == null)
        //    {
        //        HttpContext.Current.Response.Redirect("Login.aspx");
        //    }
        //}

        #endregion

        #region Sessions
        public User CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session[_userSession] != null)
                {
                    return (User)HttpContext.Current.Session[_userSession];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_userSession] = value;
            }
        }

        public Device CurrentDevice
        {
            get
            {
                if (HttpContext.Current.Session[_deviceSession] != null)
                {
                    return (Device)HttpContext.Current.Session[_deviceSession];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_deviceSession] = value;
            }
        }

        public Project CurrentProject
        {
            get
            {
                if (HttpContext.Current.Session[_projectSession] != null)
                {
                    return (Project)HttpContext.Current.Session[_projectSession];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_projectSession] = value;
            }
        }

        public OptimizeRun CurrentOptimizeRun
        {
            get
            {
                if (HttpContext.Current.Session[_optimizeRunSession] != null)
                {
                    return (OptimizeRun)HttpContext.Current.Session[_optimizeRunSession];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_optimizeRunSession] = value;
            }
        }

        public ELeftMenuItemLevel CurrentNavigation
        {
            get
            {
                if (HttpContext.Current.Session[_navigationSession] != null)
                {
                    return (ELeftMenuItemLevel)System.Enum.Parse(typeof(ELeftMenuItemLevel), HttpContext.Current.Session[_navigationSession].ToString());
                }
                return ELeftMenuItemLevel.Home;
            }
            set
            {
                HttpContext.Current.Session[_navigationSession] = value;
            }
        }

        //public ELicenseType CurrentOperation
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[_operationSession] != null)
        //        {
        //            return (ELicenseType)System.Enum.Parse(typeof(ELicenseType), HttpContext.Current.Session[_operationSession].ToString());
        //        }
        //        return ELicenseType.None;
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[_operationSession] = value;
        //    }
        //}

        public ELicenseType CurrentDlpSubOperation
        {
            get
            {
                if (HttpContext.Current.Session[_dlpSubOperation] != null)
                {
                    return (ELicenseType)System.Enum.Parse(typeof(ELicenseType), HttpContext.Current.Session[_dlpSubOperation].ToString());
                }
                return ELicenseType.None;
            }
            set
            {
                HttpContext.Current.Session[_dlpSubOperation] = value;
            }
        }

        public string CurrentBranchName
        {
            get
            {
                if (HttpContext.Current.Session[_branchNameSession] != null)
                {
                    return HttpContext.Current.Session[_branchNameSession].ToString();
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_branchNameSession] = value;
            }
        }

        public int CurrentProjLimit
        {
            get
            {
                if (HttpContext.Current.Session[_projLimitSession] != null)
                {
                    return System.Convert.ToInt32(HttpContext.Current.Session[_projLimitSession].ToString());
                }
                return 0;
            }
            set
            {
                HttpContext.Current.Session[_projLimitSession] = value;
            }
        }

        public TempProjectInfo CurrentColorOrgRequest
        {
            get
            {
                if (HttpContext.Current.Session[_colorOrgRequest] != null)
                {
                    return (TempProjectInfo)HttpContext.Current.Session[_colorOrgRequest];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_colorOrgRequest] = value;
            }
        }

        public Entity.CPSyncV.TempValidationInfo CurrentValidationRequest
        {
            get
            {
                if (HttpContext.Current.Session[_validationRequest] != null)
                {
                    return (Entity.CPSyncV.TempValidationInfo)HttpContext.Current.Session[_validationRequest];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_validationRequest] = value;
            }
        }

        

        public int SelectedCompany
        {
            get
            {
                if (HttpContext.Current.Session[_selectedCompany] != null)
                {
                    return System.Convert.ToInt32(HttpContext.Current.Session[_selectedCompany]);
                }
                return 0;
            }
            set
            {
                HttpContext.Current.Session[_selectedCompany] = value;
            }
        }

        public int SelectedBranch
        {
            get
            {
                if (HttpContext.Current.Session[_selectedBranch] != null)
                {
                    return System.Convert.ToInt32(HttpContext.Current.Session[_selectedBranch]);
                }
                return 0;
            }
            set
            {
                HttpContext.Current.Session[_selectedBranch] = value;
            }
        }

        public string PageErrorMessage
        {
            get
            {
                if (HttpContext.Current.Session[_pageErrorMessage] != null)
                {
                    return HttpContext.Current.Session[_pageErrorMessage].ToString();
                }
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session[_pageErrorMessage] = value;
            }
        }

        public List<Project> ProjectList
        {
            get
            {
                if (HttpContext.Current.Session[_projectListSession] != null)
                {
                    return (List<Project>)HttpContext.Current.Session[_projectListSession];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_projectListSession] = value;
            }
        }

        public List<Entity.CPSync1D.OptimizeRunCurve> CurveExportView
        {
            get
            {
                if (HttpContext.Current.Session[_curveExportView] != null)
                {
                    return (List<Entity.CPSync1D.OptimizeRunCurve>)HttpContext.Current.Session[_curveExportView];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_curveExportView] = value;
            }
        }        

        public List<Entity.CPSync1D.OptimizeRunCurve> CurveDeltaRefView
        {
            get
            {
                if (HttpContext.Current.Session[_curveDeltaRefView] != null)
                {
                    return (List<Entity.CPSync1D.OptimizeRunCurve>)HttpContext.Current.Session[_curveDeltaRefView];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_curveDeltaRefView] = value;
            }
        }

        public List<Entity.CPSync1D.OptimizeRunCurve> CurveDeltaPrevRunView
        {
            get
            {
                if (HttpContext.Current.Session[_curveDeltaPrevRunView] != null)
                {
                    return (List<Entity.CPSync1D.OptimizeRunCurve>)HttpContext.Current.Session[_curveDeltaPrevRunView];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_curveDeltaPrevRunView] = value;
            }
        }

        public COErrorMessage COErrorMessage
        {
            get
            {
                if (HttpContext.Current.Session[_coErrorMessage] != null)
                {
                    return HttpContext.Current.Session[_coErrorMessage] as COErrorMessage;
                }
                return new COErrorMessage();
            }
            set
            {
                HttpContext.Current.Session[_coErrorMessage] = value;
            }
        }

        #endregion
    }
}
