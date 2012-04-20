using System;
using System.Linq;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        protected void lbtnOptimize_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            _presenterConfirmSave.GetMeasurementsOnRunId();
            if (!IsErrorOccured)
            {
                ProfileDescription = SessionManager.Instance.CurrentDevice.DeviceName + " " + SessionManager.Instance.CurrentProject.ProposedDlpName;
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
        public List<int> MeasurementIds { get; set; }
        public Entity.Project Project { get; set; }
        public string ProfileDescription { get; set; }
        #endregion
    }
}
