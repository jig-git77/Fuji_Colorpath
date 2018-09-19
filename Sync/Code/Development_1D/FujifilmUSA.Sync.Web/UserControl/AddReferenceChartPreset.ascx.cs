using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddReferenceChartPreset : BaseControl, IViewAddMetricTarget
    {
        #region Private Variables - Globalization
        private PresenterMetricTargetManagement _presenterMetricTargetManagement;
        private PresenterMetricTargetManagement _presenterAddMetricTargetManagement;
        #endregion


        public AddReferenceChartPreset()
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
            //fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;
            if (ViewState["CheckBoxArray"] != null)
            {
                CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
            }
            else
            {
                CheckBoxArray = new ArrayList();
            }
            BindControls();

        }
        #endregion

        #region Methods   
        private void BindControls()
        {
            MetricTargetPresetList = new List<Entity.CPSyncV.MetricTargetPreset>();
            _presenterMetricTargetManagement.GetMetricTargetPresetList();
            if (!IsErrorOccured)
            {
                cmbTargetPresets.DataValueField = "MetricTargetPresetID";
                cmbTargetPresets.DataTextField = "MetricTargetPresetName";
                cmbTargetPresets.DataSource = MetricTargetPresetList;
                cmbTargetPresets.DataBind();
            }
        }
        private void BindGrid()
        {
            MetricTargetPreset = new Entity.CPSyncV.MetricTargetPreset();
            MetricTargetPreset.MetricTargetPresetID = Convert.ToInt32(cmbTargetPresets.SelectedValue.ToString());
            _presenterMetricTargetManagement.GetMetricTargetPresetReferenceCharts();
            if (!IsErrorOccured)
            {
                grdPresetRefChart.DataSource = dtPresetRefChart;
                grdPresetRefChart.DataBind();
            }
        }
        #endregion

        #region Control Events
        protected void cmbTargetPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            //MetricSumName = txtMetricSum.Text;
            //_presenterMetricTargetManagement.SaveMetricSum();
            //if (!IsErrorOccured)
            //{
            //    Response.Redirect("~/MetricTargetManagement.aspx");
            //}
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPresetRefChart.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {

            grdPresetRefChart.PageIndex = e.NewPageIndex;
            grdPresetRefChart.DataBind();
            if (ViewState["CheckBoxArray"] != null)
            {
                ArrayList CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
                string checkAllIndex = "chkAll-" + grdPresetRefChart.PageIndex;

                if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
                {
                    CheckBox chkAll = (CheckBox)grdPresetRefChart.HeaderRow.Cells[0].FindControl("chkAll");
                    chkAll.Checked = true;
                }
                for (int i = 0; i < grdPresetRefChart.Rows.Count; i++)
                {

                    if (grdPresetRefChart.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
                        {
                            CheckBox chk = (CheckBox)grdPresetRefChart.Rows[i].Cells[0].FindControl("CheckBox1");
                            chk.Checked = true;
                            grdPresetRefChart.Rows[i].Attributes.Add("style", "background-color:aqua");
                        }
                        else
                        {
                            int CheckBoxIndex = grdPresetRefChart.PageSize * (grdPresetRefChart.PageIndex) + (i + 1);
                            if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1)
                            {
                                CheckBox chk = (CheckBox)grdPresetRefChart.Rows[i].Cells[0].FindControl("CheckBox1");
                                chk.Checked = true;
                                grdPresetRefChart.Rows[i].Attributes.Add("style", "background-color:aqua");
                            }
                        }
                    }
                }
            }
        }
        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
            }
        }
        #endregion

        #region Implement IViewAddMetricTarget
        public Entity.CPSyncV.MetricTargetPreset MetricTargetPreset { get; set; }
        public List<Entity.CPSyncV.MetricTargetPreset> MetricTargetPresetList { get; set; }
        public List<Entity.CPSyncV.MetricSum> MetricSumList { get; set; }
        public Dictionary<int, string> MetricSums { get; set; }
        public string MetricSumName { get; set; }
        public DataTable dtPresetRefChart { get; set; }
        ArrayList CheckBoxArray { get; set; }
       
        #endregion
    }
}