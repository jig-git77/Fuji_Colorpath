using System;
using System.Linq;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Web.UI.WebControls;
using System.IO;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProfilerOptions : BaseControl, IViewDeviceFamilyProfilerOption, IViewMeasurementSetting
    {
        #region "Declaration"
        public bool IsXmfNewProject
        {
            get
            {
                return (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser) &&
                        SessionManager.Instance.CurrentProject.ProjectId == 0);
            }
        }

        public string ProfilerOptions_JavascriptAllownumbers
        {
            get
            {
                return ResourceManager.GetResourceMessage("ProfilerOptions_JavascriptAllownumbers");
            }
        }
        public string ProfilerOptions_JavascriptTVIRange
        {
            get
            {
                return ResourceManager.GetResourceMessage("ProfilerOptions_JavascriptTVIRange");
            }
        }
        public string ProfilerOptions_JavascriptDensityRange
        {
            get
            {
                return ResourceManager.GetResourceMessage("ProfilerOptions_JavascriptDensityRange");
            }
        }
        private const string _vsDeviceFamilyProfilerOption = "vsDeviceFamilyProfilerOption";
        private const string _vsDeviceFamilyMetricPresetOption = "vsDeviceFamilyMetricPresetOption";
        private const string _vsProfileRendering = "vsProfileRendering";
        private const string _vsMetricTargetPreset = "vsMetricTargetPreset";
        private PresenterDeviceFamilyProfilerOption _presenterDeviceFamilyProfilerOption;
        private PresenterMeasurementSetting _presenterMeasurementSetting;
        private const string _recSelectText = "selectText";
        private const string _recNoCertifiedPrintCondition = "noCertifiedPrintCondition";

        #endregion

        public ProfilerOptions()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterDeviceFamilyProfilerOption = new PresenterDeviceFamilyProfilerOption(this);
            _presenterMeasurementSetting = new PresenterMeasurementSetting(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lnkbtnTargetColorSpace.Visible = IsXmfNewProject;
            PanelOptionalRetarget.GroupingText = "<b>" + GetResourceString("PanelOptionalRetarget") + "</b>";

            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
            {
                if (ViewState[_vsMetricTargetPreset] == null)
                {
                    //Sync1D
                    tbOptionPreset.Visible = false;
                    tbMetricPreset.Visible = true;
                    BindMetricPresets();
                    GetPrintStandardTVI();
                    GetPrintStandardDensity();
                    //BindReTargetList();
                    BindDevices();

                    if (SessionManager.Instance.CurrentProject.PrintCondition.PrintingType == EPrintingTypes.Flexo) //----If Sync1D project's printing type is Flexo then only show Hue angle targets panel
                    {
                        GetPrintStandardHueAngle();
                        dvHueAngleTargets.Visible = true;
                    }
                    LoadProjectInfoFromSession();

                    if (ProjectTypeID == 1) //-----Option only for G7 Sync1D projects
                    {
                        Div_LinearizationType.Visible = true;
                    }
                }
            }
            else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLPPlus)
            {
                if (ViewState[_vsMetricTargetPreset] == null && ViewState[_vsDeviceFamilyProfilerOption] == null)
                {
                    tbMetricPreset.Visible = true;
                    PanelOptionalRetarget.Visible = false;
                    tbOptionPreset.Visible = true;
                    spanMetricSetting.Visible = true;
                    trAdvancedOptions.Visible = false;
                    RadToolTipTolerancesDlpPlus.Visible = true;
                    BindProfileRenderings();
                    BindPresets();
                    BindMetricPresets();

                    LoadProjectInfoFromSession();
                }
            }
            else
            {
                if (ViewState[_vsDeviceFamilyProfilerOption] == null)
                {
                    tbOptionPreset.Visible = true;
                    tbMetricPreset.Visible = false;
                    BindProfileRenderings();
                    BindPresets();

                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync4DCertification) //-----If Sync4D Certification Type, disable Preset Selection box
                    {
                        cmbSelectedPresets.Enabled = false;
                    }
                    LoadProjectInfoFromSession();
                }
            }
        }
        #endregion

        #region Method
        private void BindProfileRenderings()
        {
            if (ViewState[_vsProfileRendering] == null)
            {
                cmbRendering.DataValueField = "Key";
                cmbRendering.DataTextField = "Value";
                cmbRendering.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<EProfileRendering>();
                cmbRendering.DataBind();
            }
        }

        private void BindPresets()
        {

            if (ViewState[_vsDeviceFamilyProfilerOption] == null || !SessionManager.Instance.CurrentProject.PrintCondition.IsCustomProfilerOption)
            {
                DeviceFamilyId = SessionManager.Instance.CurrentDevice.DeviceFamily.DeviceFamilyId;
                LicenseTypeId = (short)SessionManager.Instance.CurrentProject.LicenseType;
                UserID = SessionManager.Instance.CurrentUser.UserId;
                IsCustomPreset = SessionManager.Instance.CurrentProject.PrintCondition.IsCustomProfilerOption;
                _presenterDeviceFamilyProfilerOption.GetDeviceFamilyProfilerOption();
                if (!IsErrorOccured)
                {

                    cmbSelectedPresets.DataValueField = "DeviceFamilyProfilerOptionID";
                    cmbSelectedPresets.DataTextField = "ProfilerOptionsName";
                    ViewState[_vsDeviceFamilyProfilerOption] = ProfileOption;
                    cmbSelectedPresets.DataSource = ViewState[_vsDeviceFamilyProfilerOption];
                    if (ProfileOption != null)
                    {
                        cmbSelectedPresets.SelectedIndex = 0;
                    }
                    cmbSelectedPresets.DataBind();
                    cmbSelectedPresets_SelectedIndexChanged(null, null);
                }
            }
            else
            {
                trSelectPreset.Visible = true;
                cmbSelectedPresets.DataValueField = "DeviceFamilyProfilerOptionID";
                cmbSelectedPresets.DataTextField = "ProfilerOptionsName";
                cmbSelectedPresets.DataSource = ViewState[_vsDeviceFamilyProfilerOption];
                cmbSelectedPresets.DataBind();
                cmbSelectedPresets_SelectedIndexChanged(null, null);
            }

        }

        private void LoadProjectInfoFromSession()
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
            {
                //Sync1D
                if (SessionManager.Instance.CurrentProject != null && SessionManager.Instance.CurrentProject.MetricTargetPresetId != 0)
                {
                    ddlMetricPresets.SelectedValue = SessionManager.Instance.CurrentProject.MetricTargetPresetId.ToString();
                    ddlMetricPresets_SelectedIndexChanged(null, null);
                }
                if (SessionManager.Instance.CurrentProject != null)
                {
                    if (SessionManager.Instance.CurrentProject.ReTargetProjectID != 0)
                    {
                        cmbDevice.SelectedValue = Convert.ToString(SessionManager.Instance.CurrentProject.ReTargetProjectDeviceID);
                        cmbDevice_SelectedIndexChanged(null, null);
                        ddlReTargetList.SelectedValue = SessionManager.Instance.CurrentProject.ReTargetProjectID.ToString();
                        ddlReTargetList_SelectedIndexChanged(null, null);
                    }
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0) //Once project is saved, Project type selection is disable
                    {
                        ddlReTargetList.Enabled = false;
                        cmbDevice.Enabled = false;
                    }

                    if (SessionManager.Instance.CurrentProject.ProjectOption != null && SessionManager.Instance.CurrentProject.ProjectOption.Count > 0) //Profiler options value set from session for existing Sync 1D projects
                    {
                        int indexLType = SessionManager.Instance.CurrentProject.ProjectOption.FindIndex(item => item.ProfilerOptionId == 40 && item.ProfilerOptionValue == "TR015");
                        if (indexLType > 0) //If LinearizationType is selected, then send LR015
                        {
                            chkLinearizationType.Checked = true;
                        }
                        else //else send G7
                        {
                            chkLinearizationType.Checked = false;
                        }
                        int indexMCorrection = SessionManager.Instance.CurrentProject.ProjectOption.FindIndex(item => item.ProfilerOptionId == 7);
                        if (indexMCorrection > 0)
                        {
                            cmbMeaCorrection.SelectedValue = SessionManager.Instance.CurrentProject.ProjectOption[indexMCorrection].ProfilerOptionValue;
                        }
                    }
                }
            }
            else
            {                
                if (SessionManager.Instance.CurrentProject != null && SessionManager.Instance.CurrentProject.DeviceFamilyProfilerOptionId != 0)
                {                    
                    cmbSelectedPresets.SelectedValue = SessionManager.Instance.CurrentProject.DeviceFamilyProfilerOptionId.ToString();
                    cmbSelectedPresets_SelectedIndexChanged(null, null);

                    if (SessionManager.Instance.CurrentProject.ProjectOption != null && SessionManager.Instance.CurrentProject.ProjectOption.Count > 0) //Profiler options value set from session for existing Sync 4D projects
                    {
                        int indexMode = SessionManager.Instance.CurrentProject.ProjectOption.FindIndex(item => item.ProfilerOptionId == 20);
                        if (indexMode > 0)
                        {
                            txtMode.Text = SessionManager.Instance.CurrentProject.ProjectOption[indexMode].ProfilerOptionValue;
                            //rdsMode.Value = Convert.ToDecimal(SessionManager.Instance.CurrentProject.ProjectOption[indexMode].ProfilerOptionValue);   //---Comment out right now to make it non- editable advanced options
                        }
                        int indexBlackStart = SessionManager.Instance.CurrentProject.ProjectOption.FindIndex(item => item.ProfilerOptionId == 21);
                        if (indexBlackStart > 0)
                        {
                            txtBlackStart.Text = SessionManager.Instance.CurrentProject.ProjectOption[indexBlackStart].ProfilerOptionValue;
                            rdsBlackStart.Value = Convert.ToDecimal(SessionManager.Instance.CurrentProject.ProjectOption[indexBlackStart].ProfilerOptionValue);
                        }
                        int indexBlackWidth = SessionManager.Instance.CurrentProject.ProjectOption.FindIndex(item => item.ProfilerOptionId == 22);
                        if (indexBlackWidth > 0)
                        {
                            txtBlackWidth.Text = SessionManager.Instance.CurrentProject.ProjectOption[indexBlackWidth].ProfilerOptionValue;
                            rdsBlackWidth.Value = Convert.ToDecimal(SessionManager.Instance.CurrentProject.ProjectOption[indexBlackWidth].ProfilerOptionValue);
                        }
                        int indexInkTotal = SessionManager.Instance.CurrentProject.ProjectOption.FindIndex(item => item.ProfilerOptionId == 19);
                        if (indexInkTotal > 0)
                        {
                            txtInkTotal.Text = SessionManager.Instance.CurrentProject.ProjectOption[indexInkTotal].ProfilerOptionValue;
                            //rdsInkTotal.Value = Convert.ToDecimal(SessionManager.Instance.CurrentProject.ProjectOption[indexInkTotal].ProfilerOptionValue);   //---Comment out right now to make it non- editable advanced options
                        }
                        int indexInkMax = SessionManager.Instance.CurrentProject.ProjectOption.FindIndex(item => item.ProfilerOptionId == 18);
                        if (indexInkMax > 0)
                        {
                            txtBlackPointCustom.Text = SessionManager.Instance.CurrentProject.ProjectOption[indexInkMax].ProfilerOptionValue;
                        }
                    }
                }
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLPPlus) //If only Sync4D project and selected branch has license allowed for custom presets, then only display option for custom
                {
                    if (SessionManager.Instance.CurrentDevice.License.IsCustomProfOptions == true)
                    {
                        trPresetoptionType.Visible = true;
                    }
                }
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLPMultiColor) //If only DLPMultiColor project and selected branch has license allowed for custom presets, then only display option for custom
                {
                    if (SessionManager.Instance.CurrentDevice.License.IsCustomProfOptions == true)
                    {
                        trPresetoptionType.Visible = true;
                    }
                }
                if (SessionManager.Instance.CurrentProject.PrintCondition != null)
                {
                    rdBtnPresetType.SelectedValue = SessionManager.Instance.CurrentProject.PrintCondition.IsCustomProfilerOption.ToString();
                    if (SessionManager.Instance.CurrentProject.PrintCondition.IsCustomProfilerOption)
                    {
                        trPresetUplod.Visible = true;
                    }
                }
            }
        }

        private void BindMetricPresets()
        {
            if (ViewState[_vsMetricTargetPreset] == null)
            {
                PrintingTypeId = (short)SessionManager.Instance.CurrentProject.PrintCondition.PrintingType;
                _presenterMeasurementSetting.GetMetricTargetPreset();
                if (!IsErrorOccured)
                {
                    ddlMetricPresets.DataValueField = "MetricTargetPresetID";
                    ddlMetricPresets.DataTextField = "MetricTargetPresetName";
                    ViewState[_vsMetricTargetPreset] = MetricTargetPreset;
                    ddlMetricPresets.DataSource = MetricTargetPreset;
                    ddlMetricPresets.DataBind();
                    ddlMetricPresets_SelectedIndexChanged(null, null);
                }
            }
        }

        private void BindTolerances()
        {
            MetricTargetPresetId = Convert.ToInt32(ddlMetricPresets.SelectedValue);
            LicenseTypeId = (short)SessionManager.Instance.CurrentProject.LicenseType;
            _presenterDeviceFamilyProfilerOption.GetMetricSumTargetValues();
            if (!IsErrorOccured)
            {
                lvTolerances.DataSource = MetricSumTargetValues;
                lvTolerances.DataBind();
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLPPlus)
                {
                    lvTolerancesDlpPlus.DataSource = MetricSumTargetValues;
                    lvTolerancesDlpPlus.DataBind();
                }
            }
        }

        private void GetPrintStandardTVI()
        {
            PrintStandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeID;

            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
            {
                if (SessionManager.Instance.CurrentProject.PrintStandardTVI == null || SessionManager.Instance.CurrentProject.PrintStandardTVI.Count == 0)
                {
                    _presenterDeviceFamilyProfilerOption.GetPrintStandardTVI();

                    if (!IsErrorOccured)
                    {
                        BindPrintStandardTVI(PrintStandardTVI);
                    }
                }
                else // Load from session for existing project
                {
                    BindPrintStandardTVI(SessionManager.Instance.CurrentProject.PrintStandardTVI);
                }
            }
        }

        private void BindPrintStandardTVI(List<Entity.CPSync1D.PrintStandardTVI> PrintStandardTVI)
        {
            foreach (var item in PrintStandardTVI)
            {
                switch (item.PatchType.Type.ToString())
                {
                    //--Cyan for all CMY patches----//
                    case "Cyan10":
                    case "Cyan_10":
                        txtTVI10CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan20":
                    case "Cyan_20":
                        txtTVI20CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan25":
                    case "Cyan_25":
                    case "Cyan Quarter Tone":
                    case "Cyan_Quarter_Tone":
                        txtTVI25CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan30":
                    case "Cyan_30":
                        txtTVI30CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan40":
                    case "Cyan_40":
                        txtTVI40CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan50":
                    case "Cyan_50":
                    case "Cyan Half Tone":
                    case "Cyan_Half_Tone":
                        txtTVI50CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan60":
                    case "Cyan_60":
                        txtTVI60CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan70":
                    case "Cyan_70":
                        txtTVI70CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan75":
                    case "Cyan_75":
                    case "Cyan Three Quarter Tone":
                    case "Cyan_Three_Quarter_Tone":
                        txtTVI75CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan80":
                    case "Cyan_80":
                        txtTVI80CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan90":
                    case "Cyan_90":
                        txtTVI90CMY.Text = item.TVI.ToString();
                        break;
                    //----For Black patches----//
                    case "Black10":
                    case "Black_10":
                        txtTVI10K.Text = item.TVI.ToString();
                        break;
                    case "Black20":
                    case "Black_20":
                        txtTVI20K.Text = item.TVI.ToString();
                        break;
                    case "Black25":
                    case "Black_25":
                    case "Black Quarter Tone":
                    case "Black_Quarter_Tone":
                        txtTVI25K.Text = item.TVI.ToString();
                        break;
                    case "Black30":
                    case "Black_30":
                        txtTVI30K.Text = item.TVI.ToString();
                        break;
                    case "Black40":
                    case "Black_40":
                        txtTVI40K.Text = item.TVI.ToString();
                        break;
                    case "Black50":
                    case "Black_50":
                    case "Black Half Tone":
                    case "Black_Half_Tone":
                        txtTVI50K.Text = item.TVI.ToString();
                        break;
                    case "Black60":
                    case "Black_60":
                        txtTVI60K.Text = item.TVI.ToString();
                        break;
                    case "Black70":
                    case "Black_70":
                        txtTVI70K.Text = item.TVI.ToString();
                        break;
                    case "Black75":
                    case "Black_75":
                    case "Black Three Quarter Tone":
                    case "Black_Three_Quarter_Tone":
                        txtTVI75K.Text = item.TVI.ToString();
                        break;
                    case "Black80":
                    case "Black_80":
                        txtTVI80K.Text = item.TVI.ToString();
                        break;
                    case "Black90":
                    case "Black_90":
                        txtTVI90K.Text = item.TVI.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private void GetPrintStandardDensity()
        {
            PrintStandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeID;

            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
            {
                if (SessionManager.Instance.CurrentProject.ProjectTargetDensity == null || SessionManager.Instance.CurrentProject.ProjectTargetDensity.Count == 0)
                {
                    _presenterDeviceFamilyProfilerOption.GetPrintStandardDensity();

                    if (!IsErrorOccured)
                    {
                        BindPrintStandardDensity(ProjectTargetDensity);
                    }
                }
                else // Load from session for existing project
                {
                    BindPrintStandardDensity(SessionManager.Instance.CurrentProject.ProjectTargetDensity);
                }
            }
        }

        private void BindPrintStandardDensity(List<Entity.CPSync1D.ProjectTargetDensity> ProjectTargetDensity)
        {
            foreach (var item in ProjectTargetDensity)
            {
                switch (item.PatchType.Type.ToString())
                {
                    case "Cyan":
                        txtDensityC.Text = item.Target.ToString();
                        break;
                    case "Magenta":
                        txtDensityM.Text = item.Target.ToString();
                        break;
                    case "Yellow":
                        txtDensityY.Text = item.Target.ToString();
                        break;
                    case "Black":
                        txtDensityK.Text = item.Target.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private void GetPrintStandardHueAngle()
        {
            PrintStandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeID;

            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
            {
                if (SessionManager.Instance.CurrentProject.ProjectTargetHueAngle == null || SessionManager.Instance.CurrentProject.ProjectTargetHueAngle.Count == 0)
                {
                    _presenterDeviceFamilyProfilerOption.GetPrintStandardHueAngle();

                    if (!IsErrorOccured)
                    {
                        BindPrintStandardHueAngle(ProjectTargetHueAngle);
                    }
                }
                else // Load from session for existing project
                {
                    BindPrintStandardHueAngle(SessionManager.Instance.CurrentProject.ProjectTargetHueAngle);
                }
            }
        }

        private void BindPrintStandardHueAngle(List<Entity.CPSync1D.ProjectTargetHueAngle> ProjectTargetHueAngle)
        {
            foreach (var item in ProjectTargetHueAngle)
            {
                switch (item.PatchType.Type.ToString())
                {
                    case "Cyan":
                        txtHue_C.Text = item.Target.ToString();
                        break;
                    case "Magenta":
                        txtHue_M.Text = item.Target.ToString();
                        break;
                    case "Yellow":
                        txtHue_Y.Text = item.Target.ToString();
                        break;
                    case "Overprint_Red":
                        txtHue_R.Text = item.Target.ToString();
                        break;
                    case "Overprint_Green":
                        txtHue_G.Text = item.Target.ToString();
                        break;
                    case "Overprint_Blue":
                        txtHue_B.Text = item.Target.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private void BindReTargetList()
        {
            DeviceId = Convert.ToInt32(cmbDevice.SelectedValue.ToString());

            _presenterMeasurementSetting.GetReTargetProjectList();
            if (!IsErrorOccured)
            {
                if (ReTargetProjectList.Count > 0)
                {
                    ddlReTargetList.DataValueField = "Key";
                    ddlReTargetList.DataTextField = "Value";
                    ddlReTargetList.DataSource = ReTargetProjectList;
                    ddlReTargetList.DataBind();
                    ddlReTargetList.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), "0"));
                }
                else
                {
                    ddlReTargetList.Items.Clear();
                    ddlReTargetList.Items.Insert(0, new ListItem(GetResourceString(_recNoCertifiedPrintCondition), "0"));
                }
            }
            divRetarget.Visible = true;
        }

        private void BindDevices()
        {
            CompanyID = SessionManager.Instance.SelectedCompany;
            BranchID = SessionManager.Instance.SelectedBranch;
            _presenterDeviceFamilyProfilerOption.DeviceListCertified1DProjects();
            if (!IsErrorOccured)
            {
                cmbDevice.DataValueField = "DeviceId";
                cmbDevice.DataTextField = "DeviceName";
                cmbDevice.DataSource = Devices;
                cmbDevice.DataBind();
                cmbDevice.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), "0"));
            }
            divDevice.Visible = true;
        }

        private void BindRetargetProjectDetails()
        {
            RetargetProjectID = Convert.ToInt32(ddlReTargetList.SelectedValue.ToString());
            _presenterDeviceFamilyProfilerOption.GetReTargetProjectDetails();
            if (!IsErrorOccured)
            {
                lblPrintStandardTypeName.Text = RetargetPrintStandardTypeName;
                lblMetricTargetPresetName.Text = RetargetMetricTargetPresetName;
            }
            divRetargetDetails.Visible = true;
        }

        private void AddProfilerOptionSettings(List<ProfilerOptionPreset> lstProfilerOptionPreset)
        {
            List<DeviceFamilyProfilerOption> DeviceOptionList = new List<DeviceFamilyProfilerOption>();

            DeviceFamilyProfilerOption = new DeviceFamilyProfilerOption();
            DeviceFamilyProfilerOption.DeviceFamilyProfilerOptionId = -1;
            DeviceFamilyProfilerOption.DeviceFamilyId = 1;
            DeviceFamilyProfilerOption.ProfilerOptionsName = "Custom";
            DeviceFamilyProfilerOption.ProfilerOptionsDesc = "Custom Description";
            DeviceFamilyProfilerOption.ProjectTypeID = 3;
            DeviceFamilyProfilerOption.UserID = SessionManager.Instance.CurrentUser.UserId;


            var rows = 0;
            bool bDestProfileType = false;
            bool bDevProfileType = false;
            bool bOptProfileType = false;

            #region Validation for ProfileTypes
            List<ProfilerOptionPreset> lstFilter = new List<ProfilerOptionPreset>();
            lstFilter = lstProfilerOptionPreset.FindAll(item => item.ProfileTypeId == (short)EProfileType.DestinationProfile);
            if (lstFilter.Count > 0)
                bDestProfileType = true;
            lstFilter = lstProfilerOptionPreset.FindAll(item => item.ProfileTypeId == (short)EProfileType.DeviceLinkProfile);
            if (lstFilter.Count > 0)
                bDevProfileType = true;
            lstFilter = lstProfilerOptionPreset.FindAll(item => item.ProfileTypeId == (short)EProfileType.OptimizeDlp);
            if (lstFilter.Count > 0)
                bOptProfileType = true;
            #endregion

            if (!(bDestProfileType && bDevProfileType && bOptProfileType))
                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_CSV_ProfilerOptionsPreset"), rows));
            else
            {
                DeviceFamilyProfilerOption.ProfilerOptionPresets = (lstProfilerOptionPreset.
                                                            Select(x => new ProfilerOptionPreset()
                                                            {
                                                                ProfileTypeId = x.ProfileTypeId,
                                                                ProfilerOption = x.ProfilerOption,
                                                                ProfilerOptionValue = x.ProfilerOptionValue
                                                            }).ToList());
            }

            DeviceOptionList.Add(DeviceFamilyProfilerOption);
            UserID = SessionManager.Instance.CurrentUser.UserId;
            DeviceFamilyProfilerOption = DeviceFamilyProfilerOption;
            _presenterDeviceFamilyProfilerOption.InsertCustomDeviceFamilyProfilerOption();
            ViewState[_vsDeviceFamilyProfilerOption] = DeviceOptionList;

            BindPresets();
        }


        #endregion

        #region Control Events
        protected void cmbSelectedPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAdvancedOptionsControls();
            if (ViewState[_vsDeviceFamilyProfilerOption] != null)
            {
                var familyOption =
                    ((List<DeviceFamilyProfilerOption>)ViewState[_vsDeviceFamilyProfilerOption]).Single(
                        x => x.DeviceFamilyProfilerOptionId.ToString() == cmbSelectedPresets.SelectedValue);

                //Start:-----If CustomSourceProfile or ReProfile Custom Source Type, then pass different set of preset options
                int MainProfileTypeID = 0;
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.CustomSourceProfile || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.ReProfileCustomSource)
                {
                    MainProfileTypeID = Convert.ToInt32(EProfileType.DestinationProfile);
                }
                else
                {
                    MainProfileTypeID = Convert.ToInt32(EProfileType.DeviceLinkProfile);
                }
                //End

                txtProfileDescription.Text = familyOption.ProfilerOptionsDesc;
                familyOption.
                    ProfilerOptionPresets.Where(x => x.ProfileTypeId == MainProfileTypeID).ToList().
                    ForEach(x =>
                                {
                                    var option = (EProfilerOption)Enum.Parse(typeof(EProfilerOption), x.ProfilerOption.ProfilerOptionId.ToString());
                                    switch (option)
                                    {
                                        case EProfilerOption.Rendering:
                                            cmbRendering.SelectedValue = x.ProfilerOptionValue;
                                            break;
                                        case EProfilerOption.InkMax:
                                            txtBlackPointCustom.Text = x.ProfilerOptionValue;
                                            break;
                                        case EProfilerOption.InkTotal:
                                            //rdsInkTotal.Value = Convert.ToDecimal(x.ProfilerOptionValue);     //---Comment out right now to make it non- editable advanced options
                                            txtInkTotal.Text = x.ProfilerOptionValue;
                                            break;
                                        case EProfilerOption.BlackGeneration:
                                            //rdsMode.Value = Convert.ToDecimal(x.ProfilerOptionValue);     //---Comment out right now to make it non- editable advanced options
                                            txtMode.Text = x.ProfilerOptionValue;
                                            break;
                                        case EProfilerOption.BlackStart:
                                            rdsBlackStart.Value = Convert.ToDecimal(x.ProfilerOptionValue);
                                            txtBlackStart.Text = x.ProfilerOptionValue;
                                            break;
                                        case EProfilerOption.BlackWidth:
                                            rdsBlackWidth.Value = Convert.ToDecimal(x.ProfilerOptionValue);
                                            txtBlackWidth.Text = x.ProfilerOptionValue;
                                            break;
                                        case EProfilerOption.DLExceptions:
                                            #region Profiler Exceptions

                                            var exception = (EProfilerException)Enum.Parse(typeof(EProfilerException), x.ProfilerOptionValue);
                                            if (exception > 0)
                                            {
                                                if ((exception & EProfilerException.Triplex) == EProfilerException.Triplex)
                                                {
                                                    chkTriplex.Checked = true;
                                                }
                                                if ((exception & EProfilerException.Duplex) == EProfilerException.Duplex)
                                                {
                                                    chkDuplex.Checked = true;
                                                }
                                                if ((exception & EProfilerException.PureGray) == EProfilerException.PureGray)
                                                {
                                                    chkGray.Checked = true;
                                                }
                                                if ((exception & EProfilerException.PureBlack) == EProfilerException.PureBlack)
                                                {
                                                    chk100Black.Checked = true;
                                                }
                                                if ((exception & EProfilerException.PureCMY) == EProfilerException.PureCMY)
                                                {
                                                    chk100CMY.Checked = true;
                                                }
                                                if ((exception & EProfilerException.PureRGB) == EProfilerException.PureRGB)
                                                {
                                                    chk100RGB.Checked = true;
                                                }
                                                if ((exception & EProfilerException.MaxCMY) == EProfilerException.MaxCMY)
                                                {
                                                    chkMaxCMY.Checked = true;
                                                }
                                                if ((exception & EProfilerException.MaxRGB) == EProfilerException.MaxRGB)
                                                {
                                                    chkMaxRGB.Checked = true;
                                                }
                                                if ((exception & EProfilerException.PurePrimaries) == EProfilerException.PurePrimaries)
                                                {
                                                    chkPrimaries.Checked = true;
                                                    chkPC.Checked = true;
                                                    chkPM.Checked = true;
                                                    chkPY.Checked = true;
                                                }
                                                if ((exception & EProfilerException.PureSecondaries) == EProfilerException.PureSecondaries)
                                                {
                                                    chkSecondaries.Checked = true;
                                                    chkSCM.Checked = true;
                                                    chkSCY.Checked = true;
                                                    chkSMY.Checked = true;
                                                }
                                                if ((exception & EProfilerException.BlackOverprint) == EProfilerException.BlackOverprint)
                                                {
                                                    chkBlackOverPrint.Checked = true;
                                                }
                                                if ((exception & EProfilerException.ClipToEdge) == EProfilerException.ClipToEdge)
                                                {
                                                    chkBorderClipping.Checked = true;
                                                }
                                                if ((exception & EProfilerException.PureWhite) == EProfilerException.PureWhite)
                                                {
                                                    chkWhite.Checked = true;
                                                }
                                                if ((exception & EProfilerException.FullBlack) == EProfilerException.FullBlack)
                                                {
                                                    chk400Black.Checked = true;
                                                }
                                            }
                                            break;

                                        #endregion
                                        case EProfilerOption.DLExceptionRange:
                                            rdsRange.Value = Convert.ToDecimal(x.ProfilerOptionValue);
                                            txtRange.Text = x.ProfilerOptionValue;
                                            break;
                                        case EProfilerOption.ProfileType:
                                        case EProfilerOption.ProfileSize:
                                        case EProfilerOption.ProfileFormat:
                                        case EProfilerOption.PerceptualCompression2:
                                        case EProfilerOption.PerceptualCompressionMergeFactor:
                                        case EProfilerOption.MeasurementCorrection:
                                        case EProfilerOption.BrightenerCorrection:
                                        case EProfilerOption.AdaptionType:
                                        case EProfilerOption.WhiteXYZCorrection:
                                        case EProfilerOption.GridA2B0:
                                        case EProfilerOption.GridA2B1:
                                        case EProfilerOption.GridA2B2:
                                        case EProfilerOption.GridB2A0:
                                        case EProfilerOption.GridB2A1:
                                        case EProfilerOption.GridB2A2:
                                        case EProfilerOption.GridGamut:
                                        case EProfilerOption.DLBypassBlack:
                                        case EProfilerOption.DLOptimizationLimit:
                                        case EProfilerOption.DLOptimizeExceptions:
                                        case EProfilerOption.DLOptimizeInkRange:
                                        case EProfilerOption.DLApplyInkSave:
                                        case EProfilerOption.DLReplaceCMY:
                                        case EProfilerOption.DLAddBlack:
                                        case EProfilerOption.DLAddCMY:
                                        case EProfilerOption.DLType:
                                        case EProfilerOption.Flags:
                                        case EProfilerOption.Attributes0:
                                        case EProfilerOption.Attributes1:
                                        case EProfilerOption.SaveProfilerInfo:
                                        case EProfilerOption.SaveProfilerData:
                                        case EProfilerOption.Version:
                                            break;
                                    }
                                });
            }
        }

        protected void ddlMetricPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearMetricPresetAdvancedOptionsControls();
            if (ViewState[_vsMetricTargetPreset] != null)
            {
                txtMPresetDescription.Text = ((List<Entity.CPSyncV.MetricTargetPreset>)ViewState[_vsMetricTargetPreset]).Where(c => c.MetricTargetPresetID == Convert.ToInt32(ddlMetricPresets.SelectedValue)).Select(c => c.MetricTargetPresetDesc).FirstOrDefault();
                BindTolerances();
            }
        }

        protected void lnkbtnTargetColorSpace_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.TargetColorSpace;
            Response.Redirect(string.Format("~/COAddProject.aspx?id={0}", Guid.NewGuid()));
        }

        protected void lnkbtnProfilerOptions_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
            {
                SessionManager.Instance.CurrentProject.IsWeightedTolerances = chkWeightedTolerances.Checked;
                SessionManager.Instance.CurrentProject.MetricTargetPresetId = Convert.ToInt32(ddlMetricPresets.SelectedValue);
                SessionManager.Instance.CurrentProject.PrintStandardTVI = GetPrintStandardTVIList();
                SessionManager.Instance.CurrentProject.ProjectTargetDensity = GetProjectTargetDensityList();
                SessionManager.Instance.CurrentProject.ProjectTargetHueAngle = GetProjectTargetHueAngleList();

                //if (ddlReTargetList.SelectedValue.ToString() != "0")
                SessionManager.Instance.CurrentProject.ReTargetProjectID = ((ddlReTargetList.Items.Count == 0) ? 0 : Convert.ToInt32(ddlReTargetList.SelectedValue));

                SessionManager.Instance.CurrentProject.ProjectOption = new List<ProjectOption>();

                SessionManager.Instance.CurrentProject.ProjectOption.Add(new ProjectOption()
                {
                    ProfileTypeId = 0,
                    ProfilerOptionId = 1,
                    ProfilerOptionValue = "LinearizationDeviceLink",
                });
                SessionManager.Instance.CurrentProject.ProjectOption.Add(new ProjectOption()
                {
                    ProfileTypeId = 0,
                    ProfilerOptionId = 7,
                    ProfilerOptionValue = cmbMeaCorrection.SelectedValue,
                });
                if (ProjectTypeID == 1) //-----Option only for G7 Sync1D projects
                {
                    SessionManager.Instance.CurrentProject.ProjectOption.Add(new ProjectOption()
                    {
                        ProfileTypeId = 0,
                        ProfilerOptionId = 40,
                        ProfilerOptionValue = (chkLinearizationType.Checked) ? "TR015" : "G7",
                    });

                    //if (cmbDevice.SelectedValue.ToString() != "0")
                    SessionManager.Instance.CurrentProject.ReTargetProjectDeviceID = Convert.ToInt32(cmbDevice.SelectedValue);
                }

            }
            else
            {
                SessionManager.Instance.CurrentProject.DeviceFamilyProfilerOptionId = Convert.ToInt32(cmbSelectedPresets.SelectedValue);
                SessionManager.Instance.CurrentProject.ProjectOption = new List<ProjectOption>();
                var familyPreset =
                    ((List<DeviceFamilyProfilerOption>)ViewState[_vsDeviceFamilyProfilerOption]).Single(
                        x => x.DeviceFamilyProfilerOptionId.ToString() == cmbSelectedPresets.SelectedValue);

                //Start:-----If CustomSourceProfile or ReProfile Custom Source Type, then pass different set of preset options
                int MainProfileTypeID = 0;
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.CustomSourceProfile || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.ReProfileCustomSource)
                {
                    MainProfileTypeID = Convert.ToInt32(EProfileType.DestinationProfile);
                }
                else
                {
                    MainProfileTypeID = Convert.ToInt32(EProfileType.DeviceLinkProfile);
                }
                //End

                familyPreset.
                ProfilerOptionPresets.Where(x => x.ProfileTypeId == MainProfileTypeID).ToList().
                ForEach(x =>
                {
                    var option = (EProfilerOption)Enum.Parse(typeof(EProfilerOption), x.ProfilerOption.ProfilerOptionId.ToString());
                    switch (option)
                    {
                        case EProfilerOption.BlackGeneration:
                            //x.ProfilerOptionValue = rdsMode.Value.ToString();     //---Comment out right now to make it non- editable advanced options
                            x.ProfilerOptionValue = txtMode.Text;
                            break;
                        case EProfilerOption.BlackStart:
                            x.ProfilerOptionValue = rdsBlackStart.Value.ToString();
                            x.ProfilerOptionValue = txtBlackStart.Text;
                            break;
                        case EProfilerOption.BlackWidth:
                            x.ProfilerOptionValue = rdsBlackWidth.Value.ToString();
                            x.ProfilerOptionValue = txtBlackWidth.Text;
                            break;
                        case EProfilerOption.InkTotal:
                            //x.ProfilerOptionValue = rdsInkTotal.Value.ToString();     //---Comment out right now to make it non- editable advanced options
                            x.ProfilerOptionValue = txtInkTotal.Text;
                            break;
                        case EProfilerOption.InkMax:
                            x.ProfilerOptionValue = txtBlackPointCustom.Text;
                            break;
                    }
                });

                familyPreset.ProfilerOptionPresets.ForEach(x =>
                                                           SessionManager.Instance.CurrentProject.ProjectOption.
                                                               Add(new ProjectOption()
                                                               {
                                                                   ProfileTypeId = x.ProfileTypeId,
                                                                   ProfilerOptionId = x.ProfilerOption.ProfilerOptionId,
                                                                   ProfilerOptionValue = x.ProfilerOptionValue,
                                                               }));


            }

            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLPPlus)
            {
                SessionManager.Instance.CurrentProject.MetricTargetPresetId = Convert.ToInt32(ddlMetricPresets.SelectedValue);
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.MeasurementSetup;
            }
            else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimizationSource) //---If "Ink Optimization Source" project type, then skip Measurement and Validation setup pages.
            {
                //----Pass dummy data-----//
                SessionManager.Instance.CurrentProject.MeasurementSetting =
                       new MeasurementSetting()
                       {
                           Illuminant = 1,
                           ObserverType = 1,
                           MeasurementDeviceId = null,
                           ReferenceChartType = new KeyValuePair<short, string>(0, " "),
                           OptReferenceChartType = new KeyValuePair<short, string>(0, " "),
                           UVMode = 0,
                           MeasurementCount = 0,
                           ReadMode = null
                       };

                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Save;
            }
            else
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.MeasurementSetup;
            }
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }

        protected void rdsMode_ValueChanged(object sender, EventArgs e)
        {
            txtMode.Text = (sender as Telerik.Web.UI.RadSlider).Value.ToString();
        }

        protected void rdsBlackStart_ValueChanged(object sender, EventArgs e)
        {
            txtBlackStart.Text = (sender as Telerik.Web.UI.RadSlider).Value.ToString();
        }

        protected void rdsBlackWidth_ValueChanged(object sender, EventArgs e)
        {
            txtBlackWidth.Text = (sender as Telerik.Web.UI.RadSlider).Value.ToString();
        }

        protected void cmbDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDevice.SelectedValue.ToString() != "0")
            {
                BindReTargetList();
            }
            else
            {
                ddlReTargetList.SelectedValue = "0";
                divRetarget.Visible = false;
            }

            divRetargetDetails.Visible = false;
        }

        protected void ddlReTargetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReTargetList.SelectedValue.ToString() != "0")
            {
                BindRetargetProjectDetails();
            }
            else
            {
                divRetargetDetails.Visible = false;
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            short profilerTypeID = 2;
            short emptyLine = 0;
            if (fnUploadProfileSettings.UploadedFiles.Count > 0)
            {
                var sr = new StreamReader(fnUploadProfileSettings.UploadedFiles[0].InputStream);
                List<ProfilerOptionPreset> lstProfilerOptionPreset = new List<ProfilerOptionPreset>();
                var rows = 0;

                while (!sr.EndOfStream)
                {
                    bool bEmptyLine = false;
                    rows++;
                    ProfilerOptionPreset tmpProfilerOptionPreset = new ProfilerOptionPreset();
                    if (rows == 1)
                    {
                        #region Column Validation
                        var columns = sr.ReadLine().Split(',');
                        if (columns.Length != 3)
                        {
                            throw new Exception(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnCountNotMatch"));
                        }
                        columns.ToList().ForEach(x => x = x.Trim());
                        if (!columns[0].Equals("ProfilerOptionID", StringComparison.CurrentCultureIgnoreCase) ||
                            !columns[1].Equals("ProfilerOptionDesc", StringComparison.CurrentCultureIgnoreCase) ||
                            !columns[2].Equals("ProfilerOptionValue", StringComparison.CurrentCultureIgnoreCase)
                          )
                        {
                            throw new Exception(FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnNamesNotMatch"));
                        }
                    }
                    else
                    {
                        var dataRow = sr.ReadLine().Split(',');
                        if (dataRow.Length != 3)
                        {
                            throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnCountNotMatchOnRow"), rows));
                        }
                        dataRow.ToList().ForEach(x => x = x.Trim());
                        short profilerOptionID = 0;

                        if (string.IsNullOrEmpty(dataRow[1]) && string.IsNullOrEmpty(dataRow[2]) && string.IsNullOrEmpty(dataRow[1]))
                        { emptyLine++; bEmptyLine = true; }

                        //Empty line is given between profile types. if not more than 2 empty lines proceed
                        if ((emptyLine < 3) && !bEmptyLine)
                        {
                            if (short.TryParse(dataRow[0].ToString(), out profilerOptionID))
                            {
                                tmpProfilerOptionPreset.ProfilerOption = new ProfilerOption();
                                tmpProfilerOptionPreset.ProfilerOption.ProfilerOptionId = profilerOptionID;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //ProfilerOptionDesc is not needed but cannot be empty                               
                            if ((!string.IsNullOrEmpty(dataRow[2])) || (string.IsNullOrEmpty(dataRow[1])))
                            {
                                tmpProfilerOptionPreset.ProfilerOptionValue = dataRow[2];
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnEmptyValueOnRow"), rows));
                            }
                            #endregion

                            if (profilerOptionID == 1)
                            {
                                switch (dataRow[2].ToUpper())
                                {
                                    case "PRINTER":
                                        profilerTypeID = (short)EProfileType.DestinationProfile;
                                        break;
                                    case "DEVICELINK":
                                        profilerTypeID = (short)EProfileType.DeviceLinkProfile;
                                        break;
                                    case "OPTIMIZEDEVICELINK":
                                        profilerTypeID = (short)EProfileType.OptimizeDlp;
                                        break;
                                }
                            }

                            tmpProfilerOptionPreset.ProfileTypeId = profilerTypeID;
                            tmpProfilerOptionPreset.DeviceFamilyProfilerOptionId = 0;
                            tmpProfilerOptionPreset.ProfilerOptionPresetId = 0;

                            lstProfilerOptionPreset.Add(tmpProfilerOptionPreset);
                        }
                    }
                }
                AddProfilerOptionSettings(lstProfilerOptionPreset);
            }
            else
            {
                ///-----------Missing message for Upload .csv file
                fujiStatusbar.DisplayMessage(Common.ResourceManager.GetResourceMessage("Error_ProfilerSettings_MissingUploadFile"), false);
            }
        }

        protected void rdBtnPresetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.PrintCondition.IsCustomProfilerOption = Convert.ToBoolean(rdBtnPresetType.SelectedValue);
            if (SessionManager.Instance.CurrentProject.PrintCondition.IsCustomProfilerOption)
            {
                trSelectPreset.Visible = false;
                trPresetUplod.Visible = true;
                txtProfileDescription.Text = "";
            }
            else
            {
                trSelectPreset.Visible = true;
                trPresetUplod.Visible = false;
                BindPresets();
            }
        }

        #endregion

        #region Methods

        private List<Entity.CPSync1D.PrintStandardTVI> GetPrintStandardTVIList()
        {
            List<Entity.CPSync1D.PrintStandardTVI> printStandardTVIList = new List<Entity.CPSync1D.PrintStandardTVI>();

            #region ---For Cyan Color
            //--older codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_Quarter_Tone,
                    Type = CPPatchType.Cyan_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25CMY.Text.Trim()) ? "0" : txtTVI25CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_Half_Tone,
                    Type = CPPatchType.Cyan_Half_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50CMY.Text.Trim()) ? "0" : txtTVI50CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_Three_Quarter_Tone,
                    Type = CPPatchType.Cyan_Three_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75CMY.Text.Trim()) ? "0" : txtTVI75CMY.Text.Trim()))
            });
            //--newer Codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_10,
                    Type = CPPatchType.Cyan_10.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI10CMY.Text.Trim()) ? "0" : txtTVI10CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_20,
                    Type = CPPatchType.Cyan_20.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI20CMY.Text.Trim()) ? "0" : txtTVI20CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_25,
                    Type = CPPatchType.Cyan_25.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25CMY.Text.Trim()) ? "0" : txtTVI25CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_30,
                    Type = CPPatchType.Cyan_30.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI30CMY.Text.Trim()) ? "0" : txtTVI30CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_40,
                    Type = CPPatchType.Cyan_40.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI40CMY.Text.Trim()) ? "0" : txtTVI40CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_50,
                    Type = CPPatchType.Cyan_50.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50CMY.Text.Trim()) ? "0" : txtTVI50CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_60,
                    Type = CPPatchType.Cyan_60.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI60CMY.Text.Trim()) ? "0" : txtTVI60CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_70,
                    Type = CPPatchType.Cyan_70.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI70CMY.Text.Trim()) ? "0" : txtTVI70CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_75,
                    Type = CPPatchType.Cyan_75.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75CMY.Text.Trim()) ? "0" : txtTVI75CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_80,
                    Type = CPPatchType.Cyan_80.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI80CMY.Text.Trim()) ? "0" : txtTVI80CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan_90,
                    Type = CPPatchType.Cyan_90.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI90CMY.Text.Trim()) ? "0" : txtTVI90CMY.Text.Trim()))
            });
            #endregion

            #region ---For Magenta Color
            //--older codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_Quarter_Tone,
                    Type = CPPatchType.Magenta_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25CMY.Text.Trim()) ? "0" : txtTVI25CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_Half_Tone,
                    Type = CPPatchType.Magenta_Half_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50CMY.Text.Trim()) ? "0" : txtTVI50CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_Three_Quarter_Tone,
                    Type = CPPatchType.Magenta_Three_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75CMY.Text.Trim()) ? "0" : txtTVI75CMY.Text.Trim()))
            });
            //---newer codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_10,
                    Type = CPPatchType.Magenta_10.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI10CMY.Text.Trim()) ? "0" : txtTVI10CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_20,
                    Type = CPPatchType.Magenta_20.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI20CMY.Text.Trim()) ? "0" : txtTVI20CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_25,
                    Type = CPPatchType.Magenta_25.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25CMY.Text.Trim()) ? "0" : txtTVI25CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_30,
                    Type = CPPatchType.Magenta_30.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI30CMY.Text.Trim()) ? "0" : txtTVI30CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_40,
                    Type = CPPatchType.Magenta_40.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI40CMY.Text.Trim()) ? "0" : txtTVI40CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_50,
                    Type = CPPatchType.Magenta_50.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50CMY.Text.Trim()) ? "0" : txtTVI50CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_60,
                    Type = CPPatchType.Magenta_60.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI60CMY.Text.Trim()) ? "0" : txtTVI60CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_70,
                    Type = CPPatchType.Magenta_70.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI70CMY.Text.Trim()) ? "0" : txtTVI70CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_75,
                    Type = CPPatchType.Magenta_75.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75CMY.Text.Trim()) ? "0" : txtTVI75CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_80,
                    Type = CPPatchType.Magenta_80.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI80CMY.Text.Trim()) ? "0" : txtTVI80CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta_90,
                    Type = CPPatchType.Magenta_90.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI90CMY.Text.Trim()) ? "0" : txtTVI90CMY.Text.Trim()))
            });
            #endregion

            #region ---For Yellow Color
            //--older codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_Quarter_Tone,
                    Type = CPPatchType.Yellow_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25CMY.Text.Trim()) ? "0" : txtTVI25CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_Half_Tone,
                    Type = CPPatchType.Yellow_Half_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50CMY.Text.Trim()) ? "0" : txtTVI50CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_Three_Quarter_Tone,
                    Type = CPPatchType.Yellow_Three_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75CMY.Text.Trim()) ? "0" : txtTVI75CMY.Text.Trim()))
            });
            //--newer codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_10,
                    Type = CPPatchType.Yellow_10.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI10CMY.Text.Trim()) ? "0" : txtTVI10CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_20,
                    Type = CPPatchType.Yellow_20.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI20CMY.Text.Trim()) ? "0" : txtTVI20CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_25,
                    Type = CPPatchType.Yellow_25.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25CMY.Text.Trim()) ? "0" : txtTVI25CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_30,
                    Type = CPPatchType.Yellow_30.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI30CMY.Text.Trim()) ? "0" : txtTVI30CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_40,
                    Type = CPPatchType.Yellow_40.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI40CMY.Text.Trim()) ? "0" : txtTVI40CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_50,
                    Type = CPPatchType.Yellow_50.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50CMY.Text.Trim()) ? "0" : txtTVI50CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_60,
                    Type = CPPatchType.Yellow_60.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI60CMY.Text.Trim()) ? "0" : txtTVI60CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_70,
                    Type = CPPatchType.Yellow_70.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI70CMY.Text.Trim()) ? "0" : txtTVI70CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_75,
                    Type = CPPatchType.Yellow_75.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75CMY.Text.Trim()) ? "0" : txtTVI75CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_80,
                    Type = CPPatchType.Yellow_80.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI80CMY.Text.Trim()) ? "0" : txtTVI80CMY.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow_90,
                    Type = CPPatchType.Yellow_90.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI90CMY.Text.Trim()) ? "0" : txtTVI90CMY.Text.Trim()))
            });
            #endregion

            #region ---For Black Color
            //--older codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_Quarter_Tone,
                    Type = CPPatchType.Black_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25K.Text.Trim()) ? "0" : txtTVI25K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_Half_Tone,
                    Type = CPPatchType.Black_Half_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50K.Text.Trim()) ? "0" : txtTVI50K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_Three_Quarter_Tone,
                    Type = CPPatchType.Black_Three_Quarter_Tone.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75K.Text.Trim()) ? "0" : txtTVI75K.Text.Trim()))
            });
            //--newer codes
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_10,
                    Type = CPPatchType.Black_10.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI10K.Text.Trim()) ? "0" : txtTVI10K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_20,
                    Type = CPPatchType.Black_20.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI20K.Text.Trim()) ? "0" : txtTVI20K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_25,
                    Type = CPPatchType.Black_25.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25K.Text.Trim()) ? "0" : txtTVI25K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_30,
                    Type = CPPatchType.Black_30.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI30K.Text.Trim()) ? "0" : txtTVI30K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_40,
                    Type = CPPatchType.Black_40.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI40K.Text.Trim()) ? "0" : txtTVI40K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_50,
                    Type = CPPatchType.Black_50.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50K.Text.Trim()) ? "0" : txtTVI50K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_60,
                    Type = CPPatchType.Black_60.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI60K.Text.Trim()) ? "0" : txtTVI60K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_70,
                    Type = CPPatchType.Black_70.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI70K.Text.Trim()) ? "0" : txtTVI70K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_75,
                    Type = CPPatchType.Black_75.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75K.Text.Trim()) ? "0" : txtTVI75K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_80,
                    Type = CPPatchType.Black_80.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI80K.Text.Trim()) ? "0" : txtTVI80K.Text.Trim()))
            });
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black_90,
                    Type = CPPatchType.Black_90.ToString()
                },
                TVI = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI90K.Text.Trim()) ? "0" : txtTVI90K.Text.Trim()))
            });
            #endregion   

            return printStandardTVIList;
        }

        private List<Entity.CPSync1D.ProjectTargetDensity> GetProjectTargetDensityList()
        {
            List<Entity.CPSync1D.ProjectTargetDensity> ProjectTargetDensityList = new List<Entity.CPSync1D.ProjectTargetDensity>();

            #region ---For CMYK Colors
            ProjectTargetDensityList.Add(new Entity.CPSync1D.ProjectTargetDensity()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan,
                    Type = CPPatchType.Cyan.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtDensityC.Text.Trim()) ? "0" : txtDensityC.Text.Trim())),
            });
            ProjectTargetDensityList.Add(new Entity.CPSync1D.ProjectTargetDensity()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta,
                    Type = CPPatchType.Magenta.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtDensityM.Text.Trim()) ? "0" : txtDensityM.Text.Trim())),
            });
            ProjectTargetDensityList.Add(new Entity.CPSync1D.ProjectTargetDensity()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow,
                    Type = CPPatchType.Yellow.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtDensityY.Text.Trim()) ? "0" : txtDensityY.Text.Trim())),
            });
            ProjectTargetDensityList.Add(new Entity.CPSync1D.ProjectTargetDensity()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black,
                    Type = CPPatchType.Black.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtDensityK.Text.Trim()) ? "0" : txtDensityK.Text.Trim())),
            });
            #endregion

            return ProjectTargetDensityList;
        }

        private List<Entity.CPSync1D.ProjectTargetHueAngle> GetProjectTargetHueAngleList()
        {
            List<Entity.CPSync1D.ProjectTargetHueAngle> ProjectTargetHueAngleList = new List<Entity.CPSync1D.ProjectTargetHueAngle>();

            #region ---For CMYRGB Colors
            ProjectTargetHueAngleList.Add(new Entity.CPSync1D.ProjectTargetHueAngle()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan,
                    Type = CPPatchType.Cyan.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtHue_C.Text.Trim()) ? "0" : txtHue_C.Text.Trim())),
            });
            ProjectTargetHueAngleList.Add(new Entity.CPSync1D.ProjectTargetHueAngle()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta,
                    Type = CPPatchType.Magenta.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtHue_M.Text.Trim()) ? "0" : txtHue_M.Text.Trim())),
            });
            ProjectTargetHueAngleList.Add(new Entity.CPSync1D.ProjectTargetHueAngle()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow,
                    Type = CPPatchType.Yellow.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtHue_Y.Text.Trim()) ? "0" : txtHue_Y.Text.Trim())),
            });
            ProjectTargetHueAngleList.Add(new Entity.CPSync1D.ProjectTargetHueAngle()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Overprint_Red,
                    Type = CPPatchType.Overprint_Red.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtHue_R.Text.Trim()) ? "0" : txtHue_R.Text.Trim())),
            });
            ProjectTargetHueAngleList.Add(new Entity.CPSync1D.ProjectTargetHueAngle()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Overprint_Green,
                    Type = CPPatchType.Overprint_Green.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtHue_G.Text.Trim()) ? "0" : txtHue_G.Text.Trim())),
            });
            ProjectTargetHueAngleList.Add(new Entity.CPSync1D.ProjectTargetHueAngle()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Overprint_Blue,
                    Type = CPPatchType.Overprint_Blue.ToString()
                },
                Target = Convert.ToDecimal((string.IsNullOrEmpty(txtHue_B.Text.Trim()) ? "0" : txtHue_B.Text.Trim())),
            });
            #endregion

            return ProjectTargetHueAngleList;
        }


        private void ClearAdvancedOptionsControls()
        {
            cmbRendering.SelectedValue = "0";
            txtBlackPointCustom.Text = string.Empty;
            txtInkTotal.Text = string.Empty;
            rdsMode.Value = rdsMode.MinimumValue;
            txtMode.Text = string.Empty;
            rdsBlackStart.Value = rdsBlackStart.MinimumValue;
            txtBlackStart.Text = string.Empty;
            rdsBlackWidth.Value = rdsBlackWidth.MinimumValue;
            txtBlackWidth.Text = string.Empty;
            chkTriplex.Checked =
                chkDuplex.Checked =
                chkGray.Checked =
                chk100Black.Checked =
                chk100CMY.Checked =
                chk100RGB.Checked =
                chkMaxCMY.Checked =
                chkMaxRGB.Checked =
                chkPrimaries.Checked =
                chkPC.Checked =
                chkPM.Checked =
                chkPY.Checked =
                chkSecondaries.Checked =
                chkSCM.Checked =
                chkSCY.Checked =
                chkSMY.Checked =
                chkBlackOverPrint.Checked =
                chkBorderClipping.Checked =
                chkWhite.Checked =
                chk400Black.Checked = false;
            rdsRange.Value = rdsRange.MinimumValue;
            txtRange.Text = string.Empty;
        }

        private void ClearMetricPresetAdvancedOptionsControls()
        {

        }
        #endregion

        #region IViewDeviceFamilyProfilerOption Members
        public short DeviceFamilyId { get; set; }
        public List<DeviceFamilyProfilerOption> ProfileOption { get; set; }
        public int MetricTargetPresetId { get; set; }
        public List<Entity.CPSyncV.MetricSum> MetricSumTargetValues { get; set; }
        public int PrintStandardTypeId { get; set; }
        public List<Entity.CPSync1D.PrintStandardTVI> PrintStandardTVI { get; set; }
        public List<Entity.CPSync1D.ProjectTargetDensity> ProjectTargetDensity { get; set; }
        public List<Entity.CPSync1D.ProjectTargetHueAngle> ProjectTargetHueAngle { get; set; }
        public short LicenseTypeId { get; set; }
        public int UserID { get; set; }
        public bool IsCustomPreset { get; set; }
        #endregion

        #region IViewMeasurementSetting Members
        public List<Illuminant> Illumin
        {
            get;
            set;
        }

        public List<ObserverType> Observer
        {
            get;
            set;
        }

        public int DeviceId
        {
            get;
            set;
        }

        public List<MeasurementDevice> MeasurementDevices
        {
            get;
            set;
        }

        public int MeasurementDeviceId
        {
            get;
            set;
        }

        public List<ReferenceChartType> ReferenceChartType
        {
            get;
            set;
        }

        public short ReferenceChartTypeId
        {
            get;
            set;
        }

        public byte[] ReferenceChartTypeImage
        {
            get;
            set;
        }

        public int ProjectId
        {
            get;
            set;
        }

        public Project Project
        {
            get;
            set;
        }

        public int ChartDisplayType
        {
            get;
            set;
        }

        public List<Entity.CPSyncV.MetricTargetPreset> MetricTargetPreset
        {
            get;
            set;
        }

        public List<Entity.CPSync1D.ProjectTargetLAB> ProjectTargetLABList { get; set; }

        public int PrintstandardTypeId { get; set; }

        public int MetricTargetPresetID { get; set; }

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
        public bool IsVerification
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
            }
        }
        public Dictionary<int, string> ReTargetProjectList { get; set; }
        public byte[] ChartPreview { get; set; }
        public bool IsSpotTargetType
        {
            get
            {
                return (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary);
            }
        }
        public short PrintingTypeId { get; set; }
        public List<Device> Devices { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public int DeviceID { get; set; }
        public int RetargetProjectID { get; set; }
        public string RetargetPrintStandardTypeName { get; set; }
        public string RetargetMetricTargetPresetName { get; set; }

        public ProfilerOptionPreset ProfilerOptionPreset { get; set; }
        public DeviceFamilyProfilerOption DeviceFamilyProfilerOption { get; set; }
        public int DeviceFamilyProfilerOptionID { get; set; }
        public bool IsDLPPlus { get; set; }
        #endregion
    }
}
