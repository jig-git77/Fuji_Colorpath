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
                rgSourceProfile.ItemCommand += rgSourceProfile_ItemCommand;
                BindSourceProfileList();
            }
        }
        #endregion

        #region Methods

        public void BindSourceProfileList()
        {
            _presentersourceprofile.GetSourceProfiles();
            if (!IsErrorOccured)
            {
                rgSourceProfile.DataSource = SourceProfiles;
                rgSourceProfile.DataBind();
            }
        }

        #endregion

        #region Control Events
        protected void rgSourceProfile_ItemCommand(object sender, GridCommandEventArgs e)
        {
            SourceProfile = new Entity.SourceProfile();
            switch (e.CommandName)
            {
                case "ClearFilter":
                    foreach (GridColumn column in rgSourceProfile.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgSourceProfile.MasterTableView.FilterExpression = string.Empty;
                    rgSourceProfile.MasterTableView.Rebind();
                    break;
                case "Delete":
                    SourceProfile.SourceProfileId = (short)Convert.ToInt32(e.CommandArgument);
                    _presentersourceprofile.DeleteSourceProfile();
                    break;
                case "Edit":
                    SourceProfile.SourceProfileId = (short)Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("AddSourceProfile.aspx?SourceProfileId=" + SourceProfile.SourceProfileId);
                    break;
            }
        }
        protected void rgSourceProfile_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _presentersourceprofile.GetSourceProfiles();
            if (!IsErrorOccured)
            {
                rgSourceProfile.DataSource = SourceProfiles;
            }
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