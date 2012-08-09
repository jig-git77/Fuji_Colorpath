using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web
{
    public partial class ReportSingleMeasurement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["MeasurementID"] != null)
                {
                    TextBox txt_MeasurementID = ucReportSingle.FindControl("txt_MeasurementID") as TextBox;
                    txt_MeasurementID.Text = Request.QueryString["MeasurementID"].ToString();
                }
                if (Request.QueryString["From_ΔE"] != null)
                {
                    TextBox txt_dTE = ucReportSingle.FindControl("txt_dTE") as TextBox;
                    txt_dTE.Text = Request.QueryString["From_ΔE"].ToString();
                }
                if (Request.QueryString["DLPName"] != null)
                {
                    TextBox txt_DLPName = ucReportSingle.FindControl("txt_DLPName") as TextBox;
                    txt_DLPName.Text = Request.QueryString["DLPName"].ToString();
                }
            }
        }
    }
}