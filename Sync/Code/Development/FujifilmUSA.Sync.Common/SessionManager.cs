using System.Web;
using FujifilmUSA.Sync.Entity;

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

        public EOperation CurrentOperation
        {
            get
            {
                if (HttpContext.Current.Session[_operationSession] != null)
                {
                    return (EOperation)System.Enum.Parse(typeof(EOperation), HttpContext.Current.Session[_operationSession].ToString());
                }
                return EOperation.None;
            }
            set
            {
                HttpContext.Current.Session[_operationSession] = value;
            }
        }
        #endregion
    }
}
