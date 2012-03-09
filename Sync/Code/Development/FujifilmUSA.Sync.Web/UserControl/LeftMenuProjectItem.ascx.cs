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
    public partial class LeftMenuProjectItem : System.Web.UI.UserControl
    {
        #region Variable Declarations
     
        //private PresenterLeftMenu _presenterLeftMenu = null;
        public LeftMenuItemLevel ItemLevel { get; set; }
        public string ItemName { get; set; }
        public OptimizeRun CurrentSyncRun { get; set; }
        public bool IsLastRun
        {
            get { return (this.CurrentSyncRun.OptimizeRunId == SessionManager.Instance.CurrentProject.OptimizeRuns.Last().OptimizeRunId); }
        }
        
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            //_presenterLeftMenu = new PresenterLeftMenu(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            lnkSyncRunName.Text = ItemName;

            switch (ItemLevel)
            {
                case LeftMenuItemLevel.ProjectSetup:
                    ulSyncSubMenu.Visible = false;
                    break;
                case LeftMenuItemLevel.SyncRun:
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId == CurrentSyncRun.OptimizeRunId)
                    {
                        lnkMeasurement.Visible = false;
                        lnkAnalyse.Visible = false;
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 1)
                        {
                            lnkExport.Visible = false;
                            lnkSyncRunName.Enabled = false;
                        }
                    }
                    else
                    {
                        //lnkExport.Enabled = CurrentSyncRun.HasProfile;
                    }

                    if (CurrentSyncRun.Status == Convert.ToInt16(OptimizeRunStatus.Closed))
                    {
                        lnkMeasurement.Visible = false;
                    }

                    if (CurrentSyncRun != null && SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == CurrentSyncRun.OptimizeRunId)
                    {
                        lnkMeasurement.Font.Bold = SessionManager.Instance.CurrentNavigation == LeftMenuItemLevel.Measure;
                        lnkAnalyse.Font.Bold = SessionManager.Instance.CurrentNavigation == LeftMenuItemLevel.Analyze;
                        lnkExport.Font.Bold = SessionManager.Instance.CurrentNavigation == LeftMenuItemLevel.Download;
                    }
                    if (IsLastRun)
                    {
                        lnkAnalyse.Attributes.Add("onclick", "return CheckMeasurement();");
                    }
                    break;
            }
        }
        #endregion

        #region Control events
        protected void lnkItem_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                var lnkItem = sender as LinkButton;
                if (lnkItem != null)
                {
                    SessionManager.Instance.CurrentOptimizeRun = this.CurrentSyncRun;
                    if (lnkItem.ID == "lnkMeasurement")
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
                    SessionManager.Instance.CurrentOptimizeRun = this.CurrentSyncRun;
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId == this.CurrentSyncRun.OptimizeRunId)
                    {
                        Response.Redirect("SyncRunExport.aspx");
                    }
                    Response.Redirect(
                        this.CurrentSyncRun.Status == Convert.ToInt16(OptimizeRunStatus.Open)
                            ? "AddMeasurement.aspx"
                            : "SyncRunAnalyze.aspx");
                    Response.Redirect("AddProject.aspx");
                    break;
            }
        }
        #endregion
    }
}