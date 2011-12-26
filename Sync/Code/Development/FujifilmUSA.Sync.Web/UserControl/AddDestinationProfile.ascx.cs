using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddDestinationProfile : System.Web.UI.UserControl, IViewAddDestinationProfile
    {
        #region "Declaration"
        private PresenterAddDestinationProfile _presenteradddestinationprofile;
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
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
            ProfileId = ucAddMeasurementCommon.ProfileId;
            _presenteradddestinationprofile.CreateDestinationProfile();
            Response.Redirect("DestinationProfileList.aspx");
        }
        #endregion      
    
        #region IViewAddDestinationProfile Members

        public int ProfileId { get; set; }
        
        #endregion

        #region IViewGetMeasurementDeviceType Members

        public System.Collections.Generic.Dictionary<string, string> MeasurementDevices { get; set; }
        
        #endregion

        #region IViewBaseSyncDevice Members

        public int SyncDeviceId { get; set; }
        
        #endregion
    }
}