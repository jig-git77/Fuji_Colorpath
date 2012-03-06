using System;
using System.Collections.Generic;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class DeviceList : System.Web.UI.UserControl
    {
        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionManager.CheckLogin();
            if (!IsPostBack)
            {
                BindDeviceList();
            }
        }
        #endregion

        #region Control events
        protected void cmbCompany_SelectedIndexChanged1(object sender, EventArgs e)
        {
        }
        protected void cmbBranch_SelectedIndexChanged1(object sender, EventArgs e)
        {
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
        private void BindDeviceList()
        {
            rgDeviceList.DataSource = GetList();
            rgDeviceList.DataBind(); 
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
    
    }

    public class MockDeviceName
    {
        public string DeviceName { get; set; }
    }

}

