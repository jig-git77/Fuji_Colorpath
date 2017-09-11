using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Analyze_SpotColor : BaseControl, IViewAnalyze
    {
        #region Variable Declarations
        private PresenterAnalyze _presenterAnalyze;
        //private PresenterGlobal _presenterGlobal;
        #endregion

        #region Page Method
        protected override void OnInit(EventArgs e)
        {
            _presenterAnalyze = new PresenterAnalyze(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                if (SessionManager.Instance.CurrentProject.SpotTargetProject != null && SessionManager.Instance.CurrentProject.SpotTargetProject.PrintCondition != null)
                {
                    lblMedia.Text = SessionManager.Instance.CurrentProject.SpotTargetProject.PrintCondition.Media;
                    lblColorant.Text = SessionManager.Instance.CurrentProject.SpotTargetProject.PrintCondition.Colorant;
                    lblRipSettings.Text = SessionManager.Instance.CurrentProject.SpotTargetProject.PrintCondition.RipSettings;
                    lblSpotTarget.Text = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotTarget.SpotTargetName;
                }
            }
        }

        protected void rgPatchDetails_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            if (SessionManager.Instance.CurrentOptimizeRun != null)
            {
                MeasurementId = 0;
                OptimizeRunId = Convert.ToInt32(SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId);
                hdfOptimizationRunID.Value = OptimizeRunId.ToString(); ;
                //Now we are using DataTable to bind the grid               
                dtPatchDetails = new DataTable();
                _presenterAnalyze.GetSpotPatchDetailsofMeasurement();

                if (!IsErrorOccured)
                {
                    //Now we are using DataTable as DataSource                    
                    rgPatchDetails.DataSource = dtPatchDetails;
                    decimal totalDeltaE = 0;
                    decimal avgDeltaE = 0;

                    //count Avg, Max and Min ΔE
                    decimal minDtE = int.MaxValue;
                    decimal maxDtE = int.MinValue;

                    if (dtPatchDetails.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtPatchDetails.Rows)
                        {
                            decimal DtE = dr.Field<decimal>("ΔE");
                            totalDeltaE += DtE;
                            minDtE = Math.Min(minDtE, DtE);
                            maxDtE = Math.Max(maxDtE, DtE);
                        }
                        avgDeltaE = totalDeltaE / dtPatchDetails.Rows.Count;
                    }


                    lblMinDeltaE.Text = minDtE.ToString();
                    lblMaxDeltaE.Text = maxDtE.ToString();
                    lblAvgDeltaE.Text = String.Format("{0:0.###}", avgDeltaE);
                }
            }
        }

        protected void rgPatchDetails_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Fillter":
                    foreach (GridColumn column in rgPatchDetails.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgPatchDetails.MasterTableView.FilterExpression = string.Empty;
                    rgPatchDetails.MasterTableView.Rebind();
                    break;
            }
        }

        protected void rgPatchDetails_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            //Apply custom sorting -- make "Descending" order by default

            GridSortExpression sortExpr = new GridSortExpression();
            switch (e.OldSortOrder)
            {
                case GridSortOrder.None:
                    sortExpr.FieldName = e.SortExpression;
                    sortExpr.SortOrder = GridSortOrder.Descending;

                    e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
                    break;
                case GridSortOrder.Ascending:
                    sortExpr.FieldName = e.SortExpression;
                    sortExpr.SortOrder = rgPatchDetails.MasterTableView.AllowNaturalSort ? GridSortOrder.None : GridSortOrder.Descending;
                    e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
                    break;
                case GridSortOrder.Descending:
                    sortExpr.FieldName = e.SortExpression;
                    sortExpr.SortOrder = GridSortOrder.Ascending;

                    e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
                    break;
            }

            e.Canceled = true;
            rgPatchDetails.Rebind();
        }

        protected void rgPatchDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                Label lblHTML = (Label)item["HtmlColor"].FindControl("lbl_HTML");
                lblHTML.BackColor = System.Drawing.ColorTranslator.FromHtml(item.GetDataKeyValue("HTMLColor").ToString());

                //LinkButton lnklnkView = (LinkButton)item["ViewDetail"].FindControl("lnkView");

                var lnklnkView = e.Item.FindControl("lnkView") as ImageButton;

                string HTMLColor = Convert.ToString(DataBinder.Eval(item.DataItem, "HTMLColor"));
                if (!string.IsNullOrEmpty(HTMLColor))
                    HTMLColor = HTMLColor.Substring(1, HTMLColor.Length - 1);
                lnklnkView.Attributes.Add("onclick",
                                                 "return ShowSpotColorPatchDetailsWindow('" + HTMLColor + "','" +
                                                 DataBinder.Eval(item.DataItem, "CIELAB_L") + "','" + DataBinder.Eval(item.DataItem, "CIELAB_a") + "','" +
                                                 DataBinder.Eval(item.DataItem, "CIELAB_b") + "','" + DataBinder.Eval(item.DataItem, "TargetL") + "','" +
                                                 DataBinder.Eval(item.DataItem, "Targeta") + "','" + DataBinder.Eval(item.DataItem, "Targetb") + "')");

            }
        }

        #endregion

        #region IViewAnalyze Members

        public int ProjectId { get; set; }

        public int OptimizeRunId { get; set; }

        public List<int> MeasurementIds { get; set; }

        public List<Entity.Measurement> Measurements { get; set; }

        public Entity.Measurement Measurement { get; set; }

        public string DestinationProfileDescription { get; set; }

        public string DlpDescription { get; set; }

        public List<MeasurementPatch_AvgDelta> MeasurementsDelta { get; set; }

        public List<MeasurementPatchDetails> PatchDetails { get; set; }

        public int MeasurementId { get; set; }

        public List<AnalyzeChart> DTDeltaEPrimaryPatches { get; set; }

        public List<AnalyzeChart> DTPaperPatches { get; set; }

        public List<AnalyzeChart> DTDensity { get; set; }

        public List<Entity.AnalyzeChart_Gray> DTLABChart { get; set; }
        public List<Entity.AnalyzeChart_Gray> DTGaumatChart1 { get; set; }
        public List<Entity.AnalyzeChart_Gray> DTGaumatChart2 { get; set; }

        public string From_ΔE { get; set; }
        public bool IsVerification
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
            }
        }


        public List<Entity.Measurement> MeasurementSummary { get; set; }

        public List<Entity.AnalyzeChart_G7> GrayFChart { get; set; }
        public List<Entity.AnalyzeChart_G7> GrayFSummary { get; set; }
        public List<Entity.AnalyzeChart_G7> BlackLChart { get; set; }
        public List<Entity.AnalyzeChart_G7> BlackLSummary { get; set; }
        public List<Entity.AnalyzeChart_G7> GrayLChart { get; set; }
        public List<Entity.AnalyzeChart_G7> GrayLSummary { get; set; }
        public List<Entity.AnalyzeChart_G7> BlackNPDChart { get; set; }
        public List<Entity.AnalyzeChart_G7> GrayNPDChart { get; set; }

        public DataTable dtPatchDetails { get; set; }
        public DataTable dtFilteredPatchDetails { get; set; }

        public bool useDLP { get; set; }
        public byte[] DeviceLinkProfile { get; set; }
        public PCSDataTypes PCSDataTypes { get; set; }
        public DensityTypes DensityType { get; set; }
        public bool IsSync1DType
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D);
            }
        }

        public List<Entity.AnalyzeChart_DotGain> DTTVIChart { get; set; }
        public List<Entity.AnalyzeChart_DotGain> DTISOTVIChart { get; set; }
        public List<Entity.AnalyzeChart_DotGain> DTSpreadChartData { get; set; }
        public List<Entity.AnalyzeChart_DotGain> DTSpreadChartData_CMYK { get; set; }
        public List<Entity.AnalyzeChart_DotGain> DTSpreadSplineRangeChartData { get; set; }
        public DataTable DTMaxSpreadChartData { get; set; }

        public bool IsAverageOfMeasurements
        {
            get
            {
                //return chkAnalyzeAverageOfMeasurements.Checked;
                return false;
            }
            set
            {
                IsAverageOfMeasurements = false;//chkAnalyzeAverageOfMeasurements.Checked;
            }
        }

        public bool Is1DMeasurementSmoothing
        {
            get
            {
                //return chk1DMeasurementSmoothing.Checked;
                return false;
            }
            set
            {
                Is1DMeasurementSmoothing = false;// chk1DMeasurementSmoothing.Checked;
            }
        }

        public string FileName;
        #endregion

        protected void lbtnDownloadAndSaveNCP_Click(object sender, EventArgs e)
        {
            Response.Redirect("Export.aspx");
        }

        [WebMethod(EnableSession = true)]
        public static string GetPatchDetails()
        {
            string jsonPathDetails = "";
            //if (SessionManager.Instance.CurrentOptimizeRun != null)
            //{

            //    MeasurementId = 0;
            //    OptimizeRunId = Convert.ToInt32(SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId);
            //    //Now we are using DataTable to bind the grid               
            //    dtPatchDetails = new DataTable();
            //    _presenterAnalyze.GetSpotPatchDetailsofMeasurement();

            //    if (!IsErrorOccured)
            //    {
            //        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            //        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            //        Dictionary<string, object> childRow;
            //        foreach (DataRow row in dtPatchDetails.Rows)
            //        {
            //            childRow = new Dictionary<string, object>();
            //            foreach (DataColumn col in dtPatchDetails.Columns)
            //            {
            //                childRow.Add(col.ColumnName, row[col]);
            //            }
            //            parentRow.Add(childRow);
            //        }
            //        jsonPathDetails= jsSerializer.Serialize(parentRow);
            //    }
            //}
            return jsonPathDetails;
        }
    }
}