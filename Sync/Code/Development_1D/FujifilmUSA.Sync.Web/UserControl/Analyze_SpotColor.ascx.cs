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
        private const string _recCreateDLP = "locCreateNCPResource1";
        private const string _recNext = "nextText";
        private const string _recnewPrintCondition = "newNCPPrintConditionText";
        private const string _recAnalyze = "analyze";
        public string DLPName = "";
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
                    hdnBaseServiceUrl.Value = Utilities.ReadConfigValue("BaseServiceURL");
                    OptimizeRunId = Convert.ToInt32(SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId);
                    hdfOptimizationRunID.Value = OptimizeRunId.ToString();
                    hdnIsNewNCP.Value=IsNewNCP.ToString();
                    hdnIsNcpCreated.Value = IsNCPCreated.ToString();
                    hdfCurrentCulture.Value = getCurrentCultureLanguage();
                }

                if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.First().OptimizeRunId)
                {
                    header.InnerText = GetResourceString(_recnewPrintCondition);//"New Print Condition - Analyze (No DLP)";
                    DLPName = "No NCP";
                }
                else
                {
                    header.InnerText = string.Format(GetResourceString(_recAnalyze), SessionManager.Instance.CurrentOptimizeRun.LastDlpName);//Analyze - {0}
                    DLPName = SessionManager.Instance.CurrentOptimizeRun.LastDlpName.Replace(SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName + " ", ""); //---DLPName to pass into report
                    DLPName = DLPName.Remove(DLPName.Length - 4);
                }

                lbtnDownloadAndSaveNCP.Text = IsNewNCP
                                                  ? "<span><img id='imgAdd' src='Images/export.png'/>" + GetResourceString(_recCreateDLP) + "</span>"
                                                  : "<span><img id='imgAdd' src='Images/save_icon.png'/>" + GetResourceString(_recNext) + "</span>";//Create DLP - Next

                hdHasMeasurement.Value = SessionManager.Instance.CurrentOptimizeRun.HasMeasurements.ToString();

                //------------If this run is closed /certify run, then invisible download(Next) button, because there is no DLP available to download for this run-----//
                if (SessionManager.Instance.CurrentOptimizeRun.Status == EOptimizeRunStatus.Certify)
                    lbtnDownloadAndSaveNCP.Visible = false;
            }
        }

        #endregion



        protected void lbtnDownloadAndSaveNCP_Click(object sender, EventArgs e)
        {
            if (!IsNCPCreated)
            {
                SpotProjectId = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId;
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                ProjectStatus = Convert.ToInt32(EOptimizeRunStatus.Closed);
                _presenterAnalyze.UpdateOptimizeRun_SpotProject();
                if (!IsErrorOccured)
                {
                    _presenterAnalyze.GetProjectDetail_SpotProject();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last();
                        Response.Redirect("Export.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("Export.aspx");
            }
            
        }

        public bool IsNewNCP
        {
            get
            {
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary) //---If Sync spot library type of project
                {
                    return SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Count == 1;
                }
                return false;
            }
        }

        public bool IsNCPCreated
        {
            get { return SessionManager.Instance.CurrentOptimizeRun.HasProfile; }
        }

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
        public int ProjectStatus { get; set; }
        public Project Project { get; set; }
        public int SpotProjectId { get; set; }
        public bool IsUsed { get; set; }
        #endregion
    }
}