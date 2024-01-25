using System;
using System.Web.UI;
using Taskero.Business;
using Taskero.Common;

namespace TaskeroMobile
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