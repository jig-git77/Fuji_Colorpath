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
using Fuji.Common;
#endregion

public partial class DataMonitorWindow : PageBase
{
    #region Page Load And Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Error Message ==========//
            LblError.Text = "";

            //========== Auto refresh ==========//
            //HtmlMeta metarefresh = new HtmlMeta();
            //metarefresh.Attributes.Add("http-equiv", "Refresh");
            //metarefresh.Attributes.Add("content", "60");
            //this.Header.Controls.Add(metarefresh);

            //========== Set Grid Settings ==========//
            Fuji.Common.CommonFunctions.SetDataGridDefaultSettings(DG1);
            DG1.AllowPaging = false;
            DG1.Attributes.Add("align", "center");
            DG1.Style.Add("width", "100%");

            //========== Fill Grid ==========//
            if (IsPostBack == false)
            {
                //========== Check The access ==========//
                //if (Fuji.Common.Sessions.RoleDesc != Fuji.Common.Enums.RoleType.TaskeroAdmin)
                //{ Fuji.Common.CommonFunctions.PageRedirect("AccessDenied", ""); }

                //========== Clear Session ==========//
                Fuji.Common.Sessions.SetSessions("DT_DataMonitor", null);

                //========== Order ==========//
                //ViewState["Order"] = "";

                //========== Fill Grid ==========//
                FillGrid();
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void DG1_OnItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //========== Get Label And Fill It ==========//
                Literal LTRL_Link = (Literal)e.Item.FindControl("LTRL_Link");
                LTRL_Link.Text = "<a OnClick=\"javascript:Pageopen('" + e.Item.Cells[0].Text + "');\" href=\"#\">Show</a>";
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    #endregion

    #region Fill Grid
    protected void FillGrid()
    {
        //========== Get the new data ==========//
        if (Fuji.Common.Sessions.GetSession("DT_DataMonitor") == null)
        { Fuji.Common.Sessions.SetSessions("DT_DataMonitor", (new Fuji.Business.CPMeasurementData()).uspGetTOP100CPMeasurements(Sessions.UserID, Sessions.RoleID)); }
        DataTable DT = (DataTable)Fuji.Common.Sessions.GetSession("DT_DataMonitor");

        //========== Bind DataGrid ==========//
        DG1.DataSource = DT;
        DG1.DataBind();
    }
    #endregion
}
