using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProjectInformation : System.Web.UI.UserControl, IViewAddProject, IViewProjectList   
    {
        #region Variable Declarations
        private PresenterAddProject _presenteraddproject;
        private PresenterProjectList _presenterprojectlist;            
        #endregion

        #region Page Events

        protected override void OnInit(EventArgs e)
        {
            _presenteraddproject = new PresenterAddProject(this);
            _presenterprojectlist = new PresenterProjectList(this);
        }        

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                    if(Request.QueryString["ProjectID"]!=null)
                    {
                        LoadProjectByProjectId();        
                    }
                }
                
            }
        }

        #region Methods
        private void LoadProjectByProjectId()
        {
            this.Project = new Project();
                //Get Current Project Detail by Project Id

            if(Request.QueryString["ProjectID"]!=null)            
            {
                this.Project.ProjectId=Convert.ToInt32(Request.QueryString["ProjectID"].ToString());
            }
                _presenterprojectlist.GetProject();
                lblProjectName.Text = Project.ProjectName;
                lblProjectStatus.Text = ((ECPSyncRunStatus)Project.ProjectStatus).ToString();
                grdSyncRun.DataSource = Project.ProjectRuns;
                grdSyncRun.DataBind();
                _presenteraddproject.GetDeviceLinkProfile();
                lblSourceProfile.Text = deviceLinkProfile.SrcProfile.ProfileName;
                lblDestinationProfile.Text = deviceLinkProfile.DestProfile.ProfileName;
                lblDLP.Text = deviceLinkProfile.ProfileName;                                 
        }
       
        #endregion

        #region IViewAddProject Members

        public Dictionary<string, string> MeasurementDevices { get; set; }

        public Dictionary<int, string> ReferenceChartTypes { get; set; }

        public List<DeviceLinkProfile> DeviceLinkProfiles { get; set; }

        public DeviceLinkProfile deviceLinkProfile { get; set; }

        public int SourceProfileId { get; set; }

        public int DestinationProfileId { get; set; }

        public int MeasurementDeviceId { get; set; }

        public MeasurementSetting MeasurementSetting { get; set; }

        public Project Project { get; set; }

        public List<Project> Projects { get; set; }

        #endregion
     
        #region IViewGetSourceDestinationCombo Members

        public Dictionary<int, string> GetSourceProfiles { get; set; }

        public Dictionary<int, string> GetDestinationProfiles { get; set; }

        #endregion

        #region IViewBaseSyncDevice Members

        public int SyncDeviceId { get; set; }

        #endregion

        protected void grdSyncRun_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                lblStatus.Text=((ECPSyncRunStatus)Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"Status"))).ToString();
            }
        }
        
        
    }
}