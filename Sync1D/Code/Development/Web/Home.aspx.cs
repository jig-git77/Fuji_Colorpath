#region
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

public partial class Home : PageBase
{
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                //========== Check Session ===========//
                Fuji.Common.Sessions.CheckUserLogin();
                IMG_LargeImage.Src = "Branding/LargeImage/" + Fuji.Common.Sessions.LargeImage.ToString();
            }
        }
        catch (Exception Ex)
        {
            HandleException(Ex);
        }
    }
    #endregion
}
