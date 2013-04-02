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

        private const string _vsDeviceFamilyProfilerOption = "vsDeviceFamilyProfilerOption";
        private const string _vsDeviceFamilyMetricPresetOption = "vsDeviceFamilyMetricPresetOption";
        private const string _vsProfileRendering = "vsProfileRendering";
        private const string _vsMetricTargetPreset = "vsMetricTargetPreset";
        private PresenterDeviceFamilyProfilerOption _presenterDeviceFamilyProfilerOption;
        private PresenterMeasurementSetting _presenterMeasurementSetting;
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
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
            {
                //Sync1D
                tbOptionPreset.Visible = false;
                tbMetricPreset.Visible = true;
                BindMetricPresets();
                BindPrintStandardTVI();
            }
            else
            {
                tbOptionPreset.Visible = true;
                tbMetricPreset.Visible = false;
                BindProfileRenderings();
                BindPresets();
                LoadProjectInfoFromSession();
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

        private void BindPrintStandardTVI()
        {
            PrintStandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeID;
            _presenterDeviceFamilyProfilerOption.GetPrintStandardTVI();
            if (!IsErrorOccured)
            {
                CPPatchType cpPatchType;
                foreach (var item in PrintStandardTVI)
                {
                    cpPatchType = (CPPatchType)Enum.Parse(typeof(CPPatchType), item.PatchType.Type, true);
                    switch (cpPatchType)
                    {
                        case CPPatchType.Cyan:
                            txtTVI25Cyan.Text = item.TVI25.ToString();
                            txtTVI50Cyan.Text = item.TVI50.ToString();
                            txtTVI75Cyan.Text = item.TVI75.ToString();
                            break;
                        case CPPatchType.Magenta:
                            txtTVI25Magenta.Text = item.TVI25.ToString();
                            txtTVI50Magenta.Text = item.TVI50.ToString();
                            txtTVI75Magenta.Text = item.TVI75.ToString();
                            break;
                        case CPPatchType.Yellow:
                            txtTVI25Yellow.Text = item.TVI25.ToString();
                            txtTVI50Yellow.Text = item.TVI50.ToString();
                            txtTVI75Yellow.Text = item.TVI75.ToString();
                            break;
                        case CPPatchType.Black:
                            txtTVI25Black.Text = item.TVI25.ToString();
                            txtTVI50Black.Text = item.TVI50.ToString();
                            txtTVI75Black.Text = item.TVI75.ToString();
                            break;
                        case CPPatchType.Overprint_Red:
                            break;
                        case CPPatchType.Overprint_Green:
                            break;
                        case CPPatchType.Overprint_Blue:
                            break;
                        case CPPatchType.Paper:
                            break;
                        case CPPatchType.Triplet_Solid_Gray:
                            break;
                        case CPPatchType.Gray_C_M_Y:
                            break;
                        case CPPatchType.Other:
                            break;
                        case CPPatchType.Cyan_Tint:
                            break;
                        case CPPatchType.Magenta_Tint:
                            break;
                        case CPPatchType.Yellow_Tint:
                            break;
                        case CPPatchType.Black_Tint:
                            break;
                        case CPPatchType.Cyan_Quarter_Tone:
                            break;
                        case CPPatchType.Cyan_Half_Tone:
                            break;
                        case CPPatchType.Cyan_Three_Quarter_Tone:
                            break;
                        case CPPatchType.Magenta_Quarter_Tone:
                            break;
                        case CPPatchType.Magenta_Half_Tone:
                            break;
                        case CPPatchType.Magenta_Three_Quarter_Tone:
                            break;
                        case CPPatchType.Yellow_Quarter_Tone:
                            break;
                        case CPPatchType.Yellow_Half_Tone:
                            break;
                        case CPPatchType.Yellow_Three_Quarter_Tone:
                            break;
                        case CPPatchType.Black_Quarter_Tone:
                            break;
                        case CPPatchType.Black_Half_Tone:
                            break;
                        case CPPatchType.Black_Three_Quarter_Tone:
                            break;
                        case CPPatchType.Gray_25:
                            break;
                        case CPPatchType.Gray_50:
                            break;
                        case CPPatchType.Gray_75:
                            break;
                        default:
                            break;
                    }
                }
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
                                            //rdsMode.Value = Convert.ToDecimal(x.ProfilerOptionValue);
                                            //txtMode.Text = x.ProfilerOptionValue;
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
                SessionManager.Instance.CurrentProject.ProjectTypeId = ((List<Entity.CPSyncV.MetricTargetPreset>)ViewState[_vsMetricTargetPreset]).Where(c => c.MetricTargetPresetID == Convert.ToInt32(ddlMetricPresets.SelectedValue)).Select(c => c.ProjectTypeId).FirstOrDefault();
                SessionManager.Instance.CurrentProject.PrintStandardTVI = GetPrintStandardTVIList();
            }
            else
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
            }
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.MeasurementSetup;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }
        #endregion

        #region Methods

        private List<Entity.CPSync1D.PrintStandardTVI> GetPrintStandardTVIList()
        {
            List<Entity.CPSync1D.PrintStandardTVI> printStandardTVIList = new List<Entity.CPSync1D.PrintStandardTVI>();
            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Cyan,
                    Type = CPPatchType.Cyan.ToString()

                },
                TVI25 = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25Cyan.Text.Trim()) ? "0" : txtTVI25Cyan.Text.Trim())),
                TVI50 = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50Cyan.Text.Trim()) ? "0" : txtTVI25Cyan.Text.Trim())),
                TVI75 = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75Cyan.Text.Trim()) ? "0" : txtTVI25Cyan.Text.Trim()))
            });

            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Magenta,
                    Type = CPPatchType.Magenta.ToString()

                },
                TVI25 = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25Magenta.Text.Trim()) ? "0" : txtTVI25Magenta.Text.Trim())),
                TVI50 = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50Magenta.Text.Trim()) ? "0" : txtTVI25Magenta.Text.Trim())),
                TVI75 = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75Magenta.Text.Trim()) ? "0" : txtTVI25Magenta.Text.Trim()))
            });

            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Yellow,
                    Type = CPPatchType.Yellow.ToString()

                },
                TVI25 = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25Yellow.Text.Trim()) ? "0" : txtTVI25Yellow.Text.Trim())),
                TVI50 = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50Yellow.Text.Trim()) ? "0" : txtTVI25Yellow.Text.Trim())),
                TVI75 = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75Yellow.Text.Trim()) ? "0" : txtTVI25Yellow.Text.Trim()))
            });

            printStandardTVIList.Add(new Entity.CPSync1D.PrintStandardTVI()
            {
                PatchType = new PatchType()
                {
                    PatchTypeId = (int)CPPatchType.Black,
                    Type = CPPatchType.Black.ToString()

                },
                TVI25 = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI25Black.Text.Trim()) ? "0" : txtTVI25Black.Text.Trim())),
                TVI50 = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI50Black.Text.Trim()) ? "0" : txtTVI25Black.Text.Trim())),
                TVI75 = Convert.ToDecimal((string.IsNullOrEmpty(txtTVI75Black.Text.Trim()) ? "0" : txtTVI25Black.Text.Trim()))
            });

            return printStandardTVIList;
        }

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
        #endregion

    }
}
