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
    public partial class AddDeviceLinkProfile : System.Web.UI.UserControl, IViewAddDeviceLinkProfile, IViewGetSourceDestinationCombo
    {
        #region "Declaration"
        private PresenterGetSourceDestinationCombo _presenterGetSourceDestinationCombo;
        private PresenterAddDeviceLinkProfile _presenteradddevicelinkprofile;
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presenterGetSourceDestinationCombo = new PresenterGetSourceDestinationCombo(this);
            _presenteradddevicelinkprofile = new PresenterAddDeviceLinkProfile(this);
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
            _presenterGetSourceDestinationCombo.GetSourceProfileList();
            cmbSourceProfile.DataSource = GetSourceProfiles;
            cmbSourceProfile.DataBind();
            cmbSourceProfile.Items.Insert(0, new ListItem("-- Select --", ""));
        }
        private void BindDestinationProfile()
        {
            this.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
            _presenterGetSourceDestinationCombo.GetDestinationProfiles();
            cmbDestinationProfile.DataSource = GetDestinationProfiles;
            cmbDestinationProfile.DataBind();
            cmbDestinationProfile.Items.Insert(0, new ListItem("-- Select --", ""));
        }
        #endregion

        #region Control Events
        protected void lbtnDeviceLinkProfileSave_Click(object sender, EventArgs e)
        {
            DeviceLinkProfile = new DeviceLinkProfile
                                    {
                                        ProfileName = txtDeviceLinkProfileName.Text.Trim(),
                                        DestProfile = (new DestinationProfile() {ProfileId = int.Parse(cmbDestinationProfile.SelectedValue)}),
                                        SrcProfile = (new SourceProfile() {ProfileId = int.Parse(cmbSourceProfile.SelectedValue)}),
                                        IsUse = true
                                    };
            _presenteradddevicelinkprofile.CreateDeviceLinkProfile();
            Response.Redirect("DeviceLinkProfileList.aspx", true);
        }
        #endregion

        #region IViewAddDeviceLinkProfile Members

        public DeviceLinkProfile DeviceLinkProfile { get; set; }

        #endregion

        #region IViewGetSourceDestination Members

        public Dictionary<int, string> GetSourceProfiles { get; set; }

        public Dictionary<int, string> GetDestinationProfiles { get; set; }

        #endregion

        #region IViewBaseSyncDevice Members

        public int SyncDeviceId { get; set; }

        #endregion
    }
}