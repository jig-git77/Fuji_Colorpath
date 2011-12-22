using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddDeviceLinkProfile : System.Web.UI.UserControl,IViewAddProject
    {
         #region "Declaration"
        private PresenterAddProject _presenteraddproject;
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presenteraddproject = new PresenterAddProject(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
            
            if (!Page.IsPostBack)
            {               
                BindSourceProfile();
                BindDestinationProfile();
            }
        }

        #endregion
         
        #region Methods
       

       
        private void BindSourceProfile()
        {
            _presenteraddproject.GetSourceProfileList();
            cmbSourceProfile.DataSource = GetSourceProfileList;
            cmbSourceProfile.DataBind();
            cmbSourceProfile.Items.Insert(0, new ListItem("-- Select --", ""));
        }
        private void BindDestinationProfile()
        {
            this.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
            _presenteraddproject.GetDestinationProfiles();
            cmbDestinationProfile.DataSource = GetDestinationProfiles;
            cmbDestinationProfile.DataBind();
            cmbDestinationProfile.Items.Insert(0, new ListItem("-- Select --", ""));
        }
        #endregion

        #region Control Events
        protected void lbtnDestinationProfileSave_Click(object sender, EventArgs e)
        {
            lbtnDeviceLinkProfileSave.Text = "Save";
        }      
        //protected void cmbDeviceType_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    BindReferenceChartType();
        //}
        #endregion

        #region IViewAddProject Members
        public Dictionary<int, string> GetSourceProfileList { get; set; }
        public Dictionary<int, string> GetDestinationProfiles { get; set; }
        public int SourceProfileId { get; set; }
        public int DestinationProfileId { get; set; }
        public List<DeviceLinkProfile> GetDeviceLinkProfileList { get; set; }
        public int SyncDeviceId { get; set; }
        public Dictionary<string, string> GetMeasurementDeviceListBySyncDeviceId { get; set; }
        public int MeasurementDeviceId { get; set; }
        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId { get; set; }
        public Project ObjProject { get; set; }
        public MeasurementSetting ObjMeasurementSetting { get; set; }
        public Project InsertProjectAndMeasurementSetting { get; set; }
        #endregion

        
    }    
}