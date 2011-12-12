using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class LeftMenuProjectItem : System.Web.UI.UserControl, IViewLeftMenu
    {
        private PresenterLeftMenu _presenterLeftMenu = null;
        public LeftMenuItemLevel ItemLevel { get; set; }
        public string ItemName { get; set; }
        public SyncRun CurrentSyncRun { get; set; }

        protected override void OnInit(EventArgs e)
        {
            _presenterLeftMenu = new PresenterLeftMenu(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            switch (ItemLevel)
            {
                case LeftMenuItemLevel.ProjectSetup:
                    ulSyncSubMenu.Visible = false;
                    break;
                case LeftMenuItemLevel.SyncRun:
                    if(SessionManager.Instance.CurrentProject.ProjectRuns.First().SyncRunId == CurrentSyncRun.SyncRunId)
                    {
                        lnkMeasurement.Visible = false;
                        lnkAnalyse.Visible = false;
                    }
                    //if (CurrentSyncRun.Status == Convert.ToInt16(ECPSyncRunStatus.Closed))
                    //{
                    //    lnkMeasurement.Visible = false;
                    //}
                    ulSyncSubMenu.Visible = true;
                    break;
                case LeftMenuItemLevel.NewSyncRun:
                    ulSyncSubMenu.Visible = false;
                    break;
                
            }
            lnkSyncRunName.Text = ItemName;

            if(CurrentSyncRun != null && SessionManager.Instance.CurrentSyncRun.SyncRunId == CurrentSyncRun.SyncRunId)
            {
                lnkSyncRunName.Font.Bold = true;
            }
        }

        protected void lnkItem_Click(object sender, EventArgs e)
        {
            if(sender != null)
            {
                var lnkItem = sender as LinkButton;
                if(lnkItem != null)
                {
                    SessionManager.Instance.CurrentSyncRun = this.CurrentSyncRun;
                    if(lnkItem.ID == "lnkMeasurement")
                    {
                        Response.Redirect("AddMeasurement.aspx");
                    }
                    else if (lnkItem.ID == "lnkAnalyse")
                    {
                        Response.Redirect("SyncRunAnalyze.aspx");
                    }
                    else if (lnkItem.ID == "lnkExport")
                    {
                        Response.Redirect("SyncRunExport.aspx");
                    }
                }
            }
        }

        protected void lnkSyncRunName_Click(object sender, EventArgs e)
        {
            switch (ItemLevel)
            {
                case LeftMenuItemLevel.ProjectSetup:
                    Response.Redirect("AddProject.aspx");
                    break;
                case LeftMenuItemLevel.SyncRun:
                    SessionManager.Instance.CurrentSyncRun = this.CurrentSyncRun;
                    if (SessionManager.Instance.CurrentProject.ProjectRuns.First().SyncRunId == this.CurrentSyncRun.SyncRunId)
                    {
                        Response.Redirect("SyncRunExport.aspx");
                    }
                    Response.Redirect(
                        this.CurrentSyncRun.Status == Convert.ToInt16(ECPSyncRunStatus.Open)
                            ? "AddMeasurement.aspx"
                            : "SyncRunAnalyze.aspx");
                    Response.Redirect("AddProject.aspx");
                    break;
                case LeftMenuItemLevel.NewSyncRun:
                    ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                    Status = ECPSyncRunStatus.Open;
                    CreatedDateTime = DateTime.UtcNow;
                    _presenterLeftMenu.AddProjectSyncRun();
                    SessionManager.Instance.CurrentProject = this.CurrentProject;
                    SessionManager.Instance.CurrentSyncRun = this.CurrentProject.ProjectRuns.Last();
                    Response.Redirect("AddMeasurement.aspx");
                    break;
            }
        }

        #region IViewLeftMenu Members

        public int ProjectId { get; set; }
        
        public ECPSyncRunStatus Status { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
        
        public Project CurrentProject { get; set; }
        
        #endregion
    }
}