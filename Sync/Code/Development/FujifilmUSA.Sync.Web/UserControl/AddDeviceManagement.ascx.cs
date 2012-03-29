using System;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Collections.Generic;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddDeviceManagement : BaseControl, IViewAddDeviceManagement
    {
        private PresenterDeviceManagement _presenterDeviceManagement;
        #region Declaration
        private int DId
        {
            get {
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
            ucAdminStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;

            BindCompnay();
            BindDeviceFamily();
            BindBranch();
            BindLicenses();
            if (Request.QueryString["DeviceID"] != null)
                SetDevice(Convert.ToInt32(Request.QueryString["DeviceID"]));
        }
        #endregion

        #region Methods
        private void BindDeviceFamily()
        {
            cmbDeviceFamily.Items.Clear();
            DeviceFamilyList = new Dictionary<int, string>();
            _presenterDeviceManagement.GetDeviceFamilyList();
            cmbDeviceFamily.DataTextField = "Value";
            cmbDeviceFamily.DataValueField = "Key";
            cmbDeviceFamily.DataSource = DeviceFamilyList;
            cmbDeviceFamily.DataBind();
            cmbDeviceFamily.Items.Insert(0, new ListItem("--Select Device Family--", string.Empty));
        }
        private void BindCompnay()
        {
            cmbCompany.Items.Clear();
            Compnaies = new Dictionary<int, string>();
            _presenterDeviceManagement.GetCompanies();
            cmbCompany.DataTextField = "Value";
            cmbCompany.DataValueField = "Key";
            cmbCompany.DataSource = Compnaies;
            cmbCompany.DataBind();
            cmbCompany.Items.Insert(0, new ListItem("--Select Company--", string.Empty));
        }
        private void BindBranch()
        {
            cmbBranch.Items.Clear();
            if (cmbCompany.SelectedIndex != 0 && cmbCompany.SelectedValue != null && !string.IsNullOrEmpty(cmbCompany.SelectedValue))
            {
                Branches = new Dictionary<int, string>();
                CompanyId = Convert.ToInt32(cmbCompany.SelectedValue);
                _presenterDeviceManagement.GetBranches();
                cmbBranch.DataTextField = "Value";
                cmbBranch.DataValueField = "Key";
                cmbBranch.DataSource = Branches;
                cmbBranch.DataBind();
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
                cmbLicense.DataTextField = "Value";
                cmbLicense.DataValueField = "Key";
                cmbLicense.DataSource = Licenses;
                cmbLicense.DataBind();
            }
            cmbLicense.Items.Insert(0, new ListItem("--Select License--", string.Empty));
        }
        private void SaveDevice()
        {
            if(cmbCompany.SelectedIndex == 0)
            {
                ucAdminStatusbar.DisplayMessage("Please select compnay.", false);
                return;
            }
            if (cmbBranch.SelectedIndex == 0)
            {
                ucAdminStatusbar.DisplayMessage("Please select branch.", false);
                return;
            }
            if (cmbLicense.SelectedIndex == 0)
            {
                ucAdminStatusbar.DisplayMessage("Please select license.", false);
                return;
            }
            if (cmbDeviceFamily.SelectedIndex == 0)
            {
                ucAdminStatusbar.DisplayMessage("Please select device family.", false);
                return;
            }

            int? licenseId = Convert.ToInt32(cmbLicense.SelectedValue.Equals("--Select--") ? null : cmbLicense.SelectedValue);
            Device = new Device()
                         {
                             DeviceName = txtDeviceName.Text,
                             LicenseId = (int) licenseId,
                             DeviceFamily =
                                 new DeviceFamily() {DeviceFamilyId = Convert.ToInt16(cmbDeviceFamily.SelectedValue)},
                             IsActive = chkIsActive.Checked
                         };
            if (DId == 0) // Add New Compnay
            {
               _presenterDeviceManagement.InsertDevice();
            }
            else // Edit New Compnay
            {
                Device.DeviceId = DId;
                _presenterDeviceManagement.UpdateDevice();
            }
            Response.Redirect("DeviceManagement.aspx");
        }
        private void SetDevice(int deviceId)
        {
            Device = new Device {DeviceId = deviceId};
            _presenterDeviceManagement.GetDevice();  
            DId = deviceId;
            cmbCompany.SelectedValue = Device.License.Branch.Company.CompanyId.ToString();
            BindBranch();
            cmbBranch.SelectedValue = Device.License.Branch.BranchId.ToString();
            BindLicenses();
            cmbLicense.SelectedValue = Device.License.LicenseId.ToString();

            txtDeviceName.Text = Device.DeviceName;
            cmbDeviceFamily.SelectedValue = Device.DeviceFamilyId.ToString();
            chkIsActive.Checked = Device.IsActive;

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
            ucAdminStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
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

        #endregion
    }
}