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
    public partial class InkOptSourceMiddle : BaseControl, IViewAnalyze
    {
        private PresenterAnalyze _presenterAnalyze;
        public DateTime EndDate;
        private const string _recDLPText = "dlpText";
        private const string _recDLPMsgText = "dlpMsgText";
        private const string _reclocCreateSourceProfile = "locCreateSourceProfile";
        private const string _reclocCreateReProfileSourceProfile = "locCreateSourceProfile";   

        public string CreateDLPUpdateProgressClientID
        {
            get { return updateProgress.ClientID; }
        }
        public string UpdateProgressClientID
        {
            get { return ((UpdateProgress)Page.Master.FindControl("UpdateProgress")).ClientID; }
        }
      
        protected override void OnInit(EventArgs e)
        {
            _presenterAnalyze = new PresenterAnalyze(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.CustomSourceProfile) //--If "Custom Source Profile", then change button name
            {
                locCreateDLP.Text = GetResourceString(_reclocCreateSourceProfile);
            }
            else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.ReProfileCustomSource) //--If "ReProfile Custom Source", then change button name
            {
                locCreateDLP.Text = GetResourceString(_reclocCreateReProfileSourceProfile);
            }
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimizationSource || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.CustomSourceProfile || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.ReProfileCustomSource) //---If  "Ink Optimization Source" project type
            {
                lbtnReTargetProject.Visible = false;
                lbtnSpotProjectChartCreation.Visible = false;
            }
            else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
            {
                lbtnReTargetProject.Visible = false;
                lbtnDownloadAndSaveDLP.Visible = false;
                lbtnSpotProjectChartCreation.Visible = true;
            }
            else
            {
                lbtnDownloadAndSaveDLP.Visible = false;
                lbtnSpotProjectChartCreation.Visible = false;
            }
        }

        protected void lbtnDownloadAndSaveDLP_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;

            _presenterAnalyze.GetMeasurementsOnRunId();
            if (!IsErrorOccured)
            {
                EndDate = System.DateTime.Now;
                DestinationProfileDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName);
                DlpDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName);
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.CustomSourceProfile || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.ReProfileCustomSource) //---If custom Source profile then call CreateSourceProfile function----//
                {
                    _presenterAnalyze.CreateCustomSourceProfileDlp();
                }
                else   //--call SourcetoSource function----//
                {
                    _presenterAnalyze.CreateSourceToSourceDlp();
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


        protected void lbtnReTargetProject_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            EndDate = System.DateTime.Now;

            _presenterAnalyze.CreateReTargetProject();

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

        protected void lbtnSpotProjectChartCreation_Click(object sender, EventArgs e)
        {
            //----------Place chart generation service code here--------------//
            Response.Write("1");
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
        public List<Entity.AnalyzeChart_DotGain> DTISOTVIChart { get; set; }
        public List<Entity.AnalyzeChart_DotGain> DTSpreadChartData { get; set; }
        public List<Entity.AnalyzeChart_DotGain> DTSpreadChartData_CMYK { get; set; }
        public List<Entity.AnalyzeChart_DotGain> DTSpreadSplineRangeChartData { get; set; }
        public DataTable DTMaxSpreadChartData { get; set; }

        public PCSDataTypes PCSDataTypes { get; set; }
        public DensityTypes DensityType { get; set; }

        public bool IsAverageOfMeasurements { get; set; }
        public bool Is1DMeasurementSmoothing { get; set; }
        public int ProjectStatus { get; set; }
        public Project Project { get; set; }
        public int SpotProjectId { get; set; }
        public bool IsUsed { get; set; }
        #endregion


    }
}