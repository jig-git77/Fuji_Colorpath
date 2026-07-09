using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSync1D;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        #region Page Method
        protected override void OnInit(EventArgs e)
        {
            _presenterAnalyze = new PresenterAnalyze(this);
            _presenterGlobal = new PresenterGlobal();
        }



        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            lbtnMeasurement.Visible = IsXMFUser;
            Project = SessionManager.Instance.CurrentProject;
            if (!Page.IsPostBack)
            {
                hdnBaseServiceUrl.Value = Utilities.ReadConfigValue("BaseServiceURL");
                //hdnBasePluginServiceUrl.Value = Utilities.ReadConfigValue("BasePluginServiceURL");
                hdnCurrentUserID.Value = Convert.ToString(SessionManager.Instance.CurrentUser.UserId) ?? "0";
                hdfCurrentCulture.Value = getCurrentCultureLanguage();
                hdnProjectID.Value = SessionManager.Instance.CurrentOptimizeRun.ProjectId.ToString();
                hdnOptimizationRunID.Value = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId.ToString();
                hdnValidationType.Value = ValidationType.ToString();
                hdnCurrentProjectLicenseType.Value = SessionManager.Instance.CurrentProject.LicenseType.ToString();
                hdnCurrentDeviceName.Value = SessionManager.Instance.CurrentDevice.DeviceName.ToString();
                hdnCurrentUserRole.Value = SessionManager.Instance.CurrentUser.Role.RoleId.ToString();
                hdfAuthToken.Value = SessionManager.Instance.CurrentUser.APIAuthKey;

                //--Start: Display Button to export measurement in CloudFlow DotGain format if this device family has CloudFlow support
                ChkCloudFlowSupport();
                //--End

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
                    txtReportTitle.Text = "Validation Report";
                }
                else
                {
                    txtReportTitle.Text = "Optimization Report";
                    SessionManager.Instance.CurrentDlpSubOperation = SessionManager.Instance.CurrentProject.LicenseType; //---Assign DLP or Sync1D project type
                    if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D || SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1DMultiColor) //-----Sync1D ----Sync1DMultiColor
                    {
                        //------If project is Sync1D type, Assign 'E' to radiolist, bydefault (because Sync 1D has only dt_E type)----------------//
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
                hdnSyncDlpPlusType.Value = IsSyncDLPPlusType.ToString();
                hdnIsDlpMultiColor.Value = Convert.ToString(SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLPMultiColor ? true : false);
                hdnIsSync1DMultiColor.Value = Convert.ToString(SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1DMultiColor ? true : false);
                hdnCurrentRunName.Value = SessionManager.Instance.CurrentOptimizeRun.CurrentRunName.ToString();

                if (SessionManager.Instance.CurrentOptimizeRun.Status == EOptimizeRunStatus.Certify)
                {
                    //------------If this run is closed /certify run, then invisible download(Next) button, because there is no DLP available to download for this run-----//
                    lbtnDownloadAndSaveDLP.Visible = false;
                }
                ((Label)updateProgress.FindControl("ucCreateDLP").FindControl("lblMessage")).Text = DLPMessage;


                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    //Verification mode
                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP) //---Only display Report Grading for Sync4D Validation
                    {
                        //Div_ReportGrading.Visible = true; //---Disable for time being in Production code
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
                                                     "','" + Server.UrlEncode(this.CurrentDLPName) + "')");

                    trRBtnListDeltaE.Visible = false;

                }
                else if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D || SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1DMultiColor) //-----Sync1D -----Sync1DMultiColor
                {
                    //Hide Verification button
                    lbtnMeasurementVerification.Visible = false;
                    lbtnOptimize.Visible = false;
                    trRBtnListDeltaE.Visible = false;
                    //Div_Opt_Header.Visible = true;

                    td1DSmoothing.Visible = true;  //Display in Sync1D for Smoothing measurements (Commented out for temporary untill get decided to use this feature)
                    //Div_ReportCMYKDensity.Visible = true;  //---Disable for time being in Production code

                }
                else if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.DLPPlus)
                {
                    trRBtnListDeltaE.Visible = false;
                }
                else
                {
                    //Hide Verification button
                    lbtnMeasurementVerification.Visible = false;
                    lbtnOptimize.Visible = false;
                    trRBtnListDeltaE.Visible = true;
                }
                var licenseType = SessionManager.Instance.CurrentProject.LicenseType;

                // CSS - 36
                // Show / Hide checkbox
                chk_IsPaperScaling4D.Visible =
                     licenseType == ELicenseType.DLP
                    || licenseType == ELicenseType.Sync4DCertification
                    || licenseType == ELicenseType.DLPMultiColor;

                tdPaperScaling.Visible = licenseType == ELicenseType.Sync1D
                    || licenseType == ELicenseType.Sync1DMultiColor
                    || licenseType == ELicenseType.DLPPlus;

                // Sync1D / SpotLibrary hidden
                //if (licenseType == ELicenseType.SpotLibrary)
                //{
                //    chk_IsPaperScaling4D.Visible = false;
                //}


                // Checked State (common for supported licenses)
                if (licenseType == ELicenseType.DLP ||
                    licenseType == ELicenseType.Sync4DCertification ||
                    licenseType == ELicenseType.DLPMultiColor)
                {
                    chk_IsPaperScaling4D.Checked = SessionManager.Instance.CurrentProject.EnabledPaperScaling;
                }

                if (
                    licenseType == ELicenseType.Sync1D ||
                    licenseType == ELicenseType.Sync1DMultiColor ||
                    licenseType == ELicenseType.DLPPlus)
                {
                    chk_IsPaperScaling1D.Checked = SessionManager.Instance.CurrentProject.EnabledPaperScaling;
                }

                ProjectTypeId = SessionManager.Instance.CurrentProject.ProjectTypeId;
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                LicenseTypeId = (int)SessionManager.Instance.CurrentProject.LicenseType;
                if (SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld != 0)
                    PrintstandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld;
                else
                    PrintstandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeID;

                if (SessionManager.Instance.CurrentProject.ProjectTargetLAB == null || !SessionManager.Instance.CurrentProject.ProjectTargetLAB.Any())
                {
                    _presenterAnalyze.GetTargetLABList();
                    SessionManager.Instance.CurrentProject.ProjectTargetLAB = ProjectTargetLABList;
                    SessionManager.Instance.CurrentProject.NewPaper_White_L_Old = Project.NewPaper_White_L_Old;
                    SessionManager.Instance.CurrentProject.NewPaper_White_a_Old = Project.NewPaper_White_a_Old;
                    SessionManager.Instance.CurrentProject.NewPaper_White_b_Old = Project.NewPaper_White_b_Old;
                }

                bool isSpotLibraryProject_ = SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary;

                // update current highlighted measurement if not created DLP,  CSS - 36.
                if (SessionManager.Instance.CurrentProject.EnabledPaperScaling)
                {
                    if(SessionManager.Instance.CurrentOptimizeRun.MasterRunId == -1)
                    {
                        if (SessionManager.Instance.CurrentProject.EnabledPaperScaling)
                        {
                            _presenterAnalyze.GetMeasurementIDs_RPS();
                            MeasurementId = MeasurementIds.Any() ? MeasurementIds.Max() : 0;
                            _presenterAnalyze.GetActualValuesByMeasurementId();
                            if (licenseType == ELicenseType.DLP
                            || licenseType == ELicenseType.Sync4DCertification
                            || licenseType == ELicenseType.DLPMultiColor)
                            {
                                Project.NewPaper_White_L = Convert.ToDecimal(measurementPatchDetails.CielabL);
                                Project.NewPaper_White_a = Convert.ToDecimal(measurementPatchDetails.CielabA);
                                Project.NewPaper_White_b = Convert.ToDecimal(measurementPatchDetails.CielabB);
                                EnabledPaperScaling = SessionManager.Instance.CurrentProject.EnabledPaperScaling;
                                IsUpdateNewPaper = true;
                                IsUpdateDelta = true;
                                _presenterAnalyze.UpdateTargetLabList();
                            }
                            else if (licenseType == ELicenseType.Sync1D ||
                            licenseType == ELicenseType.Sync1DMultiColor ||
                            licenseType == ELicenseType.DLPPlus)
                            {
                                _presenterAnalyze.GetTargetLABList();
                                double NewPaper_White_L = Convert.ToDouble(measurementPatchDetails.CielabL);
                                double NewPaper_White_a = Convert.ToDouble(measurementPatchDetails.CielabA);
                                double NewPaper_White_b = Convert.ToDouble(measurementPatchDetails.CielabB);
                                EnabledPaperScaling = SessionManager.Instance.CurrentProject.EnabledPaperScaling;
                                CalculateTargetLAB(NewPaper_White_L, NewPaper_White_a, NewPaper_White_b, true, true);
                            }
                        }
                        else
                        {
                            // 1. take current optimize run id's before one.
                            // 2. based on previous optimize run id get measurement ids and get it's cielab values and take avg.
                            // 3. by avg values calculate target LAB / update new paper based on condition 4d or 1d.
                            // Current optimize run
                            var currentRun = SessionManager.Instance.CurrentOptimizeRun;

                            // Get previous optimize run
                            var previousRun = SessionManager.Instance.CurrentProject.OptimizeRuns
                                                .Where(x => x.OptimizeRunId < currentRun.OptimizeRunId)
                                                .OrderByDescending(x => x.OptimizeRunId)
                                                .FirstOrDefault();

                            if (previousRun != null)
                            {
                                OptimizeRunId = Project.OptimizeRuns.Select(x => x.OptimizeRunId).First();
                                // Get all measurement ids for previous run
                                _presenterAnalyze.GetMeasurementIDs_RPS();

                                if (MeasurementIds != null && MeasurementIds.Any())
                                {
                                    decimal avgL = 0;
                                    decimal avgA = 0;
                                    decimal avgB = 0;
                                    int count = 0;

                                    foreach (int measurementId in MeasurementIds)
                                    {
                                        MeasurementId = measurementId;

                                        _presenterAnalyze.GetActualValuesByMeasurementId();

                                        if (measurementPatchDetails != null)
                                        {
                                            avgL += Convert.ToDecimal(measurementPatchDetails.CielabL);
                                            avgA += Convert.ToDecimal(measurementPatchDetails.CielabA);
                                            avgB += Convert.ToDecimal(measurementPatchDetails.CielabB);
                                            count++;
                                        }
                                    }

                                    if (count > 0)
                                    {
                                        avgL /= count;
                                        avgA /= count;
                                        avgB /= count;

                                        avgL = Math.Round(avgL, 2, MidpointRounding.AwayFromZero);
                                        avgA = Math.Round(avgA, 2, MidpointRounding.AwayFromZero);
                                        avgB = Math.Round(avgB, 2, MidpointRounding.AwayFromZero);

                                        if (licenseType == ELicenseType.DLP
                                            || licenseType == ELicenseType.Sync4DCertification
                                            || licenseType == ELicenseType.DLPMultiColor)
                                        {
                                            Project.NewPaper_White_L = avgL;
                                            Project.NewPaper_White_a = avgA;
                                            Project.NewPaper_White_b = avgB;

                                            EnabledPaperScaling = SessionManager.Instance.CurrentProject.EnabledPaperScaling;
                                            IsUpdateNewPaper = true;
                                            IsUpdateDelta = true;

                                            _presenterAnalyze.UpdateTargetLabList();
                                        }
                                        else if (licenseType == ELicenseType.Sync1D
                                              || licenseType == ELicenseType.Sync1DMultiColor
                                              || licenseType == ELicenseType.DLPPlus)
                                        {
                                            _presenterAnalyze.GetTargetLABList();

                                            EnabledPaperScaling = SessionManager.Instance.CurrentProject.EnabledPaperScaling;

                                            CalculateTargetLAB(Convert.ToDouble(avgL),Convert.ToDouble(avgA),Convert.ToDouble(avgB),true,true);
                                        }
                                    }
                                }
                            }
                        }
                    }
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
            if (SessionManager.Instance.CurrentDlpSubOperation != ELicenseType.Validation)  //----Only display print label feature in validation mode
            {
                ibtnPrintPreview.Visible = false;
                ibtnPrintLabel.Visible = false;
                ibtnPrintPreviewNew.Visible = false;
            }

            chk_IsPaperScaling1D.Enabled = false;
            chk_IsPaperScaling4D.Enabled = false;

            bool isSpotLibraryProject = SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary;

            if (SessionManager.Instance.CurrentProject.CurrentDisplayStatus == ECurrentProjectStatus.New)
            {
                if ((SessionManager.Instance.CurrentProject.SourceProfile != null &&
                        SessionManager.Instance.CurrentProject.SourceProfile.SourceProfileId > 0 &&
                        SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.CustomSourceProfile &&
                        SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.DestinationProfile) //No Target ColorSpace for Custom Source Profile project type
                        || (isSpotLibraryProject && SessionManager.Instance.CurrentProject.SpotTargetProject.SpotTarget != null)) // Show Target Color Space for Spot Color Target project type
                {
                    chk_IsPaperScaling1D.Enabled = chk_IsPaperScaling4D.Enabled = true;
                }
            }
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

        #region Check CloudFlow Support
        public void ChkCloudFlowSupport()
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            CurrentRefChartTypeId = Convert.ToInt16(SessionManager.Instance.CurrentOptimizeRun.ReferenceChartType.Key);
            int CompatibilityNo = _presenterAnalyze.ChkCloudFlowSupport();
            if (CompatibilityNo == 1)    //Error in matching between CloudFlow support in DeviceFamily vs ReferenceChartType
            {
                hdnCloudFlowCheck.Value = "1";
            }
            else   //Success, No difference
            {
                hdnCloudFlowCheck.Value = "0";
            }
        }
        #endregion

        #region Control Events
        #region CSS - 36
        protected void chk_IsPaperScaling4D_CheckedChanged(object sender, EventArgs e)
        {
            ProjectTypeId = SessionManager.Instance.CurrentProject.ProjectTypeId;
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            LicenseTypeId = (int)SessionManager.Instance.CurrentProject.LicenseType;
            int HighlightedMeasurementId = Convert.ToInt32(hdnCurrentMeasurement.Value);
            EnabledPaperScaling = chk_IsPaperScaling4D.Checked;
            if (SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld != 0)
                PrintstandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld;
            else
                PrintstandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeID;

            double NewPaper_White_L = 0.0;
            double NewPaper_White_a = 0.0;
            double NewPaper_White_b = 0.0;

            _presenterAnalyze.GetMeasurementIDs_RPS();

            foreach (var measurementId in MeasurementIds.OrderBy(x => x == HighlightedMeasurementId ? 1 : 0))
            {
                MeasurementId = measurementId;
                _presenterAnalyze.GetActualValuesByMeasurementId();
                if (EnabledPaperScaling)
                {
                    NewPaper_White_L = Convert.ToDouble(measurementPatchDetails.CielabL);
                    NewPaper_White_a = Convert.ToDouble(measurementPatchDetails.CielabA);
                    NewPaper_White_b = Convert.ToDouble(measurementPatchDetails.CielabB);
                }
                else
                {
                    NewPaper_White_L = Convert.ToDouble(SessionManager.Instance.CurrentProject.NewPaper_White_L_Old.ToString());
                    NewPaper_White_a = Convert.ToDouble(SessionManager.Instance.CurrentProject.NewPaper_White_a_Old.ToString());
                    NewPaper_White_b = Convert.ToDouble(SessionManager.Instance.CurrentProject.NewPaper_White_b_Old.ToString());
                }
                IsUpdateNewPaper = true;
                IsUpdateDelta = true;
                Project.NewPaper_White_L = Convert.ToDecimal(NewPaper_White_L);
                Project.NewPaper_White_a = Convert.ToDecimal(NewPaper_White_a);
                Project.NewPaper_White_b = Convert.ToDecimal(NewPaper_White_b);
                _presenterAnalyze.UpdateTargetLabList();
            }
            SessionManager.Instance.CurrentProject.EnabledPaperScaling = chk_IsPaperScaling4D.Checked;
            SessionManager.Instance.CurrentProject.NewPaper_White_L = Convert.ToDecimal(NewPaper_White_L);
            SessionManager.Instance.CurrentProject.NewPaper_White_a = Convert.ToDecimal(NewPaper_White_a);
            SessionManager.Instance.CurrentProject.NewPaper_White_b = Convert.ToDecimal(NewPaper_White_b);
        }

        protected void chk_IsPaperScaling1D_CheckedChanged(object sender, EventArgs e)
        {
            ProjectTypeId = SessionManager.Instance.CurrentProject.ProjectTypeId;
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            Project.DeviceId = SessionManager.Instance.CurrentProject.DeviceId;
            LicenseTypeId = (int)SessionManager.Instance.CurrentProject.LicenseType;
            if (SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld != 0)
                PrintstandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld;
            else
                PrintstandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeID;

            EnabledPaperScaling = chk_IsPaperScaling1D.Checked;
            //Create list for new lab values
            List<Entity.CPSync1D.ProjectTargetLAB> NewProjectTargetList = new List<Entity.CPSync1D.ProjectTargetLAB>();

            // last highlighted measurement id.
            int HighlightedMeasurementId = Convert.ToInt32(hdnCurrentMeasurement.Value);

            double NewPaper_White_L = 0.0;
            double NewPaper_White_a = 0.0;
            double NewPaper_White_b = 0.0;

            if (EnabledPaperScaling)
            {
                if (SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld != SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeID)
                {
                    _presenterAnalyze.GetTargetLABList();
                    SessionManager.Instance.CurrentProject.ProjectTargetLAB = ProjectTargetLABList;
                    SessionManager.Instance.CurrentProject.NewPaper_White_L_Old = Project.NewPaper_White_L_Old;
                    SessionManager.Instance.CurrentProject.NewPaper_White_a_Old = Project.NewPaper_White_a_Old;
                    SessionManager.Instance.CurrentProject.NewPaper_White_b_Old = Project.NewPaper_White_b_Old;
                }
                else
                    ProjectTargetLABList = SessionManager.Instance.CurrentProject.ProjectTargetLAB;
            }
            else
            {
                if (SessionManager.Instance.CurrentProject.OldProjectTargetLAB == null || !SessionManager.Instance.CurrentProject.OldProjectTargetLAB.Any())
                {
                    _presenterAnalyze.GetTargetLABList();
                    SessionManager.Instance.CurrentProject.ProjectTargetLAB = ProjectTargetLABList;
                    SessionManager.Instance.CurrentProject.NewPaper_White_L_Old = Project.NewPaper_White_L_Old;
                    SessionManager.Instance.CurrentProject.NewPaper_White_a_Old = Project.NewPaper_White_a_Old;
                    SessionManager.Instance.CurrentProject.NewPaper_White_b_Old = Project.NewPaper_White_b_Old;
                }
            }

            //1. get measurement IDs
            //2. in loop get actual values by measurement id, except the highlighted one
            //3. calculate matrix and delta values update in db
            //4. the last highlighted id proccess at last and update new paper value in it.

            _presenterAnalyze.GetMeasurementIDs_RPS();

            foreach (var measurementId in MeasurementIds.OrderBy(x => x == HighlightedMeasurementId ? 1 : 0))
            {
                MeasurementId = measurementId;
                _presenterAnalyze.GetActualValuesByMeasurementId();
                if (EnabledPaperScaling)
                {
                    NewPaper_White_L = Convert.ToDouble(measurementPatchDetails.CielabL);
                    NewPaper_White_a = Convert.ToDouble(measurementPatchDetails.CielabA);
                    NewPaper_White_b = Convert.ToDouble(measurementPatchDetails.CielabB);
                }
                else
                {
                    NewPaper_White_L = Convert.ToDouble(SessionManager.Instance.CurrentProject.NewPaper_White_L_Old.ToString());
                    NewPaper_White_a = Convert.ToDouble(SessionManager.Instance.CurrentProject.NewPaper_White_a_Old.ToString());
                    NewPaper_White_b = Convert.ToDouble(SessionManager.Instance.CurrentProject.NewPaper_White_b_Old.ToString());
                }

                // get actual values by measurement id.
                //IsUpdateNewPaper = (measurementId == HighlightedMeasurementId);
                IsUpdateNewPaper = LicenseTypeId == (int)ELicenseType.DLPPlus ? true : (measurementId == HighlightedMeasurementId);
                IsUpdateDelta = true;
                CalculateTargetLAB(NewPaper_White_L, NewPaper_White_a, NewPaper_White_b, IsUpdateNewPaper, IsUpdateDelta);
            }
            SessionManager.Instance.CurrentProject.ProjectTargetLAB = ProjectTargetLABList;
            SessionManager.Instance.CurrentProject.EnabledPaperScaling = chk_IsPaperScaling1D.Checked;
            SessionManager.Instance.CurrentProject.NewPaper_White_L = Convert.ToDecimal(NewPaper_White_L);
            SessionManager.Instance.CurrentProject.NewPaper_White_a = Convert.ToDecimal(NewPaper_White_a);
            SessionManager.Instance.CurrentProject.NewPaper_White_b = Convert.ToDecimal(NewPaper_White_b);
        }

        protected void btnRPSPostBack1D_click(object sender, EventArgs e)
        {
            //bool isSpotLibraryProject = SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary;
            //if (SessionManager.Instance.CurrentProject.CurrentDisplayStatus == ECurrentProjectStatus.New)
            //{
            //    if ((SessionManager.Instance.CurrentProject.SourceProfile != null &&
            //            SessionManager.Instance.CurrentProject.SourceProfile.SourceProfileId > 0 &&
            //            SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.CustomSourceProfile &&
            //            SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.DestinationProfile) //No Target ColorSpace for Custom Source Profile project type
            //            || (isSpotLibraryProject && SessionManager.Instance.CurrentProject.SpotTargetProject.SpotTarget != null)) // Show Target Color Space for Spot Color Target project type
            //    {
            if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId != 0)
            {
                if (SessionManager.Instance.CurrentOptimizeRun.MasterRunId == -1)
                {
                    ProjectTypeId = SessionManager.Instance.CurrentProject.ProjectTypeId;
                    ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                    LicenseTypeId = (int)SessionManager.Instance.CurrentProject.LicenseType;
                    MeasurementId = Convert.ToInt32(hdnCurrentMeasurement.Value);
                    if (SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld != 0)
                        PrintstandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld;
                    else
                        PrintstandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeID;
                    EnabledPaperScaling = chk_IsPaperScaling1D.Checked;

                    double NewPaper_White_L = Convert.ToDouble(hdnCielabL.Value);
                    double NewPaper_White_a = Convert.ToDouble(hdnCielabA.Value);
                    double NewPaper_White_b = Convert.ToDouble(hdnCielabB.Value);
                    if (SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld != SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeID)
                    {
                        _presenterAnalyze.GetTargetLABList();
                    }
                    else
                        ProjectTargetLABList = SessionManager.Instance.CurrentProject.ProjectTargetLAB;
                    CalculateTargetLAB(NewPaper_White_L, NewPaper_White_a, NewPaper_White_b, true, false);
                }
            }
            //    }
            //}
        }

        protected void CalculateTargetLAB(double NewPaper_White_L, double NewPaper_White_a, double NewPaper_White_b, bool IsUpdateNewPaper_, bool IsUpdateDelta_)
        {
            if (ProjectTargetLABList != null && ProjectTargetLABList.Count > 0)
            {
                CIELab Finallab = new CIELab(0, 0, 0);
                CIEXYZ CurrentPaperxyz = new CIEXYZ(0, 0, 0);
                CIEXYZ NewPaperxyz = new CIEXYZ(0, 0, 0);
                CIEXYZ NewMinxyz = new CIEXYZ(0, 0, 0);

                //Create list for Paper Patch
                List<Entity.CPSync1D.ProjectTargetLAB> PaperProjectTargetList = new List<Entity.CPSync1D.ProjectTargetLAB>();
                PaperProjectTargetList = ProjectTargetLABList.ToList().Where(p => p.PatchType.PatchTypeId == 8).ToList();

                //Create list for new lab values
                List<Entity.CPSync1D.ProjectTargetLAB> NewProjectTargetList = new List<Entity.CPSync1D.ProjectTargetLAB>();

                double Black_L = Convert.ToDouble(SessionManager.Instance.CurrentProject.NewPaper_Black_L.ToString());
                double Black_a = Convert.ToDouble(SessionManager.Instance.CurrentProject.NewPaper_Black_a.ToString());
                double Black_b = Convert.ToDouble(SessionManager.Instance.CurrentProject.NewPaper_Black_b.ToString());

                //convert new Paper's LAB to XYZ
                NewPaperxyz = ColorMath.LabtoXYZ(NewPaper_White_L, NewPaper_White_a, NewPaper_White_b);
                NewMinxyz = ColorMath.LabtoXYZ(Black_L, Black_a, Black_b);

                //convert current Paper's LAB to XYZ
                CurrentPaperxyz = ColorMath.LabtoXYZ(Convert.ToDouble(PaperProjectTargetList[0].TargetL), Convert.ToDouble(PaperProjectTargetList[0].TargetA), Convert.ToDouble(PaperProjectTargetList[0].TargetB));

                for (int i = 0; i < ProjectTargetLABList.Count; i++)
                {
                    Finallab = ColorMath.CalcPaperScaleLAB(CurrentPaperxyz.X, CurrentPaperxyz.Y, CurrentPaperxyz.Z, NewPaperxyz.X, NewPaperxyz.Y, NewPaperxyz.Z, NewMinxyz.X, NewMinxyz.Y, NewMinxyz.Z, Convert.ToDouble(ProjectTargetLABList[i].TargetL), Convert.ToDouble(ProjectTargetLABList[i].TargetA), Convert.ToDouble(ProjectTargetLABList[i].TargetB));
                    NewProjectTargetList.Add(new Entity.CPSync1D.ProjectTargetLAB() { PatchType = ProjectTargetLABList[i].PatchType, TargetL = (decimal)Finallab.L, TargetA = (decimal)Finallab.A, TargetB = (decimal)Finallab.B });
                }
                int index = NewProjectTargetList.FindIndex(x => x.PatchType.PatchTypeId == 8);

                if (index != -1)
                {
                    NewProjectTargetList[index].TargetL = Convert.ToDecimal(NewPaper_White_L);
                    NewProjectTargetList[index].TargetA = Convert.ToDecimal(NewPaper_White_a);
                    NewProjectTargetList[index].TargetB = Convert.ToDecimal(NewPaper_White_b);
                }
                ProjectTargetLABList = NewProjectTargetList;
                Project.NewPaper_White_L = Convert.ToDecimal(NewPaper_White_L);
                Project.NewPaper_White_a = Convert.ToDecimal(NewPaper_White_a);
                Project.NewPaper_White_b = Convert.ToDecimal(NewPaper_White_b);
                IsUpdateNewPaper = IsUpdateNewPaper_;
                IsUpdateDelta = IsUpdateDelta_;
                _presenterAnalyze.UpdateTargetLabList();
            }
        }

        protected void btnRPSPostBack4D_click(object sender, EventArgs e)
        {
            //bool isSpotLibraryProject = SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary;
            //if (SessionManager.Instance.CurrentProject.CurrentDisplayStatus == ECurrentProjectStatus.New)
            //{
            //    if ((SessionManager.Instance.CurrentProject.SourceProfile != null &&
            //            SessionManager.Instance.CurrentProject.SourceProfile.SourceProfileId > 0 &&
            //            SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.CustomSourceProfile &&
            //            SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.DestinationProfile) //No Target ColorSpace for Custom Source Profile project type
            //            || (isSpotLibraryProject && SessionManager.Instance.CurrentProject.SpotTargetProject.SpotTarget != null)) // Show Target Color Space for Spot Color Target project type
            //    {
            if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId != 0)
            {
                if (SessionManager.Instance.CurrentOptimizeRun.MasterRunId == -1)
                {
                    ProjectTypeId = SessionManager.Instance.CurrentProject.ProjectTypeId;
                    ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                    LicenseTypeId = (int)SessionManager.Instance.CurrentProject.LicenseType;
                    EnabledPaperScaling = chk_IsPaperScaling4D.Checked;
                    MeasurementId = Convert.ToInt32(hdnCurrentMeasurement.Value);

                    double NewPaper_White_L = Convert.ToDouble(hdnCielabL.Value);
                    double NewPaper_White_a = Convert.ToDouble(hdnCielabA.Value);
                    double NewPaper_White_b = Convert.ToDouble(hdnCielabB.Value);
                    IsUpdateNewPaper = true;
                    IsUpdateDelta = false;
                    Project.NewPaper_White_L = Convert.ToDecimal(NewPaper_White_L);
                    Project.NewPaper_White_a = Convert.ToDecimal(NewPaper_White_a);
                    Project.NewPaper_White_b = Convert.ToDecimal(NewPaper_White_b);
                    _presenterAnalyze.UpdateTargetLabList();
                }
            }
            //    }
            //}
        }

        #endregion
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
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1DMultiColor) //------Sync1DMultiColor
                    _presenterAnalyze.Get1DMeasurementsOnRunId();
                else
                    _presenterAnalyze.GetMeasurementsOnRunId();
                if (!IsErrorOccured)
                {
                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1DMultiColor) //------Sync1DMultiColor
                    {
                        EndDate = System.DateTime.Now;
                        //Pass use DLP as false for initial call
                        useDLP = false;
                        DeviceLinkProfile = null;
                        DlpDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLPText) + EndDate.Month.ToString("00") + EndDate.Day.ToString("00") + EndDate.Hour.ToString("00") + EndDate.Minute.ToString("00"));

                        if (SessionManager.Instance.CurrentProject.ProjectTypeId == (short)EProjectType.G7)
                            _presenterAnalyze.CreateG7AlignmentCurve();
                        else//ISO                                                    
                            _presenterAnalyze.CreateISOAlignmentCurve();
                    }
                    else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLPPlus)
                    {
                        EndDate = System.DateTime.Now;
                        DestinationProfileDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName);
                        DlpDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLPText) + EndDate.Month.ToString("00") + EndDate.Day.ToString("00") + EndDate.Hour.ToString("00") + EndDate.Minute.ToString("00"));
                        _presenterAnalyze.CreateDestinationAndDlpPlus();
                    }
                    else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLPMultiColor)
                    {
                        EndDate = System.DateTime.Now;
                        DestinationProfileDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName);
                        DlpDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLPText) + EndDate.Month.ToString("00") + EndDate.Day.ToString("00") + EndDate.Hour.ToString("00") + EndDate.Minute.ToString("00"));
                        _presenterAnalyze.CreateDestinationAndDlpMultiColor();
                    }
                    else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.CustomSourceProfile)
                    {
                        EndDate = System.DateTime.Now;
                        DestinationProfileDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName);
                        DlpDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLPText) + EndDate.Month.ToString("00") + EndDate.Day.ToString("00") + EndDate.Hour.ToString("00") + EndDate.Minute.ToString("00"));
                        _presenterAnalyze.CreateCustomSourceProfileDlp();
                    }
                    else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DestinationProfile)
                    {
                        EndDate = System.DateTime.Now;
                        DestinationProfileDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName);
                        DlpDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLPText) + EndDate.Month.ToString("00") + EndDate.Day.ToString("00") + EndDate.Hour.ToString("00") + EndDate.Minute.ToString("00"));
                        _presenterAnalyze.CreateDestination();
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
            Response.Redirect(string.Format("COValidationCallback.aspx?projectid={0}&result={1}&id={2}", SessionManager.Instance.CurrentProject.ProjectId, Convert.ToString(hdnValidationMeasurementStatus.Value), Guid.NewGuid()));
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
        //public int JsRandomNumber { get { return new Random().Next(1, 100); } }
        public string JsRandomNumber { get { return Guid.NewGuid().ToString(); } }

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
                return ((SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D || SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1DMultiColor)); //------Sync1DMultiColor
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
        public bool IsSyncDLPPlusType
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.DLPPlus);
            }
        }
        public bool IsDlpMultiColor { get; set; }
        public bool IsSync1DMultiColor { get; set; }
        public string CurrentRunname { get; set; }
        public int LicenseTypeId { get; set; }
        public DataTable dtMeasurement { get; set; }
        public decimal Dec_E_Value { get; set; }
        public int CurrentRefChartTypeId { get; set; }
        public bool IsDestinationProfile { get; set; }
        public List<ProjectTargetLAB> ProjectTargetLABList { get; set; }
        public bool EnabledPaperScaling { get; set; }
        public int ProjectTypeId { get; set; }
        public int PrintstandardTypeId { get; set; }
        public MeasurementPatchDetails measurementPatchDetails { get; set; }
        public bool IsUpdateNewPaper { get; set; }
        public bool IsUpdateDelta { get; set; }
        #endregion
    }
}