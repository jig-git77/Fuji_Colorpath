using System;
using System.Linq;
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
            rgDeviceList.PageSize = Common.Utilities.PageSize;
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
                BindCompanyDropDown();
            }
        }
        #endregion

        #region Control events
        protected void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBranch.Items.Clear();
            BindBranchDropDown();
            CompanyId = string.IsNullOrEmpty(cmbCompany.SelectedValue) ? 0 : Convert.ToInt32(cmbCompany.SelectedValue);
            BranchId = string.IsNullOrEmpty(cmbBranch.SelectedValue) ? 0 : Convert.ToInt32(cmbBranch.SelectedValue);
        }
        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            CompanyId = string.IsNullOrEmpty(cmbCompany.SelectedValue) ? 0 : Convert.ToInt32(cmbCompany.SelectedValue);
            BranchId = string.IsNullOrEmpty(cmbBranch.SelectedValue) ? 0 : Convert.ToInt32(cmbBranch.SelectedValue);
            rgDeviceList.Rebind();
        }
        protected void rgDeviceList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Filter":
                    CompanyId = SessionManager.Instance.CurrentUser.UserCompany.CompanyId > 0
                            ? SessionManager.Instance.CurrentUser.UserCompany.CompanyId
                            : Convert.ToInt32(cmbCompany.SelectedValue);
                    BranchId = Convert.ToInt32(cmbBranch.SelectedValue);
                    break;
                case "Select":
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
                    Response.RedirectPermanent("OperationSelection.aspx");
                    break;
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
            cmbCompany_SelectedIndexChanged(null, null);
        }
        private void BindBranchDropDown()
        {
            this.CompanyId = string.IsNullOrEmpty(cmbCompany.SelectedValue)
                                 ? 0
                                 : Convert.ToInt32(cmbCompany.SelectedValue);
            _presenterdevice.BranchList();
            if (!IsErrorOccured)
            {
                cmbBranch.DataSource = BranchList;
                cmbBranch.DataBind();
            }
            cmbBranch.Items.Insert(0, new ListItem("-- Select --", "0"));
            cmbBranch_SelectedIndexChanged(null, null);
        }
        private void BindDeviceList()
        {
            _presenterdevice.DeviceListById();
            if (!IsErrorOccured)
            {
                rgDeviceList.DataSource = DeviceListById;

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
        public List<Device> DeviceListById { get; set; }
        #endregion
    }
}
