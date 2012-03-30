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
    public partial class MeasurementDeviceList : BaseControl, IViewDevice
    {
        #region Declaration
        private PresenterDevice _presenterdevice;
        public string MeasurementDeviceList_DeleteMeasurementDevice_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("MeasurementDeviceList_DeleteMeasurementDevice_Confirmation");
            }
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterdevice = new PresenterDevice(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindMeasurementDeviceList();
            }
        }
        #endregion

        #region Methods

        public void BindMeasurementDeviceList()
        {
            _presenterdevice.GetMeasurementDevices();
            if (!IsErrorOccured)
            {
                rgMeasurementDevice.DataSource = MeasurementDevices;
                rgMeasurementDevice.DataBind();
            }
        }

        #endregion

        #region Control Events
        protected void rgMeasurementDevice_ItemCommand(object sender, GridCommandEventArgs e)
        {
            MeasurementDevice = new Entity.MeasurementDevice();
            switch (e.CommandName)
            {
                case "Filter":
                    break;
                case "ClearFilter":
                    foreach (GridColumn column in rgMeasurementDevice.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgMeasurementDevice.MasterTableView.FilterExpression = string.Empty;
                    rgMeasurementDevice.MasterTableView.Rebind();
                    break;
                case "Delete":
                    MeasurementDevice.MeasurementDeviceId = (short)Convert.ToInt32(e.CommandArgument);
                    _presenterdevice.DeleteMeasurementDevice();
                    break;
                case "Edit":
                    MeasurementDevice.MeasurementDeviceId = (short)Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("AddMeasurementDevice.aspx?MeasurementDeviceId=" + MeasurementDevice.MeasurementDeviceId);
                    break;
            }
        }
        protected void rgMeasurementDevice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _presenterdevice.GetMeasurementDevices();
            if (!IsErrorOccured)
            {
                rgMeasurementDevice.DataSource = MeasurementDevices;
            }
        }

        protected void likAdd_Click(object sender, EventArgs e)
        {
            //SessionManager.Instance.CurrentMeasurementDevice = null;
            Response.RedirectPermanent("AddMeasurementDevice.aspx");
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
