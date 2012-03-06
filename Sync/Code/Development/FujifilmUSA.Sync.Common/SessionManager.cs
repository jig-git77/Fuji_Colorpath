using System.Collections.Generic;
using System.Web;

namespace FujifilmUSA.Sync.Common
{
    public class SessionManager
    {
        #region Singleton

        private SessionManager() { }

        private static readonly SessionManager _instance = new SessionManager();
        
        public static SessionManager Instance
        {
            get { return _instance; }
        }
        #endregion
    }
}
