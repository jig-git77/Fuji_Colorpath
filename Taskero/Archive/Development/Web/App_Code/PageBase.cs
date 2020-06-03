#region System
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
#endregion

#region Others
using Fuji.Business;
#endregion


public class PageBase : System.Web.UI.Page
{
    #region Constructor
    public PageBase()
    {
        this.Title = Fuji.Common.WebSettings.SiteTitle;
    }
    #endregion

    #region Exection Handling
    public string HandleException(Exception Exc)
    {
        return (new ErrorLogData()).HandleException(Exc);
    }
    #endregion
}
