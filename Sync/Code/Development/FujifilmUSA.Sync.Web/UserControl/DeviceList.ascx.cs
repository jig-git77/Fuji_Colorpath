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

        protected void rlvDeviceList_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectDevice")
            {
                RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
                
                if (item != null) SessionManager.Instance.CurrentSyncDevice = item.DataItem as SyncDevice;
                //Response.Redirect("ProjectList.aspx");
            }
        }

      
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SessionManager.CheckLogin();

                if (SessionManager.Instance.CurrentUser.RoleId == 1)
                {
                    trFilter.Visible = true;
                    BindCompanyDropDown();
                    cmbBranch.Items.Insert(0, new RadComboBoxItem("", "-- Select --"));
                    
                }
                else
                {
                    trFilter.Visible = false;
                    BindDeviceList();
                }
                
            }
        }
        protected void cmbCompany_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbCompany.SelectedIndex != 0)
            {
                BindBranchDropDown(int.Parse(cmbCompany.SelectedValue));
            }
        }
        protected void cmbBranch_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbBranch.SelectedIndex != 0)
            {
                BindDeviceList();
            }
        }

        protected void rlvDeviceList_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            if (cmbBranch.SelectedIndex != 0)
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
                rlvDeviceList.DataSource = GetSyncDeviceListByUserId;
            }

        }

      

        #endregion

        #region Method
        private void BindCompanyDropDown()
        {
            _presentersyncdevice.GetCompanyList();
            cmbCompany.DataSource = GetCompanyList;
            cmbCompany.DataBind();
            cmbCompany.Items.Insert(0, new RadComboBoxItem("", "-- Select --" ));
        }
        private void BindBranchDropDown(int companyId)
        {
            this.CompanyId = companyId;
            _presentersyncdevice.GetBranchList();
            cmbBranch.DataSource = GetBranchList;
            cmbBranch.DataBind();
            cmbBranch.Items.Insert(0, new RadComboBoxItem("", "-- Select --"));
        }
        private void BindDeviceList()
        {
            this.TaskeroUserId = SessionManager.Instance.CurrentUser.TaskeroUserId;
            this.RoleId = SessionManager.Instance.CurrentUser.RoleId;
            this.CompanyId = 0;
            this.BranchId = 0;
            if (SessionManager.Instance.CurrentUser.RoleId == 1)
            {
               
                this.CompanyId = int.Parse(cmbCompany.SelectedValue);
                this.BranchId= int.Parse(cmbBranch.SelectedValue);
            }
            _presentersyncdevice.GetSyncDeviceListByUserId();
            rlvDeviceList.DataSource = GetSyncDeviceListByUserId;
            rlvDeviceList.DataBind();
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