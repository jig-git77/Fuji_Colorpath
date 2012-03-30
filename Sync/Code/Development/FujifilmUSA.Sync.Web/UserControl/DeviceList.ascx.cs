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
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Home;
                if (SessionManager.Instance.CurrentUser.UserCompany.CompanyId > 0)
                {
                    trCompany.Visible = false;
                    if (SessionManager.Instance.CurrentUser.UserBranches.Count > 0)
                    {
                        if (SessionManager.Instance.CurrentUser.UserBranches.Count == 1)
                        {
                            trBranch.Visible = false;
                            CompanyId = SessionManager.Instance.CurrentUser.UserCompany.CompanyId;
                            BranchId = SessionManager.Instance.CurrentUser.UserBranches.First().BranchId;
                        }
                        else
                        {
                            BindBranchDropDown(
                                SessionManager.Instance.CurrentUser.UserCompany.CompanyId,
                                SessionManager.Instance.CurrentUser.UserBranches);
                        }
                    }
                    else
                    {
                        BindBranchDropDown(SessionManager.Instance.CurrentUser.UserCompany.CompanyId);
                    }
                }
                else
                {
                    BindCompanyDropDown();
                }
            }
        }
        #endregion

        #region Control events
        protected void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBranch.Items.Clear();
            //if (cmbCompany.SelectedIndex != 0)
            {
                BindBranchDropDown(int.Parse(cmbCompany.SelectedValue));
                //if (cmbCompany.SelectedIndex > 0 && cmbBranch.SelectedIndex > 0)
                {
                    CompanyId = Convert.ToInt32(cmbCompany.SelectedValue);
                    BranchId = Convert.ToInt32(cmbBranch.SelectedValue);
                    rgDeviceList.Rebind();
                    //BindDeviceList();
                }
            }
            if (cmbCompany.SelectedIndex == 0)
            {
                cmbBranch.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            //rgDeviceList.Visible = cmbCompany.SelectedIndex != 0;
        }
        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbCompany.SelectedIndex > 0 && cmbBranch.SelectedIndex > 0)
            {

                CompanyId = SessionManager.Instance.CurrentUser.UserCompany.CompanyId > 0
                                ? SessionManager.Instance.CurrentUser.UserCompany.CompanyId
                                : Convert.ToInt32(cmbCompany.SelectedValue);
                BranchId = Convert.ToInt32(cmbBranch.SelectedValue);
                rgDeviceList.Rebind();
                //BindDeviceList();
            }
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
        private void BindBranchDropDown(int companyId, IEnumerable<UserBranch> branches = null)
        {

            this.CompanyId = companyId;
            _presenterdevice.BranchList();
            if (branches != null)
            {
                BranchList = BranchList.Where(x => branches.Where(y => y.BranchId == x.Key).Count() > 0).ToDictionary(x => x.Key, x => x.Value);
            }
            if (!IsErrorOccured)
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
