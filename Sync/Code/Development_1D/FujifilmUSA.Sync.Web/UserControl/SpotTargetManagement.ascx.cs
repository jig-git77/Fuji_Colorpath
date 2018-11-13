using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity.CPSpot;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SpotTargetManagement : BaseControl, IViewSpotTargetManagement
    {
        #region Declaration
        private PresenterSpotTargetManagement _presenterSpotTarget;
        public string SpotTargetManagement_DeleteSpot_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("SpotTargetManagement_DeleteSpot_Confirmation");
            }
        }
        #endregion

        #region Constructor
        public SpotTargetManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterSpotTarget = new PresenterSpotTargetManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rgSpotTarget.PageSize = Common.Utilities.PageSize;
            if (!Page.IsPostBack)
            {
                rgSpotTarget.ItemCommand += rgSpotTarget_ItemCommand;
                BindSpotTargetList();
            }
        }
        #endregion

        #region Methods

        public void BindSpotTargetList()
        {
            _presenterSpotTarget.GetSpotTargets();
            if (!IsErrorOccured)
            {
                rgSpotTarget.DataSource = SpotTargetList;
                rgSpotTarget.DataBind();
            }
        }

        #endregion

        #region Control Events
        protected void rgSpotTarget_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "ClearFilter":
                    foreach (GridColumn column in rgSpotTarget.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgSpotTarget.MasterTableView.FilterExpression = string.Empty;
                    rgSpotTarget.MasterTableView.Rebind();
                    break;
                case "Delete":
                    SpotTargetID = Convert.ToInt32(e.CommandArgument);
                    int deleteResult = _presenterSpotTarget.DeleteSpotTarget();
                    if (deleteResult == -2)
                        lblMessage.Text = "This spot target has BCO projects open, so please delete those first before deleting this.";
                    else
                        lblMessage.Text = "";
                    break;
                case "Edit":
                    SpotTargetID = Convert.ToInt32(e.CommandArgument);
                    string parms = Service.Common.Encryption.CryptHelper.EncryptString(SpotTargetID.ToString(), FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey);
                    Response.Redirect("AddSpotTarget.aspx?SpotTargetId=" + ContentEncoder.Encode(parms));
                    break;
            }
        }
        protected void rgSpotTarget_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _presenterSpotTarget.GetSpotTargets();
            if (!IsErrorOccured)
            {
                rgSpotTarget.DataSource = SpotTargetList;
            }
        }

        protected void likAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddSpotTarget.aspx");
        }

        #endregion

        #region IViewSpotTargetManagement Members
        //public SpotMeasurement ReferenceChartType { get; set; }
        public int SpotTargetID { get; set; }
        public List<SpotTarget> SpotTargetList { get; set; }
        #endregion
    }
}