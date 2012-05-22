using System;
using System.Linq;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ClosePrintCondition : BaseControl, IViewClosePrintCondition
    {
        #region "Declare Variable"
        private PresenterClosePrintCondition _presenterClosePrintCondition;
        #endregion

        #region "Event"
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterClosePrintCondition = new PresenterClosePrintCondition(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblClosePrintConditionMessage.Text = string.Format("Options for \"{0}\" print condition", Request.QueryString["ProjectName"]);
                
                //----------------If LicenseTypeID = 3 (Ink Optimization), then they can not create optimize runs, they has to close after 1st run--------------------//
                if (Convert.ToInt32(SessionManager.Instance.CurrentOperation) == 3)
                {
                    lbtnReOptimize.Visible = false;
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterClosePrintCondition.GetProjectDetail();
                if (!IsErrorOccured && Project != null)
                {
                    ((Label)updateProgress.FindControl("ucCreateDLP").FindControl("lblMessage")).Text = string.Format(
                        "Once optimization is complete, apply {0} to workflow and reprint the {1} chart.",
                        Project.OptimizeRuns.Last().DlpName,
                        ((Project.OptimizeRuns != null)
                             ? Project.OptimizeRuns.Last().ReferenceChartType.Value
                             : string.Empty));
                }
            }
        }
        protected void lbtnReOptimize_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                DeviceName = SessionManager.Instance.CurrentDevice.DeviceName;
               
                if (!IsErrorOccured)
                {
                    _presenterClosePrintCondition.CreateOptimizeRun();
                    if (!IsErrorOccured)
                    {
                        _presenterClosePrintCondition.GetProjectDetail();
                        if (!IsErrorOccured)
                        {
                            SessionManager.Instance.CurrentProject = Project;
                            SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                            SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='Measurement.aspx';", true);
                        }
                    }
                }
            }
        }
        protected void lbtnViewLastAnalysis_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterClosePrintCondition.GetProjectDetail();
                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                    SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1)
                    {
                        SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.New;
                    }
                    SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                    Utilities.PopupWindowsRedirect(this, "Analyze.aspx");
                }
            }
        }
        protected void lbtnDownloadCurrentDLP_Click(object sender, EventArgs e)
        {
            ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
            _presenterClosePrintCondition.GetCurrentDeviceLinkProfile();
            if (!IsErrorOccured)
            {
                //CurrentDlp = null;
                if (CurrentDlp != null)
                {
                    Utilities.DownloadFile(CurrentDlp, "Profile.icc");
                }
                else
                {
                    fujiStatusbar.DisplayMessage("Sorry!There is no dlp to download", false);
                }
            }
        }
        #endregion

        #region IViewClosePrintCondition Members

        public int ProjectId { get; set; }

        public byte[] CurrentDlp { get; set; }

        public Entity.Project Project { get; set; }

        public string DeviceName { get; set; }
        
        #endregion
    }
}