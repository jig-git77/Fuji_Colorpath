using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;

using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProjectList : System.Web.UI.UserControl, IViewProjectList
    {
        private PresenterProjectList _presenterProjectList;
        #region "Declaration"
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
            _presenterProjectList = new PresenterProjectList(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentDevice.DeviceId = 1;
            if (!IsPostBack)
            {
                BindProject();
            }
        }
        #endregion

        #region Control events
        protected void lbtnCreateNewPrintCondition_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject = new Project() { IsNewlyCreated = true };
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Description;
            Response.Redirect("AddProject.aspx");
        }

        //Grid Events
        protected void rgProjectList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete" && e.CommandArgument != null)
            {
                DeleteProject(Convert.ToInt32(e.CommandArgument));
            }
        }
        protected void rgProjectList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetSourceProfiles();
            GetProjectList();
            rgProjectList.DataSource = Projects;
        }
        protected void rgProjectList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            var project = e.Item.DataItem as Entity.Project;
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                var btnSelect = e.Item.FindControl("imgbtnSelect") as ImageButton;
                var imgStatus = e.Item.FindControl("imgStatus") as Image;
                if (imgStatus != null)
                {
                    switch (project.Status)
                    {
                        case EOptimizeRunStatus.Open:
                            if (btnSelect != null)
                                btnSelect.Attributes.Add("onclick", "javascript: return ShowOpenPrintConditionWindow('" + project.ProjectId + "','" + project.ProjectName + "')");
                            imgStatus.ImageUrl = ResolveUrl("../Images/Project_Open.png");
                            imgStatus.ToolTip = EOptimizeRunStatus.Open.ToString();
                            imgStatus.AlternateText = EOptimizeRunStatus.Open.ToString();
                            break;
                        case EOptimizeRunStatus.Closed:
                            if (btnSelect != null)
                                btnSelect.Attributes.Add("onclick", "javascript: return ShowClosePrintConditionWindow('" + project.ProjectName + "')");
                            imgStatus.ImageUrl = ResolveUrl("../Images/Project_Close.png");
                            imgStatus.ToolTip = EOptimizeRunStatus.Closed.ToString();
                            imgStatus.AlternateText = EOptimizeRunStatus.Closed.ToString();
                            break;
                        case EOptimizeRunStatus.Certify:
                            if (btnSelect != null)
                                btnSelect.Attributes.Add("onclick", "javascript: return false;");
                            imgStatus.ImageUrl = ResolveUrl("../Images/Project_Certify.png");
                            imgStatus.ToolTip = EOptimizeRunStatus.Certify.ToString();
                            imgStatus.AlternateText = EOptimizeRunStatus.Certify.ToString();
                            break;
                        case EOptimizeRunStatus.NotUsed:
                            if (btnSelect != null)
                                btnSelect.Attributes.Add("onclick", "javascript: return false;");
                            imgStatus.ImageUrl = ResolveUrl("../Images/no_image.png");
                            break;
                    }
                }
                var lblStandard = e.Item.FindControl("lblStandard") as Label;
                if (lblStandard != null && SourceProfile.ContainsKey(project.SourceProfileId))
                    lblStandard.Text = SourceProfile[project.SourceProfileId];
            }
        }

        //Print Standard Rad Combobox event     
        protected void RadComboBoxPrintStandard_PreRender(object sender, EventArgs e)
        {
            RadComboBox combo = sender as RadComboBox;
            GetSourceProfiles();
            BindSearchPrintStandardType(combo);
            combo.SelectedValue = hdSelectedSourceProfileId.Value.ToString();
        }
        protected void RadComboBoxPrintStandard_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            hdSelectedSourceProfileId.Value = e.Value;
            //rgProjectList.MasterTableView.FilterExpression = "([SourceProfileId] = '" + e.Value + "')"; ;
            //rgProjectList.MasterTableView.Rebind();
        }
        #endregion

        #region Method

        //private void BindSearchSyncRunStatus(RadComboBox SyncRunStatus)
        //{
        //    //Dictionary<int, string> dic = Utilities.GetEnumDropdown<ECPSyncRunStatus>();
        //    //SyncRunStatus.DataSource = dic;
        //    //SyncRunStatus.DataTextField = "Value";
        //    //SyncRunStatus.DataValueField = "Key";
        //    //SyncRunStatus.DataBind();
        //}
        private void GetSourceProfiles()
        {
            CompanyId = 1;
            SourceProfile = new Dictionary<int, string>();
            _presenterProjectList.GetSourceProfileByCompany();
        }
        private void BindSearchPrintStandardType(RadComboBox cmbSearchPrintStandardType)
        {
            foreach (var profile in SourceProfile)
            {
                cmbSearchPrintStandardType.Items.Add(new RadComboBoxItem(profile.Value.ToString(), profile.Key.ToString()));
            }
        }
        protected DateTime? FillterCreatedDateDate(GridItem item)
        {
            if (item.OwnerTableView.GetColumn("CreateDate").CurrentFilterValue == string.Empty)
            {
                return new DateTime?();
            }
            else
            {
                var dateValue = item.OwnerTableView.GetColumn("CreateDate").CurrentFilterValue.Split(' ')[0];
                return DateTime.Parse(dateValue);
            }
        }

        private void BindProject()
        {
            GetSourceProfiles();
            GetProjectList();
            rgProjectList.DataSource = Projects;
            rgProjectList.DataBind();

        }
        private void GetProjectList()
        {
            Project = new Entity.Project();
            Projects = new List<Project>();
            Project.DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            _presenterProjectList.GetProjectListByDeviceId();
        }

        private void DeleteProject(int projectId)
        {
            Project = new Entity.Project { ProjectId = projectId };
            _presenterProjectList.DeleteProject();
        }


        #endregion

        #region IViewProjectList Members
        public List<Project> Projects { get; set; }
        public Project Project { get; set; }
        public int CompanyId { get; set; }
        public Dictionary<int, string> SourceProfile { get; set; }
        public int ProjectId { get; set; }
        #endregion

    }

}