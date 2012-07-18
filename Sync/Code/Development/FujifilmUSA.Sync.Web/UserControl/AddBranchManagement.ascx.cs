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
            get
            {
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
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;

            lblHeader.Text = "Add Branch";
            BindRegionDropDown();
            if (Request.QueryString["BranchId"] != null)
            {
                SetBranch(Convert.ToInt32(Request.QueryString["BranchId"]));
                lblHeader.Text = "Edit Branch"; 
            }

        }
        #endregion

        #region Methods

        private void BindRegionDropDown()
        {
            _presenterBranchManagement.RegionList();
            if (!IsErrorOccured)
            {
                cmbRegion.DataSource = RegionList;
                cmbRegion.DataBind();
                cmbRegion.Items.Insert(0, new ListItem("-- Select Region --", "0"));
            }
        }

        private void Bindcompany()
        {
            Companies = new Dictionary<int, string>();
            RegionId = Convert.ToInt32(cmbRegion.SelectedValue);
            _presenterBranchManagement.GetCompaniesByRegion();
            if (!IsErrorOccured)
            {
                cmbCompanyName.DataTextField = "Value";
                cmbCompanyName.DataValueField = "Key";
                cmbCompanyName.DataSource = Companies;
                cmbCompanyName.DataBind();
                cmbCompanyName.Items.Insert(0, new ListItem("--Select--", string.Empty));
            }
        }
        private void SaveBranch()
        {            
            Branch = new Branch
                         {
                             BranchName = txtBranchName.Text,
                             Location = txtLocation.Text,
                             CompanyId = Convert.ToByte(cmbCompanyName.SelectedValue),
                             SupportID= txtSupportID.Text,
                             IsActive = chkIsActive.Checked
                         };
            if (BId == 0) // Add New branch
            {
                _presenterBranchManagement.InsertBranch();
                if (IsErrorOccured) return;
            }
            else // Edit New branch
            {
                Branch.BranchId = BId;
                _presenterBranchManagement.UpdateBranch();
                if (IsErrorOccured) return;
            }
            Response.Redirect("BranchManagement.aspx");
        }
        private void SetBranch(int branchId)
        {
            Branch = new Branch { BranchId = branchId };
            _presenterBranchManagement.GetBranch();
            if (!IsErrorOccured)
            {
                BId = branchId;
                txtBranchName.Text = Branch.BranchName;
                txtLocation.Text = Branch.Location;
                cmbRegion.SelectedValue = Convert.ToString(Branch.Region.RegionId);
                cmbRegion_SelectedIndexChanged(null, null);
                cmbCompanyName.SelectedValue = Branch.CompanyId.ToString();
                chkIsActive.Checked = Branch.IsActive;
                txtSupportID.Text = Branch.SupportID;         
            }
        }
        #endregion

        #region Control Events

        protected void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bindcompany();
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveBranch();
        }
        #endregion

        #region Implement Interface
        public Branch Branch { get; set; }
        public Dictionary<int, string> Companies { get; set; }
        public Dictionary<int, string> RegionList { get; set; }
        public int RegionId { get; set; }
        #endregion
    }
}