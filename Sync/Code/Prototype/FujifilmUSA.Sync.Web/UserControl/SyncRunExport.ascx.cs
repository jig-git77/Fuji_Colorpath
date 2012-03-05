using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SyncRunExport : System.Web.UI.UserControl, IViewExportProfile
    {
        #region Variable Declarations
        private PresenterExportProfile _presenterExportProfile;
        #endregion

        #region Page Method
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
        #endregion

        #region Control Events
        
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
            if (Profile != null && Profile.Data.Profile != null)
                Utilities.DownloadFile(Profile.Data.Profile, "Profile.icc");
        }

        #endregion

        #region Error Handling Method
        public void ErrorOccured(object sender, Business.Interface.ErrorArgs e)
        {
            ucStatusbar1.DisplayMessage(e.ErrorMessage, false);
        }
        #endregion

        //*******************************************************************//

        #region IViewExportProfile Members

        public ECPSyncRunStatus Status { get; set;}

        public int SyncRunId { get; set;}
        
        public DateTime EndDateTime { get; set;}
        
        public int ProjectId { get; set;}
        
        #endregion

        #region IViewBaseProfile Members

        public IBaseProfile Profile { get; set;}
        
        #endregion
    }
}
