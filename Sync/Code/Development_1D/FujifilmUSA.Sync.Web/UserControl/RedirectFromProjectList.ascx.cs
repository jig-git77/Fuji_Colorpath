using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class RedirectFromProjectList : BaseControl, IViewProjectList
    {
        #region Variable Declaration
        private PresenterProjectList _presenterProjectList;
        #endregion
        
        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterProjectList = new PresenterProjectList(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["ProjectID"])) && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["RedirectType"])))
            {
                ProjectId = Convert.ToInt32(Request.QueryString["ProjectID"]);
                if (Convert.ToString(Request.QueryString["RedirectType"]) == "AddSpotProject")
                {
                    SessionManager.Instance.CurrentProject = new Project()
                    {
                        DeviceId = SessionManager.Instance.CurrentDevice.DeviceId,
                        IsUsed = true,
                        CurrentDisplayStatus = ECurrentProjectStatus.New,
                        LicenseType = ELicenseType.SpotLibrary,
                        SpotTargetMasterProjectID = ProjectId,
                        SpotTargetProject = new Entity.CPSpot.SpotProjectLibrary()
                        {
                            ProjectId = ProjectId
                        }
                    };

                    SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Description;

                    Response.Redirect("AddProject.aspx");
                }
                else if (Convert.ToString(Request.QueryString["RedirectType"]) == "ProjectValidation")
                {
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
                }
                else if(Convert.ToString(Request.QueryString["RedirectType"]) == "SelectSpotTargetProject")
                {
                    _presenterProjectList.GetProjectDetail_SpotProject();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = Project.SpotTargetProject.OptimizeRuns.Last();
                        SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.SpotLibrary;
                        SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                        Response.Redirect("Analyze_SpotColor.aspx");
                    }
                }
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
        public Entity.CPSpot.SpotProjectList SpotProjectList { get; set; }
        public int SpotProjectId { get; set; }

        #endregion
    }
}