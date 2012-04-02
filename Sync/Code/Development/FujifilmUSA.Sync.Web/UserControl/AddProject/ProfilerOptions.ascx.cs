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
        //private PresenterProjectList _presenterProjectList;
        private const string _vsDeviceFamilyProfilerOption = "vsDeviceFamilyProfilerOption";
        private PresenterDeviceFamilyProfilerOption _presenterDeviceFamilyProfilerOption;
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterDeviceFamilyProfilerOption = new PresenterDeviceFamilyProfilerOption(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            BindPresets();
            LoadProjectInfoFromSession();      
        }
        #endregion

        #region Method
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

        #region Method
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

        #region IViewDeviceFamilyProfilerOption Members
        public short DeviceFamilyId { get; set; }
        public List<DeviceFamilyProfilerOption> ProfileOption { get; set; }
        #endregion

        protected void cmbSelectedPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewState[_vsDeviceFamilyProfilerOption] != null)
            {
                txtProfileDescription.Text = ((List<DeviceFamilyProfilerOption>)ViewState[_vsDeviceFamilyProfilerOption]).Single(x => x.DeviceFamilyProfilerOptionId.ToString() == cmbSelectedPresets.SelectedValue).ProfilerOptionsDesc;
            }
        }
    }
}
