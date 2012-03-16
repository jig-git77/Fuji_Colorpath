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
    public partial class BranchManagement : System.Web.UI.UserControl, IViewBranchManagement
    {
        private PresenterBranchManagement _presenterBranchManagement;

        #region Declaration
        public string CompanyManagement_DeleteCompany_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("CompanyManagement_DeleteCompany_Confirmation");
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
            SessionManager.CheckLogin();
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
            _presenterBranchManagement.GetBranchs();
        }
        private void BindBranches()
        {
            GetBranches();
            rgBranch.DataSource = BranchList;
            rgBranch.DataBind();
        }
        #endregion

        #region Control Events
        protected void likAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddBranchManagement.aspx");
        }
        protected void rgBranch_ItemCommand(object sender, GridCommandEventArgs e)
        {
            Branch branch = new Entity.Branch();
            switch (e.CommandName)
            {
                case "Delete" :
                    branch.BranchId = Convert.ToInt32(e.CommandArgument);
                    _presenterBranchManagement.DeleteBranch();
                    break;
                case "Edit":
                    branch.BranchId = Convert.ToInt32(e.CommandArgument);
                    GridDataItem item = (GridDataItem)e.Item; 
                    Response.Redirect("AddBranchManagement.aspx?BranchId=" + branch.CompanyId
                                            + "&BranchName=" + item.GetDataKeyValue("BranchName").ToString()
                                            + "&Location=" + item.GetDataKeyValue("Location").ToString()
                                            + "&CompanyID=" +  item.GetDataKeyValue("CompanyID").ToString()
                                            + "&IsActive=" + item.GetDataKeyValue("IsActive").ToString()
                                            );  
                    break;
            }
        }
        protected void rgBranch_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetBranches();
            rgBranch.DataSource = BranchList;
        }
      

         #endregion

        #region Implement Interface
        public Branch Branch { get; set; }
        public List<Branch> BranchList { get; set; } 
        public Dictionary<int,string>  Compnaies{ get; set; }
        #endregion

    }
}