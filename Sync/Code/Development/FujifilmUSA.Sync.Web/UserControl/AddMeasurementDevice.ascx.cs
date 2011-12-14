using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddMeasurementDevice : System.Web.UI.UserControl, IViewSyncDevice,IViewMeasurementDevice
    {
        #region Declaration
        private PresenterSyncDevice _presentersyncdevice;
        private PresenterMeasurementDevice _presentermeasurementdevice;       
        #endregion

        #region
        #endregion
        protected override void OnInit(EventArgs e)
        {
            _presentersyncdevice = new PresenterSyncDevice(this);
            _presentermeasurementdevice = new PresenterMeasurementDevice(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillCompanyList();
                FillDeviceList();

                if (SessionManager.Instance.CurrentMeasurementDevice != null)
                {
                    LoadMeasurementDevice();
                    ToggleInputControlState(false);
                }
                else
                    ToggleInputControlState(true);
            }
        }
        public void FillCompanyList()
        {
            _presentersyncdevice.GetCompanyList();
            cmbCompanyName.DataSource = GetCompanyList;
            cmbCompanyName.DataBind();
            cmbCompanyName.Items.Insert(0,new RadComboBoxItem("--Select Company--",""));
        }
        public void FillBranchList()
        {
            if (!string.IsNullOrEmpty(cmbCompanyName.SelectedItem.Value))
            {
                this.CompanyId = Convert.ToInt32(cmbCompanyName.SelectedItem.Value);
            }
                _presentersyncdevice.GetBranchList();
                cmbBranch.DataSource = GetBranchList;
                cmbBranch.DataBind();
                cmbBranch.Items.Insert(0, new RadComboBoxItem("--Select Branch--", ""));
        }
        public void FillDongleList()
        {
            if (!string.IsNullOrEmpty(cmbBranch.SelectedItem.Value))
            {
                this.BranchId = Convert.ToInt32(cmbBranch.SelectedItem.Value);
            }
            _presentermeasurementdevice.GetDongleList();
            cmbDongleSerialNo.DataSource = DongleList;
            cmbDongleSerialNo.DataBind();
            cmbDongleSerialNo.Items.Insert(0, new RadComboBoxItem("--Select Dongle--", ""));
        }
        public void FillDeviceList()
        {
            _presentermeasurementdevice.GetMeasurementDeviceTypeList();
            cmbDeviceType.DataSource = MeasurementDeviceTypeList;
            cmbDeviceType.DataBind();            
        }
        protected void cmbCompanyName_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbCompanyName.SelectedItem.Value))
            {
                FillBranchList();
            }
        }
        protected void cmbBranch_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbBranch.SelectedItem.Value))
            {
                FillDongleList();
            }
        }

        protected void lbtnProjectSave_Click(object sender, EventArgs e)
        {
            if (CheckValidation())
            {
                SaveMeasurementDevice();
            }
        }
        public void SaveMeasurementDevice()
        {
            MeasurementDevice objMeasurementDevice = new MeasurementDevice();

            objMeasurementDevice.CPDongleId = Convert.ToInt32(cmbDongleSerialNo.SelectedItem.Value);
            objMeasurementDevice.MeasurementDeviceTypeDetail = new MeasurementDeviceType() { DeviceTypeId = Convert.ToInt32(cmbDeviceType.SelectedItem.Value) };
            objMeasurementDevice.SerialNumber = txtSerialNo.Text;
            objMeasurementDevice.IsActive = chkIsActive.Checked;

            if (SessionManager.Instance.CurrentMeasurementDevice != null)
            {                
                objMeasurementDevice.MeasurementDeviceId =Convert.ToInt32(SessionManager.Instance.CurrentMeasurementDevice.MeasurementDeviceId);
                this.SaveSyncMeasurementDevice = objMeasurementDevice;              
                _presentermeasurementdevice.UpdateSyncMeasurementDevice();
                SessionManager.Instance.CurrentMeasurementDevice = null;
            }
            else
            {
                this.SaveSyncMeasurementDevice = objMeasurementDevice;              
                _presentermeasurementdevice.InsertSyncMeasurementDevice();
            }
            Response.Redirect("MeasurementDeviceList.aspx");
        }
        public void LoadMeasurementDevice()
        {
            SyncMeasurementDevice = SessionManager.Instance.CurrentMeasurementDevice;
            //Step - 1 Select CompanyId
            cmbCompanyName.SelectedValue = SyncMeasurementDevice.CompanyId.ToString();          
            //Step - 2 FillBranchList
            FillBranchList(); //Selected companyId is used to fill the branch list.
            //Step - 3 Select BranchId
            cmbBranch.SelectedValue = SyncMeasurementDevice.BranchId.ToString();            
            //Step - 4 FillDongleList
            FillDongleList(); //Selected brach Id is used to fill the dongle list.            
            cmbDongleSerialNo.SelectedValue = SyncMeasurementDevice.CPDongleId.ToString();           
            txtSerialNo.Text = SyncMeasurementDevice.SerialNumber;            
            cmbDeviceType.SelectedValue = SyncMeasurementDevice.MeasurementDeviceTypeDetail.DeviceTypeId.ToString();            
            chkIsActive.Checked = SyncMeasurementDevice.IsActive;
        }
        public void ToggleInputControlState(bool value)
        {
            cmbCompanyName.Enabled = value;
            cmbBranch.Enabled = value;
            cmbDongleSerialNo.Enabled =value;
            txtSerialNo.Enabled = value;
            cmbDeviceType.Enabled = value;
        }

        public bool CheckValidation()
        {
            bool check = true;

            if (string.IsNullOrEmpty(cmbCompanyName.SelectedValue))
            {
                ucStatusbar1.DisplayMessage(ResourceManager.GetResourceMessage("AddMeasurementDevice_CompanyName_Required"), false);
                check = false;
            }
            else if (string.IsNullOrEmpty(cmbBranch.SelectedValue))
            {
                ucStatusbar1.DisplayMessage(ResourceManager.GetResourceMessage("AddMeasurementDevice_BranchName_Required"), false);
                check = false;
            }
            else if (string.IsNullOrEmpty(cmbDongleSerialNo.SelectedValue))
            {
                ucStatusbar1.DisplayMessage(ResourceManager.GetResourceMessage("AddMeasurementDevice_DongleSerialNo_Required"), false);
                check = false;
            }
            else if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
            {
                ucStatusbar1.DisplayMessage(ResourceManager.GetResourceMessage("AddMeasurementDevice_SerialNo_Required"), false);
                check = false;
            }
            else if (IsDuplicateDeviceSerialNumber(txtSerialNo.Text.Trim()))
            {
                ucStatusbar1.DisplayMessage(ResourceManager.GetResourceMessage("Duplicate_MeasurementDevice_SerialNo"), false);
                check = false;
            }
            

            return check;
        }

        #region Duplicate Device Serial Number
        /// <summary>
        /// Check whether device with duplicate serial number is found or not?
        /// Current condition checks on following criteria
        /// 1. Device Serial No. - Inputed in textbox
        /// </summary>
        /// <param name="serialNo"></param>
        /// <returns></returns>
        private bool IsDuplicateDeviceSerialNumber(string serialNo)
        {
            this._presentermeasurementdevice.GetSyncMeasurementDeviceList();
            var measurementDevice = SyncMeasurementDeviceList.Where(d => d.SerialNumber.Trim().Equals(serialNo, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (measurementDevice == null)
                return false;
            else
                return true;
        }
        #endregion

        #region IViewSyncDevice Member
        public  int TaskeroUserId { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public Dictionary<int, string> GetBranchList { get; set; }
        public Dictionary<int, string> GetCompanyList { get; set; }
        public List<SyncDevice> GetSyncDeviceListByUserId { get; set; }
        #endregion

        #region IViewMeasurementDevice Member
        public MeasurementDevice SyncMeasurementDevice { get; set; }
        public List<MeasurementDevice> SyncMeasurementDeviceList { get; set; }
        public int SyncMeasurementDeviceResult { get; set; }
        public MeasurementDevice SaveSyncMeasurementDevice { get; set; }
        public int SyncMeasurementDeviceId { get; set; }
        public Dictionary<int, string> DongleList { get; set; }
        public Dictionary<int, string> MeasurementDeviceTypeList { get; set; }
        #endregion
    }
    
}