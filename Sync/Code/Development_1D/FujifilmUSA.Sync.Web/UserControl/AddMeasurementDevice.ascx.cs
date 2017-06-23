using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddMeasurementDevice : BaseControl, IViewDevice
    {
        #region Private Variables - Globalization
        private const string _recAddMeasurementDevice = "lblHeaderResource1.Text";
        private const string _recEditMeasurementDevice = "editMeasurementDeviceText";
        private const string _recSelectBranch = "selectBranchText";
        private const string _recSelectCompany = "selectCompanyText";
        private const string _recSelectLicenseType = "selectLicenseText";
        private const string _recSelectInstrumentType = "selectInstrumentTypeText";
        #endregion

        public AddMeasurementDevice()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region "Declaration"
        private PresenterDevice _presenterdevice;
        private int MId
        {
            get
            {
                return (ViewState["MeasurementDeviceId"] != null && !string.IsNullOrEmpty(ViewState["MeasurementDeviceId"].ToString())) ? Convert.ToInt32(ViewState["MeasurementDeviceId"]) : 0;
            }
            set { ViewState["MeasurementDeviceId"] = value.ToString(); }
        }
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presenterdevice = new PresenterDevice(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (IsPostBack) return;
            
            BindCompanyDropDown();
            cmbCompanyName_SelectedIndexChanged(null, null);
            cmbBranch_SelectedIndexChanged(null, null);
            BindInstrumentTypeDropDown();
            lblHeader.Text = GetResourceString(_recAddMeasurementDevice); // "Add Measurement Device";
            if (Request.QueryString["MeasurementDeviceId"] != null)
            {
                var MeasurementDeviceId = Request.QueryString["MeasurementDeviceId"];
                var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(MeasurementDeviceId, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                int _MeasurementDeviceId;
                if (Int32.TryParse(id, out _MeasurementDeviceId))
                {
                    lblHeader.Text = GetResourceString(_recEditMeasurementDevice); //"Edit Measurement Device";
                    LoadMeasurementDevice(_MeasurementDeviceId);
                }
            }
        }

        #endregion

        #region Control events

        protected void cmbCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBranch.Items.Clear();
            if (cmbCompanyName.SelectedIndex != 0)
            {
                BindBranchDropDown(int.Parse(cmbCompanyName.SelectedValue));
            }
            else
                cmbBranch.Items.Insert(0, new ListItem(GetResourceString(_recSelectBranch), ""));//-- Select Branch --
            cmbBranch_SelectedIndexChanged(null, null);
        }

        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbLicenseType.Items.Clear();
            if (cmbBranch.SelectedIndex != 0)
            {
                BindLicenseDropDown(int.Parse(cmbBranch.SelectedValue));
            }
            else
                cmbLicenseType.Items.Insert(0, new ListItem(GetResourceString(_recSelectLicenseType), ""));//-- Select License Type --

        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            MeasurementDevice = new MeasurementDevice();
            MeasurementDevice.LicenseId = Convert.ToInt32(cmbLicenseType.SelectedValue);
            MeasurementDevice.SerialNumber = txtSerialNumber.Text.Trim();
            MeasurementDevice.InstrumentTypeId = (short)(Convert.ToInt32(cmbInstrumentType.SelectedValue));
            MeasurementDevice.Description = txtDescription.Text;
            if (chkIsActive.Checked == true)
                MeasurementDevice.IsActive = true;
            else
                MeasurementDevice.IsActive = false;

            if (hdnMeasurementDeviceId.Value == "0")
            {
                _presenterdevice.InsertMeasurementDevice();
                if (IsErrorOccured) return;
                Response.Redirect("MeasurementDeviceList.aspx");
            }
            else
            {
                MeasurementDevice.MeasurementDeviceId = (short)Convert.ToInt32(hdnMeasurementDeviceId.Value);
                _presenterdevice.UpdateMeasurementDevice();
                if (IsErrorOccured) return;
                Response.Redirect("MeasurementDeviceList.aspx");
            }
        }

        #endregion

        #region Method

        private void BindCompanyDropDown()
        {
            _presenterdevice.CompanyList();
            if (!IsErrorOccured)
            {
                cmbCompanyName.DataSource = CompanyList;
                cmbCompanyName.DataBind();
                cmbCompanyName.Items.Insert(0, new ListItem(GetResourceString(_recSelectCompany), ""));//-- Select Company --
            }
        }

        private void BindBranchDropDown(int companyId)
        {
            this.CompanyId = companyId;
            _presenterdevice.BranchList();
            if (!IsErrorOccured)
            {
                cmbBranch.DataSource = BranchList;
                cmbBranch.DataBind();
                cmbBranch.Items.Insert(0, new ListItem(GetResourceString(_recSelectBranch), ""));//-- Select Branch --
            }
        }

        private void BindLicenseDropDown(int branchId)
        {
            this.BranchId = branchId;
            _presenterdevice.LicenseList();
            if (!IsErrorOccured)
            {
                cmbLicenseType.DataSource = LicenseList;
                cmbLicenseType.DataBind();
                cmbLicenseType.Items.Insert(0, new ListItem(GetResourceString(_recSelectLicenseType), ""));//-- Select License Type --
            }
            if (cmbLicenseType.Items.Count > 1)
            {
                cmbLicenseType.SelectedIndex = 1;
            }
        }

        private void BindInstrumentTypeDropDown()
        {
            _presenterdevice.InstrumentTypeList();
            if (!IsErrorOccured)
            {
                cmbInstrumentType.DataSource = InstrumentTypeList;
                cmbInstrumentType.DataBind();
                cmbInstrumentType.Items.Insert(0, new ListItem(GetResourceString(_recSelectInstrumentType), ""));//-- Select Instrument Type --
            }
        }

        private void LoadMeasurementDevice(int iMeasurementDeviceId)
        {
            MeasurementDevice = new MeasurementDevice
                                    {
                                        MeasurementDeviceId = (short)iMeasurementDeviceId
                                    };
            _presenterdevice.GetMeasurementDevice();
            if (!IsErrorOccured)
            {
                hdnMeasurementDeviceId.Value = Convert.ToString(iMeasurementDeviceId);
                cmbCompanyName.SelectedValue = Convert.ToString(MeasurementDevice.CompanyId);
                BindBranchDropDown(Convert.ToInt32(cmbCompanyName.SelectedValue));
                cmbBranch.SelectedValue = Convert.ToString(MeasurementDevice.BranchId);
                BindLicenseDropDown(Convert.ToInt32(cmbBranch.SelectedValue));
                cmbLicenseType.SelectedValue = Convert.ToString(MeasurementDevice.LicenseId);
                cmbInstrumentType.SelectedValue = Convert.ToString(MeasurementDevice.InstrumentType.InstrumentTypeId);
                txtSerialNumber.Text = MeasurementDevice.SerialNumber;
                chkIsActive.Checked = (MeasurementDevice.IsActive == true ? true : false);
                txtDescription.Text = MeasurementDevice.Description.ToString();
            }
        }

        #endregion

        #region IViewDevice Members
        public int TaskeroUserId { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public MeasurementDevice MeasurementDevice { get; set; }
        public List<MeasurementDevice> MeasurementDevices { get; set; }
        public Dictionary<int, string> BranchList { get; set; }
        public Dictionary<int, string> CompanyList { get; set; }
        public Dictionary<int, string> LicenseList { get; set; }
        public Dictionary<int, string> InstrumentTypeList { get; set; }
        public List<Device> Devices { get; set; }
        public int DeviceId { get; set; }
        public Device SelectedDevice { get; set; }
        #endregion
    }
}
