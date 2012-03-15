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
    public partial class ProfilerOptions : System.Web.UI.UserControl, IViewDeviceFamilyProfilerOption
    {
        #region "Declaration"
        //private PresenterProjectList _presenterProjectList;
        private string _vsDeviceFamilyProfilerOption = "vsDeviceFamilyProfilerOption";
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
        }
        #endregion

        #region Method
        private void BindPresets()
        {
            if (ViewState[_vsDeviceFamilyProfilerOption] == null)
            {
                DeviceFamilyId = SessionManager.Instance.CurrentDevice.DeviceFamily.DeviceFamilyId;
                _presenterDeviceFamilyProfilerOption.GetDeviceFamilyProfilerOption();
                cmbSelectedPresets.DataValueField = "DeviceFamilyProfilerOptionID";
                cmbSelectedPresets.DataTextField = "ProfilerOptionsName";
                ViewState[_vsDeviceFamilyProfilerOption] = ProfileOption;
                cmbSelectedPresets.DataSource = (List<DeviceFamilyProfilerOption>)ViewState[_vsDeviceFamilyProfilerOption];
                cmbSelectedPresets.DataBind();
                cmbSelectedPresets_SelectedIndexChanged(null, null);
            }
            
            //var dic = new Dictionary<int, string> { { 1, "Preserved Black" } };
            //cmbSelectedPresets.DataSource = dic;
            //cmbSelectedPresets.DataTextField = "Value";
            //cmbSelectedPresets.DataValueField = "Key";
            //cmbSelectedPresets.DataBind();
        }
        #endregion

        #region Method
        protected void lnkbtnProfilerOptions_Click(object sender, EventArgs e)
        {
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
                //txtProfileDescription.Text = "Hi";
                txtProfileDescription.Text = ((List<DeviceFamilyProfilerOption>)ViewState[_vsDeviceFamilyProfilerOption]).Single(x => x.DeviceFamilyProfilerOptionId.ToString() == cmbSelectedPresets.SelectedValue).ProfilerOptionsDesc;
            }
        }

    }
}
