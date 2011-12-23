using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddDestinationProfile : System.Web.UI.UserControl, IViewAddProject, IViewAddDestinationProfile
    {
        #region "Declaration"
        private PresenterAddProject _presenteraddproject;
        private PresenterAddDestinationProfile _presenteradddestinationprofile;
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presenteraddproject = new PresenterAddProject(this);
            _presenteradddestinationprofile = new PresenterAddDestinationProfile(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
        } 

        #endregion


        #region Control Events
        protected void lbtnDestinationProfileSave_Click(object sender, EventArgs e)
        {
            DestinationId = ucAddMeasurementCommon.ProfileId;
            _presenteradddestinationprofile.CreateDestinationProfile();
            if(CreateResult)
                Response.Redirect("DestinationProfileList.ascx");
        }
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
        
        #region IViewAddDestinationProfile Members
        public int DestinationId { get; set; }
        public string MeasurementIds { get; set; }
        public bool CreateResult { get; set; }
        #endregion
    }
}