using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class MToolVersionManagement : BaseControl, IViewMToolVersionManagement
    {
        private PresenterMToolVersionManagement _presenterMToolVersionManagement;

        #region Declaration
        public string MToolVersionManagement_DeleteMToolVersion_Confirmation
        {
            get
            {
                return "Are your sure want to delete this version?";
            }
        }
        #endregion
        public MToolVersionManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterMToolVersionManagement = new PresenterMToolVersionManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rgMToolList.PageSize = Common.Utilities.PageSize;
            if (!Page.IsPostBack)
            {
                BindMToolVersionList();
            }
        }
        #endregion

        #region Methods
        private void GetMToolVersionList()
        {
            MToolVersionList = new List<MToolVersionUpdateList>();
            _presenterMToolVersionManagement.GetMToolVersions();
        }
        private void BindMToolVersionList()
        {
            GetMToolVersionList();
            if (!IsErrorOccured)
            {
                rgMToolList.DataSource = MToolVersionList;
                rgMToolList.DataBind();
            }
        }
        #endregion

        #region Control Events
        protected void likAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddMToolVersionManagement.aspx");
        }
        protected void rgMToolList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            MToolVersion = new Entity.MToolVersionUpdateList();
            switch (e.CommandName)
            {
                case "Filter":
                    break;
                case "ClearFilter":
                    foreach (GridColumn column in rgMToolList.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgMToolList.MasterTableView.FilterExpression = string.Empty;
                    rgMToolList.MasterTableView.Rebind();
                    break;                
                case "Edit":
                    MToolVersion.OSPlatformDetailsID = Convert.ToInt16(e.CommandArgument);
                    GridDataItem item = (GridDataItem)e.Item;
                    string parms = Service.Common.Encryption.CryptHelper.EncryptString(MToolVersion.OSPlatformDetailsID.ToString(), FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey);
                    Response.Redirect("AddMToolVersionManagement.aspx?OSPlatformDetailsID=" + ContentEncoder.Encode(parms));
                    break;
                case "Delete":
                    MToolVersion.OSPlatformDetailsID = Convert.ToInt16(e.CommandArgument);
                    _presenterMToolVersionManagement.DeleteMToolVersion();
                    break;
            }
        }
        protected void rgMToolList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetMToolVersionList();
            if (!IsErrorOccured)
            {
                rgMToolList.DataSource = MToolVersionList;
            }
        }


        #endregion

        #region Implement Interface
        public MToolVersionUpdateList MToolVersion { get; set; }
        public List<MToolVersionUpdateList> MToolVersionList { get; set; }
        public Dictionary<int,string> OSPlatformNameList { get; set; }
        #endregion
    }
}