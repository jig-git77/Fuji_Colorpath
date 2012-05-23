using System;
using System.Web.UI.WebControls;
using System.Linq;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Collections.Generic;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddDeviceManagement : BaseControl, IViewAddDeviceManagement
    {
        public string ELicenseSync1DText { get; set; }
        public string ELicenseDlpText { get; set; }
        public string ELicenseInkOptimizationText { get; set; }
        public string ELicenseVarificationText { get; set; }

        private PresenterDeviceManagement _presenterDeviceManagement;
        #region Declaration
        private int DId
        {
            get
            {
                return (ViewState["DeviceID"] != null && !string.IsNullOrEmpty(ViewState["DeviceID"].ToString())) ? Convert.ToInt32(ViewState["DeviceID"]) : 0;
            }
            set { ViewState["DeviceID"] = value.ToString(); }
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterDeviceManagement = new PresenterDeviceManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            ELicenseSync1DText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.Sync1D);
            ELicenseDlpText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.DLP);
            ELicenseInkOptimizationText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.InkOptimization);
            ELicenseVarificationText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.Verification);

            if (Page.IsPostBack) return;

            lblHeader.Text = "Add Device";
            Bindcompany();
            BindDeviceFamily();
            BindBranch();
            BindLicenses();
            
            if (Request.QueryString["DeviceID"] != null)
            {
                lblHeader.Text = "Edit Device";
                SetDevice(Convert.ToInt32(Request.QueryString["DeviceID"]));
            }
        }
        #endregion

        #region Methods
        private void BindDeviceFamily()
        {
            cmbDeviceFamily.Items.Clear();
            DeviceFamilyList = new Dictionary<int, string>();

            _presenterDeviceManagement.GetDeviceFamilyList();
            if (!IsErrorOccured)
            {
                cmbDeviceFamily.DataTextField = "Value";
                cmbDeviceFamily.DataValueField = "Key";
                cmbDeviceFamily.DataSource = DeviceFamilyList;
                cmbDeviceFamily.DataBind();
                cmbDeviceFamily.Items.Insert(0, new ListItem("--Select Device Family--", string.Empty));
            }
        }
        private void Bindcompany()
        {
            cmbCompany.Items.Clear();
            Compnaies = new Dictionary<int, string>();

            _presenterDeviceManagement.GetCompanies();
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
            if (cmbCompany.SelectedIndex != 0 && cmbCompany.SelectedValue != null && !string.IsNullOrEmpty(cmbCompany.SelectedValue))
            {
                Branches = new Dictionary<int, string>();
                CompanyId = Convert.ToInt32(cmbCompany.SelectedValue);

                _presenterDeviceManagement.GetBranches();
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
        private void BindLicenses()
        {
            cmbLicense.Items.Clear();
            if (cmbBranch.SelectedIndex != 0 && cmbBranch.SelectedValue != null && !string.IsNullOrEmpty(cmbBranch.SelectedValue))
            {
                Licenses = new Dictionary<int, string>();
                BranchId = Convert.ToInt32(cmbBranch.SelectedValue);

                _presenterDeviceManagement.GetLicenses();
                if (!IsErrorOccured)
                {
                    cmbLicense.DataTextField = "Value";
                    cmbLicense.DataValueField = "Key";
                    cmbLicense.DataSource = Licenses;
                    cmbLicense.DataBind();
                    if (cmbLicense.Items.Count == 0)
                    {
                        fujiStatusbar.DisplayMessage("No license found for this branch!", false);
                        txtSync1D.Enabled = false;
                        txtDlp.Enabled = false;
                        txtInkOpt.Enabled = false;
                        txtVarification.Enabled = false;
                    }
                    else
                    {
                        cmbLicense.SelectedIndex = 0;
                        txtSync1D.Text = txtDlp.Text = txtInkOpt.Text = txtVarification.Text = "0";                        
                        txtSync1D.Enabled = (License.LicenseType.Where(x => x == ELicenseType.Sync1D).Count() > 0);
                        txtDlp.Enabled = (License.LicenseType.Where(x => x == ELicenseType.DLP).Count() > 0);
                        txtInkOpt.Enabled = (License.LicenseType.Where(x => x == ELicenseType.InkOptimization).Count() > 0);
                        txtVarification.Enabled = (License.LicenseType.Where(x => x == ELicenseType.Verification).Count() > 0);
                    }
                }
            }
        }

        private void SaveDevice()
        {
            if (cmbCompany.SelectedIndex == 0)
            {
                fujiStatusbar.DisplayMessage("Please select company.", false);
                return;
            }
            if (cmbBranch.SelectedIndex == 0)
            {
                fujiStatusbar.DisplayMessage("Please select branch.", false);
                return;
            }
            if (cmbDeviceFamily.SelectedIndex == 0)
            {
                fujiStatusbar.DisplayMessage("Please select device family.", false);
                return;
            }

            int? licenseId = Convert.ToInt32(cmbLicense.SelectedValue.Equals("--Select--") ? null : cmbLicense.SelectedValue);
            Device = new Device()
                         {
                             DeviceName = txtDeviceName.Text,
                             LicenseId = (int)licenseId,
                             DeviceFamily =
                                 new DeviceFamily() { DeviceFamilyId = Convert.ToInt16(cmbDeviceFamily.SelectedValue) },
                             IsActive = chkIsActive.Checked
                         };

            if (txtSync1D.Enabled)
                Device.LicenseTypeProject.Add(ELicenseType.Sync1D, Convert.ToInt32(txtSync1D.Text));
            if (txtDlp.Enabled)
                Device.LicenseTypeProject.Add(ELicenseType.DLP, Convert.ToInt32(txtDlp.Text));
            if (txtInkOpt.Enabled)
                Device.LicenseTypeProject.Add(ELicenseType.InkOptimization, Convert.ToInt32(txtInkOpt.Text));
            if (txtVarification.Enabled)
                Device.LicenseTypeProject.Add(ELicenseType.Verification, Convert.ToInt32(txtVarification.Text));

            if (DId == 0) // Add New company
            {
                _presenterDeviceManagement.InsertDevice();
                if (IsErrorOccured) return;
            }
            else // Edit New company
            {
                Device.DeviceId = DId;
                _presenterDeviceManagement.UpdateDevice();
                if (IsErrorOccured) return;
            }
            Response.Redirect("DeviceManagement.aspx");
        }
        private void SetDevice(int deviceId)
        {
            Device = new Device { DeviceId = deviceId };
            _presenterDeviceManagement.GetDevice();
            if (!IsErrorOccured)
            {
                DId = deviceId;
                cmbCompany.SelectedValue = Device.License.Branch.Company.CompanyId.ToString();
                BindBranch();
                cmbBranch.SelectedValue = Device.License.Branch.BranchId.ToString();
                BindLicenses();
                cmbLicense.SelectedValue = Device.License.LicenseId.ToString();

                txtDeviceName.Text = Device.DeviceName;
                cmbDeviceFamily.SelectedValue = Device.DeviceFamilyId.ToString();
                chkIsActive.Checked = Device.IsActive;

                //-----------In Edit Device page, put MaxProjects value from DB----------------//
                foreach (KeyValuePair<ELicenseType, int> item in Device.LicenseTypeProject)
                {
                    string key = Common.Utilities.GetEnumDescriptionAttributeValue(item.Key);
                    int val = item.Value;

                    if (item.Key.ToString() == "Sync1D")
                    {
                       txtSync1D.Text = val.ToString();
                    }
                    else if (item.Key.ToString() == "DLP")
                    {
                        txtDlp.Text = val.ToString();
                    }
                    else if (item.Key.ToString() == "InkOptimization")
                    {
                        txtInkOpt.Text = val.ToString();
                    }
                    else if (item.Key.ToString() == "Verification")
                    {
                        txtVarification.Text = val.ToString();
                    }
                }
            }
        }
        #endregion

        #region Control Events
        protected void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBranch();
            BindLicenses();
        }
        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLicenses();
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveDevice();
        }
        #endregion

        #region Implement Interface
        public Device Device { get; set; }

        public int CompanyId { get; set; }

        public int BranchId { get; set; }

        public Dictionary<int, string> Compnaies { get; set; }

        public Dictionary<int, string> Branches { get; set; }

        public Dictionary<int, string> Licenses { get; set; }

        public Dictionary<int, string> DeviceFamilyList { get; set; }

        public License License { get; set; }
        #endregion
    }
}