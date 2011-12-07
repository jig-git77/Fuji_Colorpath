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
            SessionManager.CheckLogin();
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
            if (e.Item.DataItem != null)
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
            BindDeviceLinkProfile();
        }
        #endregion

        #region Control Events
        //protected void cmbDestinationProfile_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(cmbDestinationProfile.SelectedValue))
        //    {
        //        //BindDeviceLinkProfile();
        //    }
        //}

        //protected void cmbSourceProfile_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(cmbSourceProfile.SelectedValue))
        //    {
        //        //BindDeviceLinkProfile();
        //    }
        //}
        protected void lbtnProjectSave_Click(object sender, EventArgs e)
        {            
            
            //Validate source profile
            if (string.IsNullOrEmpty(cmbSourceProfile.SelectedValue))
                ucStatusbar1.DisplayMessage("Please select source profile.", false);
            //Validate destination profile
            else if(string.IsNullOrEmpty(cmbDestinationProfile.SelectedValue))
                ucStatusbar1.DisplayMessage("Please select destination profile.", false);
            //Validate the selected DLP
            else if (hdnDeviceLinkProfileId.Value == "0")
                ucStatusbar1.DisplayMessage("Please select DLP.", false);            
            //If everything is validate fire the save event
            else            
                SaveProjectAndMeasurementSetting();            
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
            this.MeasurementDeviceId = int.Parse(cmbDeviceType.SelectedValue);
            _presenteraddproject.GetReferenceChartTypeListByMeasurementDeviceId();
            cmbAlignmentChart.DataSource = GetReferenceChartTypeListByMeasurementDeviceId;
            cmbAlignmentChart.DataBind();
        }
        #endregion

        private int SaveProjectAndMeasurementSetting()
        {
            ObjProject = new Project();
            ObjMeasurementSetting = new MeasurementSetting();
            if (SessionManager.Instance.CurrentSyncDevice != null)
            {
                ObjProject.ProjectName = txtProjectName.Text.Trim();
                ObjProject.CreatedDate = DateTime.Now;
                ObjProject.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;// 1;
                ObjProject.ΔEType = 1;
                ObjProject.DeviceLinkProfileId = Convert.ToInt32(hdnDeviceLinkProfileId.Value);
                ObjProject.IsUsed = true;
                ObjProject.UseDLP = true;
                ObjProject.ReTargetId = 0;
                //EProjectType projectType = (EProjectType)Enum.Parse(typeof(EProjectType), cmbProjectType.SelectedValue);
                ObjProject.ProjectTypeId = (int)(EProjectType)Enum.Parse(typeof(EProjectType), cmbProjectType.SelectedValue);
                ObjProject.OutputTypeId = (int)(EOutputType)Enum.Parse(typeof(EOutputType), cmbOutputType.SelectedValue);
                ObjProject.StartDate = DateTime.Now;
                ObjProject.SyncRunId = 0;
                ObjProject.ProjectStatus = ECPSyncRunStatus.Open;


                ObjMeasurementSetting.UsePlugin = chkUseMeasurementPlugin.Checked;
                ObjMeasurementSetting.Observer = Convert.ToInt32(cmbObserver.SelectedValue);
                ObjMeasurementSetting.Illuminant = (int)(EMeasurementIlluminant)Enum.Parse(typeof(EMeasurementIlluminant), cmbIlluminant.SelectedValue);
                ObjMeasurementSetting.DensityType = (int)(EMeasurementDensityType)Enum.Parse(typeof(EMeasurementDensityType), cmbDensityType.SelectedValue);
                //to-do
                ObjMeasurementSetting.UVIncluded = true;
                ObjMeasurementSetting.ReferenceChartTypeId = Convert.ToInt32(cmbAlignmentChart.SelectedValue);
                ObjMeasurementSetting.IlluminationMode = 1;
                ObjMeasurementSetting.InstrumentTypeId = 1;  // Static
                ObjMeasurementSetting.SerialNumber = "304060"; // Static
                _presenteraddproject.InsertProjectAndMeasurementSetting();
                if (InsertProjectAndMeasurementSetting != null)
                {
                    SessionManager.Instance.CurrentProject = InsertProjectAndMeasurementSetting;
                    Response.Redirect("AddMeasurement.aspx");
                    return 1;
                }
            }

            return 0;
        }

        #endregion

        #region IViewAddProject Members
        public Dictionary<int, string> GetSourceProfileList { get; set; }
        public Dictionary<int, string> GetDestinationProfileList { get; set; }
        public int SourceProfileId { get; set; }
        public int DestinationProfileId { get; set; }
        public List<DeviceLinkProfile> GetDeviceLinkProfileList { get; set; }
        public int SyncDeviceId { get; set; }
        public Dictionary<string , string> GetMeasurementDeviceListBySyncDeviceId { get; set; }
        public int MeasurementDeviceId { get; set; }
        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId { get; set; }
        public Project ObjProject { get; set; }
        public MeasurementSetting ObjMeasurementSetting { get; set; }
        public Project InsertProjectAndMeasurementSetting { get; set; }
        #endregion


    }
}
