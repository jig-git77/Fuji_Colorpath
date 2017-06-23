using System;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Collections.Generic;
using Telerik.Web.UI;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddBranchManagement : BaseControl, IViewAddBranchManagement
    {
        private PresenterBranchManagement _presenterBranchManagement;

        #region Private Variables - Globalization
        private const string _recAddBranch = "addBranchText";
        private const string _recEditBranch = "editBranchText";
        private const string _recSelectRegionText = "selectRegionText";
        private const string _recSelectCompanyText = "selectCompanyText";
        #endregion

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

        public AddBranchManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterBranchManagement = new PresenterBranchManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;

            lblHeader.Text = GetResourceString(_recAddBranch);  //"Add Branch";
            BindRegionDropDown();
            if (Request.QueryString["BranchId"] != null)
            {
                var BranchId = Request.QueryString["BranchId"];
                var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(BranchId, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                int _BranchId;
                if (Int32.TryParse(id, out _BranchId))
                {
                    SetBranch(_BranchId);
                    lblHeader.Text = GetResourceString(_recEditBranch); //"Edit Branch"; 
                }
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
                cmbRegion.Items.Insert(0, new ListItem(GetResourceString(_recSelectRegionText), "0"));
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
                cmbCompanyName.Items.Insert(0, new ListItem(GetResourceString(_recSelectCompanyText), string.Empty));
            }
        }
        private void SaveBranch()
        {            
            Branch = new Branch
                         {
                             BranchName = txtBranchName.Text,
                             Location = txtLocation.Text,
                             CompanyId = Convert.ToInt32(cmbCompanyName.SelectedValue),
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