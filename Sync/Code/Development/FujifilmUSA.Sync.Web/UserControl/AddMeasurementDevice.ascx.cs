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
            SessionManager.CheckLogin();
            if (!IsPostBack)
            {
                BindCompanyDropDown();
                BindInstrumentTypeDropDown();
                if (Request.QueryString["MeasurementDeviceId"] == null) 
                {
                }
                else
                {
                    LoadMeasurementDevice(Convert.ToInt32(Request.QueryString["MeasurementDeviceId"]));
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
                cmbBranch.Items.Insert(0, new ListItem("-- Select --", "0"));

        }

        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbLicenseType.Items.Clear();
            if (cmbBranch.SelectedIndex != 0)
            {
                BindLicenseDropDown(int.Parse(cmbBranch.SelectedValue));
            }
            else
                cmbLicenseType.Items.Insert(0, new ListItem("-- Select --", "0"));

        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            MeasurementDevice = new MeasurementDevice();
            MeasurementDevice.LicenseId = Convert.ToInt32(cmbLicenseType.SelectedValue);
            MeasurementDevice.SerialNumber = txtSerialNumber.Text.Trim();
            MeasurementDevice.InstrumentTypeId = (short)(Convert.ToInt32(cmbInstrumentType.SelectedValue));
            if (chkIsActive.Checked == true)
                MeasurementDevice.IsActive = true;
            else
                MeasurementDevice.IsActive = false;
            
            if (hdnMeasurementDeviceId.Value == "0")
            {
                _presenterdevice.InsertMeasurementDevice();
                Response.Redirect("MeasurementDeviceList.aspx");
            }
            else
            {
                MeasurementDevice.MeasurementDeviceId =(short)Convert.ToInt32(hdnMeasurementDeviceId.Value);
                _presenterdevice.UpdateMeasurementDevice();
                Response.Redirect("MeasurementDeviceList.aspx");
            }
        }

        #endregion

        #region Method

        private void BindCompanyDropDown()
        {
            _presenterdevice.CompanyList();
            cmbCompanyName.DataSource = CompanyList;
            cmbCompanyName.DataBind();
            cmbCompanyName.Items.Insert(0, new ListItem("-- Select Company --", "0"));
        }
        
        private void BindBranchDropDown(int companyId)
        {
            this.CompanyId = companyId;
            _presenterdevice.BranchList();
            cmbBranch.DataSource = BranchList;
            cmbBranch.DataBind();
            cmbBranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
        }
        
        private void BindLicenseDropDown(int branchId)
        {
            this.BranchId = branchId;
            _presenterdevice.LicenseList();
            cmbLicenseType.DataSource = LicenseList;
            cmbLicenseType.DataBind();
            cmbLicenseType.Items.Insert(0, new ListItem("-- Select License Type --", "0"));
        }
        
        private void BindInstrumentTypeDropDown()
        {
            _presenterdevice.InstrumentTypeList();
            cmbInstrumentType.DataSource = InstrumentTypeList;
            cmbInstrumentType.DataBind();
            cmbInstrumentType.Items.Insert(0, new ListItem("-- Select Instrument Type --", "0"));
        }

        private void LoadMeasurementDevice(int iMeasurementDeviceId)
        {
            MeasurementDevice = new MeasurementDevice();
            MeasurementDevice.MeasurementDeviceId =(short)iMeasurementDeviceId;
            _presenterdevice.GetMeasurementDevice();
            hdnMeasurementDeviceId.Value =Convert.ToString(iMeasurementDeviceId);
            cmbCompanyName.SelectedValue = Convert.ToString(MeasurementDevice.CompanyId);
            BindBranchDropDown(Convert.ToInt32(cmbCompanyName.SelectedValue));
            cmbBranch.SelectedValue = Convert.ToString(MeasurementDevice.BranchId);
            BindLicenseDropDown(Convert.ToInt32(cmbBranch.SelectedValue));
            cmbLicenseType.SelectedValue = Convert.ToString(MeasurementDevice.LicenseId);
            cmbInstrumentType.SelectedValue = Convert.ToString(MeasurementDevice.InstrumentType.InstrumentTypeId);
            txtSerialNumber.Text = MeasurementDevice.SerialNumber;
            chkIsActive.Checked = (MeasurementDevice.IsActive==true? true:false);
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
        public List<Device> DeviceListById { get; set; }
        #endregion
    }
}