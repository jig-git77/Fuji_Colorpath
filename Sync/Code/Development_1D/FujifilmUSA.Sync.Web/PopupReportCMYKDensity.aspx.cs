using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web
{
    public partial class PopupReportCMYKDensity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["OptimizeRunID"] != null)
                {
                    TextBox txt_OptimizeRunID = ucReportCMYKDensityWindow.FindControl("txt_OptimizeRunID") as TextBox;
                    txt_OptimizeRunID.Text = Request.QueryString["OptimizeRunID"].ToString();
                }
              
            }
        }
    }
}