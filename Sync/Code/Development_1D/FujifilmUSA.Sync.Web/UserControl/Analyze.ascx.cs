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
using System.IO;


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
        public int ValidationType = -1;

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

            if (!Page.IsPostBack)
            {
               

                ViewState["MetricSumID"] = "";

                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;

                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    header.InnerText = GetResourceString(_recValidationAnalyzeHeaderText); //"Validation - Analyze";
                    //------Assign dEMethod value to radiolist, based on selected MetricTargetPresetID----------------//
                    rbtnListDeltaE.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.dEMethod.ToString().Substring(1);
                }
                else
                {
                    SessionManager.Instance.CurrentDlpSubOperation = SessionManager.Instance.CurrentProject.LicenseType; //---Assign DLP or Sync1D project type
                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                    {
                        //------If project is Sync1D type, Assign 'E' to radiolist, bydefault (because Sync 1D has only dt_E type)----------------//
                        rbtnListDeltaE.SelectedValue = "E";
                        rbtnListDeltaE.Enabled = false;
                    }
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
                    Div_Validation_Header.Visible = true;
                    if (ValidationType == 1)  //G7 type
                    {
                        dvTab6.Visible = Tab_G7.Visible = true;
                    }
                    else
                    {
                        dvTab6.Visible = Tab_G7.Visible = false;
                    }
                }
                else   //-----DLP Optimization
                {
                    //None
                    //Hide Verification button
                    lbtnMeasurementVerification.Visible = false;
                    lbtnOptimize.Visible = false;
                    trRBtnListDeltaE.Visible = true;
                    Div_Opt_Header.Visible = true;
                    dvTab6.Visible = Tab_G7.Visible = false;
                    Div_MetricSumFilter.Visible = false;
                }
            }
            else
            {
                BindMesurementChart();
            }
        }

        #endregion

        #region Methods

        #region Bind Trendline Chart
        private void BindMesurementChart()
        {
            From_ΔE = rbtnListDeltaE.SelectedValue;
            chartAnalyze.ChartAreas["ChartArea1"].AxisY.Title = "Δ" + From_ΔE;
            Measurements = new List<Entity.Measurement>();
            bool IsValidation = false;

            if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
            {
                //This will be called when Verification process is going on
                ProjectId = SessionManager.Instance.CurrentOptimizeRun.ProjectId;
                _presenterAnalyze.GetAnalyzeChartVMeasurement();
                IsValidation = true;
            }
            else
            {              
                //In case of None (i.e. Optimization or Sync1D)
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                _presenterAnalyze.GetAnalyzeChartMeasurement();
            }
            if (!IsErrorOccured)
            {
                List<Entity.Measurement> MeasurementList = new List<Entity.Measurement>();
                MeasurementList = Measurements;

                if (IsValidation)  //This will be called when Verification process is going on
                {
                    MeasurementList = AlignPaging(Measurements);
                    //-----Update hdMeasurement value to last measurementID of list, if it is a part of validation and paging
                    int index = MeasurementList.FindIndex(item => item.MeasurementId == Convert.ToInt32(hdMeasurement.Value));
                    if (index < 0)
                    {
                        // element not exists, update to last record's measurementID
                        hdMeasurement.Value = MeasurementList.Last().MeasurementId.ToString();
                    }
                }

                var manager = new AnalyzeChartManager(chartAnalyze, MeasurementList, IsValidation)
                {
                    CurrentMeasurementID = Convert.ToInt32(hdMeasurement.Value)
                };
                manager.BindChart();

                lblMeasurementName.Text = manager.MeasurementName;

                hdMeasurement.Value = manager.CurrentMeasurementID.ToString();

                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)      //---Validation
                {
                    From_ΔE = rbtnListDeltaE.SelectedValue = MeasurementList.Find(item => item.MeasurementId == Convert.ToInt32(hdMeasurement.Value)).dEMethod.Substring(1);
                    chartAnalyze.ChartAreas["ChartArea1"].AxisY.Title = "Δ" + From_ΔE;
                }
                //----------Bind Patch Details Gridview depending on selected measurement from Analyze Chart on "Patch Details" Tab-------//
                BindAvgDelta();
                FillAllTabsData();
            }
        }
        #endregion

        #region Fill Summary tab

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
                    ValidationType = MeasurementSummary[0].ValidationType;

                    dvValidationSummary.DataSource = MeasurementSummary;
                    dvValidationSummary.DataBind();

                    rgValidationSummaryMetrics.DataSource = MeasurementSummary[0].ValidationMetrics;
                    rgValidationSummaryMetrics.DataBind();

                    PreparePrintLabelXML(MeasurementSummary[0]);

                    if (Page.IsPostBack == false)   //-------Fill MetricSumList when 1st time page load only
                    {
                        FillMetricSumList(MeasurementSummary[0].ValidationMetrics);
                    }
                }

                //-----------Assign Report iFrame to load report in "Report" tab----------------------//
                string path = "";
                if (ValidationType == 1)  //G7 type
                {
                    dvTab6.Visible = Tab_G7.Visible = true;
                    path = "ReportSingleMeasurement_G7_Vali.aspx?MeasurementID=" + MeasurementId + "&From_ΔE=" + rbtnListDeltaE.SelectedValue + "&DLPName=" + DLPName + "&Vali_Type=" + ValidationType;
                }
                else
                {
                    dvTab6.Visible = Tab_G7.Visible = false;
                    path = "ReportSingleMeasurement.aspx?MeasurementID=" + MeasurementId + "&From_ΔE=" + rbtnListDeltaE.SelectedValue + "&DLPName=" + DLPName + "&Vali_Type=" + ValidationType;
                }
                iframeGeneratePDFReport.Attributes.Add("src", path);
                iframeGeneratePDFReport.Style.Add("display", "");
            }
            else
            {
                //In case of None (i.e. Optimization or Sync1D project type)
                ibtnPrintPreview.Visible = false;
                ibtnPrintLabel.Visible = false;
                tbValidationSummary.Visible = false;
                dvTab6.Visible = Tab_G7.Visible = false;
                MeasurementsDelta = new List<Entity.MeasurementPatch_AvgDelta>();
                _presenterAnalyze.GetAvgDeltaMeasurement_Optimization();
                if (!IsErrorOccured)
                {
                    ValidationType = -1; //-----For DLP Optimization Report
                    grdMeasurement.DataSource = MeasurementsDelta;
                    grdMeasurement.DataBind();
                }

                //-----------Assign Report iFrame to load report in "Report" tab----------------------//
                string path = "ReportSingleMeasurement.aspx?MeasurementID=" + MeasurementId + "&From_ΔE=" + rbtnListDeltaE.SelectedValue + "&DLPName=" + DLPName + "&Vali_Type=" + ValidationType;
                iframeGeneratePDFReport.Attributes.Add("src", path);
                iframeGeneratePDFReport.Style.Add("display", "");
            }
        }

        #endregion

        #region FillAllTabs

        public void FillAllTabsData()
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
            }

            _presenterAnalyze.GetGrayChart();
            if (!IsErrorOccured)
            {
                FillGrayCharts(DTLABChart, DTGaumatChart1, DTGaumatChart2);
            }

            if (IsVerification == true && ValidationType == 1)  //-------------Only call G7 & TVI tab data if it is G7 Validation type
            {
                _presenterAnalyze.GetG7Chart();
                if (!IsErrorOccured)
                {
                    FillG7Chart(GrayFChart, GrayFSummary, BlackLChart, BlackLSummary, GrayLChart, GrayLSummary);
                }
            }
            if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D) //-----For Sync1D projects, call TVI tab data
            {
                _presenterAnalyze.GetTVIChart();
                if (!IsErrorOccured)
                {
                    FillTVIChart(DTTVIChart);
                }
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

        #region Fill Delta E Primary Patches Chart
        private void FillDeltaEPrimaryPatchesChart(List<AnalyzeChart> DT)
        {
            chartDeltaE.BorderColor = Color.White;
            chartDeltaE.BorderWidth = 0;
            chartDeltaE.BackColor = Color.White;

            //========== Clear Points and Show Chart ==========//
            chartDeltaE.Visible = true;
            chartDeltaE.Series[0].Points.Clear();
            chartDeltaE.Series[1].Points.Clear();
            chartDeltaE.Series[2].Points.Clear();

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

            // === Series2 to display red marker on Thresold value === //
            Series objSeries2 = (Series)chartDeltaE.Series[2];
            objSeries2.XValueType = ChartValueType.Int32;
            objSeries2.YValueType = ChartValueType.String;
            objSeries2.CustomProperties = "DrawingStyle=Cylinder";

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

                    BindDeltaEValueACTTHValue(objSeries0, objSeries1, objSeries2, DT, i, TooltipTitle);
                }
                LitDeltaEPatches.Text = SBDeltaEPatches.ToString();
            }
            #endregion
        }

        private void BindDeltaEValueACTTHValue(Series objSeries0, Series objSeries1, Series objSeries2, List<AnalyzeChart> DR, int Index, string TooltipTitle)
        {
            Double Y1, Y2, Y3;
            Y1 = Y2 = Y3 = 0;

            Y1 = (Convert.ToString(Common.Utilities.DecimalRound(Convert.ToDouble(DtELimit))) == "" ? 0 : Convert.ToDouble(Common.Utilities.DecimalRound(Convert.ToDouble(DtELimit))));
            Y2 = (Convert.ToString(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].ΔE.ToString()))) == "" ? 0 : Convert.ToDouble(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].ΔE.ToString()))));
            Y3 = (Convert.ToString(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].ΔELimit.ToString()))) == "" ? 0 : Convert.ToDouble(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].ΔELimit.ToString()))));

            // === When Actual value is not available then no series will display and no marker will display
            if (Y2 == 0)
                Y1 = Y2;

            objSeries0.Points.AddXY((Double)0, Y1);
            objSeries0.Points[Index]["PointWidth"] = "-1";

            Series SeriesActual = objSeries1;
            objSeries1.Points.AddXY((Double)0, Y2);

            // objSeries2.Points.AddXY((Double)0, Y3); //------Red marker image plot for Tolerance

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

        #region Fill G7 Tab
        private void FillG7Chart(List<AnalyzeChart_G7> GrayFChart, List<AnalyzeChart_G7> GrayFSummary, List<AnalyzeChart_G7> BlackLChart, List<AnalyzeChart_G7> BlackLSummary, List<AnalyzeChart_G7> GrayLChart, List<AnalyzeChart_G7> GrayLSummary)
        {
            if (GrayFChart != null && BlackLChart != null && GrayLChart != null)
                FillGrayFLChart(GrayFChart, BlackLChart, GrayLChart);
            if (GrayFSummary != null)
                FillGrayQuartileSummary(GrayFSummary, BlackLSummary, GrayLSummary);
            // if (BlackNPDChart != null && GrayNPDChart != null)
            // FillNPDChart(BlackNPDChart, GrayNPDChart);
        }

        #region Fill Gray Ch / F  and ΔL Charts
        private void FillGrayFLChart(List<AnalyzeChart_G7> DT, List<AnalyzeChart_G7> DTBlackL, List<AnalyzeChart_G7> DTGrayL)
        {
            #region Fill Gray Ch / F Chart
            Chart_GrayF.Series["Series1"].Points.Clear();
            Chart_GrayF.Series["Series1"]["TooltipOffset"] = "True";
            Chart_GrayF.Series["Series1Point"].Points.Clear();
            Chart_GrayF.Series["Series2"].Points.Clear();
            Chart_GrayF.Series["Series2Point"].Points.Clear();

            if (DT != null && DT.Count > 0)
            {
                for (int i = 0; i < DT.Count; i++)
                {
                    AddPointInChart(i, Chart_GrayF.Series["Series1"], Chart_GrayF.Series["Series1Point"], Convert.ToInt64(DT[i].Cmyk_Value).ToString(), DT[i].ΔValue.ToString(), Color.Black.Name.ToString(), true, "ΔCh*");
                    AddPointInChart(i, Chart_GrayF.Series["Series2"], Chart_GrayF.Series["Series2Point"], Convert.ToInt64(DT[i].Cmyk_Value).ToString(), DT[i].ΔLimit.ToString(), Color.Red.Name.ToString(), false, "ΔCh*");
                }
            }
            #endregion

            #region Fill Black ΔL Chart
            Chart_BlackL.Series["Series1"].Points.Clear();
            Chart_BlackL.Series["Series1"]["TooltipOffset"] = "True";
            Chart_BlackL.Series["Series1Point"].Points.Clear();
            Chart_BlackL.Series["Series2"].Points.Clear();
            Chart_BlackL.Series["Series2Point"].Points.Clear();

            if (DTBlackL != null && DTBlackL.Count > 0)
            {
                for (int i = 0; i < DTBlackL.Count; i++)
                {
                    AddPointInChart(i, Chart_BlackL.Series["Series1"], Chart_BlackL.Series["Series1Point"], Convert.ToInt64(DTBlackL[i].Cmyk_Value).ToString(), DTBlackL[i].ΔValue.ToString(), Color.Black.Name.ToString(), true, "ΔL*");
                    AddPointInChart(i, Chart_BlackL.Series["Series2"], Chart_BlackL.Series["Series2Point"], Convert.ToInt64(DTBlackL[i].Cmyk_Value).ToString(), DTBlackL[i].ΔLimit.ToString(), Color.Red.Name.ToString(), false, "ΔL*");
                }
            }
            #endregion

            #region Fill CMY / Gray ΔL Chart
            Chart_GrayL.Series["Series1"].Points.Clear();
            Chart_GrayL.Series["Series1"]["TooltipOffset"] = "True";
            Chart_GrayL.Series["Series1Point"].Points.Clear();
            Chart_GrayL.Series["Series2"].Points.Clear();
            Chart_GrayL.Series["Series2Point"].Points.Clear();

            if (DTGrayL != null && DTGrayL.Count > 0)
            {
                for (int i = 0; i < DTGrayL.Count; i++)
                {
                    AddPointInChart(i, Chart_GrayL.Series["Series1"], Chart_GrayL.Series["Series1Point"], Convert.ToInt64(DTGrayL[i].Cmyk_Value).ToString(), DTGrayL[i].ΔValue.ToString(), Color.Black.Name.ToString(), true, "ΔL*");
                    AddPointInChart(i, Chart_GrayL.Series["Series2"], Chart_GrayL.Series["Series2Point"], Convert.ToInt64(DTGrayL[i].Cmyk_Value).ToString(), DTGrayL[i].ΔLimit.ToString(), Color.Red.Name.ToString(), false, "ΔL*");
                }
            }
            #endregion
        }

        private void AddPointInChart(int _Index, Series _objSeries, Series _objSeriesPoint, string _XValue, string _YValue, string ColorName, bool showCircle, string From_Delta)
        {
            _objSeries.ChartType = SeriesChartType.Spline;
            _objSeries.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;
            _objSeries.Points[_Index].BorderWidth = 1;


            _objSeriesPoint.ChartType = SeriesChartType.Point;
            _objSeriesPoint.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeriesPoint.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeriesPoint.Points[_Index].MarkerStyle = showCircle ? MarkerStyle.Circle : MarkerStyle.None;
            _objSeriesPoint.Points[_Index].MarkerSize = showCircle ? 5 : 0;
            _objSeriesPoint.Points[_Index].BorderWidth = 1;
            _objSeriesPoint.Points[_Index].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(ColorName);


            #region Add Tooltip HTML
            StringBuilder toolTip = new StringBuilder();
            toolTip.Append("<table>");
            toolTip.Append("<tr><td><b>" + _XValue + "% Patch Metrics</b></td></tr>");
            toolTip.Append("<tr><td><table>");
            if (!showCircle) { toolTip.Append("<tr><td>" + From_Delta + " Threshold : </td><td>"); }
            else { toolTip.Append("<tr><td>" + From_Delta + " : </td><td>"); };
            toolTip.Append(_YValue);
            toolTip.Append("</td></tr>");
            toolTip.Append("</table></td></tr>");
            toolTip.Append("</table>");
            #endregion
            _objSeriesPoint.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";

        }


        #endregion

        #region Fill Gray Quartile Summary

        private void FillGrayQuartileSummary(List<AnalyzeChart_G7> DT_GrayF, List<AnalyzeChart_G7> DT_BlackL, List<AnalyzeChart_G7> DT_GrayL)
        {
            RG_ΔChSummary.DataSource = DT_GrayF;
            RG_ΔChSummary.DataBind();

            RG_ΔLBlackSummary.DataSource = DT_BlackL;
            RG_ΔLBlackSummary.DataBind();

            RG_ΔLGraySummary.DataSource = DT_GrayL;
            RG_ΔLGraySummary.DataBind();
        }

        #endregion

        #region Fill NPD Chart
        private void FillNPDChart(List<AnalyzeChart_G7> DTBlack, List<AnalyzeChart_G7> DTGray)
        {
            //CHRT_BlackNPD.Series["Series0"].Points.Clear();
            //CHRT_BlackNPD.Series["Series0Point"].Points.Clear();

            //// There is an issue with tooltip on spline chart, by default it ignores 
            ////http://support2.dundas.com/OnlineDocumentation/WebChart2003/CustomAttributes_TooltipOffset.html
            //CHRT_BlackNPD.Series["Series0"]["TooltipOffset"] = "True";

            //if (DTBlack != null && DTBlack.Count > 0)
            //{
            //    for (int i = 0; i < DTBlack.Count; i++)
            //    {
            //        AddNPDPointChart(i, CHRT_BlackNPD.Series["Series0"], Convert.ToInt64(DTBlack[i].Cmyk_Value).ToString(), DTBlack[i].ΔValue.ToString(), Color.Black.Name.ToString(), "Black Target NPD", "G7NPDC");
            //    }
            //}

            //CHRT_GrayNPD.Series["Series0"].Points.Clear();
            //// CHRT_GrayNPD.Series["Series0Point"].Points.Clear();           

            //CHRT_GrayNPD.Series["Series0"]["TooltipOffset"] = "True";

            //if (DTGray != null && DTGray.Count > 0)
            //{
            //    for (int i = 0; i < DTGray.Count; i++)
            //    {
            //        AddNPDPointChart(i, CHRT_GrayNPD.Series["Series0"], Convert.ToInt64(DTGray[i].Cmyk_Value).ToString(), DTGray[i].ΔValue.ToString(), Color.Black.Name.ToString(), "Gray Target NPD", "G7NPDC");
            //    }
            //}
        }

        //private void AddNPDPointChart(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName, string TitleName, string ValueName)
        //{
        //    _objSeries.ChartType = SeriesChartType.Spline;

        //    _objSeries.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
        //    _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
        //    _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;
        //    _objSeries.Points[_Index].MarkerSize = 1;  //changed 0 by chirag on 10/23/10 as we will drawing more points and make smooth curve 
        //    _objSeries.Points[_Index].BorderWidth = 1;
        //    _objSeries.Points[_Index].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(ColorName);

        //    #region Add Tooltip HTML
        //    StringBuilder toolTip = new StringBuilder();
        //    toolTip.Append("<table>");
        //    toolTip.Append("<tr><td><b> " + TitleName + " " + _XValue + "% Patch Metrics</b></td></tr>");
        //    toolTip.Append("<tr><td><table>");
        //    toolTip.Append("<tr><td>" + ValueName + " : </td><td>");
        //    toolTip.Append(Utilities.DecimalRound(Convert.ToDouble(_YValue)).ToString());
        //    toolTip.Append("</td></tr>");
        //    toolTip.Append("</table></td></tr>");
        //    toolTip.Append("</table>");
        //    #endregion
        //    _objSeries.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
        //}

        #endregion

        #endregion

        #region FIll TVI Tab

        private void FillTVIChart(List<AnalyzeChart_DotGain> DT)
        {
            try
            {
                CHRT_SummaryDotgain.Series["Series1"].Points.Clear();
                CHRT_SummaryDotgain.Series["Series2"].Points.Clear();
                CHRT_SummaryDotgain.Series["Series3"].Points.Clear();
                CHRT_SummaryDotgain.Series["Series4"].Points.Clear();
               
                if (DT != null && DT.Count > 0)
                {
                    int C = 0;
                    int M = 0;
                    int Y = 0;
                    int K = 0;


                    for (int i = 0; i < DT.Count; i++)
                    {
                        if (Convert.ToInt16(DT[i].CmykC) == 0 && Convert.ToInt16(DT[i].CmykM) == 0 && Convert.ToInt16(DT[i].CmykY) == 0 && Convert.ToInt16(DT[i].CmykK) == 0)
                        {
                            AddPointChartWithColor(C, CHRT_SummaryDotgain.Series["Series1"], CHRT_SummaryDotgain.Series["Series1Point"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].DotGain.ToString(), Color.Cyan.Name.ToString());
                            AddPointChartWithColor(M, CHRT_SummaryDotgain.Series["Series2"], CHRT_SummaryDotgain.Series["Series2Point"], Convert.ToInt64(DT[i].CmykM).ToString(), DT[i].DotGain.ToString(), Color.Magenta.Name.ToString());
                            AddPointChartWithColor(Y, CHRT_SummaryDotgain.Series["Series3"], CHRT_SummaryDotgain.Series["Series3Point"], Convert.ToInt64(DT[i].CmykY).ToString(), DT[i].DotGain.ToString(), Color.Yellow.Name.ToString());
                            AddPointChartWithColor(K, CHRT_SummaryDotgain.Series["Series4"], CHRT_SummaryDotgain.Series["Series4Point"], Convert.ToInt64(DT[i].CmykK).ToString(), DT[i].DotGain.ToString(), Color.Black.Name.ToString());
                            C++; M++; Y++; K++;
                        }
                        if (Convert.ToInt16(DT[i].CmykC) != 0)
                        {
                            AddPointChartWithColor(C, CHRT_SummaryDotgain.Series["Series1"], CHRT_SummaryDotgain.Series["Series1Point"], Convert.ToInt64(DT[i].CmykC).ToString(), DT[i].DotGain.ToString(), Color.Cyan.Name.ToString());
                            C++;
                        }
                        if (Convert.ToInt16(DT[i].CmykM) != 0)
                        {
                            AddPointChartWithColor(M, CHRT_SummaryDotgain.Series["Series2"], CHRT_SummaryDotgain.Series["Series2Point"], Convert.ToInt64(DT[i].CmykM).ToString(), DT[i].DotGain.ToString(), Color.Magenta.Name.ToString());
                            M++;
                        }
                        if (Convert.ToInt16(DT[i].CmykY) != 0)
                        {
                            AddPointChartWithColor(Y, CHRT_SummaryDotgain.Series["Series3"], CHRT_SummaryDotgain.Series["Series3Point"], Convert.ToInt64(DT[i].CmykY).ToString(), DT[i].DotGain.ToString(), Color.Yellow.Name.ToString());
                            Y++;
                        }
                        if (Convert.ToInt16(DT[i].CmykK) != 0)
                        {
                            AddPointChartWithColor(K, CHRT_SummaryDotgain.Series["Series4"], CHRT_SummaryDotgain.Series["Series4Point"], Convert.ToInt64(DT[i].CmykK).ToString(), DT[i].DotGain.ToString(), Color.Black.Name.ToString());
                            K++;
                        }
                    }
                }
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }
        //Add Point with color
        private void AddPointChartWithColor(int _Index, Series _objSeries, Series _objSeriesPoint, string _XValue, string _YValue, string ColorName)
        {
            try
            {
                _objSeries.ChartType = SeriesChartType.Spline;
                _objSeries.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;
                _objSeries.Points[_Index].BorderWidth = 1;

                _objSeriesPoint.ChartType = SeriesChartType.Point;
                _objSeriesPoint.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
                _objSeriesPoint.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
                _objSeriesPoint.Points[_Index].MarkerStyle = MarkerStyle.Circle;
                _objSeriesPoint.Points[_Index].MarkerSize = 5;
                _objSeriesPoint.Points[_Index].BorderWidth = 1;
                _objSeriesPoint.Points[_Index].MarkerBorderColor = Color.Black;

                if (Utilities.DecimalRound(Convert.ToDouble(_YValue)) > 0)
                {
                    #region Add Tooltip HTML
                    StringBuilder toolTip = new StringBuilder();
                    toolTip.Append("<table>");
                    toolTip.Append("<tr><td><b>" + ColorName + " " + _XValue + "% Patch Metrics</b></td></tr>");
                    toolTip.Append("<tr><td><table>");
                    toolTip.Append("<tr><td>Dot Gain : </td><td>");
                    toolTip.Append(_YValue);
                    toolTip.Append("</td></tr>");
                    toolTip.Append("</table></td></tr>");
                    toolTip.Append("</table>");
                    #endregion

                    //_objSeries.Points[_Index].ToolTip = "(" + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)).ToString() + "," + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)).ToString() + ")";
                    _objSeriesPoint.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                }
                //_objSeries.Points[_Index].ToolTip = "(" + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)).ToString() + "," + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)).ToString() + ")";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #endregion

        #region Methods for Patch Details tab
        private DataTable FilteredPatchDetailsTable(string MetricSumID, DataTable dtPatchDetails)
        {
            DataTable DT = new DataTable();

            if (MetricSumID == "3")     //----For 'Paper ΔE Avg'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 8)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "4")    //----For 'CMYK ΔE Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 1 || r.Field<byte>("PatchTypeID") == 2 || r.Field<byte>("PatchTypeID") == 3 || r.Field<byte>("PatchTypeID") == 4)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "5")    //----For 'CMYK ΔH Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 1 || r.Field<byte>("PatchTypeID") == 2 || r.Field<byte>("PatchTypeID") == 3 || r.Field<byte>("PatchTypeID") == 4)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "6")    //----For 'FOGRA Gray ΔH Avg'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 52 || r.Field<byte>("PatchTypeID") == 53 || r.Field<byte>("PatchTypeID") == 54 || r.Field<byte>("PatchTypeID") == 55 || r.Field<byte>("PatchTypeID") == 56 || r.Field<byte>("PatchTypeID") == 57)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "7")    //----For 'CMYKRGB ΔH Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 1 || r.Field<byte>("PatchTypeID") == 2 || r.Field<byte>("PatchTypeID") == 3 || r.Field<byte>("PatchTypeID") == 4 || r.Field<byte>("PatchTypeID") == 5 || r.Field<byte>("PatchTypeID") == 6 || r.Field<byte>("PatchTypeID") == 7)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "8" || MetricSumID == "9")    //----For 'G7 Gray ΔCh Avg' or ----For 'G7 Gray ΔCh Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 9 || r.Field<byte>("PatchTypeID") == 10 || r.Field<byte>("PatchTypeID") == 28 || r.Field<byte>("PatchTypeID") == 29 || r.Field<byte>("PatchTypeID") == 30 || r.Field<byte>("PatchTypeID") == 31 || r.Field<byte>("PatchTypeID") == 32 || r.Field<byte>("PatchTypeID") == 33 || r.Field<byte>("PatchTypeID") == 34 || r.Field<byte>("PatchTypeID") == 35 || r.Field<byte>("PatchTypeID") == 36 || r.Field<byte>("PatchTypeID") == 37 || r.Field<byte>("PatchTypeID") == 38 || r.Field<byte>("PatchTypeID") == 39 || r.Field<byte>("PatchTypeID") == 40 || r.Field<byte>("PatchTypeID") == 41 || r.Field<byte>("PatchTypeID") == 42 || r.Field<byte>("PatchTypeID") == 43 || r.Field<byte>("PatchTypeID") == 44 || r.Field<byte>("PatchTypeID") == 45 || r.Field<byte>("PatchTypeID") == 46 || r.Field<byte>("PatchTypeID") == 47 || r.Field<byte>("PatchTypeID") == 48 || r.Field<byte>("PatchTypeID") == 49 || r.Field<byte>("PatchTypeID") == 50)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "10" || MetricSumID == "11")   //----For 'G7 Black ΔL Avg' or ----For 'G7 Black ΔL Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 4 || r.Field<byte>("PatchTypeID") == 15 || r.Field<byte>("PatchTypeID") == 25 || r.Field<byte>("PatchTypeID") == 26 || r.Field<byte>("PatchTypeID") == 27)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "12" || MetricSumID == "13")    //----For 'G7 CMY / Gray ΔL Avg' or ----For 'G7 CMY / Gray ΔL Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 9 || r.Field<byte>("PatchTypeID") == 28 || r.Field<byte>("PatchTypeID") == 29 || r.Field<byte>("PatchTypeID") == 30 || r.Field<byte>("PatchTypeID") == 31 || r.Field<byte>("PatchTypeID") == 32 || r.Field<byte>("PatchTypeID") == 33 || r.Field<byte>("PatchTypeID") == 34 || r.Field<byte>("PatchTypeID") == 35 || r.Field<byte>("PatchTypeID") == 36 || r.Field<byte>("PatchTypeID") == 37 || r.Field<byte>("PatchTypeID") == 38 || r.Field<byte>("PatchTypeID") == 39 || r.Field<byte>("PatchTypeID") == 40 || r.Field<byte>("PatchTypeID") == 41 || r.Field<byte>("PatchTypeID") == 42 || r.Field<byte>("PatchTypeID") == 43 || r.Field<byte>("PatchTypeID") == 44 || r.Field<byte>("PatchTypeID") == 45 || r.Field<byte>("PatchTypeID") == 46 || r.Field<byte>("PatchTypeID") == 47 || r.Field<byte>("PatchTypeID") == 48 || r.Field<byte>("PatchTypeID") == 49 || r.Field<byte>("PatchTypeID") == 50)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "14")    //----For 'RGB ΔE Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 5 || r.Field<byte>("PatchTypeID") == 6 || r.Field<byte>("PatchTypeID") == 7)
                        .CopyToDataTable();
            }
            else
            {
                DT = dtPatchDetails;
            }

            return DT;
        }

        private void FillMetricSumList(List<ValidationMetrics> ValidationMetrics)
        {
            if (ValidationMetrics == null)
            {
                MeasurementId = 0;
                if (hdMeasurement.Value != "")
                {
                    MeasurementId = Convert.ToInt32(hdMeasurement.Value);  //----Pass selected measurement id from analyze chart above
                }
                _presenterAnalyze.GetAvgDeltaMeasurement_Validation();
                if (!IsErrorOccured)
                {
                    ValidationMetrics = MeasurementSummary[0].ValidationMetrics;
                }
            }

            rcmbMetricSumlist.Items.Clear();
            for (int i = 0; i < ValidationMetrics.Count; i++)
            {
                rcmbMetricSumlist.Items.Add(new RadComboBoxItem() { Text = ValidationMetrics[i].Metric, Value = ValidationMetrics[i].MetricSumID.ToString() });
            }

            rcmbMetricSumlist.Items.Insert(0, new RadComboBoxItem() { Text = "ALL", Value = "" });
            rcmbMetricSumlist.SelectedIndex = 0;
        }

        public void ConfigureExport()
        {
            //------------Patch Details GridView Export Settings----------------//
            rgPatchDetails.ExportSettings.ExportOnlyData = true;
            rgPatchDetails.ExportSettings.IgnorePaging = true;
            rgPatchDetails.ExportSettings.OpenInNewWindow = true;

            rgPatchDetails.MasterTableView.Columns.FindByUniqueName("HtmlColor").Visible = false;
        }

        #endregion

        #region Paging for top Trendline Chart
        protected void DDL_PageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //========== Alter the page size ==========//
            LTRL_PageSize.Text = DDL_PageSize.SelectedValue;

            int _PageSize = Convert.ToInt32(LTRL_PageSize.Text);
            int _PageCount = Convert.ToInt32(LTRL_TotalRec.Text) / _PageSize; if ((Convert.ToInt32(LTRL_TotalRec.Text) % _PageSize) > 0) { _PageCount = _PageCount + 1; }

            //========== Filling the DropDown ==========//
            DDL_GoToPage.Items.Clear();
            FillDDLGoToPage(_PageCount);

            BindMesurementChart();
        }

        protected void DDL_GoToPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindMesurementChart();
        }

        protected void IBTN_FirstPage_Click(object sender, ImageClickEventArgs e)
        {
            //========== Go To Page 1 ==========//
            DDL_GoToPage.SelectedIndex = 0;
            BindMesurementChart();
        }

        protected void IBTN_PreviousPage_Click(object sender, ImageClickEventArgs e)
        {
            //========== Go To previous page ==========//
            DDL_GoToPage.SelectedValue = Convert.ToString(Convert.ToInt32(DDL_GoToPage.SelectedValue) - 1);
            BindMesurementChart();
        }

        protected void IBTN_NextPage_Click(object sender, ImageClickEventArgs e)
        {
            //========== Go To next page ==========//
            DDL_GoToPage.SelectedValue = Convert.ToString(Convert.ToInt32(DDL_GoToPage.SelectedValue) + 1);
            BindMesurementChart();
        }

        protected void IBTN_LastPage_Click(object sender, ImageClickEventArgs e)
        {
            //========== Go To last page ==========//
            DDL_GoToPage.SelectedIndex = DDL_GoToPage.Items.Count - 1;
            BindMesurementChart();
        }

        public List<Entity.Measurement> AlignPaging(List<Entity.Measurement> MeasurementList)
        {
            List<Entity.Measurement> MeasurementReturn = new List<Entity.Measurement>();

            #region Paging
            #region Paging Variables
            //========== Settings for paging ==========//          
            int _PageSize = Convert.ToInt32(LTRL_PageSize.Text);
            int _CurrentPageIndex = Convert.ToInt32(LTRL_CurrentPage.Text);
            int _PageCount = MeasurementList.Count / _PageSize; if ((MeasurementList.Count % _PageSize) > 0) { _PageCount = _PageCount + 1; }
            int _StartIndex = 1;
            int _EndIndex = 0;
            #endregion

            //========== Filling the DropDown ==========//
            if (Page.IsPostBack == false)
            {
                FillDDLGoToPage(_PageCount);
                LTRL_TotalRec.Text = MeasurementList.Count.ToString();
            }

            #region Settings for Paging
            _CurrentPageIndex = Convert.ToInt32(DDL_GoToPage.SelectedValue);

            if (_CurrentPageIndex == 0) { _StartIndex = 0; } else { _StartIndex = (_CurrentPageIndex * _PageSize); }

            _EndIndex = _StartIndex + _PageSize;

            if (MeasurementList.Count < _EndIndex) { _EndIndex = MeasurementList.Count; }

            //========== Show Hide Settings and Setting Label Values ==========//        
            IBTN_FirstPage.Enabled = true; IBTN_FirstPage.ImageUrl = "~/Images/Nav_FirstPage.gif";
            IBTN_PreviousPage.Enabled = true; IBTN_PreviousPage.ImageUrl = "~/Images/Nav_PreviousPage.gif";

            IBTN_NextPage.Enabled = true; IBTN_NextPage.ImageUrl = "~/Images/Nav_NextPage.gif";
            IBTN_LastPage.Enabled = true; IBTN_LastPage.ImageUrl = "~/Images/Nav_LastPage.gif";

            //=========== Don't Show if Current Page = 0 =========//
            if (_CurrentPageIndex == 0)
            {
                IBTN_FirstPage.Enabled = false;
                IBTN_PreviousPage.Enabled = false;

                IBTN_FirstPage.ImageUrl = "~/Images/Nav_FirstPage_off.gif";
                IBTN_PreviousPage.ImageUrl = "~/Images/Nav_PreviousPage_off.gif";
            }
            //=========== Don't Show if Current Page = Last Page =========//
            if (_CurrentPageIndex == (_PageCount - 1))
            {
                IBTN_NextPage.Enabled = false;
                IBTN_LastPage.Enabled = false;

                IBTN_NextPage.ImageUrl = "~/Images/Nav_NextPage_Off.gif";
                IBTN_LastPage.ImageUrl = "~/Images/Nav_LastPage_Off.gif";
            }

            //=========== Don't Show if PageCount = 0 =========//
            if (_PageCount == 0)
            {
                IBTN_FirstPage.Enabled = false;
                IBTN_PreviousPage.Enabled = false;

                IBTN_NextPage.Enabled = false;
                IBTN_LastPage.Enabled = false;

                IBTN_FirstPage.ImageUrl = "~/Images/Nav_FirstPage_off.gif";
                IBTN_PreviousPage.ImageUrl = "~/Images/Nav_PreviousPage_off.gif";

                IBTN_NextPage.ImageUrl = "~/Images/Nav_NextPage_Off.gif";
                IBTN_LastPage.ImageUrl = "~/Images/Nav_LastPage_Off.gif";
            }

            //========== Set Values Back To Label ==========//
            LTRL_StartIndex.Text = _StartIndex.ToString();
            LTRL_CurrentPage.Text = _CurrentPageIndex.ToString();
            LTRL_StartRecord.Text = Convert.ToString(_StartIndex + 1);
            LTRL_EndRecord.Text = _EndIndex.ToString();
            LTRL_TotalRec.Text = MeasurementList.Count.ToString();

            #endregion

            for (int i = _StartIndex; i < _EndIndex; i = i + 1)
            {
                MeasurementReturn.Add(MeasurementList[i]);
            }

            return MeasurementReturn;

            #endregion
        }

        private void FillDDLGoToPage(int _Count)
        {
            try
            {
                DDL_GoToPage.Items.Clear();
                //========== Fills the Paging =========//
                for (int i = 0; i < _Count; i = i + 1)
                {
                    DDL_GoToPage.Items.Insert(i, (new ListItem((i + 1).ToString(), i.ToString())));
                }

                //========== Add A Def =========//
                if (_Count == 0) { DDL_GoToPage.Items.Insert(0, (new ListItem("--", "0"))); }

                //========== Sets Default =========//
                DDL_GoToPage.SelectedIndex = DDL_GoToPage.Items.Count - 1;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        private void PreparePrintLabelXML(Entity.Measurement measurmentSummary)
        {
            //Open label template from XML(.label) file            
            StreamReader streamReader = new StreamReader(Server.MapPath(@"Templates\CertifiedLabel.label"));
            //StreamReader streamReader = new StreamReader(@"D:\Chirag.Vidani\Work\FujiFilm\Dymo\Labels\CertifiedLabel.label");
            string labelXML = streamReader.ReadToEnd();
            streamReader.Close();
            //string labelXML = "XML";
            //Set the values to Placeholder
            labelXML = labelXML.Replace("[Job]", measurmentSummary.MeasurementName);
            labelXML = labelXML.Replace("[Device]", SessionManager.Instance.CurrentDevice.DeviceName);
            labelXML = labelXML.Replace("[Metric]", measurmentSummary.MetricTargetPresetName);
            labelXML = labelXML.Replace("[PrintStandard]", measurmentSummary.PrintStandardType);
            labelXML = labelXML.Replace("[Timestamp]", measurmentSummary.MeasurementDate.ToString("g"));
            labelXML = labelXML.Replace("[Customer]", measurmentSummary.Customer);
            labelXML = labelXML.Replace("[dEType]", measurmentSummary.dEMethod);
            System.Text.StringBuilder metricName = new StringBuilder();
            System.Text.StringBuilder metricTol = new StringBuilder();
            System.Text.StringBuilder metricActual = new StringBuilder();
            bool appendNewLine = true;
            foreach (var item in measurmentSummary.ValidationMetrics)
            {
                appendNewLine = true;
                if (measurmentSummary.ValidationMetrics.Count == measurmentSummary.ValidationMetrics.IndexOf(item) + 1)
                    appendNewLine = false;
                metricName.Append(item.Metric + (appendNewLine ? Environment.NewLine : ""));
                metricTol.Append(item.Tolerance.ToString() + (appendNewLine ? Environment.NewLine : ""));
                metricActual.Append(item.Actual.ToString() + (appendNewLine ? Environment.NewLine : ""));
            }
            labelXML = labelXML.Replace("[MetricCaption]", metricName.ToString());
            labelXML = labelXML.Replace("[dTol]", metricTol.ToString());
            labelXML = labelXML.Replace("[dActual]", metricActual.ToString());
            labelXML = labelXML.Replace("\r\n", @"\ \r\n");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LabelXML", " function GetLabelXml() { return '" + labelXML.ToString() + "'; };", true);

            string imagePath = "";
            if (measurmentSummary.MeasurementStatus.Equals("OK", StringComparison.InvariantCultureIgnoreCase))
                imagePath = Server.MapPath(@"Images\VStatusSuccessIcon.png");
            else
                imagePath = Server.MapPath(@"Images\VStatusFailedIcon.png");


            byte[] binaryData = File.ReadAllBytes(imagePath);
            string base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ImageString", " function GetStatusIconImageString() { return '" + base64String + "' ;};", true);
        }

        #endregion

        #region Properties
        public int JsRandomNumber { get { return new Random().Next(1, 100); } }
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
                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)      //--If validation, bind MetricSumList for filtering
                {
                    ViewState["MetricSumID"] = "";
                    FillMetricSumList(MeasurementSummary[0].ValidationMetrics);
                }
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
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                    _presenterAnalyze.Get1DMeasurementsOnRunId();
                else
                    _presenterAnalyze.GetMeasurementsOnRunId();
                if (!IsErrorOccured)
                {
                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                    {
                        //Pass use DLP as false for initial call
                        useDLP = false;
                        DeviceLinkProfile = null;

                        if (SessionManager.Instance.CurrentProject.ProjectTypeId == (short)EProjectType.G7)                                                 
                            _presenterAnalyze.CreateG7AlignmentCurve();                        
                        else//ISO                                                    
                            _presenterAnalyze.CreateISOAlignmentCurve();                        
                    }
                    else
                    {
                        EndDate = System.DateTime.Now;
                        DestinationProfileDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName);
                        DlpDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLPText) + EndDate.Month.ToString("00") + EndDate.Day.ToString("00") + EndDate.Hour.ToString("00") + EndDate.Minute.ToString("00"));
                        _presenterAnalyze.CreateDestinationAndDlp();
                    }
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

                //Now we are using DataTable to bind the grid               
                dtPatchDetails = new DataTable();

                _presenterAnalyze.GetPatchDetailsofMeasurement();
                if (!IsErrorOccured)
                {
                    //Now we are using DataTable as DataSource                    
                    rgPatchDetails.DataSource = FilteredPatchDetailsTable(ViewState["MetricSumID"].ToString(), dtPatchDetails);

                    //--------------If Sync1D project type, hide Target L, A and B columns--------//
                    if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D)
                    {
                        rgPatchDetails.MasterTableView.Columns.FindByDataField("TargetL").Visible = false;  //----TargetL
                        rgPatchDetails.MasterTableView.Columns.FindByDataField("Targeta").Visible = false;  //----Targeta
                        rgPatchDetails.MasterTableView.Columns.FindByDataField("Targetb").Visible = false;  //----Targetb
                        rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔE94").Visible = false;  //----ΔE94
                        rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔE2000").Visible = false;  //----ΔE2000
                    }
                    #region Make Visible/Invisible columns according to MetricSum selected in Validation
                    if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)      //--If validation, visible metricsum columns respectively
                    {
                        if (ViewState["MetricSumID"].ToString() == "5" || ViewState["MetricSumID"].ToString() == "6" || ViewState["MetricSumID"].ToString() == "7")
                        {
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔH").Visible = true;  //----ΔH
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔCh").Visible = false;  //----ΔCh
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔL").Visible = false;  //----ΔL
                        }
                        else if (ViewState["MetricSumID"].ToString() == "8" || ViewState["MetricSumID"].ToString() == "9")
                        {
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔCh").Visible = true;  //----ΔCh
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔH").Visible = false;  //----ΔH
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔL").Visible = false;  //----ΔL
                        }
                        else if (ViewState["MetricSumID"].ToString() == "10" || ViewState["MetricSumID"].ToString() == "11" || ViewState["MetricSumID"].ToString() == "12" || ViewState["MetricSumID"].ToString() == "13")
                        {
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔL").Visible = true;  //-----ΔL
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔH").Visible = false;  //----ΔH
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔCh").Visible = false;  //----ΔCh
                        }
                        else
                        {
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔH").Visible = false;  //----ΔH
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔCh").Visible = false;  //----ΔCh
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔL").Visible = false;  //----ΔL
                        }
                    }
                    #endregion
                }
            }
        }

        protected void rgPatchDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                Label lblHTML = (Label)item["HtmlColor"].FindControl("lbl_HTML");
                lblHTML.BackColor = System.Drawing.ColorTranslator.FromHtml(item.GetDataKeyValue("HTMLColor").ToString());
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

        protected void rbtnListDeltaE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //----------Bind Patch Details Gridview depending on selected measurement from Analyze Chart on "Patch Details" Tab-------//            
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

        protected void RG_ΔChSummary_ItemDataBound(object sender, GridItemEventArgs e)
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

        protected void RG_ΔLBlackSummary_ItemDataBound(object sender, GridItemEventArgs e)
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

        protected void RG_ΔLGraySummary_ItemDataBound(object sender, GridItemEventArgs e)
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

        protected void rcmbMetricSumlist_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //--------Set selected MetricSumID in viewstate and re-bind Patch Details RadGrid---------------//
            ViewState["MetricSumID"] = e.Value;
            rgPatchDetails.Rebind();
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

        public List<Entity.AnalyzeChart_G7> GrayFChart { get; set; }
        public List<Entity.AnalyzeChart_G7> GrayFSummary { get; set; }
        public List<Entity.AnalyzeChart_G7> BlackLChart { get; set; }
        public List<Entity.AnalyzeChart_G7> BlackLSummary { get; set; }
        public List<Entity.AnalyzeChart_G7> GrayLChart { get; set; }
        public List<Entity.AnalyzeChart_G7> GrayLSummary { get; set; }
        public List<Entity.AnalyzeChart_G7> BlackNPDChart { get; set; }
        public List<Entity.AnalyzeChart_G7> GrayNPDChart { get; set; }

        public DataTable dtPatchDetails { get; set; }

        public bool useDLP { get; set; }
        public byte[] DeviceLinkProfile { get; set; }

        public bool IsSync1DType
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D);
            }
        }

        public List<Entity.AnalyzeChart_DotGain> DTTVIChart { get; set; }

        #endregion        
    }
}
