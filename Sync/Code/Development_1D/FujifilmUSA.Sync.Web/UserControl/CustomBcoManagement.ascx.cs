using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSpot;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.Data;
using Telerik.Web.UI;
using Telerik.Web.UI.Skins;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class CustomBcoManagement : BaseControl, IViewSpotTargetManagement
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

        /// <summary>
        /// CustomBcoManagement
        /// </summary>
        public CustomBcoManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        #region Page Events

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            _presenterSpotTarget = new PresenterSpotTargetManagement(this);
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// BindSpotTargetList
        /// </summary>
        public void BindSpotTargetList()
        {
            _presenterSpotTarget.GetSpotTargets_CustomBCO();
            if (!IsErrorOccured)
            {
                rgSpotTarget.DataSource = SpotTargetList;
                rgSpotTarget.DataBind();

                if (SessionManager.Instance.CurrentUser.UserCompany.CompanyId > 0)
                {
                    rgSpotTarget.MasterTableView.GetColumn("CompanyName").Visible = false;
                }
            }
        }

        #endregion

        #region Control Events

        /// <summary>
        /// rgSpotTarget_ItemCommand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        lblMessage.Text = "This spot target has BCO projects open, so please delete those first before deleting this.";// "This spot target has BCO projects open, so please delete those first before deleting this.";
                    else
                        lblMessage.Text = "";
                    break;
                case "Edit":
                    SpotTargetID = Convert.ToInt32(e.CommandArgument);
                    string parms = Service.Common.Encryption.CryptHelper.EncryptString(SpotTargetID.ToString(), FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey);
                    Response.Redirect("AddCustomBcoTarget.aspx?SpotTargetId=" + ContentEncoder.Encode(parms));
                    break;
            }
        }

        /// <summary>
        /// rgSpotTarget_NeedDataSource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rgSpotTarget_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _presenterSpotTarget.GetSpotTargets_CustomBCO();
            if (!IsErrorOccured)
            {
                rgSpotTarget.DataSource = SpotTargetList;
            }
        }

        /// <summary>
        /// likAdd_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void likAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddCustomBcoTarget.aspx");
        }

        /// <summary>
        /// likDelete_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void likDelete_Click(object sender, EventArgs e)
        {
            if (rgSpotTarget.Items.Count > 0)
            {
                lstDeleteSpotTarget = new List<string>();

                foreach (GridDataItem item in rgSpotTarget.MasterTableView.Items)
                {
                    CheckBox chkobj = item["DeleteConfirm"].FindControl("chkDelete") as CheckBox;
                    if (chkobj.Checked)
                    {
                        lstDeleteSpotTarget.Add((item["SpotTargetID"].FindControl("lblSpotTargetID") as Label).Text);
                    }
                }

                if (lstDeleteSpotTarget.Count > 0)
                {
                    int deleteResult = _presenterSpotTarget.DeleteSpotTargetList();
                    if (deleteResult == -2)
                        lblMessage.Text = "This spot target has BCO projects open, so please delete those first before deleting this.";// "This spot target has BCO projects open, so please delete those first before deleting this.";
                    else
                    {
                        lblMessage.Text = "";
                        rgSpotTarget_NeedDataSource(null, null);
                        rgSpotTarget.DataBind();
                    }
                }
            }
        }

        #endregion

        #region IViewSpotTargetManagement Members
        //public SpotMeasurement ReferenceChartType { get; set; }
        public int SpotTargetID { get; set; }
        public List<SpotTarget> SpotTargetList { get; set; }
        public List<string> lstDeleteSpotTarget { get; set; }
        #endregion
    }
}