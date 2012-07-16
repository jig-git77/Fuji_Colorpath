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

        public bool IsXmfUser
        {
            get
            {
                return (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser));
            }
        }

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

            lbtnDownloadAndSaveDLP.Visible = !IsXmfUser;

            lbtnDownloadAndSaveDLP.Text = IsNewDlp
                                              ? "<span><img id='imgAdd' src='Images/export.png'/>Create DLP<span>"
                                              : "<span><img id='imgAdd' src='Images/save_icon.png'/>Next<span>";
            if (!Page.IsPostBack)
            {
                if (SessionManager.Instance.CurrentOptimizeRun.Status == EOptimizeRunStatus.Closed && !SessionManager.Instance.CurrentOptimizeRun.HasProfile)
                {
                    //------------If this run is closed /certify run, then invisible download(Next) button, because there is no DLP available to download for this run-----//
                    lbtnDownloadAndSaveDLP.Visible = false;
                }
                BindMesurementChart();
                ((Label)updateProgress.FindControl("ucCreateDLP").FindControl("lblMessage")).Text = DLPMessage;
                BindAvgDelta();
               // FillAllTabsData();
            }
            BindMesurementChart();           
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
                FillAllTabsData();
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

        #region FillAllTabsChart

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
                FillInkPaperChart(DTDeltaEPrimaryPatches, DTPaperPatches); //---Fill out Ink Paper Tab with 3 charts depending on selected Measurement ID-----//
            }

            _presenterAnalyze.GetGrayChart();
             if (!IsErrorOccured)
             {
                 FillLabCharts(DTLABChart, DTGaumatChart1, DTGaumatChart2);
             }
        }

        #region Fill InkPaper Tab  
        private void FillInkPaperChart(List<AnalyzeChart> DTDeltaEPrimaryPatches, List<AnalyzeChart> Paper)
        {
            try
            {
                if (DTDeltaEPrimaryPatches != null)
                    FillDeltaEPrimaryPatchesChart(DTDeltaEPrimaryPatches);
                if (Paper != null)
                    FillPaperChart(Paper);
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }
       

        #region Fill DeltaE Primary Patches Chart
        private void FillDeltaEPrimaryPatchesChart(List<AnalyzeChart> DT)
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
             
                #region //========== Declarations ==========//
                // === Series0 to display Thresold value (ΔELimit) === //
                Series objSeries0 = (Series)ChartSummary_DeltaE.Series[0];
                objSeries0.XValueType = ChartValueType.Int32;
                objSeries0.YValueType = ChartValueType.String;         
                objSeries0.ChartType = SeriesChartType.Bar;
                         
                // === Series1 to display Actual value (ΔE) === //
                Series objSeries1 = (Series)ChartSummary_DeltaE.Series[1];
                objSeries1.XValueType = ChartValueType.Int32;
                objSeries1.YValueType = ChartValueType.String;            
                objSeries1.ChartType = SeriesChartType.Bar;
                objSeries1.CustomProperties = "DrawingStyle=Cylinder";                
            
                // === Make Chart area look like 2D even it is 3D === //
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.IsRightAngleAxes = true;
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.IsClustered = true;
                            
                #endregion

                #region //========== ChartArea Settings ==========//
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
                ChartSummary_DeltaE.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1.0;

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
                if (DT != null && DT.Count > 0)
                {
                    for (int i = 0; i < DT.Count; i++)
                    {
                        SBDeltaEPatches.Append("<tr style='height: 10px'>");
                       
                        if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Cyan).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:4px;'>Cyan</td>");
                            TooltipTitle = "Cyan Patch Metrics";
                        }
                        else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Magenta).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Magenta</td>");
                            TooltipTitle = "Magenta Patch Metrics";
                        }
                        else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Yellow).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:0px;'>Yellow</td>");
                            TooltipTitle = "Yellow Patch Metrics";
                        }
                        else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Black).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:0px;'>Black</td>");
                            TooltipTitle = "Black Patch Metrics";
                        }
                        else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Overprint_Red).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:2px;'>Red</td>");
                            TooltipTitle = "Red Patch Metrics";
                        }
                        else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Overprint_Green).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:2px;'>Green</td>");
                            TooltipTitle = "Green Patch Metrics";
                        }
                        else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Overprint_Blue).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:4px;'>Blue</td>");
                            TooltipTitle = "Blue Patch Metrics";
                        }
                        else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Paper).ToString())
                        {
                            SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:2px; font-family:Tahoma; font-size:13px; padding-bottom:8px;'>Paper</td>");
                            TooltipTitle = "Paper Patch Metrics";
                        }
                      
                        SBDeltaEPatches.Append("</tr>");
                        
                        BindDeltaEValueACTTHValue(objSeries0, objSeries1, DT, i, TooltipTitle);
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
        
        private void BindDeltaEValueACTTHValue(Series objSeries0, Series objSeries1, List<AnalyzeChart> DR, int Index, string TooltipTitle)
        {
            try
            {                
                Double Y1, Y2;
                Y1 = Y2 = 0;

                Y1 = (Convert.ToString(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].ΔELimit.ToString()))) == "" ? 0 : Convert.ToDouble(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].ΔELimit.ToString()))));
                Y2 = (Convert.ToString(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].ΔE.ToString()))) == "" ? 0 : Convert.ToDouble(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].ΔE.ToString()))));

                // === When Actual value is not available then no series will display and no marker will display
                if (Y2 == 0)
                    Y1 = Y2;
                               
                objSeries0.Points.AddXY((Double)0, Y1);               
                objSeries0.Points[Index]["PointWidth"] = "-1";
            
                Series SeriesActual = objSeries1;
                objSeries1.Points.AddXY((Double)0, Y2);

                #region Add Tooltip HTML
                StringBuilder toolTip = new StringBuilder();
                toolTip.Append("<table>");
                toolTip.Append("<tr><td><b>" + TooltipTitle + "</b></td></tr>");
                toolTip.Append("<tr><td><table>");
                toolTip.Append("<tr><td>Actual</td><td>");
                toolTip.Append("L*" + DR[Index].CielabL.ToString());
                toolTip.Append(" a*" + DR[Index].CielabA.ToString());
                toolTip.Append(" b*" + DR[Index].CielabB.ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>Target</td><td>");
                toolTip.Append("L*" + DR[Index].TargetL.ToString());
                toolTip.Append(" a*" + DR[Index].Targeta.ToString());
                toolTip.Append(" b*" + DR[Index].Targetb.ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>&Delta;E</td><td>");
                toolTip.Append(DR[Index].ΔE.ToString());             
                toolTip.Append("</td></tr>");      
                toolTip.Append("</td></tr>");
                toolTip.Append("</table></td></tr>");
                toolTip.Append("</table>");
                #endregion

                objSeries1.Points[Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                          
                SeriesActual.Points[Index].Color = System.Drawing.ColorTranslator.FromHtml(DR[Index].HtmlColor.ToString());
                SeriesActual.Points[Index]["PointWidth"] = "1.0";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Fill Paper Chart
        private void FillPaperChart(List<AnalyzeChart> DT)
        {
            try
            {
                if (DT != null && DT.Count > 0)
                {
                    #region Fill Paper 1 Chart

                    CHRT_Paper1.Series["Green"].Points.Clear();
                    CHRT_Paper1.Series["Paper"].Points.Clear();

                    CHRT_Paper1.Series["Green"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(0));
                    CHRT_Paper1.Series["Green"].ChartType = SeriesChartType.Point;
                    CHRT_Paper1.Series["Green"].Points[0].BorderWidth = 2;
                    CHRT_Paper1.Series["Green"].CustomProperties = "PolarDrawingStyle=Marker";

                    StringBuilder toolTip = new StringBuilder();
                    #region Add Tooltip HTML
                    toolTip.Append("<table>");
                    toolTip.Append("<tr><td><b>Paper Patch Metrics</b></td></tr>");
                    toolTip.Append("<tr><td><table>");
                    toolTip.Append("<tr><td>Actual</td><td>");
                    toolTip.Append("L*" + DT[0].CielabL.ToString());
                    toolTip.Append(" a*" + DT[0].CielabA.ToString());
                    toolTip.Append(" b*" + DT[0].CielabB.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>Target</td><td>");
                    toolTip.Append("L*" + DT[0].TargetL.ToString());
                    toolTip.Append(" a*" + DT[0].Targeta.ToString());
                    toolTip.Append(" b*" + DT[0].Targetb.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>&Delta;L*</td><td>");
                    toolTip.Append(DT[0].ΔL.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>&Delta;a*</td><td>");
                    toolTip.Append(DT[0].Δa.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>&Delta;b*</td><td>");
                    toolTip.Append(DT[0].Δb.ToString());
                    toolTip.Append("</td></tr>");

                    toolTip.Append("</table></td></tr>");
                    toolTip.Append("</table>");
                    #endregion

                    Double Paper_Δa = Convert.ToDouble(DT[0].Δa) == 0 ? 0.01 : Convert.ToDouble(DT[0].Δa); //-------If Δa = 0, then it makes problem in display chart, so just temporary assign 0.01 for negligible difference
                    CHRT_Paper1.Series["Paper"].Points.AddXY(Paper_Δa, Convert.ToDouble(DT[0].Δb));                   
                    CHRT_Paper1.Series["Paper"].ChartType = SeriesChartType.Point;
                    CHRT_Paper1.Series["Paper"].Points[0].BorderWidth = 4;
                    CHRT_Paper1.Series["Paper"].CustomProperties = "PolarDrawingStyle=Marker";
                    CHRT_Paper1.Series["Paper"].Points[0].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                    #endregion

                    #region Fill Paper 2 chart

                    CHRT_Paper2.Series["Series1"].Points.Clear();
                    CHRT_Paper2.Series["Series2"].Points.Clear();

                    #region Line Chart Series For Dispaly Line on 0 value
                    CHRT_Paper2.Series["Series2"].ChartType = SeriesChartType.Line;
                    CHRT_Paper2.Series["Series2"].XAxisType = AxisType.Secondary;
                    CHRT_Paper2.Series["Series2"].Points.AddXY(0, 0);
                    CHRT_Paper2.Series["Series2"].Points.AddXY(1, 0);
                    #endregion

                    CHRT_Paper2.Series["Series1"].Points.AddY(Convert.ToDouble(DT[0].ΔL));
                    CHRT_Paper2.Series["Series1"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    CHRT_Paper2.Series["Series1"].CustomProperties = "DrawingStyle=Cylinder";
                    CHRT_Paper2.Series["Series1"].Points[0].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                    #endregion
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region Fill Lab Tab
        private void FillLabCharts(List<AnalyzeChart_Gray> DTLABChart, List<AnalyzeChart_Gray> DTGaumatChart1, List<AnalyzeChart_Gray> DTGaumatChart2)
        {
            try
            {
                if (DTLABChart != null && DTLABChart.Count > 0)
                    FillLABChart(DTLABChart);
                if (DTGaumatChart1 != null && DTGaumatChart1.Count > 0 && DTGaumatChart2 != null && DTGaumatChart2.Count > 0)
                    FillGaumatChart(DTGaumatChart1, DTGaumatChart2);

            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }


        #region Fill Gaumat Chart
        private void FillGaumatChart(List<AnalyzeChart_Gray> DTTarget, List<AnalyzeChart_Gray> DTColors)
        {
            Double Papera = 0;
            Double Paperb = 0;

            #region Clear Chart_ABComparison's Points
            CHART_ABComparison.Series["Targets"].Points.Clear();
            CHART_ABComparison.Series["Cyan"].Points.Clear();
            CHART_ABComparison.Series["Magenta"].Points.Clear();
            CHART_ABComparison.Series["Yellow"].Points.Clear();
            CHART_ABComparison.Series["Red"].Points.Clear();
            CHART_ABComparison.Series["Green"].Points.Clear();
            CHART_ABComparison.Series["Blue"].Points.Clear();
            CHART_ABComparison.Series["Paper"].Points.Clear();
            #endregion

            if (DTTarget.Contains(DTTarget.Find(item => item.PatchType == CPPatchType.Paper.ToString())))
            {
                AnalyzeChart_Gray List_Paper = DTTarget.Find(item => item.PatchType == CPPatchType.Paper.ToString());

                Papera = Convert.ToDouble(List_Paper.CielabA);
                Paperb = Convert.ToDouble(List_Paper.CielabB);
            }

            #region Set the colors first
            BindLABComparision("Cyan", DTColors, Papera, Paperb, 5, false, false);
            BindLABComparision("Magenta", DTColors, Papera, Paperb, 5, false, false);
            BindLABComparision("Yellow", DTColors, Papera, Paperb, 5, false, false);

            BindLABComparision("Cyan", DTTarget, 0, 0, 9, true, false);
            BindLABComparision("Magenta", DTTarget, 0, 0, 9, true, false);
            BindLABComparision("Yellow", DTTarget, 0, 0, 9, true, false);
            BindLABComparision("Red", DTTarget, 0, 0, 9, true, false);
            BindLABComparision("Green", DTTarget, 0, 0, 9, true, false);
            BindLABComparision("Blue", DTTarget, 0, 0, 9, true, false);
            BindLABComparision("Paper", DTTarget, 0, 0, 9, true, false);

            BindLABComparision("Cyan", DTTarget, 0, 0, 6, false, true);
            BindLABComparision("Magenta", DTTarget, 0, 0, 6, false, true);
            BindLABComparision("Yellow", DTTarget, 0, 0, 6, false, true);
            BindLABComparision("Red", DTTarget, 0, 0, 6, false, true);
            BindLABComparision("Green", DTTarget, 0, 0, 6, false, true);
            BindLABComparision("Blue", DTTarget, 0, 0, 6, false, true);
            BindLABComparision("Paper", DTTarget, 0, 0, 6, false, true);
            #endregion
        }

        private void BindLABComparision(string ColorName, List<AnalyzeChart_Gray> DT, Double Papera, Double Paperb, int MarkerSize, bool isTarget, bool isActual)
        {
            StringBuilder toolTip = new StringBuilder();
            List<AnalyzeChart_Gray> ListFilter = new List<AnalyzeChart_Gray>();
            
            ListFilter = DT.FindAll(item => item.PatchType == ColorName);

            for (int i = 0; i < ListFilter.Count; i = i + 1)
            {
                #region Bind AB Comparison
                if (i == 0 && !isTarget && !isActual)
                {
                    CHART_ABComparison.Series[ColorName].ChartType = SeriesChartType.Line;
                    CHART_ABComparison.Series[ColorName].Points.AddXY(Papera, Paperb);
                }

                int pointCount = ((isTarget) ? CHART_ABComparison.Series["Targets"].Points.Count : CHART_ABComparison.Series[ColorName].Points.Count);


                if (isTarget || isActual)
                {
                    #region Add Tooltip HTML
                    toolTip.Append("<table>");
                    toolTip.Append("<tr><td><b>" + ColorName + " Patch Metrics</b></td></tr>");
                    toolTip.Append("<tr><td><table>");
                    toolTip.Append("<tr><td>Actual</td><td>");
                    toolTip.Append("L*" + ListFilter[i].CielabL.ToString());
                    toolTip.Append(" a*" + ListFilter[i].CielabA.ToString());
                    toolTip.Append(" b*" + ListFilter[i].CielabB.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>Target</td><td>");
                    toolTip.Append("L*" + ListFilter[i].TargetL.ToString());
                    toolTip.Append(" a*" + ListFilter[i].Targeta.ToString());
                    toolTip.Append(" b*" + ListFilter[i].Targetb.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>&Delta;E</td><td>");
                    toolTip.Append(ListFilter[i].ΔE.ToString());
                    toolTip.Append("</td></tr>");
                    //toolTip.Append("<tr><td>&Delta;H</td><td>");
                    //toolTip.Append(dv[i]["ΔH"].ToString());
                    //toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>&Delta;L*</td><td>");
                    toolTip.Append(ListFilter[i].ΔL.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>&Delta;a*</td><td>");
                    toolTip.Append(ListFilter[i].Δa.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>&Delta;b*</td><td>");
                    toolTip.Append(ListFilter[i].Δb.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("</table></td></tr>");
                    toolTip.Append("</table>");
                    #endregion
                }


                if (isTarget)
                {
                    CHART_ABComparison.Series["Targets"].ChartType = SeriesChartType.FastPoint;
                    CHART_ABComparison.Series["Targets"].Points.AddXY(Convert.ToDouble(ListFilter[i].Targeta), Convert.ToDouble(ListFilter[i].Targetb));
                    CHART_ABComparison.Series["Targets"].MarkerSize = MarkerSize;
                    CHART_ABComparison.Series["Targets"].MarkerColor = Color.Green;
                }
                else
                {
                    CHART_ABComparison.Series[ColorName].ChartType = SeriesChartType.Line;
                    CHART_ABComparison.Series[ColorName].Points.AddXY(Convert.ToDouble(ListFilter[i].CielabA), Convert.ToDouble(ListFilter[i].CielabB));
                }


                if (i == ListFilter.Count - 1 && (isTarget || isActual))
                {
                    if (isActual)
                    {
                        CHART_ABComparison.Series[ColorName].Points[pointCount].BorderWidth = 1;
                        CHART_ABComparison.Series[ColorName].Points[pointCount].MarkerSize = MarkerSize;
                        CHART_ABComparison.Series[ColorName].Points[pointCount].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                        CHART_ABComparison.Series[ColorName].Points[pointCount].Color = (Color)new ColorConverter().ConvertFromString(ColorName.Equals("paper", StringComparison.CurrentCultureIgnoreCase) ? "White" : ColorName);
                    }
                }
                else
                {
                    CHART_ABComparison.Series[ColorName].Points[pointCount].BorderWidth = 1;
                    CHART_ABComparison.Series[ColorName].Points[pointCount].MarkerSize = 0;
                    CHART_ABComparison.Series[ColorName].Points[pointCount].Color = (Color)new ColorConverter().ConvertFromString(ColorName.Equals("paper", StringComparison.CurrentCultureIgnoreCase) ? "White" : ColorName);
                }
                #endregion

                //Bind L Comparision
                #region Bind L Comparision

                #region //========== Declarations ==========//

                Series objSeries0 = (Series)CHART_LComparison.Series[0];
                objSeries0.CustomProperties = "PolarDrawingStyle=Marker";

                Series objSeries1 = (Series)CHART_LComparison.Series[1];
                objSeries1.CustomProperties = "PolarDrawingStyle=Marker";

                Series objSeries2 = (Series)CHART_LComparison.Series[2];
                objSeries2.CustomProperties = "PolarDrawingStyle=Marker";

                Series objSeries3 = (Series)CHART_LComparison.Series[3];
                objSeries3.CustomProperties = "PolarDrawingStyle=Marker";

                Series objSeries4 = (Series)CHART_LComparison.Series[4];
                objSeries4.CustomProperties = "PolarDrawingStyle=Marker";

                #endregion

                if (isTarget)
                {
                    int series = 0;
                    if (Convert.ToString(ListFilter[i].PatchType).ToLower() == "paper")
                    {
                        series = 2;
                    }

                    pointCount = CHART_LComparison.Series[series].Points.Count;

                    CHART_LComparison.Series[series].Points.AddXY(Convert.ToDouble(ListFilter[i].CielabA), Convert.ToDouble(0));
                    CHART_LComparison.Series[series].ChartType = SeriesChartType.Point;
                    CHART_LComparison.Series[series].Points[i].BorderWidth = 2;
                    CHART_LComparison.Series[series].Points[pointCount].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";


                    CHART_LComparison.Series[series + 1].Points.AddXY(Convert.ToDouble(ListFilter[i].CielabA), Convert.ToDouble(ListFilter[i].ΔL));   //---Use ΔL instead of L Deviation (Value is same, just name is different)
                    CHART_LComparison.Series[series + 1].ChartType = SeriesChartType.Point;
                    CHART_LComparison.Series[series + 1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    CHART_LComparison.Series[series + 1].Points[i].BorderWidth = 2;
                    CHART_LComparison.Series[series].Points[pointCount].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";


                    if (Convert.ToString(ListFilter[i].PatchType).ToLower() == "paper")
                    {
                        CHART_LComparison.Series[series].Points[pointCount].MarkerColor = Color.White;
                        CHART_LComparison.Series[series + 1].Points[pointCount].MarkerColor = Color.White;
                    }
                    else
                    {
                        CHART_LComparison.Series[series].Points[pointCount].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(ListFilter[i].PatchType));
                        CHART_LComparison.Series[series + 1].Points[pointCount].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(ListFilter[i].PatchType));
                    }
                    CHART_LComparison.Series[series].Points[pointCount].MarkerSize = 9;
                    CHART_LComparison.Series[series + 1].Points[pointCount].MarkerSize = 5;
                }
                #endregion
            }
        }

        #endregion

        #region Fill L* a* b* Chart
        private void FillLABChart(List<AnalyzeChart_Gray> DT)
        {
            try
            {
                Chart_L.Series["Series1"].Points.Clear();
                Chart_a.Series["Series1"].Points.Clear();
                Chart_b.Series["Series1"].Points.Clear();
                Chart_L.Series["Series2"].Points.Clear();
                Chart_a.Series["Series2"].Points.Clear();
                Chart_b.Series["Series2"].Points.Clear();

                if (DT != null && DT.Count > 0)
                {
                    for (int i = 0; i < DT.Count; i++)
                    {
                        AddPointLABChart(i, Chart_L.Series["Series1"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].CielabL.ToString(), Color.Black.Name.ToString(), DT, true);
                        AddPointLABChart(i, Chart_a.Series["Series1"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].CielabA.ToString(), Color.Black.Name.ToString(), DT, true);
                        AddPointLABChart(i, Chart_b.Series["Series1"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].CielabB.ToString(), Color.Black.Name.ToString(), DT, true);

                        if (DT[i].TargetL != null)
                        {
                            AddPointLABChart(i, Chart_L.Series["Series2"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].TargetL.ToString(), Color.Green.Name.ToString(), DT, false);
                        }
                        AddPointLABChart(i, Chart_a.Series["Series2"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].Targeta.ToString(), Color.Green.Name.ToString(), DT, false);
                        AddPointLABChart(i, Chart_b.Series["Series2"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].Targetb.ToString(), Color.Green.Name.ToString(), DT, false);
                    }
                }
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }

        private void AddPointLABChart(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName, List<AnalyzeChart_Gray> DR, bool isActual)
        {
            try
            {
                StringBuilder toolTip = new StringBuilder();
                if (isActual)
                {
                    #region Add Tooltip HTML
                    toolTip.Append("<table>");
                    toolTip.Append("<tr><td><b>" + _XValue + "% GB Patch Metrics</b></td></tr>");
                    toolTip.Append("<tr><td><table>");
                    toolTip.Append("<tr><td>Actual</td><td>");
                    toolTip.Append("L*" + DR[_Index].CielabL.ToString());
                    toolTip.Append(" a*" + DR[_Index].CielabA.ToString());
                    toolTip.Append(" b*" + DR[_Index].CielabB.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>Target</td><td>");
                    toolTip.Append("L*" + DR[_Index].TargetL.ToString());
                    toolTip.Append(" a*" + DR[_Index].Targeta.ToString());
                    toolTip.Append(" b*" + DR[_Index].Targetb.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>&Delta;E</td><td>");
                    toolTip.Append(DR[_Index].ΔE.ToString());
                    toolTip.Append("</td></tr>");
                    //toolTip.Append("<tr><td>&Delta;H</td><td>");
                    //toolTip.Append(DR[0].ΔE.ToString());
                    //toolTip.Append("</td></tr>");

                    toolTip.Append("<tr><td>&Delta;L*</td><td>");
                    toolTip.Append(DR[_Index].ΔL.ToString());
                    toolTip.Append("</td></tr>");

                    toolTip.Append("<tr><td>&Delta;a*</td><td>");
                    toolTip.Append(DR[_Index].Δa.ToString());
                    toolTip.Append("</td></tr>");

                    toolTip.Append("<tr><td>&Delta;b*</td><td>");
                    toolTip.Append(DR[_Index].Δb.ToString());
                    toolTip.Append("</td></tr>");

                    toolTip.Append("</table></td></tr>");
                    toolTip.Append("</table>");
                    #endregion
                }

                _objSeries.ChartType = SeriesChartType.Spline;
                _objSeries.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
                if (isActual)
                    _objSeries.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";

                _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
                _objSeries.Points[_Index].MarkerSize = 5;
                _objSeries.Points[_Index].BorderWidth = 1;
                _objSeries.Points[_Index].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #endregion

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

        public List<AnalyzeChart> DTDeltaEPrimaryPatches { get; set; }

        public List<AnalyzeChart> DTPaperPatches { get; set; }

        public List<Entity.AnalyzeChart_Gray> DTLABChart { get; set; }
        public List<Entity.AnalyzeChart_Gray> DTGaumatChart1 { get; set; }
        public List<Entity.AnalyzeChart_Gray> DTGaumatChart2 { get; set; }

        #endregion

    }
}
