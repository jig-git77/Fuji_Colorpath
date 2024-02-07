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
    public partial class ProofandPlatesWindow : PageBase
    {
        #region Page Load and Evens
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Clear Error ==========//
                LblError.Text = "";

                ////========== Auto refresh ==========//
                //HtmlMeta metarefresh = new HtmlMeta();
                //metarefresh.Attributes.Add("http-equiv", "Refresh");
                //metarefresh.Attributes.Add("content", "60");
                //this.Header.Controls.Add(metarefresh);

                //========== Assign Settings to DataGrid ==========//            
                DG1.AllowPaging = false;
                DG1.Attributes.Add("align", "center");
                DG1.Style.Add("width", "100%");

                //========== Fill Data Grid ==========//
                if (IsPostBack == false)
                {
                    //=========== Clear DongleList From Session ==========//   
                    Sessions.SetSessions("ProofPlatesData", null);

                    //=========== Bind Proof & Plates ==========//   
                    FillForm();
                }
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void DG1_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == Telerik.WebControls.GridItemType.Item || e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
                {
                    //========== Get the Link Button ==========//
                    LinkButton LBTN_Reset = (LinkButton)e.Item.FindControl("LBTN_Reset");

                    //========== Show if Red/Organe is the status ==========//
                    if (e.Item.Cells[3].Text.Trim() == "3" || e.Item.Cells[3].Text.Trim() == "4") { LBTN_Reset.Visible = true; }
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void DG1_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditProof")
                {
                    //========== Declarations ==========//
                    CPDeviceStatusData objCPDeviceStatus = new CPDeviceStatusData();

                    //========== Update the record ==========//
                    objCPDeviceStatus.CPDeviceID = Convert.ToInt32(e.Item.Cells[2].Text);
                    objCPDeviceStatus.uspInsertCPDeviceStatusResetNew();

                    //========== Fill Form ==========//
                    FillForm();
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void DG1_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            try
            {
                //========== Check Session For the DongleData ===========//
                if (Sessions.GetSession("ProofPlatesData") != null)
                {
                    //========== Get the session Data From the Session ===========//
                    DataTable DT_ProofPlates = (DataTable)Sessions.GetSession("ProofPlatesData");
                    DG1.DataSource = DT_ProofPlates;
                }
            }
            catch (Exception exc)
            { LblError.Text = HandleException(exc); }
        }
        protected void BTN_Reset_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Check grid column in the grid ===========//
                foreach (Telerik.WebControls.GridColumn column in DG1.MasterTableView.Columns)
                {
                    //========== set all column filter value to empty ===========//
                    column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                    column.CurrentFilterValue = String.Empty;
                }
                //========== set all filter expression to empty ===========//
                DG1.MasterTableView.FilterExpression = String.Empty;
                DG1.MasterTableView.Rebind();
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        #endregion

        #region Methods and Functions
        private void FillForm()
        {
            try
            {
                //========== Declare  ==========//        
                CPDeviceData objCPDevice = new CPDeviceData();

                //==========  Get Data Table and Bind ==========//
                DataTable DT_ProofPlates = new DataTable();
                if (Sessions.GetSession("ProofPlatesData") == null)
                {
                    if (Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin)
                    {
                        DT_ProofPlates = objCPDevice.uspGetProofAndPlateAdmin();
                        Sessions.SetSessions("ProofPlatesData", DT_ProofPlates);
                    }
                    else
                    {
                        DT_ProofPlates = objCPDevice.uspGetProofAndPlateByTaskeroUserID(Sessions.UserID);
                        Sessions.SetSessions("ProofPlatesData", DT_ProofPlates);
                    }
                }

                //========== Get the session Data From the Session ===========//
                DT_ProofPlates = (DataTable)Sessions.GetSession("ProofPlatesData");

                //========== Assign the session data to Grid ===========//
                if (DT_ProofPlates != null && DT_ProofPlates.Rows.Count > 0)
                {
                    DG1.DataSource = DT_ProofPlates; DG1.DataBind(); DG1.Visible = true;
                    //********* Calling the Method for Applying the Page Right ************//
                    CheckRights();

                }

                else
                { DG1.DataSource = null; DG1.DataBind(); DG1.Visible = false; }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void CheckRights()
        {
            try
            {
                //********* Setting Right for Job Tab************//
                if (Sessions.Reset_Device_Status == false)
                { DG1.Columns[8].Visible = false; }
                else
                { DG1.Columns[8].Visible = true; }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }
}