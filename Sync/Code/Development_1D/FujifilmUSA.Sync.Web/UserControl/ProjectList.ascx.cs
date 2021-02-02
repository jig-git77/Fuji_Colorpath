using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Linq;
using FujifilmUSA.Sync.Entity.CPSpot;
using Newtonsoft.Json;

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
        public string ProjectListDeleteSpotProjectConfirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("ProjectList_DeleteSpotProject_Confirmation");
            }
        }
        public int JsRandomNumber { get { return new Random().Next(1, 100); } }
        public string JsRandomGuid { get { return Guid.NewGuid().ToString(); } }
        #endregion
        public ProjectList()
        {
            this.Init += (sender, e) => this.ExceptionHandling(Page_Init, sender, e);
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
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
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Device;
                //set dlpsuboperation session to none everytime page loads fresh(!ispostback)...
                SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.None;
                hdnBaseServiceUrl.Value = Utilities.ReadConfigValue("BaseServiceURL");
                hdfHasDeletePermission.Value = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.CreateDeleteProject).ToString();
                hdfCurrentUser.Value = Convert.ToString(SessionManager.Instance.CurrentUser.UserId);
                hdfCurrentDeviceID.Value = Convert.ToString(SessionManager.Instance.CurrentDevice.DeviceId);
                hdfCurrentCulture.Value = getCurrentCultureLanguage();
                hdfAuthToken.Value = SessionManager.Instance.CurrentUser.APIAuthKey;
            }
        }
    
        #endregion

        #region Control events

        protected void lbtnCreateNewPrintCondition_Click(object sender, EventArgs e)
        {
            //GetProjectList(); //---T0 assign projects list in session-------//
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
        #endregion

        #region Method
        private void GetProjectList()
        {
            Projects = new List<Project>();
            DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            //Now we are passing LicenseTypeID as None to bring all projects based on DeviceId
            LicenseTypeID = Convert.ToInt32(ELicenseType.None);
            _presenterProjectList.GetProjectListByDeviceId();
            if (!IsErrorOccured)
            {
                SessionManager.Instance.ProjectList = Projects;
            }
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
        //public List<Entity.CPSpot.SpotProjectLibrary> SpotProjectLibraries { get; set; }
        public SpotProjectList SpotProjectList { get; set; }
        public int SpotProjectId { get; set; }

        #endregion
    }
}
