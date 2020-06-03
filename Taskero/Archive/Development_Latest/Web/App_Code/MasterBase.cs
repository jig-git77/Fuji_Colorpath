#region Using
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Fuji.Business;
#endregion

/// <summary>
/// Summary description for ControlBase
/// </summary>
public class MasterBase : System.Web.UI.MasterPage
{
    
    #region Constructor
    public MasterBase()
    {
        
    }
    #endregion

    #region Exection Handling
    public string HandleException(Exception Exc)
    {
        return (new ErrorLogData()).HandleException(Exc);
    }
    #endregion
}
