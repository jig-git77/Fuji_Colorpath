using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProductList : System.Web.UI.UserControl, IViewProjectList
    {
        #region "Declaration"
        private PresenterProjectList _presenterprojectlist;
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presenterprojectlist = new PresenterProjectList(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();            
        }

        protected void grdProjectList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteProject")
            { 
                GridDataItem selectedItem = e.Item as GridDataItem;
                this.ProjectId = Convert.ToInt32(selectedItem.GetDataKeyValue("ProjectId"));
                _presenterprojectlist.DeleteProject();

                //Rebind the grid to refresh the data
                grdProjectList.Rebind();
            }
        }

        protected void grdProjectList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.DataItem != null && e.Item.ItemType == GridItemType.Item)
            {
                Project project = e.Item.DataItem as Project;
                if (project != null)
                {
                    System.Web.UI.WebControls.Image imgStatus = e.Item.FindControl("imgStatus") as System.Web.UI.WebControls.Image;
                    if (imgStatus != null)
                    {
                        switch (project.ProjectStatus)
                        {
                            case ECPSyncRunStatus.Open: imgStatus.ImageUrl = ResolveUrl("../Images/edit_icon.png"); break;
                            case ECPSyncRunStatus.Certify: imgStatus.ImageUrl = ResolveUrl("../Images/right_icon   .png"); break;
                            case ECPSyncRunStatus.Closed: imgStatus.ImageUrl = ResolveUrl("../Images/close_icon.png"); break;
                            case ECPSyncRunStatus.NotUsed: break; //not to be used currently
                        }
                    }

                    System.Web.UI.WebControls.ImageButton imgbtnDelete = e.Item.FindControl("imgbtnDelete") as System.Web.UI.WebControls.ImageButton;
                    if (imgbtnDelete != null)
                    {
                        imgbtnDelete.Attributes.Add("onclick", "javascript: return ConfirmDeleteProject();");
                    }
                }
            }
        }

        protected void grdProjectList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            BindProject();
        }
        #endregion

        #region Method
        private void BindProject()
        {
            if (SessionManager.Instance.CurrentSyncDevice != null)
            {
                this.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
                _presenterprojectlist.GetProjectListBySyncDeviceId();
                grdProjectList.DataSource = GetProjectListBySyncDeviceId;
                //grdProjectList.DataBind();
            }
        }      
        #endregion

        #region IViewProjectList Members
        public int SyncDeviceId { get; set; }
        public List<Project> GetProjectListBySyncDeviceId { get; set; }
        public int ProjectId { get; set; }
        public int DeleteProject { get; set; }
        public Project GetProjectByProjectId { get; set; }
        #endregion
    }
}