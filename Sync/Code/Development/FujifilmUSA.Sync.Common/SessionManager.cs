using System.Collections.Generic;
using System.Web;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Common
{
    public class SessionManager
    {
        #region Private Variables

        private const string _userSession = "UserSession";

        #endregion

        #region Singleton

        private SessionManager() { }

        private static readonly SessionManager _instance = new SessionManager();

        public static SessionManager Instance
        {
            get { return _instance; }
        }
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
        #endregion
    }
}
