#region System
using System.Configuration;
#endregion

namespace Oris.Common
{
    public class WebSettings
    {
        #region Constructor
        public WebSettings()
        { }
        #endregion

        #region Properties
        public static readonly string ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
        public static readonly string Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
        public static readonly string Email = ConfigurationSettings.AppSettings["Email"].ToString();
        public static readonly string Pass = ConfigurationSettings.AppSettings["Pass"].ToString();
        public static readonly string SMTP = ConfigurationSettings.AppSettings["SMTP"].ToString();
        public static readonly string ErrorLogType = ConfigurationSettings.AppSettings["ErrorLogType"].ToString();
        public static readonly string SiteTitle = "CGS Publishing Technologies International";
        public static readonly string IsTrackUserAccessedPages = ConfigurationSettings.AppSettings["TrackUserAccessedPages"].ToString();
        public static readonly string ImageURL = ConfigurationSettings.AppSettings["ImageURL"].ToString();
        #endregion
    }
}
