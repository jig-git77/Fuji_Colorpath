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
                if (SessionManager.Instance.CurrentUser.RoleId == 1)
                {
                    trFilter.Visible = true;
                    BindCompanyDropDown();
                    cmbBranch.Items.Insert(0, new RadComboBoxItem("-- Select --", "0"));

                }
                else
                {
                    trFilter.Visible = false;
                }

            }
        }


        protected void rlvDeviceList_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item is RadListViewDataItem)
            {
                var item = (RadListViewDataItem)e.Item;
                var lbtnSelectDevice = (LinkButton)e.Item.FindControl("lbtnSelectDevice");
                var lbtnDestination = (LinkButton)e.Item.FindControl("lbtnDestination");
                lbtnSelectDevice.Attributes.Add("SyncDeviceId", ((SyncDevice)item.DataItem).SyncDeviceId.ToString());
                lbtnDestination.Attributes.Add("SyncDeviceId", ((SyncDevice)item.DataItem).SyncDeviceId.ToString());                
            }
        }
        protected void lbtnSelectDevice_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentSyncDevice =
                (SessionManager.Instance.DictionaryList.Where(x => x.Key == int.Parse(((LinkButton) (sender)).Attributes["SyncDeviceId"].ToString())).
                    Select(x => new SyncDevice()
                                    {
                                        SyncDeviceId = x.Key,
                                        DeviceName = x.Value.ToString()
                                    }
                    ).FirstOrDefault());
            Response.Redirect("ProjectList.aspx");
        }
        protected void lbtnDestination_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentSyncDevice =
               (SessionManager.Instance.DictionaryList.Where(x => x.Key == int.Parse(((LinkButton)(sender)).Attributes["SyncDeviceId"].ToString())).
                   Select(x => new SyncDevice()
                   {
                       SyncDeviceId = x.Key,
                       DeviceName = x.Value.ToString()
                   }
                   ).FirstOrDefault());
            Response.Redirect("DestinationProfileList.aspx");
        }
        protected void rlvDeviceList_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            BindDeviceList();
        }
        protected void cmbCompany_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbCompany.SelectedIndex != 0)
            {
                BindBranchDropDown(int.Parse(cmbCompany.SelectedValue));
                BindDeviceList();
                rlvDeviceList.DataBind();
            }
        }
        protected void cmbBranch_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbBranch.SelectedIndex != 0)
            {
                BindDeviceList();
                rlvDeviceList.DataBind();
            }
        }
        #endregion

        #region Method
        private void BindCompanyDropDown()
        {
            _presentersyncdevice.GetCompanyList();
            cmbCompany.DataSource = GetCompanyList;
            cmbCompany.DataBind();
            cmbCompany.Items.Insert(0, new RadComboBoxItem("-- Select --","0" ));
        }
        private void BindBranchDropDown(int companyId)
        {
            cmbBranch.Items.Clear();
            this.CompanyId = companyId;
            _presentersyncdevice.GetBranchList();
            cmbBranch.DataSource = GetBranchList;
            cmbBranch.DataBind();
            cmbBranch.Items.Insert(0, new RadComboBoxItem("-- Select --","0"));
        }
        private void BindDeviceList()
        {
            if (cmbCompany.SelectedIndex > 0)
            {
                this.TaskeroUserId = SessionManager.Instance.CurrentUser.TaskeroUserId;
                this.RoleId = SessionManager.Instance.CurrentUser.RoleId;
                this.CompanyId = 0;
                this.BranchId = 0;
                if (SessionManager.Instance.CurrentUser.RoleId == 1)
                {

                    this.CompanyId = int.Parse(cmbCompany.SelectedValue);
                    this.BranchId = int.Parse(cmbBranch.SelectedValue);
                }
                _presentersyncdevice.GetSyncDeviceListByUserId();
                rlvDeviceList.DataSource = GetSyncDeviceListByUserId;

                SessionManager.Instance.DictionaryList = (GetSyncDeviceListByUserId.Select(
                    deviceList => new
                    {
                        Value = deviceList.SyncDeviceId,
                        Text = deviceList.DeviceName
                    })).ToDictionary(deviceList => deviceList.Value, deviceList => deviceList.Text);
            }
        }

        #endregion

        #region IViewSyncDevice Members
        public int TaskeroUserId { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public Dictionary<int, string> GetBranchList { get; set; }
        public Dictionary<int, string> GetCompanyList { get; set; }
        public List<SyncDevice> GetSyncDeviceListByUserId { get; set; }
        #endregion

     
    }
}
