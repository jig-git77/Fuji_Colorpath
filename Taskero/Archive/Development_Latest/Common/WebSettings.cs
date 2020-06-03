#region System
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Fuji.Common
{
    public class WebSettings
    {
        #region Constructor
        public WebSettings()
        { }
        #endregion

        #region Properties
        public static readonly string ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToString();
        public static readonly string Domain = System.Configuration.ConfigurationSettings.AppSettings["Domain"].ToString();
        public static readonly string Email = System.Configuration.ConfigurationSettings.AppSettings["Email"].ToString();
        public static readonly string Email_FROM = System.Configuration.ConfigurationSettings.AppSettings["Email_FROM"].ToString();
        public static readonly string Pass = System.Configuration.ConfigurationSettings.AppSettings["Pass"].ToString();
        public static readonly string SMTP = System.Configuration.ConfigurationSettings.AppSettings["SMTP"].ToString();
        public static readonly string ErrorLogType = System.Configuration.ConfigurationSettings.AppSettings["ErrorLogType"].ToString();
        public static readonly string SiteTitle = "Taskero Universe";
        public static readonly string IsTrackUserAccessedPages = System.Configuration.ConfigurationSettings.AppSettings["TrackUserAccessedPages"].ToString();
        public static readonly string ImageURL = System.Configuration.ConfigurationSettings.AppSettings["ImageURL"].ToString();

        #endregion
    }
}
