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
            SaveMeasurementDevice();
        }
        public void SaveMeasurementDevice()
        {

            MeasurementDevice objMeasurementDevice = new MeasurementDevice();

            objMeasurementDevice.CPDongleId = Convert.ToInt32(cmbDongleSerialNo.SelectedItem.Value);
            objMeasurementDevice.MeasurementDeviceTypeDetail = new MeasurementDeviceType() {  DeviceTypeId = Convert.ToInt32(1) };
            objMeasurementDevice.SerialNumber = txtSerialNo.Text;
            objMeasurementDevice.IsActive = chkIsActive.Checked;
            this.SaveSyncMeasurementDevice = objMeasurementDevice;
            _presentermeasurementdevice.InsertSyncMeasurementDevice();
        }


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