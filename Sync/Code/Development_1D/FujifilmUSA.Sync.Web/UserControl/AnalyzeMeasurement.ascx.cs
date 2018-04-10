using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AnalyzeMeasurement : BaseControl, IViewAnalyze
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

        //public string CreateDLPUpdateProgressClientID
        //{
        //    get { return updateProgress.ClientID; }
        //}
        public string CreateDLPUpdateProgressClientID
        {
            get;
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

        #region Page Method
        protected override void OnInit(EventArgs e)
        {
            _presenterAnalyze = new PresenterAnalyze(this);
            _presenterGlobal = new PresenterGlobal();
        }

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
        protected void Page_Load(object sender, EventArgs e)
        {
            lbtnMeasurement.Visible = IsXMFUser;
            if (!Page.IsPostBack)
            {
                hdnBaseServiceUrl.Value = Utilities.ReadConfigValue("BaseServiceURL");
                hdnBasePluginServiceUrl.Value = Utilities.ReadConfigValue("BasePluginServiceURL");
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
                ((Label)updateProgress.FindControl("ucCreateDLP").FindControl("lblMessage")).Text = DLPMessage;


                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    //Verification mode
                    //if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP) //---Only display Report Grading for Sync4D Validation
                    //{
                    //    Div_ReportGrading.Visible = true; //---Disable for time being in Production code
                    //}
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
                                                     "','" + Server.UrlEncode(this.CurrentDLPName) + "')");

                    trRBtnListDeltaE.Visible = false;
                    //Div_Validation_Header.Visible = true;
                    //if (ValidationType == 1)  //G7 type
                    //{
                    //    dvTab_G7.Visible = Tab_G7.Visible = true;
                    //}
                    //else
                    //{
                    //    dvTab_G7.Visible = Tab_G7.Visible = false;
                    //}
                }
                else if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D) //-----Sync1D
                {
                    //Hide Verification button
                    lbtnMeasurementVerification.Visible = false;
                    lbtnOptimize.Visible = false;
                    trRBtnListDeltaE.Visible = false;
                    //Div_Opt_Header.Visible = true;

                    td1DSmoothing.Visible = true;  //Display in Sync1D for Smoothing measurements (Commented out for temporary untill get decided to use this feature)
                    //if (ValidationType == 1)  //G7 type
                    //{
                    //    //dvTab_G7.Visible = Tab_G7.Visible = true;
                    //    //tdAverageOfMeasurements.Visible = true; //Display in Sync1D G7 mode for Averaging measurements
                    //}

                }
                else
                {
                    //Hide Verification button
                    lbtnMeasurementVerification.Visible = false;
                    lbtnOptimize.Visible = false;
                    trRBtnListDeltaE.Visible = true;
                    //Div_Opt_Header.Visible = true;
                    //dvTab_G7.Visible = Tab_G7.Visible = false;
                    //dvTab_TVI.Visible = Tab_TVI.Visible = false;
                    //Div_MetricSumFilter.Visible = false;
                }
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


        #region Control Events
        protected void lbtnMeasurementVerification_Click(object sender, EventArgs e)
        {
            Response.Redirect("Measurement.aspx");
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
                        EndDate = System.DateTime.Now;
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

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("COValidationCallback.aspx?projectid={0}&result={1}&id={2}", SessionManager.Instance.CurrentProject.ProjectId, ValidationMeasurementStatus, Guid.NewGuid()));

        }
        protected void lbtnMeasurement_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Measure;
            Response.Redirect(string.Format("COMeasurement.aspx?id={0}", Guid.NewGuid()));
        }
        protected void chk1DMeasurementSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.Is1DMeasurementSmoothing = chk1DMeasurementSmoothing.Checked;
        }

        
        #endregion

        #region Properties
        public int JsRandomNumber { get { return new Random().Next(1, 100); } }

        public string ValidationMeasurementStatus
        {
            get
            {
                if (ViewState["MeasurementStatus"] != null)
                    return ViewState["MeasurementStatus"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["MeasurementStatus"] = value;
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

        //public bool IsAverageOfMeasurements
        //{
        //    get
        //    {
        //        return chkAnalyzeAverageOfMeasurements.Checked;
        //    }
        //    set
        //    {
        //        IsAverageOfMeasurements = chkAnalyzeAverageOfMeasurements.Checked;
        //    }
        //}

        //public bool Is1DMeasurementSmoothing
        //{
        //    get
        //    {
        //        return chk1DMeasurementSmoothing.Checked;
        //    }
        //    set
        //    {
        //        Is1DMeasurementSmoothing = chk1DMeasurementSmoothing.Checked;
        //    }
        //}
        public bool IsAverageOfMeasurements
        {
            get;
            set;
        }

        public bool Is1DMeasurementSmoothing
        {
            get;
            set;
        }

        public string FileName;
        public int ProjectStatus { get; set; }
        public Project Project { get; set; }
        public int SpotProjectId { get; set; }
        public bool IsUsed { get; set; }
        #endregion
    }
}