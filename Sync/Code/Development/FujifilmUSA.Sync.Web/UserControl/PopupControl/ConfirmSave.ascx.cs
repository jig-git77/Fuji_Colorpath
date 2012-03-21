using System;
using System.Linq;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ConfirmSave : System.Web.UI.UserControl, IViewConfirmSave
    {
        private PresenterConfirmSave _presenterConfirmSave;

        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterConfirmSave = new PresenterConfirmSave(this);
        }

        protected void lbtnOptimize_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            _presenterConfirmSave.GetMeasurementsOnRunId();
            _presenterConfirmSave.OptimizeDlp();

            _presenterConfirmSave.GetProjectDetail();
            SessionManager.Instance.CurrentProject = Project;
            SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.Last();
            Utilities.PopupWindowsRedirect(this, "Export.aspx");
        }

        protected void lbtnYes_Click(object sender, EventArgs e)
        {
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            _presenterConfirmSave.CloseOptimizeRun();
            Utilities.PopupWindowsRedirect(this, "ProjectList.aspx");
        }

        #region IViewConfirmSave Members
        public int ProjectId { get; set; }
        public int OptimizeRunId { get; set; }
        public List<int> MeasurementIds { get; set; }
        public Entity.Project Project { get; set; }
        #endregion
    }
}
