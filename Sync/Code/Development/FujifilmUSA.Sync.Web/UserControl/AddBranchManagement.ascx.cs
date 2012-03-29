using System;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Collections.Generic;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddBranchManagement : BaseControl, IViewAddBranchManagement
    {
        private PresenterBranchManagement _presenterBranchManagement;
        
        #region Declaration
        private int BId
        {
            get {
                return (ViewState["BranchId"] != null && !string.IsNullOrEmpty(ViewState["BranchId"].ToString())) ? Convert.ToInt32(ViewState["BranchId"]) : 0; 
                }
            set { ViewState["BranchId"] = value.ToString(); }
        }
        #endregion


        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterBranchManagement = new PresenterBranchManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ucAdminStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required,true);
            if (Page.IsPostBack) return;
            
            BindCompnay();
            if (Request.QueryString["BranchId"] != null)
                SetBranch(Convert.ToInt32(Request.QueryString["BranchId"]));
        
        }
        #endregion

        #region Methods
        private void BindCompnay()
        {
            Compnaies = new Dictionary<int, string>();
            _presenterBranchManagement.GetCompanies();
            cmbCompanyName.DataTextField = "Value";
            cmbCompanyName.DataValueField = "Key";
            cmbCompanyName.DataSource = Compnaies;
            cmbCompanyName.DataBind();
            cmbCompanyName.Items.Insert(0,new ListItem("--Select--",string.Empty));
        }
        private void SaveCompnay()
        {
            Branch = new Branch
                         {
                             BranchName = txtBranchName.Text,
                             Location = txtLocation.Text,
                             CompanyId = Convert.ToByte(cmbCompanyName.SelectedValue),
                             IsActive = chkIsActive.Checked
                         };
            if (BId == 0) // Add New Compnay
            {
                _presenterBranchManagement.InsertBranch();
            }
            else // Edit New Compnay
            {
                Branch.BranchId = BId;
                _presenterBranchManagement.UpdateBranch();
            }
            Response.Redirect("BranchManagement.aspx");
        }
        private void SetBranch(int branchId)
        {
            Branch = new Branch {BranchId = branchId};
            _presenterBranchManagement.GetBranch(); 

            BId = branchId;
            txtBranchName.Text = Branch.BranchName;
            txtLocation.Text = Branch.Location;
            cmbCompanyName.SelectedValue = Branch.CompanyId.ToString();
            chkIsActive.Checked = Branch.IsActive;
        }
        #endregion

        #region Control Events
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            ucAdminStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveCompnay();
        }
        #endregion

        #region Implement Interface
        public Branch Branch { get; set; }
        public Dictionary<int, string> Compnaies { get; set; }
        #endregion
    }
}