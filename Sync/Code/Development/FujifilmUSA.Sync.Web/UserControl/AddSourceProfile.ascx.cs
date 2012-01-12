using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddSourceProfile : System.Web.UI.UserControl,IViewAddSourceProfile
    {
        #region "Declaration"
        private PresenterAddSourceProfile _presenteraddsourceprofile;
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            SessionManager.Instance.CurrentSyncDevice = new SyncDevice();
            SessionManager.Instance.CurrentSyncDevice.SyncDeviceId = 6;
            _presenteraddsourceprofile = new PresenterAddSourceProfile(this);
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
        protected void lbtnSourceProfileSave_Click(object sender, EventArgs e)
        {
            //ProfileId = ucAddMeasurementCommon.ProfileId;
            ProfileId = 9;
            ProfilerOptionId = Convert.ToInt32(cmbProfilerOption.SelectedValue);
            _presenteraddsourceprofile.CreateSourceProfile();
            Response.Redirect("SourceProfileList.aspx");
        }
        #endregion

        #region Methods
        public void BindProfileOption()
        {
            _presenteraddsourceprofile.GetProfilerOption();
            cmbProfilerOption.DataSource = GetProfileOption;
            cmbProfilerOption.DataTextField = "Value";
            cmbProfilerOption.DataValueField = "Key";
            cmbProfilerOption.DataBind();
        }
        #endregion

        #region Error Handling Method
        public void ErrorOccured(object sender, Business.Interface.ErrorArgs e)
        {
            ucMeasurementStatusbar.DisplayMessage(e.ErrorMessage, false);
        }
        #endregion

        //*******************************************************************//

        #region IViewAddSourceProfile Members

        public Dictionary<int, string> GetProfileOption { get; set; }
        public int ProfileId { get; set; }
        public int ProfilerOptionId { get; set; } 

        #endregion
    }
}