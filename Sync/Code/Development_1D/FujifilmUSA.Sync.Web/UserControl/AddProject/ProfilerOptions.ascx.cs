using System;
using System.Linq;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Web.UI.WebControls;

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
                    BindReTargetList();

                    LoadProjectInfoFromSession();

                    if (ProjectTypeID == 1) //-----Option only for G7 Sync1D projects
                    {
                        Div_LinearizationType.Visible = true;
                    }
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
            if (ViewState[_vsDeviceFamilyProfilerOption] == null)
            {
                DeviceFamilyId = SessionManager.Instance.CurrentDevice.DeviceFamily.DeviceFamilyId;
                LicenseTypeId = (short)SessionManager.Instance.CurrentProject.LicenseType;
                _presenterDeviceFamilyProfilerOption.GetDeviceFamilyProfilerOption();
                if (!IsErrorOccured)
                {
                    cmbSelectedPresets.DataValueField = "DeviceFamilyProfilerOptionID";
                    cmbSelectedPresets.DataTextField = "ProfilerOptionsName";
                    ViewState[_vsDeviceFamilyProfilerOption] = ProfileOption;
                    cmbSelectedPresets.DataSource = ViewState[_vsDeviceFamilyProfilerOption];
                    cmbSelectedPresets.DataBind();
                    cmbSelectedPresets_SelectedIndexChanged(null, null);
                }
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
                        ddlReTargetList.SelectedValue = SessionManager.Instance.CurrentProject.ReTargetProjectID.ToString();
                    }
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0) //Once project is saved, Project type selection is disable
                    {
                        ddlReTargetList.Enabled = false;
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
                            rdsMode.Value = Convert.ToDecimal(SessionManager.Instance.CurrentProject.ProjectOption[indexMode].ProfilerOptionValue);
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
                            rdsInkTotal.Value = Convert.ToDecimal(SessionManager.Instance.CurrentProject.ProjectOption[indexInkTotal].ProfilerOptionValue);
                        }
                        int indexInkMax = SessionManager.Instance.CurrentProject.ProjectOption.FindIndex(item => item.ProfilerOptionId == 18);
                        if (indexInkMax > 0)
                        {
                            txtBlackPointCustom.Text = SessionManager.Instance.CurrentProject.ProjectOption[indexInkMax].ProfilerOptionValue;
                        }
                    }
                }
            }
        }
      
        private void BindMetricPresets()
        {           
            if (ViewState[_vsMetricTargetPreset] == null)
            {
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
            _presenterDeviceFamilyProfilerOption.GetMetricSumTargetValues();
            if (!IsErrorOccured)
            {
                lvTolerances.DataSource = MetricSumTargetValues;
                lvTolerances.DataBind();
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
                    case "Cyan10": case "Cyan_10":
                        txtTVI10CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan20": case "Cyan_20":
                        txtTVI20CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan25": case "Cyan_25": case "Cyan Quarter Tone": case "Cyan_Quarter_Tone":
                        txtTVI25CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan30": case "Cyan_30":
                        txtTVI30CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan40": case "Cyan_40":
                        txtTVI40CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan50": case "Cyan_50": case "Cyan Half Tone": case "Cyan_Half_Tone":
                        txtTVI50CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan60": case "Cyan_60":
                        txtTVI60CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan70": case "Cyan_70":
                        txtTVI70CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan75": case "Cyan_75": case "Cyan Three Quarter Tone": case "Cyan_Three_Quarter_Tone":
                        txtTVI75CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan80": case "Cyan_80":
                        txtTVI80CMY.Text = item.TVI.ToString();
                        break;
                    case "Cyan90": case "Cyan_90":
                        txtTVI90CMY.Text = item.TVI.ToString();
                        break;
                    //----For Black patches----//
                    case "Black10": case "Black_10":
                        txtTVI10K.Text = item.TVI.ToString();
                        break;
                    case "Black20": case "Black_20":
                        txtTVI20K.Text = item.TVI.ToString();
                        break;
                    case "Black25": case "Black_25": case "Black Quarter Tone": case "Black_Quarter_Tone":
                        txtTVI25K.Text = item.TVI.ToString();
                        break;
                    case "Black30": case "Black_30":
                        txtTVI30K.Text = item.TVI.ToString();
                        break;
                    case "Black40": case "Black_40":
                        txtTVI40K.Text = item.TVI.ToString();
                        break;
                    case "Black50": case "Black_50": case "Black Half Tone": case "Black_Half_Tone":
                        txtTVI50K.Text = item.TVI.ToString();
                        break;
                    case "Black60": case "Black_60":
                        txtTVI60K.Text = item.TVI.ToString();
                        break;
                    case "Black70": case "Black_70":
                        txtTVI70K.Text = item.TVI.ToString();
                        break;
                    case "Black75": case "Black_75": case "Black Three Quarter Tone": case "Black_Three_Quarter_Tone":
                        txtTVI75K.Text = item.TVI.ToString();
                        break;
                    case "Black80": case "Black_80":
                        txtTVI80K.Text = item.TVI.ToString();
                        break;
                    case "Black90": case "Black_90":
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

        private void BindReTargetList()
        {
            DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
          
            _presenterMeasurementSetting.GetReTargetProjectList();
            if (!IsErrorOccured)
            {
                ddlReTargetList.DataValueField = "Key";
                ddlReTargetList.DataTextField = "Value";
                ddlReTargetList.DataSource = ReTargetProjectList;
                ddlReTargetList.DataBind();
                ddlReTargetList.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), "0"));
            }
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

                txtProfileDescription.Text = familyOption.ProfilerOptionsDesc;
                familyOption.
                    ProfilerOptionPresets.Where(x => x.ProfileTypeId == Convert.ToInt32(EProfileType.DeviceLinkProfile)).ToList().
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
                                            rdsInkTotal.Value = Convert.ToDecimal(x.ProfilerOptionValue);
                                            txtInkTotal.Text = x.ProfilerOptionValue;
                                            break;
                                        case EProfilerOption.BlackGeneration:
                                            rdsMode.Value = Convert.ToDecimal(x.ProfilerOptionValue);
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
                SessionManager.Instance.CurrentProject.ReTargetProjectID = Convert.ToInt32(ddlReTargetList.SelectedValue);

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
                }
                
            }
            else
            {
                SessionManager.Instance.CurrentProject.DeviceFamilyProfilerOptionId = Convert.ToInt32(cmbSelectedPresets.SelectedValue);
                SessionManager.Instance.CurrentProject.ProjectOption = new List<ProjectOption>();
                var familyPreset =
                    ((List<DeviceFamilyProfilerOption>)ViewState[_vsDeviceFamilyProfilerOption]).Single(
                        x => x.DeviceFamilyProfilerOptionId.ToString() == cmbSelectedPresets.SelectedValue);

                familyPreset.
                ProfilerOptionPresets.Where(x => x.ProfileTypeId == Convert.ToInt32(EProfileType.DeviceLinkProfile)).ToList().
                ForEach(x =>
                {
                    var option = (EProfilerOption)Enum.Parse(typeof(EProfilerOption), x.ProfilerOption.ProfilerOptionId.ToString());
                    switch (option)
                    {
                        case EProfilerOption.BlackGeneration:
                            x.ProfilerOptionValue = rdsMode.Value.ToString();
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
                            x.ProfilerOptionValue = rdsInkTotal.Value.ToString();
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

            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimizationSource) //---If "Ink Optimization Source" project type, then skip Measurement and Validation setup pages.
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

        protected void rdsInkTotal_ValueChanged(object sender, EventArgs e)
        {
            txtInkTotal.Text = (sender as Telerik.Web.UI.RadSlider).Value.ToString();
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
        public short LicenseTypeId { get; set; }
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
        #endregion

    }
}
