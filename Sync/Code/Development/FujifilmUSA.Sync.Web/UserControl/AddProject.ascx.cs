using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddProduct : System.Web.UI.UserControl, IViewAddProject
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
            BindOutputType();
            

        }
        protected void grdDLP_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            BindDeviceLinkProfile();
            //dynamic data = new[] {
            //    new { Source = "123456", Paper = "paper3",PrintVariables="ass",Separations="asasasas"},
            //    new { Source = "123456", Paper = "paper3",PrintVariables="ass",Separations="asasasas"},
            //        new { Source = "123456", Paper = "paper3",PrintVariables="ass",Separations="asasasas"},
            //            new { Source = "123456", Paper = "paper3",PrintVariables="ass",Separations="asasasas"}
            //};
            //grdDLP.DataSource = data;
        }
        #endregion

        #region Method
        private void BindOutputType()
        {
            cmbOutputType.DataSource = Enum.GetValues(typeof(EOutputType));
            cmbOutputType.DataBind();

        }
        private void BindProjectType()
        {
            cmbOutputType.DataSource = Enum.GetValues(typeof(EProjectType));
            cmbOutputType.DataBind();
        }
        private void BindSourceProfile()
        {
            _presenteraddproject.GetSourceProfileList();
            cmbSourceProfile.DataSource = GetSourceProfileList;
            cmbSourceProfile.DataBind();
            cmbSourceProfile.Items.Insert(0, new RadComboBoxItem("", "-- Select --"));

        }
        private void BindDestinationProfile()
        {
            _presenteraddproject.GetDestinationProfileList();
            cmbDestinationProfile.DataSource = GetDestinationProfileList;
            cmbDestinationProfile.DataBind();
            cmbDestinationProfile.Items.Insert(0, new RadComboBoxItem("", "-- Select --"));
        }
        private void BindDeviceLinkProfile()
        {
            this.SourceProfileId = int.Parse(cmbSourceProfile.SelectedValue);
            this.DestinationProfileId = int.Parse(cmbDestinationProfile.SelectedValue);
            _presenteraddproject.GetDeviceLinkProfileList();
            grdDLP.DataSource = GetDeviceLinkProfileList;
        }
        private void BindMeasurementIlluminant()
        {
           // cmbIlluminant.DataSource = Enum.GetValues(typeof(EMeasurementIlluminant));
           // cmbIlluminant.DataBind();
        }
        private void BindReferenceDensityType()
        {
            // cmbDensityTyp.DataSource = Enum.GetValues(typeof(EMeasurementDensityType));
            // cmbDensityTyp.DataBind();

        }
        private void BindMeasurementDevice()
        {
            this.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
            _presenteraddproject.GetMeasurementDeviceListBySyncDeviceId();
            //cmbMeasurementDevice.DataSource = GetMeasurementDeviceListBySyncDeviceId;
            //cmbMeasurementDevice.DataBind();
            
        }
        private void BindReferenceChartType()
        {
            //this.MeasurementDeviceId = int.Parse(cmbMeasurementDevice.SelectedValue);
            _presenteraddproject.GetReferenceChartTypeListByMeasurementDeviceId();
            //cmbReferenceChartType.DataSource = GetReferenceChartTypeListByMeasurementDeviceId;
            //cmbReferenceChartType.DataBind();
        }

        private int SaveProjectAndMEasurementSetting()
        {
            ObjProject = null;
            ObjMeasurementSetting = null;
            _presenteraddproject.InsertProjectAndMeasurementSetting();
            //InsertProjectAndMeasurementSetting
            return 1;
        }

        #endregion

        #region IViewAddProject Members
        public Dictionary<int, string> GetSourceProfileList { get; set; }
        public Dictionary<int, string> GetDestinationProfileList { get; set; }
        public int SourceProfileId { get; set; }
        public int DestinationProfileId { get; set; }
        public List<DeviceLinkProfile> GetDeviceLinkProfileList { get; set; }
        public int SyncDeviceId { get; set; }
        public Dictionary<int, string> GetMeasurementDeviceListBySyncDeviceId { get; set; }
        public int MeasurementDeviceId { get; set; }
        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId { get; set; }
        public Project ObjProject { get; set; }
        public MeasurementSetting ObjMeasurementSetting { get; set; }
        public Project InsertProjectAndMeasurementSetting { get; set; }
        #endregion
    }
}