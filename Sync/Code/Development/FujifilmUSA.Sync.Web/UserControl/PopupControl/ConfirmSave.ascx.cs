using System;
using System.Linq;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Entity;
using System.Collections;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ConfirmSave : BaseControl, IViewConfirmSave
    {
        private PresenterConfirmSave _presenterConfirmSave;

        public string DLPMessage
        {
            get
            {
                return
                    (SessionManager.Instance.CurrentProject != null && SessionManager.Instance.CurrentOptimizeRun != null)
                        ? string.Format(
                            "Once optimization is complete, apply {0} to workflow and reprint the {1} chart.",
                            SessionManager.Instance.CurrentOptimizeRun.DlpName,
                            SessionManager.Instance.CurrentOptimizeRun.ReferenceChartType.Value)
                        : string.Empty;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterConfirmSave = new PresenterConfirmSave(this);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ((Label)updateProgress.FindControl("ucCreateDLP").FindControl("lblMessage")).Text = DLPMessage;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                //-----------------Checking the Optimization Run limit set in web.config file, when user hits on "No, Optimize" button-------------//
                int i, TotalOptRuns = 0;
                for (i = SessionManager.Instance.CurrentProject.OptimizeRuns.Count - 2; i > 0; i--)
                {
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns[i].Status == EOptimizeRunStatus.Closed && SessionManager.Instance.CurrentProject.OptimizeRuns[i].HasProfile)
                    {
                        TotalOptRuns = TotalOptRuns + 1;
                    }
                    else
                    {
                        break;
                    }
                }               
                hdOptRunLimit.Value = TotalOptRuns.ToString();

                //----------------If CurrentOperation = 3 (Ink Optimization), then they can not create optimize runs, they has to close run at 1st run--------------------//
                if (SessionManager.Instance.CurrentOperation == EOperation.InkOptimization)
                {
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 2)
                    {
                        lbtnOptimize.Visible = false;
                    }
                }
                else
                {
                    //-------------Have to check whether its previous run is Certify Run, if yes, only visible Optimize button to user,
                    // as he can not create any simultanously certify run--------------------//
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 2)
                    {
                        OptimizeRunId = Convert.ToInt32(SessionManager.Instance.CurrentOptimizeRun.MasterRunId);
                        _presenterConfirmSave.GetLastRunStatus();

                        if (LastRunStatus == "Certify")
                        {
                            lbtnYes.Visible = false;
                        }
                    }
                }
            }
        }

        protected void lbtnOptimize_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            _presenterConfirmSave.GetMeasurementsOnRunId();
            if (!IsErrorOccured)
            {
                ProfileDescription = SessionManager.Instance.CurrentProject.ProposedDlpName;
                _presenterConfirmSave.OptimizeDlp();
                if (!IsErrorOccured)
                {
                    _presenterConfirmSave.GetProjectDetail();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.Last();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='Export.aspx';", true);
                    }
                }
            }
        }

        protected void lbtnYes_Click(object sender, EventArgs e)
        {
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            MasterRunId = Convert.ToInt32(SessionManager.Instance.CurrentOptimizeRun.MasterRunId);
            _presenterConfirmSave.CloseOptimizeRun();

            if (!IsErrorOccured)
            {
                SessionManager.Instance.CurrentProject = null;
                SessionManager.Instance.CurrentOptimizeRun = null;
                Utilities.PopupWindowsRedirect(this, "ProjectList.aspx");
            }
        }

        #region IViewConfirmSave Members
        public int ProjectId { get; set; }
        public int OptimizeRunId { get; set; }
        public int MasterRunId { get; set; }
        public List<int> MeasurementIds { get; set; }
        public Entity.Project Project { get; set; }
        public string ProfileDescription { get; set; }
        public string LastRunStatus { get; set; }       
        #endregion
        
    }
}
