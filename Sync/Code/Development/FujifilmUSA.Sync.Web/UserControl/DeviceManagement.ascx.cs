using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class DeviceManagement : BaseControl, IViewDeviceManagement
    {
        private PresenterDeviceManagement _presenterDeviceManagement;

        #region Declaration
        public string DeviceManagement_DeleteDevice_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("DeviceManagement_DeleteDevice_Confirmation");
            }
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterDeviceManagement = new PresenterDeviceManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
              BindBranches();
            }
        }
        #endregion

        #region Methods
       
        private void GetDevices()
        {
            DeviceList = new List<Device>();
            _presenterDeviceManagement.GetDevices();
        }
        private void BindBranches()
        {
            GetDevices();
            rgDevice.DataSource = DeviceList;
            rgDevice.DataBind();
        }
        #endregion

        #region Control Events
        protected void likAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddDeviceManagement.aspx");
        }
        protected void lbtnAddDeviceFamily_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddDeviceFamily.aspx");
        }
        protected void rgDevice_ItemCommand(object sender, GridCommandEventArgs e)
        {
            Device = new Entity.Device();
            switch (e.CommandName)
            {
                case "Filter":
                    break;
                case "ClearFilter":
                    foreach (GridColumn column in rgDevice.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgDevice.MasterTableView.FilterExpression = string.Empty;
                    rgDevice.MasterTableView.Rebind();
                    break;
                case "Delete":
                    Device.DeviceId = Convert.ToInt32(e.CommandArgument);
                    _presenterDeviceManagement.DeleteDevice();
                    break;
                case "Edit":
                    Device.DeviceId = Convert.ToInt32(e.CommandArgument);
                    GridDataItem item = (GridDataItem)e.Item;
                    Response.Redirect("AddDeviceManagement.aspx?DeviceID=" + Device.DeviceId);
                    break;
            }
        }
        protected void rgDevice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetDevices();
            rgDevice.DataSource = DeviceList;
        }
      

         #endregion

        #region Implement Interface
        public Device Device { get; set; }
        public List<Device> DeviceList { get; set; }
        #endregion
    }
}