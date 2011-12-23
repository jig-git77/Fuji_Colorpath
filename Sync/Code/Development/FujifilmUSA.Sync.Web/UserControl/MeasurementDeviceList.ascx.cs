using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class MeasurementDeviceList : System.Web.UI.UserControl, IViewMeasurementDevice
    {
        #region Declaration
        private PresenterMeasurementDevice _presentermeasurementdevice;
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
            _presentermeasurementdevice = new PresenterMeasurementDevice(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
            if (!Page.IsPostBack)
            {
                BindMeasurementDeviceList();
            }
        }
        #endregion

        #region Methods
        public object DisplayIsActiveCustomValue(object value)
        {
            if (Convert.ToBoolean(value))
                return ResourceManager.GetResourceMessage("MeasurementDeviceList_IsActive_True");
            else
                return ResourceManager.GetResourceMessage("MeasurementDeviceList_IsActive_False");
        }

        public void BindMeasurementDeviceList()
        {
            _presentermeasurementdevice.GetSyncMeasurementDeviceList();
            grdMeasurementDevice.DataSource = SyncMeasurementDeviceList;
            grdMeasurementDevice.DataBind();
        }
        #endregion


        #region Control Events

        protected void grdMeasurementDevice_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            this.SyncMeasurementDeviceId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "EditMeasurementDevice")
            {
                _presentermeasurementdevice.GetSyncMeasurementDeviceByMeasurementDeviceId();
                SessionManager.Instance.CurrentMeasurementDevice = SyncMeasurementDevice;

                if (SessionManager.Instance.CurrentMeasurementDevice != null)
                {
                    Response.Redirect("AddMeasurementDevice.aspx");
                }
            }
            if (e.CommandName == "DeleteMeasurementDevice")
            {
                _presentermeasurementdevice.DeleteSyncMeasurementDevice();
                BindMeasurementDeviceList();
            }

        }

        #endregion

        #region IViewMeasurementDevice Members
        public MeasurementDevice SyncMeasurementDevice { get; set; }
        public List<MeasurementDevice> SyncMeasurementDeviceList { get; set; }
        public int SyncMeasurementDeviceResult { get; set; }
        public MeasurementDevice SaveSyncMeasurementDevice { get; set; }
        public int SyncMeasurementDeviceId { get; set; }
        public Dictionary<int, string> DongleList { get; set; }
        public Dictionary<int, string> MeasurementDeviceTypeList { get; set; }
        public int BranchId { get; set; }
        #endregion


    }
}
