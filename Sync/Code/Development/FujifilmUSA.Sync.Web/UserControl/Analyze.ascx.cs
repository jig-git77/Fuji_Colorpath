using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Text;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Analyze : BaseControl, IViewAnalyze
    {
        #region Variable Declarations
        private PresenterAnalyze _presenterAnalyze;
        public DateTime EndDate;
        public string AddMeasurementDeleteMeasurementConfirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }

        public bool IsNewDlp
        {
            get { return SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1; }
        }

        public bool IsDlpCreated
        {
            get { return SessionManager.Instance.CurrentOptimizeRun.HasProfile; }
        }

        public string CreateDLPUpdateProgressClientID
        {
            get { return updateProgress.ClientID; }
        }
        public string UpdateProgressClientID
        {
            get { return ((UpdateProgress)Page.Master.FindControl("UpdateProgress")).ClientID; }
        }

        public string DLPMessage
        {
            get
            {
                string DLPName = SessionManager.Instance.CurrentProject.ProjectName + " DLP" + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + ".icc";
                return string.Format("Once optimization is complete, apply {0} to workflow and reprint the {1} chart.", DLPName, SessionManager.Instance.CurrentOptimizeRun.ReferenceChartType.Value);
            }
        }
        #endregion

        #region Page Method
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterAnalyze = new PresenterAnalyze(this);
        }       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
            }

            if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId)
            {
                header.InnerText = "New Print Condition - Analyze (No DLP)";
            }
            else
            {
                header.InnerText = string.Format("Analyze - {0}", SessionManager.Instance.CurrentOptimizeRun.LastDlpName);
            }

            lbtnDownloadAndSaveDLP.Text = IsNewDlp
                                              ? "<span><img id='imgAdd' src='Images/export.png'/>Create DLP<span>"
                                              : "<span><img id='imgAdd' src='Images/save_icon.png'/>Next<span>";
            if (!Page.IsPostBack)
            {
                if (SessionManager.Instance.CurrentOptimizeRun.Status == ERefChartDisplayType.Closed && !SessionManager.Instance.CurrentOptimizeRun.HasProfile)
                {
                    //------------If this run is closed /certify run, then invisible download(Next) button, because there is no DLP available to download for this run-----//
                    lbtnDownloadAndSaveDLP.Visible = false;
                }
                BindMesurementChart();
                ((Label)updateProgress.FindControl("ucCreateDLP").FindControl("lblMessage")).Text = DLPMessage;
                BindAvgDelta();
                FillAllTabsData();
            }
            BindMesurementChart();

           // FillInkPaperChart();
        }
        #endregion

        #region Methods

        private void BindMesurementChart()
        {
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            Measurements = new List<Entity.Measurement>();
            _presenterAnalyze.GetAnalyzeChartMeasurement();
            if (!IsErrorOccured)
            {
                var manager = new AnalyzeChartManager(chartAnalyze, Measurements)
                {
                    CurrentMeasurementID = Convert.ToInt32(hdMeasurement.Value)
                };
                manager.BindChart();

                lblMeasurementName.Text = manager.MeasurementName;

                hdMeasurement.Value = manager.CurrentMeasurementID.ToString();

                //----------Bind Patch Details Gridview depending on selected measurement from Analyze Chart on "Patch Details" Tab-------//
                GridPatchDetails.Rebind();
            }
        }

        private void BindAvgDelta()    //------Code for Avg Delta display on "Summary" Tab
        {
            try
            {
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                MeasurementsDelta = new List<Entity.MeasurementPatch_AvgDelta>();
                _presenterAnalyze.GetAvgDeltaMeasurement();
                grdMeasurement.DataSource = MeasurementsDelta;
                grdMeasurement.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        public void ConfigureExport()
        {
            GridPatchDetails.ExportSettings.ExportOnlyData = true;
            GridPatchDetails.ExportSettings.IgnorePaging = true;
            GridPatchDetails.ExportSettings.OpenInNewWindow = true;

            GridPatchDetails.MasterTableView.Columns.FindByUniqueName("HtmlColor").Visible = false;
        }

        public void FillAllTabsData()
        {
            MeasurementId = 0;
            if (hdMeasurement.Value != "")
            {
                MeasurementId = Convert.ToInt32(hdMeasurement.Value);  //----Pass selected measurement id from analyze chart above
            }
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
          
            _presenterAnalyze.GetInkPaperChart();
            if (!IsErrorOccured)
            {
                FillInkPaperChart(DTDeltaEPrimaryPatches, DTDeltaEPrimaryPatches, DTDeltaEPrimaryPatches);
            }
        }

        #region Fill InkPaper Tab  
        private void FillInkPaperChart(DataTable DTDeltaEPrimaryPatches, DataTable DTDensity, DataTable Paper)
        {
            try
            {
                if (DTDeltaEPrimaryPatches != null)
                    FillDeltaEPrimaryPatchesChart(DTDeltaEPrimaryPatches);
               // if (DTDensity != null)
                   // FillDensityChart(DTDensity);
               // if (Paper != null)
                   // FillPaperChart(Paper);

            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }
        #endregion

        #region Fill DeltaE Primary Patches Chart
        private void FillDeltaEPrimaryPatchesChart(DataTable DT)
        {
            try
            {
                ChartSummary_DeltaE.BorderColor = Color.White;
                ChartSummary_DeltaE.BorderWidth = 0;
                ChartSummary_DeltaE.BackColor = Color.White;

                //========== Clear Points and Show Chart ==========//
                ChartSummary_DeltaE.Visible = true;
                ChartSummary_DeltaE.Series[0].Points.Clear();
                ChartSummary_DeltaE.Series[1].Points.Clear();
                ChartSummary_DeltaE.Series[2].Points.Clear();

                #region //========== Declarations ==========//
                // === Series0 to display Thresold value (ΔELimit) === //
                Series objSeries0 = (Series)ChartSummary_DeltaE.Series[0];
                objSeries0.XValueType = ChartValueType.Int32;
                objSeries0.YValueType = ChartValueType.String;         
                objSeries0.ChartType = SeriesChartType.Bar;
                objSeries0.BackGradientStyle = GradientStyle.DiagonalRight;
                objSeries0.CustomProperties = "LabelStyle=Top,DrawingStyle=Cylinder";

                // === Series1 to display Actual value (ΔE) === //
                Series objSeries1 = (Series)ChartSummary_DeltaE.Series[1];
                objSeries1.XValueType = ChartValueType.Int32;
                objSeries1.YValueType = ChartValueType.String;            
                objSeries1.ChartType = SeriesChartType.Bar;
                objSeries1.CustomProperties = "DrawingStyle=Cylinder";

                // === Series2 to display red marker on Thresold value === //
                Series objSeries2 = (Series)ChartSummary_DeltaE.Series[2];
                objSeries2.XValueType = ChartValueType.Int32;
                objSeries2.YValueType = ChartValueType.String;              
                objSeries2.ChartType = SeriesChartType.Bar;
                objSeries2.CustomProperties = "DrawingStyle=Cylinder";

                // === Make Chart area look like 2D even it is 3D === //
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.IsRightAngleAxes = true;
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.IsClustered = true;
                //ChartSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.XAngle = 0;
                //ChartSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.YAngle = 0;
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].BackColor = Color.White;
              
                #endregion

                #region //========== ChartArea Settings ==========//
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;

                ChartSummary_DeltaE.ChartAreas["ChartArea1"].Position.Auto = true;
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].Position.X = float.Parse("0");
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].Position.Y = float.Parse("5");
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].Position.Width = float.Parse("99");
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].Position.Height = float.Parse("97");

                ChartSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.X = float.Parse("0");
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Y = float.Parse("2");
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Width = float.Parse("98");
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Height = float.Parse("97");

                ChartSummary_DeltaE.ChartAreas["ChartArea1"].ShadowOffset = 0;
                ChartSummary_DeltaE.BorderSkin.SkinStyle = BorderSkinStyle.None;
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].BorderWidth = 0;
                ChartSummary_DeltaE.BorderSkin.BorderWidth = 0;



                #endregion

                #region " === Fill Series === "
                // === Limit with red marker === //
                StringBuilder SBDeltaEPatches = new StringBuilder();
                string TooltipTitle = string.Empty;
                if (DT != null && DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        SBDeltaEPatches.Append("<tr style='height: 10px'>");
                        //if (Convert.ToDecimal(DT.Rows[i]["ΔE"]) < Convert.ToDecimal(DT.Rows[i]["ΔELimit"]))
                        //    SBDeltaEPatches.Append("<td valign='middle' align='center' style='padding-right: 5px' class='SyncBottomBorder'><img src=\"Images/Proofok.gif\" width='19px' height='10px' ></td>");
                        //else
                        //    SBDeltaEPatches.Append("<td valign='middle' align='center' style='padding-right: 5px' class='SyncBottomBorder'><img src=\"Images/ProofFailed.gif\" width='19px' height='10px' ></td>");

                        if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(CPPatchType.Cyan).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:4px;'>Cyan</td>");
                            TooltipTitle = "Cyan Patch Metrics";
                        }
                        else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(CPPatchType.Magenta).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Magenta</td>");
                            TooltipTitle = "Magenta Patch Metrics";
                        }
                        else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(CPPatchType.Yellow).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Yellow</td>");
                            TooltipTitle = "Yellow Patch Metrics";
                        }
                        else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(CPPatchType.Black).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Black</td>");
                            TooltipTitle = "Black Patch Metrics";
                        }
                        else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(CPPatchType.Overprint_Red).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Red</td>");
                            TooltipTitle = "Red Patch Metrics";
                        }
                        else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(CPPatchType.Overprint_Green).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Green</td>");
                            TooltipTitle = "Green Patch Metrics";
                        }
                        else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(CPPatchType.Overprint_Blue).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Blue</td>");
                            TooltipTitle = "Blue Patch Metrics";
                        }
                        else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(CPPatchType.Paper).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Paper</td>");
                            TooltipTitle = "Paper Patch Metrics";
                        }
                        else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(CPPatchType.Triplet_Solid_Gray).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>3-C</td>");
                            TooltipTitle = "3-C Patch Metrics";
                        }

                        SBDeltaEPatches.Append("</tr>");
                        
                        BindDeltaEValueACTTHValue(objSeries0, objSeries1, objSeries2, DT.Rows[i], i, TooltipTitle);
                    }
                    LitDeltaEPatches.Text = SBDeltaEPatches.ToString();
                }
                #endregion
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }
        
        private void BindDeltaEValueACTTHValue(Series objSeries0, Series objSeries1, Series objSeries2, DataRow DR, int Index, string TooltipTitle)
        {
            try
            {                
                Double Y1, Y2;
                Y1 = Y2 = 0;

                Y1 = (Convert.ToString(Common.Utilities.DecimalRound(Convert.ToDouble(DR["ΔELimit"].ToString()))) == "" ? 0 : Convert.ToDouble(Common.Utilities.DecimalRound(Convert.ToDouble(DR["ΔELimit"].ToString()))));
                Y2 = (Convert.ToString(Common.Utilities.DecimalRound(Convert.ToDouble(DR["ΔE"].ToString()))) == "" ? 0 : Convert.ToDouble(Common.Utilities.DecimalRound(Convert.ToDouble(DR["ΔE"].ToString()))));

                // === When Actual value is not available then no series will display and no marker will display
                if (Y2 == 0)
                    Y1 = Y2;
                
                Series SeriesActual = objSeries1;
               // objSeries0.Points.AddXY((Double)0, Y1);
               
               // objSeries0.Points[Index]["PointWidth"] = "1.0";
                objSeries1.Points.AddXY((Double)0, Y2);

                #region Add Tooltip HTML
                StringBuilder toolTip = new StringBuilder();
                toolTip.Append("<table>");
                toolTip.Append("<tr><td><b>" + TooltipTitle + "</b></td></tr>");
                toolTip.Append("<tr><td><table>");
                toolTip.Append("<tr><td>Actual</td><td>");
                toolTip.Append("L*" + DR["CIELAB_L*"].ToString());
                toolTip.Append(" a*" + DR["CIELAB_a*"].ToString());
                toolTip.Append(" b*" + DR["CIELAB_b*"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>Target</td><td>");
                toolTip.Append("L*" + DR["TargetL*"].ToString());
                toolTip.Append(" a*" + DR["Targeta*"].ToString());
                toolTip.Append(" b*" + DR["Targetb*"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>&Delta;E</td><td>");
                toolTip.Append(DR["ΔE"].ToString());
                //toolTip.Append("(Thr." + DR["ΔELimit"].ToString() + ")");
                toolTip.Append("</td></tr>");
                //toolTip.Append("<tr><td>&Delta;H</td><td>");
                //toolTip.Append(DR["ΔH"].ToString());

                toolTip.Append("</td></tr>");
                toolTip.Append("</table></td></tr>");
                toolTip.Append("</table>");
                #endregion

                objSeries1.Points[Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                              
               // objSeries2.Points.AddXY((Double)0, Y1);
               // objSeries0.Points[Index].BackGradientStyle = GradientStyle.DiagonalRight;

                SeriesActual.Points[Index].Color = System.Drawing.ColorTranslator.FromHtml(DR["HTMLColor"].ToString());
                SeriesActual.Points[Index]["PointWidth"] = "1.0";              

                if (Y1 == 0 && Y2 == 0)
                {
                   // objSeries0.Points[Index].MarkerImage = "";
                   // objSeries2.Points[Index].MarkerImage = "";
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #endregion

        #region Control Events

        protected void hdChartButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(hdCheckBoxChange.Value) == 1)
            {
                Measurement = new Entity.Measurement();
                Measurement.MeasurementId = Convert.ToInt32(hdMeasurement.Value);
                Measurement.IsUsed = hdChartCheckBox.Checked;

                _presenterAnalyze.UpdateMeasurementByAnalyzeChart();
            }
            if (!IsErrorOccured)
            {
                BindMesurementChart();
            }
        }

        protected void lbtnDownloadAndSaveDLP_Click(object sender, EventArgs e)
        {
            if (!IsNewDlp && IsDlpCreated)
            {
                Response.Redirect("Export.aspx");
            }
            else
            {
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                _presenterAnalyze.GetMeasurementsOnRunId();
                if (!IsErrorOccured)
                {
                    EndDate = System.DateTime.Now;
                    DestinationProfileDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName);
                    DlpDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName + " DLP" + EndDate.Month.ToString("00") + EndDate.Day.ToString("00") + EndDate.Hour.ToString("00") + EndDate.Minute.ToString("00"));

                    _presenterAnalyze.CreateDestinationAndDlp();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject.OptimizeRuns[0].EndDateTime = EndDate;
                        SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasProfile = true;
                        if (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser))
                        {
                            Response.Redirect(string.Format("COTransferSiteControl.aspx?projectid={0}&id={1}", SessionManager.Instance.CurrentProject.ProjectId, Guid.NewGuid()));
                        }
                        else
                        {
                            Response.Redirect("Export.aspx");
                        }
                    }
                }
            }
        }

        protected void GridPatchDetails_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                MeasurementId = 0;
                if (hdMeasurement.Value != "")
                {
                    MeasurementId = Convert.ToInt32(hdMeasurement.Value);  //----Pass selected measurement id from analyze chart above

                    OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                    PatchDetails = new List<Entity.MeasurementPatchDetails>();
                    _presenterAnalyze.GetPatchDetailsofMeasurement();
                    GridPatchDetails.DataSource = PatchDetails;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridPatchDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                Label lbl_HTML = (Label)item["HtmlColor"].FindControl("lbl_HTML");
                lbl_HTML.BackColor = System.Drawing.ColorTranslator.FromHtml(item.GetDataKeyValue("HtmlColor").ToString());
            }
        }

        protected void GridPatchDetails_ItemCommand(object sender, GridCommandEventArgs e)
        {        
            switch (e.CommandName)
            {
                case "Fillter":
                    foreach (GridColumn column in GridPatchDetails.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    GridPatchDetails.MasterTableView.FilterExpression = string.Empty;
                    GridPatchDetails.MasterTableView.Rebind();
                    break;

                case "Filter":          //------------Apply default CurrentFilterFunction for each column of PatchDetails RadGrid-----------------//
                    ((Pair)e.CommandArgument).First = "GreaterThanOrEqualTo";
                    break;
            }
        }

        protected void GridPatchDetails_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
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
                    sortExpr.SortOrder = GridPatchDetails.MasterTableView.AllowNaturalSort ? GridSortOrder.None : GridSortOrder.Descending;
                    e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
                    break;
                case GridSortOrder.Descending:
                    sortExpr.FieldName = e.SortExpression;
                    sortExpr.SortOrder = GridSortOrder.Ascending;

                    e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
                    break;
            }

            e.Canceled = true;
            GridPatchDetails.Rebind();
        }

        protected void btn_Excel_Click(object sender, EventArgs e)
        {
            ConfigureExport();
            GridPatchDetails.MasterTableView.ExportToExcel();
        }

        protected void btn_CSV_Click(object sender, EventArgs e)
        {
            ConfigureExport();
            GridPatchDetails.MasterTableView.ExportToCSV();
        }

        protected void ImgBtn_Report_Click(object sender, ImageClickEventArgs e)
        {

            byte[] bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/PDFReports/Sync DLP Report.pdf"));
            Utilities.DownloadFile(bytes, SessionManager.Instance.CurrentProject.ProjectName + " Report", ".pdf");
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

        public DataTable DTDeltaEPrimaryPatches { get; set; }

        #endregion

    }
}
