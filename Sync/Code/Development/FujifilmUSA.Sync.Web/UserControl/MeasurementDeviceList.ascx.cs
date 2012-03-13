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
using Telerik.Web.UI;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class MeasurementDeviceList : System.Web.UI.UserControl 
    {
        #region Declaration
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
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
            if (!Page.IsPostBack)
            {
                //BindMeasurementDeviceList();
            }
        }
        #endregion

        #region Methods
        //public object DisplayIsActiveCustomValue(object value)
        //{
        //    if (Convert.ToBoolean(value))
        //        return ResourceManager.GetResourceMessage("MeasurementDeviceList_IsActive_True");
        //    else
        //        return ResourceManager.GetResourceMessage("MeasurementDeviceList_IsActive_False");
        //}

        //public void BindMeasurementDeviceList()
        //{
        //    _presentermeasurementdevice.GetSyncMeasurementDeviceList();
        //    grdMeasurementDevice.DataSource = SyncMeasurementDeviceList;
        //    grdMeasurementDevice.DataBind();
        //}

        //private List<MeasurementDevice> GetMesurmentDevice()
        //{
        //    List<MeasurementDevice> lst = new List<MeasurementDevice>();
        //    lst.Add(new MeasurementDevice () { MeasurementDeviceId = 1, LicenseId= 1, SerialNumber="SerialNumber 1", IsActive=true, InstrumentType= new InstrumentType () { InstrumentTypeId = 1, InstrumentType1= "InstrumentType", IsUsed=true}, InstrumentTypeId= 1, MeasurementSettings= new List<MeasurementSetting>() });
        //    lst.Add(new MeasurementDevice() { MeasurementDeviceId = 2, LicenseId = 1, SerialNumber = "SerialNumber 2", IsActive = true, InstrumentType = new InstrumentType() { InstrumentTypeId = 1, InstrumentType1 = "InstrumentType", IsUsed = true }, InstrumentTypeId = 1, MeasurementSettings = new List<MeasurementSetting>() });
        //    lst.Add(new MeasurementDevice() { MeasurementDeviceId = 3, LicenseId = 1, SerialNumber = "SerialNumber 3", IsActive = true, InstrumentType = new InstrumentType() { InstrumentTypeId = 1, InstrumentType1 = "InstrumentType", IsUsed = true }, InstrumentTypeId = 1, MeasurementSettings = new List<MeasurementSetting>() });
        //    lst.Add(new MeasurementDevice() { MeasurementDeviceId = 4, LicenseId = 1, SerialNumber = "SerialNumber 4", IsActive = true, InstrumentType = new InstrumentType() { InstrumentTypeId = 1, InstrumentType1 = "InstrumentType", IsUsed = true }, InstrumentTypeId = 1, MeasurementSettings = new List<MeasurementSetting>() });
        //    lst.Add(new MeasurementDevice() { MeasurementDeviceId = 5, LicenseId = 1, SerialNumber = "SerialNumber 5", IsActive = true, InstrumentType = new InstrumentType() { InstrumentTypeId = 1, InstrumentType1 = "InstrumentType", IsUsed = true }, InstrumentTypeId = 1, MeasurementSettings = new List<MeasurementSetting>() });
        //    return lst;
        //}

        //private void BindMesurmentDevice(List<MeasurementDevice> lst)
        //{
        //    rgMeasurementDevice.DataSource = GetMesurmentDevice();
        //    rgMeasurementDevice.DataBind();

        //}
        #endregion

        #region Control Events
        protected void rgMeasurementDevice_ItemCommand(object sender, GridCommandEventArgs e)
        {
            //this.SyncMeasurementDeviceId = Convert.ToInt32(e.CommandArgument);
            //if (e.CommandName == "EditMeasurementDevice")
            //{
            //    _presentermeasurementdevice.GetSyncMeasurementDeviceByMeasurementDeviceId();
            //    SessionManager.Instance.CurrentMeasurementDevice = SyncMeasurementDevice;

            //    if (SessionManager.Instance.CurrentMeasurementDevice != null)
            //    {
            //        Response.Redirect("AddMeasurementDevice.aspx");
            //    }
            //}
            //if (e.CommandName == "DeleteMeasurementDevice")
            //{
            //    _presentermeasurementdevice.DeleteSyncMeasurementDevice();
            //    BindMeasurementDeviceList();
            //}
        }
        protected void rgMeasurementDevice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
        }
        protected void rgMeasurementDevice_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }
  

        protected void likAdd_Click(object sender, EventArgs e)
        {
            //SessionManager.Instance.CurrentMeasurementDevice = null;
            //Response.RedirectPermanent("AddMeasurementDevice.aspx");
        }

        #endregion
   


        //*******************************************************************//
        //#region IViewMeasurementDevice Members
        //public MeasurementDevice SyncMeasurementDevice { get; set; }
        //public List<MeasurementDevice> SyncMeasurementDeviceList { get; set; }
        //public int SyncMeasurementDeviceResult { get; set; }
        //public MeasurementDevice SaveSyncMeasurementDevice { get; set; }
        //public int SyncMeasurementDeviceId { get; set; }
        //public Dictionary<int, string> DongleList { get; set; }
        //public Dictionary<int, string> MeasurementDeviceTypeList { get; set; }
        //public int BranchId { get; set; }
        //#endregion

      

      


    }
}
