using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;

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

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentSyncRun.Status == Convert.ToInt16(ECPSyncRunStatus.Closed))
            {
                btnCloseRun.Enabled = false;
            }
        }

        protected void btnCloseRun_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            SyncRunId = SessionManager.Instance.CurrentSyncRun.SyncRunId;
            Status = ECPSyncRunStatus.Closed;
            EndDateTime = DateTime.UtcNow;
            _presenterExportProfile.CloseSyncRunProcess();
            SessionManager.Instance.CurrentProject = null;
            SessionManager.Instance.CurrentSyncRun = null;
            Response.Redirect("ProjectList.aspx");
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            SyncRunId = SessionManager.Instance.CurrentSyncRun.SyncRunId;
            _presenterExportProfile.GetIccProfileBySyncRunId();
            //Prompt for download file
            if (IccProfile != null && IccProfile.Length > 0)
                FujifilmUSA.Sync.Common.Utilities.DownloadFile(IccProfile,"Profile.icc");
        }

        #region IViewExportProfile Members
        public int SyncRunId { get; set; }
        public ECPSyncRunStatus Status { get; set; }
        public DateTime EndDateTime { get; set; }
        public int ProjectId { get; set; }
        public string MeasurementIds { get; set; }
        public byte[] IccProfile { get; set; }
        #endregion
    }
}
