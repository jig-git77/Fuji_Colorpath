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
    public partial class DestinationProfileList : System.Web.UI.UserControl,IViewDestinationProfile
    {
        #region Declaration
        private PresenterDestinationProfile _presenterdestinationprofile;

        public string DestinationProfile_DeleteDestinationProfile_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("DestinationProfile_DeleteDestinationProfile_Confirmation");
            }
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterdestinationprofile = new PresenterDestinationProfile(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();

            if (!Page.IsPostBack)
            {
                BindDestinationProfileList();    
            }
        }
        #endregion

        #region Methods
        public void BindDestinationProfileList()
        {
            this.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
            _presenterdestinationprofile.GetDestinationProfileList();
            grdDestinationProfile.DataSource = DestinationProfileListing;
            grdDestinationProfile.DataBind();
        }
        public object DisplayIsActiveCustomValue(object value)
        {
            if (Convert.ToBoolean(value))
                return ResourceManager.GetResourceMessage("DestimationProfile_IsUse_True");
            else
                return ResourceManager.GetResourceMessage("DestimationProfile_IsUse_False");
        } 

        #endregion

        #region Control Events
        protected void grdDestinationProfile_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteDestinationProfile")
            {

            }
        }
        #endregion

        #region IViewDestinationProfile Members
        public List<DestinationProfile> DestinationProfileListing { get; set; }
        public int SyncDeviceId { get; set; }
        #endregion

        
    }
}