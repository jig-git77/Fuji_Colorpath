using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using System.Linq;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddProduct : System.Web.UI.UserControl, IViewAddProject,IViewProjectList,IViewDeviceLinkProfile
    {
        #region "Declaration"
        private PresenterAddProject _presenteraddproject;
        private PresenterProjectList _presenterprojectlist;
        private PresenterDeviceLinkProfile _presenterdevicelinkprofile;
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presenteraddproject = new PresenterAddProject(this);
            _presenterprojectlist = new PresenterProjectList(this);
            _presenterdevicelinkprofile = new PresenterDeviceLinkProfile(this);
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
              

                if (SessionManager.Instance.CurrentProject != null)
                {
                    LoadProjectByProjectId();
                    ToggleInputControlState(false);
                }
            }
        }

        protected void grdDLP_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                DeviceLinkProfile dlp = e.Item.DataItem as DeviceLinkProfile;
                
                System.Web.UI.WebControls.RadioButton rbtnDeviceLinkProfileId = e.Item.FindControl("rbtnDeviceLinkProfileId") as System.Web.UI.WebControls.RadioButton;
                if (rbtnDeviceLinkProfileId != null)
                {
                    rbtnDeviceLinkProfileId.Attributes.Add("onclick", "javascript: return SelectDeviceLinkProfileId(" + dlp.DeviceLinkProfileId + ");");
                }
                if (SessionManager.Instance.CurrentProject != null)
                {
                    if (dlp.DeviceLinkProfileId == SessionManager.Instance.CurrentProject.DeviceLinkProfileId)
                    {
                        rbtnDeviceLinkProfileId.Checked = true;
                    }
                }
            }
        }

        //protected void grdDLP_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item.DataItem != null)
        //    {
        //        DeviceLinkProfile dlp = e.Item.DataItem as DeviceLinkProfile;

        //        System.Web.UI.WebControls.RadioButton rbtnDeviceLinkProfileId = e.Item.FindControl("rbtnDeviceLinkProfileId") as System.Web.UI.WebControls.RadioButton;
        //        if (rbtnDeviceLinkProfileId != null)
        //        {
        //            rbtnDeviceLinkProfileId.Attributes.Add("onclick", "javascript: return SelectDeviceLinkProfileId(" + dlp.DeviceLinkProfileId + ");");
        //        }
        //    }
        //}

        protected void grdDLP_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //BindDeviceLinkProfile();
        }
        #endregion

        #region Control Events        

        protected void cmbDestinationProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbDestinationProfile.SelectedValue))
            {
                BindDeviceLinkProfile();
            }
        }

        protected void cmbSourceProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbSourceProfile.SelectedValue))
            {
                BindDeviceLinkProfile();
            }
        }
        

        protected void lbtnProjectSave_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");

            //Project name is required field
            if (string.IsNullOrEmpty(txtProjectName.Text.Trim()))
                ucStatusbar1.DisplayMessage(ResourceManager.GetResourceMessage("AddProject_ProjectName_Required"), false);
            //Validate source profile
            else if (string.IsNullOrEmpty(cmbSourceProfile.SelectedValue))
                ucStatusbar1.DisplayMessage(ResourceManager.GetResourceMessage("AddProject_SourceProfile_Required"), false);
            //Validate destination profile
            else if (string.IsNullOrEmpty(cmbDestinationProfile.SelectedValue))
                ucStatusbar1.DisplayMessage(ResourceManager.GetResourceMessage("AddProject_DestinationProfile_Required"), false);
            //Validate the selected DLP
            else if (hdnDeviceLinkProfileId.Value == "0")
                ucStatusbar1.DisplayMessage(ResourceManager.GetResourceMessage("AddProject_DLP_Required"), false);

            //If everything is validate fire the save event
            else
                SaveProjectAndMeasurementSetting();

        }
        #endregion

        #region Method
        private void BindOutputType()
        {           
            var dictionary = new Dictionary<int, string>();
            foreach (int value in Enum.GetValues(typeof(EOutputType)))
            {
                dictionary.Add(value, Enum.GetName(typeof(EOutputType), value));
            }

            cmbOutputType.DataSource = dictionary;
            cmbOutputType.DataTextField = "Value";
            cmbOutputType.DataValueField = "Key";
            cmbOutputType.DataBind();

        }
        private void BindProjectType()
        {
            var dictionary = new Dictionary<int, string>();
            foreach (int value in Enum.GetValues(typeof(EProjectType)))
            {
                dictionary.Add(value, Enum.GetName(typeof(EProjectType), value));
            }

            cmbProjectType.DataSource = dictionary;
            cmbProjectType.DataTextField = "Value";
            cmbProjectType.DataValueField = "Key";
            cmbProjectType.DataBind();
        }
        private void BindSourceProfile()
        {
            _presenteraddproject.GetSourceProfileList();
            cmbSourceProfile.DataSource = GetSourceProfileList;
            cmbSourceProfile.DataBind();
            cmbSourceProfile.Items.Insert(0,new ListItem("-- Select --", ""));
        }
        private void BindDestinationProfile()
        {
            this.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
            _presenteraddproject.GetDestinationProfiles();
            cmbDestinationProfile.DataSource = GetDestinationProfiles;
            cmbDestinationProfile.DataBind();
            cmbDestinationProfile.Items.Insert(0, new ListItem("-- Select --", ""));
        }
        private void BindDeviceLinkProfile()
        {
            if (!string.IsNullOrEmpty(cmbSourceProfile.SelectedValue) && !string.IsNullOrEmpty(cmbDestinationProfile.SelectedValue))
            {
                this.SourceProfileId = int.Parse(cmbSourceProfile.SelectedValue);
                this.DestinationProfileId = int.Parse(cmbDestinationProfile.SelectedValue);
            }
            else
            {
                this.SourceProfileId = 0;
                this.DestinationProfileId = 0;
            }
            _presenteraddproject.GetDeviceLinkProfileList();
            grdDLP.DataSource = GetDeviceLinkProfileList;
            grdDLP.DataBind();
            RefreshProjectId();
        }
        private void RefreshProjectId()
        {
            hdnDeviceLinkProfileId.Value = "0";
        }
        #region Measurement Setup - Methods
        private void BindMeasurementIlluminant()
        {
            var dictionary = new Dictionary<int, string>();
            foreach (int value in Enum.GetValues(typeof(EMeasurementIlluminant)))
            {
                dictionary.Add(value, Enum.GetName(typeof(EMeasurementIlluminant), value));
            }

            cmbIlluminant.DataSource = dictionary;
            cmbIlluminant.DataTextField = "Value";
            cmbIlluminant.DataValueField = "Key";
            cmbIlluminant.DataBind();
          
        }
         
        private void BindMeasurementObserver()
        {
            RadComboBoxItem item = new RadComboBoxItem();
            cmbObserver.Items.Clear();
            cmbObserver.Items.Add(new ListItem("2", "1"));
        }

        private void BindReferenceDensityType()
        {
            var dictionary = new Dictionary<int, string>();
            foreach (int value in Enum.GetValues(typeof(EMeasurementDensityType)))
            {
                dictionary.Add(value, Enum.GetName(typeof(EMeasurementDensityType), value));
            }

            cmbDensityType.DataSource = dictionary;
            cmbDensityType.DataTextField = "Value";
            cmbDensityType.DataValueField = "Key";
            cmbDensityType.DataBind();

        }
       
       

        private void LoadProjectByProjectId()
        {
            if (SessionManager.Instance.CurrentProject != null)
            {

                //Get Current Project Detail by Project Id
                this.ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                _presenterprojectlist.GetProjectByProjectId();
                txtProjectName.Text = GetProjectByProjectId.ProjectName;
                cmbProjectType.SelectedValue = GetProjectByProjectId.ProjectTypeId.ToString();
                cmbOutputType.SelectedValue = GetProjectByProjectId.OutputTypeId.ToString();
                this.SyncDeviceId = GetProjectByProjectId.SyncDeviceId;

                // Get DevicelinkProfileList by DLPID
                this.DLPID = GetProjectByProjectId.DeviceLinkProfileId;
                _presenterdevicelinkprofile.GetDeviceLinkProfileListByDLPID();
                cmbSourceProfile.SelectedValue = GetDeviceLinkProfileListByDLPID.SrcProfile.SourceProfileId.ToString();
                cmbDestinationProfile.SelectedValue = GetDeviceLinkProfileListByDLPID.DestProfile.DestinationProfileId.ToString();
                BindDeviceLinkProfile();                

                // Get Measurement Detail By ProjectId
                _presenterprojectlist.GetMeasurementSettingByProjectID();
                cmbIlluminant.SelectedValue = GetMeasurementSettingByProjectID.Illuminant.ToString();
                cmbObserver.SelectedValue =GetMeasurementSettingByProjectID.Observer.ToString();
                cmbDensityType.SelectedValue = GetMeasurementSettingByProjectID.DensityType.ToString();                
                ucCmbDeviceType.AlignmentChart_Value = GetMeasurementSettingByProjectID.ReferenceChartTypeId.ToString();

                ucCmbDeviceType.SetCmbDeviceTypeBySerialNumber = GetMeasurementSettingByProjectID.SerialNumber;


            }
        }

        public void ToggleInputControlState(bool value)
        {
            txtProjectName.Enabled =value;
            cmbProjectType.Enabled = value;
            cmbOutputType.Enabled = value;
            cmbSourceProfile.Enabled = value;
            cmbDestinationProfile.Enabled = value;
            grdDLP.Enabled = value;
            cmbIlluminant.Enabled = value;
            cmbObserver.Enabled = value;
            cmbDensityType.Enabled = value;
            ucCmbDeviceType.DeviceType_Enable = value;
            ucCmbDeviceType.AlignmentChart_Enable = value;
            lbtnProjectSave.Enabled = value;
        }
        #endregion

        private void SaveProjectAndMeasurementSetting()
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

                ObjProject.ProjectStatus = ECPSyncRunStatus.Open;


                ObjMeasurementSetting.UsePlugin = true;//static //chkUseMeasurementPlugin.Checked;
                ObjMeasurementSetting.Observer = Convert.ToInt32(cmbObserver.SelectedValue);
                ObjMeasurementSetting.Illuminant = (int)(EMeasurementIlluminant)Enum.Parse(typeof(EMeasurementIlluminant), cmbIlluminant.SelectedValue);
                ObjMeasurementSetting.DensityType = (int)(EMeasurementDensityType)Enum.Parse(typeof(EMeasurementDensityType), cmbDensityType.SelectedValue);
                //to-do
                ObjMeasurementSetting.UVIncluded = true;
                ObjMeasurementSetting.ReferenceChartTypeId = Convert.ToInt32(ucCmbDeviceType.AlignmentChart_Value);
                ObjMeasurementSetting.IlluminationMode = 1;

                //Split the device type and take the 1st position id.
                ObjMeasurementSetting.InstrumentTypeId = Convert.ToInt32(ucCmbDeviceType.DeviceType_Value.Split(new char[] { '-' })[0].Trim());

                //Split the device type and take the 3rd position string.
                string[] splittedDeviceType = ucCmbDeviceType.DeviceType_SelectedItem.Text.Trim().Split(new char[] { '-' });
                ObjMeasurementSetting.SerialNumber = string.Empty;
                if (splittedDeviceType.Length > 2)
                    ObjMeasurementSetting.SerialNumber = splittedDeviceType[2].Trim();

                _presenteraddproject.InsertProjectAndMeasurementSetting();
                if (InsertProjectAndMeasurementSetting != null)
                {
                    SessionManager.Instance.CurrentProject = InsertProjectAndMeasurementSetting;
                    SessionManager.Instance.CurrentSyncRun = SessionManager.Instance.CurrentProject.ProjectRuns.Last();
                    Response.Redirect("SyncRunExport.aspx");
                }
            }
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
       
        #region IViewProjectList Members        
        public List<Project> GetProjectListBySyncDeviceId { get; set; }
        public int ProjectId { get; set; }
        public int DeleteProject { get; set; }
        public Project GetProjectByProjectId { get; set; }
        public MeasurementSetting GetMeasurementSettingByProjectID { get; set; }
        #endregion

        #region IViewDeviceLinkProfile
        public int DLPID { get; set; }
        public DeviceLinkProfile GetDeviceLinkProfileListByDLPID { get; set; }
        #endregion

    }
} 
