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
        private List<GridFilter> gridFilters;
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
            gridFilters = new List<GridFilter>();
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
                ApplyFiltersToGrid();                
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
            Session["MeasurementDeviceGridFilters"] = rgMeasurementDevice.MasterTableView.FilterExpression;
            SetGridFilters();

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
                chkActive.InputAttributes.Add("data-item", device.MeasurementDeviceId.ToString());

                ImageButton btnEditPopup = (ImageButton)e.Item.FindControl("btnEditPopup");
                string parms = Service.Common.Encryption.CryptHelper.EncryptString(device.MeasurementDeviceId.ToString(), FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey);
                btnEditPopup.Attributes.Add("onclick", "ShowEditMeasurementDeviceWindow('" + ContentEncoder.Encode(parms) + "')");
            }
        }

        protected void likAdd_Click(object sender, EventArgs e)
        {
            //SessionManager.Instance.CurrentMeasurementDevice = null;
            Response.RedirectPermanent("AddMeasurementDevice.aspx");
        }

        public void SetGridFilters()
        {
            string[] columnArray = { "CompanyName", "BranchName", "SerialNumber", "InstrumentType", "Description" };
            if (Session["CurrentGridFilters"] != null)
                Session["CurrentGridFilters"] = null;

            foreach (var columnName in columnArray)
            {
                GridColumn column = rgMeasurementDevice.MasterTableView.GetColumnSafe(columnName);
                
                if (column.CurrentFilterValue != "")
                {
                    gridFilters.Add(new GridFilter()
                    {
                        ColumnName = columnName,
                        FilterFunction = column.CurrentFilterFunction,
                        FilterValue = column.CurrentFilterValue
                    });
                }
            }

            Session["CurrentGridFilters"] = gridFilters;
        }

        public void ApplyFiltersToGrid()
        {
            if (Session["MeasurementDeviceGridFilters"] != null && Session["CurrentGridFilters"] != null)
            {
                var filterList = (List<GridFilter>)Session["CurrentGridFilters"];
                foreach (var filter in filterList)
                {
                    GridColumn column = rgMeasurementDevice.MasterTableView.GetColumnSafe(filter.ColumnName);
                    column.CurrentFilterFunction = filter.FilterFunction;
                    column.CurrentFilterValue = filter.FilterValue;
                }
                rgMeasurementDevice.MasterTableView.FilterExpression = Convert.ToString(Session["MeasurementDeviceGridFilters"]);
                rgMeasurementDevice.Rebind();
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
        public List<Device> Devices { get; set; }
        public int DeviceId { get; set; }
        public Device SelectedDevice { get; set; }
        #endregion
    }
    public class GridFilter
    {
        public string ColumnName { get; set; }
        public GridKnownFunction FilterFunction { get; set; }
        public string FilterValue { get; set; }
    }
}
