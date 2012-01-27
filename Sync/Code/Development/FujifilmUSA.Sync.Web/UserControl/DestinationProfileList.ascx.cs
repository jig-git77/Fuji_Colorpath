using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using System.Web.UI.WebControls;
using System.Web;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class DestinationProfileList : System.Web.UI.UserControl,IViewDestinationProfiles
    {
        #region Declaration
        private PresenterDestinationProfileList _presenterdestinationprofile;

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
            _presenterdestinationprofile = new PresenterDestinationProfileList(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
            SessionManager.Instance.ProfileTypeID = ProfileType.DestinationProfile;
            if (!Page.IsPostBack)
            {
                BindDestinationProfileList();
            }   
        }
        #endregion

        #region Control events
        protected void grdDestinationProfile_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteDestinationProfile")
            {
                this.DestinationProfileId = Convert.ToInt32(e.CommandArgument);
                _presenterdestinationprofile.DeleteDestinationProfile();
                BindDestinationProfileList();
            }
            if (e.CommandName == "DownloadDestinationProfile")
            {
                this.DestinationProfileId = Convert.ToInt32(e.CommandArgument);
                _presenterdestinationprofile.GetProfileByDestinationProfileId();
                //Prompt for download file
                if (Profile != null && Profile.Data.Profile != null)
                    Utilities.DownloadFile(Profile.Data.Profile, "DestinationProfile.icc");
            }
        }
        #endregion

        #region Methods
        
        public void BindDestinationProfileList()
        {
            this.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
            _presenterdestinationprofile.GetDestinationProfileList();
            grdDestinationProfile.DataSource = DestinationProfiles;
            grdDestinationProfile.DataBind();
        }

        public object DisplayIsActiveCustomValue(object value)
        {
            if (Convert.ToBoolean(value))
                return ResourceManager.GetResourceMessage("DestinationProfile_IsUse_True");
            else
                return ResourceManager.GetResourceMessage("DestinationProfile_IsUse_False");
        } 

        #endregion

        #region Error Handling Method
        public void ErrorOccured(object sender, Business.Interface.ErrorArgs e)
        {
            ucStatusbar1.DisplayMessage(e.ErrorMessage, false);
        }
        #endregion

        //*******************************************************************//

        #region IViewDestinationProfiles Members

        public int DestinationProfileId { get; set; }
        public List<DestinationProfile> DestinationProfiles { get; set; }
        
        #endregion

        #region IViewBaseSyncDevice Members

        public int SyncDeviceId{ get; set; }
        
        #endregion

        #region IViewBaseProfile Members

        public IBaseProfile Profile { get; set; }

        #endregion
        
    }
}