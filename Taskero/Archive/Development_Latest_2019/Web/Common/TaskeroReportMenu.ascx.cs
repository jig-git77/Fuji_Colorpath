#region Using
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

public partial class Common_TaskeroReportMenu : ControlBase
{
    #region Declarations
    protected string _STYLE = "";
    #endregion

    #region Page Load & Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 0)
            {
                string[] ReportID = Request.QueryString.GetValues("ID");
                FillReports(Convert.ToInt16(ReportID[0].ToString()));
            }
            else
            {
                if (Fuji.Common.Sessions.GetSession("ReportID") != null)
                {
                    string ReportID = (string)Fuji.Common.Sessions.GetSession("ReportID");
                    FillReports(Convert.ToInt16(ReportID.ToString()));
                }
                else
                    Fuji.Common.CommonFunctions.PageRedirect("TaskeroReports", "1");
            }
        }
    }
    protected void rptReportType_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string URL = Request.Url.ToString();
            System.Web.UI.HtmlControls.HtmlGenericControl LI = (HtmlGenericControl)e.Item.FindControl("LIReports");
            if (URL.IndexOf(DataBinder.Eval(e.Item.DataItem, "AspxPageName").ToString()) > 0)
            {
                LI.Attributes.Add("class", "active");
                Main.Attributes.Add("class", "");
            }
        }
    }
    #endregion

    #region Method & Function
    public void FillReports(short ReportID)
    {
        try
        {
            //==========  Declaring Objects ==========//
            ReportData objReportType = new ReportData();
            objReportType.ReportTypeID = ReportID;

            //==========  Get Data Table and Bind ==========//spCompanySelectByUserID
            DataTable DT_ReportType = new DataTable();
            DT_ReportType = objReportType.uspGetReportByReportTypeID();


            //========== Fill DDL ==========//
            if (DT_ReportType != null && DT_ReportType.Rows.Count > 0)
            {
                rptReportType.DataSource = DT_ReportType;
                rptReportType.DataBind();

                _STYLE = "";
            }
            else
            { rptReportType.DataSource = null; rptReportType.DataBind(); _STYLE = " style=\"display:none\""; }
        }
        catch (Exception Exc)
        {
            HandleException(Exc).ToString();
        }        
    }
    #endregion
}
