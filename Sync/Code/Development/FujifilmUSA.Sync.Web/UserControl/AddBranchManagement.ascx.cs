using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Collections.Generic;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddBranchManagement : System.Web.UI.UserControl, IViewAddBranchManagement
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
            if (!Page.IsPostBack)
            {
                BindCompnay();
                if (Request.QueryString["CompanyId"] == null) //Add New Compnay 
                {
                }
                else //Edit Compnay
                {
                    SetBranch(
                              Convert.ToInt32(Request.QueryString["BranchId"]),
                              Request.QueryString["BranchName"].ToString(),
                              Request.QueryString["Location"].ToString(),
                              Convert.ToInt32(Request.QueryString["CompanyID"]),
                              Convert.ToBoolean(Request.QueryString["IsActive"]));
                }
            }
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
        }
        private void SaveCompnay()
        {
            Branch = new Branch();
            Branch.BranchName = txtBranchName.Text;
            Branch.Location = txtLocation.Text;
            Branch.CompanyId = Convert.ToByte(cmbCompanyName.SelectedValue);
            Branch.IsActive = chkIsActive.Checked;
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
        private void SetBranch(int branchId,string branchName,string location,int compnayId,bool isActive)
        {
            BId = branchId;
            txtBranchName.Text = branchName;
            txtLocation.Text = location;
            cmbCompanyName.SelectedValue = compnayId.ToString();
            chkIsActive.Checked = isActive;
        }
        #endregion

        #region Control Events
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            SaveCompnay();
        }
        #endregion

        #region Implement Interface
        public Branch Branch { get; set; }
        public Dictionary<int, string> Compnaies { get; set; }
        #endregion


        
    }
}