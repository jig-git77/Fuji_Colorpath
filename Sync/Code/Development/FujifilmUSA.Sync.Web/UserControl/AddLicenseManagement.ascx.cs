using System;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Collections.Generic;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddLicenseManagement : BaseControl, IViewAddLicenseManagement
    {
        private PresenterLicenseManagement _presenterLicenseManagement;
        #region Declaration
        private int LId
        {
            get
            {
                return (ViewState["LicenseID"] != null && !string.IsNullOrEmpty(ViewState["LicenseID"].ToString())) ? Convert.ToInt32(ViewState["LicenseID"]) : 0;
            }
            set { ViewState["LicenseID"] = value.ToString(); }
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterLicenseManagement = new PresenterLicenseManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;

            BindCompnay();
            BindLicenseType();
            BindBranch();
            if (Request.QueryString["LicenseId"] != null)
                SetLicense(Convert.ToInt32(Request.QueryString["LicenseID"]));

        }
        #endregion

        #region Methods
        private void BindLicenseType()
        {
            cmbLicenseType.Items.Clear();
            LicenseTypes = new Dictionary<int, string>();
            _presenterLicenseManagement.GetLicenseType();
            if (!IsErrorOccured)
            {
                cmbLicenseType.DataTextField = "Value";
                cmbLicenseType.DataValueField = "Key";
                cmbLicenseType.DataSource = LicenseTypes;
                cmbLicenseType.DataBind();
                cmbLicenseType.Items.Insert(0, new ListItem("--Select Device Family--", string.Empty));
            }
        }
        private void BindCompnay()
        {
            cmbCompany.Items.Clear();
            Compnaies = new Dictionary<int, string>();
            _presenterLicenseManagement.GetCompanies();
            if (!IsErrorOccured)
            {
                cmbCompany.DataTextField = "Value";
                cmbCompany.DataValueField = "Key";
                cmbCompany.DataSource = Compnaies;
                cmbCompany.DataBind();
                cmbCompany.Items.Insert(0, new ListItem("--Select Company--", string.Empty));
            }
        }
        private void BindBranch()
        {
            cmbBranch.Items.Clear();
            if (cmbCompany.SelectedIndex != 0 && cmbCompany.SelectedValue != null)
            {
                Branches = new Dictionary<int, string>();
                CompanyId = Convert.ToInt32(cmbCompany.SelectedValue);
                _presenterLicenseManagement.GetBranches();
                if (!IsErrorOccured)
                {

                    cmbBranch.DataTextField = "Value";
                    cmbBranch.DataValueField = "Key";
                    cmbBranch.DataSource = Branches;
                    cmbBranch.DataBind();
                }
            }
            cmbBranch.Items.Insert(0, new ListItem("--Select Branch--", string.Empty));
        }

        private void SaveDevice()
        {
            if (cmbCompany.SelectedIndex == 0)
            {
                fujiStatusbar.DisplayMessage("Please select compnay.", false);
                return;
            }
            if (cmbBranch.SelectedIndex == 0)
            {
                fujiStatusbar.DisplayMessage("Please select branch.", false);
                return;
            }
            if (cmbLicenseType.SelectedIndex == 0)
            {
                fujiStatusbar.DisplayMessage("Please select license type.", false);
                return;
            }

            License = new License()
                          {
                              Branch = new Branch()
                                            {
                                                BranchId = Convert.ToInt32(cmbBranch.SelectedValue)
                                            }
                                 ,
                              ActivationDate = Convert.ToDateTime(txtActivationDate.Text)
                                 ,
                              ExpirationDate = Convert.ToDateTime(txtExpirationDate.Text)
                                 ,
                              LicenseDesc = txtLicenseDescription.Text
                                 ,
                              LicenseTypeId = Convert.ToInt16(cmbLicenseType.SelectedValue)
                                 ,
                              IsActive = chkIsActive.Checked
                          };
            if (LId == 0) // Add New Compnay
            {
                _presenterLicenseManagement.InsertLicense();
                if (IsErrorOccured) return;
            }
            else // Edit New Compnay
            {
                License.LicenseId = LId;
                _presenterLicenseManagement.UpdateLicense();
                if (IsErrorOccured) return;
            }
            Response.Redirect("LicenseManagement.aspx");
        }
        private void SetLicense(int licenseId)
        {
            License = new License { LicenseId = licenseId };
            _presenterLicenseManagement.GetLicens();
            if (!IsErrorOccured)
            {
                LId = License.LicenseId;
                cmbCompany.SelectedValue = License.Branch.CompanyId.ToString();
                BindBranch();
                cmbBranch.SelectedValue = License.BranchId.ToString();
                cmbLicenseType.SelectedValue = License.LicenseTypeId.ToString();
                txtLicenseDescription.Text = License.LicenseDesc;
                txtActivationDate.Text = Convert.ToDateTime(License.ActivationDate).ToString("MM/dd/yyyy");
                txtExpirationDate.Text = Convert.ToDateTime(License.ExpirationDate).ToString("MM/dd/yyyy");
                chkIsActive.Checked = License.IsActive;
            }
        }
        #endregion

        #region Control Events
        protected void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBranch();
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveDevice();
        }
        #endregion

        #region Implement Interface
        public License License { get; set; }
        public Dictionary<int, string> LicenseTypes { get; set; }
        public int CompanyId { get; set; }
        public Dictionary<int, string> Compnaies { get; set; }
        public Dictionary<int, string> Branches { get; set; }
        #endregion
    }
}