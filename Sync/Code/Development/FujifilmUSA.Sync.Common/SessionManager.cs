using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using System.Web;

namespace FujifilmUSA.Sync.Common
{
    public class SessionManager
    {
        #region Private Variables
        private const string _currentUser = "SESSION_CURRENTUSER";
        private const string _currentSyncDevice = "SESSION_CURRENTSYNCDEVICE";
        private const string _dictionaryList = "SESSION_DICTIONARYLIST";
        private const string _currentProject = "SESSION_CURRENTPROJECT";
        private const string _currentSyncRun = "SESSION_CURRENTSYNCRUN";
        private const string _currentNavigation = "SESSION_CURRENTNAVIGATION";
        #endregion

        #region Singleton

        private SessionManager() { }

        private static readonly SessionManager _instance = new SessionManager();
        
        public static SessionManager Instance
        {
            get { return _instance; }
        }
        #endregion

        public TaskeroUser CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session[_currentUser] != null)
                {
                    return (TaskeroUser)HttpContext.Current.Session[_currentUser];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_currentUser] = value;
            }
        }
        public SyncDevice CurrentSyncDevice
        {
            get
            {
                if (HttpContext.Current.Session[_currentSyncDevice] != null)
                {
                    return (SyncDevice)HttpContext.Current.Session[_currentSyncDevice];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_currentSyncDevice] = value;
            }
        }
        public Project CurrentProject
        {
            get
            {
                if (HttpContext.Current.Session[_currentProject] != null)
                {
                    return (Project)HttpContext.Current.Session[_currentProject];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_currentProject] = value;
            }
        }

        public SyncRun CurrentSyncRun
        {
            get
            {
                if (HttpContext.Current.Session[_currentSyncRun] != null)
                {
                    return (SyncRun)HttpContext.Current.Session[_currentSyncRun];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_currentSyncRun] = value;
            }
        }

        public Dictionary<int, string> DictionaryList
        {
            get
            {
                if (HttpContext.Current.Session[_dictionaryList] != null)
                {
                    return (Dictionary<int, string>)HttpContext.Current.Session[_dictionaryList];
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[_dictionaryList] = value;
            }
        }

        public LeftMenuItemLevel CurrentNavigation
        {
            get
            {
                if (HttpContext.Current.Session[_currentNavigation] != null)
                {
                    return (LeftMenuItemLevel) System.Enum.Parse(typeof (LeftMenuItemLevel), HttpContext.Current.Session[_currentNavigation].ToString());
                }
                return LeftMenuItemLevel.Home;
            }
            set
            {
                HttpContext.Current.Session[_currentNavigation] = value;
            }
        }

        public static void CheckLogin()
        {
            if(SessionManager.Instance.CurrentUser==null) { HttpContext.Current.Response.Redirect("Login.aspx"); }
        }
    }
}
