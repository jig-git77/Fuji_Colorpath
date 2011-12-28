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
            ProfileId = ucAddMeasurementCommon.ProfileId;
            ProfilerOptionId =Convert.ToInt32(cmbProfilerOption.SelectedValue);
            _presenteradddestinationprofile.CreateDestinationProfile();
            Response.Redirect("DestinationProfileList.aspx");
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

        #region IViewAddDestinationProfile Members

        public int ProfileId { get; set; }

        public int ProfilerOptionId { get; set; }        

        public Dictionary<int, string> GetProfileOption { get; set; }

        #endregion

        #region IViewGetMeasurementDeviceType Members

        public Dictionary<string, string> MeasurementDevices { get; set; }       
        
        #endregion

        #region IViewBaseSyncDevice Members

        public int SyncDeviceId { get; set; }
        
        #endregion


    }
}