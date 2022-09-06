using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


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
        private const string _recTriplet = "tripletText";
        private const string _recTripletPathMetrics = "tripletPathMetrics";
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
        private const string _reclocOptimizeResource1 = "locOptimizeResource1.Text";
        private const string _recpercentPatchMetrics = "percentPatchMetrics";
        private const string _recactualTVI = "actualTVI";
        private const string _rectargetTVI = "targetTVI";
        private const string _recspreadThr = "spreadThr";
        private const string _recTVIThr = "TVIThr";
        private const string _recDotGain = "dotGain";
        private const string _recPreDeltaE = "PreDeltaE";
        private const string _recPrevious = "PreviousText";

        //Label localization variables
        private const string _reclocLabelJob = "locLabelJob";
        private const string _reclocLabelDevice = "locLabelDevice";
        private const string _reclocLabelMetric = "locLabelMetric";
        private const string _reclocLabelStandard = "locLabelStandard";
        private const string _reclocLabelTimestamp = "locLabelTimestamp";
        private const string _reclocLabelCustomer = "locLabelCustomer";
        private const string _reclocLabelPageOrSurface = "locLabelPageOrSurface";
        private const string _reclocLabelJDFJobID = "locLabelJDFJobID";
        private const string _reclocLabeldEType = "locLabeldEType";
        private const string _reclocLabelTol = "locLabelTol";
        private const string _reclocLabelAct = "locLabelAct";
        private const string _reclocLabelSignature = "locLabelSignature";
        private const string _reclocLabelDate = "locLabelDate";

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
            get
            {
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync4DCertification) //---If Sync 4D Certification type of project
                {
                    return false;
                }
                else
                {
                    return SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1;
                }
            }
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
            Load += (sender, e) => ExceptionHandling(Page_Load, sender, e);
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
                hdnBaseServiceUrl.Value = Utilities.ReadConfigValue("BaseServiceURL");
                hdnCurrentUserID.Value = Convert.ToString(SessionManager.Instance.CurrentUser.UserId) ?? "0";
                hdfCurrentCulture.Value = getCurrentCultureLanguage();
                hdnProjectID.Value = SessionManager.Instance.CurrentOptimizeRun.ProjectId.ToString();
                hdnOptimizationRunID.Value = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId.ToString();
                hdnValidationType.Value = ValidationType.ToString();
                hdnCurrentProjectLicenseType.Value = SessionManager.Instance.CurrentProject.LicenseType.ToString();
                //--Start: Load average Compatibility values
                chkAnalyzeAverageOfMeasurements.Attributes.Add("onclick", "return CheckAvgCompability();");
                ChkAvgMeasurementCompatibility();
                //--End

                ViewState["MetricSumID"] = "";

                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;

                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    header.InnerText = GetResourceString(_recValidationAnalyzeHeaderText); //"Validation - Analyze";
                    //------Assign dEMethod value to radiolist, based on selected MetricTargetPresetID----------------//
                    hdnDefaultSelectedDelta.Value = rbtnListDeltaE.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.dEMethod.ToString().Substring(1);
                }
                else
                {
                    SessionManager.Instance.CurrentDlpSubOperation = SessionManager.Instance.CurrentProject.LicenseType; //---Assign DLP or Sync1D project type
                    if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D) //-----Sync1D
                    {
                        //------If project is Sync1D type, Assign 'E' to radiolist, bydefault (because Sync 1D has only dt_E type)----------------//
                        //rbtnListDeltaE.SelectedValue = "E";
                        hdnDefaultSelectedDelta.Value = rbtnListDeltaE.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.dEMethod.ToString().Substring(1);
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
                        hdnReportDLPName.Value = DLPName;
                    }
                }
                lbtnDownloadAndSaveDLP.Text = IsNewDlp
                                                  ? "<span><img id='imgAdd' src='Images/export.png'/>" + GetResourceString(_recCreateDLP) + "</span>"
                                                  : "<span><img id='imgAdd' src='Images/save_icon.png'/>" + GetResourceString(_recNext) + "</span>";//Create DLP - Next

                hdHasMeasurement.Value = SessionManager.Instance.CurrentOptimizeRun.HasMeasurements.ToString();
                hdnVerification.Value = IsVerification.ToString();
                hdnCurrentDlpSubOperation.Value = ((int)SessionManager.Instance.CurrentDlpSubOperation).ToString();
                hdnSync1DType.Value = IsSync1DType.ToString();
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
                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP) //---Only display Report Grading for Sync4D Validation
                    {
                        Div_ReportGrading.Visible = true; //---Disable for time being in Production code
                    }

                    lbtnDownloadAndSaveDLP.Visible = false;
                    lbtnMeasurementVerification.Visible = true;
                    if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.Sync4DCertification) //---If not Sync 4D Certification type of project, then only display Optimize button
                    {
                        lbtnOptimize.Visible = true;
                    }
                    //Here we are assigning TEXT because when partial postback takes place the text of the linkbutton is removed as we adding attributes onClick
                    //So its a kind of bug fix.
                    lbtnOptimize.Text = "<span>" + GetResourceString(_reclocOptimizeResource1) + "</span>";
                    lbtnOptimize.Attributes.Add("onclick",
                                                     "javascript: return ShowClosePrintConditionWindow('" +
                                                     SessionManager.Instance.CurrentProject.ProjectId +
                                                     "','" + Server.UrlEncode(SessionManager.Instance.CurrentProject.ProjectName) +
                                                     "','" + Server.UrlEncode(Convert.ToInt16(SessionManager.Instance.CurrentProject.LicenseType).ToString()) +
                                                     "','" + Server.UrlEncode(CurrentDLPName) + "')");
                    trRBtnListDeltaE.Visible = false;
                    Div_Validation_Header.Visible = true;
                    if (ValidationType == 1)  //G7 type
                    {
                        dvTab_G7.Visible = Tab_G7.Visible = true;
                    }
                    else
                    {
                        dvTab_G7.Visible = Tab_G7.Visible = false;
                    }
                }
                else if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D) //-----Sync1D
                {
                    //Hide Verification button
                    lbtnMeasurementVerification.Visible = false;
                    lbtnOptimize.Visible = false;
                    trRBtnListDeltaE.Visible = false;
                    Div_Opt_Header.Visible = true;
                    td1DSmoothing.Visible = true;  //Display in Sync1D for Smoothing measurements (Commented out for temporary untill get decided to use this feature)

                    if (ValidationType == 1)  //G7 type
                    {
                        dvTab_G7.Visible = Tab_G7.Visible = true;
                        tdAverageOfMeasurements.Visible = true; //Display in Sync1D G7 mode for Averaging measurements
                    }
                    else
                    {
                        dvTab_G7.Visible = Tab_G7.Visible = false;
                        if (ValidationType == 2)  //ISO type
                        {
                            tdAverageOfMeasurements.Visible = true; //Display in Sync1D ISO mode for Averaging measurements
                        }
                    }
                    dvTab_TVI.Visible = Tab_TVI.Visible = true;
                    if (ValidationType == 0)   //Default View type, Hide TVI tab
                    {
                        dvTab_TVI.Visible = Tab_TVI.Visible = false;
                    }
                    Div_MetricSumFilter.Visible = true;
                    Div_ReportCMYKDensity.Visible = true;  //---Disable for time being in Production code
                }
                else   //-----DLP Optimization
                {
                    //None
                    //Hide Verification button
                    lbtnMeasurementVerification.Visible = false;
                    lbtnOptimize.Visible = false;
                    trRBtnListDeltaE.Visible = true;
                    Div_Opt_Header.Visible = true;
                    dvTab_G7.Visible = Tab_G7.Visible = false;
                    dvTab_TVI.Visible = Tab_TVI.Visible = false;
                    Div_MetricSumFilter.Visible = false;
                }
            }
            else
            {
                BindMesurementChart();
            }
            if (IsXMFUser && SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
            {
                if (SessionManager.Instance.CurrentValidationRequest != null)
                {
                    lbtnSubmit.Visible = true;
                }
                else
                {
                    lbtnSubmit.Visible = false;
                    lbtnMeasurement.Visible = false;
                }
                lbtnOptimize.Visible = false;
                lbtnMeasurementVerification.Visible = false;
                lbtnDownloadAndSaveDLP.Visible = false;
            }

            if (IsXMFUser && Request.QueryString["viewonly"] != null && Convert.ToBoolean(Request.QueryString["viewonly"]) == true)
            {
                lbtnSubmit.Visible = false;
                lbtnMeasurement.Visible = false;
                lbtnOptimize.Visible = false;
                lbtnMeasurementVerification.Visible = false;
                lbtnDownloadAndSaveDLP.Visible = false;
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

                if (MeasurementList != null && MeasurementList.Count > 0)
                {
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
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            From_ΔE = rbtnListDeltaE.SelectedValue;

            if ((SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation) || (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D)) //---Validation mode or Sync1D project type
            {
                //This will be called when Verification process is going on
                _presenterAnalyze.GetAvgDeltaMeasurement_Validation();
                if (!IsErrorOccured)
                {
                    if (MeasurementSummary != null && MeasurementSummary.Count > 0)
                    {
                        tbOptSummary.Visible = false;
                        ValidationType = MeasurementSummary[0].ValidationType;
                        hdnValidationType.Value = ValidationType.ToString();
                        if (!IsAverageOfMeasurements)   //--Normal display Mode
                        {
                            dvValidationSummary.DataSource = MeasurementSummary;
                            dvValidationSummary.DataBind();
                        }
                        else   //---Average of Measurements display mode
                        {
                            dvValidationSummary.DataSource = new List<Entity.Measurement>();
                            dvValidationSummary.DataBind();
                            chartAnalyze.MapAreas.Clear();
                            chartAnalyze.IsMapEnabled = false;
                        }


                        if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                        {
                            dvValidationSummary.Fields[1].Visible = false; //Out of Tolerance
                            dvValidationSummary.Fields[8].Visible = false; //Page or Surface
                            dvValidationSummary.Fields[9].Visible = false; //JDF Job Id

                            dvValidationSummary.Fields[6].Visible = false; //Customer
                            dvValidationSummary.Fields[10].Visible = false; //Notes

                            if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                            {
                                dvValidationSummary.Fields[1].Visible = true; //Out of Tolerance
                                dvValidationSummary.Fields[6].Visible = true; //Customer
                                dvValidationSummary.Fields[10].Visible = true; //Notes
                            }
                        }

                        rgValidationSummaryMetrics.DataSource = MeasurementSummary[0].ValidationMetrics;
                        rgValidationSummaryMetrics.DataBind();

                        if (MeasurementSummary[0].PatchTolerances != null && MeasurementSummary[0].PatchTolerances.Count > 0) //--If Patch Tolerance available for this mesurement then only display RadGrid
                        {
                            rgPatchTolerance.DataSource = MeasurementSummary[0].PatchTolerances;
                            rgPatchTolerance.DataBind();
                        }
                        ValidationMeasurementStatus = MeasurementSummary[0].MeasurementStatus;

                        if (Page.IsPostBack == false)   //-------Fill MetricSumList when 1st time page load only
                        {
                            FillMetricSumList(MeasurementSummary[0].ValidationMetrics);
                        }

                        if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D)  //----Only display print label feature in validation mode
                        {
                            ibtnPrintPreview.Visible = false;
                            ibtnPrintLabel.Visible = false;
                        }
                        else
                        {
                            PreparePrintLabelXML(MeasurementSummary[0]);
                        }
                    }
                }
            }
            else
            {
                //In case of None (i.e. Optimization (Sync4D) project type)
                ibtnPrintPreview.Visible = false;
                ibtnPrintLabel.Visible = false;
                tbValidationSummary.Visible = false;

                MeasurementsDelta = new List<Entity.MeasurementPatch_AvgDelta>();
                _presenterAnalyze.GetAvgDeltaMeasurement_Optimization();
                if (!IsErrorOccured)
                {
                    ValidationType = -1; //-----For DLP Optimization Report
                    grdMeasurement.DataSource = MeasurementsDelta;
                    grdMeasurement.DataBind();
                }
            }

            //-----------Assign Report iFrame to load report in "Report" tab----------------------//
            string path = "";
            if (ValidationType == 1)  //G7 type
            {
                dvTab_G7.Visible = Tab_G7.Visible = true;
                ValidationType = 1;
                path = "ReportSingleMeasurement_G7_Vali.aspx?MeasurementID=" + MeasurementId + "&From_ΔE=" + rbtnListDeltaE.SelectedValue + "&DLPName=" + DLPName + "&Vali_Type=" + ValidationType;
                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D && ValidationType == 1) //Sync1D and G7
                {
                    path = "ReportSingleMeasurement_G7_Vali.aspx?MeasurementID=" + MeasurementId + "&From_ΔE=" + rbtnListDeltaE.SelectedValue + "&DLPName=" + DLPName + "&Vali_Type=" + ValidationType + "&IsAvgMeas=" + IsAverageOfMeasurements;
                }
            }
            else if (ValidationType == 2) //ISO type
            {
                dvTab_G7.Visible = Tab_G7.Visible = false;
                path = "ReportSingleMeasurement.aspx?MeasurementID=" + MeasurementId + "&From_ΔE=" + rbtnListDeltaE.SelectedValue + "&DLPName=" + DLPName + "&Vali_Type=" + ValidationType;
                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D && ValidationType == 2) //Sync1D and ISO
                {
                    path = "ReportSingleMeasurement.aspx?MeasurementID=" + MeasurementId + "&From_ΔE=" + rbtnListDeltaE.SelectedValue + "&DLPName=" + DLPName + "&Vali_Type=" + ValidationType + "&IsAvgMeas=" + IsAverageOfMeasurements;
                }
            }
            else //ValidationType = 0 for Default View type
            {
                dvTab_G7.Visible = Tab_G7.Visible = false;
                path = "ReportSingleMeasurement.aspx?MeasurementID=" + MeasurementId + "&From_ΔE=" + rbtnListDeltaE.SelectedValue + "&DLPName=" + DLPName + "&Vali_Type=" + ValidationType;
            }
            iframeGeneratePDFReport.Attributes.Add("src", "~/" + path);
            iframeGeneratePDFReport.Style.Add("display", "");

            //-------Assign Report Grading Pop up to load report in "Report" tab--------------//
            ReportGradingWindow.NavigateUrl = "../PopupReportGrading.aspx?MeasurementID=" + MeasurementId + "&From_ΔE=" + rbtnListDeltaE.SelectedValue + "&DLPName=" + DLPName + "&Vali_Type=" + ValidationType + "&IsAvgMeas=" + IsAverageOfMeasurements;

            //-------Assign Report CMYK Density Pop up to load report in "Report" tab--------------//
            ReportCMYKDensityWindow.NavigateUrl = "../PopupReportCMYKDensity.aspx?ProjectID=" + SessionManager.Instance.CurrentProject.ProjectId + "&OptimizeRunID=" + SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId + "&MeasurementID=" + MeasurementId;

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
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            From_ΔE = rbtnListDeltaE.SelectedValue;
            lblEPryPatches.Text = From_ΔE;

            _presenterAnalyze.GetInkPaperChart();
            if (!IsErrorOccured)
            {
                FillInkPaperChart(DTDeltaEPrimaryPatches, DTPaperPatches, DTDensity); //---Fill out Ink Paper Tab with 3 charts depending on selected Measurement ID-----//
            }

            _presenterAnalyze.GetGrayChart();
            if (!IsErrorOccured)
            {
                FillGrayCharts(DTLABChart, DTGaumatChart1, DTGaumatChart2);
            }

            if (ValidationType == 1)  //-------------Only call G7 tab data if it is G7 type of Validation / Sync 1D / Sync 4D Project
            {
                _presenterAnalyze.GetG7Chart();
                if (!IsErrorOccured)
                {
                    FillG7Chart(GrayFChart, GrayFSummary, BlackLChart, BlackLSummary, GrayLChart, GrayLSummary, BlackNPDChart, GrayNPDChart);
                }
            }
            if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D || SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation) //-----For Sync1D projects or validation projects
            {
                Tab_TVI.Visible = dvTab_TVI.Visible = true;
                if (ValidationType == 2)  //ISO type
                {
                    _presenterAnalyze.GetISOTVIChart();
                    if (!IsErrorOccured)
                    {
                        try
                        {
                            FillISOTVIChart(DTISOTVIChart);
                            FillSpreadChart(DTSpreadChartData, DTSpreadSplineRangeChartData, DTMaxSpreadChartData);
                            FillSpreadChart_CMYK(DTSpreadChartData_CMYK);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    UpdatePanel_TVI_ISO.Visible = true;
                    UpdatePanel_TVI_G7.Visible = false;
                }
                else if (ValidationType == 1)  //G7 type
                {
                    _presenterAnalyze.GetTVIChart();
                    if (!IsErrorOccured)
                    {
                        FillTVIChart(DTTVIChart);
                    }
                    UpdatePanel_TVI_ISO.Visible = false;
                    UpdatePanel_TVI_G7.Visible = true;
                }
                else    //Default display view type
                {
                    Tab_TVI.Visible = dvTab_TVI.Visible = false;
                }

            }
        }

        #region Fill InkPaper Tab
        private void FillInkPaperChart(List<AnalyzeChart> DTDeltaEPrimaryPatches, List<AnalyzeChart> Paper, List<AnalyzeChart> DTDensity)
        {
            if (DTDeltaEPrimaryPatches != null)
            {
                FillDeltaEPrimaryPatchesChart(DTDeltaEPrimaryPatches);
            }

            if (Paper != null)
            {
                FillPaperChart(Paper);
            }

            if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.DLP || ValidationType == 0)  //If Default view in Validation of Sync4D or Normal Sync4D optimization mode, don't display Density chart
            {
                Table_Density.Visible = false;
            }
            else
            {
                Table_Density.Visible = true;
                if (DTDensity != null)
                {
                    FillDensityChart(DTDensity);
                }
            }
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
            chartDeltaE.Series[3].Points.Clear();

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

            // === Series3 to display red marker on Thresold value for Previous Run=== //
            Series objSeries3 = (Series)chartDeltaE.Series[3];
            objSeries3.XValueType = ChartValueType.Int32;
            objSeries3.YValueType = ChartValueType.String;
            objSeries3.CustomProperties = "DrawingStyle=Cylinder";

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
                //---Set up lenght of background DtELimit charts bar poll.
                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D || SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    DtELimit = (from c in DT select c.ΔELimit).Max() + 1;  //-----------Get ΔElimit by getting ΔELimit plus add 1 more points
                    decimal DtELimit1 = (from c in DT select c.ΔE).Max() + 1;
                    if (DtELimit1 > DtELimit)
                    {
                        DtELimit = DtELimit1;
                    }
                    DtELimit = (DtELimit > 4 ? DtELimit : DtELimit + 1);
                }
                else
                {
                    DtELimit = (from c in DT select c.ΔE).Max() + 2;  //-----------Get ΔElimit by getting maxΔE / maxΔE2000 plus add 2 more points
                    DtELimit = (DtELimit > 4 ? DtELimit : DtELimit + 1);
                }

                for (int i = 0; i < DT.Count; i++)
                {
                    SBDeltaEPatches.Append("<tr style='height: 10px'>");

                    if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Cyan).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:0px;'>" + GetResourceString(_recCyan) + "</td>");//Cyan
                        TooltipTitle = GetResourceString(_recCyanPathMetrics); //"Cyan Patch Metrics";
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Magenta).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:0px;'>" + GetResourceString(_recMagenta) + "</td>");//Magenta
                        TooltipTitle = GetResourceString(_recMagentaPathMetrics);//Magenta Patch Metrics
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Yellow).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:0px;'>" + GetResourceString(_recYellow) + "</td>");//Yellow
                        TooltipTitle = GetResourceString(_recYellowPathMetrics); //"Yellow Patch Metrics";
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Black).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:0px;'>" + GetResourceString(_recBlack) + "</td>");//Black
                        TooltipTitle = GetResourceString(_recBlackPathMetrics); //"Black Patch Metrics";
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Overprint_Red).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:0px;'>" + GetResourceString(_recRed) + "</td>");
                        TooltipTitle = GetResourceString(_recRedPathMetrics); //"Red Patch Metrics";
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Overprint_Green).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:0px;'>" + GetResourceString(_recGreen) + "</td>");//Green
                        TooltipTitle = GetResourceString(_recGreenPathMetrics); //"Green Patch Metrics";
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Overprint_Blue).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:0px;'>" + GetResourceString(_recBlue) + "</td>");//Blue
                        TooltipTitle = GetResourceString(_recBluePathMetrics); //"Blue Patch Metrics";
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Paper).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:0px;'>" + GetResourceString(_recPaper) + "</td>");
                        TooltipTitle = GetResourceString(_recPaperPathMetrics); //"Paper Patch Metrics";
                    }
                    else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Triplet_Solid_Gray).ToString())
                    {
                        SBDeltaEPatches.Append("<td colspan='2' valign='middle' class='SyncBottomBorder' style='padding-left:10px; padding-top:0px; font-family:Tahoma; font-size:13px; padding-bottom:0px;'>" + GetResourceString(_recTriplet) + "</td>");
                        TooltipTitle = GetResourceString(_recTripletPathMetrics); //"Triplet Patch Metrics";
                    }
                    SBDeltaEPatches.Append("</tr>");

                    BindDeltaEValueACTTHValue(objSeries0, objSeries1, objSeries2, objSeries3, DT, i, TooltipTitle);
                }
                LitDeltaEPatches.Text = SBDeltaEPatches.ToString();
            }
            #endregion

            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartDeltaE.ImageLocation) + "." + chartDeltaE.ImageType;
            chartDeltaE.SaveImage(Server.MapPath(FileName));
        }

        private void BindDeltaEValueACTTHValue(Series objSeries0, Series objSeries1, Series objSeries2, Series objSeries3, List<AnalyzeChart> DR, int Index, string TooltipTitle)
        {
            Double Y1, Y2, Y3, Y4;
            Y1 = Y2 = Y3 = Y4 = 0;

            Y1 = (Convert.ToString(Common.Utilities.DecimalRound(Convert.ToDouble(DtELimit))) == "" ? 0 : Convert.ToDouble(Common.Utilities.DecimalRound(Convert.ToDouble(DtELimit))));
            Y2 = (Convert.ToString(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].ΔE.ToString()))) == "" ? 0 : Convert.ToDouble(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].ΔE.ToString()))));
            Y3 = (Convert.ToString(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].ΔELimit.ToString()))) == "" ? 0 : Convert.ToDouble(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].ΔELimit.ToString()))));
            Y4 = (Convert.ToString(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].PreAvgΔE.ToString()))) == "" ? 0 : Convert.ToDouble(Common.Utilities.DecimalRound(Convert.ToDouble(DR[Index].PreAvgΔE.ToString()))));

            // === When Actual value is not available then no series will display and no marker will display
            if (Y2 == 0)
            {
                Y1 = Y2;
            }

            objSeries0.Points.AddXY((Double)0, Y1);
            objSeries0.Points[Index]["PointWidth"] = "-1";

            Series SeriesActual = objSeries1;
            objSeries1.Points.AddXY((Double)0, Y2);

            if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D || SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation) //-----For Sync1D projects or validation projects
            {
                objSeries2.Points.AddXY((Double)0, Y3); //------Red marker image plot for Tolerance
                if (Y4 > 0)
                {
                    objSeries3.Points.AddXY((Double)0, Y4); //------Green marker image plot for Previous Run's Avg ΔE
                }
            }

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

            if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D || SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation) //-----For Sync1D projects or validation projects
            {
                toolTip.Append(" (Thr." + DR[Index].ΔELimit.ToString() + ")");
            }
            toolTip.Append("</td></tr>");
            if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D && DR[Index].PreAvgΔE > 0) //-----For Sync1D projects, display Previous ΔE in tooltip also
            {
                toolTip.Append("<tr><td>" + GetResourceString(_recPreDeltaE) + "</td><td>"); //Previous ΔE Value
                toolTip.Append(DR[Index].PreAvgΔE.ToString());
                toolTip.Append("</td></tr>");
            }
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


                FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartPaper1.ImageLocation) + "." + chartPaper1.ImageType;
                chartPaper1.SaveImage(Server.MapPath(FileName));
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


                FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartPaper2.ImageLocation) + "." + chartPaper2.ImageType;
                chartPaper2.SaveImage(Server.MapPath(FileName));
                #endregion
            }
        }
        #endregion

        #region Fill Density Chart

        private void FillDensityChart(List<AnalyzeChart> DT)
        {
            if (DT != null && DT.Count > 0)
            {
                Double CValue = 0;

                //========== Clear Points and Show Chart ==========//
                CHRT_SummaryDensity.Visible = true;
                CHRT_SummaryDensity.Series[0].Points.Clear();
                CHRT_SummaryDensity.Series[1].Points.Clear();
                CHRT_SummaryDensity.Series[2].Points.Clear();
                CHRT_SummaryDensity.Series[3].Points.Clear();

                #region //========== Declarations ==========//
                // === Series0 to display Thresold value === //    
                Series objSeries0 = (Series)CHRT_SummaryDensity.Series[0];
                objSeries0.XValueType = ChartValueType.String;
                objSeries0.YValueType = ChartValueType.String;
                objSeries0.CustomProperties = "DrawingStyle=Cylinder";

                // === Series1 to display Actual value === //
                Series objSeries1 = (Series)CHRT_SummaryDensity.Series[1];
                objSeries1.XValueType = ChartValueType.String;
                objSeries1.YValueType = ChartValueType.String;
                objSeries1.CustomProperties = "DrawingStyle=Cylinder";

                // === Series2 to display red marker on Thresold value === //
                Series objSeries2 = (Series)CHRT_SummaryDensity.Series[2];
                objSeries2.XValueType = ChartValueType.String;
                objSeries2.YValueType = ChartValueType.String;
                objSeries2.CustomProperties = "DrawingStyle=Cylinder";

                // === Series3 to display Green marker on Previous Run's average value === //
                Series objSeries3 = (Series)CHRT_SummaryDensity.Series[3];
                objSeries3.XValueType = ChartValueType.String;
                objSeries3.YValueType = ChartValueType.String;
                objSeries3.CustomProperties = "DrawingStyle=Cylinder";

                #endregion

                if (DT != null && DT.Count > 0)
                {
                    for (int i = 0; i < DT.Count; i++)
                    {
                        CValue = Utilities.DecimalRound(Convert.ToDouble(DT[i].Density));
                        if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Cyan).ToString())
                        {
                            AddDensityPoint(i, objSeries0, objSeries1, objSeries2, objSeries3, "C", CValue, DT[i].HtmlColor.ToString(), DT[i].Density, DT[i].DensityLimit, DT[i].PreAvgDensity, GetResourceString(_recCyanPathMetrics), true);
                        }
                        else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Magenta).ToString())
                        {
                            AddDensityPoint(i, objSeries0, objSeries1, objSeries2, objSeries3, "M", CValue, DT[i].HtmlColor.ToString(), DT[i].Density, DT[i].DensityLimit, DT[i].PreAvgDensity, GetResourceString(_recMagentaPathMetrics), true);
                        }
                        else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Yellow).ToString())
                        {
                            AddDensityPoint(i, objSeries0, objSeries1, objSeries2, objSeries3, "Y", CValue, DT[i].HtmlColor.ToString(), DT[i].Density, DT[i].DensityLimit, DT[i].PreAvgDensity, GetResourceString(_recYellowPathMetrics), true);
                        }
                        else if (DT[i].PatchTypeId.ToString() == Convert.ToInt16(CPPatchType.Black).ToString())
                        {
                            AddDensityPoint(i, objSeries0, objSeries1, objSeries2, objSeries3, "K", CValue, DT[i].HtmlColor.ToString(), DT[i].Density, DT[i].DensityLimit, DT[i].PreAvgDensity, GetResourceString(_recBlackPathMetrics), true);
                        }
                    }
                }
                else
                {
                    CHRT_SummaryDensity.ChartAreas["Default"].AxisY.Minimum = -10;
                    CHRT_SummaryDensity.ChartAreas["Default"].AxisY.Maximum = 10;

                    AddDensityPoint(0, objSeries0, objSeries1, objSeries2, objSeries3, "C", 0, "#E9E9E9", 0, 0, 0, "", false);
                    AddDensityPoint(1, objSeries0, objSeries1, objSeries2, objSeries3, "M", 0, "#E9E9E9", 0, 0, 0, "", false);
                    AddDensityPoint(2, objSeries0, objSeries1, objSeries2, objSeries3, "Y", 0, "#E9E9E9", 0, 0, 0, "", false);
                    AddDensityPoint(3, objSeries0, objSeries1, objSeries2, objSeries3, "K", 0, "#E9E9E9", 0, 0, 0, "", false);
                }

                FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHRT_SummaryDensity.ImageLocation) + "." + CHRT_SummaryDensity.ImageType;
                CHRT_SummaryDensity.SaveImage(Server.MapPath(FileName));
            }
        }

        private void AddDensityPoint(int Index, Series objSeries0, Series objSeries1, Series objSeries2, Series objSeries3, string _XValue, double _YValue, string HTMLColor, decimal _Actual, decimal _Target, decimal _PreAvgDensity, string ColorName, bool isTooltip)
        {
            try
            {
                objSeries0.Points.AddXY(_XValue, _Target);
                objSeries1.Points.AddXY(_XValue, _YValue);
                StringBuilder toolTip = new StringBuilder();
                if (isTooltip)
                {
                    #region Add Tooltip HTML
                    toolTip.Append("<table>");
                    toolTip.Append("<tr><td><b>" + ColorName + "</b></td></tr>");
                    toolTip.Append("<tr><td><table>");
                    toolTip.Append("<tr><td>" + GetResourceString(_recActual) + "</td><td>");
                    toolTip.Append(_Actual);
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>" + GetResourceString(_recTarget) + "</td><td>");
                    toolTip.Append(_Target);
                    toolTip.Append("</td></tr>");
                    if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D && _PreAvgDensity > 0) //-----For Sync1D projects, display Previous Density Avg in tooltip also
                    {
                        toolTip.Append("<tr><td>" + GetResourceString(_recPrevious) + "</td><td>");
                        toolTip.Append(_PreAvgDensity);
                        toolTip.Append("</td></tr>");
                    }
                    toolTip.Append("</table></td></tr>");
                    toolTip.Append("</table>");
                    #endregion
                }
                objSeries1.Points[Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                objSeries2.Points.AddXY(_XValue, _Target);  //------Red marker image plot for Tolerance
                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D && _PreAvgDensity > 0) //-----For Sync1D projects, display Previous Density Avg in green marker plot also
                {
                    objSeries3.Points.AddXY(_XValue, _PreAvgDensity); //------Green marker image plot for Previous Run's Avg Density
                }
                objSeries1.Points[Index].Color = System.Drawing.ColorTranslator.FromHtml(HTMLColor);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #endregion

        #region Fill Gray Tab
        private void FillGrayCharts(List<AnalyzeChart_Gray> DTLABChart, List<AnalyzeChart_Gray> DTGaumatChart1, List<AnalyzeChart_Gray> DTGaumatChart2)
        {
            if (DTLABChart != null && DTLABChart.Count > 0)
            {
                FillGrayLABCharts(DTLABChart);
            }

            if (DTGaumatChart1 != null && DTGaumatChart1.Count > 0 && DTGaumatChart2 != null && DTGaumatChart2.Count > 0)
            {
                FillGaumatChart(DTGaumatChart1, DTGaumatChart2);
            }
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

            #region Clear chartABComparison _1's Points
            chartABComparison_1.Series["Series0"].Points.Clear();
            chartABComparison_1.Series["Series1"].Points.Clear();
            chartABComparison_1.Series["Series2"].Points.Clear();
            chartABComparison_1.Series["Series3"].Points.Clear();
            chartABComparison_1.Series["Series4"].Points.Clear();

            #endregion

            if (DTTarget.Contains(DTTarget.Find(item => item.PatchType == CPPatchType.Paper.ToString())))
            {
                AnalyzeChart_Gray List_Paper = DTTarget.Find(item => item.PatchType == CPPatchType.Paper.ToString());

                Papera = Convert.ToDouble(List_Paper.CielabA);
                Paperb = Convert.ToDouble(List_Paper.CielabB);
            }

            #region Set the colors first
            BindLABComparision("Cyan", GetResourceString(_recCyanPathMetrics), DTColors, Papera, Paperb, 5, false, false);
            BindLABComparision("Magenta", GetResourceString(_recMagentaPathMetrics), DTColors, Papera, Paperb, 5, false, false);
            BindLABComparision("Yellow", GetResourceString(_recYellowPathMetrics), DTColors, Papera, Paperb, 5, false, false);
            BindLABComparision("Red", GetResourceString(_recRedPathMetrics), DTColors, Papera, Paperb, 5, false, false);
            BindLABComparision("Green", GetResourceString(_recGreenPathMetrics), DTColors, Papera, Paperb, 5, false, false);
            BindLABComparision("Blue", GetResourceString(_recBluePathMetrics), DTColors, Papera, Paperb, 5, false, false);

            BindLABComparision("Cyan", GetResourceString(_recCyanPathMetrics), DTTarget, 0, 0, 9, true, false);
            BindLABComparision("Magenta", GetResourceString(_recMagentaPathMetrics), DTTarget, 0, 0, 9, true, false);
            BindLABComparision("Yellow", GetResourceString(_recYellowPathMetrics), DTTarget, 0, 0, 9, true, false);
            BindLABComparision("Red", GetResourceString(_recRedPathMetrics), DTTarget, 0, 0, 9, true, false);
            BindLABComparision("Green", GetResourceString(_recGreenPathMetrics), DTTarget, 0, 0, 9, true, false);
            BindLABComparision("Blue", GetResourceString(_recBluePathMetrics), DTTarget, 0, 0, 9, true, false);
            BindLABComparision("Paper", GetResourceString(_recPaperPathMetrics), DTTarget, 0, 0, 9, true, false);

            BindLABComparision("Cyan", GetResourceString(_recCyanPathMetrics), DTTarget, 0, 0, 6, false, true);
            BindLABComparision("Magenta", GetResourceString(_recMagentaPathMetrics), DTTarget, 0, 0, 6, false, true);
            BindLABComparision("Yellow", GetResourceString(_recYellowPathMetrics), DTTarget, 0, 0, 6, false, true);
            BindLABComparision("Red", GetResourceString(_recRedPathMetrics), DTTarget, 0, 0, 6, false, true);
            BindLABComparision("Green", GetResourceString(_recGreenPathMetrics), DTTarget, 0, 0, 6, false, true);
            BindLABComparision("Blue", GetResourceString(_recBluePathMetrics), DTTarget, 0, 0, 6, false, true);
            BindLABComparision("Paper", GetResourceString(_recPaperPathMetrics), DTTarget, 0, 0, 6, false, true);
            #endregion

            BindLABComparison_1(DTTarget);

            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartABComparison.ImageLocation) + "." + chartABComparison.ImageType;
            chartABComparison.SaveImage(Server.MapPath(FileName));

            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartABComparison_1.ImageLocation) + "." + chartABComparison_1.ImageType;
            chartABComparison_1.SaveImage(Server.MapPath(FileName));

            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartLComparison.ImageLocation) + "." + chartLComparison.ImageType;
            chartLComparison.SaveImage(Server.MapPath(FileName));
        }

        private void BindLABComparison_1(List<AnalyzeChart_Gray> DT)
        {
            List<AnalyzeChart_Gray> ListFilter_1 = new List<AnalyzeChart_Gray>();

            if (DT != null)
            {
                #region Measurement LAB Value
                if (DT.Count > 0)
                {
                    int i = 0;

                    #region Cyan
                    ListFilter_1 = DT.FindAll(item => item.PatchType == CPPatchType.Cyan.ToString());

                    if (ListFilter_1.Count > 0)
                    {
                        i = 0;
                        chartABComparison_1.Series[0].Points.AddXY(Convert.ToDouble(ListFilter_1[0].Targeta), Convert.ToDouble(ListFilter_1[0].Targetb));
                        chartABComparison_1.Series[0].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[0].Points[i].BorderWidth = 1;

                        chartABComparison_1.Series[1].Points.AddXY(Convert.ToDouble(ListFilter_1[0].CielabA), Convert.ToDouble(ListFilter_1[0].CielabB));
                        chartABComparison_1.Series[1].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                        chartABComparison_1.Series[1].Points[i].MarkerColor = Color.Cyan;
                        chartABComparison_1.Series[1].Points[i].BorderWidth = 1;
                    }
                    #endregion

                    #region Green
                    ListFilter_1 = DT.FindAll(item => item.PatchType == "Green");
                    if (ListFilter_1.Count > 0)
                    {
                        i = 1;
                        chartABComparison_1.Series[0].Points.AddXY(Convert.ToDouble(ListFilter_1[0].Targeta), Convert.ToDouble(ListFilter_1[0].Targetb));
                        chartABComparison_1.Series[0].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[0].Points[i].BorderWidth = 1;

                        chartABComparison_1.Series[1].Points.AddXY(Convert.ToDouble(ListFilter_1[0].CielabA), Convert.ToDouble(ListFilter_1[0].CielabB));
                        chartABComparison_1.Series[1].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                        chartABComparison_1.Series[1].Points[i].MarkerColor = Color.Green;
                        chartABComparison_1.Series[1].Points[i].BorderWidth = 1;
                    }
                    #endregion

                    #region Yellow
                    ListFilter_1 = DT.FindAll(item => item.PatchType == CPPatchType.Yellow.ToString());
                    if (ListFilter_1.Count > 0)
                    {
                        i = 2;
                        chartABComparison_1.Series[0].Points.AddXY(Convert.ToDouble(ListFilter_1[0].Targeta), Convert.ToDouble(ListFilter_1[0].Targetb));
                        chartABComparison_1.Series[0].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[0].Points[i].BorderWidth = 1;

                        chartABComparison_1.Series[1].Points.AddXY(Convert.ToDouble(ListFilter_1[0].CielabA), Convert.ToDouble(ListFilter_1[0].CielabB));
                        chartABComparison_1.Series[1].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                        chartABComparison_1.Series[1].Points[i].MarkerColor = Color.Yellow;
                        chartABComparison_1.Series[1].Points[i].BorderWidth = 1;
                    }
                    #endregion

                    #region Red
                    ListFilter_1 = DT.FindAll(item => item.PatchType == "Red");
                    if (ListFilter_1.Count > 0)
                    {
                        i = 3;
                        chartABComparison_1.Series[0].Points.AddXY(Convert.ToDouble(ListFilter_1[0].Targeta), Convert.ToDouble(ListFilter_1[0].Targetb));
                        chartABComparison_1.Series[0].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[0].Points[i].BorderWidth = 1;

                        chartABComparison_1.Series[1].Points.AddXY(Convert.ToDouble(ListFilter_1[0].CielabA), Convert.ToDouble(ListFilter_1[0].CielabB));
                        chartABComparison_1.Series[1].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                        chartABComparison_1.Series[1].Points[i].MarkerColor = Color.Red;
                        chartABComparison_1.Series[1].Points[i].BorderWidth = 1;
                    }
                    #endregion

                    #region Magenta
                    ListFilter_1 = DT.FindAll(item => item.PatchType == CPPatchType.Magenta.ToString());
                    if (ListFilter_1.Count > 0)
                    {
                        i = 4;
                        chartABComparison_1.Series[0].Points.AddXY(Convert.ToDouble(ListFilter_1[0].Targeta), Convert.ToDouble(ListFilter_1[0].Targetb));
                        chartABComparison_1.Series[0].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[0].Points[i].BorderWidth = 1;

                        chartABComparison_1.Series[1].Points.AddXY(Convert.ToDouble(ListFilter_1[0].CielabA), Convert.ToDouble(ListFilter_1[0].CielabB));
                        chartABComparison_1.Series[1].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                        chartABComparison_1.Series[1].Points[i].MarkerColor = Color.Magenta;
                        chartABComparison_1.Series[1].Points[i].BorderWidth = 1;
                    }
                    #endregion

                    #region Blue
                    ListFilter_1 = DT.FindAll(item => item.PatchType == "Blue");
                    if (ListFilter_1.Count > 0)
                    {
                        i = 5;
                        chartABComparison_1.Series[0].Points.AddXY(Convert.ToDouble(ListFilter_1[0].Targeta), Convert.ToDouble(ListFilter_1[0].Targetb));
                        chartABComparison_1.Series[0].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[0].Points[i].BorderWidth = 1;

                        chartABComparison_1.Series[1].Points.AddXY(Convert.ToDouble(ListFilter_1[0].CielabA), Convert.ToDouble(ListFilter_1[0].CielabB));
                        chartABComparison_1.Series[1].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                        chartABComparison_1.Series[1].Points[i].MarkerColor = Color.Blue;
                        chartABComparison_1.Series[1].Points[i].BorderWidth = 1;
                    }
                    #endregion

                    #region Other
                    ListFilter_1 = DT.FindAll(item => item.PatchType == CPPatchType.Cyan.ToString());
                    if (ListFilter_1.Count > 0)
                    {
                        i = 6;
                        chartABComparison_1.Series[0].Points.AddXY(Convert.ToDouble(ListFilter_1[0].Targeta), Convert.ToDouble(ListFilter_1[0].Targetb));
                        chartABComparison_1.Series[0].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[0].Points[i].BorderWidth = 1;

                        chartABComparison_1.Series[1].Points.AddXY(Convert.ToDouble(ListFilter_1[0].CielabA), Convert.ToDouble(ListFilter_1[0].CielabB));
                        chartABComparison_1.Series[1].ChartType = SeriesChartType.Line;
                        chartABComparison_1.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                        chartABComparison_1.Series[1].Points[i].MarkerColor = Color.Cyan;
                        chartABComparison_1.Series[1].Points[i].BorderWidth = 1;
                    }
                    #endregion

                    #region Middle Point
                    ListFilter_1 = DT.FindAll(item => item.PatchType == "Paper");
                    if (ListFilter_1.Count > 0)
                    {
                        i = 0;
                        chartABComparison_1.Series[2].Points.AddXY(Convert.ToDouble(ListFilter_1[0].Targeta), Convert.ToDouble(ListFilter_1[0].Targetb));
                        chartABComparison_1.Series[2].ChartType = SeriesChartType.Point;
                        chartABComparison_1.Series[2].Points[i].BorderWidth = 1;

                        chartABComparison_1.Series[3].Points.AddXY(Convert.ToDouble(ListFilter_1[0].CielabA), Convert.ToDouble(ListFilter_1[0].CielabB));
                        chartABComparison_1.Series[3].ChartType = SeriesChartType.Point;
                        chartABComparison_1.Series[3].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                        chartABComparison_1.Series[3].Points[i].MarkerColor = Color.White;
                        chartABComparison_1.Series[3].Points[i].BorderWidth = 1;
                    }
                    #endregion
                }
                #endregion

            }
        }

        private void BindLABComparision(string ColorName, string TooltipColorName, List<AnalyzeChart_Gray> DT, Double Papera, Double Paperb, int MarkerSize, bool isTarget, bool isActual)
        {
            StringBuilder toolTip = new StringBuilder();
            List<AnalyzeChart_Gray> ListFilter = new List<AnalyzeChart_Gray>();

            ListFilter = DT.FindAll(item => item.PatchType == ColorName);

            for (int i = 0; i < ListFilter.Count; i = i + 1)
            {
                int pointCount;

                #region Bind AB Comparison
                if (i == 0 && !isTarget && !isActual)
                {
                    chartABComparison.Series[ColorName].ChartType = SeriesChartType.Line;
                    chartABComparison.Series[ColorName].Points.AddXY(Papera, Paperb);
                }

                pointCount = ((isTarget) ? chartABComparison.Series["Targets"].Points.Count : chartABComparison.Series[ColorName].Points.Count);

                if (isTarget || isActual)
                {
                    #region Add Tooltip HTML
                    toolTip.Append("<table>");
                    toolTip.Append("<tr><td><b>" + TooltipColorName + "</b></td></tr>");
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
                    chartABComparison.Series[ColorName].MarkerBorderColor = Color.Black;
                }


                if (i == ListFilter.Count - 1 && (isTarget || isActual))
                {
                    if (isActual)
                    {
                        chartABComparison.Series[ColorName].Points[pointCount].BorderWidth = 1;
                        chartABComparison.Series[ColorName].Points[pointCount].MarkerSize = MarkerSize;
                        chartABComparison.Series[ColorName].MarkerBorderColor = Color.Black;
                        chartABComparison.Series[ColorName].Points[pointCount].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                        chartABComparison.Series[ColorName].Points[pointCount].Color = (Color)new ColorConverter().ConvertFromString(ColorName.Equals("paper", StringComparison.CurrentCultureIgnoreCase) ? "White" : ColorName);
                    }
                }
                else
                {
                    chartABComparison.Series[ColorName].Points[pointCount].BorderWidth = 1;
                    chartABComparison.Series[ColorName].Points[pointCount].MarkerSize = MarkerSize;
                    chartABComparison.Series[ColorName].MarkerBorderColor = Color.Black;
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
                    AddPointLABChart(i, chartL.Series["Series1"], Convert.ToInt64(DT[i].CmykC), DT[i].CielabL, Color.Black.Name.ToString(), DT, true);
                    AddPointLABChart(i, chartA.Series["Series1"], Convert.ToInt64(DT[i].CmykC), DT[i].CielabA, Color.Black.Name.ToString(), DT, true);
                    AddPointLABChart(i, chartB.Series["Series1"], Convert.ToInt64(DT[i].CmykC), DT[i].CielabB, Color.Black.Name.ToString(), DT, true);

                    if (DT[i].TargetL != null)
                    {
                        AddPointLABChart(i, chartL.Series["Series2"], Convert.ToInt64(DT[i].CmykC), DT[i].TargetL, Color.Green.Name.ToString(), DT, false);
                    }
                    AddPointLABChart(i, chartA.Series["Series2"], Convert.ToInt64(DT[i].CmykC), DT[i].Targeta, Color.Green.Name.ToString(), DT, false);
                    AddPointLABChart(i, chartB.Series["Series2"], Convert.ToInt64(DT[i].CmykC), DT[i].Targetb, Color.Green.Name.ToString(), DT, false);
                }
            }

            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartL.ImageLocation) + "." + chartL.ImageType;
            chartL.SaveImage(Server.MapPath(FileName));
            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartA.ImageLocation) + "." + chartA.ImageType;
            chartA.SaveImage(Server.MapPath(FileName));
            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartB.ImageLocation) + "." + chartB.ImageType;
            chartB.SaveImage(Server.MapPath(FileName));
        }

        private void AddPointLABChart(int _Index, Series _objSeries, double _XValue, decimal _YValue, string ColorName, List<AnalyzeChart_Gray> DR, bool isActual)
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
            {
                _objSeries.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
            }

            _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            _objSeries.Points[_Index].MarkerSize = 5;
            _objSeries.Points[_Index].BorderWidth = 1;
            _objSeries.Points[_Index].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
        }

        #endregion

        #endregion

        #region Fill G7 Tab
        private void FillG7Chart(List<AnalyzeChart_G7> GrayFChart, List<AnalyzeChart_G7> GrayFSummary, List<AnalyzeChart_G7> BlackLChart, List<AnalyzeChart_G7> BlackLSummary, List<AnalyzeChart_G7> GrayLChart, List<AnalyzeChart_G7> GrayLSummary, List<AnalyzeChart_G7> BlackNPDChart, List<AnalyzeChart_G7> GrayNPDChart)
        {
            if (GrayFChart != null && BlackLChart != null && GrayLChart != null)
            {
                FillGrayFLChart(GrayFChart, BlackLChart, GrayLChart);
            }

            if (GrayFSummary != null)
            {
                FillGrayQuartileSummary(GrayFSummary, BlackLSummary, GrayLSummary);
            }

            if (BlackNPDChart != null && GrayNPDChart != null)
            {
                FillNPDChart(BlackNPDChart, GrayNPDChart);
            }
        }

        #region Fill Gray Ch / F  and ΔL Charts
        private void FillGrayFLChart(List<AnalyzeChart_G7> DT, List<AnalyzeChart_G7> DTBlackL, List<AnalyzeChart_G7> DTGrayL)
        {
            #region Fill Gray Ch / F Chart
            Chart_GrayF.Series["Series1"].Points.Clear();
            Chart_GrayF.Series["Series1"]["TooltipOffset"] = "True";
            Chart_GrayF.Series["Series1Point"].Points.Clear();
            Chart_GrayF.Series["Series2"].Points.Clear();
            Chart_GrayF.Series["Series2"]["TooltipOffset"] = "True";
            Chart_GrayF.Series["Series2Point"].Points.Clear();
            Chart_GrayF.Series["Series3"].Points.Clear();
            Chart_GrayF.Series["Series3Point"].Points.Clear();

            if (DT != null && DT.Count > 0)
            {
                for (int i = 0; i < DT.Count; i++)
                {
                    AddPointInChart(i, Chart_GrayF.Series["Series1"], Chart_GrayF.Series["Series1Point"], Convert.ToInt64(DT[i].Cmyk_Value), DT[i].ΔValue, Color.Black.Name.ToString(), true, "ΔCh*");
                    if (SessionManager.Instance.CurrentProject.MeasurementSetting.PresetWeighted == true) //--If selected Metric Target Prest has Weighted Delta enabled, then only display weighted deltas blue bar
                    {
                        AddPointInChart(i, Chart_GrayF.Series["Series2"], Chart_GrayF.Series["Series2Point"], Convert.ToInt64(DT[i].Cmyk_Value), DT[i].wΔValue, Color.Blue.Name.ToString(), true, "wΔCh*");
                    }
                    AddPointInChart(i, Chart_GrayF.Series["Series3"], Chart_GrayF.Series["Series3Point"], Convert.ToInt64(DT[i].Cmyk_Value), DT[i].ΔLimit, Color.Red.Name.ToString(), false, "ΔCh*");
                }
            }

            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(Chart_GrayF.ImageLocation) + "." + Chart_GrayF.ImageType;
            Chart_GrayF.SaveImage(Server.MapPath(FileName));
            #endregion

            #region Fill Black ΔL Chart
            Chart_BlackL.Series["Series1"].Points.Clear();
            Chart_BlackL.Series["Series1"]["TooltipOffset"] = "True";
            Chart_BlackL.Series["Series1Point"].Points.Clear();
            Chart_BlackL.Series["Series2"].Points.Clear();
            Chart_BlackL.Series["Series2"]["TooltipOffset"] = "True";
            Chart_BlackL.Series["Series2Point"].Points.Clear();
            Chart_BlackL.Series["Series3"].Points.Clear();
            Chart_BlackL.Series["Series3Point"].Points.Clear();

            if (DTBlackL != null && DTBlackL.Count > 0)
            {
                for (int i = 0; i < DTBlackL.Count; i++)
                {
                    AddPointInChart(i, Chart_BlackL.Series["Series1"], Chart_BlackL.Series["Series1Point"], Convert.ToInt64(DTBlackL[i].Cmyk_Value), DTBlackL[i].ΔValue, Color.Black.Name.ToString(), true, "ΔL*");
                    if (SessionManager.Instance.CurrentProject.MeasurementSetting.PresetWeighted == true) //--If selected Metric Target Prest has Weighted Delta enabled, then only display weighted deltas blue bar
                    {
                        AddPointInChart(i, Chart_BlackL.Series["Series2"], Chart_BlackL.Series["Series2Point"], Convert.ToInt64(DTBlackL[i].Cmyk_Value), DTBlackL[i].wΔValue, Color.Blue.Name.ToString(), true, "wΔL*");
                    }
                    AddPointInChart(i, Chart_BlackL.Series["Series3"], Chart_BlackL.Series["Series3Point"], Convert.ToInt64(DTBlackL[i].Cmyk_Value), DTBlackL[i].ΔLimit, Color.Red.Name.ToString(), false, "ΔL*");
                }
            }

            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(Chart_BlackL.ImageLocation) + "." + Chart_BlackL.ImageType;
            Chart_BlackL.SaveImage(Server.MapPath(FileName));
            #endregion

            #region Fill CMY / Gray ΔL Chart
            Chart_GrayL.Series["Series1"].Points.Clear();
            Chart_GrayL.Series["Series1"]["TooltipOffset"] = "True";
            Chart_GrayL.Series["Series1Point"].Points.Clear();
            Chart_GrayL.Series["Series2"].Points.Clear();
            Chart_GrayL.Series["Series2"]["TooltipOffset"] = "True";
            Chart_GrayL.Series["Series2Point"].Points.Clear();
            Chart_GrayL.Series["Series3"].Points.Clear();
            Chart_GrayL.Series["Series3Point"].Points.Clear();

            if (DTGrayL != null && DTGrayL.Count > 0)
            {
                for (int i = 0; i < DTGrayL.Count; i++)
                {
                    AddPointInChart(i, Chart_GrayL.Series["Series1"], Chart_GrayL.Series["Series1Point"], Convert.ToInt64(DTGrayL[i].Cmyk_Value), DTGrayL[i].ΔValue, Color.Black.Name.ToString(), true, "ΔL*");
                    if (SessionManager.Instance.CurrentProject.MeasurementSetting.PresetWeighted == true) //--If selected Metric Target Prest has Weighted Delta enabled, then only display weighted deltas blue bar
                    {
                        AddPointInChart(i, Chart_GrayL.Series["Series2"], Chart_GrayL.Series["Series2Point"], Convert.ToInt64(DTGrayL[i].Cmyk_Value), DTGrayL[i].wΔValue, Color.Blue.Name.ToString(), true, "wΔL*");
                    }
                    AddPointInChart(i, Chart_GrayL.Series["Series3"], Chart_GrayL.Series["Series3Point"], Convert.ToInt64(DTGrayL[i].Cmyk_Value), DTGrayL[i].ΔLimit, Color.Red.Name.ToString(), false, "ΔL*");
                }
            }

            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(Chart_GrayL.ImageLocation) + "." + Chart_GrayL.ImageType;
            Chart_GrayL.SaveImage(Server.MapPath(FileName));
            #endregion
        }

        private void AddPointInChart(int _Index, Series _objSeries, Series _objSeriesPoint, long _XValue, decimal _YValue, string ColorName, bool showCircle, string From_Delta)
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
            toolTip.Append("<tr><td><b>" + _XValue + GetResourceString(_recpercentPatchMetrics) + "</b></td></tr>");
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
            CHRT_BlackNPD.Series["Series0"].Points.Clear();
            CHRT_BlackNPD.Series["Series0Point"].Points.Clear();

            // There is an issue with tooltip on spline chart, by default it ignores 
            //http://support2.dundas.com/OnlineDocumentation/WebChart2003/CustomAttributes_TooltipOffset.html
            CHRT_BlackNPD.Series["Series0"]["TooltipOffset"] = "True";
            CHRT_BlackNPD.Series["Series0Point"]["TooltipOffset"] = "True";

            if (DTBlack != null && DTBlack.Count > 0)
            {
                for (int i = 0; i < DTBlack.Count; i++)
                {
                    AddNPDPointChart(i, CHRT_BlackNPD.Series["Series0"], Convert.ToInt64(DTBlack[i].Cmyk_Value).ToString(), DTBlack[i].ΔLimit.ToString(), Color.Black.Name.ToString(), "Black Target NPD", "G7NPDC");
                    AddNPDPointChart(i, CHRT_BlackNPD.Series["Series0Point"], Convert.ToInt64(DTBlack[i].Cmyk_Value).ToString(), DTBlack[i].ΔValue.ToString(), Color.Green.Name.ToString(), "Black Actual NPD", "G7NPDC Actual");
                }
            }

            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHRT_BlackNPD.ImageLocation) + "." + CHRT_BlackNPD.ImageType;
            CHRT_BlackNPD.SaveImage(Server.MapPath(FileName));

            CHRT_GrayNPD.Series["Series0"].Points.Clear();
            CHRT_GrayNPD.Series["Series0Point"].Points.Clear();

            CHRT_GrayNPD.Series["Series0"]["TooltipOffset"] = "True";
            CHRT_GrayNPD.Series["Series0Point"]["TooltipOffset"] = "True";

            if (DTGray != null && DTGray.Count > 0)
            {
                for (int i = 0; i < DTGray.Count; i++)
                {
                    AddNPDPointChart(i, CHRT_GrayNPD.Series["Series0"], Convert.ToInt64(DTGray[i].Cmyk_Value).ToString(), DTGray[i].ΔLimit.ToString(), Color.Black.Name.ToString(), "Gray Target NPD", "G7NPDC");
                    AddNPDPointChart(i, CHRT_GrayNPD.Series["Series0Point"], Convert.ToInt64(DTGray[i].Cmyk_Value).ToString(), DTGray[i].ΔValue.ToString(), Color.Green.Name.ToString(), "Gray Actual NPD", "G7NPDC Actual");
                }
            }

            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHRT_GrayNPD.ImageLocation) + "." + CHRT_GrayNPD.ImageType;
            CHRT_GrayNPD.SaveImage(Server.MapPath(FileName));
        }

        private void AddNPDPointChart(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName, string TitleName, string ValueName)
        {
            _objSeries.ChartType = SeriesChartType.Spline;

            _objSeries.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;
            _objSeries.Points[_Index].MarkerSize = 1;  //changed 0 by chirag on 10/23/10 as we will drawing more points and make smooth curve 
            _objSeries.Points[_Index].BorderWidth = 1;
            _objSeries.Points[_Index].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(ColorName);

            #region Add Tooltip HTML
            StringBuilder toolTip = new StringBuilder();
            toolTip.Append("<table>");
            toolTip.Append("<tr><td><b> " + TitleName + " " + _XValue + "% Patch Metrics</b></td></tr>");
            toolTip.Append("<tr><td><table>");
            toolTip.Append("<tr><td>" + ValueName + " : </td><td>");
            toolTip.Append(Utilities.DecimalRound(Convert.ToDouble(_YValue)).ToString());
            toolTip.Append("</td></tr>");
            toolTip.Append("</table></td></tr>");
            toolTip.Append("</table>");
            #endregion
            _objSeries.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
        }

        #endregion

        #endregion

        #region FIll TVI Tab

        private void FillTVIChart(List<AnalyzeChart_DotGain> DT)
        {
            try
            {
                #region " === Clear Points === "
                //========== Clear Points and Show Chart ==========//
                CHRT_SummaryDotgain.Series[0].Points.Clear();
                CHRT_SummaryDotgain.Series[1].Points.Clear();
                CHRT_SummaryDotgain.Series[2].Points.Clear();
                CHRT_SummaryDotgain.Series[3].Points.Clear();
                CHRT_SummaryDotgain.Series[4].Points.Clear();
                CHRT_SummaryDotgain.Series[5].Points.Clear();
                CHRT_SummaryDotgain.Series[6].Points.Clear();
                CHRT_SummaryDotgain.Series[7].Points.Clear();
                #endregion

                #region " === Fill Series === "
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
                            AddG7TVIPointChart(C, CHRT_SummaryDotgain.Series["Series1"], CHRT_SummaryDotgain.Series["Series1Point"], Convert.ToInt64(DT[i].CmykC), DT[i].DotGain, GetResourceString(_recCyan));
                            AddG7TVIPointChart(M, CHRT_SummaryDotgain.Series["Series2"], CHRT_SummaryDotgain.Series["Series2Point"], Convert.ToInt64(DT[i].CmykM), DT[i].DotGain, GetResourceString(_recMagenta));
                            AddG7TVIPointChart(Y, CHRT_SummaryDotgain.Series["Series3"], CHRT_SummaryDotgain.Series["Series3Point"], Convert.ToInt64(DT[i].CmykY), DT[i].DotGain, GetResourceString(_recYellow));
                            AddG7TVIPointChart(K, CHRT_SummaryDotgain.Series["Series4"], CHRT_SummaryDotgain.Series["Series4Point"], Convert.ToInt64(DT[i].CmykK), DT[i].DotGain, GetResourceString(_recBlack));
                            C++; M++; Y++; K++;
                        }
                        if (Convert.ToInt16(DT[i].CmykC) != 0)
                        {
                            AddG7TVIPointChart(C, CHRT_SummaryDotgain.Series["Series1"], CHRT_SummaryDotgain.Series["Series1Point"], Convert.ToInt64(DT[i].CmykC), DT[i].DotGain, GetResourceString(_recCyan));
                            C++;
                        }
                        if (Convert.ToInt16(DT[i].CmykM) != 0)
                        {
                            AddG7TVIPointChart(M, CHRT_SummaryDotgain.Series["Series2"], CHRT_SummaryDotgain.Series["Series2Point"], Convert.ToInt64(DT[i].CmykM), DT[i].DotGain, GetResourceString(_recMagenta));
                            M++;
                        }
                        if (Convert.ToInt16(DT[i].CmykY) != 0)
                        {
                            AddG7TVIPointChart(Y, CHRT_SummaryDotgain.Series["Series3"], CHRT_SummaryDotgain.Series["Series3Point"], Convert.ToInt64(DT[i].CmykY), DT[i].DotGain, GetResourceString(_recYellow));
                            Y++;
                        }
                        if (Convert.ToInt16(DT[i].CmykK) != 0)
                        {
                            AddG7TVIPointChart(K, CHRT_SummaryDotgain.Series["Series4"], CHRT_SummaryDotgain.Series["Series4Point"], Convert.ToInt64(DT[i].CmykK), DT[i].DotGain, GetResourceString(_recBlack));
                            K++;
                        }
                    }
                }

                FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHRT_SummaryDotgain.ImageLocation) + "." + CHRT_SummaryDotgain.ImageType;
                CHRT_SummaryDotgain.SaveImage(Server.MapPath(FileName));

                #endregion
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }

        private void FillISOTVIChart(List<AnalyzeChart_DotGain> DT1)
        {
            try
            {
                double ThresholdValue = 0;
                double ThresholdTop = 0;
                double ThresholdBottom = 0;
                if (DT1 != null && DT1.Count > 0)
                {
                    #region //========== Clear Points of all 4 charts==========//
                    CHRT_DotgainISOCyan.Series["Series0"].Points.Clear();
                    CHRT_DotgainISOCyan.Series["Series1"].Points.Clear();
                    CHRT_DotgainISOCyan.Series["SeriesTop"].Points.Clear();

                    CHRT_DotgainISOMeganta.Series["Series0"].Points.Clear();
                    CHRT_DotgainISOMeganta.Series["Series1"].Points.Clear();
                    CHRT_DotgainISOMeganta.Series["SeriesTop"].Points.Clear();

                    CHRT_DotgainISOYellow.Series["Series0"].Points.Clear();
                    CHRT_DotgainISOYellow.Series["Series1"].Points.Clear();
                    CHRT_DotgainISOYellow.Series["SeriesTop"].Points.Clear();

                    CHRT_DotgainISOBlack.Series["Series0"].Points.Clear();
                    CHRT_DotgainISOBlack.Series["Series1"].Points.Clear();
                    CHRT_DotgainISOBlack.Series["SeriesTop"].Points.Clear();
                    #endregion

                    string _TitleValue = string.Empty;

                    #region Cyan
                    //Filter DT1 = "(PatchTypeID in (1,8,16,17,18))";
                    List<AnalyzeChart_DotGain> ListCyan = new List<AnalyzeChart_DotGain>();
                    ListCyan = DT1.Where(x => (x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Paper) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_Quarter_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_Half_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_Three_Quarter_Tone)
                                              || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_10) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_20) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_25)
                                              || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_30) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_40) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_50)
                                              || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_60) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_70) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_75)
                                              || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_80) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Cyan_90))).OrderBy(item => item.CmykC).ToList();

                    if (ListCyan.Count > 0)
                    {
                        for (int i = 0; i < ListCyan.Count; i++)
                        {
                            //---Target
                            AddISOTVIPointChart(i, CHRT_DotgainISOCyan.Series["Series0"], ListCyan[i].CmykC, ListCyan[i].TargetDotGain, ListCyan[i].TargetDotGain, ListCyan[i].Threshold, ListCyan[i].PatchTypeId, ListCyan[i].HtmlColor.ToString(), GetResourceString(_recCyan), false);
                            //--Actual                          
                            AddISOTVIPointChart(i, CHRT_DotgainISOCyan.Series["Series1"], ListCyan[i].CmykC, ListCyan[i].DotGain, ListCyan[i].TargetDotGain, ListCyan[i].Threshold, ListCyan[i].PatchTypeId, ListCyan[i].HtmlColor.ToString(), GetResourceString(_recCyan), true);

                            if (Convert.ToInt16(ListCyan[i].Threshold) == 0)
                            {
                                ThresholdValue = 0;
                            }
                            else
                            {
                                ThresholdValue = Convert.ToDouble(ListCyan[i].Threshold);
                            }

                            ThresholdTop = Convert.ToDouble(ListCyan[i].TargetDotGain) + ThresholdValue;
                            ThresholdBottom = Convert.ToDouble(ListCyan[i].TargetDotGain) - ThresholdValue;

                            //CHRT_DotgainISOCyan.Series["SeriesTop"].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(ListCyan[i].CmykC.ToString())), Utilities.DecimalRound(ThresholdTop), Utilities.DecimalRound(ThresholdBottom));
                            if ((Convert.ToDouble(ListCyan[i].CmykC.ToString()) > 0 && Convert.ToDouble(ListCyan[i].CmykC.ToString()) < 100 && ThresholdValue > 0) || Convert.ToDouble(ListCyan[i].CmykC.ToString()) == 0 || Convert.ToDouble(ListCyan[i].CmykC.ToString()) == 100)//---If ThresholdValue is 0, then don't make entry on chart, because it will display screwy spread (Only if it is not starting 0 or ending 100 point patch)
                            {
                                CHRT_DotgainISOCyan.Series["SeriesTop"].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(ListCyan[i].CmykC.ToString())), Utilities.DecimalRound(ThresholdTop), Utilities.DecimalRound(ThresholdBottom));
                            }
                        }
                    }

                    FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHRT_DotgainISOCyan.ImageLocation) + "." + CHRT_DotgainISOCyan.ImageType;
                    CHRT_DotgainISOCyan.SaveImage(Server.MapPath(FileName));
                    #endregion

                    #region Magenta
                    ThresholdValue = 0;
                    ThresholdTop = 0;
                    ThresholdBottom = 0;

                    //Filter DT1 = "(PatchTypeID in (2,8,19,20,21))";
                    List<AnalyzeChart_DotGain> ListMagenta = new List<AnalyzeChart_DotGain>();
                    // ListMagenta = DT1.Where(x => (x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Paper) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_Quarter_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_Half_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_Three_Quarter_Tone))).OrderBy(item => item.CmykM).ToList();
                    ListMagenta = DT1.Where(x => (x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Paper) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_Quarter_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_Half_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_Three_Quarter_Tone)
                                               || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_10) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_20) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_25)
                                               || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_30) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_40) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_50)
                                               || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_60) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_70) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_75)
                                               || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_80) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Magenta_90))).OrderBy(item => item.CmykM).ToList();

                    if (ListMagenta.Count > 0)
                    {
                        for (int i = 0; i < ListMagenta.Count; i++)
                        {
                            //---Target
                            AddISOTVIPointChart(i, CHRT_DotgainISOMeganta.Series["Series0"], ListMagenta[i].CmykM, ListMagenta[i].TargetDotGain, ListMagenta[i].TargetDotGain, ListMagenta[i].Threshold, ListMagenta[i].PatchTypeId, ListMagenta[i].HtmlColor.ToString(), GetResourceString(_recMagenta), false);
                            //---Actual
                            AddISOTVIPointChart(i, CHRT_DotgainISOMeganta.Series["Series1"], ListMagenta[i].CmykM, ListMagenta[i].DotGain, ListMagenta[i].TargetDotGain, ListMagenta[i].Threshold, ListMagenta[i].PatchTypeId, ListMagenta[i].HtmlColor.ToString(), GetResourceString(_recMagenta), true);

                            if (Convert.ToInt16(ListMagenta[i].Threshold) == 0)
                            {
                                ThresholdValue = 0;
                            }
                            else
                            {
                                ThresholdValue = Convert.ToDouble(ListMagenta[i].Threshold);
                            }

                            ThresholdTop = Convert.ToDouble(ListMagenta[i].TargetDotGain) + ThresholdValue;
                            ThresholdBottom = Convert.ToDouble(ListMagenta[i].TargetDotGain) - ThresholdValue;

                            //CHRT_DotgainISOMeganta.Series["SeriesTop"].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(ListMagenta[i].CmykM.ToString())), Utilities.DecimalRound(ThresholdTop), Utilities.DecimalRound(ThresholdBottom));
                            if ((Convert.ToDouble(ListMagenta[i].CmykM.ToString()) > 0 && Convert.ToDouble(ListMagenta[i].CmykM.ToString()) < 100 && ThresholdValue > 0) || Convert.ToDouble(ListMagenta[i].CmykM.ToString()) == 0 || Convert.ToDouble(ListMagenta[i].CmykM.ToString()) == 100) //---If ThresholdValue is 0, then don't make entry on chart, because it will display screwy spread (Only if it is not starting 0 or ending 100 point patch)
                            {
                                CHRT_DotgainISOMeganta.Series["SeriesTop"].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(ListMagenta[i].CmykM.ToString())), Utilities.DecimalRound(ThresholdTop), Utilities.DecimalRound(ThresholdBottom));
                            }
                        }

                        FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHRT_DotgainISOMeganta.ImageLocation) + "." + CHRT_DotgainISOMeganta.ImageType;
                        CHRT_DotgainISOMeganta.SaveImage(Server.MapPath(FileName));
                    }
                    #endregion

                    #region Yellow
                    ThresholdValue = 0;
                    ThresholdTop = 0;
                    ThresholdBottom = 0;

                    //Filter DT1 = "(PatchTypeID in (3,8,22,23,24))";
                    List<AnalyzeChart_DotGain> ListYellow = new List<AnalyzeChart_DotGain>();
                    //ListYellow = DT1.Where(x => (x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Paper) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_Quarter_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_Half_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_Three_Quarter_Tone))).OrderBy(item => item.CmykY).ToList();
                    ListYellow = DT1.Where(x => (x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Paper) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_Quarter_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_Half_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_Three_Quarter_Tone)
                                                || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_10) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_20) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_25)
                                                || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_30) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_40) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_50)
                                                || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_60) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_70) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_75)
                                                || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_80) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Yellow_90))).OrderBy(item => item.CmykY).ToList();


                    if (ListYellow.Count > 0)
                    {
                        for (int i = 0; i < ListYellow.Count; i++)
                        {
                            //----Target
                            AddISOTVIPointChart(i, CHRT_DotgainISOYellow.Series["Series0"], ListYellow[i].CmykY, ListYellow[i].TargetDotGain, ListYellow[i].TargetDotGain, ListYellow[i].Threshold, ListYellow[i].PatchTypeId, ListYellow[i].HtmlColor.ToString(), GetResourceString(_recYellow), false);
                            //---Actual                         
                            AddISOTVIPointChart(i, CHRT_DotgainISOYellow.Series["Series1"], ListYellow[i].CmykY, ListYellow[i].DotGain, ListYellow[i].TargetDotGain, ListYellow[i].Threshold, ListYellow[i].PatchTypeId, ListYellow[i].HtmlColor.ToString(), GetResourceString(_recYellow), true);

                            if (Convert.ToInt16(ListYellow[i].Threshold) == 0)
                            {
                                ThresholdValue = 0;
                            }
                            else
                            {
                                ThresholdValue = Convert.ToDouble(ListYellow[i].Threshold);
                            }

                            ThresholdTop = Convert.ToDouble(ListYellow[i].TargetDotGain) + ThresholdValue;
                            ThresholdBottom = Convert.ToDouble(ListYellow[i].TargetDotGain) - ThresholdValue;

                            //CHRT_DotgainISOYellow.Series["SeriesTop"].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(ListYellow[i].CmykY.ToString())), Utilities.DecimalRound(ThresholdTop), Utilities.DecimalRound(ThresholdBottom));
                            if ((Convert.ToDouble(ListYellow[i].CmykY.ToString()) > 0 && Convert.ToDouble(ListYellow[i].CmykY.ToString()) < 100 && ThresholdValue > 0) || Convert.ToDouble(ListYellow[i].CmykY.ToString()) == 0 || Convert.ToDouble(ListYellow[i].CmykY.ToString()) == 100) //---If ThresholdValue is 0, then don't make entry on chart, because it will display screwy spread (Only if it is not starting 0 or ending 100 point patch)
                            {
                                CHRT_DotgainISOYellow.Series["SeriesTop"].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(ListYellow[i].CmykY.ToString())), Utilities.DecimalRound(ThresholdTop), Utilities.DecimalRound(ThresholdBottom));
                            }
                        }

                        FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHRT_DotgainISOYellow.ImageLocation) + "." + CHRT_DotgainISOYellow.ImageType;
                        CHRT_DotgainISOYellow.SaveImage(Server.MapPath(FileName));
                    }
                    #endregion

                    #region Black
                    ThresholdValue = 0;
                    ThresholdTop = 0;
                    ThresholdBottom = 0;

                    //Filter DT1 = "(PatchTypeID in (4,8,25,26,27))";
                    List<AnalyzeChart_DotGain> ListBlack = new List<AnalyzeChart_DotGain>();
                    //ListBlack = DT1.Where(x => (x.PatchTypeId == Convert.ToInt16(CPPatchType.Black) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Paper) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_Quarter_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_Half_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_Three_Quarter_Tone))).OrderBy(item => item.CmykK).ToList();
                    ListBlack = DT1.Where(x => (x.PatchTypeId == Convert.ToInt16(CPPatchType.Black) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Paper) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_Quarter_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_Half_Tone) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_Three_Quarter_Tone)
                                                || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_10) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_20) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_25)
                                                || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_30) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_40) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_50)
                                                || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_60) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_70) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_75)
                                                || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_80) || x.PatchTypeId == Convert.ToInt16(CPPatchType.Black_90))).OrderBy(item => item.CmykK).ToList();


                    if (ListBlack.Count > 0)
                    {
                        for (int i = 0; i < ListBlack.Count; i++)
                        {
                            //---Target
                            AddISOTVIPointChart(i, CHRT_DotgainISOBlack.Series["Series0"], ListBlack[i].CmykK, ListBlack[i].TargetDotGain, ListBlack[i].TargetDotGain, ListBlack[i].Threshold, ListBlack[i].PatchTypeId, ListBlack[i].HtmlColor.ToString(), GetResourceString(_recBlack), false);
                            //---Actual
                            AddISOTVIPointChart(i, CHRT_DotgainISOBlack.Series["Series1"], ListBlack[i].CmykK, ListBlack[i].DotGain, ListBlack[i].TargetDotGain, ListBlack[i].Threshold, ListBlack[i].PatchTypeId, ListBlack[i].HtmlColor.ToString(), GetResourceString(_recBlack), true);

                            if (Convert.ToInt16(ListBlack[i].Threshold) == 0)
                            {
                                ThresholdValue = 0;
                            }
                            else
                            {
                                ThresholdValue = Convert.ToDouble(ListBlack[i].Threshold);
                            }

                            ThresholdTop = Convert.ToDouble(ListBlack[i].TargetDotGain) + ThresholdValue;
                            ThresholdBottom = Convert.ToDouble(ListBlack[i].TargetDotGain) - ThresholdValue;

                            //CHRT_DotgainISOBlack.Series["SeriesTop"].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(ListBlack[i].CmykK.ToString())), Utilities.DecimalRound(ThresholdTop), Utilities.DecimalRound(ThresholdBottom));
                            if ((Convert.ToDouble(ListBlack[i].CmykK.ToString()) > 0 && Convert.ToDouble(ListBlack[i].CmykK.ToString()) < 100 && ThresholdValue > 0) || Convert.ToDouble(ListBlack[i].CmykK.ToString()) == 0 || Convert.ToDouble(ListBlack[i].CmykK.ToString()) == 100) //---If ThresholdValue is 0, then don't make entry on chart, because it will display screwy spread (Only if it is not starting 0 or ending 100 point patch)
                            {
                                CHRT_DotgainISOBlack.Series["SeriesTop"].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(ListBlack[i].CmykK.ToString())), Utilities.DecimalRound(ThresholdTop), Utilities.DecimalRound(ThresholdBottom));
                            }
                        }

                        FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHRT_DotgainISOBlack.ImageLocation) + "." + CHRT_DotgainISOBlack.ImageType;
                        CHRT_DotgainISOBlack.SaveImage(Server.MapPath(FileName));
                    }
                    #endregion
                }
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }

        private void FillSpreadChart(List<AnalyzeChart_DotGain> DTSpread, List<AnalyzeChart_DotGain> DTSpreadSplineRange, DataTable DTMaxSpreasTable)
        {
            try
            {
                #region //========== Clear Points==========//
                CHRT_SummaryISODotgain.Series["Series1"].Points.Clear();
                CHRT_SummaryISODotgain.Series["Series2"].Points.Clear();
                CHRT_SummaryISODotgain.Series["Series3"].Points.Clear();

                CHRT_SummaryISODotgain.Series["Series1Point"].Points.Clear();
                CHRT_SummaryISODotgain.Series["Series2Point"].Points.Clear();
                CHRT_SummaryISODotgain.Series["Series3Point"].Points.Clear();
                CHRT_SummaryISODotgain.Series["SeriesTop"].Points.Clear();
                #endregion

                #region Dotgain Spread Chart
                if (DTSpread != null && DTSpread.Count > 0)
                {
                    int C = 0;
                    int M = 0;
                    int Y = 0;
                    Int16 CyanValue = 0;
                    Int16 MagentaValue = 0;
                    Int16 YellowValue = 0;
                    Int16 DiffrentOne = 0;
                    Int16 DiffrentTwo = 0;
                    String DiffrentTitleOne = string.Empty;
                    String DiffrentTitleTwo = string.Empty;
                    AnalyzeChart_DotGain FilterSpread = new AnalyzeChart_DotGain();

                    for (int i = 0; i < DTSpread.Count; i++)
                    {
                        if (Convert.ToInt16(DTSpread[i].CmykC) == 0 && Convert.ToInt16(DTSpread[i].CmykM) == 0 && Convert.ToInt16(DTSpread[i].CmykY) == 0)
                        {
                            if (ChkC.Checked)
                            {
                                AddTVISpreadPointChart(C, CHRT_SummaryISODotgain.Series["Series1"], CHRT_SummaryISODotgain.Series["Series1Point"], Convert.ToInt64(DTSpread[i].CmykC), DTSpread[i].DotGain, "Cyan", GetResourceString(_recCyan), DTSpread[i].DotGainSpreadTh, "", "", "", "");
                            }

                            if (ChkM.Checked)
                            {
                                AddTVISpreadPointChart(M, CHRT_SummaryISODotgain.Series["Series2"], CHRT_SummaryISODotgain.Series["Series2Point"], Convert.ToInt64(DTSpread[i].CmykM), DTSpread[i].DotGain, "Magenta", GetResourceString(_recMagenta), DTSpread[i].DotGainSpreadTh, "", "", "", "");
                            }

                            if (ChkY.Checked)
                            {
                                AddTVISpreadPointChart(Y, CHRT_SummaryISODotgain.Series["Series3"], CHRT_SummaryISODotgain.Series["Series3Point"], Convert.ToInt64(DTSpread[i].CmykY), DTSpread[i].DotGain, "Yellow", GetResourceString(_recYellow), DTSpread[i].DotGainSpreadTh, "", "", "", "");
                            }

                            C++; M++; Y++;

                        }
                        if (Convert.ToInt16(DTSpread[i].CmykC) != 0 && ChkC.Checked)
                        {
                            FilterSpread = DTSpread.Find(item => item.CmykM == DTSpread[i].CmykC);
                            MagentaValue = Convert.ToInt16(FilterSpread == null ? 0 : FilterSpread.DotGain);

                            FilterSpread = DTSpread.Find(item => item.CmykY == DTSpread[i].CmykC);
                            YellowValue = Convert.ToInt16(FilterSpread == null ? 0 : FilterSpread.DotGain);

                            DiffrentOne = Convert.ToInt16(Convert.ToInt16(DTSpread[i].DotGain) - MagentaValue);
                            DiffrentTwo = Convert.ToInt16(Convert.ToInt16(DTSpread[i].DotGain) - YellowValue);

                            DiffrentTitleOne = "C - M";
                            DiffrentTitleTwo = "C - Y";

                            AddTVISpreadPointChart(C, CHRT_SummaryISODotgain.Series["Series1"], CHRT_SummaryISODotgain.Series["Series1Point"], Convert.ToInt64(DTSpread[i].CmykC), DTSpread[i].DotGain, "Cyan", GetResourceString(_recCyan), DTSpread[i].DotGainSpreadTh, DiffrentTitleOne, DiffrentTitleTwo, Math.Abs(DiffrentOne).ToString(), Math.Abs(DiffrentTwo).ToString());
                            C++;
                        }
                        if (Convert.ToInt16(DTSpread[i].CmykM) != 0 && ChkM.Checked)
                        {
                            FilterSpread = DTSpread.Find(item => item.CmykC == DTSpread[i].CmykM);
                            CyanValue = Convert.ToInt16(FilterSpread == null ? 0 : FilterSpread.DotGain);

                            FilterSpread = DTSpread.Find(item => item.CmykY == DTSpread[i].CmykM);
                            YellowValue = Convert.ToInt16(FilterSpread == null ? 0 : FilterSpread.DotGain);

                            DiffrentOne = Convert.ToInt16(Convert.ToInt16(DTSpread[i].DotGain) - CyanValue);
                            DiffrentTwo = Convert.ToInt16(Convert.ToInt16(DTSpread[i].DotGain) - YellowValue);

                            DiffrentTitleOne = "M - C";
                            DiffrentTitleTwo = "M - Y";

                            AddTVISpreadPointChart(M, CHRT_SummaryISODotgain.Series["Series2"], CHRT_SummaryISODotgain.Series["Series2Point"], Convert.ToInt64(DTSpread[i].CmykM), DTSpread[i].DotGain, "Magenta", GetResourceString(_recMagenta), DTSpread[i].DotGainSpreadTh, DiffrentTitleOne, DiffrentTitleTwo, Math.Abs(DiffrentOne).ToString(), Math.Abs(DiffrentTwo).ToString());
                            M++;
                        }
                        if (Convert.ToInt16(DTSpread[i].CmykY) != 0 && ChkY.Checked)
                        {
                            FilterSpread = DTSpread.Find(item => item.CmykC == DTSpread[i].CmykY);
                            CyanValue = Convert.ToInt16(FilterSpread == null ? 0 : FilterSpread.DotGain);

                            FilterSpread = DTSpread.Find(item => item.CmykM == DTSpread[i].CmykY);
                            MagentaValue = Convert.ToInt16(FilterSpread == null ? 0 : FilterSpread.DotGain);

                            DiffrentOne = Convert.ToInt16(Convert.ToInt16(DTSpread[i].DotGain) - CyanValue);
                            DiffrentTwo = Convert.ToInt16(Convert.ToInt16(DTSpread[i].DotGain) - MagentaValue);

                            DiffrentTitleOne = "Y - C";
                            DiffrentTitleTwo = "Y - M";

                            AddTVISpreadPointChart(Y, CHRT_SummaryISODotgain.Series["Series3"], CHRT_SummaryISODotgain.Series["Series3Point"], Convert.ToInt64(DTSpread[i].CmykY), DTSpread[i].DotGain, "Yellow", GetResourceString(_recYellow), DTSpread[i].DotGainSpreadTh, DiffrentTitleOne, DiffrentTitleTwo, Math.Abs(DiffrentOne).ToString(), Math.Abs(DiffrentTwo).ToString());
                            Y++;
                        }
                    }
                }
                #endregion

                #region Spread Spline Range (green boundries)
                if (DTSpreadSplineRange != null && DTSpreadSplineRange.Count > 0)
                {
                    // CHRT_SummaryISODotgain.Series["SeriesTop"].Points.AddXY(Utilities.DecimalRound(0), Utilities.DecimalRound(0), Utilities.DecimalRound(0));
                    for (int i = 0; i < DTSpreadSplineRange.Count; i++)
                    {
                        if (DTSpreadSplineRange[i].UpperLimit != 0 && DTSpreadSplineRange[i].LowerLimit != 0) //When chart has 20-40-60-80 patches then dont display 25-50-75 patches and vice versa
                        {
                            CHRT_SummaryISODotgain.Series["SeriesTop"].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DTSpreadSplineRange[i].TVI.ToString())), Utilities.DecimalRound(Convert.ToDouble(DTSpreadSplineRange[i].UpperLimit.ToString())), Utilities.DecimalRound(Convert.ToDouble(DTSpreadSplineRange[i].LowerLimit.ToString())));
                        }
                    }
                    // CHRT_SummaryISODotgain.Series["SeriesTop"].Points.AddXY(Utilities.DecimalRound(100), Utilities.DecimalRound(0), Utilities.DecimalRound(0));
                }
                #endregion

                #region Max Spread Table
                if (DTMaxSpreasTable != null && DTMaxSpreasTable.Rows.Count > 0)
                {
                    rgChromVariation.DataSource = DTMaxSpreasTable;
                    rgChromVariation.DataBind();
                }

                #endregion

                FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHRT_SummaryISODotgain.ImageLocation) + "." + CHRT_SummaryISODotgain.ImageType;
                CHRT_SummaryISODotgain.SaveImage(Server.MapPath(FileName));
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }

        private void FillSpreadChart_CMYK(List<AnalyzeChart_DotGain> DTSpread)
        {
            try
            {
                #region //========== Clear Points==========//
                CHRT_SummaryISODotgain_CMYK.Series["Series1"].Points.Clear();
                CHRT_SummaryISODotgain_CMYK.Series["Series2"].Points.Clear();
                CHRT_SummaryISODotgain_CMYK.Series["Series3"].Points.Clear();
                CHRT_SummaryISODotgain_CMYK.Series["Series4"].Points.Clear();

                CHRT_SummaryISODotgain_CMYK.Series["Series1Point"].Points.Clear();
                CHRT_SummaryISODotgain_CMYK.Series["Series2Point"].Points.Clear();
                CHRT_SummaryISODotgain_CMYK.Series["Series3Point"].Points.Clear();
                CHRT_SummaryISODotgain_CMYK.Series["Series4Point"].Points.Clear();

                #endregion

                #region Dotgain Spread CMYK Chart
                if (DTSpread != null && DTSpread.Count > 0)
                {
                    int C = 0;
                    int M = 0;
                    int Y = 0;
                    int K = 0;
                    AnalyzeChart_DotGain FilterSpread = new AnalyzeChart_DotGain();

                    for (int i = 0; i < DTSpread.Count; i++)
                    {
                        if (Convert.ToInt16(DTSpread[i].CmykC) == 0 && Convert.ToInt16(DTSpread[i].CmykM) == 0 && Convert.ToInt16(DTSpread[i].CmykY) == 0 && Convert.ToInt16(DTSpread[i].CmykK) == 0)
                        {
                            if (ChkCMYK_C.Checked)
                            {
                                AddTVISpreadPointChart(C, CHRT_SummaryISODotgain_CMYK.Series["Series1"], CHRT_SummaryISODotgain_CMYK.Series["Series1Point"], Convert.ToInt64(DTSpread[i].CmykC), DTSpread[i].DotGain, "Cyan", GetResourceString(_recCyan), DTSpread[i].DotGainSpreadTh, "", "", "", "", "CMYK");
                            }

                            if (ChkCMYK_M.Checked)
                            {
                                AddTVISpreadPointChart(M, CHRT_SummaryISODotgain_CMYK.Series["Series2"], CHRT_SummaryISODotgain_CMYK.Series["Series2Point"], Convert.ToInt64(DTSpread[i].CmykM), DTSpread[i].DotGain, "Magenta", GetResourceString(_recMagenta), DTSpread[i].DotGainSpreadTh, "", "", "", "", "CMYK");
                            }

                            if (ChkCMYK_Y.Checked)
                            {
                                AddTVISpreadPointChart(Y, CHRT_SummaryISODotgain_CMYK.Series["Series3"], CHRT_SummaryISODotgain_CMYK.Series["Series3Point"], Convert.ToInt64(DTSpread[i].CmykY), DTSpread[i].DotGain, "Yellow", GetResourceString(_recYellow), DTSpread[i].DotGainSpreadTh, "", "", "", "", "CMYK");
                            }

                            if (ChkCMYK_K.Checked)
                            {
                                AddTVISpreadPointChart(K, CHRT_SummaryISODotgain_CMYK.Series["Series4"], CHRT_SummaryISODotgain_CMYK.Series["Series4Point"], Convert.ToInt64(DTSpread[i].CmykK), DTSpread[i].DotGain, "Black", GetResourceString(_recBlack), DTSpread[i].DotGainSpreadTh, "", "", "", "", "CMYK");
                            }

                            C++; M++; Y++; K++;

                        }
                        if (Convert.ToInt16(DTSpread[i].CmykC) != 0 && ChkCMYK_C.Checked)
                        {
                            AddTVISpreadPointChart(C, CHRT_SummaryISODotgain_CMYK.Series["Series1"], CHRT_SummaryISODotgain_CMYK.Series["Series1Point"], Convert.ToInt64(DTSpread[i].CmykC), DTSpread[i].DotGain, "Cyan", GetResourceString(_recCyan), DTSpread[i].DotGainSpreadTh, "", "", "", "", "CMYK");
                            C++;
                        }
                        if (Convert.ToInt16(DTSpread[i].CmykM) != 0 && ChkCMYK_M.Checked)
                        {
                            AddTVISpreadPointChart(M, CHRT_SummaryISODotgain_CMYK.Series["Series2"], CHRT_SummaryISODotgain_CMYK.Series["Series2Point"], Convert.ToInt64(DTSpread[i].CmykM), DTSpread[i].DotGain, "Magenta", GetResourceString(_recMagenta), DTSpread[i].DotGainSpreadTh, "", "", "", "", "CMYK");
                            M++;
                        }
                        if (Convert.ToInt16(DTSpread[i].CmykY) != 0 && ChkCMYK_Y.Checked)
                        {
                            AddTVISpreadPointChart(Y, CHRT_SummaryISODotgain_CMYK.Series["Series3"], CHRT_SummaryISODotgain_CMYK.Series["Series3Point"], Convert.ToInt64(DTSpread[i].CmykY), DTSpread[i].DotGain, "Yellow", GetResourceString(_recYellow), DTSpread[i].DotGainSpreadTh, "", "", "", "", "CMYK");
                            Y++;
                        }
                        if (Convert.ToInt16(DTSpread[i].CmykK) != 0 && ChkCMYK_K.Checked)
                        {
                            AddTVISpreadPointChart(K, CHRT_SummaryISODotgain_CMYK.Series["Series4"], CHRT_SummaryISODotgain_CMYK.Series["Series4Point"], Convert.ToInt64(DTSpread[i].CmykK), DTSpread[i].DotGain, "Black", GetResourceString(_recBlack), DTSpread[i].DotGainSpreadTh, "", "", "", "", "CMYK");
                            K++;
                        }
                    }
                }
                #endregion


                FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHRT_SummaryISODotgain_CMYK.ImageLocation) + "." + CHRT_SummaryISODotgain_CMYK.ImageType;
                CHRT_SummaryISODotgain_CMYK.SaveImage(Server.MapPath(FileName));
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }


        //Add Point with color
        private void AddG7TVIPointChart(int _Index, Series _objSeries, Series _objSeriesPoint, long _XValue, decimal _YValue, string ColorName)
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
                    toolTip.Append("<tr><td><b>" + ColorName + " " + _XValue + GetResourceString(_recpercentPatchMetrics) + "</b></td></tr>");
                    toolTip.Append("<tr><td><table>");
                    toolTip.Append("<tr><td>" + GetResourceString(_recDotGain) + ": </td><td>");
                    toolTip.Append(_YValue);
                    toolTip.Append("</td></tr>");
                    toolTip.Append("</table></td></tr>");
                    toolTip.Append("</table>");
                    #endregion

                    _objSeriesPoint.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void AddTVISpreadPointChart(int _Index, Series _objSeries, Series _objSeriesPoint, long _XValue, decimal _YValue, string ColorName, string ToolltipColor, decimal _TVISpreadTh, string DiffrentTitleOne, string DiffrentTitleTwo, string DiffrentOne, string DiffrentTwo, string ComingFrom = "")
        {
            try
            {
                _objSeries.ChartType = SeriesChartType.Spline;
                _objSeries.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
                //_objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;

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
                    toolTip.Append("<tr><td><b>" + ToolltipColor + " " + _XValue + GetResourceString(_recpercentPatchMetrics) + "</b></td></tr>");
                    toolTip.Append("<tr><td><table>");
                    toolTip.Append("<tr><td>" + GetResourceString(_recactualTVI) + "</td><td>");
                    toolTip.Append(_YValue);
                    toolTip.Append("</td></tr>");
                    if (ComingFrom != "CMYK")
                    {
                        toolTip.Append("<tr><td>" + GetResourceString(_recspreadThr) + "</td><td>");
                        toolTip.Append(_TVISpreadTh);
                        toolTip.Append("</td></tr>");
                        toolTip.Append("<tr><td>" + DiffrentTitleOne + ":</td><td>");
                        toolTip.Append(DiffrentOne);
                        toolTip.Append("</td></tr>");
                        toolTip.Append("<tr><td>" + DiffrentTitleTwo + ":</td><td>");
                        toolTip.Append(DiffrentTwo);
                        toolTip.Append("</td></tr>");
                    }
                    toolTip.Append("</table></td></tr>");
                    toolTip.Append("</table>");
                    #endregion

                    _objSeriesPoint.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void AddISOTVIPointChart(int _Index, Series _objSeries, decimal _XValue, decimal _YValue, decimal _Target, decimal _Threshold, short _PatchTypeId, string HTMLColor, string ColorName, bool isTooltip)
        {
            try
            {
                _objSeries.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));

                _objSeries.ChartType = SeriesChartType.Spline;
                _objSeries.Points[_Index].MarkerColor = System.Drawing.Color.Green;
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
                _objSeries.Points[_Index].MarkerSize = 8;

                _objSeries.Points[_Index].BorderWidth = 1;

                if (isTooltip && Utilities.DecimalRound(Convert.ToDouble(_YValue)) > 0)
                {
                    _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(HTMLColor); //--Target Values Plotting with its original patch color

                    #region ToolTio Title
                    string _TitleValue = "";
                    if ((_PatchTypeId == Convert.ToInt32(CPPatchType.Cyan_Quarter_Tone)) || (_PatchTypeId == Convert.ToInt32(CPPatchType.Magenta_Quarter_Tone)) || (_PatchTypeId == Convert.ToInt32(CPPatchType.Yellow_Quarter_Tone)) || (_PatchTypeId == Convert.ToInt16(CPPatchType.Black_Quarter_Tone)))
                    {
                        _TitleValue = "25%";
                    }
                    else if ((_PatchTypeId == Convert.ToInt32(CPPatchType.Cyan_Half_Tone)) || (_PatchTypeId == Convert.ToInt32(CPPatchType.Magenta_Half_Tone)) || (_PatchTypeId == Convert.ToInt32(CPPatchType.Yellow_Half_Tone)) || (_PatchTypeId == Convert.ToInt16(CPPatchType.Black_Half_Tone)))
                    {
                        _TitleValue = "50%";
                    }
                    else if ((_PatchTypeId == Convert.ToInt32(CPPatchType.Cyan_Three_Quarter_Tone)) || (_PatchTypeId == Convert.ToInt32(CPPatchType.Magenta_Three_Quarter_Tone)) || (_PatchTypeId == Convert.ToInt32(CPPatchType.Yellow_Three_Quarter_Tone)) || (_PatchTypeId == Convert.ToInt16(CPPatchType.Black_Three_Quarter_Tone)))
                    {
                        _TitleValue = "75%";
                    }
                    #endregion

                    #region Add Tooltip HTML
                    StringBuilder toolTip = new StringBuilder();
                    toolTip.Append("<table>");
                    toolTip.Append("<tr><td><b>" + ColorName + " " + _TitleValue + GetResourceString(_recPatchMetrics) + "</b></td></tr>");
                    toolTip.Append("<tr><td><table>");
                    toolTip.Append("<tr><td>" + GetResourceString(_recactualTVI) + "</td><td>");
                    toolTip.Append(_YValue + "%");
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>" + GetResourceString(_rectargetTVI) + "</td><td>");
                    toolTip.Append(_Target + "%");
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>" + GetResourceString(_recTVIThr) + "</td><td>");
                    toolTip.Append(_Threshold + "%");
                    toolTip.Append("</td></tr>");
                    toolTip.Append("</table></td></tr>");
                    toolTip.Append("</table>");
                    #endregion

                    _objSeries.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                }
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
            else if (MetricSumID == "8" || MetricSumID == "9" || MetricSumID == "36" || MetricSumID == "37")    //----For 'G7 Gray ΔCh Avg' or ----For 'G7 Gray ΔCh Max' or ----For 'wG7 Gray ΔCh Avg'  or ---_For 'wG7 Gray ΔCh Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 9 || r.Field<byte>("PatchTypeID") == 10 || r.Field<byte>("PatchTypeID") == 28 || r.Field<byte>("PatchTypeID") == 29 || r.Field<byte>("PatchTypeID") == 30 || r.Field<byte>("PatchTypeID") == 31 || r.Field<byte>("PatchTypeID") == 32 || r.Field<byte>("PatchTypeID") == 33 || r.Field<byte>("PatchTypeID") == 34 || r.Field<byte>("PatchTypeID") == 35 || r.Field<byte>("PatchTypeID") == 36 || r.Field<byte>("PatchTypeID") == 37 || r.Field<byte>("PatchTypeID") == 38 || r.Field<byte>("PatchTypeID") == 39 || r.Field<byte>("PatchTypeID") == 40 || r.Field<byte>("PatchTypeID") == 41 || r.Field<byte>("PatchTypeID") == 42 || r.Field<byte>("PatchTypeID") == 43 || r.Field<byte>("PatchTypeID") == 44 || r.Field<byte>("PatchTypeID") == 45 || r.Field<byte>("PatchTypeID") == 46 || r.Field<byte>("PatchTypeID") == 47 || r.Field<byte>("PatchTypeID") == 48 || r.Field<byte>("PatchTypeID") == 49 || r.Field<byte>("PatchTypeID") == 50)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "10" || MetricSumID == "11" || MetricSumID == "38" || MetricSumID == "39")   //----For 'G7 Black ΔL Avg' or ----For 'G7 Black ΔL Max' or ----For 'wG7 Black ΔL Avg' or ----For 'wG7 Black ΔL Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 4 || r.Field<byte>("PatchTypeID") == 15 || r.Field<byte>("PatchTypeID") == 25 || r.Field<byte>("PatchTypeID") == 26 || r.Field<byte>("PatchTypeID") == 27)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "12" || MetricSumID == "13" || MetricSumID == "40" || MetricSumID == "41")    //----For 'G7 CMY / Gray ΔL Avg' or ----For 'G7 CMY / Gray ΔL Max' or ----For 'wG7 CMY / Gray ΔL Avg' or ----For 'wG7 CMY / Gray ΔL Max'
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
            else if (MetricSumID == "15" || MetricSumID == "18")    //----For 'ΔTVI25', 'ΔTVI25Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 16 || r.Field<byte>("PatchTypeID") == 19 || r.Field<byte>("PatchTypeID") == 22 || r.Field<byte>("PatchTypeID") == 25)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "16" || MetricSumID == "19")    //----For 'ΔTVI50', 'ΔTVI50Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 17 || r.Field<byte>("PatchTypeID") == 20 || r.Field<byte>("PatchTypeID") == 23 || r.Field<byte>("PatchTypeID") == 26)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "17" || MetricSumID == "20")    //----For 'ΔTVI75', 'ΔTVI75Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 18 || r.Field<byte>("PatchTypeID") == 21 || r.Field<byte>("PatchTypeID") == 24 || r.Field<byte>("PatchTypeID") == 27)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "21" || MetricSumID == "22" || MetricSumID == "23")    //----For 'Paper ΔL', 'Paper Δa', 'Paper Δb'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 8)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "24")    //----For 'CMY100 ΔE Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 9)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "25")    //----For 'Japan Gray ΔH Avg'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 61 || r.Field<byte>("PatchTypeID") == 62 || r.Field<byte>("PatchTypeID") == 63 || r.Field<byte>("PatchTypeID") == 64 || r.Field<byte>("PatchTypeID") == 65)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "26")    //----For 'G7 Gray ΔH Avg'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 8 || r.Field<byte>("PatchTypeID") == 9 || r.Field<byte>("PatchTypeID") == 28 || r.Field<byte>("PatchTypeID") == 29 || r.Field<byte>("PatchTypeID") == 30 || r.Field<byte>("PatchTypeID") == 31 || r.Field<byte>("PatchTypeID") == 32 || r.Field<byte>("PatchTypeID") == 33 || r.Field<byte>("PatchTypeID") == 34 || r.Field<byte>("PatchTypeID") == 35 || r.Field<byte>("PatchTypeID") == 36 || r.Field<byte>("PatchTypeID") == 37 || r.Field<byte>("PatchTypeID") == 38 || r.Field<byte>("PatchTypeID") == 39 || r.Field<byte>("PatchTypeID") == 40 || r.Field<byte>("PatchTypeID") == 41 || r.Field<byte>("PatchTypeID") == 42 || r.Field<byte>("PatchTypeID") == 43 || r.Field<byte>("PatchTypeID") == 44 || r.Field<byte>("PatchTypeID") == 45 || r.Field<byte>("PatchTypeID") == 46 || r.Field<byte>("PatchTypeID") == 47 || r.Field<byte>("PatchTypeID") == 48 || r.Field<byte>("PatchTypeID") == 49 || r.Field<byte>("PatchTypeID") == 50)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "27")    //----For 'ΔE Max No Paper'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") != 8)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "28" || MetricSumID == "32")    //----For 'ΔTVI20', 'ΔTVI20Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 113 || r.Field<byte>("PatchTypeID") == 124 || r.Field<byte>("PatchTypeID") == 135 || r.Field<byte>("PatchTypeID") == 146)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "29" || MetricSumID == "33")    //----For 'ΔTVI40', 'ΔTVI40Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 116 || r.Field<byte>("PatchTypeID") == 127 || r.Field<byte>("PatchTypeID") == 138 || r.Field<byte>("PatchTypeID") == 149)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "30" || MetricSumID == "34")    //----For 'ΔTVI60', 'ΔTVI60Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 118 || r.Field<byte>("PatchTypeID") == 129 || r.Field<byte>("PatchTypeID") == 140 || r.Field<byte>("PatchTypeID") == 151)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "31" || MetricSumID == "35")    //----For 'ΔTVI80', 'ΔTVI80Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 121 || r.Field<byte>("PatchTypeID") == 132 || r.Field<byte>("PatchTypeID") == 143 || r.Field<byte>("PatchTypeID") == 154)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "48")    //----For 'Fogra Gray ΔCh Avg'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 9 || r.Field<byte>("PatchTypeID") == 52 || r.Field<byte>("PatchTypeID") == 53 || r.Field<byte>("PatchTypeID") == 54 || r.Field<byte>("PatchTypeID") == 55 || r.Field<byte>("PatchTypeID") == 56 || r.Field<byte>("PatchTypeID") == 57 || r.Field<byte>("PatchTypeID") == 58 || r.Field<byte>("PatchTypeID") == 59 || r.Field<byte>("PatchTypeID") == 60)
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
            MeasurementId = 0;
            if (hdMeasurement.Value != "")
            {
                MeasurementId = Convert.ToInt32(hdMeasurement.Value);  //----Pass selected measurement id from top analyze page trendline chart
            }
            //-----Give name to RadGrid Exported file according to selected measurement-------//
            rgPatchDetails.ExportSettings.FileName = MeasurementId.ToString() + "_MeasurementFile";
        }

        #endregion

        #region Paging for top Trendline Chart
        protected void DDL_PageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //========== Alter the page size ==========//
            LTRL_PageSize.Text = DDL_PageSize.SelectedValue;

            int _PageSize = Convert.ToInt32(LTRL_PageSize.Text);
            //Modified for localization
            //int _PageCount = Convert.ToInt32(LTRL_TotalRec.Text) / _PageSize; if ((Convert.ToInt32(LTRL_TotalRec.Text) % _PageSize) > 0) { _PageCount = _PageCount + 1; }
            int _PageCount = Convert.ToInt32(hdnTotalRecord.Value) / _PageSize; if ((Convert.ToInt32(hdnTotalRecord.Value) % _PageSize) > 0) { _PageCount = _PageCount + 1; }

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
            //int _ReminderVal = _PageSize - (MeasurementList.Count % _PageSize);
            int _ReminderVal = (MeasurementList.Count % _PageSize);
            int _EndIndex = 0;
            #endregion

            //========== Filling the DropDown ==========//
            if (Page.IsPostBack == false)
            {
                FillDDLGoToPage(_PageCount);
                //Modified for localization
                //LTRL_TotalRec.Text = MeasurementList.Count.ToString();
                ltrlRecordsSummary.Text = GetResourceString("ltrlRecordsSummaryResource1.Text");
                ltrlRecordsSummary.Text = ltrlRecordsSummary.Text.Replace("{start}", "0");
                ltrlRecordsSummary.Text = ltrlRecordsSummary.Text.Replace("{end}", "0");
                ltrlRecordsSummary.Text = ltrlRecordsSummary.Text.Replace("{total}", MeasurementList.Count.ToString());
                hdnTotalRecord.Value = MeasurementList.Count.ToString();
            }

            #region Settings for Paging
            _CurrentPageIndex = Convert.ToInt32(DDL_GoToPage.SelectedValue);

            //---Changed the code to display highest to last page and rest reminder to the first page in trendline chart---//
            if (_ReminderVal > 0)
            {
                _EndIndex = ((_CurrentPageIndex * _PageSize) + _ReminderVal);
            }
            else   //--If no ReminderValue then add Index to 1 more in order to get right number of endpoint like in ex., Pagesize = 5 case when total no. of Measurements = 20---//
            {
                _EndIndex = (((_CurrentPageIndex + 1) * _PageSize) + _ReminderVal);
            }
            if (_CurrentPageIndex == 0)
            {
                _StartIndex = 0;
            }
            else
            {
                _StartIndex = _EndIndex - _PageSize;
            }

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
            ltrlRecordsSummary.Text = GetResourceString("ltrlRecordsSummaryResource1.Text");
            ltrlRecordsSummary.Text = ltrlRecordsSummary.Text.Replace("{start}", Convert.ToString(_StartIndex + 1));
            ltrlRecordsSummary.Text = ltrlRecordsSummary.Text.Replace("{end}", _EndIndex.ToString());
            ltrlRecordsSummary.Text = ltrlRecordsSummary.Text.Replace("{total}", MeasurementList.Count.ToString());
            hdnTotalRecord.Value = MeasurementList.Count.ToString();


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

        #region Print Label Method
        private void PreparePrintLabelXML(Entity.Measurement measurmentSummary)
        {
            //Open label template from XML(.label) file            
            StreamReader streamReader = new StreamReader(Server.MapPath(@"Templates\CertifiedLabel.label"));
            //StreamReader streamReader = new StreamReader(@"D:\Chirag.Vidani\Work\FujiFilm\Dymo\Labels\CertifiedLabel.label");
            string labelXML = streamReader.ReadToEnd();
            streamReader.Close();
            //string labelXML = "XML";

            //Set the localization values for label caption to placeholder
            labelXML = labelXML.Replace("locJob", GetResourceString(_reclocLabelJob));
            labelXML = labelXML.Replace("locDevice", GetResourceString(_reclocLabelDevice));
            labelXML = labelXML.Replace("locMetric", GetResourceString(_reclocLabelMetric));
            labelXML = labelXML.Replace("locStandard", GetResourceString(_reclocLabelStandard));
            labelXML = labelXML.Replace("locTimestamp", GetResourceString(_reclocLabelTimestamp));
            labelXML = labelXML.Replace("locCustomer", GetResourceString(_reclocLabelCustomer));
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
            {
                labelXML = labelXML.Replace("locPageOrSurface", "");
                labelXML = labelXML.Replace("locJDFJobID", "");
                labelXML = labelXML.Replace("locJob", GetResourceString("locMeasurementNameResource1.Text"));
            }
            else
            {
                labelXML = labelXML.Replace("locPageOrSurface", GetResourceString(_reclocLabelPageOrSurface));
                labelXML = labelXML.Replace("locJDFJobID", GetResourceString(_reclocLabelJDFJobID));
                labelXML = labelXML.Replace("locJob", GetResourceString(_reclocLabelJob));
            }
            labelXML = labelXML.Replace("locdEType", GetResourceString(_reclocLabeldEType));
            labelXML = labelXML.Replace("locTol", GetResourceString(_reclocLabelTol));
            labelXML = labelXML.Replace("locAct", GetResourceString(_reclocLabelAct));
            labelXML = labelXML.Replace("locSignature", GetResourceString(_reclocLabelSignature));
            labelXML = labelXML.Replace("locDate", GetResourceString(_reclocLabelDate));

            //Replace 5 sensitive characters of XML before passing them
            Regex badAmpersand = new Regex("&(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
            Regex badLessThan = new Regex("<(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
            Regex badGreaterThan = new Regex(">(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
            Regex badDoubleQuotation = new Regex("\"(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
            Regex badapostrophes = new Regex("'(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
            const string goodAmpersand = "&amp;";
            const string goodLessThan = "&lt;";
            const string goodGreaterThan = "&gt;";
            const string goodDoubleQuotation = "&quot;";
            const string goodapostrophes = "&apos;";

            //Set the values to Placeholder
            labelXML = labelXML.Replace("[Job]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(measurmentSummary.MeasurementName, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if Measurement name contains "&" letter or rest of 4 letters
            labelXML = labelXML.Replace("[Device]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(SessionManager.Instance.CurrentDevice.DeviceName, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if Device name contains "&" letter or rest of 4 letters
            labelXML = labelXML.Replace("[Metric]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(measurmentSummary.MetricTargetPresetName, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if MTP name contains "&" letter or rest of 4 letters
            labelXML = labelXML.Replace("[PrintStandard]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(measurmentSummary.PrintStandardType, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if PrintStandardType name contains "&" letter or rest of 4 letters
            labelXML = labelXML.Replace("[Timestamp]", measurmentSummary.MeasurementDate.ToString("g"));
            labelXML = labelXML.Replace("[Customer]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(measurmentSummary.Customer, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if Customer name contains "&" letter or rest of 4 letters
            labelXML = labelXML.Replace("[dEType]", measurmentSummary.dEMethod);
            labelXML = labelXML.Replace("[PageOrSurface]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(measurmentSummary.PageOrSurface, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if PageOrSurface contains "&" letter or rest of 4 letters
            labelXML = labelXML.Replace("[JDFJobId]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(measurmentSummary.JDFJobId, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if JDFJobId contains "&" letter or rest of 4 letters
                                                                                                                                                                                                                                                                                              //labelXML = labelXML.Replace("[Job]", measurmentSummary.MeasurementName);
                                                                                                                                                                                                                                                                                              //labelXML = labelXML.Replace("[Device]", SessionManager.Instance.CurrentDevice.DeviceName);
                                                                                                                                                                                                                                                                                              //labelXML = labelXML.Replace("[Metric]", measurmentSummary.MetricTargetPresetName);
                                                                                                                                                                                                                                                                                              //labelXML = labelXML.Replace("[PrintStandard]", measurmentSummary.PrintStandardType);
                                                                                                                                                                                                                                                                                              //labelXML = labelXML.Replace("[Customer]", measurmentSummary.Customer);
                                                                                                                                                                                                                                                                                              //labelXML = labelXML.Replace("[PageOrSurface]", measurmentSummary.PageOrSurface);
                                                                                                                                                                                                                                                                                              //labelXML = labelXML.Replace("[JDFJobId]", measurmentSummary.JDFJobId);

            System.Text.StringBuilder metricName = new StringBuilder();
            System.Text.StringBuilder metricTol = new StringBuilder();
            System.Text.StringBuilder metricActual = new StringBuilder();
            bool appendNewLine = true;
            foreach (var item in measurmentSummary.ValidationMetrics)
            {
                appendNewLine = true;
                if (measurmentSummary.ValidationMetrics.Count == measurmentSummary.ValidationMetrics.IndexOf(item) + 1)
                {
                    appendNewLine = false;
                }

                metricName.Append(item.Metric + (appendNewLine ? Environment.NewLine : ""));
                metricTol.Append(item.Tolerance.ToString() + (appendNewLine ? Environment.NewLine : ""));
                metricActual.Append(item.Actual.ToString() + (appendNewLine ? Environment.NewLine : ""));
            }
            labelXML = labelXML.Replace("[MetricCaption]", metricName.ToString());
            labelXML = labelXML.Replace("[dTol]", metricTol.ToString());
            labelXML = labelXML.Replace("[dActual]", metricActual.ToString());
            labelXML = labelXML.Replace("\r\n", @"\ \r\n");
            ScriptManager.RegisterStartupScript(this, GetType(), "LabelXML", " function GetLabelXml() { return '" + labelXML.ToString() + "'; };", true);

            string imagePath = "";
            if (measurmentSummary.MeasurementStatus.Equals("OK", StringComparison.InvariantCultureIgnoreCase))
            {
                imagePath = Server.MapPath(@"Images\VStatusSuccessIcon.png");
            }
            else
            {
                imagePath = Server.MapPath(@"Images\VStatusFailedIcon.png");
            }

            byte[] binaryData = File.ReadAllBytes(imagePath);
            string base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);
            ScriptManager.RegisterStartupScript(this, GetType(), "ImageString", " function GetStatusIconImageString() { return '" + base64String + "' ;};", true);
        }
        #endregion

        #region Check Average Measurement Compatibility Mode
        public void ChkAvgMeasurementCompatibility()
        {
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            int CompatibilityNo = _presenterAnalyze.ChkAvgMeasurementCompatibility();
            if (CompatibilityNo == 1)    //Error in different ReferenceCharts throughout whole OptimizationRun
            {
                hd_CompError.Value = "1";
            }
            else if (CompatibilityNo == 2)   //Error in different MetricTargetPresets throughout whole OptimizationRun
            {
                hd_CompError.Value = "2";
            }
            else   //Success, No difference
            {
                hd_CompError.Value = "0";
            }
        }
        #endregion

        #endregion

        #region Properties
        public int JsRandomNumber { get { return new Random().Next(1, 100); } }

        public string ValidationMeasurementStatus
        {
            get
            {
                if (ViewState["MeasurementStatus"] != null)
                {
                    return ViewState["MeasurementStatus"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["MeasurementStatus"] = value;
            }
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
                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)      //--If validation, bind MetricSumList for filtering
                {
                    ViewState["MetricSumID"] = "";
                    FillMetricSumList(MeasurementSummary[0].ValidationMetrics);
                }
                rgPatchDetails.Rebind();  //----Load Patch Details GridView in "Patch Details" tab
            }
            ChkAvgMeasurementCompatibility();   //----Check Avg MeasurementCompability whenever measurement's IsUsed property change the value
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
                {
                    _presenterAnalyze.Get1DMeasurementsOnRunId();
                }
                else
                {
                    _presenterAnalyze.GetMeasurementsOnRunId();
                }

                if (!IsErrorOccured)
                {
                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                    {
                        EndDate = System.DateTime.Now;
                        //Pass use DLP as false for initial call
                        useDLP = false;
                        DeviceLinkProfile = null;

                        if (SessionManager.Instance.CurrentProject.ProjectTypeId == (short)EProjectType.G7)
                        {
                            _presenterAnalyze.CreateG7AlignmentCurve();
                        }
                        else//ISO                                                    
                        {
                            _presenterAnalyze.CreateISOAlignmentCurve();
                        }
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
                        PCSDataTypes = SessionManager.Instance.CurrentProject.PCSDataTypes;
                        DensityType = SessionManager.Instance.CurrentProject.DensityType;
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

            if (hdMeasurement.Value != "" && hdMeasurement.Value != "0")
            {
                MeasurementId = Convert.ToInt32(hdMeasurement.Value);  //----Pass selected measurement id from analyze chart above                   

                //Now we are using DataTable to bind the grid               
                dtPatchDetails = new DataTable();

                _presenterAnalyze.GetPatchDetailsofMeasurement();
                if (!IsErrorOccured)
                {
                    //Now we are using DataTable as DataSource                    
                    rgPatchDetails.DataSource = FilteredPatchDetailsTable(ViewState["MetricSumID"].ToString(), dtPatchDetails);

                    #region Make Visible/Invisible columns according to MetricSum selected in Validation and Sync 1D projects
                    if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation || SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D)      //--If validation / Sync 1D, visible metricsum columns respectively
                    {
                        if (ViewState["MetricSumID"].ToString() == "5" || ViewState["MetricSumID"].ToString() == "6" || ViewState["MetricSumID"].ToString() == "7" || ViewState["MetricSumID"].ToString() == "25" || ViewState["MetricSumID"].ToString() == "26")
                        {
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔH").Visible = true;  //----ΔH
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔCh").Visible = false;  //----ΔCh
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔL").Visible = false;  //----ΔL
                        }
                        else if (ViewState["MetricSumID"].ToString() == "8" || ViewState["MetricSumID"].ToString() == "9" || ViewState["MetricSumID"].ToString() == "36" || ViewState["MetricSumID"].ToString() == "37" || ViewState["MetricSumID"].ToString() == "48")
                        {
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔCh").Visible = true;  //----ΔCh
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔH").Visible = false;  //----ΔH
                            rgPatchDetails.MasterTableView.Columns.FindByDataField("ΔL").Visible = false;  //----ΔL
                        }
                        else if (ViewState["MetricSumID"].ToString() == "10" || ViewState["MetricSumID"].ToString() == "11" || ViewState["MetricSumID"].ToString() == "12" || ViewState["MetricSumID"].ToString() == "13" || ViewState["MetricSumID"].ToString() == "38" || ViewState["MetricSumID"].ToString() == "39" || ViewState["MetricSumID"].ToString() == "40" || ViewState["MetricSumID"].ToString() == "41")
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
                    item.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    item.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void rgPatchTolerance_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //---------Changed color of row, if that metric is failed---------------//
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                Label lblHTML = (Label)item["HtmlColor"].FindControl("lbl_HTML");
                lblHTML.BackColor = System.Drawing.ColorTranslator.FromHtml(item.GetDataKeyValue("HTMLColor").ToString());

                if (item["IsNotOK"].Text == "True")
                {
                    item.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    item.ForeColor = System.Drawing.Color.Green;
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
                    item.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    item.ForeColor = System.Drawing.Color.Green;
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
                    item.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    item.ForeColor = System.Drawing.Color.Green;
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
                    item.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    item.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void rgChromVariation_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                #region Make all columns visible
                rgChromVariation.Columns[0].Visible = true;
                rgChromVariation.Columns[1].Visible = true;
                rgChromVariation.Columns[2].Visible = true;
                rgChromVariation.Columns[3].Visible = true;
                rgChromVariation.Columns[4].Visible = true;
                rgChromVariation.Columns[5].Visible = true;
                rgChromVariation.Columns[6].Visible = true;
                #endregion

                //----If any TVI Tone value is 0, then hide that column from list (ex., either 20, 40, 60 and 80% can be display or either 25, 50, 75% display on list; not both at same time)
                if (item["Tone20"].Text == "0.00" || item["Tone20"].Text == "0,00") //"0,00" for Localization in German and French version of "0.00"
                {
                    rgChromVariation.Columns[0].Visible = false;
                }
                if (item["Tone25"].Text == "0.00" || item["Tone25"].Text == "0,00")
                {
                    rgChromVariation.Columns[1].Visible = false;
                }
                if (item["Tone40"].Text == "0.00" || item["Tone40"].Text == "0,00")
                {
                    rgChromVariation.Columns[2].Visible = false;
                }
                if (item["Tone50"].Text == "0.00" || item["Tone50"].Text == "0,00")
                {
                    rgChromVariation.Columns[3].Visible = false;
                }
                if (item["Tone60"].Text == "0.00" || item["Tone60"].Text == "0,00")
                {
                    rgChromVariation.Columns[4].Visible = false;
                }
                if (item["Tone75"].Text == "0.00" || item["Tone75"].Text == "0,00")
                {
                    rgChromVariation.Columns[5].Visible = false;
                }
                if (item["Tone80"].Text == "0.00" || item["Tone80"].Text == "0,00")
                {
                    rgChromVariation.Columns[6].Visible = false;
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

        protected void btnCGATSExport_Click(object sender, EventArgs e)
        {
            MeasurementId = 0;
            if (hdMeasurement.Value != "")
            {
                MeasurementId = Convert.ToInt32(hdMeasurement.Value);  //----Pass selected measurement id from analyze chart above                   

                //Now we are using DataTable to get data of RadGrid      
                dtPatchDetails = new DataTable();
                dtFilteredPatchDetails = new DataTable();

                _presenterAnalyze.GetPatchDetailsofMeasurement();
                if (!IsErrorOccured)
                {
                    //Now we are using DataTable as DataSource                    
                    dtFilteredPatchDetails = FilteredPatchDetailsTable(ViewState["MetricSumID"].ToString(), dtPatchDetails);

                    //#region Generate CGATS .txt file
                    //System.IO.StreamWriter sw;
                    //sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/") + MeasurementId.ToString() + "_MeasurementFile.txt");
                    //sw.WriteLine(@"ORIGINATOR	""FujiFilm Colorpath Sync""");
                    //sw.WriteLine(@"MEASUREMENT_SOURCE	""WhiteBase=Absolute  Filter=No""");
                    //sw.WriteLine(@"ILLUMINATION_NAME	""D50""");
                    //sw.WriteLine(@"OBSERVER_ANGLE	""2""");
                    //sw.WriteLine(@"KEYWORD	""SampleID""");
                    //sw.WriteLine(@"KEYWORD	""SAMPLE_NAME""");

                    //if (IsSync1DType)
                    //{
                    //    sw.WriteLine("NUMBER_OF_FIELDS" + "\t" + "55"); //-----Only for Sync1D projects, add Spectral data into CGATES file
                    //}
                    //else
                    //{
                    //    sw.WriteLine("NUMBER_OF_FIELDS" + "\t" + "19");
                    //}
                    //sw.WriteLine("BEGIN_DATA_FORMAT");

                    //if (IsSync1DType)
                    //{
                    //    sw.WriteLine("SampleID\tSAMPLE_NAME\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K\tDE\tDE94\tDE2000\tLAB_L_TARGET\tLAB_A_TARGET\tLAB_B_TARGET\tLAB_L\tLAB_A\tLAB_B\tD_RED\tD_GREEN\tD_BLUE\tD_VIS\tnm380\tnm390\tnm400\tnm410\tnm420\tnm430\tnm440\tnm450\tnm460\tnm470\tnm480\tnm490\tnm500\tnm510\tnm520\tnm530\tnm540\tnm550\tnm560\tnm570\tnm580\tnm590\tnm600\tnm610\tnm620\tnm630\tnm640\tnm650\tnm660\tnm670\tnm680\tnm690\tnm700\tnm710\tnm720\tnm730");
                    //}
                    //else
                    //{
                    //    sw.WriteLine("SampleID\tSAMPLE_NAME\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K\tDE\tDE94\tDE2000\tLAB_L_TARGET\tLAB_A_TARGET\tLAB_B_TARGET\tLAB_L\tLAB_A\tLAB_B\tD_RED\tD_GREEN\tD_BLUE\tD_VIS");
                    //}
                    //sw.WriteLine("END_DATA_FORMAT");
                    //sw.WriteLine("NUMBER_OF_SETS" + "\t" + dtFilteredPatchDetails.Rows.Count.ToString());
                    //sw.WriteLine("BEGIN_DATA");

                    //for (int i = 0; i < dtFilteredPatchDetails.Rows.Count; i++)
                    //{
                    //    if (IsSync1DType)
                    //    {
                    //        sw.WriteLine(dtFilteredPatchDetails.Rows[i]["SampleID"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["SampleName"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_K"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE94"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE2000"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["TargetL"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targeta"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targetb"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_L"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_A"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_B"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_V"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm380"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm390"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm400"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm410"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm420"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm430"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm440"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm450"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm460"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm470"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm480"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm490"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm500"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm510"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm520"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm530"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm540"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm550"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm560"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm570"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm580"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm590"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm600"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm610"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm620"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm630"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm640"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm650"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm660"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm670"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm680"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm690"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm700"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm710"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm720"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm730"].ToString());
                    //    }
                    //    else
                    //    {
                    //        sw.WriteLine(dtFilteredPatchDetails.Rows[i]["SampleID"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["SampleName"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_K"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE94"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE2000"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["TargetL"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targeta"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targetb"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_L"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_A"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_B"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_V"].ToString());
                    //    }
                    //}

                    //sw.WriteLine("END_DATA");
                    //sw.Close();

                    //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/"), MeasurementId.ToString() + "_MeasurementFile.txt", MeasurementId.ToString() + "_MeasurementFile.txt");
                    //#endregion

                    byte[] bytes = null;
                    using (var ms = new MemoryStream())
                    {
                        TextWriter tw = new StreamWriter(ms);
                        tw.WriteLine(@"ORIGINATOR	""FujiFilm Colorpath Sync""");
                        tw.WriteLine(@"MEASUREMENT_SOURCE	""WhiteBase=Absolute  Filter=No""");
                        tw.WriteLine(@"ILLUMINATION_NAME	""D50""");
                        tw.WriteLine(@"OBSERVER_ANGLE	""2""");
                        tw.WriteLine(@"KEYWORD	""SampleID""");
                        tw.WriteLine(@"KEYWORD	""SAMPLE_NAME""");
                        tw.WriteLine("NUMBER_OF_FIELDS" + "\t" + "19");

                        tw.WriteLine("BEGIN_DATA_FORMAT");
                        tw.WriteLine("SampleID\tSAMPLE_NAME\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K\tDE\tDE94\tDE2000\tLAB_L_TARGET\tLAB_A_TARGET\tLAB_B_TARGET\tLAB_L\tLAB_A\tLAB_B\tD_RED\tD_GREEN\tD_BLUE\tD_VIS");
                        tw.WriteLine("END_DATA_FORMAT");
                        tw.WriteLine("NUMBER_OF_SETS" + "\t" + dtFilteredPatchDetails.Rows.Count.ToString());
                        tw.WriteLine("BEGIN_DATA");
                        for (int i = 0; i < dtFilteredPatchDetails.Rows.Count; i++)
                        {
                            tw.WriteLine(dtFilteredPatchDetails.Rows[i]["SampleID"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["SampleName"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_K"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE94"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE2000"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["TargetL"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targeta"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targetb"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_L"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_A"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_B"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_V"].ToString());
                        }

                        tw.WriteLine("END_DATA");
                        // tw.Close();

                        tw.Flush();
                        ms.Position = 0;
                        bytes = ms.ToArray();


                        //string str = System.Text.Encoding.UTF8.GetString(bytes);
                        string s2 = BitConverter.ToString(bytes);
                    }
                }
            }
        }


        protected void btnCXFExport_Click(object sender, EventArgs e)
        {
            MeasurementId = 0;
            if (hdMeasurement.Value != "")
            {
                MeasurementId = Convert.ToInt32(hdMeasurement.Value);  //----Pass selected measurement id from analyze chart above                   

                //Now we are using DataTable to get data of RadGrid      
                dtPatchDetails = new DataTable();
                dtFilteredPatchDetails = new DataTable();

                _presenterAnalyze.GetPatchDetailsofMeasurement();
                if (!IsErrorOccured)
                {
                    //Now we are using DataTable as DataSource                    
                    dtFilteredPatchDetails = FilteredPatchDetailsTable(ViewState["MetricSumID"].ToString(), dtPatchDetails);
                    char qt = '\u0022'; //---Pass " as a char into string

                    #region Generate .Cxf file
                    System.IO.StreamWriter sw;
                    sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/") + MeasurementId.ToString() + "_MeasurementFile.cxf");
                    sw.WriteLine(@"<?xml version=""1.0"" encoding=""UTF-8"" ?>");
                    sw.WriteLine(@"<cc:CxF xmlns:xsi=""http:///www.w3.org/2001/XMLSchema-instance"" xmlns:cc=""http://colorexchangeformat.com/CxF3-core"">");
                    sw.WriteLine(@"    <cc:FileInformation>");
                    sw.WriteLine(@"        <cc:Creator>Fujifilm</cc:Creator>");
                    sw.WriteLine(@"        <cc:CreationDate>2016-12-19T16:31:27</cc:CreationDate>");
                    sw.WriteLine(@"        <cc:Description>Unknown</cc:Description>");
                    sw.WriteLine(@"    </cc:FileInformation>");
                    sw.WriteLine(@"    <cc:Resources>");
                    sw.WriteLine(@"        <cc:ObjectCollection>");


                    for (int i = 0; i < dtFilteredPatchDetails.Rows.Count; i++)
                    {
                        sw.WriteLine(@"            <cc:Object ObjectType=""Standard"" Name=" + qt + dtFilteredPatchDetails.Rows[i]["SampleName"].ToString() + qt + " Id=" + qt + dtFilteredPatchDetails.Rows[i]["SampleName"].ToString() + qt + ">");
                        sw.WriteLine(@"                <cc:CreationDate>" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss") + "</cc:CreationDate>");
                        sw.WriteLine(@"                <cc:ColorValues>");
                        sw.WriteLine(@"                    <cc:ColorCIELab ColorSpecification=""M0"">");
                        sw.WriteLine(@"                        <cc:L>" + dtFilteredPatchDetails.Rows[i]["CIELAB_L"].ToString() + "</cc:L>");
                        sw.WriteLine(@"                        <cc:a>" + dtFilteredPatchDetails.Rows[i]["CIELAB_A"].ToString() + "</cc:a>");
                        sw.WriteLine(@"                        <cc:b>" + dtFilteredPatchDetails.Rows[i]["CIELAB_B"].ToString() + "</cc:b>");
                        sw.WriteLine(@"                    </cc:ColorCIELab>");
                        sw.WriteLine(@"                </cc:ColorValues>");
                        sw.WriteLine(@"            </cc:Object>");
                    }

                    sw.WriteLine(@"        </cc:ObjectCollection>");
                    sw.WriteLine(@"        <cc:ColorSpecificationCollection>");
                    sw.WriteLine(@"            <cc:ColorSpecification Id=""M0"">");
                    sw.WriteLine(@"                <cc:MeasurementSpec>");
                    sw.WriteLine(@"                    <cc:MeasurementType>Colorimetric Reflectance</cc:MeasurementType>");
                    sw.WriteLine(@"                    <cc:GeometryChoice>");
                    sw.WriteLine(@"                        <cc:SingleAngle>");
                    sw.WriteLine(@"                            <cc:SingleAngleConfiguration>Uniplanar</cc:SingleAngleConfiguration>");
                    sw.WriteLine(@"                            <cc:IlluminationAngle>45</cc:IlluminationAngle>");
                    sw.WriteLine(@"                            <cc:MeasurementAngle>0</cc:MeasurementAngle>");
                    sw.WriteLine(@"                        </cc:SingleAngle>");
                    sw.WriteLine(@"                    </cc:GeometryChoice>");
                    sw.WriteLine(@"                    <cc:Device>");
                    sw.WriteLine(@"                        <cc:DeviceIllumination>M0 Incandescent</cc:DeviceIllumination>");
                    sw.WriteLine(@"                    </cc:Device>");
                    sw.WriteLine(@"                </cc:MeasurementSpec>");
                    sw.WriteLine(@"            </cc:ColorSpecification>");
                    sw.WriteLine(@"        </cc:ColorSpecificationCollection>");
                    sw.WriteLine(@"    </cc:Resources>");
                    sw.WriteLine(@"</cc:CxF>");
                    sw.Close();

                    Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/"), MeasurementId.ToString() + "_MeasurementFile.cxf", MeasurementId.ToString() + "_MeasurementFile.cxf");
                    #endregion
                }
            }
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

        protected void Chk_CheckedChanged(object sender, EventArgs e)
        {
            //----Call Javascript function to hide TVI charts row and only show Spread Chart row------------//
            ScriptManager.RegisterStartupScript(Page, typeof(Page), Guid.NewGuid().ToString(), "funSpreadVisiblity();", true);
        }

        protected void Chk_CMYKCheckedChanged(object sender, EventArgs e)
        {
            //----Call Javascript function to hide TVI charts row and only show Spread Chart row------------//
            ScriptManager.RegisterStartupScript(Page, typeof(Page), Guid.NewGuid().ToString(), "funCMYKVisiblity();", true);
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("COValidationCallback.aspx?projectid={0}&result={1}&id={2}", SessionManager.Instance.CurrentProject.ProjectId, ValidationMeasurementStatus, Guid.NewGuid()));

        }

        protected void chkAnalyzeAverageOfMeasurements_Click(object sender, EventArgs e)
        {
            if (chkAnalyzeAverageOfMeasurements.Checked == true)
            {
                dvTab_PatchDetails.Visible = Tab_PatchDetails.Visible = false; //Because we can't display Patch Details tab in averaging mode
            }
            else
            {
                dvTab_PatchDetails.Visible = Tab_PatchDetails.Visible = true;
                lblCompError.Text = "";
            }
        }

        protected void chk1DMeasurementSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.Is1DMeasurementSmoothing = chk1DMeasurementSmoothing.Checked;
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
        public List<SpotProjectLAB> SpotProjectLabs { get; set; }
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
                return chkAnalyzeAverageOfMeasurements.Checked;
            }
            set
            {
                IsAverageOfMeasurements = chkAnalyzeAverageOfMeasurements.Checked;
            }
        }

        public bool Is1DMeasurementSmoothing
        {
            get
            {
                return chk1DMeasurementSmoothing.Checked;
            }
            set
            {
                Is1DMeasurementSmoothing = chk1DMeasurementSmoothing.Checked;
            }
        }

        public string FileName;
        public int ProjectStatus { get; set; }
        public Project Project { get; set; }
        public int SpotProjectId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsSyncDLPPlusType
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.DLPPlus);
            }
        }
        public bool IsDlpMultiColor
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.DLPMultiColor);
            }
            set
            {
                IsDlpMultiColor = value;
            }
        }
        public string CurrentRunname { get; set; }
        public int LicenseTypeId { get; set; }
        public DataTable dtMeasurement { get; set; }
        public decimal Dec_E_Value { get; set; }
        #endregion

    }
}
