using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddMetricTarget : BaseControl, IViewAddMetricTarget
    {
        #region Private Variables - Globalization
        private PresenterMetricTargetManagement _presenterMetricTargetManagement;
        #endregion


        public AddMetricTarget()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterMetricTargetManagement = new PresenterMetricTargetManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;

            //lblHeader.Text = GetResourceString(_recAddMetricTargetPreset);  //"Add MetricTargetPreset";
            //BindRegionDropDown();
            if (Request.QueryString["MetricTargetPresetId"] != null)
            {
                var MetricTargetPresetId = Request.QueryString["MetricTargetPresetId"];
                var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(MetricTargetPresetId, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                int _MetricTargetPresetId;
                if (Int32.TryParse(id, out _MetricTargetPresetId))
                {
                    hdfTargetPresetID.Value = _MetricTargetPresetId.ToString();
                    SetMetricTargetPreset(_MetricTargetPresetId);
                }
            }
        }
        #endregion

        #region Control Events
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveMetricTargetPreset();
        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MetricTargetManagement.aspx");
        }
        protected void grdMetricSum_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                LinkButton lnkRemove = (LinkButton)e.Row.FindControl("lnkRemove");
                if (lnkRemove != null)
                {
                    if (dt.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dt.Rows.Count - 1)
                        {
                            lnkRemove.Visible = false;
                        }
                    }
                    else
                    {
                        lnkRemove.Visible = false;
                    }
                }
            }
        }
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        protected void lnkRemove_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["CurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count - 1)
                    {
                        //Remove the Selected Row data and reset row number  
                        dt.Rows.Remove(dt.Rows[rowID]);
                        ResetRowID(dt);
                    }
                }

                //Store the current data in ViewState for future reference  
                ViewState["CurrentTable"] = dt;

                //Re bind the GridView for the updated data  
                grdMetricSum.DataSource = dt;
                grdMetricSum.DataBind();
            }

            //Set Previous Data on Postbacks  
            SetPreviousData();
        }


        protected void lbtnMetricSumSave_Click(object sender, EventArgs e)
        {
            MetricSumName = txtMetricSum.Text;
            _presenterMetricTargetManagement.SaveMetricSum();
            if (!IsErrorOccured)
            {
                Response.Redirect("~/MetricTargetManagement.aspx");
            }
        }
        #endregion

        #region Methods
        private void SetMetricTargetPreset(int MetricTargetPresetId)
        {
            MetricTargetPreset = new Entity.CPSyncV.MetricTargetPreset { MetricTargetPresetID = MetricTargetPresetId };
            _presenterMetricTargetManagement.GetMetricTargetPreset();
            if (!IsErrorOccured)
            {
                lblPresetName.Text = MetricTargetPreset.MetricTargetPresetName;
                if (MetricTargetPreset.MetricSumTolerances.Count > 0)
                {
                    BindGrid();
                }
                else
                {
                    SetInitialRow();
                }
                //grdMetricSum.DataSource = MetricTargetPreset.MetricSumTolerances;
                //grdMetricSum.DataBind();
            }

        }
        private void SaveMetricTargetPreset()
        {
            MetricTargetPreset = new Entity.CPSyncV.MetricTargetPreset();
            MetricTargetPreset.MetricTargetPresetID = Convert.ToInt32(hdfTargetPresetID.Value);
            MetricSumList = new List<Entity.CPSyncV.MetricSum>();
            //string xml = "<MetricTargetList>";
            //if (ViewState["CurrentTable"] != null)
            //{
            //    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //    foreach (var item in dtCurrentTable.Rows)
            //    {
            //        DataRow dr = (DataRow)item;

            //        if()
            //        MetricSumList.Add(new Entity.CPSyncV.MetricSum()
            //        {
            //            MetricTargetID = Convert.ToInt32(dr["MetricTargetId"]),
            //            MetricSumId = Convert.ToInt32(dr["MetricSumID"]),
            //            TargetValue = Convert.ToDecimal(dr["TargetValue"]),
            //            MetricTargetPresetID= MetricTargetPreset.MetricTargetPresetID
            //        });
            //    }
            //}
            if (grdMetricSum.Rows.Count > 0)
            {
                foreach (var item in grdMetricSum.Rows)
                {
                    var row = (GridViewRow)item;
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        //HiddenField hdfMetricSumId = (HiddenField)row.FindControl("hdfMetricSumId");
                        DropDownList ddlMetricSum = (DropDownList)row.FindControl("ddlMetricSum");
                        HiddenField hdfTargetMetricId = (HiddenField)row.FindControl("hdfTargetMetricId");
                        TextBox txtTargetValue = (TextBox)row.FindControl("txtTargetValue");
                        if (ddlMetricSum.SelectedItem.Value != "-1")
                        {
                            MetricSumList.Add(new Entity.CPSyncV.MetricSum()
                            {
                                MetricTargetID = Convert.ToInt32(hdfTargetMetricId.Value),
                                MetricSumId = Convert.ToInt32(ddlMetricSum.SelectedItem.Value),
                                TargetValue = Convert.ToDecimal(txtTargetValue.Text),
                                MetricTargetPresetID = MetricTargetPreset.MetricTargetPresetID
                            });
                        }
                    }
                }
            }
            _presenterMetricTargetManagement.UpdateMetricTarget();
            if (!IsErrorOccured)
            {
                Response.Redirect("~/MetricTargetManagement.aspx");
            }


        }
        private void BindGrid()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("MetricTargetId", typeof(string)));//for DropDownList selected item   
            dt.Columns.Add(new DataColumn("MetricSumID", typeof(string)));//for DropDownList selected item   
            dt.Columns.Add(new DataColumn("TargetValue", typeof(string)));//for DropDownList selected item   

            int index = 1;
            foreach (var item in MetricTargetPreset.MetricSumTolerances)
            {
                DataRow dr1 = dt.NewRow();
                dr1["RowNumber"] = index++;
                dr1["MetricTargetID"] = Convert.ToString(item.MetricTargetID);
                dr1["MetricSumID"] = Convert.ToString(item.MetricSumId);
                dr1["TargetValue"] = Convert.ToString(item.TargetValue);
                dt.Rows.Add(dr1);

            }
            DataRow dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["MetricTargetID"] = "0";
            dr["MetricSumID"] = "-1";
            dr["TargetValue"] = "0";
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdMetricSum.DataSource = dt;
            grdMetricSum.DataBind();
            SetPreviousData();
        }
        private void SetInitialRow()
        {

            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("MetricTargetId", typeof(string)));//for DropDownList selected item   
            dt.Columns.Add(new DataColumn("MetricSumID", typeof(string)));//for DropDownList selected item   
            dt.Columns.Add(new DataColumn("TargetValue", typeof(string)));//for DropDownList selected item   


            dr = dt.NewRow();
            dr["RowNumber"] = 1;            
            dr["MetricTargetID"] = "0";
            dr["MetricSumID"] = "-1";
            dr["TargetValue"] = "0";
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["CurrentTable"] = dt;

            //Bind the Gridview   
            grdMetricSum.DataSource = dt;
            grdMetricSum.DataBind();

            //After binding the gridview, we can then extract and fill the DropDownList with Data   
            DropDownList ddl1 = (DropDownList)grdMetricSum.Rows[0].Cells[1].FindControl("ddlMetricSum");
            FillDropDownList(ddl1);
        }
        private void AddNewRowToGrid()
        {

            if (ViewState["CurrentTable"] != null)
            {

                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                    drCurrentRow["MetricTargetID"] = "0";
                    drCurrentRow["TargetValue"] = "0";
                    //add new row to DataTable   
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //Store the current data to ViewState for future reference   

                    ViewState["CurrentTable"] = dtCurrentTable;


                    for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                    {

                        //extract the TextBox values   

                        DropDownList ddlMetricSum = (DropDownList)grdMetricSum.Rows[i].Cells[1].FindControl("ddlMetricSum");
                        TextBox txtTargetValue = (TextBox)grdMetricSum.Rows[i].Cells[1].FindControl("txtTargetValue");
                        //HiddenField hdfMetricSumId = (HiddenField)grdMetricSum.Rows[i].Cells[2].FindControl("hdfMetricSumId");
                        HiddenField hdfTargetMetricId = (HiddenField)grdMetricSum.Rows[i].Cells[1].FindControl("hdfTargetMetricId");

                        dtCurrentTable.Rows[i]["TargetValue"] = txtTargetValue.Text;
                        dtCurrentTable.Rows[i]["MetricSumID"] = ddlMetricSum.SelectedItem.Value;
                        dtCurrentTable.Rows[i]["MetricTargetId"] = hdfTargetMetricId.Value;

                    }

                    //Rebind the Grid with the current data to reflect changes   
                    grdMetricSum.DataSource = dtCurrentTable;
                    grdMetricSum.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");

            }
            //Set Previous Data on Postbacks   
            SetPreviousData();
        }
        private void FillDropDownList(DropDownList ddl1)
        {
            _presenterMetricTargetManagement.GetMetricSums();
            ddl1.DataTextField = "Value";
            ddl1.DataValueField = "Key";
            ddl1.DataSource = MetricSums;
            ddl1.DataBind();
        }

        private void SetPreviousData()
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtTargetValue = (TextBox)grdMetricSum.Rows[i].Cells[1].FindControl("txtTargetValue");
                        DropDownList ddlMetricSum = (DropDownList)grdMetricSum.Rows[rowIndex].Cells[1].FindControl("ddlMetricSum");
                        HiddenField hdfTargetMetricId = (HiddenField)grdMetricSum.Rows[i].Cells[1].FindControl("hdfTargetMetricId");

                        //Fill the DropDownList with Data   
                        FillDropDownList(ddlMetricSum);


                        if (i < dt.Rows.Count - 1)
                        {
                            //Assign the value from DataTable to the TextBox   
                            txtTargetValue.Text = dt.Rows[i]["TargetValue"].ToString();
                            hdfTargetMetricId.Value = dt.Rows[i]["MetricTargetId"].ToString();
                            //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                            ddlMetricSum.ClearSelection();
                            ddlMetricSum.Items.FindByValue(dt.Rows[i]["MetricSumID"].ToString()).Selected = true;
                        }

                        rowIndex++;
                    }
                }
            }
        }

        private void ResetRowID(DataTable dt)
        {
            int rowNumber = 1;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row[0] = rowNumber;
                    rowNumber++;
                }
            }
        }
        #endregion

        #region Implement IViewAddMetricTargetPreset
        public Entity.CPSyncV.MetricTargetPreset MetricTargetPreset { get; set; }       
        public List<Entity.CPSyncV.MetricSum> MetricSumList { get; set; }
        public Dictionary<int, string> MetricSums { get; set; }
        public string MetricSumName { get; set; }
        #endregion
    }
}