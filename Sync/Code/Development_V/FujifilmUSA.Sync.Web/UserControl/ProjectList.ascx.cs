using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Linq;

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
        public ProjectList()
        {
            this.Init += (sender, e) => this.ExceptionHandling(Page_Init, sender, e);
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
            this.PreRender += (sender, e) => this.ExceptionHandling(Page_PreRender, sender, e);
        }

        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
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
                //set dlpsuboperation session to none everytime page loads fresh(!ispostback)...
                SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.None;
                lblTitle.Text = Utilities.GetEnumDescriptionAttributeValue(SessionManager.Instance.CurrentOperation);
                ViewState["ProjectLimit"] = 0;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //------------Apply default CurrentFilterFunction for "CreateDate" column of rgProjectList RadGrid-----------------//
            rgProjectList.Columns.FindByDataField("CreateDate").CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.GreaterThanOrEqualTo;
        }
        #endregion

        #region Control events
        protected void lbtnCreateNewPrintCondition_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ViewState["ProjectLimit"]) >= SessionManager.Instance.CurrentProjLimit)
            {
                //--------------Project Limit reach, so can not create more projects under this Device now-------//  
                Response.Redirect("ProjectMaxLimit.aspx");
            }
            else
            {
                //------------Create new Project under this Device-----------------//
                SessionManager.Instance.CurrentProject = new Project()
                                                             {
                                                                 DeviceId = SessionManager.Instance.CurrentDevice.DeviceId,
                                                                 IsUsed = true,
                                                                 CurrentDisplayStatus = ECurrentProjectStatus.New
                                                             };
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Description;
                Response.Redirect("AddProject.aspx");
            }
        }
        protected void rgProjectList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Varification":
                    ProjectId = Convert.ToInt32(e.CommandArgument);
                    _presenterProjectList.GetProjectDetailWithVarification();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                        SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.Verification;
                        Response.Redirect(SessionManager.Instance.CurrentOptimizeRun.HasMeasurements
                                              ? "Analyze.aspx"
                                              : "Measurement.aspx");
                    }
                    break;
                case "Delete":
                    if (e.CommandArgument != null)
                    {
                        DeleteProject(Convert.ToInt32(e.CommandArgument));
                        rgProjectList.MasterTableView.Rebind();
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
            var imgbtnVarification = e.Item.FindControl("imgbtnVarification") as Image;
            if (imgStatus == null || btnSelect == null || imgbtnVarification == null) return;
            imgStatus.ToolTip = imgStatus.AlternateText = GetResourceString("ProjectStatus_" + project.Status.ToString());

            //here we are not showing validation image icon when project is not certified, i.e. in any other state
            imgbtnVarification.Visible = false;
            var days = (DateTime.Now - project.CreateDate).TotalDays;


            switch (project.Status)
            {
                case EOptimizeRunStatus.Open:
                    if (SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject))
                    {
                        btnSelect.Attributes.Add("onclick",
                                                 "javascript: return ShowOpenPrintConditionWindow('" +
                                                 project.ProjectId + "','" + Server.UrlEncode(project.ProjectName) + "','" +
                                                 Math.Round(days, 1) +
                                                 "','" + Server.UrlEncode(project.CurrentDlpName) + "','" + project.TotalRuns + "')");
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
                                                 project.ProjectId + "','" + Server.UrlEncode(project.ProjectName) + "','" + Server.UrlEncode(project.CurrentDlpName) + "')");
                    }
                    else
                        btnSelect.Attributes.Add("onclick", "javascript: return false;");
                    imgStatus.ImageUrl = ResolveUrl("../Images/Project_Close.png");
                    break;
                case EOptimizeRunStatus.Certify:
                    if (SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject))
                    {
                        btnSelect.Attributes.Add("onclick",
                                                 "javascript: return ShowClosePrintConditionWindow('" +
                                                 project.ProjectId + "','" + Server.UrlEncode(project.ProjectName) + "','" + Server.UrlEncode(project.CurrentDlpName) + "')");
                    }
                    else
                        btnSelect.Attributes.Add("onclick", "javascript: return false;");
                    
                    imgStatus.ImageUrl = ResolveUrl("../Images/Project_Certify.png");

                    //Here are we showing validation status icon if project is in certified state
                    imgbtnVarification.ImageUrl = ResolveUrl("../Images/ValidationSuccess.png");
                    imgbtnVarification.Visible = true;
                    break;
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
            LicenseTypeID = Convert.ToInt32(SessionManager.Instance.CurrentOperation);
            _presenterProjectList.GetProjectListByDeviceId();
            if (!IsErrorOccured)
            {
                rgProjectList.DataSource = Projects;
            }
            ViewState["ProjectLimit"] = Projects.Count;   //------Store the total projects to check ProjectLimit counter--------//
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
        public int LicenseTypeID { get; set; }
        public Project Project { get; set; }
        #endregion
    }
}
