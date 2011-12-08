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
            if (!IsPostBack)
                BindProject();
        }       
        #endregion

        #region Control events
        protected void lbtnProjectName_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.LinkButton lbtnProjectName = sender as System.Web.UI.WebControls.LinkButton;
            this.ProjectId = Convert.ToInt32(lbtnProjectName.Attributes["projectid"].ToString());
            _presenterprojectlist.GetProjectByProjectId();
            SessionManager.Instance.CurrentProject = GetProjectByProjectId;
            Response.Redirect("AddMeasurement.aspx");
        }

        #region Telerik grid
        //protected void grdProjectList_ItemCommand(object sender, GridCommandEventArgs e)
        //{
        //    if (e.CommandName == "DeleteProject")
        //    {
        //        GridDataItem selectedItem = e.Item as GridDataItem;
        //        this.ProjectId = Convert.ToInt32(selectedItem.GetDataKeyValue("ProjectId"));
        //        _presenterprojectlist.DeleteProject();

        //        //Rebind the grid to refresh the data
        //        grdProjectList.Rebind();
        //    }
        //}

        //protected void grdProjectList_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item.DataItem != null)
        //    {
        //        Project project = e.Item.DataItem as Project;
        //        if (project != null)
        //        {
        //            System.Web.UI.WebControls.Image imgStatus = e.Item.FindControl("imgStatus") as System.Web.UI.WebControls.Image;
        //            if (imgStatus != null)
        //            {
        //                switch (project.ProjectStatus)
        //                {
        //                    case ECPSyncRunStatus.Open: imgStatus.ImageUrl = ResolveUrl("../Images/edit_icon.png");
        //                        imgStatus.ToolTip = "Open";
        //                        imgStatus.AlternateText = "Open";
        //                        break;
        //                    case ECPSyncRunStatus.Certify: imgStatus.ImageUrl = ResolveUrl("../Images/right_icon   .png");
        //                        imgStatus.ToolTip = "Certify";
        //                        imgStatus.AlternateText = "Certify";
        //                        break;
        //                    case ECPSyncRunStatus.Closed: imgStatus.ImageUrl = ResolveUrl("../Images/close_icon.png");
        //                        imgStatus.ToolTip = "Closed";
        //                        imgStatus.AlternateText = "Closed";
        //                        break;
        //                    case ECPSyncRunStatus.NotUsed: break; //not to be used currently
        //                }
        //            }

        //            System.Web.UI.WebControls.ImageButton imgbtnDelete = e.Item.FindControl("imgbtnDelete") as System.Web.UI.WebControls.ImageButton;
        //            if (imgbtnDelete != null)
        //            {
        //                imgbtnDelete.Attributes.Add("onclick", "javascript: return ConfirmDeleteProject();");
        //            }

        //            System.Web.UI.WebControls.LinkButton lbtnProjectName = e.Item.FindControl("lbtnProjectName") as System.Web.UI.WebControls.LinkButton;
        //            if (lbtnProjectName != null)
        //            {
        //                lbtnProjectName.Attributes.Add("ProjectId", project.ProjectId.ToString());
        //            }

        //            System.Web.UI.WebControls.ImageButton btnInfo = e.Item.FindControl("btnInfo") as System.Web.UI.WebControls.ImageButton;
        //            if (btnInfo != null)
        //            {
        //                btnInfo.Attributes.Add("onclick", "javascript: return OpenInformationBox('"+ ResolveUrl("~/ProjectInformation.aspx") +"');");

        //            }
        //        }
        //    }
        //}

        //protected void grdProjectList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    BindProject();
        //} 
        #endregion

        protected void grdProjectList_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                Project project = e.Item.DataItem as Project;
                if (project != null)
                {
                    System.Web.UI.WebControls.Image imgStatus = e.Item.FindControl("imgStatus") as System.Web.UI.WebControls.Image;
                    if (imgStatus != null)
                    {
                        switch (project.ProjectStatus)
                        {
                            case ECPSyncRunStatus.Open: imgStatus.ImageUrl = ResolveUrl("../Images/edit_icon.png");
                                imgStatus.ToolTip = "Open";
                                imgStatus.AlternateText = "Open";
                                break;
                            case ECPSyncRunStatus.Certify: imgStatus.ImageUrl = ResolveUrl("../Images/right_icon   .png");
                                imgStatus.ToolTip = "Certify";
                                imgStatus.AlternateText = "Certify";
                                break;
                            case ECPSyncRunStatus.Closed: imgStatus.ImageUrl = ResolveUrl("../Images/close_icon.png");
                                imgStatus.ToolTip = "Closed";
                                imgStatus.AlternateText = "Closed";
                                break;
                            case ECPSyncRunStatus.NotUsed: break; //not to be used currently
                        }
                    }

                    System.Web.UI.WebControls.ImageButton imgbtnDelete = e.Item.FindControl("imgbtnDelete") as System.Web.UI.WebControls.ImageButton;
                    if (imgbtnDelete != null)
                    {
                        imgbtnDelete.Attributes.Add("onclick", "javascript: return ConfirmDeleteProject();");
                    }

                    System.Web.UI.WebControls.LinkButton lbtnProjectName = e.Item.FindControl("lbtnProjectName") as System.Web.UI.WebControls.LinkButton;
                    if (lbtnProjectName != null)
                    {
                        lbtnProjectName.Attributes.Add("ProjectId", project.ProjectId.ToString());
                    }

                    System.Web.UI.WebControls.ImageButton btnInfo = e.Item.FindControl("btnInfo") as System.Web.UI.WebControls.ImageButton;
                    if (btnInfo != null)
                    {
                        btnInfo.Attributes.Add("onclick", "javascript: return OpenInformationBox('" + ResolveUrl("~/ProjectInformation.aspx") + "');");

                    }
                }
            }
        }

        protected void grdProjectList_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteProject")
            {
                //GridDataItem selectedItem = e.Item as GridDataItem;
                this.ProjectId = Convert.ToInt32(e.CommandArgument); //Convert.ToInt32(selectedItem.GetDataKeyValue("ProjectId"));
                _presenterprojectlist.DeleteProject();

                ////Rebind the grid to refresh the data
                //grdProjectList.Rebind();
                BindProject();
            }
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
                grdProjectList.DataBind();
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