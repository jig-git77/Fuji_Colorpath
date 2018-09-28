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
using Fuji.Common;
using Fuji.Business;
#endregion


public partial class Home : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //========== Check User Login ==========//
        Sessions.CheckUserLoginForMobi();
    }   

    
}
