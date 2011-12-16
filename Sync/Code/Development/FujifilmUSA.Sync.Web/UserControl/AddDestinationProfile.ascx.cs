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
    public partial class AddDestinationProfile : System.Web.UI.UserControl,IViewAddProject
    {
        #region "Declaration"
        private PresenterAddProject _presenteraddproject;
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presenteraddproject = new PresenterAddProject(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
            if (!Page.IsPostBack)
            {               
                BindMeasurementDeviceType();
                BindReferenceChartType();
            }
        }

        #endregion

        #region Methods
        private void BindMeasurementDeviceType()
        {
            if (SessionManager.Instance.CurrentSyncDevice != null)
            {
                this.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
                _presenteraddproject.GetMeasurementDeviceListBySyncDeviceId();
                cmbDeviceType.DataSource = GetMeasurementDeviceListBySyncDeviceId;
                cmbDeviceType.DataBind();
            }

        }

        private void BindReferenceChartType()
        {
            //Split device type and take the 2nd position number as MeasurementDeviceId
            this.MeasurementDeviceId = int.Parse(cmbDeviceType.SelectedValue.Split(new char[] { '-' })[1].Trim());
            _presenteraddproject.GetReferenceChartTypeListByMeasurementDeviceId();
            cmbAlignmentChart.DataSource = GetReferenceChartTypeListByMeasurementDeviceId;
            cmbAlignmentChart.DataBind();
        }

        #endregion
       

        #region IViewAddProject Members
        public Dictionary<int, string> GetSourceProfileList { get; set; }
        public Dictionary<int, string> GetDestinationProfiles { get; set; }
        public int SourceProfileId { get; set; }
        public int DestinationProfileId { get; set; }
        public List<DeviceLinkProfile> GetDeviceLinkProfileList { get; set; }
        public int SyncDeviceId { get; set; }
        public Dictionary<string, string> GetMeasurementDeviceListBySyncDeviceId { get; set; }
        public int MeasurementDeviceId { get; set; }
        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId { get; set; }
        public Project ObjProject { get; set; }
        public MeasurementSetting ObjMeasurementSetting { get; set; }
        public Project InsertProjectAndMeasurementSetting { get; set; }
        #endregion
    }
}