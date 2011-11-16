using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Common
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
        public static readonly string SiteTitle = "Taskero Sync USA";
        #endregion
    }
}
