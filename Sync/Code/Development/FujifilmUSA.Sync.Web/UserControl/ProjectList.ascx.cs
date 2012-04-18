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
    public partial class ProjectList : BaseControl, IViewProjectList
    {
        #region Variable Declaration
        private PresenterProjectList _presenterProjectList;
        public string ProjectListDeleteProjectConfirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("ProjectList_DeleteProject_Confirmation");
            }
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterProjectList = new PresenterProjectList(this);

            if (SessionManager.Instance.CurrentUser != null && !SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.CreateDeleteProject))
            {
                lbtnCreateNewPrintCondition.Visible = false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rgProjectList.PageSize = Common.Utilities.PageSize;
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Operation;
            }
        }
        #endregion

        #region Control events
        protected void lbtnCreateNewPrintCondition_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject = new Project()
                                                         {
                                                             DeviceId = SessionManager.Instance.CurrentDevice.DeviceId,
                                                             IsUsed = true,
                                                             CurrentDisplayStatus = ECurrentProjectStatus.New
                                                         };
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Description;
            Response.Redirect("AddProject.aspx");
        }
        protected void rgProjectList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    if (e.CommandArgument != null)
                    {
                        DeleteProject(Convert.ToInt32(e.CommandArgument));
                    }
                    break;
                case "ClearFilter":
                    foreach (GridColumn column in rgProjectList.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgProjectList.MasterTableView.FilterExpression = string.Empty;
                    rgProjectList.MasterTableView.Rebind();
                    break;
            }
        }
        protected void rgProjectList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetProjectList();
        }
        protected void rgProjectList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            var project = e.Item.DataItem as Project;
            if (e.Item.ItemType != GridItemType.Item && e.Item.ItemType != GridItemType.AlternatingItem) return;
            var btnSelect = e.Item.FindControl("imgbtnSelect") as ImageButton;
            var imgStatus = e.Item.FindControl("imgStatus") as Image;
            if (imgStatus == null || btnSelect == null) return;
            imgStatus.ToolTip = imgStatus.AlternateText = project.Status.ToString();

            var days = (DateTime.Now - project.CreateDate).TotalDays;


                switch (project.Status)
                {
                    case EOptimizeRunStatus.Open:
                        if (SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject))
                        {
                            btnSelect.Attributes.Add("onclick",
                                                     "javascript: return ShowOpenPrintConditionWindow('" +
                                                     project.ProjectId + "','" + project.ProjectName + "','" +
                                                     Math.Round(days, 1) +
                                                     "','" + project.CurrentDlpName + "')");
                        }
                        else
                            btnSelect.Attributes.Add("onclick", "javascript: return false;");
                        imgStatus.ImageUrl = ResolveUrl("../Images/Project_Open.png");
                        break;
                    case EOptimizeRunStatus.Closed:
                        if (SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject))
                        {
                            btnSelect.Attributes.Add("onclick",
                                                     "javascript: return ShowClosePrintConditionWindow('" +
                                                     project.ProjectId + "','" + project.ProjectName + "')");
                        }
                        else
                            btnSelect.Attributes.Add("onclick", "javascript: return false;");
                        imgStatus.ImageUrl = ResolveUrl("../Images/Project_Close.png");
                        break;
                    //case EOptimizeRunStatus.Certify:
                    //    btnSelect.Attributes.Add("onclick", "javascript: return false;");
                    //    imgStatus.ImageUrl = ResolveUrl("../Images/Project_Certify.png");
                    //    break;
                }

            if (!SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.CreateDeleteProject))
            {
                var imgDelete = e.Item.FindControl("imgbtnDelete") as ImageButton;
                if (imgDelete != null)
                {
                    imgDelete.Visible = false;
                }
            }
        }
        #endregion

        #region Method
        private void GetSourceProfiles()
        {
            //TODO: SessionManager.Instance.CurrentUser.UserCompany.CompanyId;
            CompanyId = 1;
            SourceProfile = new Dictionary<int, string>();
            _presenterProjectList.GetSourceProfileByCompany();
        }
        protected DateTime? FillterCreatedDateDate(GridItem item)
        {
            if (item.OwnerTableView.GetColumn("CreateDate").CurrentFilterValue == string.Empty)
            {
                return new DateTime?();
            }
            var dateValue = item.OwnerTableView.GetColumn("CreateDate").CurrentFilterValue.Split(' ')[0];
            return DateTime.Parse(dateValue);
        }
        private void GetProjectList()
        {
            Projects = new List<Project>();
            DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            _presenterProjectList.GetProjectListByDeviceId();
            if (!IsErrorOccured)
            {
                rgProjectList.DataSource = Projects;
            }
        }
        private void DeleteProject(int projectId)
        {
            ProjectId = projectId;
            _presenterProjectList.DeleteProject();
        }
        #endregion

        #region IViewProjectList Members
        public List<Project> Projects { get; set; }
        public int CompanyId { get; set; }
        public Dictionary<int, string> SourceProfile { get; set; }
        public int ProjectId { get; set; }
        public int DeviceId { get; set; }
        #endregion
    }
}