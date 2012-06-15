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

public partial class AccessDenied : PageBase
{
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //==========  Clear Error ==========//
            LblError.Text = "";

            //==========  Default Settings ==========//
            if (IsPostBack == false)
            {
                //========= Check User Login =========//
                Fuji.Common.Sessions.CheckUserLogin();
            }
        }
        catch (Exception exc)
        {            
            LblError.Text = HandleException(exc);
        }        
    }
    #endregion
}
