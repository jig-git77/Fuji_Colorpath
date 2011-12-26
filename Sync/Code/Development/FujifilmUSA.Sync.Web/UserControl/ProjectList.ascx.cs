using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using System.Linq;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProductList : System.Web.UI.UserControl, IViewProjectList
    {
        #region "Declaration"
        private PresenterProjectList _presenterprojectlist;

        public string ProjectList_DeleteProject_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("ProjectList_DeleteProject_Confirmation");
            }
        }
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presenterprojectlist = new PresenterProjectList(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
            SessionManager.Instance.ProfileTypeID = ProfileType.Optimization;
            if (!IsPostBack)
                BindProject();
        }
        #endregion

        #region Control events
        protected void lbtnProjectName_Click(object sender, EventArgs e)
        {
            var lbtnProjectName = sender as LinkButton;
            if (lbtnProjectName == null) return;

            Project = new Project()
                          {
                              ProjectId = Convert.ToInt32(lbtnProjectName.Attributes["projectid"])
                          };
            _presenterprojectlist.GetProject();
            SessionManager.Instance.CurrentProject = Project;
            SessionManager.Instance.CurrentSyncRun = SessionManager.Instance.CurrentProject.ProjectRuns.Last();

            if (SessionManager.Instance.CurrentProject.ProjectRuns.Count() == 1)
            {
                Response.Redirect("SyncRunExport.aspx");
            }
            else
            {
                Response.Redirect(
                    SessionManager.Instance.CurrentProject.ProjectRuns.Last().Status ==
                    Convert.ToInt16(ECPSyncRunStatus.Open)
                        ? "AddMeasurement.aspx"
                        : "SyncRunAnalyze.aspx");
            }
        }

        protected void grdProjectList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                var project = e.Item.DataItem as Project;
                if (project != null)
                {
                    var imgStatus = e.Item.FindControl("imgStatus") as Image;
                    if (imgStatus != null)
                    {
                        switch (project.ProjectStatus)
                        {
                            case ECPSyncRunStatus.Open: imgStatus.ImageUrl = ResolveUrl("../Images/Project_Open.png");
                                imgStatus.ToolTip = ECPSyncRunStatus.Open.ToString();
                                imgStatus.AlternateText = ECPSyncRunStatus.Open.ToString();
                                break;
                            case ECPSyncRunStatus.Certify: imgStatus.ImageUrl = ResolveUrl("../Images/Project_Certify.png");
                                imgStatus.ToolTip = ECPSyncRunStatus.Certify.ToString();
                                imgStatus.AlternateText = ECPSyncRunStatus.Certify.ToString();
                                break;
                            case ECPSyncRunStatus.Closed: imgStatus.ImageUrl = ResolveUrl("../Images/Project_Close.png");
                                imgStatus.ToolTip = ECPSyncRunStatus.Closed.ToString();
                                imgStatus.AlternateText = ECPSyncRunStatus.Closed.ToString();
                                break;
                            case ECPSyncRunStatus.NotUsed: break;
                        }
                    }

                    var imgbtnDelete = e.Item.FindControl("imgbtnDelete") as ImageButton;
                    if (imgbtnDelete != null)
                    {
                        imgbtnDelete.Attributes.Add("onclick", "javascript: return ConfirmDeleteProject();");
                    }

                    var lbtnProjectName = e.Item.FindControl("lbtnProjectName") as LinkButton;
                    if (lbtnProjectName != null)
                    {
                        lbtnProjectName.Attributes.Add("ProjectId", project.ProjectId.ToString());
                    }

                    var btnInfo = e.Item.FindControl("btnInfo") as ImageButton;
                    if (btnInfo != null)
                    {
                        btnInfo.Attributes.Add("onclick", "javascript: return OpenInformationBox('" + ResolveUrl("~/ProjectInformation.aspx?ProjectID="+project.ProjectId.ToString()) + "');");

                    }
                }
            }
        }

        protected void grdProjectList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteProject")
            {
                Project = new Project()
                              {
                                  ProjectId = Convert.ToInt32(e.CommandArgument)
                              };
                _presenterprojectlist.DeleteProject();
                BindProject();
            }
        }
        #endregion

        #region Method
        private void BindProject()
        {
            if (SessionManager.Instance.CurrentSyncDevice != null)
            {
                Project = new Project()
                              {
                                  SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId
                              };
                _presenterprojectlist.GetProjectListBySyncDeviceId();
                grdProjectList.DataSource = Projects;
                grdProjectList.DataBind();
            }
        }
        #endregion

        #region IViewProjectList Members

        public List<Project> Projects { get; set;}
        
        #endregion

        #region IViewBaseProject Members

        public Project Project{ get; set;}
        
        #endregion
    }
}