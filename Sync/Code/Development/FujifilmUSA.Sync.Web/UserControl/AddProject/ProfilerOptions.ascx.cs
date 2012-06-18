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
    public partial class ProfilerOptions : BaseControl, IViewDeviceFamilyProfilerOption
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

        private const string _vsDeviceFamilyProfilerOption = "vsDeviceFamilyProfilerOption";
        private const string _vsProfileRendering = "vsProfileRendering";
        private PresenterDeviceFamilyProfilerOption _presenterDeviceFamilyProfilerOption;
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterDeviceFamilyProfilerOption = new PresenterDeviceFamilyProfilerOption(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lnkbtnTargetColorSpace.Visible = IsXmfNewProject;
            BindProfileRenderings();
            BindPresets();
            LoadProjectInfoFromSession();
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
            if (SessionManager.Instance.CurrentProject != null && SessionManager.Instance.CurrentProject.DeviceFamilyProfilerOptionId != 0)
            {
                cmbSelectedPresets.SelectedValue = SessionManager.Instance.CurrentProject.DeviceFamilyProfilerOptionId.ToString();
                cmbSelectedPresets_SelectedIndexChanged(null, null);
                //cmbSelectedPresets.Enabled = !(SessionManager.Instance.CurrentProject.OptimizeRuns != null &&
                //                              SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0);
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
                                            txtMaxInk.Text = x.ProfilerOptionValue;
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

        protected void lnkbtnTargetColorSpace_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.TargetColorSpace;
            Response.Redirect(string.Format("~/COAddProject.aspx?id={0}", Guid.NewGuid()));
        }

        protected void lnkbtnProfilerOptions_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.DeviceFamilyProfilerOptionId = Convert.ToInt32(cmbSelectedPresets.SelectedValue);
            SessionManager.Instance.CurrentProject.ProjectOption = new List<ProjectOption>();
            var familyPreset =
                ((List<DeviceFamilyProfilerOption>)ViewState[_vsDeviceFamilyProfilerOption]).Single(
                    x => x.DeviceFamilyProfilerOptionId.ToString() == cmbSelectedPresets.SelectedValue);
            familyPreset.ProfilerOptionPresets.ForEach(x =>
                                                       SessionManager.Instance.CurrentProject.ProjectOption.
                                                           Add(new ProjectOption()
                                                                   {
                                                                       ProfileTypeId = x.ProfileTypeId,
                                                                       ProfilerOptionId = x.ProfilerOption.ProfilerOptionId,
                                                                       ProfilerOptionValue = x.ProfilerOptionValue,
                                                                   }));
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.MeasurementSetup;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }
        #endregion

        #region Methods
        private void ClearAdvancedOptionsControls()
        {
            cmbRendering.SelectedValue = "0";
            txtBlackPointCustom.Text = string.Empty;
            txtMaxInk.Text = string.Empty;
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
        #endregion

        #region IViewDeviceFamilyProfilerOption Members
        public short DeviceFamilyId { get; set; }
        public List<DeviceFamilyProfilerOption> ProfileOption { get; set; }
        #endregion
    }
}
