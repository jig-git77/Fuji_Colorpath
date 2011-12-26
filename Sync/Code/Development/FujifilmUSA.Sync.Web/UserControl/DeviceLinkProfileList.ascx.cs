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
    public partial class DeviceLinkProfileList : System.Web.UI.UserControl, IViewDeviceLinkProfiles
    {
        #region Declaration
        private PresenterDeviceLinkProfileList _presenterdevicelinkprofile;

        public string DLP_DeleteDestinationProfile_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("DLP_DeleteDestinationProfile_Confirmation");
            }
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterdevicelinkprofile = new PresenterDeviceLinkProfileList(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
            SessionManager.Instance.ProfileTypeID = ProfileType.Dlp;
            if (!Page.IsPostBack)
            {
                BindDeviceLinkProfileList();
            }
        }
        #endregion

        #region Control events
        protected void grdDLP_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteDestinationProfile")
            {
                this.DLPId = Convert.ToInt32(e.CommandArgument);
                _presenterdevicelinkprofile.DeleteDeviceLinkProfile();
                BindDeviceLinkProfileList();
            }
        }
        #endregion

        #region Methods
        public void BindDeviceLinkProfileList()
        {
            this.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
            _presenterdevicelinkprofile.GetDeviceLinkProfiles();
            grdDLP.DataSource = DeviceLinkProfiles;
            grdDLP.DataBind();
        }
        public object DisplayIsActiveCustomValue(object value)
        {
            if (Convert.ToBoolean(value))
                return ResourceManager.GetResourceMessage("DeviceLinkProfile_IsUse_True");
            else
                return ResourceManager.GetResourceMessage("DeviceLinkProfile_IsUse_False");
        }

        #endregion

        #region IViewDeviceLinkProfile Members
        public List<DeviceLinkProfile> DeviceLinkProfiles { get; set; }
        public int SyncDeviceId { get; set; }
        public DeviceLinkProfile DeviceLinkProfile { get; set; }
        public int DLPId { get; set; }
        #endregion


        

    }
}