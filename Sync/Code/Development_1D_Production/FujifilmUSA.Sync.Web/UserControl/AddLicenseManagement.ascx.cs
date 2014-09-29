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
    public partial class AddLicenseManagement : BaseControl, IViewAddLicenseManagement
    {
        #region Private Variables - Globalization
        private const string _recAddLicense = "lblHeaderResource1.Text";
        private const string _recEditLicense = "editLicenseText";
        private const string _recSelectCompany = "selectCompanyText";
        private const string _recSelectBranch = "selectBranchText";
        private const string _recPlzSelectCompany = "plzSelectCompany";
        private const string _recPlzSelectBranch = "plzSelectBranch";
        private const string _recPlzSelectLicenseType = "plzSelectLicenseType";
        #endregion

        #region Declaration
        private PresenterLicenseManagement _presenterLicenseManagement;
        private int LId
        {
            get
            {
                return (ViewState["LicenseID"] != null && !string.IsNullOrEmpty(ViewState["LicenseID"].ToString())) ? Convert.ToInt32(ViewState["LicenseID"]) : 0;
            }
            set { ViewState["LicenseID"] = value.ToString(); }
        }
        #endregion
        public AddLicenseManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterLicenseManagement = new PresenterLicenseManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;

            Bindcompany();
            BindLicenseType();
            BindBranch();
            lblHeader.Text = GetResourceString(_recAddLicense); //"Add License";
            if (Request.QueryString["LicenseId"] != null)
            {
                var LicenseId = Request.QueryString["LicenseId"];
                var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(LicenseId, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                int _LicenseId;
                if (Int32.TryParse(id, out _LicenseId))
                {
                    lblHeader.Text = GetResourceString(_recEditLicense);  //"Edit License";
                    SetLicense(_LicenseId); 
                }
            }
        }
        #endregion

        #region Methods
        private void BindLicenseType()
        {
            chkSync1D.Text = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.Sync1D);
            chkDlp.Text = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.DLP);
            chkInk.Text = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.InkOptimization);
            chkInk_Source.Text = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.InkOptimizationSource);
            chkSync4D_Certification.Text = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.Sync4DCertification); 
        }
        private void Bindcompany()
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
                cmbCompany.Items.Insert(0, new ListItem(GetResourceString(_recSelectCompany), string.Empty));//--Select Company--
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
            cmbBranch.Items.Insert(0, new ListItem(GetResourceString(_recSelectBranch), string.Empty));//--Select Branch--
        }

        private void SaveDevice()
        {
            if (cmbCompany.SelectedIndex == 0)
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recPlzSelectCompany), false);//Please select company.
                return;
            }
            if (cmbBranch.SelectedIndex == 0)
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recPlzSelectBranch), false);
                return;
            }
            if (!chkSync1D.Checked && !chkDlp.Checked && !chkInk.Checked && !chkInk_Source.Checked && !chkSync4D_Certification.Checked)
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recPlzSelectLicenseType), false);//Please select any one license type.
                return;
            }

            License = new License()
                          {
                              Branch = new Branch()
                                            {
                                                BranchId = Convert.ToInt32(cmbBranch.SelectedValue)
                                            },
                              ActivationDate = Convert.ToDateTime(txtActivationDate.Text),
                              ExpirationDate = Convert.ToDateTime(txtExpirationDate.Text),
                              LicenseDesc = txtLicenseDescription.Text,
                              IsActive = chkIsActive.Checked
                          };
            if (chkSync1D.Checked)
                License.LicenseType.Add(ELicenseType.Sync1D);
            if (chkDlp.Checked)
                License.LicenseType.Add(ELicenseType.DLP);
            if (chkInk.Checked)
                License.LicenseType.Add(ELicenseType.InkOptimization);
            if (chkInk_Source.Checked)
                License.LicenseType.Add(ELicenseType.InkOptimizationSource);
            if (chkSync4D_Certification.Checked)
                License.LicenseType.Add(ELicenseType.Sync4DCertification);

            if (LId == 0) // Add New company
            {
                _presenterLicenseManagement.InsertLicense();
                if (IsErrorOccured) return;
            }
            else // Edit New company
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
                License.LicenseType.ForEach(x =>
                                                {
                                                    switch (x)
                                                    {
                                                        case ELicenseType.Sync1D:
                                                            chkSync1D.Checked = true;
                                                            break;
                                                        case ELicenseType.DLP:
                                                            chkDlp.Checked = true;
                                                            break;
                                                        case ELicenseType.InkOptimization:
                                                            chkInk.Checked = true;
                                                            break;
                                                        case ELicenseType.InkOptimizationSource:
                                                            chkInk_Source.Checked = true;
                                                            break;
                                                        case ELicenseType.Sync4DCertification:
                                                            chkSync4D_Certification.Checked = true;
                                                            break;
                                                    }
                                                });
                txtLicenseDescription.Text = License.LicenseDesc;
                txtActivationDate.Text = Convert.ToDateTime(License.ActivationDate).ToString("d");
                txtExpirationDate.Text = Convert.ToDateTime(License.ExpirationDate).ToString("d");
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
        public int CompanyId { get; set; }
        public Dictionary<int, string> Compnaies { get; set; }
        public Dictionary<int, string> Branches { get; set; }
        #endregion
    }
}