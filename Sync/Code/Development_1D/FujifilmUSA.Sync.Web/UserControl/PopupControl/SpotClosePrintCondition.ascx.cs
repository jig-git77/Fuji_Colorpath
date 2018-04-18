using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class SpotClosePrintCondition : BaseControl, IViewClosePrintCondition
    {
        #region "Declare Variable"
        private PresenterClosePrintCondition _presenterClosePrintCondition;
        private const string _recprintOptionsText = "printOptionsText";
        private const string _recoptimizeCompleteText = "optimizeCompleteText";
        private const string _recnoNCPText = "noNCPText";
        private const string _recnoSourceText = "noSourceText";

        #endregion

        #region "Constructor"
        public SpotClosePrintCondition()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
            this.PreRender += (sender, e) => this.ExceptionHandling(Page_PreRender, sender, e);
        }
        #endregion

        #region Property
        private ELicenseType CurrentLicenseType
        {
            get
            {
                if (Request.QueryString["LicenseTypeId"] != null && Request.QueryString["LicenseTypeId"].Length > 0)
                    return (ELicenseType)Convert.ToInt32(Request.QueryString["LicenseTypeId"]);
                else
                    return ELicenseType.None;
            }
        }
        #endregion

        #region "Event"
        protected override void OnInit(EventArgs e)
        {
            _presenterClosePrintCondition = new PresenterClosePrintCondition(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblClosePrintConditionMessage.Text = string.Format(GetResourceString(_recprintOptionsText), Request.QueryString["ProjectName"]);

                if (CurrentLicenseType == ELicenseType.InkOptimization) //-------If LicenseTypeID = 3 (Ink Optimization), then they can not create optimize runs, they has to close after 1st run--------------------//
                {
                    lbtnReOptimize.Visible = false;
                }
                else if (CurrentLicenseType == ELicenseType.InkOptimizationSource) //-------If LicenseTypeID = 4 (Ink Optimization Source), then they can not create optimize runs, they has to close after 1st run--------------------//
                {
                    lbtnReOptimize.Visible = false;
                    lbtnViewLastAnalysis.Visible = false;
                    //lbtnViewDetail.Visible = true;
                }
                else if (CurrentLicenseType == ELicenseType.Sync4DCertification) //---If Sync 4D Certification type of project, then they can not create optimize runs or Download DLP since there is no DLP
                {
                    lbtnReOptimize.Visible = false;
                    //lbtnDownloadCurrentDLP.Visible = false;
                }
                else if (CurrentLicenseType == ELicenseType.CustomSourceProfile || CurrentLicenseType == ELicenseType.ReProfileCustomSource)  //-------If LicenseTypeID = 6 (Custom Source Profile) or LicenseTypeID = 7 (ReProfile Custom Source), then they can not create optimize runs, they has to close after 1st run--------------------//
                {
                    //lbtnDownloadCurrentDLP.Visible = false;
                    lbtnReOptimize.Visible = false;
                    lbtnViewLastAnalysis.Visible = false;
                    //lbtnDownloadSourceProf.Visible = true;
                    //lbtnViewDetail.Visible = true;
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                SpotProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterClosePrintCondition.GetProjectDetail_SpotProject();
                if (!IsErrorOccured && Project != null)
                {
                    ((Label)updateProgress.FindControl("ucCreateDLP").FindControl("lblMessage")).Text = string.Format(
                        GetResourceString(_recoptimizeCompleteText),
                        Project.SpotTargetProject.OptimizeRuns.Last().DlpName,
                        ((Project.SpotTargetProject.OptimizeRuns != null)
                             ? Project.SpotTargetProject.OptimizeRuns.Last().ReferenceChartType.Value
                             : string.Empty));
                }
            }
        }

        protected void lbtnReOptimize_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                SpotProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterClosePrintCondition.CreateOptimizeRun_SpotProject();
                if (!IsErrorOccured)
                {
                    _presenterClosePrintCondition.GetProjectDetail_SpotProject();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last();
                        SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='Spot_Measurement.aspx';", true);
                    }
                }
            }
        }
        protected void lbtnViewLastAnalysis_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                SpotProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterClosePrintCondition.GetProjectDetail_SpotProject();
                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentOptimizeRun = Project.SpotTargetProject.OptimizeRuns.Last();
                    SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.SpotLibrary;
                    SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                    Utilities.PopupWindowsRedirect(this, "Analyze_SpotColor.aspx");
                }
            }
        }

        protected void lbtnDownloadCurrentDLP_Click(object sender, EventArgs e)
        {
            string DLPName = "NCPProfile.icc";
            if (Request.QueryString["DlpName"] != null && Convert.ToString(Request.QueryString["DlpName"]) != "")
            {
                DLPName = Request.QueryString["DlpName"].ToString();
            }

            SpotProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
            _presenterClosePrintCondition.GetCurrentNamedColorProfile();
            if (!IsErrorOccured)
            {
                //CurrentDlp = null;
                if (CurrentDlp != null)
                {
                    Utilities.DownloadFile(CurrentDlp, DLPName);
                }
                else
                {
                    fujiStatusbar.DisplayMessage(GetResourceString(_recnoNCPText), false);
                }
            }
        }
        #endregion

        #region IViewClosePrintCondition Members

        public int ProjectId { get; set; }

        public byte[] CurrentDlp { get; set; }

        public Entity.Project Project { get; set; }

        public string DeviceName { get; set; }
        public int SpotProjectId { get; set; }

        #endregion
    }
}