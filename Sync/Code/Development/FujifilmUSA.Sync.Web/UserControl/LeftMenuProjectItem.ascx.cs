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
                    ulSyncSubMenu.Visible = true;
                    lnkSyncRunName.OnClientClick = "return false";
                    break;
                case LeftMenuItemLevel.NewSyncRun:
                    ulSyncSubMenu.Visible = false;
                    break;
                
            }
            lnkSyncRunName.Text = ItemName;
        }

        protected void lnkItem_Click(object sender, EventArgs e)
        {
            if(sender != null)
            {
                var lnkItem = sender as LinkButton;
                if(lnkItem != null)
                {
                    if(lnkItem.ID == "lnkMeasurement")
                    {
                        Response.Redirect("AddMeasurement.aspx");
                    }
                    else if (lnkItem.ID == "lnkAnalyse")
                    {
                        Response.Redirect("AnalyseMeasurements.aspx");
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
                case LeftMenuItemLevel.NewSyncRun:
                    ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                    Status = ECPSyncRunStatus.Open;
                    CreatedDateTime = DateTime.UtcNow;
                    _presenterLeftMenu.AddProjectSyncRun();
                    SessionManager.Instance.CurrentProject = this.CurrentProject;
                    SessionManager.Instance.CurrentSyncRun = SessionManager.Instance.CurrentProject.ProjectRuns.SingleOrDefault(x=>x.Status == Convert.ToInt16(ECPSyncRunStatus.Open));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region IViewLeftMenu Members

        public int ProjectId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ECPSyncRunStatus Status
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime CreatedDateTime
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Project CurrentProject { get; set; }
        
        #endregion
    }
}