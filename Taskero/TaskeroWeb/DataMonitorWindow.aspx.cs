#region System
using System;
using System.Data;
using System.Web.UI.WebControls;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;
#endregion

namespace TaskeroWeb
{
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
                CommonFunctions.SetDataGridDefaultSettings(DG1);
                DG1.AllowPaging = false;
                DG1.Attributes.Add("align", "center");
                DG1.Style.Add("width", "100%");

                //========== Fill Grid ==========//
                if (IsPostBack == false)
                {
                    //========== Check The access ==========//
                    //if (Sessions.RoleDesc != Enums.RoleType.TaskeroAdmin)
                    //{ CommonFunctions.PageRedirect("AccessDenied", ""); }

                    //========== Clear Session ==========//
                   Sessions.SetSessions("DT_DataMonitor", null);

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
            if (Sessions.GetSession("DT_DataMonitor") == null)
            { Sessions.SetSessions("DT_DataMonitor", (new CPMeasurementData()).uspGetTOP100CPMeasurements(Sessions.UserID, Sessions.RoleID)); }
            DataTable DT = (DataTable)Sessions.GetSession("DT_DataMonitor");

            //========== Bind DataGrid ==========//
            DG1.DataSource = DT;
            DG1.DataBind();
        }
        #endregion
    }

}