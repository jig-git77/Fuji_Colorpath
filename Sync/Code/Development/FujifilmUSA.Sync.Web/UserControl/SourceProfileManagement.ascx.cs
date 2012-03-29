using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SourceProfileManagement : BaseControl, IViewSourceProfile
    {

        #region Declaration
        private PresenterSourceProfile _presentersourceprofile;
        public string SourceProfileManagement_DeleteSourceProfile_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("SourceProfileManagement_DeleteSourceProfile_Confirmation");
            }
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presentersourceprofile = new PresenterSourceProfile(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rgSourceProfile.ItemCommand+=new GridCommandEventHandler(rgSourceProfile_ItemCommand);
                BindSourceProfileList();
            }
        }
        #endregion

        #region Methods

        public void BindSourceProfileList()
        {
            _presentersourceprofile.GetSourceProfiles();
            rgSourceProfile.DataSource = SourceProfiles;
            rgSourceProfile.DataBind();
        }

        #endregion

        #region Control Events
        protected void rgSourceProfile_ItemCommand(object sender, GridCommandEventArgs e)
        {
            SourceProfile = new Entity.SourceProfile();
            SourceProfile.SourceProfileId = (short)Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Delete":
                    _presentersourceprofile.DeleteSourceProfile();
                    break;
                case "Edit":
                    Response.Redirect("AddSourceProfile.aspx?SourceProfileId=" + SourceProfile.SourceProfileId);
                    break;
            }
        }
        protected void rgSourceProfile_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _presentersourceprofile.GetSourceProfiles();
            rgSourceProfile.DataSource = SourceProfiles;
        }

        protected void likAdd_Click(object sender, EventArgs e)
        {
            //SessionManager.Instance.CurrentMeasurementDevice = null;
            Response.RedirectPermanent("AddSourceProfile.aspx");
        }

        #endregion

        #region IViewSourceProfile Members
        public int SourceProfileId { get; set; }
        public int RegionId { get; set; }
        public int CompanyId { get; set; }
        public string ProfileName { get; set; }
        public string ProfileDesc { get; set; }
        public byte[] Profile { get; set; }
        public bool IsUsed { get; set; }
        public SourceProfile SourceProfile { get; set; }
        public List<SourceProfile> SourceProfiles { get; set; }
        public Dictionary<int, string> CompanyList { get; set; }
        public Dictionary<int, string> RegionList { get; set; }
        #endregion
    }
}