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
        private PresenterGlobal _presenterGlobal;
        private const string _recDLPText = "dlpText";
        private const string _recDLPMsgText = "dlpMsgText";
        private const string _recnewPrintCondition = "newPrintConditionText";
        private const string _recAnalyze = "analyze";
        private const string _recCreateDLP = "createDLP";
        private const string _recNext = "nextText";
        private const string _recCyan = "cyanText";
        private const string _recCyanPathMetrics = "cyanPathMetrics";
        private const string _recMagenta = "magentaText";
        private const string _recMagentaPathMetrics = "magentaPathMetrics";
        private const string _recYellow = "yellowText";
        private const string _recYellowPathMetrics = "yellowPathMetrics";
        private const string _recBlack = "blackText";
        private const string _recBlackPathMetrics = "blackPathMetrics";
        private const string _recRed = "redText";
        private const string _recRedPathMetrics = "redPathMetrics";
        private const string _recGreen = "greenText";
        private const string _recGreenPathMetrics = "greenPathMetrics";
        private const string _recBlue = "blueText";
        private const string _recBluePathMetrics = "bluePathMetrics";
        private const string _recPaper = "paperText";
        private const string _recPaperPathMetrics = "paperPathMetrics";
        private const string _recActual = "actualText";
        private const string _recL = "lText";
        private const string _recA = "aText";
        private const string _recB = "bText";
        private const string _recTarget = "targetText";
        private const string _recDeltaE = "deltaEText";
        private const string _recDeltaL = "deltaLText";
        private const string _recDeltaA = "deltaAText";
        private const string _recDeltaB = "deltaBText";
        private const string _recPatchMetrics = "patchMetricsText";
        private const string _recgbPatchMetrics = "gbPatchMetricsText";
        private const string _recValidationAnalyzeHeaderText = "validationAnalyzeHeaderText";
        public DateTime EndDate;
        public decimal DtELimit = 0;
        public string DLPName = "";

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
                string DLPName = SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLPText) + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + ".icc";
                return string.Format(GetResourceString(_recDLPMsgText), DLPName, SessionManager.Instance.CurrentOptimizeRun.ReferenceChartType.Value);
            }
        }

        public bool IsXMFUser
        {
            get
            {
                return (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser));
            }
        }

        public string CurrentDLPName
        {
            get
            {
                if (SessionManager.Instance.ProjectList != null && SessionManager.Instance.ProjectList.Count > 0)
                {
                    return SessionManager.Instance.ProjectList.Where(p => p.ProjectId == SessionManager.Instance.CurrentProject.ProjectId).Select(p => p.CurrentDlpName).FirstOrDefault();
                }
                return string.Empty;
            }
        }
        #endregion
        public Analyze()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region Page Method
        protected override void OnInit(EventArgs e)
        {
            _presenterAnalyze = new PresenterAnalyze(this);
            _presenterGlobal = new PresenterGlobal();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbtnMeasurement.Visible = IsXMFUser;

            //Here we are resetting the hidden controls used to identify the async call to fill chart and grid            
            ResetFilledControlsValues();

            if (!Page.IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;

                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    header.InnerText = GetResourceString(_recValidationAnalyzeHeaderText); //"Validation - Analyze";
                    //------Assign dEMethod value to radiolist, based on selected MetricTargetPresetID----------------//
                    rbtnListDeltaE.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.dEMethod.ToString().Substring(1);
                }
                else
                {
                    if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId)
                    {
                        header.InnerText = GetResourceString(_recnewPrintCondition);//"New Print Condition - Analyze (No DLP)";
                        DLPName = "No DLP";
                    }
                    else
                    {
                        header.InnerText = string.Format(GetResourceString(_recAnalyze), SessionManager.Instance.CurrentOptimizeRun.LastDlpName);//Analyze - {0}
                        DLPName = SessionManager.Instance.CurrentOptimizeRun.LastDlpName.Replace(SessionManager.Instance.CurrentProject.ProjectName + " ", ""); //---DLPName to pass into report
                        DLPName = DLPName.Remove(DLPName.Length - 4);
                    }
                }
                lbtnDownloadAndSaveDLP.Text = IsNewDlp
                                                  ? "<span><img id='imgAdd' src='Images/export.png'/>" + GetResourceString(_recCreateDLP) + "</span>"
                                                  : "<span><img id='imgAdd' src='Images/save_icon.png'/>" + GetResourceString(_recNext) + "</span>";//Create DLP - Next

                hdHasMeasurement.Value = SessionManager.Instance.CurrentOptimizeRun.HasMeasurements.ToString();
                if (SessionManager.Instance.CurrentOptimizeRun.Status == EOptimizeRunStatus.Certify)
                {
                    //------------If this run is closed /certify run, then invisible download(Next) button, because there is no DLP available to download for this run-----//
                    lbtnDownloadAndSaveDLP.Visible = false;
                }
                BindMesurementChart();
                ((Label)updateProgress.FindControl("ucCreateDLP").FindControl("lblMessage")).Text = DLPMessage;

                //We need to hide the button if we are verification mode
                //Else upper code will execute business logic
                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    //Verification mode
                    lbtnDownloadAndSaveDLP.Visible = false;
                    lbtnMeasurementVerification.Visible = true;
                    lbtnOptimize.Visible = true;
                    //Here we are assigning TEXT because when partial postback takes place the text of the linkbutton is removed as we adding attributes onClick
                    //So its a kind of bug fix.
                    lbtnOptimize.Text = "<span>Optimize</span>";
                    lbtnOptimize.Attributes.Add("onclick",
                                                     "javascript: return ShowClosePrintConditionWindow('" +
                                                     SessionManager.Instance.CurrentProject.ProjectId + "','" + Server.UrlEncode(SessionManager.Instance.CurrentProject.ProjectName) + "','" + Server.UrlEncode(this.CurrentDLPName) + "')");
                    trRBtnListDeltaE.Visible = false;
                }
                else
                {
                    //None
                    //Hide Verification button
                    lbtnMeasurementVerification.Visible = false;
                    lbtnOptimize.Visible = false;
                    trRBtnListDeltaE.Visible = true;
                }

                //Initially we are hiding RadGrid to prevent NeedDataSource event call
                HidePatchDetailsGrid();
            }
            else
            {
                BindMesurementChart();
                //FillAllTabsData();
            }
        }

        #endregion

        #region Methods

        private void BindMesurementChart()
        {
            From_ΔE = rbtnListDeltaE.SelectedValue;
            chartAnalyze.ChartAreas["ChartArea1"].AxisY.Title = "Δ" + From_ΔE;
            Measurements = new List<Entity.Measurement>();
            if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
            {
                //This will be called when Verification process is going on
                ProjectId = SessionManager.Instance.CurrentOptimizeRun.ProjectId;
                _presenterAnalyze.GetAnalyzeChartVMeasurement();
            }
            else
            {
                //In case of None (i.e. Optimization)
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                _presenterAnalyze.GetAnalyzeChartMeasurement();
            }
            if (!IsErrorOccured)
            {
                //AlignPaging(Measurements);
                var manager = new AnalyzeChartManager(chartAnalyze, Measurements)
                {
                    CurrentMeasurementID = Convert.ToInt32(hdMeasurement.Value)
                };
                manager.BindChart();

                lblMeasurementName.Text = manager.MeasurementName;

                hdMeasurement.Value = manager.CurrentMeasurementID.ToString();

                //----------Bind Patch Details Gridview depending on selected measurement from Analyze Chart on "Patch Details" Tab-------//
                BindAvgDelta();
            }
        }

        private void BindAvgDelta()    //------Code for Avg Delta display on "Summary" Tab
        {
            MeasurementId = 0;
            if (hdMeasurement.Value != "")
            {
                MeasurementId = Convert.ToInt32(hdMeasurement.Value);  //----Pass selected measurement id from analyze chart above
            }

            From_ΔE = rbtnListDeltaE.SelectedValue;

            if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
            {
                //This will be called when Verification process is going on
                _presenterAnalyze.GetAvgDeltaMeasurement_Validation();
                if (!IsErrorOccured)
                {
                    tbOptSummary.Visible = false;
                    dvValidationSummary.DataSource = MeasurementSummary;
                    dvValidationSummary.DataBind();

                    rgValidationSummaryMetrics.DataSource = MeasurementSummary[0].ValidationMetrics;
                    rgValidationSummaryMetrics.DataBind();
                }
            }
            else
            {
                //In case of None (i.e. Optimization)
                tbValidationSummary.Visible = false;
                MeasurementsDelta = new List<Entity.MeasurementPatch_AvgDelta>();
                _presenterAnalyze.GetAvgDeltaMeasurement_Optimization();
                if (!IsErrorOccured)
                {
                    grdMeasurement.DataSource = MeasurementsDelta;
                    grdMeasurement.DataBind();
                }
            }

            //-----------Assign Report iFrame to load report in "Report" tab----------------------//
            string path = "ReportSingleMeasurement.aspx?MeasurementID=" + MeasurementId + "&From_ΔE=" + rbtnListDeltaE.SelectedValue + "&DLPName=" + DLPName;
            iframeGeneratePDFReport.Attributes.Add("src", path);
            iframeGeneratePDFReport.Style.Add("display", "");
        }

        public void ConfigureExport()
        {
            //------------Patch Details GridView Export Settings----------------//
            rgPatchDetails.ExportSettings.ExportOnlyData = true;
            rgPatchDetails.ExportSettings.IgnorePaging = true;
            rgPatchDetails.ExportSettings.OpenInNewWindow = true;

            rgPatchDetails.MasterTableView.Columns.FindByUniqueName("HtmlColor").Visible = false;
        }

        #region FillAllTabs

        public void FillAllTabsData()
        {
            //This method is converted to smaller functions as we calling it asynchronously one by one to fill relevant tab data
            GetInkPaperChart();

            GetGrayChart();
        }

        private void GetInkPaperChart()
        {
            MeasurementId = 0;

            if (hdMeasurement.Value != "")
            {
                MeasurementId = Convert.ToInt32(hdMeasurement.Value);  //----Pass selected measurement id from analyze chart above
            }

            From_ΔE = rbtnListDeltaE.SelectedValue;
            lblEPryPatches.Text = From_ΔE;
            _presenterAnalyze.GetInkPaperChart();
            if (!IsErrorOccured)
            {
                FillInkPaperChart(DTDeltaEPrimaryPatches, DTPaperPatches); //---Fill out Ink Paper Tab with 3 charts depending on selected Measurement ID-----//
                hdnIsInkPaperFilled.Value = "1";
                tbInkPaper.Visible = true;
            }
        }

        private void GetGrayChart()
        {
            MeasurementId = 0;

            if (hdMeasurement.Value != "")
            {
                MeasurementId = Convert.ToInt32(hdMeasurement.Value);  //----Pass selected measurement id from analyze chart above
            }

            _presenterAnalyze.GetGrayChart();
            if (!IsErrorOccured)
            {
                FillGrayCharts(DTLABChart, DTGaumatChart1, DTGaumatChart2);
                //Set the values to indicate that is chart is displayed with data
                hdnIsGrayFilled.Value = "1";
                tbGray.Visible = true;
            }
        }

        #region Fill InkPaper Tab
        private void FillInkPaperChart(List<AnalyzeChart> DTDeltaEPrimaryPatches, List<AnalyzeChart> Paper)
        {
            if (DTDeltaEPrimaryPatches != null)
                FillDeltaEPrimaryPatchesChart(DTDeltaEPrimaryPatches);
            if (Paper != null)
                FillPaperChart(Paper);
        }

        #region Fill DeltaE Primary Patches Chart
        private void FillDeltaEPrimaryPatchesChart(List<AnalyzeChart> DT)
        {
            chartDeltaE.BorderColor = Color.White;
            chartDeltaE.BorderWidth = 0;
            chartDeltaE.BackColor = Color.White;

            //========== Clear Points and Show Chart ==========//
            chartDeltaE.Visible = true;
            chartDeltaE.Series[0].Points.Clear();
            chartDeltaE.Series[1].Points.Clear();

            #region //========== Declarations ==========//
            // === Series0 to display Thresold value (ΔELimit) === //
            Series objSeries0 = (Series)chartDeltaE.Series[0];
            objSeries0.XValueType = ChartValueType.Int32;
            objSeries0.YValueType = ChartValueType.String;
            objSeries0.CustomProperties = "DrawingStyle=Cylinder";

            // === Series1 to display Actual value (ΔE) === //
            Series objSeries1 = (Series)chartDeltaE.Series[1];
            objSeries1.XValueType = ChartValueType.Int32;
            objSeries1.YValueType = ChartValueType.String;
            objSeries1.CustomProperties = "DrawingStyle=Cylinder";

            // === Make Chart area look like 2D even it is 3D === //
            chartDeltaE.ChartAreas["ChartArea1"].Area3DStyle.IsRightAngleAxes = true;

            #endregion

            #region //========== ChartArea Settings ==========//
            chartDeltaE.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            chartDeltaE.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1.0;

            chartDeltaE.ChartAreas["ChartArea1"].Position.Auto = true;
            chartDeltaE.ChartAreas["ChartArea1"].Position.X = float.Parse("0");
            chartDeltaE.ChartAreas["ChartArea1"].Position.Y = float.Parse("5");
            chartDeltaE.ChartAreas["ChartArea1"].Position.Width = float.Parse("99");
            chartDeltaE.ChartAreas["ChartArea1"].Position.Height = float.Parse("97");

            chartDeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            chartDeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.X = float.Parse("0");
            chartDeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Y = float.Parse("2");
            chartDeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Width = float.Parse("98");
            chartDeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Height = float.Parse("97");

            chartDeltaE.ChartAreas["ChartArea1"].ShadowOffset = 0;
            chartDeltaE.BorderSkin.SkinStyle = BorderSkinStyle.None;
            chartDeltaE.ChartAreas["ChartArea1"].BorderWidth = 0;
            chartDeltaE.BorderSkin.BorderWidth = 0;

            #endregion

            #region " === Fill Series === "
            // === Limit with red marker === //
            StringBuilder SBDeltaEPatches = new StringBuilder();
            string TooltipTitle = string.Empty;

            if (DT != null && DT.Count > 0)
            {
                DtELimit = (from c in DT select c.ΔE).Max() + 2;  //-----------Get ΔElimit by getting maxΔE / maxΔE2000 plus add 2 more points
                DtELimit = (DtELimit > 4 ? DtELimit : DtELimit + 1);

                for (int i = 0; i < DT.Count; i++)
                {
                    SBDeltaEPatches.Append("<tr style='height: 10px'>");

                    if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Cyan).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:2px;'>" + GetResourceString(_recCyan) + "</td>");//Cyan
                        TooltipTitle = GetResourceString(_recCyanPathMetrics); //"Cyan Patch Metrics";
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Magenta).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:2px;'>" + GetResourceString(_recMagenta) + "</td>");//Magenta
                        TooltipTitle = GetResourceString(_recMagentaPathMetrics);//Magenta Patch Metrics
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Yellow).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:2px;'>" + GetResourceString(_recYellow) + "</td>");//Yellow
                        TooltipTitle = GetResourceString(_recYellowPathMetrics); //"Yellow Patch Metrics";
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Black).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:2px;'>" + GetResourceString(_recBlack) + "</td>");//Black
                        TooltipTitle = GetResourceString(_recBlackPathMetrics); //"Black Patch Metrics";
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Overprint_Red).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:2px;'>" + GetResourceString(_recRed) + "</td>");
                        TooltipTitle = GetResourceString(_recRedPathMetrics); //"Red Patch Metrics";
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Overprint_Green).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>" + GetResourceString(_recGreen) + "</td>");//Green
                        TooltipTitle = GetResourceString(_recGreenPathMetrics); //"Green Patch Metrics";
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Overprint_Blue).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>" + GetResourceString(_recBlue) + "</td>");//Blue
                        TooltipTitle = GetResourceString(_recBluePathMetrics); //"Blue Patch Metrics";
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Paper).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>" + GetResourceString(_recPaper) + "</td>");
                        TooltipTitle = GetResourceString(_recPaperPathMetrics); //"Paper Patch Metrics";
                    }

                    SBDeltaEPatches.Append("</tr>");

                    BindDeltaEValueACTTHValue(objSeries0, objSeries1, DT, i, TooltipTitle);
                }
                LitDeltaEPatches.Text = SBDeltaEPatches.ToString();
            }
            #endregion
        }

        private void BindDeltaEValueACTTHValue(Series objSeries0, Series objSeries1, List<AnalyzeChart> DR, int Index, string TooltipTitle)
        {
            Double Y1, Y2;
            Y1 = Y2 = 0;

            Y1 = (Convert.ToString(Common.Utilities.DecimalRound(Convert.ToDouble(DtELimit))) == "" ? 0 : Convert.ToDouble(Common.Utilities.DecimalRound(Convert.ToDouble(DtELimit))));
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
            toolTip.Append("<tr><td>" + GetResourceString(_recActual) + "</td><td>"); //Actual LAB
            toolTip.Append(GetResourceString(_recL) + DR[Index].CielabL.ToString());
            toolTip.Append(" " + GetResourceString(_recA) + DR[Index].CielabA.ToString());
            toolTip.Append(" " + GetResourceString(_recB) + DR[Index].CielabB.ToString());
            toolTip.Append("</td></tr>");
            toolTip.Append("<tr><td>" + GetResourceString(_recTarget) + "</td><td>"); //Target LAB
            toolTip.Append(GetResourceString(_recL) + DR[Index].TargetL.ToString());
            toolTip.Append(" " + GetResourceString(_recA) + DR[Index].Targeta.ToString());
            toolTip.Append(" " + GetResourceString(_recB) + DR[Index].Targetb.ToString());
            toolTip.Append("</td></tr>");
            toolTip.Append("<tr><td>" + GetResourceString(_recDeltaE) + "</td><td>"); //ΔE Value
            toolTip.Append(DR[Index].ΔE.ToString());
            toolTip.Append("</td></tr>");
            toolTip.Append("</table></td></tr>");
            toolTip.Append("</table>");
            #endregion

            objSeries1.Points[Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";

            SeriesActual.Points[Index].Color = System.Drawing.ColorTranslator.FromHtml(DR[Index].HtmlColor.ToString());
            SeriesActual.Points[Index]["PointWidth"] = "1.0";
        }
        #endregion

        #region Fill Paper Chart
        private void FillPaperChart(List<AnalyzeChart> DT)
        {
            if (DT != null && DT.Count > 0)
            {
                #region Fill Paper 1 Chart

                chartPaper1.Series["Green"].Points.Clear();
                chartPaper1.Series["Paper"].Points.Clear();

                chartPaper1.Series["Green"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(0));
                chartPaper1.Series["Green"].ChartType = SeriesChartType.Point;
                chartPaper1.Series["Green"].Points[0].BorderWidth = 2;
                chartPaper1.Series["Green"].CustomProperties = "PolarDrawingStyle=Marker";

                StringBuilder toolTip = new StringBuilder();

                #region Add Tooltip HTML
                toolTip.Append("<table>");
                toolTip.Append("<tr><td><b>" + GetResourceString(_recPaperPathMetrics) + "</b></td></tr>"); //Paper Patch Metrics
                toolTip.Append("<tr><td><table>");
                toolTip.Append("<tr><td>" + GetResourceString(_recActual) + "</td><td>"); //Actual LAB
                toolTip.Append(GetResourceString(_recL) + DT[0].CielabL.ToString());
                toolTip.Append(" " + GetResourceString(_recA) + DT[0].CielabA.ToString());
                toolTip.Append(" " + GetResourceString(_recB) + DT[0].CielabB.ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>" + GetResourceString(_recTarget) + "</td><td>"); //Target LAB
                toolTip.Append(GetResourceString(_recL) + DT[0].TargetL.ToString());
                toolTip.Append(" " + GetResourceString(_recA) + DT[0].Targeta.ToString());
                toolTip.Append(" " + GetResourceString(_recB) + DT[0].Targetb.ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>" + GetResourceString(_recDeltaL) + "</td><td>"); //ΔL*
                toolTip.Append(DT[0].ΔL.ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>" + GetResourceString(_recDeltaA) + "</td><td>"); //Δa*
                toolTip.Append(DT[0].Δa.ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>" + GetResourceString(_recDeltaB) + "</td><td>"); //Δb*
                toolTip.Append(DT[0].Δb.ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("</table></td></tr>");
                toolTip.Append("</table>");
                #endregion

                Double Paper_Δa = Convert.ToDouble(DT[0].Δa) == 0 ? 0.01 : Convert.ToDouble(DT[0].Δa); //-------If Δa = 0, then it makes problem in display chart, so just temporary assign 0.01 for negligible difference
                chartPaper1.Series["Paper"].Points.AddXY(Paper_Δa, Convert.ToDouble(DT[0].Δb));
                chartPaper1.Series["Paper"].ChartType = SeriesChartType.Point;
                chartPaper1.Series["Paper"].Points[0].BorderWidth = 4;
                chartPaper1.Series["Paper"].CustomProperties = "PolarDrawingStyle=Marker";
                chartPaper1.Series["Paper"].Points[0].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                #endregion

                #region Fill Paper 2 chart

                chartPaper2.Series["Series1"].Points.Clear();
                chartPaper2.Series["Series2"].Points.Clear();

                #region Line Chart Series For Dispaly Line on 0 value
                chartPaper2.Series["Series2"].ChartType = SeriesChartType.Line;
                chartPaper2.Series["Series2"].XAxisType = AxisType.Secondary;
                chartPaper2.Series["Series2"].Points.AddXY(0, 0);
                chartPaper2.Series["Series2"].Points.AddXY(1, 0);
                #endregion

                chartPaper2.Series["Series1"].Points.AddY(Convert.ToDouble(DT[0].ΔL));
                chartPaper2.Series["Series1"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                chartPaper2.Series["Series1"].CustomProperties = "DrawingStyle=Cylinder";
                chartPaper2.Series["Series1"].Points[0].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                #endregion
            }
        }
        #endregion

        #endregion

        #region Fill Gray Tab
        private void FillGrayCharts(List<AnalyzeChart_Gray> DTLABChart, List<AnalyzeChart_Gray> DTGaumatChart1, List<AnalyzeChart_Gray> DTGaumatChart2)
        {
            if (DTLABChart != null && DTLABChart.Count > 0)
                FillGrayLABCharts(DTLABChart);
            if (DTGaumatChart1 != null && DTGaumatChart1.Count > 0 && DTGaumatChart2 != null && DTGaumatChart2.Count > 0)
                FillGaumatChart(DTGaumatChart1, DTGaumatChart2);
        }
        
        #region Fill Gaumat Chart
        private void FillGaumatChart(List<AnalyzeChart_Gray> DTTarget, List<AnalyzeChart_Gray> DTColors)
        {
            Double Papera = 0;
            Double Paperb = 0;

            #region Clear chartABComparison's Points
            chartABComparison.Series["Targets"].Points.Clear();
            chartABComparison.Series["Cyan"].Points.Clear();
            chartABComparison.Series["Magenta"].Points.Clear();
            chartABComparison.Series["Yellow"].Points.Clear();
            chartABComparison.Series["Red"].Points.Clear();
            chartABComparison.Series["Green"].Points.Clear();
            chartABComparison.Series["Blue"].Points.Clear();
            chartABComparison.Series["Paper"].Points.Clear();
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
                    chartABComparison.Series[ColorName].ChartType = SeriesChartType.Line;
                    chartABComparison.Series[ColorName].Points.AddXY(Papera, Paperb);
                }

                int pointCount = ((isTarget) ? chartABComparison.Series["Targets"].Points.Count : chartABComparison.Series[ColorName].Points.Count);

                if (isTarget || isActual)
                {
                    #region Add Tooltip HTML
                    toolTip.Append("<table>");
                    toolTip.Append("<tr><td><b>" + ColorName + " " + GetResourceString(_recPatchMetrics) + "</b></td></tr>");
                    toolTip.Append("<tr><td><table>");
                    toolTip.Append("<tr><td>" + GetResourceString(_recActual) + "</td><td>"); //Actual
                    toolTip.Append(GetResourceString(_recL) + ListFilter[i].CielabL.ToString());
                    toolTip.Append(" " + GetResourceString(_recA) + ListFilter[i].CielabA.ToString());
                    toolTip.Append(" " + GetResourceString(_recB) + ListFilter[i].CielabB.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>" + GetResourceString(_recTarget) + "</td><td>"); //Target
                    toolTip.Append(GetResourceString(_recL) + ListFilter[i].TargetL.ToString());
                    toolTip.Append(" " + GetResourceString(_recA) + ListFilter[i].Targeta.ToString());
                    toolTip.Append(" " + GetResourceString(_recB) + ListFilter[i].Targetb.ToString());
                    toolTip.Append("</td></tr>");
                    //toolTip.Append("<tr><td>" + GetResourceString(_recDeltaE) + "</td><td>"); //ΔE
                    //toolTip.Append(ListFilter[i].ΔE.ToString());
                    //toolTip.Append("</td></tr>");

                    toolTip.Append("<tr><td>" + GetResourceString(_recDeltaL) + "</td><td>"); //ΔL*
                    toolTip.Append(ListFilter[i].ΔL.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>" + GetResourceString(_recDeltaA) + "</td><td>"); //ΔA*
                    toolTip.Append(ListFilter[i].Δa.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>" + GetResourceString(_recDeltaB) + "</td><td>"); //ΔB*
                    toolTip.Append(ListFilter[i].Δb.ToString());
                    toolTip.Append("</td></tr>");
                    toolTip.Append("</table></td></tr>");
                    toolTip.Append("</table>");
                    #endregion
                }


                if (isTarget)
                {
                    chartABComparison.Series["Targets"].ChartType = SeriesChartType.FastPoint;
                    chartABComparison.Series["Targets"].Points.AddXY(Convert.ToDouble(ListFilter[i].Targeta), Convert.ToDouble(ListFilter[i].Targetb));
                    chartABComparison.Series["Targets"].MarkerSize = MarkerSize;
                    chartABComparison.Series["Targets"].MarkerColor = Color.Green;
                }
                else
                {
                    chartABComparison.Series[ColorName].ChartType = SeriesChartType.Line;
                    chartABComparison.Series[ColorName].Points.AddXY(Convert.ToDouble(ListFilter[i].CielabA), Convert.ToDouble(ListFilter[i].CielabB));
                }


                if (i == ListFilter.Count - 1 && (isTarget || isActual))
                {
                    if (isActual)
                    {
                        chartABComparison.Series[ColorName].Points[pointCount].BorderWidth = 1;
                        chartABComparison.Series[ColorName].Points[pointCount].MarkerSize = MarkerSize;
                        chartABComparison.Series[ColorName].Points[pointCount].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                        chartABComparison.Series[ColorName].Points[pointCount].Color = (Color)new ColorConverter().ConvertFromString(ColorName.Equals("paper", StringComparison.CurrentCultureIgnoreCase) ? "White" : ColorName);
                    }
                }
                else
                {
                    chartABComparison.Series[ColorName].Points[pointCount].BorderWidth = 1;
                    chartABComparison.Series[ColorName].Points[pointCount].MarkerSize = 0;
                    chartABComparison.Series[ColorName].Points[pointCount].Color = (Color)new ColorConverter().ConvertFromString(ColorName.Equals("paper", StringComparison.CurrentCultureIgnoreCase) ? "White" : ColorName);
                }
                #endregion

                //Bind L Comparision
                #region Bind L Comparision

                #region //========== Declarations ==========//

                Series objSeries0 = (Series)chartLComparison.Series[0];
                objSeries0.CustomProperties = "PolarDrawingStyle=Marker";

                Series objSeries1 = (Series)chartLComparison.Series[1];
                objSeries1.CustomProperties = "PolarDrawingStyle=Marker";

                Series objSeries2 = (Series)chartLComparison.Series[2];
                objSeries2.CustomProperties = "PolarDrawingStyle=Marker";

                Series objSeries3 = (Series)chartLComparison.Series[3];
                objSeries3.CustomProperties = "PolarDrawingStyle=Marker";

                Series objSeries4 = (Series)chartLComparison.Series[4];
                objSeries4.CustomProperties = "PolarDrawingStyle=Marker";

                #endregion

                if (isTarget)
                {
                    int series = 0;
                    if (Convert.ToString(ListFilter[i].PatchType).ToLower() == "paper")
                    {
                        series = 2;
                    }

                    pointCount = chartLComparison.Series[series].Points.Count;

                    chartLComparison.Series[series].Points.AddXY(Convert.ToDouble(ListFilter[i].CielabA), Convert.ToDouble(0));
                    chartLComparison.Series[series].ChartType = SeriesChartType.Point;
                    chartLComparison.Series[series].Points[i].BorderWidth = 2;
                    chartLComparison.Series[series].Points[pointCount].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";


                    chartLComparison.Series[series + 1].Points.AddXY(Convert.ToDouble(ListFilter[i].CielabA), Convert.ToDouble(ListFilter[i].ΔL));   //---Use ΔL instead of L Deviation (Value is same, just name is different)
                    chartLComparison.Series[series + 1].ChartType = SeriesChartType.Point;
                    chartLComparison.Series[series + 1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                    chartLComparison.Series[series + 1].Points[i].BorderWidth = 2;
                    chartLComparison.Series[series].Points[pointCount].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";


                    if (Convert.ToString(ListFilter[i].PatchType).ToLower() == "paper")
                    {
                        chartLComparison.Series[series].Points[pointCount].MarkerColor = Color.White;
                        chartLComparison.Series[series + 1].Points[pointCount].MarkerColor = Color.White;
                    }
                    else
                    {
                        chartLComparison.Series[series].Points[pointCount].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(ListFilter[i].PatchType));
                        chartLComparison.Series[series + 1].Points[pointCount].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(ListFilter[i].PatchType));
                    }
                    chartLComparison.Series[series].Points[pointCount].MarkerSize = 9;
                    chartLComparison.Series[series + 1].Points[pointCount].MarkerSize = 5;
                }
                #endregion
            }
        }

        #endregion

        #region Fill Gray L* a* b* Chart
        private void FillGrayLABCharts(List<AnalyzeChart_Gray> DT)
        {
            chartL.Series["Series1"].Points.Clear();
            chartA.Series["Series1"].Points.Clear();
            chartB.Series["Series1"].Points.Clear();
            chartL.Series["Series2"].Points.Clear();
            chartA.Series["Series2"].Points.Clear();
            chartB.Series["Series2"].Points.Clear();

            if (DT != null && DT.Count > 0)
            {
                for (int i = 0; i < DT.Count; i++)
                {
                    AddPointLABChart(i, chartL.Series["Series1"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].CielabL.ToString(), Color.Black.Name.ToString(), DT, true);
                    AddPointLABChart(i, chartA.Series["Series1"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].CielabA.ToString(), Color.Black.Name.ToString(), DT, true);
                    AddPointLABChart(i, chartB.Series["Series1"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].CielabB.ToString(), Color.Black.Name.ToString(), DT, true);

                    if (DT[i].TargetL != null)
                    {
                        AddPointLABChart(i, chartL.Series["Series2"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].TargetL.ToString(), Color.Green.Name.ToString(), DT, false);
                    }
                    AddPointLABChart(i, chartA.Series["Series2"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].Targeta.ToString(), Color.Green.Name.ToString(), DT, false);
                    AddPointLABChart(i, chartB.Series["Series2"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].Targetb.ToString(), Color.Green.Name.ToString(), DT, false);
                }
            }
        }

        private void AddPointLABChart(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName, List<AnalyzeChart_Gray> DR, bool isActual)
        {
            StringBuilder toolTip = new StringBuilder();
            if (isActual)
            {
                #region Add Tooltip HTML
                toolTip.Append("<table>");
                toolTip.Append("<tr><td><b>" + _XValue + GetResourceString(_recgbPatchMetrics) + "</b></td></tr>");
                toolTip.Append("<tr><td><table>");
                toolTip.Append("<tr><td>" + GetResourceString(_recActual) + "</td><td>");
                toolTip.Append(GetResourceString(_recL) + DR[_Index].CielabL.ToString());
                toolTip.Append(" " + GetResourceString(_recA) + DR[_Index].CielabA.ToString());
                toolTip.Append(" " + GetResourceString(_recB) + DR[_Index].CielabB.ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>" + GetResourceString(_recTarget) + "</td><td>");
                toolTip.Append("L*" + DR[_Index].TargetL.ToString());
                toolTip.Append(" a*" + DR[_Index].Targeta.ToString());
                toolTip.Append(" b*" + DR[_Index].Targetb.ToString());
                toolTip.Append("</td></tr>");
                //toolTip.Append("<tr><td>" + GetResourceString(_recDeltaE) + "</td><td>");
                //toolTip.Append(DR[_Index].ΔE.ToString());
                //toolTip.Append("</td></tr>");

                toolTip.Append("<tr><td>" + GetResourceString(_recDeltaL) + "</td><td>");
                toolTip.Append(DR[_Index].ΔL.ToString());
                toolTip.Append("</td></tr>");

                toolTip.Append("<tr><td>" + GetResourceString(_recDeltaA) + "</td><td>");
                toolTip.Append(DR[_Index].Δa.ToString());
                toolTip.Append("</td></tr>");

                toolTip.Append("<tr><td>" + GetResourceString(_recDeltaB) + "</td><td>");
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
        #endregion

        #endregion

        #endregion

        private void ResetFilledControlsValues()
        {
            hdnIsInkPaperFilled.Value = "0";
            hdnIsGrayFilled.Value = "0";

            tbInkPaper.Visible = false;
            tbGray.Visible = false;
        }

        private void HidePatchDetailsGrid()
        {
            //We are hiding PatchDetailsGrid as we need to prevent NeedDataSource event calling to optimize performance
            //Initially we are hiding RadGrid to prevent NeedDataSource event call
            rgPatchDetails.Visible = false;
            hdnIsPatchDetailsFilled.Value = "0";
            tbPatchDetails.Visible = false;

        }
        #endregion

        #region Control Events
        protected void lbtnMeasurementVerification_Click(object sender, EventArgs e)
        {
            Response.Redirect("Measurement.aspx");
        }

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
                rgPatchDetails.Rebind();  //----Load Patch Details GridView in "Patch Details" tab
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
                    DlpDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLPText) + EndDate.Month.ToString("00") + EndDate.Day.ToString("00") + EndDate.Hour.ToString("00") + EndDate.Minute.ToString("00"));
                    _presenterAnalyze.CreateDestinationAndDlp();
                    if (!IsErrorOccured)
                    {
                        WebLogActivityManager.LogActivity(LogActivityType.CreateDlp, new object[] { SessionManager.Instance.CurrentProject.ProjectId },
                                                      Request.UserHostAddress);
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
                    else
                    {
                        WebLogActivityManager.LogActivity(LogActivityType.CreateDlpError, new object[] { SessionManager.Instance.CurrentProject.ProjectId },
                                                      Request.UserHostAddress);
                    }
                }
            }
        }

        protected void rgPatchDetails_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            MeasurementId = 0;
            if (hdMeasurement.Value != "")
            {
                MeasurementId = Convert.ToInt32(hdMeasurement.Value);  //----Pass selected measurement id from analyze chart above                   
                PatchDetails = new List<Entity.MeasurementPatchDetails>();
                _presenterAnalyze.GetPatchDetailsofMeasurement();
                if (!IsErrorOccured)
                {
                    rgPatchDetails.DataSource = PatchDetails;
                }
            }
        }

        protected void rgPatchDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                Label lblHTML = (Label)item["HtmlColor"].FindControl("lbl_HTML");
                lblHTML.BackColor = System.Drawing.ColorTranslator.FromHtml(item.GetDataKeyValue("HtmlColor").ToString());
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

                default:
                    //In all other item command we are showing RadGrid to fire NeedDataSource and Rebiding grid
                    rgPatchDetails.Visible = true;
                    rgPatchDetails.Rebind();
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

        protected void rbtnListDeltaE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //----------Bind Patch Details Gridview depending on selected measurement from Analyze Chart on "Patch Details" Tab-------//
            HidePatchDetailsGrid();
            BindMesurementChart();
        }

        protected void grdMeasurement_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            #region set Lable names
            From_ΔE = rbtnListDeltaE.SelectedValue;
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblAvgΔE = (Label)e.Item.FindControl("lblAvgΔE");
                lblAvgΔE.Text = From_ΔE + ": ";
                Label lblMaxΔE = (Label)e.Item.FindControl("lblMaxΔE");
                lblMaxΔE.Text = From_ΔE + ": ";
                Label lblPriΔE = (Label)e.Item.FindControl("lblPriΔE");
                lblPriΔE.Text = From_ΔE + ": ";
                Label lblGrayΔE = (Label)e.Item.FindControl("lblGrayΔE");
                lblGrayΔE.Text = From_ΔE + ": ";
                Label lblPercΔE10 = (Label)e.Item.FindControl("lblPercΔE10");
                lblPercΔE10.Text = From_ΔE + ": ";
                Label lblPercΔE90 = (Label)e.Item.FindControl("lblPercΔE90");
                lblPercΔE90.Text = From_ΔE + ": ";
                Label lblPercΔE95 = (Label)e.Item.FindControl("lblPercΔE95");
                lblPercΔE95.Text = From_ΔE + ": ";
            }
            #endregion
        }

        protected void rgValidationSummaryMetrics_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //---------Changed color of row, if that metric is failed---------------//
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (item["IsNotOK"].Text == "True")
                {
                    item.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnExcelExport_Click(object sender, EventArgs e)
        {
            ConfigureExport();
            rgPatchDetails.MasterTableView.ExportToExcel();
        }

        protected void btnCSVExport_Click(object sender, EventArgs e)
        {
            ConfigureExport();
            rgPatchDetails.MasterTableView.ExportToCSV();
        }

        protected void lbtnMeasurement_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Measure;
            Response.Redirect(string.Format("COMeasurement.aspx?id={0}", Guid.NewGuid()));
        }

        protected void btnFillPatchDetails_Click(object sender, EventArgs e)
        {
            rgPatchDetails.Visible = true;
            rgPatchDetails.Rebind();
            hdnIsPatchDetailsFilled.Value = "1";

            tbPatchDetails.Visible = true;
        }

        protected void btnFillInkPaper_Click(object sender, EventArgs e)
        {
            GetInkPaperChart();
        }

        protected void btnFillGray_Click(object sender, EventArgs e)
        {
            GetGrayChart();
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

        public string From_ΔE { get; set; }
        public bool IsVerification
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
            }
        }


        public List<Entity.Measurement> MeasurementSummary { get; set; }

        #endregion

        protected void DDL_PageSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DDL_GoToPage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void IBTN_FirstPage_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void IBTN_PreviousPage_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void IBTN_NextPage_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void IBTN_LastPage_Click(object sender, ImageClickEventArgs e)
        {

        }

        public void AlignPaging(List<Measurement> MeasurementList)
        {
            #region Paging
            #region Paging Variables
            //========== Settings for paging ==========//
            int _PageSize = Convert.ToInt32(DDL_PageSize.SelectedValue);
           // int _CurrentPageIndex = Convert.ToInt32(LTRL_CurrentPage.Text);
          //  int _PageCount = DT.Rows.Count / _PageSize; if ((DT.Rows.Count % _PageSize) > 0) { _PageCount = _PageCount + 1; }
            int _StartIndex = 1;
            int _EndIndex = 0;
            int _CPMOnPage = 0;
            #endregion

            #endregion
        }
    }
}
