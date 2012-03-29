using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class DeviceList : BaseControl, IViewDevice
    {
        #region "Declaration"
        private PresenterDevice _presenterdevice;
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterdevice = new PresenterDevice(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
                BindCompanyDropDown();
                BindBranchDropDown();
                BindDeviceList();
            }
        }
        #endregion

        #region Control events
        protected void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBranchDropDown();
            BindDeviceList();
            rgDeviceList.DataBind();
        }
        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeviceList();
            rgDeviceList.DataBind();
        }
        protected void rgDeviceList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                SessionManager.Instance.CurrentDevice = new Device()
                                                            {
                                                                DeviceId = Convert.ToInt32(((GridDataItem)e.Item).GetDataKeyValue("DeviceId")),
                                                                DeviceName = ((GridDataItem)e.Item).GetDataKeyValue("DeviceName").ToString(),
                                                                DeviceFamily = new DeviceFamily()
                                                                                   {
                                                                                       DeviceFamilyId =
                                                                                           Convert.ToInt16(((GridDataItem)e.Item).GetDataKeyValue("DeviceFamily.DeviceFamilyId")),
                                                                                       DeviceFamilyDesc =
                                                                                           ((GridDataItem)e.Item).GetDataKeyValue("DeviceFamily.DeviceFamilyDesc").ToString()
                                                                                   }
                                                            };
                Response.Redirect("OperationSelection.aspx");
            }
        }
        protected void rgDeviceList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            BindDeviceList();
        }
        #endregion

        #region Method
        private void BindCompanyDropDown()
        {
            _presenterdevice.CompanyList();
            if (!IsErrorOccured)
            {
                cmbCompany.DataSource = CompanyList;
                cmbCompany.DataBind();
            }
            cmbCompany.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        private void BindBranchDropDown()
        {
            cmbBranch.Items.Clear();
            if (cmbCompany.SelectedIndex != 0)
            {
                this.CompanyId = Convert.ToInt32(cmbCompany.SelectedValue);
                _presenterdevice.BranchList();
                if (!IsErrorOccured)
                {
                    cmbBranch.DataSource = BranchList;
                    cmbBranch.DataBind();
                }
            }
            cmbBranch.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        private void BindDeviceList()
        {
            if (cmbBranch.SelectedIndex > 0 && cmbCompany.SelectedIndex > 0)
            {
                this.CompanyId = 0;
                this.BranchId = 0;

                this.CompanyId = int.Parse(cmbCompany.SelectedValue);
                this.BranchId = int.Parse(cmbBranch.SelectedValue);

                _presenterdevice.DeviceListById();
                if (!IsErrorOccured)
                {
                    rgDeviceList.DataSource = DeviceListById;
                }

            }
            else
                rgDeviceList.DataSource = new List<Device>();
            
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