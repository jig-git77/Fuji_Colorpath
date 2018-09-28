#region System
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
#endregion

#region Others
using System.Data.SqlClient;
using Fuji.Common;
using Fuji.Business;
using Fuji.DataAccess;
#endregion

public partial class TaskeroReports : PageBase
{
    #region Page Load & Event
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Message ==========//
            LblError.Text = "";

            //========== Check User Login ==========//
            Sessions.CheckUserLogin();

            //========== Data List Settings ==========//
            RPT_Reports_Press.Attributes.Add("align", "left");
            RPT_Reports_PrePress.Attributes.Add("align", "left");
            RPT_Reports_Miscelleneaous.Attributes.Add("align", "left");

            //========== Default Settings ==========//
            if (!IsPostBack)
            {
                //========== Fill Reports ==========//
                FillReports();
                
                if (System.IO.Directory.Exists(Server.MapPath(".") + "\\UploadFiles") == false)
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(".") + "\\UploadFiles");
                    System.IO.Directory.CreateDirectory(Server.MapPath(".") + "\\UploadFiles\\Report_PDF");
                    System.IO.Directory.CreateDirectory(Server.MapPath(".") + "\\UploadFiles\\temp");
                }
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    #endregion

    #region Method And Function
    public void FillReports()
    {
        try
        {
            //==========  Declaring Objects ==========//
            ReportData objReport = new ReportData();

            //==========  Get Data Table and Bind ==========//spCompanySelectByUserID
            DataTable DT_ReportType = objReport.uspGetAllReportsOnTaskeroUserID(Sessions.UserID);

            //========== Fill DDL ==========//

            DT_ReportType.DefaultView.RowFilter = "ReportTypeID = 1";
            RPT_Reports_PrePress.DataSource = DT_ReportType.DefaultView;
            RPT_Reports_PrePress.DataBind();

            if (RPT_Reports_PrePress.Items.Count == 0)
            {
                TRContentPrePress.Visible = false;
                TRMessagePrePress.Visible = true;
            }

            DT_ReportType.DefaultView.RowFilter = "ReportTypeID = 2";
            RPT_Reports_Press.DataSource = DT_ReportType.DefaultView;
            RPT_Reports_Press.DataBind();
            if (RPT_Reports_Press.Items.Count == 0)
            {
                TRContentPress.Visible = false;
                TRMessagePress.Visible = true;
            }

            
            DT_ReportType.DefaultView.RowFilter = "ReportTypeID = 3";
            RPT_Reports_Miscelleneaous.DataSource = DT_ReportType.DefaultView;
            RPT_Reports_Miscelleneaous.DataBind();
            if (RPT_Reports_Miscelleneaous.Items.Count == 0)
            {
                TRContentMiscelleneaous.Visible = false;
                TRMessageMiscelleneaous.Visible = true;
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

   

}
