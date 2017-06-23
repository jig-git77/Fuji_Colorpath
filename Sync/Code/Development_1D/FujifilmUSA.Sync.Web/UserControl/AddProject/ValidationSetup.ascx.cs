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
        private const string _vsMetricTargetPreset = "vsMetricTargetPreset";
        private const string _recDownloadChartNotAvailable = "downloadChartNotAvailable";

        public bool IsXmfNewProject
        {
            get
            {
                return (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser) &&
                        SessionManager.Instance.CurrentProject.ProjectId == 0);
            }
        }
        #endregion

        #region Page events
        protected override void OnInit(EventArgs e)
        {
            _presenterMeasurementSetting = new PresenterMeasurementSetting(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lnkbtnMeasurementSetup.Visible = IsXmfUser;
            if (IsXmfUser)
            {
                //trListHeader.Visible = false;
                EnableValidationValue = true; //In XMF it will be always true
                trEnableValidationOption.Visible = false;
                locValidationOptions.Text = GetResourceString("locValidationMetricsResource1.Text");
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
                lnkbtnMeasurementSetup.Visible = false;
                //trListHeader.Visible = true;
                btnValidationMeasure.Visible = false;
                locValidationOptions.Text = GetResourceString("locValidationOptionsResource1.Text");
            }

            if (ViewState[vsIsPostBack] == null)
            {
                BindMetricTargetPreset();
                BindMeasurementDevice();
                SetUVSettings();

                LoadProjectInfoFromSession();               
                ViewState[vsIsPostBack] = true;

                if (!SessionManager.Instance.CurrentDevice.IsValidation) //---If this device doesn't have permission for Validation, disable radiobuttonlist------//
                {
                    rbtnEnableValidation.Enabled = false;
                    panelValidationChart.Enabled = false;
                    panel_Metrictarget.Enabled = false;
                    lbl_Valimsg.Text = GetResourceString(_recnoValidationLicense);//"You are not currently licensed for Validation"
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
            SessionManager.Instance.CurrentProject.MeasurementSetting.VUVMode = short.Parse(cmbUVSettings.SelectedValue);
            SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementCount = short.Parse(cmbMeasurementCount.SelectedValue);
            SessionManager.Instance.CurrentProject.MeasurementSetting.VReadMode = (trReadMode.Visible == true ? cmbReadMode.SelectedValue : null);

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

        protected void lnkbtnMeasurementSetup_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.MeasurementSetup;
            Response.Redirect(string.Format("~/COAddProject.aspx?id={0}", Guid.NewGuid()));
        }

        protected void cmbMeasurementDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindValidationChart();
            SetUVSettings();
        }

        protected void cmbValidationChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayValidationChartImage();
        }

        protected void cmbMetricTargetPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewState[_vsMetricTargetPreset] != null)
            {
                txtMTPDesc.Text = ((List<Entity.CPSyncV.MetricTargetPreset>)ViewState[_vsMetricTargetPreset]).Where(c => c.MetricTargetPresetID == Convert.ToInt32(cmbMetricTargetPreset.SelectedValue)).Select(c => c.MetricTargetPresetDesc).FirstOrDefault();               
                BindMeasurementDevice(); //---Display Metric Target Presets based on selected Validation Chart
            }
        }
        protected void rbtnEnableValidation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnEnableValidation.SelectedIndex == 0)
            {
                //------If validation, then check for required fields-----//
                rfvValidationChart.ValidationGroup = "vgValidationSetup";
                rfvMetricTargetPreset.ValidationGroup = "vgValidationSetup";

                cmbMetricTargetPreset.Enabled = true;
                cmbMeasurementDevice.Enabled = true;
                cmbValidationChart.Enabled = true;
            }
            else
            {
                //------If no validation, then don't check for required fields-----//
                rfvValidationChart.ValidationGroup = "a";
                rfvMetricTargetPreset.ValidationGroup = "a";

                cmbMetricTargetPreset.Enabled = false;
                cmbMeasurementDevice.Enabled = false;
                cmbValidationChart.Enabled = false;

                cmbValidationChart.SelectedIndex = 0;
                cmbMetricTargetPreset.SelectedIndex = 0;
            }
        }

         protected void cmbReadMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IO)
                                                           || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IOPro2)) && cmbReadMode.SelectedValue == "Point")
            {
                BindMeasurementCount(true);
            }
            else
            {
                BindMeasurementCount(false);
            }
            trMeasCount.Visible = ((MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IO)
                                                || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IOPro2)) && cmbReadMode.SelectedValue == "Point");
        }

        protected void cmbUVSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IO)
                                               || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IOPro2))
            {
                BindReadMode();
            }
        }

        #endregion

        #region Methods
        private void LoadProjectInfoFromSession()
        {
            if (SessionManager.Instance.CurrentProject != null && SessionManager.Instance.CurrentProject.MeasurementSetting != null)
            {
                if (IsXmfUser)
                {
                    if (!SessionManager.Instance.CurrentDevice.IsValidation) //---If this device doesn't have permission for Validation------//
                    {
                        EnableValidationValue = false;
                    }
                    else
                    {
                        EnableValidationValue = true;
                    }
                }
                else
                    EnableValidationValue = SessionManager.Instance.CurrentProject.EnabledValidation;
               
                if (EnableValidationValue == true) //Make all disable selection enabled, if they have Validation enabled
                {
                    cmbMetricTargetPreset.Enabled = true;
                    cmbMeasurementDevice.Enabled = true;
                    cmbValidationChart.Enabled = true;
                }

                if (SessionManager.Instance.CurrentProject.MeasurementSetting.MetricTargetPreset.Key > 0)
                {
                    cmbMetricTargetPreset.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.MetricTargetPreset.Key.ToString();
                    if (ViewState[_vsMetricTargetPreset] != null)   //-----Put the Validation Metric Target Preset Description based on selected MTP---//
                    {
                        txtMTPDesc.Text = ((List<Entity.CPSyncV.MetricTargetPreset>)ViewState[_vsMetricTargetPreset]).Where(c => c.MetricTargetPresetID == Convert.ToInt32(cmbMetricTargetPreset.SelectedValue)).Select(c => c.MetricTargetPresetDesc).FirstOrDefault();
                    }
                }

                if (SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementDeviceID.HasValue && SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementDeviceID.Value > 0)
                    cmbMeasurementDevice.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementDeviceID.Value.ToString();
                else
                    cmbMeasurementDevice.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.MeasurementDeviceId.ToString();
                cmbMeasurementDevice_SelectedIndexChanged(null, null);

                if (SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key > 0)
                {
                    cmbValidationChart.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key.ToString();
                    cmbValidationChart_SelectedIndexChanged(null, null);
                }

                cmbUVSettings.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.VUVMode.ToString();
                BindReadMode();
                if (SessionManager.Instance.CurrentProject.MeasurementSetting.VReadMode != null)
                {
                    cmbReadMode.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.VReadMode.ToString();
                }

                //----Assign Read Mode selection box again to fill out Measurement Count box if "Point" is selected-----//
                cmbReadMode_SelectedIndexChanged(null, null);
                cmbMeasurementCount.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementCount.ToString();

                trMeasCount.Visible = ((MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IO)
                      || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IOPro2)) && cmbReadMode.SelectedValue == "Point");

            }

        }

        private void BindMeasurementDevice()
        {
            if (cmbMeasurementDevice.DataSource != null) return;

            DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            _presenterMeasurementSetting.GetMeasurementDevice();
            //if (SessionManager.Instance.CurrentColorOrgRequest != null)
            //{
            //    MeasurementDevices = MeasurementDevices.
            //        Where(x => x.InstrumentTypeId == SessionManager.Instance.CurrentColorOrgRequest.MeasurementDeviceTypeId).
            //        ToList();
            //}
            if (!IsErrorOccured)
            {
                cmbMeasurementDevice.DataValueField = "MeasurementDeviceId";
                cmbMeasurementDevice.DataTextField = "SerialNumber";
                cmbMeasurementDevice.DataSource = MeasurementDevices;
                cmbMeasurementDevice.DataBind();
                MeasurementDevicesView = MeasurementDevices.Select(x => new KeyValuePair<int, EInstrumentType>(
                                                                            x.MeasurementDeviceId,
                                                                            (EInstrumentType)
                                                                            Enum.Parse(typeof(EInstrumentType),
                                                                                       x.InstrumentTypeId.ToString()))).
                    ToDictionary(x => x.Key, x => x.Value);
                cmbMeasurementDevice_SelectedIndexChanged(null, null);
            }
        }

        private void BindValidationChart()
        {
            MeasurementDeviceId = MeasurementDeviceSelectedValue;
            ChartDisplayType = Convert.ToInt32(EReferenceChartDisType.ValidationChart);
            MetricTargetPresetID = Convert.ToInt32(cmbMetricTargetPreset.SelectedValue);
            _presenterMeasurementSetting.GetReferenceChartType();

            if (!IsErrorOccured)
            {
                cmbValidationChart.DataValueField = "ReferenceChartTypeID";
                cmbValidationChart.DataTextField = "ChartName";
                cmbValidationChart.DataSource = ReferenceChartType;
                cmbValidationChart.DataBind();
                cmbValidationChart.Items.Insert(0, new ListItem(GetResourceString(_recselectText), "0"));
                cmbValidationChart_SelectedIndexChanged(null, null);
            }
        }

        private void BindMetricTargetPreset()
        {
            if (cmbMetricTargetPreset.DataSource != null) return;

            _presenterMeasurementSetting.GetMetricTargetPreset_Verification();
            if (!IsErrorOccured)
            {
                cmbMetricTargetPreset.DataValueField = "MetricTargetPresetID";
                cmbMetricTargetPreset.DataTextField = "MetricTargetPresetName";
                ViewState[_vsMetricTargetPreset] = MetricTargetPreset;
                cmbMetricTargetPreset.DataSource = MetricTargetPreset;
                cmbMetricTargetPreset.DataBind();
                cmbMetricTargetPreset.Items.Insert(0, new ListItem(GetResourceString(_recselectText), "0"));
                cmbMetricTargetPreset_SelectedIndexChanged(null, null);
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

        private void SetUVSettings()
        {
            if (string.IsNullOrEmpty(cmbMeasurementDevice.SelectedValue) || MeasurementDevicesView == null)
            {
                trUVSettings.Visible = false;
                trReadMode.Visible = false;
                trMeasCount.Visible = false;
                return;
            }
            trUVSettings.Visible = (MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1Sis)
                                    || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1Pro2)
                                    || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IOPro2)
                                    || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.eXact) 
                                    || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1Sis2));
            trReadMode.Visible = (MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IO)
                                    || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IOPro2));

            trMeasCount.Visible = ((MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IO)
                                    || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IOPro2)) && cmbReadMode.SelectedValue == "Point");
            BindUVSettings();
        }

        private void BindUVSettings()
        {
            cmbUVSettings.DataValueField = "Key";
            cmbUVSettings.DataTextField = "Value";
            cmbUVSettings.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<EUVIncludedMode>();

            if (MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] ==
                                    (EInstrumentType.i1Sis))
            {
                cmbUVSettings.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<EUVIncludedMode>().Where(p => p.Value != "M1").ToDictionary(p => p.Key, p => p.Value);
            }
            cmbUVSettings.DataBind();
        }

        private void BindMeasurementCount(bool IsPro2Type)
        {
            cmbMeasurementCount.Items.Clear();
            if (IsPro2Type)
            {
                //----1 to 5 for Patch reading for i1-Pro2 instrument type
                cmbMeasurementCount.Items.Add(new ListItem("1", "1"));
                cmbMeasurementCount.Items.Add(new ListItem("2", "2"));
                cmbMeasurementCount.Items.Add(new ListItem("3", "3"));
                cmbMeasurementCount.Items.Add(new ListItem("4", "4"));
                cmbMeasurementCount.Items.Add(new ListItem("5", "5"));
            }
            else
                //-----0 for Strip reading for all other instruments type
                cmbMeasurementCount.Items.Add(new ListItem("0", "0"));          
        }

        private void BindReadMode()
        {
            cmbReadMode.Items.Clear();
            cmbReadMode.Items.Add(new ListItem("Strip", "Strip"));
            cmbReadMode.Items.Add(new ListItem("Point", "Point"));

            cmbReadMode_SelectedIndexChanged(null, null);
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

        private Dictionary<int, EInstrumentType> MeasurementDevicesView
        {
            get
            {
                if (ViewState["vsMeasurementDevice"] == null)
                    return new Dictionary<int, EInstrumentType>();
                else
                    return (Dictionary<int, EInstrumentType>)ViewState["vsMeasurementDevice"];
            }
            set
            {
                ViewState["vsMeasurementDevice"] = value;
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

        public ELicenseType LicenseType
        {
            get
            {
                return SessionManager.Instance.CurrentProject.LicenseType;
            }
        }
        public int ProjectTypeID
        {
            get
            {
                return SessionManager.Instance.CurrentProject.ProjectTypeId;
            }
        }
        public int MetricTargetPresetID { get; set; }
        public Dictionary<int, string> ReTargetProjectList { get; set; }
        public byte[] ChartPreview { get; set; }
        #endregion

        protected void lbtnDownloadChart_Click(object sender, EventArgs e)
        {
            string ChartName = "";
            if (ValidationChartSelectedValue > 0)
            {
                ReferenceChartTypeId = Convert.ToInt16(ValidationChartSelectedValue);
                ChartName = cmbValidationChart.SelectedItem.Text;

                _presenterMeasurementSetting.GetChartPreview();
                if (!IsErrorOccured)
                {
                    if (ChartPreview != null)
                        Utilities.DownloadFile(ChartPreview, string.Format("{0}.pdf", ChartName), ".pdf");
                    else
                        fujiStatusbar.DisplayMessage(GetResourceString(_recDownloadChartNotAvailable), true);
                }
            }
            else
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recDownloadChartNotAvailable), true);
            }
        }

    }
}
