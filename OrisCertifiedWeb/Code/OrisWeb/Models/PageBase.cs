using Oris.Business;
using Oris.Common;
using System;
using System.Web.UI;

namespace OrisWeb.Models
{
    public class PageBase : Page
    {
        #region Constructor
        public PageBase()
        {
            this.Title = WebSettings.SiteTitle;
        }
        #endregion

        #region Exection Handling
        public string HandleException(Exception Exc)
        {
            return (new ErrorLogData()).HandleException(Exc);
        }
        #endregion
    }

}