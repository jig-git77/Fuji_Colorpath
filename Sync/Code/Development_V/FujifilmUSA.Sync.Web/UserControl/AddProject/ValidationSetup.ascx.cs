using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl.AddProject
{
    public partial class ValidationSetup : BaseControl, IViewMeasurementSetting
    {
        public ValidationSetup()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        #region Private Variables - Globalization
        private const string _recnoValidationLicense = "noValidationLicense";
        private const string _recselectText = "selectText";
        private const string _recrfvcMeasurementDeviceContainer = "rfvcMeasurementDeviceContainer";
        private const string _recrfvcValidationChartContainer = "rfvcValidationChartContainer";
        private const string _recvccmbMetricTargetPreset = "vccmbMetricTargetPreset";        
        #endregion

        #region Declaration
        private PresenterMeasurementSetting _presenterMeasurementSetting;
        private const string vsIsPostBack = "_IsPostBack";
        #endregion

        #region Page events
        protected override void OnInit(EventArgs e)
        {
            _presenterMeasurementSetting = new PresenterMeasurementSetting(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsXmfUser)
            {
                trListHeader.Visible = false;
                if (SessionManager.Instance.CurrentValidationRequest != null)
                {
                    lnkbtnValidationSetup.Visible = false;
                    btnValidationMeasure.Visible = true;
                }
                else
                    btnValidationMeasure.Visible = false;
            }
            else
            {
                trListHeader.Visible = true;
                btnValidationMeasure.Visible = false;
            }

            if (ViewState[vsIsPostBack] == null)
            {
                BindMeasurementDevice();
                BindMetricTargetPreset();
                LoadProjectInfoFromSession();
                ViewState[vsIsPostBack] = true;

                if (!SessionManager.Instance.CurrentDevice.IsValidation) //---If this device doesn't have permission for Validation, disable radiobuttonlist------//
                {
                    rbtnEnableValidation.Enabled = false;
                    panelValidationChart.Enabled = false;
                    panel_Metrictarget.Enabled = false;
                    lbl_Valimsg.Text = GetResourceString(_recnoValidationLicense) ;//"You are not currently licensed for Validation"
                }

                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)   //-----------If we are in Validation mode, we can't disable validation-----------//
                {
                    rbtnEnableValidation.Items[1].Enabled = false;
                }
            }
            
            //Set localized text
            rfvcMeasurementDeviceContainer.ValidationMsg = GetResourceString(_recrfvcMeasurementDeviceContainer);
            rfvcValidationChartContainer.ValidationMsg = GetResourceString(_recrfvcValidationChartContainer);
            vccmbMetricTargetPreset.ValidationMsg = GetResourceString(_recvccmbMetricTargetPreset);
        }
        #endregion

        #region Control events
        protected void lnkbtnValidationSetup_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.EnabledValidation = EnableValidationValue;
            SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementDeviceID = (short)MeasurementDeviceSelectedValue;
            SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType = new KeyValuePair<short, string>(Convert.ToInt16(ValidationChartSelectedValue), cmbValidationChart.SelectedItem.Text);
            SessionManager.Instance.CurrentProject.MeasurementSetting.MetricTargetPreset = new KeyValuePair<int, string>(Convert.ToInt32(cmbMetricTargetPreset.SelectedItem.Value), cmbMetricTargetPreset.SelectedItem.Text);

            //Load save control
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Save;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }

        protected void lnkbtnMeasure_Click(object sender, EventArgs e)
        {
            //Save modified validation setup details here
            SessionManager.Instance.CurrentProject.EnabledValidation = true;
            SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementDeviceID = (short)MeasurementDeviceSelectedValue;
            SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType = new KeyValuePair<short, string>(Convert.ToInt16(ValidationChartSelectedValue), cmbValidationChart.SelectedItem.Text);
            SessionManager.Instance.CurrentProject.MeasurementSetting.MetricTargetPreset = new KeyValuePair<int, string>(Convert.ToInt32(cmbMetricTargetPreset.SelectedItem.Value), cmbMetricTargetPreset.SelectedItem.Text);
            SessionManager.Instance.CurrentProject.CreateDate = DateTime.Now;    
            this.Project = SessionManager.Instance.CurrentProject;
            _presenterMeasurementSetting.SaveProject();

            if (!IsErrorOccured)
            {
                //Set validation as sub operation type
                SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.Validation;
                _presenterMeasurementSetting.GetProjectDetailWithVerification();
                SessionManager.Instance.CurrentProject = Project;

                //Redirect to measurement page
                Response.Redirect(string.Format("COMeasurement.aspx?id={0}&projectid={1}", Guid.NewGuid(), SessionManager.Instance.CurrentProject != null ? SessionManager.Instance.CurrentProject.ProjectId : 0));
            }
        }

        protected void cmbMeasurementDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindValidationChart();
        }

        protected void cmbValidationChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayValidationChartImage();
        }


        #endregion

        #region Methods
        private void LoadProjectInfoFromSession()
        {
            if (SessionManager.Instance.CurrentProject != null && SessionManager.Instance.CurrentProject.MeasurementSetting != null)
            {
                if (IsXmfUser)
                    EnableValidationValue = true;
                else
                    EnableValidationValue = SessionManager.Instance.CurrentProject.EnabledValidation;


                if (SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementDeviceID.HasValue && SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementDeviceID.Value > 0)
                    cmbMeasurementDevice.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementDeviceID.Value.ToString();
                else
                    cmbMeasurementDevice.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.MeasurementDeviceId.ToString();
                cmbMeasurementDevice_SelectedIndexChanged(null, null);

                if (SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key > 0)
                    cmbValidationChart.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key.ToString();
                else
                    cmbValidationChart.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key.ToString();
                cmbValidationChart_SelectedIndexChanged(null, null);

                if (SessionManager.Instance.CurrentProject.MeasurementSetting.MetricTargetPreset.Key > 0)
                    cmbMetricTargetPreset.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.MetricTargetPreset.Key.ToString();
            }

        }

        private void BindMeasurementDevice()
        {
            if (cmbMeasurementDevice.DataSource != null) return;

            DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            _presenterMeasurementSetting.GetMeasurementDevice();
            if (SessionManager.Instance.CurrentColorOrgRequest != null)
            {
                MeasurementDevices = MeasurementDevices.
                    Where(x => x.InstrumentTypeId == SessionManager.Instance.CurrentColorOrgRequest.MeasurementDeviceTypeId).
                    ToList();
            }
            if (!IsErrorOccured)
            {
                cmbMeasurementDevice.DataValueField = "MeasurementDeviceId";
                cmbMeasurementDevice.DataTextField = "SerialNumber";
                cmbMeasurementDevice.DataSource = MeasurementDevices;
                cmbMeasurementDevice.DataBind();
                cmbMeasurementDevice_SelectedIndexChanged(null, null);
            }
        }

        private void BindValidationChart()
        {
            MeasurementDeviceId = MeasurementDeviceSelectedValue;
            ChartDisplayType = Convert.ToInt32(EReferenceChartDisType.ValidationChart);
            _presenterMeasurementSetting.GetReferenceChartType();

            if (!IsErrorOccured)
            {
                cmbValidationChart.DataValueField = "ReferenceChartTypeID";
                cmbValidationChart.DataTextField = "ChartName";
                cmbValidationChart.DataSource = ReferenceChartType;
                cmbValidationChart.DataBind();
                cmbValidationChart.Items.Insert(0, new ListItem(GetResourceString(_recselectText), "0"));
            }
        }

        private void BindMetricTargetPreset()
        {
            _presenterMeasurementSetting.GetMetricTargetPreset();
            if (!IsErrorOccured)
            {
                cmbMetricTargetPreset.DataValueField = "MetricTargetPresetID";
                cmbMetricTargetPreset.DataTextField = "MetricTargetPresetName";
                cmbMetricTargetPreset.DataSource = MetricTargetPreset;
                cmbMetricTargetPreset.DataBind();
                cmbMetricTargetPreset.Items.Insert(0, new ListItem(GetResourceString(_recselectText), "0"));
            }
        }

        private int MeasurementDeviceSelectedValue
        {
            get
            {
                if (!string.IsNullOrEmpty(cmbMeasurementDevice.SelectedValue.Trim()))
                    return Convert.ToInt32(cmbMeasurementDevice.SelectedValue.Trim());
                return 0;
            }
        }

        private int ValidationChartSelectedValue
        {
            get
            {
                if (!string.IsNullOrEmpty(cmbValidationChart.SelectedValue.Trim()))
                    return Convert.ToInt32(cmbValidationChart.SelectedValue.Trim());
                return 0;
            }
        }
        private void DisplayValidationChartImage()
        {
            if (ValidationChartSelectedValue <= 0)
            {
                imgValidationChart.ImageUrl = ResolveUrl("../../Images/chart_preview.png");
            }
            else
            {
                imgValidationChart.ImageUrl = "ShowImage.aspx?id=" + ValidationChartSelectedValue;
            }
        }

        private bool EnableValidationValue
        {
            get
            {
                if (rbtnEnableValidation.SelectedIndex == 0)
                    return true;
                else if (rbtnEnableValidation.SelectedIndex == 1)
                    return false;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    rbtnEnableValidation.SelectedIndex = 0;
                    //------If validation, then check for required fields-----//
                    rfvValidationChart.ValidationGroup = "vgValidationSetup";
                    rfvMetricTargetPreset.ValidationGroup = "vgValidationSetup";
                }
                else
                {
                    rbtnEnableValidation.SelectedIndex = 1;
                    //------If no validation, then don't check for required fields-----//
                    rfvValidationChart.ValidationGroup = "a";
                    rfvMetricTargetPreset.ValidationGroup = "a";
                }
            }
        }
        #endregion

        #region Properties
        public bool IsXmfUser
        {
            get
            {
                return (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser));
            }
        }
        #endregion

        #region IViewMeasurementSetting members
        public List<Entity.Illuminant> Illumin { get; set; }

        public List<Entity.ObserverType> Observer { get; set; }

        public int DeviceId { get; set; }

        public List<Entity.MeasurementDevice> MeasurementDevices { get; set; }

        public int MeasurementDeviceId { get; set; }

        public List<Entity.ReferenceChartType> ReferenceChartType { get; set; }

        public short ReferenceChartTypeId { get; set; }

        public byte[] ReferenceChartTypeImage { get; set; }

        public int ProjectId { get; set; }

        public Entity.Project Project { get; set; }

        public int ChartDisplayType { get; set; }

        public List<Entity.CPSyncV.MetricTargetPreset> MetricTargetPreset { get; set; }

        #endregion

        protected void rbtnEnableValidation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnEnableValidation.SelectedIndex == 0)
            {
                //------If validation, then check for required fields-----//
                rfvValidationChart.ValidationGroup = "vgValidationSetup";
                rfvMetricTargetPreset.ValidationGroup = "vgValidationSetup";
            }
            else
            {
                //------If no validation, then don't check for required fields-----//
                rfvValidationChart.ValidationGroup = "a";
                rfvMetricTargetPreset.ValidationGroup = "a";

                cmbValidationChart.SelectedIndex = 0;
                cmbMetricTargetPreset.SelectedIndex = 0;
            }
        }
    }
}