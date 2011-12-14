using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Settings : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
        }

        protected void lnkMeasurementDevice_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MeasurementDeviceList.aspx");
        }
    }
}