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
    public partial class DeviceLinkProfileList : System.Web.UI.UserControl,IViewDeviceLinkProfile
    {
         #region Declaration
        private PresenterDeviceLinkProfile _presenterdevicelinkprofile;

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
            _presenterdevicelinkprofile = new PresenterDeviceLinkProfile(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();

            if (!Page.IsPostBack)
            {
                BindDeviceLinkProfileList();    
            }
        }
        #endregion

        #region Methods
        public void BindDeviceLinkProfileList()
        {
            this.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
            _presenterdevicelinkprofile.GetDeviceLinkProfileListOnSyncDeviceId();
            grdDLP.DataSource = GetDeviceLinkProfileList;
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
        public  List<DeviceLinkProfile> GetDeviceLinkProfileList { get; set; }
        public int SyncDeviceId { get; set; }
        #endregion        
        
    }
}