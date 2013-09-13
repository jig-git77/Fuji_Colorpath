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
        #region Localization variables
        private const string _recallText = "allText";
        private const string _recvalidationStatusNotCertified = "validationStatusNotCertified";
        private const string _recvalidationStatusDisabled = "validationStatusDisabled";
        private const string _recvalidationStatusLastOK = "validationStatusLastOK";
        private const string _recvalidationStatusLastFailed = "validationStatusLastFailed"; 
        #endregion

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
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Device;
                //set dlpsuboperation session to none everytime page loads fresh(!ispostback)...
                SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.None;
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //------------Apply default CurrentFilterFunction for "CreateDate" column of rgProjectList RadGrid-----------------//
            rgProjectList.Columns.FindByDataField("CreateDate").CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.GreaterThanOrEqualTo;
        }
        #endregion

        #region Control events
        protected void rgProjectList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.FilteringItem)
            {

                RadComboBox rcmbLicenseType = e.Item.FindControl("rcmbLicenseType") as RadComboBox;
                if (rcmbLicenseType != null)
                {
                    if (SessionManager.Instance.CurrentDevice != null && SessionManager.Instance.CurrentDevice.LicenseTypeProject != null && SessionManager.Instance.CurrentDevice.LicenseTypeProject.Count > 0)
                    {
                        foreach (KeyValuePair<ELicenseType, int> item in SessionManager.Instance.CurrentDevice.LicenseTypeProject)
                        {
                            rcmbLicenseType.Items.Add(new RadComboBoxItem() { Text = Common.Utilities.GetEnumDescriptionAttributeValue(item.Key), Value = item.Key.ToString() });
                        }

                        rcmbLicenseType.Items.Insert(0, new RadComboBoxItem() { Text = GetResourceString(_recallText), Value = "" });
                        rcmbLicenseType.SelectedIndex = 0;
                    }
                }

                RadComboBox rcmbValidationStatus = e.Item.FindControl("rcmbValidationStatus") as RadComboBox;
                if (rcmbValidationStatus != null)
                {
                    rcmbValidationStatus.Items.Clear();
                    rcmbValidationStatus.Items.Add(new RadComboBoxItem() { Text = GetResourceString(_recvalidationStatusNotCertified), Value = "NotCertified" });
                    rcmbValidationStatus.Items.Add(new RadComboBoxItem() { Text = GetResourceString(_recvalidationStatusDisabled), Value = "Disabled" });
                    rcmbValidationStatus.Items.Add(new RadComboBoxItem() { Text = GetResourceString(_recvalidationStatusLastOK), Value = "LastOK" });
                    rcmbValidationStatus.Items.Add(new RadComboBoxItem() { Text = GetResourceString(_recvalidationStatusLastFailed), Value = "LastFailed" });

                    rcmbValidationStatus.Items.Insert(0, new RadComboBoxItem() { Text = GetResourceString(_recallText), Value = "" });
                    rcmbValidationStatus.SelectedIndex = 0;
                }
            }
        }
        protected void lbtnCreateNewPrintCondition_Click(object sender, EventArgs e)
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
        protected void rgProjectList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Validation":
                    ProjectId = Convert.ToInt32(e.CommandArgument);
                    _presenterProjectList.GetProjectDetailWithVarification();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                        SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.Validation;
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
            btnSelect.AlternateText = btnSelect.ToolTip = Common.Utilities.GetEnumDescriptionAttributeValue(project.LicenseType);

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
                    break;
            }

            //Decide and set image URL based on conditions
            SetOperationImageURL(btnSelect, project.LicenseType, project.Status);
            SetValidationImageURL(imgbtnVarification, project);

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
        private void SetValidationImageURL(Image imgValidationButton, Project project)
        {
            switch (project.ValidationStatus)
            {
                case ValidationStatus.NotCertified:
                    //Project is in open/close state so no validation can be performed
                    imgValidationButton.ImageUrl = @"../Images/Validation_Not_Certified.png";
                    imgValidationButton.ToolTip = Common.Utilities.GetEnumDescriptionAttributeValue(ValidationStatus.NotCertified);
                    imgValidationButton.Attributes.Add("onclick", "javascript: return false;");
                    break;
                case ValidationStatus.Disabled:
                    //Validation is not enabled
                    imgValidationButton.Visible = false;
                    //imgValidationButton.ImageUrl = @"../Images/Validation_Not_Active.png";
                    //imgValidationButton.ToolTip = Common.Utilities.GetEnumDescriptionAttributeValue(ValidationStatus.Disabled);
                    //imgValidationButton.Attributes.Add("onclick", "javascript: return false;");
                    break;
                case ValidationStatus.NoMeasurement:
                    //No validation found
                    imgValidationButton.ImageUrl = @"../Images/Validation_Not_Active.png";
                    imgValidationButton.ToolTip = Common.Utilities.GetEnumDescriptionAttributeValue(ValidationStatus.NoMeasurement);
                    break;
                case ValidationStatus.LastOK:
                    //Validation Last Success
                    imgValidationButton.ImageUrl = @"../Images/Validation_Last_OK.png";
                    imgValidationButton.ToolTip = Common.Utilities.GetEnumDescriptionAttributeValue(ValidationStatus.LastOK);
                    break;
                case ValidationStatus.LastFailed:
                    //Validation Last Failed
                    imgValidationButton.ImageUrl = @"../Images/Validation_Last_Failed.png";
                    imgValidationButton.ToolTip = Common.Utilities.GetEnumDescriptionAttributeValue(ValidationStatus.LastFailed);
                    break;
                default:
                    break;
            }
        }
        private void SetOperationImageURL(ImageButton imgOperationButton, ELicenseType selectedLicenseType, EOptimizeRunStatus optimizeRunStatus)
        {
            string operationImageURL = "";
            switch (optimizeRunStatus)
            {
                //Open and Closed has same type of images
                case EOptimizeRunStatus.Open:
                case EOptimizeRunStatus.Closed:
                    switch (selectedLicenseType)
                    {
                        case ELicenseType.None:
                            break;
                        case ELicenseType.Sync1D:

                            break;
                        case ELicenseType.DLP:
                            operationImageURL = @"../Images/DLP.png";
                            break;
                        case ELicenseType.InkOptimization:
                            operationImageURL = @"../Images/InkOptimize.png";
                            break;
                        case ELicenseType.Validation:

                            break;
                        case ELicenseType.IccProfiling:

                            break;
                        default:
                            break;
                    }
                    break;

                //Certified has different type of image
                case EOptimizeRunStatus.Certify:
                    switch (selectedLicenseType)
                    {
                        case ELicenseType.None:
                            break;
                        case ELicenseType.Sync1D:

                            break;
                        case ELicenseType.DLP:
                            operationImageURL = @"../Images/DLP_Certified.png";
                            break;
                        case ELicenseType.InkOptimization:
                            operationImageURL = @"../Images/InkOptimize_Certified.png";
                            break;
                        case ELicenseType.Validation:

                            break;
                        case ELicenseType.IccProfiling:

                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            imgOperationButton.ImageUrl = operationImageURL;
            imgOperationButton.Visible = operationImageURL.Length > 0;
        }
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
            //Now we are passing LicenseTypeID as None to bring all projects based on DeviceId
            //LicenseTypeID = Convert.ToInt32(SessionManager.Instance.CurrentOperation);
            LicenseTypeID = Convert.ToInt32(ELicenseType.None);
            _presenterProjectList.GetProjectListByDeviceId();
            if (!IsErrorOccured)
            {
                SessionManager.Instance.ProjectList = Projects;
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
        public int LicenseTypeID { get; set; }
        public Project Project { get; set; }
        #endregion
    }
}
