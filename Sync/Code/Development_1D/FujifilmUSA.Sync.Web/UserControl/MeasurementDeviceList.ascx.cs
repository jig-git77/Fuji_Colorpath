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

        #region Constructor
        public MeasurementDeviceList()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        } 
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterdevice = new PresenterDevice(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            hdnBaseServiceUrl.Value = Utilities.ReadConfigValue("BaseServiceURL");
            rgMeasurementDevice.PageSize = Common.Utilities.PageSize;
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
                    string parms = Service.Common.Encryption.CryptHelper.EncryptString(MeasurementDevice.MeasurementDeviceId.ToString(), FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey);
                    Response.Redirect("AddMeasurementDevice.aspx?MeasurementDeviceId=" + ContentEncoder.Encode(parms));
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

        protected void rgMeasurementDevice_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                MeasurementDevice device = (MeasurementDevice)e.Item.DataItem;

                CheckBox chkActive = (CheckBox)e.Item.FindControl("chkStatus");
                chkActive.Attributes.Add("OnClick", "checkboxClicked(event,'"+ device.MeasurementDeviceId + "');");
                //onclick="checkboxClicked(event, '<%# Convert.ToBoolean(Eval("MeasurementDeviceId")) %>')"
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
        public List<Device> Devices { get; set; }
        public int DeviceId { get; set; }
        public Device SelectedDevice { get; set; }
        #endregion
    }
}
