using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;

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
            if (!Page.IsPostBack)
            {
                BindProfileOption();
            }
        }

        #endregion

        #region Control Events
        protected void lbtnDestinationProfileSave_Click(object sender, EventArgs e)
        {
            DestinationProfile = new DestinationProfile();
            DestinationProfile.ProfileName = txtDestinationProfile.Text;
            DestinationProfile.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
            DestinationProfile.IsUse = true;
            DestinationProfile.ProfileOptionId = int.Parse(cmbProfilerOption.SelectedValue);
            _presenteradddestinationprofile.InsertDestinationProfile();
            DestinationProfile.ProfileId = ProfileId;

            DestinationProfile.ReferenceChartTypeId = int.Parse(ucDeviceTypeSelectionCommon.ReferenceChartTypeValue);
            DestinationProfile.SerialNumber = ucDeviceTypeSelectionCommon.SerialNumber;
            SessionManager.Instance.CurrentDestinationProfile = DestinationProfile;

            if (DestinationProfile.ProfileId != 0)
                Response.Redirect("AddDestinationProfileMeasurement.aspx");
        }
        #endregion      

        #region Methods
        public void BindProfileOption()
        {
            _presenteradddestinationprofile.GetProfilerOption();
            cmbProfilerOption.DataSource = GetProfileOption;
            cmbProfilerOption.DataTextField = "Value";
            cmbProfilerOption.DataValueField = "Key";
            cmbProfilerOption.DataBind();
        }
        #endregion

        #region IViewError Members

        public void ErrorOccured(object sender, Business.Interface.ErrorArgs e)
        {
             ucStatusbar1.DisplayMessage(e.ErrorMessage, false);
        }

        #endregion

        //*******************************************************************//

        #region IViewAddDestinationProfile Members
        public int ProfileId { get; set; }
        public Dictionary<int, string> GetProfileOption { get; set; }
        public DestinationProfile DestinationProfile { get; set; }
        #endregion

        #region IViewGetMeasurementDeviceType Members

        public Dictionary<string, string> MeasurementDevices { get; set; }       
        
        #endregion

        #region IViewBaseSyncDevice Members

        public int SyncDeviceId { get; set; }
        
        #endregion
        
    }
}