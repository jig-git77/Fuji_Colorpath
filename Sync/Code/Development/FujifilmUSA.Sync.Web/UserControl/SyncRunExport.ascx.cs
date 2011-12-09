using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SyncRunExport : System.Web.UI.UserControl, IViewExportProfile
    {
        private PresenterExportProfile _presenterExportProfile;

        protected override void OnInit(EventArgs e)
        {
            _presenterExportProfile = new PresenterExportProfile(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
        }

        protected void btnCloseRun_Click(object sender, EventArgs e)
        {
            SyncRunId = SessionManager.Instance.CurrentSyncRun.SyncRunId;
            Status = Convert.ToInt32(FujifilmUSA.Sync.Entity.ECPSyncRunStatus.Closed);
            EndDateTime = DateTime.UtcNow;
            _presenterExportProfile.CloseSyncRunProcess();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            SyncRunId = SessionManager.Instance.CurrentSyncRun.SyncRunId;
            MeasurementIds = string.Empty; //TODO: Add Measurement Ids
            _presenterExportProfile.OptimizeProfile();
        }
        
        #region IViewExportProfile Members

        public int SyncRunId { get; set; }

        public int Status { get; set; }

        public DateTime EndDateTime { get; set; }

        Entity.ECPSyncRunStatus IViewExportProfile.Status { get; set; }

        public int ProjectId { get; set; }

        public string MeasurementIds { get; set; }

        #endregion
    }
}