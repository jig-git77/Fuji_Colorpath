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
    public partial class DeviceList : System.Web.UI.UserControl, IViewDevice
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
            SessionManager.CheckLogin();
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.Home;
                BindCompanyDropDown();
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
                var deviceId = Convert.ToInt32(e.CommandArgument);
                SessionManager.Instance.CurrentDevice = DeviceListById.Where(x => x.DeviceId == deviceId).SingleOrDefault();
                Response.Redirect("OperationSelection.aspx");
            }
        }
        protected void rgDeviceList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            BindDeviceList();
            //rgDeviceList.DataSource = DeviceListById;
        }
        #endregion

        #region Method
        private void BindCompanyDropDown()
        {
            _presenterdevice.CompanyList();
            cmbCompany.DataSource = CompanyList;
            cmbCompany.DataBind();
            cmbCompany.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        private void BindBranchDropDown(int companyId)
        {
            this.CompanyId = companyId;
            _presenterdevice.BranchList();
            cmbBranch.DataSource = BranchList;
            cmbBranch.DataBind();
            cmbBranch.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        #endregion

        #region Method
        private void BindDeviceList()
        {
            if (cmbBranch.SelectedIndex > 0 && cmbCompany.SelectedIndex > 0)
            {
                this.CompanyId = 0;
                this.BranchId = 0;

                this.CompanyId = int.Parse(cmbCompany.SelectedValue);
                this.BranchId = int.Parse(cmbBranch.SelectedValue);

                _presenterdevice.DeviceListById();
                rgDeviceList.DataSource = DeviceListById;
            }
           
        }

       

        #endregion

        #region IViewDevice Members
        public int TaskeroUserId { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public Dictionary<int, string> BranchList { get; set; }
        public Dictionary<int, string> CompanyList { get; set; }
        public List<Device> DeviceListById { get; set; }
        #endregion

    }

    public class MockDeviceName
    {
        //public string DeviceName { get; set; }
    }

}

