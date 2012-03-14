using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

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
            _presenterConfirmSave.OptimizeDlp();
            Response.Redirect("Export.aspx");
        }

        protected void lbtnYes_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectList.aspx");
        }

        #region IViewConfirmSave Members
        public int ProjectId { get; set; }
        public int OptimizeRunId { get; set; }
        #endregion
    }
}