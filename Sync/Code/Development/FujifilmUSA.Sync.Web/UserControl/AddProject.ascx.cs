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
            if (!IsPostBack)
            {
                BindOutputType();
                BindProjectType();


                BindSourceProfile();
                BindDestinationProfile();
                BindDeviceLinkProfile();

                BindMeasurementIlluminant();
                BindMeasurementObserver();
                BindReferenceDensityType();
                BindMeasurementDeviceType();
                BindReferenceChartType();
            }           
        }

        protected void grdDLP_ItemDataBound(object sender, GridItemEventArgs e)
        {
             if (e.Item.DataItem != null && e.Item.ItemType == GridItemType.Item)
            {
                DeviceLinkProfile dlp = e.Item.DataItem as DeviceLinkProfile;

                System.Web.UI.WebControls.RadioButton rbtnDeviceLinkProfileId = e.Item.FindControl("rbtnDeviceLinkProfileId") as System.Web.UI.WebControls.RadioButton;
                if (rbtnDeviceLinkProfileId != null)
                {
                    rbtnDeviceLinkProfileId.Attributes.Add("onclick", "javascript: return SelectDeviceLinkProfileId(" + dlp.DeviceLinkProfileId + ");");
                }
            }
        }
        
        protected void grdDLP_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //BindDeviceLinkProfile();
            //dynamic data = new[] {
            //    new { Source = "123456", Paper = "paper3",PrintVariables="ass",Separations="asasasas"},
            //    new { Source = "123456", Paper = "paper3",PrintVariables="ass",Separations="asasasas"},
            //        new { Source = "123456", Paper = "paper3",PrintVariables="ass",Separations="asasasas"},
            //            new { Source = "123456", Paper = "paper3",PrintVariables="ass",Separations="asasasas"}
            //};
            //grdDLP.DataSource = data;
            BindDeviceLinkProfile();
        }
        #endregion

        #region Control Events
        protected void cmbDestinationProfile_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbDestinationProfile.SelectedValue))
            {
                //BindDeviceLinkProfile();
            }
        }

        protected void cmbSourceProfile_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbSourceProfile.SelectedValue))
            {
                //BindDeviceLinkProfile();
            }
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
            cmbProjectType.DataSource = Enum.GetValues(typeof(EProjectType));
            cmbProjectType.DataBind();
        }
        private void BindSourceProfile()
        {
            _presenteraddproject.GetSourceProfileList();
            cmbSourceProfile.DataSource = GetSourceProfileList;
            cmbSourceProfile.DataBind();
            cmbSourceProfile.Items.Insert(0, new RadComboBoxItem("-- Select --", ""));
        }
        private void BindDestinationProfile()
        {
            _presenteraddproject.GetDestinationProfileList();
            cmbDestinationProfile.DataSource = GetDestinationProfileList;
            cmbDestinationProfile.DataBind();
            cmbDestinationProfile.Items.Insert(0, new RadComboBoxItem("-- Select --", ""));
        }
        private void BindDeviceLinkProfile()
        {
            if (!string.IsNullOrEmpty(cmbSourceProfile.SelectedValue) && !string.IsNullOrEmpty(cmbDestinationProfile.SelectedValue))
            {
                this.SourceProfileId = int.Parse(cmbSourceProfile.SelectedValue);
                this.DestinationProfileId = int.Parse(cmbDestinationProfile.SelectedValue);
                _presenteraddproject.GetDeviceLinkProfileList();
                grdDLP.DataSource = GetDeviceLinkProfileList;
            }
        }

        #region Measurement Setup - Methods
        private void BindMeasurementIlluminant()
        {
            cmbIlluminant.DataSource = Enum.GetValues(typeof(EMeasurementIlluminant));
            cmbIlluminant.DataBind();
        }

        private void BindMeasurementObserver()
        {
            RadComboBoxItem item = new RadComboBoxItem();
            cmbObserver.Items.Clear();
            cmbObserver.Items.Add(new RadComboBoxItem("2", "1"));
        }

        private void BindReferenceDensityType()
        {
            cmbDensityType.DataSource = Enum.GetValues(typeof(EMeasurementDensityType));
            cmbDensityType.DataBind();

        }
        private void BindMeasurementDeviceType()
        {
            this.SyncDeviceId = 6;//SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
            _presenteraddproject.GetMeasurementDeviceListBySyncDeviceId();
            cmbDeviceType.DataSource = GetMeasurementDeviceListBySyncDeviceId;
            cmbDeviceType.DataBind();

        }
        private void BindReferenceChartType()
        {
            this.MeasurementDeviceId = int.Parse(cmbDeviceType.SelectedValue);
            _presenteraddproject.GetReferenceChartTypeListByMeasurementDeviceId();
            cmbAlignmentChart.DataSource = GetReferenceChartTypeListByMeasurementDeviceId;
            cmbAlignmentChart.DataBind();
        }
        #endregion

        private int SaveProjectAndMEasurementSetting()
        {
            ObjProject = null;
            ObjMeasurementSetting = null;

            ObjProject.ProjectName = txtProjectName.Text.Trim();
            ObjProject.CreatedDate = DateTime.Now;
            ObjProject.SyncDeviceId = 1;
            ObjProject.ΔEType = 0;            
            ObjProject.DeviceLinkProfileId = Convert.ToInt32(hdnDeviceLinkProfileId.Value);
            ObjProject.IsUsed = false;
            ObjProject.UseDLP = false;
            ObjProject.ReTargetId = 0;
            ObjProject.ProjectTypeId = Convert.ToInt32(cmbProjectType.SelectedValue);
            ObjProject.OutputTypeId = Convert.ToInt32(cmbOutputType.SelectedValue);
            ObjProject.StartDate = DateTime.Now;
            ObjProject.SyncRunId = 0;
            ObjProject.ProjectStatus = ECPSyncRunStatus.Open;


            ObjMeasurementSetting.UsePlugin = chkUseMeasurementPlugin.Checked;
            ObjMeasurementSetting.Observer = Convert.ToInt32(cmbObserver.SelectedValue);
            ObjMeasurementSetting.Illuminant = Convert.ToInt32(cmbIlluminant.SelectedValue);
            ObjMeasurementSetting.DensityType = Convert.ToInt32(cmbDensityType.SelectedValue);
            //to-do
            //ObjMeasurementSetting.UVIncluded = ;
            //ObjMeasurementSetting.ReferenceChartTypeId = Convert.ToInt32(cmbAlignmentChart.SelectedValue);
            //ObjMeasurementSetting.IlluminationMode = ;
            //ObjMeasurementSetting.InstrumentTypeId = ;
            //ObjMeasurementSetting.SerialNumber = ;
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
