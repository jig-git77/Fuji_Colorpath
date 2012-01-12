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
    public partial class AddProduct : System.Web.UI.UserControl, IViewAddProject
    {
        #region Variable Declarations
        private PresenterAddProject _presenteraddproject;
        private PresenterGetSourceDestinationCombo _presenterGetSourceDestinationCombo;
        #endregion

        #region Page Events

        protected override void OnInit(EventArgs e)
        {
            _presenteraddproject = new PresenterAddProject(this);
            _presenterGetSourceDestinationCombo = new PresenterGetSourceDestinationCombo(this);
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

        #endregion

        #region Control Events

        protected void cmbDestinationProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeviceLinkProfile();
        }

        protected void cmbSourceProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeviceLinkProfile();
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

        protected void grdDLP_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                var dlp = e.Item.DataItem as DeviceLinkProfile;
                if (dlp == null) return;

                var rbtnDeviceLinkProfileId = e.Item.FindControl("rbtnDeviceLinkProfileId") as RadioButton;
                if (rbtnDeviceLinkProfileId == null) return;

                rbtnDeviceLinkProfileId.Attributes.Add(
                    "onclick",
                    "javascript: return fnCheckUnCheck('" +
                    rbtnDeviceLinkProfileId.ClientID + "','" + dlp.ProfileId + "');");
                if (SessionManager.Instance.CurrentProject != null)
                {
                    if (dlp.ProfileId == SessionManager.Instance.CurrentProject.DeviceLinkProfileId)
                    {
                        rbtnDeviceLinkProfileId.Checked = true;
                    }
                }
            }
        }
        #endregion

        #region Private Method
        private void BindOutputType()
        {
            cmbOutputType.DataSource = Utilities.GetEnumDropdown<EOutputType>();
            cmbOutputType.DataTextField = "Value";
            cmbOutputType.DataValueField = "Key";
            cmbOutputType.DataBind();

        }
        private void BindProjectType()
        {
            cmbProjectType.DataSource = Utilities.GetEnumDropdown<EProjectType>();
            cmbProjectType.DataTextField = "Value";
            cmbProjectType.DataValueField = "Key";
            cmbProjectType.DataBind();
        }
        private void BindSourceProfile()
        {
            _presenterGetSourceDestinationCombo.GetSourceProfileList();
            cmbSourceProfile.DataSource = GetSourceProfiles;
            cmbSourceProfile.DataBind();
            cmbSourceProfile.Items.Insert(0, new ListItem("-- Select --", ""));
        }
        private void BindDestinationProfile()
        {
            this.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
            _presenterGetSourceDestinationCombo.GetDestinationProfiles();
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
                _presenteraddproject.GetDeviceLinkProfileList();
            }
            else
            {
                this.SourceProfileId = 0;
                this.DestinationProfileId = 0;
            }
            grdDLP.DataSource = DeviceLinkProfiles;
            grdDLP.DataBind();
            RefreshProjectId();
        }
        private void RefreshProjectId()
        {
            hdnDeviceLinkProfileId.Value = "0";
        }
        private void SaveProjectAndMeasurementSetting()
        {
            Project = new Project();
            MeasurementSetting = new MeasurementSetting();
            if (SessionManager.Instance.CurrentSyncDevice != null)
            {
                Project.ProjectName = txtProjectName.Text.Trim();
                Project.CreatedDate = DateTime.Now;
                Project.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;// 1;
                Project.ΔEType = 1;
                Project.DeviceLinkProfileId = Convert.ToInt32(hdnDeviceLinkProfileId.Value);
                Project.IsUsed = true;
                Project.UseDLP = true;
                Project.ReTargetId = 0;
                Project.ProjectTypeId = (int)(EProjectType)Enum.Parse(typeof(EProjectType), cmbProjectType.SelectedValue);
                Project.OutputTypeId = (int)(EOutputType)Enum.Parse(typeof(EOutputType), cmbOutputType.SelectedValue);
                Project.StartDate = DateTime.Now;

                Project.ProjectStatus = ECPSyncRunStatus.Open;


                MeasurementSetting.UsePlugin = true;//static //chkUseMeasurementPlugin.Checked;
                MeasurementSetting.Observer = Convert.ToInt32(cmbObserver.SelectedValue);
                MeasurementSetting.Illuminant = (int)(EMeasurementIlluminant)Enum.Parse(typeof(EMeasurementIlluminant), cmbIlluminant.SelectedValue);
                MeasurementSetting.DensityType = (int)(EMeasurementDensityType)Enum.Parse(typeof(EMeasurementDensityType), cmbDensityType.SelectedValue);
                //to-do
                MeasurementSetting.UVIncluded = true;
                MeasurementSetting.ReferenceChartTypeId = Convert.ToInt32(ucDeviceTypeSelectionCommon.ReferenceChartTypeValue);
                MeasurementSetting.IlluminationMode = 1;

                MeasurementSetting.InstrumentTypeId = ucDeviceTypeSelectionCommon.InstrumentTypeId;
                MeasurementSetting.SerialNumber = ucDeviceTypeSelectionCommon.SerialNumber;

                _presenteraddproject.InsertProjectAndMeasurementSetting();
                SessionManager.Instance.CurrentProject = Project;
                SessionManager.Instance.CurrentSyncRun = SessionManager.Instance.CurrentProject.ProjectRuns.Last();
                Response.Redirect("SyncRunExport.aspx");
            }
        }
        private void BindMeasurementIlluminant()
        {
            cmbIlluminant.DataSource = Utilities.GetEnumDropdown<EMeasurementIlluminant>();
            cmbIlluminant.DataTextField = "Value";
            cmbIlluminant.DataValueField = "Key";
            cmbIlluminant.DataBind();

        }
        private void BindMeasurementObserver()
        {
            cmbObserver.Items.Clear();
            cmbObserver.Items.Add(new ListItem("2", "1"));
        }
        private void BindReferenceDensityType()
        {
            cmbDensityType.DataSource = Utilities.GetEnumDropdown<EMeasurementDensityType>();
            cmbDensityType.DataTextField = "Value";
            cmbDensityType.DataValueField = "Key";
            cmbDensityType.DataBind();

        }
        private void LoadProjectByProjectId()
        {
            if (SessionManager.Instance.CurrentProject != null)
            {
                //Get Current Project Detail by Project Id
                Project = SessionManager.Instance.CurrentProject;
                txtProjectName.Text = Project.ProjectName;
                cmbProjectType.SelectedValue = Project.ProjectTypeId.ToString();
                cmbOutputType.SelectedValue = Project.OutputTypeId.ToString();

                this.SyncDeviceId = Project.SyncDeviceId;

                _presenteraddproject.GetDeviceLinkProfile();
                cmbSourceProfile.SelectedValue = deviceLinkProfile.SrcProfile.ProfileId.ToString();
                cmbDestinationProfile.SelectedValue = deviceLinkProfile.DestProfile.ProfileId.ToString();
                BindDeviceLinkProfile();

                // Get Measurement Detail By ProjectId
                _presenteraddproject.GetMeasurementSetting();
                cmbIlluminant.SelectedValue = MeasurementSetting.Illuminant.ToString();
                cmbObserver.SelectedValue = MeasurementSetting.Observer.ToString();
                cmbDensityType.SelectedValue = MeasurementSetting.DensityType.ToString();
                //ucDeviceTypeSelectionCommon.ReferenceChartTypeValue = MeasurementSetting.ReferenceChartTypeId.ToString();
                //ucDeviceTypeSelectionCommon.SerialNumber = MeasurementSetting.SerialNumber;
            }
        }
        public void ToggleInputControlState(bool value)
        {
            txtProjectName.Enabled = value;
            cmbProjectType.Enabled = value;
            cmbOutputType.Enabled = value;
            cmbSourceProfile.Enabled = value;
            cmbDestinationProfile.Enabled = value;
            grdDLP.Enabled = value;
            cmbIlluminant.Enabled = value;
            cmbObserver.Enabled = value;
            cmbDensityType.Enabled = value;
            //ucDeviceTypeSelectionCommon.DeviceTypeEnable = value;
            //ucDeviceTypeSelectionCommon.ReferenceChartTypeEnable = value;
            lbtnProjectSave.Enabled = value;
        }
        #endregion

        #region Error Handling Method
        public void ErrorOccured(object sender, Business.Interface.ErrorArgs e)
        {
            ucStatusbar1.DisplayMessage(e.ErrorMessage, false);
        }
        #endregion

        //*******************************************************************//

        #region IViewAddProject Members

        public Dictionary<string, string> MeasurementDevices { get; set; }

        public Dictionary<int, string> ReferenceChartTypes { get; set; }

        public List<DeviceLinkProfile> DeviceLinkProfiles { get; set; }

        public DeviceLinkProfile deviceLinkProfile { get; set; }

        public int SourceProfileId { get; set; }

        public int DestinationProfileId { get; set; }

        public int MeasurementDeviceId { get; set; }

        public MeasurementSetting MeasurementSetting { get; set; }

        #endregion

        #region IViewGetSourceDestinationCombo Members

        public Dictionary<int, string> GetSourceProfiles { get; set; }

        public Dictionary<int, string> GetDestinationProfiles { get; set; }

        #endregion

        #region IViewBaseSyncDevice Members

        public int SyncDeviceId { get; set; }

        #endregion

        #region IViewBaseProject Members

        public Project Project { get; set; }

        #endregion
    }
}
