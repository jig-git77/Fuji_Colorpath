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
    public partial class DeviceList : System.Web.UI.UserControl, IViewSyncDevice
    {
        #region "Declaration"
        private PresenterSyncDevice _presentersyncdevice;
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presentersyncdevice = new PresenterSyncDevice(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
            if (!IsPostBack)
            {
                BindCompanyDropDown();
                //BindDeviceList();
            }
        }
        #endregion

        #region Control events
        protected void cmbCompany_SelectedIndexChanged1(object sender, EventArgs e)
        {
            cmbBranch.Items.Clear();
            if (cmbCompany.SelectedIndex != 0)
            {
                BindBranchDropDown(int.Parse(cmbCompany.SelectedValue));
                BindDeviceList();
                rgDeviceList.DataBind();
            }
            else
                cmbBranch.Items.Insert(0, new ListItem("-- Select --", "0"));
            rgDeviceList.Visible = cmbCompany.SelectedIndex != 0;

        }
        protected void cmbBranch_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (cmbBranch.SelectedIndex != 0)
            {
                BindDeviceList();
                rgDeviceList.DataBind();
            }
        }
        protected void rgDeviceList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string arg = e.CommandArgument as string;
                Response.Redirect("OperationSelection.aspx");
            }
        }
        protected void rgDeviceList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgDeviceList.DataSource = GetList();
        }
        #endregion

        #region Method
        private void BindCompanyDropDown()
        {
            _presentersyncdevice.GetCompanyList();
            cmbCompany.DataSource = GetCompanyList;
            cmbCompany.DataBind();
            cmbCompany.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        private void BindBranchDropDown(int companyId)
        {
            this.CompanyId = companyId;
            _presentersyncdevice.GetBranchList();
            cmbBranch.DataSource = GetBranchList;
            cmbBranch.DataBind();
            cmbBranch.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        #endregion

        #region Error Handling Method
        //public void ErrorOccured(object sender, Business.Interface.ErrorArgs e)
        //{
        //    ucStatusbar1.DisplayMessage(e.ErrorMessage, false);
        //}
        #endregion

        #region Method
        private void BindDeviceList()
        {
            if (cmbCompany.SelectedIndex > 0)
            {
                //this.TaskeroUserId = SessionManager.Instance.CurrentUser.UserId;
                //this.RoleId = SessionManager.Instance.CurrentUser.RoleId;
                this.CompanyId = 0;
                this.BranchId = 0;
                //if (SessionManager.Instance.CurrentUser.RoleId == 1)
                //{

                //    this.CompanyId = int.Parse(cmbCompany.SelectedValue);
                //    this.BranchId = int.Parse(cmbBranch.SelectedValue);
                //}

                this.CompanyId = int.Parse(cmbCompany.SelectedValue);
                this.BranchId = int.Parse(cmbBranch.SelectedValue);

                _presentersyncdevice.GetSyncDeviceListByUserId();
                rgDeviceList.DataSource = GetSyncDeviceListByUserId;

                //SessionManager.Instance.DictionaryList = (GetSyncDeviceListByUserId.Select(
                //    deviceList => new
                //    {
                //        Value = deviceList.SyncDeviceId,
                //        Text = deviceList.DeviceName
                //    })).ToDictionary(deviceList => deviceList.Value, deviceList => deviceList.Text);

            }
        }

        private List<MockDeviceName> GetList()
        {
            List<MockDeviceName> deviceList = new List<MockDeviceName>();
            deviceList.Add(new MockDeviceName() { DeviceName = "Komori LS 40 1" });
            deviceList.Add(new MockDeviceName() { DeviceName = "Komori XL 40 2" });
            deviceList.Add(new MockDeviceName() { DeviceName = "Komori XL 40 3" });
            deviceList.Add(new MockDeviceName() { DeviceName = "Komori XL 40 4" });
            deviceList.Add(new MockDeviceName() { DeviceName = "Komori XL 40 5" });
            deviceList.Add(new MockDeviceName() { DeviceName = "Komori XL 40 6" });
            deviceList.Add(new MockDeviceName() { DeviceName = "Komori XL 40 7" });
            deviceList.Add(new MockDeviceName() { DeviceName = "Heidelberg SM40 1" });
            deviceList.Add(new MockDeviceName() { DeviceName = "Heidelberg SM40 2" });
            deviceList.Add(new MockDeviceName() { DeviceName = "Heidelberg SM40 3" });
            deviceList.Add(new MockDeviceName() { DeviceName = "Heidelberg SM40 4" });
            deviceList.Add(new MockDeviceName() { DeviceName = "Heidelberg SM40 5" });
            return deviceList;
        }

        #endregion


        #region IViewSyncDevice Members
        public int TaskeroUserId { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public Dictionary<int, string> GetBranchList { get; set; }
        public Dictionary<int, string> GetCompanyList { get; set; }
        public List<Device> GetSyncDeviceListByUserId { get; set; }
        #endregion


       
    }

    public class MockDeviceName
    {
        public string DeviceName { get; set; }
    }

}

