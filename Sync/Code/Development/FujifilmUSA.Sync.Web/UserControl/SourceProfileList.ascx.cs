using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SourceProfileList : System.Web.UI.UserControl,IViewSourceProfiles
    {
        #region Declaration
        private PresenterSourceProfileList _presentersourceprofile;

        public string SourceProfile_DeleteSourceProfile_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("SourceProfile_DeleteSourceProfile_Confirmation");
            }
        }

        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presentersourceprofile = new PresenterSourceProfileList(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();            

            if (!Page.IsPostBack)
            {
                BindSourceProfileList();
            }
        }
        #endregion

        #region Control events

        protected void grdSourceProfile_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteSourceProfile")
            {
                this.SourceProfileID = Convert.ToInt32(e.CommandArgument);
                _presentersourceprofile.DeleteSourceProfile();
                 BindSourceProfileList();
            }
        }

        #endregion

        #region Methods

        public void BindSourceProfileList()
        {            
            _presentersourceprofile.GetSourceProfileList();
            grdSourceProfile.DataSource = GetSourceProfiles;
            grdSourceProfile.DataBind();
        }

        public object DisplayIsActiveCustomValue(object value)
        {
            if (Convert.ToBoolean(value))
                return ResourceManager.GetResourceMessage("SourceProfile_IsUse_True");
            else
                return ResourceManager.GetResourceMessage("SourceProfile_IsUse_False");
        }

        #endregion

        #region IViewSourceProfiles Members
        public List<SourceProfile> GetSourceProfiles { get; set; }
        public int SourceProfileID { get; set; }
        #endregion








        
    }
}