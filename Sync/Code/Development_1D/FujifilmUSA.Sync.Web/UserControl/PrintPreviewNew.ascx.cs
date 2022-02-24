using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class PrintPreviewNew : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ibtnHelp_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/DymoPrinterHelpPreview.html");

        }
    }
}