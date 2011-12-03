using FujifilmUSA.Sync.Entity;
using System.Web;

namespace FujifilmUSA.Sync.Common
{
    public class SessionManager
    {
        #region Private Variables
        private const string _currentUser = "SESSION_CURRENTUSER";
        private const string _currentSyncDevice = "SESSION_CURRENTSYNCDEVICE";
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

        public static void CheckLogin()
        {
            if(SessionManager.Instance.CurrentUser==null) { HttpContext.Current.Response.Redirect("Login.aspx"); }
        }
    }
}
