using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FujifilmUSA.Sync.Common
{
    public class WebSettings
    {
        #region Constructor
        public WebSettings()
        { }
        #endregion

        #region Properties       

        public static readonly string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        public static readonly string Domain = ConfigurationManager.AppSettings["Domain"];
        public static readonly string Email = ConfigurationManager.AppSettings["Email"];
        public static readonly string Email_FROM = ConfigurationManager.AppSettings["Email_FROM"];
        public static readonly string Pass = ConfigurationManager.AppSettings["Pass"];
        public static readonly string SMTP = ConfigurationManager.AppSettings["SMTP"];
        public static readonly string SiteTitle = "Taskero Sync USA";
        #endregion
    }
}
