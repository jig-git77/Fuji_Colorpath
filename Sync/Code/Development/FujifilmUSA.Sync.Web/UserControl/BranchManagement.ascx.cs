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
    public partial class BranchManagement : BaseControl, IViewBranchManagement
    {
        private PresenterBranchManagement _presenterBranchManagement;

        #region Declaration
        public string BranchManagement_DeleteBranch_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("BranchManagement_DeleteBranch_Confirmation");
            }
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterBranchManagement = new PresenterBranchManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindBranches();
            }
        }
        #endregion

        #region Methods
        private void GetBranches()
        {
            BranchList = new List<Branch>();
            _presenterBranchManagement.GetBranches();
        }
        private void BindBranches()
        {
            GetBranches();
            if (!IsErrorOccured)
            {
                rgBranch.DataSource = BranchList;
                rgBranch.DataBind();
            }
        }
        #endregion

        #region Control Events
        protected void likAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddBranchManagement.aspx");
        }
        protected void rgBranch_ItemCommand(object sender, GridCommandEventArgs e)
        {
            Branch = new Entity.Branch();
            switch (e.CommandName)
            {
                case "Filter":
                    break;
                case "ClearFilter":
                    foreach (GridColumn column in rgBranch.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgBranch.MasterTableView.FilterExpression = string.Empty;
                    rgBranch.MasterTableView.Rebind();
                    break;
                case "Delete":
                    Branch.BranchId = Convert.ToInt32(e.CommandArgument);
                    _presenterBranchManagement.DeleteBranch();
                    break;
                case "Edit":
                    Branch.BranchId = Convert.ToInt32(e.CommandArgument);
                    GridDataItem item = (GridDataItem)e.Item;
                    Response.Redirect("AddBranchManagement.aspx?BranchId=" + Branch.BranchId);
                    break;
            }
        }
        protected void rgBranch_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetBranches();
            if (!IsErrorOccured)
            {
                rgBranch.DataSource = BranchList;
            }
        }


        #endregion

        #region Implement Interface
        public Branch Branch { get; set; }
        public List<Branch> BranchList { get; set; }
        public Dictionary<int, string> Compnaies { get; set; }
        #endregion
    }
}