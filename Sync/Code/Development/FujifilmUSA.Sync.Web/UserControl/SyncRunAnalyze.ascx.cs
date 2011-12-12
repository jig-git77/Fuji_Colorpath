using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SyncRunAnalyze : System.Web.UI.UserControl, IViewSyncRunAnalyze
    {
        private PresenterSyncRunAnalyze _presenterSyncRunAnalyze;

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenterSyncRunAnalyze = new PresenterSyncRunAnalyze(this);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Redirect("SyncRunExport.aspx");
        }

        protected void btnOptimize_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            SyncRunId = SessionManager.Instance.CurrentSyncRun.SyncRunId;
            MeasurementIds = string.Empty; //TODO: Add Measurement Ids
            _presenterSyncRunAnalyze.OptimizeProfile();
            Response.Redirect("SyncRunExport.aspx");
        }

        #region IViewSyncRunAnalyze Members

        public int SyncRunId { get; set; }

        public int ProjectId { get; set; }

        public string MeasurementIds { get; set; }

        #endregion
    }
}